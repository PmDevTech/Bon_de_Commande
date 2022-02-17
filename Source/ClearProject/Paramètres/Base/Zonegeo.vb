Imports System.Data
Imports MySql.Data.MySqlClient
Imports System.Data.DataSet
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports System.IO.Ports
Imports DevExpress.XtraTreeList.Nodes
Imports DevExpress.XtraTreeList
Public Class Zonegeo
    Dim dtzonegeo = New DataTable
    Dim DrX As DataRow
    Dim IdPays() As Integer
    Dim IdType() As Integer
    Dim Modifie = False

    Private Sub RemplirCmbTypZone()


        'query = "select CodeZone, LibelleZone from T_ZoneGeo where NiveauStr='1'"
        'ComboPays.Properties.Items.Clear()
        'Dim dt = ExcecuteSelectQuery(query)
        'Dim CodPays As Integer = 0
        'ReDim TabCodePays(dt.Rows.Count)
        'For Each rw In dt.Rows
        '    TabCodePays(CodPays) = rw("CodeZone").ToString
        '    CodPays += 1
        '    ComboPays.Properties.Items.Add(MettreApost(rw("LibelleZone").ToString))
        'Next


        Try
            If CmbTypZone.SelectedIndex = -1 Then
                CmbTypZone.Properties.Items.Clear()
                query = "select NiveauStr, LibelleStr from T_StructGeo ORDER BY NiveauStr"
                Dim dt0 As DataTable = ExcecuteSelectQuery(query)
                ReDim IdType(dt0.Rows.Count)
                Dim i As Integer = 0
                For Each rw As DataRow In dt0.Rows
                    CmbTypZone.Properties.Items.Add(MettreApost(rw("LibelleStr").ToString))
                    IdType(i) = rw("NiveauStr").ToString
                    i += 1
                Next
            End If
        Catch ex As Exception
            FailMsg("Code Erreur 0X0002 " & vbNewLine & ex.ToString())
        End Try
    End Sub

    Private Sub RemplirCmbPays_de()

        Try
            If CmbPays_de.SelectedIndex = -1 Then
                CmbPays_de.Properties.Items.Clear()
                query = "select CodeZone, LibelleZone from T_ZoneGeo WHERE NiveauStr='1'"
                Dim dt0 As DataTable = ExcecuteSelectQuery(query)
                ReDim IdPays(dt0.Rows.Count)
                Dim i As Integer = 0

                For Each rw As DataRow In dt0.Rows
                    CmbPays_de.Properties.Items.Add(MettreApost(rw("LibelleZone").ToString))
                    IdPays(i) = rw("CodeZone").ToString
                    i += 1

                Next
            End If
        Catch ex As Exception
            FailMsg("Code Erreur 0X0002 " & vbNewLine & ex.ToString())
        End Try
    End Sub


    Private Sub RemplirCmbDevise()
        Try
            If CmbDevise.SelectedIndex = -1 Then
                CmbDevise.Properties.Items.Clear()
               query= "select LibelleDevise from T_Devise ORDER BY LibelleDevise"
                Dim dt0 As DataTable = ExcecuteSelectQuery(query)
                For Each rw As DataRow In dt0.Rows
                    CmbDevise.Properties.Items.Add(MettreApost(rw(0).ToString))
                Next
            End If
        Catch ex As Exception
            FailMsg("Code Erreur 0X0003 " & vbNewLine & ex.ToString())
        End Try
    End Sub
    Private Sub CmbIssu_de_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbIssu_de.SelectedIndexChanged
        Try
            Dim LibelZ As String = CmbIssu_de.Text
            CorrectionChaine(LibelZ)
            query = "select CodeZone, IndicZone, CodeDevise, TVA from T_ZoneGeo where LibelleZone='" & EnleverApost(LibelZ) & "'"
            Dim dt0 As DataTable = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt0.Rows

                TxtCodeZoneMereCache.Text = rw(0)
                TxtIndicatifZone.Text = rw(1)
                TxtCodeDeviseCache.Text = rw(2)
                TxtTva.Text = rw(3)
                Dim LibD As String = ""

                'Recherche de la devise***************
                query = "select LibelleDevise from T_Devise where CodeDevise='" & rw(2) & "'"
                Dim dt1 = ExcecuteSelectQuery(query)
                For Each rw1 As DataRow In dt1.Rows
                    LibD = rw1(0)
                    RestaurerChaine(LibD)
                Next
                CmbDevise.Text = LibD
                ActualiserDevise.Enabled = False
            Next
        Catch ex As Exception
            FailMsg("Code Erreur 0XIT_Z_MERE0001 " & vbNewLine & ex.ToString())
        End Try
    End Sub
    Private Sub CmbDevise_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbDevise.SelectedIndexChanged
        Try
            Dim LibelD As String = CmbDevise.Text
            CorrectionChaine(LibelD)
            query = "select CodeDevise from T_Devise where LibelleDevise='" & LibelD & "'"
            Dim dt0 As DataTable = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt0.Rows
                TxtCodeDeviseCache.Text = rw(0)
            Next
        Catch ex As Exception
            FailMsg("Code Erreur 0XIT_DEV_CODE0001 " & vbNewLine & ex.ToString())
        End Try
    End Sub

    Private Sub CmbTypZone_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbTypZone.SelectedIndexChanged
        'Dim Temp0, Temp As Decimal
        Try


            If CmbTypZone.SelectedIndex > -1 Then
                Dim LibelZ As String = CmbTypZone.Text
                CorrectionChaine(LibelZ)
                query = "select NiveauStr, LibelleStr from T_StructGeo WHERE LibelleStr = '" & CmbTypZone.Text & "'"
                Dim dt0 As DataTable = ExcecuteSelectQuery(query)
                For Each rw As DataRow In dt0.Rows
                    TxtNiveauStrCache.Text = rw(0)
                Next
                If dt0.Rows.Count = 0 Then
                    Initialiser()
                    Exit Sub
                End If


                'Dim LibelD As String = CmbDevise.Text
                'CorrectionChaine(LibelD)
                'query = "select CodeDevise from T_Devise where LibelleDevise='" & LibelD & "'"
                'Dim dt0 As DataTable = ExcecuteSelectQuery(query)
                'For Each rw As DataRow In dt0.Rows
                '    TxtCodeDeviseCache.Text = rw(0)
                'Next



                'Dim rwa As DataRow = dt0.Rows(0)
                'Temp0 = rwa(0)
                'TxtNiveauStrCache.Text = Temp0.ToString
                'Temp = Temp0 - 1

                'If Temp > 0 Then
                '    query = "select NiveauStr, LibelleZone from T_ZoneGeo WHERE NiveauStr <= '" & Temp & "';"
                '    If (Temp0 = 5) Then
                '        query = "select NiveauStr, LibelleZone from T_ZoneGeo WHERE NiveauStr >= '2' and NiveauStr <= '4'"
                '    End If
                '    If (Temp0 = 6) Then
                '        query = "select NiveauStr, LibelleZone from T_ZoneGeo WHERE NiveauStr >= '4' and NiveauStr <= '5'"
                '    End If
                '    If (Temp0 = 7) Then
                '        query = "select NiveauStr, LibelleZone from T_ZoneGeo WHERE NiveauStr >= '4' and NiveauStr <= '6'"
                '    End If
                '    If (Temp0 = 8) Then
                '        query = "select NiveauStr, LibelleZone from T_ZoneGeo WHERE NiveauStr = '4' or NiveauStr = '6'"
                '    End If
                'Else
                '    'Temp = 1
                '    query = "select NiveauStr, LibelleZone from T_ZoneGeo WHERE NiveauStr = '" & Temp & "';"
                'End If

                'dt0 = ExcecuteSelectQuery(query)
                'CmbIssu_de.Properties.Items.Clear()
                'CmbIssu_de.Text = ""
                'For Each rw As DataRow In dt0.Rows
                '    CmbIssu_de.Properties.Items.Add(MettreApost(rw(1).ToString))
                'Next
                If CmbTypZone.Text.ToLower = "pays" Then
                    TxtIndicatifZone.Enabled = True
                    CmbDevise.Enabled = True
                    TxtTva.Enabled = True
                    CmbIssu_de.Size = New Point(202, 26)
                    CmbIssu_de.Enabled = False
                    CmbPays_de.Size = New Point(420, 26)
                    CmbPays_de.Enabled = False
                    ActualiserDevise.Enabled = True
                ElseIf CmbTypZone.Text.ToLower = "district" Then
                    CmbIssu_de.Size = New Point(202, 26)
                    CmbIssu_de.Enabled = False
                    CmbPays_de.Size = New Point(420, 26)
                    CmbPays_de.Enabled = True
                    CmbDevise.Enabled = False
                    TxtIndicatifZone.Enabled = False
                    TxtTva.Enabled = False

                ElseIf CmbTypZone.Text.ToLower = "region" Or CmbTypZone.Text.ToLower = "departement" Or CmbTypZone.Text.ToLower = "sous-prefecture" Or CmbTypZone.Text.ToLower = "commune" Or CmbTypZone.Text.ToLower = "village" Then
                    CmbIssu_de.Enabled = True
                    CmbIssu_de.Size = New Point(202, 26)
                    CmbPays_de.Enabled = True
                    CmbPays_de.Size = New Point(214, 26)
                    CmbDevise.Enabled = False
                    TxtIndicatifZone.Enabled = False
                    TxtTva.Enabled = False
                Else
                    TxtIndicatifZone.Enabled = False
                    CmbDevise.Enabled = False
                    CmbIssu_de.Enabled = True
                    CmbPays_de.Enabled = True
                    TxtTva.Enabled = False
                    ActualiserDevise.Enabled = False
                End If
                If CmbTypZone.Text.ToLower = "" Then
                    CmbPays_de.Text = ""
                    CmbIssu_de.Text = ""
                Else
                    CmbPays_de.Text = ""
                    CmbIssu_de.Text = ""
                End If



                TxtNomZone.Enabled = True
                TxtAbrege.Enabled = True
                TxtTva.Enabled = True
            Else
                Initialiser()
            End If
            RemplirCmbPays_de()
        Catch ex As Exception
            FailMsg("Code Erreur 0X0004 " & vbNewLine & ex.ToString())
        End Try
    End Sub

    Private Sub ActualiserDevise_Click(sender As System.Object, e As System.EventArgs) Handles ActualiserDevise.Click
        Dialog_form(Devise)
        RemplirCmbDevise()
    End Sub

    Private Sub BtEnregistrer_Click(sender As System.Object, e As System.EventArgs) Handles BtEnregistrer.Click

        Try
            If (CmbTypZone.Text <> "" And CmbDevise.Text <> "") Then

                If Modifie = False Then

                    If CmbTypZone.SelectedIndex = -1 Then
                        SuccesMsg("Veuillez choisir le type de zone dans la liste.")
                        Exit Sub
                    End If

                    If (CmbDevise.SelectedIndex = -1) And CmbDevise.Enabled Then
                        SuccesMsg("Veuillez choisir la devise dans la liste.")
                        Exit Sub
                    End If

                    If (CmbPays_de.SelectedIndex = -1) And CmbIssu_de.Enabled Then
                        SuccesMsg("Veuillez choisir le pays dans la liste.")
                        Exit Sub
                    End If

                    If (CmbIssu_de.SelectedIndex = -1) And CmbIssu_de.Enabled Then
                        SuccesMsg("Veuillez choisir zone supérieure dans la liste.")
                        Exit Sub
                    End If

                    If (CmbTypZone.Text <> "" And TxtNomZone.Text.Trim() <> "" And TxtAbrege.Text.Trim() <> "" And TxtIndicatifZone.Text.Trim() <> "" And CmbDevise.Text <> "") Then

                        Dim codemere As Decimal = 0
                        codemere = IIf(TxtCodeZoneMereCache.Text = "", 0, TxtCodeZoneMereCache.Text)

                        query = "insert into t_zonegeo values (null,'" & EnleverApost(TxtNomZone.Text) & "','" & EnleverApost(TxtAbrege.Text) & "','" & codemere.ToString & "','" & (TxtIndicatifZone.Text) & "','" & (TxtCodeDeviseCache.Text) & "','" & (TxtNiveauStrCache.Text) & "','" & (TxtTva.Text) & "')"
                        ExecuteNonQuery(query)
                        SuccesMsg("Enregistrement éffectué avec succès")
                        AjouterZones(0)
                        Initialiser()
                    Else
                        SuccesMsg("Veuillez remplir tous les champs disponibles svp!")
                    End If

                Else
                    Dim code As Integer
                    code = TreeList1.FocusedNode.GetValue("CodeZone")
                    If (ConfirmMsg("Voulez-vous modifier la zone géographique?") = DialogResult.Yes) Then
                        If TreeList1.FocusedNode.GetValue("Type") = "PAYS" Then
                            query = "update t_zonegeo set LibelleZone='" & EnleverApost(TxtNomZone.Text) & "', AbregeZone='" & EnleverApost(TxtAbrege.Text) & "', IndicZone='" & (TxtIndicatifZone.Text).ToString & "', CodeDevise='" & (TxtCodeDeviseCache.Text).ToString & "', NiveauStr='" & (TxtNiveauStrCache.Text).ToString & "', TVA='" & (TxtTva.Text).ToString & "'Where CodeZone='" & code & "'"
                            ExecuteNonQuery(query)
                        Else
                            query = "update t_zonegeo set LibelleZone='" & EnleverApost(TxtNomZone.Text) & "', AbregeZone='" & EnleverApost(TxtAbrege.Text) & "', IndicZone='" & (TxtIndicatifZone.Text).ToString & "', TVA='" & (TxtTva.Text).ToString & "'Where CodeZone='" & code & "'"
                            ExecuteNonQuery(query)
                        End If
                        TreeList1.FocusedNode.SetValue("NomZone", TxtNomZone.Text)
                        TreeList1.FocusedNode.SetValue("Code", TxtAbrege.Text)
                        TreeList1.FocusedNode.SetValue("Indicatif", TxtIndicatifZone.Text)
                        TreeList1.FocusedNode.SetValue("Devises", CmbDevise.Text)
                        TreeList1.FocusedNode.SetValue("Tva", TxtTva.Text)
                        SuccesMsg("Modification effectuée avec succès")
                        Initialiser()
                    End If
                    Modifie = True
                End If
            End If
        Catch ex As Exception
            FailMsg("Code Erreur 0X0004 " & vbNewLine & ex.ToString())
        End Try


    End Sub


    Private Sub Zonegeo_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Initialiser()
    End Sub

    Private Sub Zonegeo_Load(sender As System.Object, e As System.EventArgs)  Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        RemplirCmbPays_de()
        RemplirCmbDevise()
        RemplirCmbTypZone()
        remplirdatagridzonegeo()
        AjouterZones(0)
        CmbIssu_de.Enabled = False
        CmbPays_de.Size = New Point(420, 26)
        CmbPays_de.Enabled = False


    End Sub

    Private Sub BtRetour_Click(sender As System.Object, e As System.EventArgs) Handles BtRetour.Click
        CmbTypZone.Enabled = True
        Initialiser()
        AjouterZones(0)
        Modifie = False
    End Sub

    Private Sub AjouterZones(ByVal CodeZoneMere As Integer, Optional parent As TreeListNode = Nothing)

        query = "SELECT Z.CodeZone, Z.AbregeZone, Z.NiveauStr, Z.IndicZone, Z.LibelleZone, Z.CodeZoneMere,D.LibelleDevise, S.LibelleStr, Z.CodeDevise, Z.TVA FROM T_ZoneGeo As Z, T_StructGeo As S, T_Devise As D WHERE Z.NiveauStr=S.NiveauStr and Z.CodeDevise=D.CodeDevise and Z.CodeZoneMere= '" & CodeZoneMere & "' "
        Dim dt = ExcecuteSelectQuery(query)
        TreeList1.BeginUnboundLoad()
        If Not IsNothing(parent) Then
            parent.Nodes.Clear()
        Else
            TreeList1.Nodes.Clear()
        End If

        For Each rw In dt.Rows
            Dim rootNode As TreeListNode = TreeList1.AppendNode(New Object() {rw("AbregeZone").ToString, rw("IndicZone").ToString, MettreApost(rw("LibelleZone").ToString), rw("LibelleDevise").ToString, rw("TVA").ToString, MettreApost(rw("LibelleStr")), rw("CodeZone").ToString, rw("CodeZoneMere").ToString, "0"}, parent)
            TreeList1.AppendNode(New Object() {"", "", "", "", "", "", "", "", ""}, rootNode)

        Next
        TreeList1.EndUnboundLoad()
    End Sub

    Private Sub Initialiser()
        CmbTypZone.Text = "pays".ToUpper
        RemplirCmbTypZone()
        CmbIssu_de.Properties.Items.Clear()
        CmbIssu_de.Text = ""
        CmbIssu_de.Enabled = False
        CmbPays_de.Text = ""
        CmbPays_de.Enabled = False
        CmbDevise.Text = ""
        TxtNomZone.Text = ""
        TxtAbrege.Text = ""
        TxtIndicatifZone.Text = ""
        TxtTva.Text = ""
        TxtCodeZone.Text = ""
        RemplirCmbDevise()

        TxtNiveauStrCache.Text = "1"
        TxtCodeZoneMereCache.Text = ""
        TxtCodeDeviseCache.Text = ""
    End Sub

    Private Sub remplirdatagridzonegeo()
        'Dim CodeDev As Decimal
        'Dim LibDev As String
        'Dim Codz As Decimal

        'dtzonegeo.Columns.Clear()
        'dtzonegeo.Columns.Add("CodeZone", Type.GetType("System.String"))
        'dtzonegeo.Columns.Add("Code", Type.GetType("System.String"))
        'dtzonegeo.Columns.Add("Indicatif", Type.GetType("System.String"))
        'dtzonegeo.Columns.Add("Nom Zone", Type.GetType("System.String"))
        'dtzonegeo.Columns.Add("Type Zone", Type.GetType("System.String"))
        'dtzonegeo.Columns.Add("Issue de", Type.GetType("System.String"))
        'dtzonegeo.Columns.Add("Devise", Type.GetType("System.String"))
        'dtzonegeo.Columns.Add("TVA", Type.GetType("System.String"))
        'dtzonegeo.Columns.Add("CodeMere", Type.GetType("System.String"))
        'dtzonegeo.Rows.Clear()

        ''remplir le datagrid

        'query = "SELECT Z.CodeZone, Z.AbregeZone, Z.IndicZone, Z.LibelleZone, Z.CodeZoneMere, S.LibelleStr, Z.CodeDevise, Z.TVA FROM T_ZoneGeo As Z, T_StructGeo As S WHERE Z.NiveauStr=S.NiveauStr and Z.NiveauStr=1"
        'Dim dt0 As DataTable = ExcecuteSelectQuery(query)
        'For Each rw As DataRow In dt0.Rows
        '    Dim drS = dtzonegeo.NewRow()
        '    drS(0) = rw(0).ToString
        '    drS(1) = rw(1).ToString
        '    drS(2) = MettreApost(rw(2).ToString)
        '    drS(3) = MettreApost(rw(3).ToString)
        '    drS(4) = rw(5).ToString
        '    Codz = rw(4).ToString

        '    If IsDBNull(rw(6).ToString) Then
        '        LibDev = ""
        '    Else
        '        CodeDev = rw(6).ToString
        '        'Procedure pour afficher la devise dans le Combo Devise
        '        query = "SELECT LibelleDevise FROM T_Devise WHERE CodeDevise = '" & CodeDev & "'"
        '        Dim dtq As DataTable = ExcecuteSelectQuery(query)
        '        For Each rwq As DataRow In dtq.Rows
        '            drS(6) = rwq(0).ToString
        '        Next
        '    End If

        '    'Procedure pour récupérer CodeZoneMere pour afficher le Combo Issu de
        '    Dim LibZ As String = ""
        '    query = ("SELECT LibelleZone FROM T_ZoneGeo WHERE CodeZone = '" & Codz & "'")
        '    Dim dt1 As DataTable = ExcecuteSelectQuery(query)
        '    For Each rw1 As DataRow In dt1.Rows
        '        LibZ = MettreApost(rw1(0).ToString)
        '    Next
        '    If (LibZ = MettreApost(rw(3).ToString)) Then
        '        drS(5) = ""
        '    Else
        '        drS(5) = MettreApost(LibZ)
        '    End If

        '    drS(7) = MettreApost(rw(7).ToString)
        '    drS(8) = Codz

        '    dtzonegeo.Rows.Add(drS)
        '    LgListZoneGeo.DataSource = dtzonegeo

        '    query = "SELECT Z.CodeZone, Z.AbregeZone, Z.IndicZone, Z.LibelleZone, Z.CodeZoneMere, S.LibelleStr, Z.CodeDevise, Z.TVA FROM T_ZoneGeo As Z, T_StructGeo As S WHERE Z.NiveauStr=S.NiveauStr and Z.NiveauStr=2 and Z.CodeZoneMere='" & rw(0).ToString & "'"
        '    dt1 = ExcecuteSelectQuery(query)
        '    For Each rw1 As DataRow In dt1.Rows
        '        Dim drS1 = dtzonegeo.NewRow()
        '        drS1(0) = rw1(0).ToString
        '        drS1(1) = rw1(1).ToString
        '        drS1(2) = MettreApost(rw1(2).ToString)
        '        drS1(3) = MettreApost(rw1(3).ToString)
        '        drS1(4) = rw1(5).ToString
        '        Codz = rw1(4).ToString

        '        If IsDBNull(rw1(6).ToString) Then
        '            LibDev = ""
        '        Else
        '            CodeDev = rw1(6).ToString
        '            'Procedure pour afficher la devise dans le Combo Devise
        '            query = "SELECT LibelleDevise FROM T_Devise WHERE CodeDevise = '" & CodeDev & "'"
        '            Dim dtq = ExcecuteSelectQuery(query)
        '            For Each rwq As DataRow In dtq.Rows
        '                drS1(6) = rwq(0).ToString
        '            Next
        '        End If

        '        'Procedure pour récupérer CodeZoneMere pour afficher le Combo Issu de
        '        Dim LibZ1 As String = ""

        '        If Codz.ToString = "0" Then

        '            drS1(5) = ""

        '        Else

        '            query = "SELECT LibelleZone FROM T_ZoneGeo WHERE CodeZone = '" & Codz & "'"
        '            Dim dt2 = ExcecuteSelectQuery(query)
        '            For Each rw2 As DataRow In dt2.Rows
        '                drS1(5) = MettreApost(rw2(0).ToString)
        '            Next

        '        End If

        '        drS1(7) = MettreApost(rw1(7).ToString)
        '        drS1(8) = Codz
        '        dtzonegeo.Rows.Add(drS1)
        '        LgListZoneGeo.DataSource = dtzonegeo

        '        query = "SELECT Z.CodeZone, Z.AbregeZone, Z.IndicZone, Z.LibelleZone, Z.CodeZoneMere, S.LibelleStr, Z.CodeDevise, Z.TVA FROM T_ZoneGeo As Z, T_StructGeo As S WHERE Z.NiveauStr=S.NiveauStr and Z.NiveauStr=3 and Z.CodeZoneMere='" & rw1(0).ToString & "'"
        '        Dim dt3 = ExcecuteSelectQuery(query)
        '        For Each rw2 As DataRow In dt3.Rows

        '            Dim drS2 = dtzonegeo.NewRow()
        '            drS2(0) = rw2(0).ToString
        '            drS2(1) = rw2(1).ToString
        '            drS2(2) = MettreApost(rw2(2).ToString)
        '            drS2(3) = MettreApost(rw2(3).ToString)
        '            drS2(4) = rw2(5).ToString
        '            Codz = rw2(4).ToString

        '            If IsDBNull(rw2(6).ToString) Then
        '                LibDev = ""
        '            Else
        '                CodeDev = rw2(6).ToString
        '                'Procedure pour afficher la devise dans le Combo Devise

        '                query = "SELECT LibelleDevise FROM T_Devise WHERE CodeDevise = '" & CodeDev & "'"
        '                Dim dtq = ExcecuteSelectQuery(query)
        '                For Each rwq As DataRow In dtq.Rows
        '                    drS2(6) = rwq(0).ToString
        '                Next
        '            End If

        '            'Procedure pour récupérer CodeZoneMere pour afficher le Combo Issu de
        '            Dim LibZ2 As String = ""

        '            query = "SELECT LibelleZone FROM T_ZoneGeo WHERE CodeZone = '" & Codz & "'"
        '            Dim dtz = ExcecuteSelectQuery(query)
        '            For Each rw3 As DataRow In dtz.Rows
        '                LibZ2 = MettreApost(rw3(0).ToString)
        '            Next
        '            If (LibZ2 = MettreApost(rw2(3).ToString)) Then
        '                drS2(5) = ""
        '            Else
        '                drS2(5) = MettreApost(LibZ2)
        '            End If

        '            drS2(7) = MettreApost(rw2(7).ToString)
        '            drS2(8) = Codz
        '            dtzonegeo.Rows.Add(drS2)
        '            LgListZoneGeo.DataSource = dtzonegeo

        '            query = "SELECT Z.CodeZone, Z.AbregeZone, Z.IndicZone, Z.LibelleZone, Z.CodeZoneMere, S.LibelleStr, Z.CodeDevise, Z.TVA FROM T_ZoneGeo As Z, T_StructGeo As S WHERE Z.NiveauStr=S.NiveauStr and Z.NiveauStr=4 and Z.CodeZoneMere='" & rw2(0).ToString & "'"
        '            dtz = ExcecuteSelectQuery(query)
        '            For Each rw3 As DataRow In dtz.Rows
        '                Dim drS3 = dtzonegeo.NewRow()
        '                drS3(0) = rw3(0).ToString
        '                drS3(1) = rw3(1).ToString
        '                drS3(2) = MettreApost(rw3(2).ToString)
        '                drS3(3) = MettreApost(rw3(3).ToString)
        '                drS3(4) = rw3(5).ToString
        '                Codz = rw3(4).ToString

        '                If IsDBNull(rw3(6).ToString) Then
        '                    LibDev = ""
        '                Else
        '                    CodeDev = rw3(6).ToString
        '                    'Procedure pour afficher la devise dans le Combo Devise

        '                    query = "SELECT LibelleDevise FROM T_Devise WHERE CodeDevise = '" & CodeDev & "'"
        '                    Dim dtq = ExcecuteSelectQuery(query)
        '                    For Each rwq As DataRow In dtq.Rows
        '                        drS3(6) = rwq(0).ToString
        '                    Next
        '                End If

        '                'Procedure pour récupérer CodeZoneMere pour afficher le Combo Issu de
        '                Dim LibZ3 As String = ""

        '                query = "SELECT LibelleZone FROM T_ZoneGeo WHERE CodeZone = '" & Codz & "'"
        '                Dim dt4 = ExcecuteSelectQuery(query)
        '                For Each rw4 As DataRow In dt4.Rows
        '                    LibZ3 = MettreApost(rw4(0).ToString)
        '                Next
        '                If (LibZ3 = MettreApost(rw3(3).ToString)) Then
        '                    drS3(5) = ""
        '                Else
        '                    drS3(5) = MettreApost(LibZ3)
        '                End If
        '                drS3(8) = Codz
        '                drS3(7) = MettreApost(rw3(7).ToString)
        '                dtzonegeo.Rows.Add(drS3)
        '                LgListZoneGeo.DataSource = dtzonegeo
        '            Next
        '        Next
        '    Next
        'Next

        'LgListZoneGeo.DataSource = dtzonegeo
        'ViewZoneGeo.Columns(0).Visible = False
        'ViewZoneGeo.Columns("CodeMere").Visible = False
        'ViewZoneGeo.OptionsView.ColumnAutoWidth = True
        'ViewZoneGeo.OptionsBehavior.AutoExpandAllGroups = True
        'ViewZoneGeo.VertScrollVisibility = True
        'ViewZoneGeo.HorzScrollVisibility = True

    End Sub

    Private Sub LgListZoneGeo_Click(sender As System.Object, e As System.EventArgs)
        'If (ViewZoneGeo.RowCount > 0) Then
        '    DrX = ViewZoneGeo.GetDataRow(ViewZoneGeo.FocusedRowHandle)
        '    TxtAbrege.Text = DrX(1).ToString
        '    CmbTypZone.Text = DrX(4).ToString
        '    CmbIssu_de.Text = DrX(5).ToString
        '    TxtNomZone.Text = DrX(3).ToString
        '    TxtIndicatifZone.Text = DrX(2).ToString
        '    TxtTva.Text = DrX(7).ToString
        '    CmbDevise.Text = DrX(6).ToString
        '    TxtCodeZone.Text = DrX(0).ToString
        '    TxtCodeZoneMereCache.Text = IIf(DrX(8).ToString = "", 0, DrX(8).ToString)

        '    Dim IDL = DrX(1).ToString
        '    ColorRowGrid(ViewZoneGeo, "[Code]='x'", Color.White, "Times New Roman", 10, FontStyle.Regular, Color.Black)
        '    ColorRowGridAnal(ViewZoneGeo, "[Code]='" & IDL & "'", Color.Navy, "Times New Roman", 10, FontStyle.Bold, Color.White, True)
        'End If
    End Sub

    Private Sub LgListZoneGeo_DoubleClick(sender As Object, e As System.EventArgs)
        'If (ViewZoneGeo.RowCount > 0) Then
        '    DrX = ViewZoneGeo.GetDataRow(ViewZoneGeo.FocusedRowHandle)

        '    TxtAbrege.Text = DrX(1).ToString
        '    CmbTypZone.Text = DrX(4).ToString
        '    CmbIssu_de.Text = DrX(5).ToString
        '    TxtNomZone.Text = DrX(3).ToString
        '    TxtIndicatifZone.Text = DrX(2).ToString
        '    TxtTva.Text = DrX(7).ToString
        '    CmbDevise.Text = DrX(6).ToString
        '    TxtCodeZone.Text = DrX(0).ToString

        '    Dim IDL = DrX(1).ToString
        '    ColorRowGrid(ViewZoneGeo, "[Code]='x'", Color.White, "Times New Roman", 10, FontStyle.Regular, Color.Black)
        '    ColorRowGridAnal(ViewZoneGeo, "[Code]='" & IDL & "'", Color.Navy, "Times New Roman", 10, FontStyle.Bold, Color.White, True)

        '    Dim City As String = DrX(3).ToString
        '    Dim State As String = DrX(5).ToString
        '    Dim queryAddress As New StringBuilder()
        '    queryAddress.Append("http://maps.google.com/maps?q=")

        '    If DrX(2).ToString <> String.Empty Then
        '        queryAddress.Append(City + " , " & " + ")
        '    End If
        '    If DrX(4).ToString <> String.Empty Then
        '        queryAddress.Append(State + " , " & " + ")
        '    End If
        '    Maps.WebBrowser1.Navigate(queryAddress.ToString())

        '    Maps.ShowDialog()
        'End If
    End Sub

    Private Sub SupprimerLaLigneSelectionnerToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles SupprimerLaLigneSelectionnerToolStripMenuItem.Click
        Try


            If TreeList1.Nodes.Count > 0 Then
                Dim code As Integer
                code = TreeList1.FocusedNode.GetValue("CodeZone")
                If (ConfirmMsg("Voulez-vous supprimer la zone géographique?") = DialogResult.Yes) Then
                    query = "delete from t_zonegeo where CodeZone='" & code & "'"
                    ExecuteNonQuery(query)
                    SuccesMsg("Suppression effectuée avec succès")
                    Initialiser()
                    AjouterZones(0)
                End If

            End If
        Catch ex As Exception
            FailMsg("Code Erreur 0X0004 " & vbNewLine & ex.ToString())
        End Try


        'If ConfirmMsg("Voulez vous supprimer cette zone géographique?") = DialogResult.Yes Then
        '    query = "delete from T_ZoneGeo where CodeZone='" & TxtCodeZone.Text & "'"
        '    ExecuteNonQuery(query)

        '    SuccesMsg("Suppression effectuée avec succès")

        '    remplirdatagridzonegeo()
        '    Initialiser()
        'End If



        'Dim action As String = ""
        'Dim madate = Now
        'Dim dd = madate.ToString("H:mm:ss")
        'madate = madate.ToString("yyyy-MM-dd")


        'If (ViewZoneGeo.RowCount > 0) Then
        '    DrX = ViewZoneGeo.GetDataRow(ViewZoneGeo.FocusedRowHandle)

        '    Dim Reponse As MsgBoxResult
        '    Reponse = MsgBox("Voulez-vous supprimer définitivement la zone ?")
        '    If (Reponse = MsgBoxResult.Yes) Then

        '        Try

        '            query = "SELECT CodeZone FROM T_ZoneGeo where CodeZone = '" + EnleverApost(DrX(0).ToString) + "'"
        '            Dim codezone = ExecuteScallar(query)


        '            query = "DELETE from T_ZoneGeo where CodeZone='" & EnleverApost(DrX(0).ToString) & "'"
        '            ExecuteNonQuery(query)
        '            SuccesMsg("Suppression effectuée avec succès")
        '            'historique
        '            action = "Suppression de la zone géogaphique : " + MettreApost(DrX(3).ToString) + ""
        '            query = "insert into t_historique values (NULL,'" + ProjetEnCours + "','" + NomUtilisateur + "','" + EnleverApost(action) + "','" + madate + "','" + dd + "')"
        '            ExecuteNonQuery(query)


        '            remplirdatagridzonegeo()
        '            Initialiser()

        '        Catch ex As Exception
        '            FailMsg("Code Erreur 0XSUP_LOCLT0001 " & vbNewLine & ex.ToString())
        '        End Try
        '    End If
        'End If
    End Sub

    Private Sub TxtNomZone_TextChanged(sender As Object, e As System.EventArgs) Handles TxtNomZone.TextChanged
        'If (ViewZoneGeo.RowCount > 0) Then

        'Else
        '    If (TxtNomZone.Text.Replace(" ", "") <> "") Then

        '        Dim partS() As String = (TxtNomZone.Text.Replace("'", "").Replace("  ", " ").Replace(" le", "").Replace(" la", "").Replace(" les", "").Replace(" l'", "").Replace(" de", "").Replace(" du", "").Replace(" des", "").Replace(" d'", "")).Split(" "c)
        '        Dim CodeS As String = ""
        '        For Each elt In partS
        '            CodeS = CodeS & Mid(elt, 1, 1).ToUpper
        '        Next
        '        TxtAbrege.Text = CodeS

        '    Else
        '        TxtAbrege.Text = ""
        '    End If
        'End If

    End Sub

    Private Sub TreeList1_FocusedNodeChanged(sender As Object, e As FocusedNodeChangedEventArgs) Handles TreeList1.FocusedNodeChanged
        'SuccesMsg(e.Node.Item("CodeZone") & " " & e.Node.Nodes.Count)
    End Sub

    Private Sub TreeList1_NodeChanged(sender As Object, e As NodeChangedEventArgs) Handles TreeList1.NodeChanged

        If e.ChangeType = NodeChangeTypeEnum.Expanded Then
            Dim Loaded As Integer = Val(e.Node.Item("loaded"))
            If Loaded = 0 Then
                AjouterZones(e.Node.Item("CodeZone"), e.Node)
                e.Node.Item("Loaded") = "1"
            End If


        End If
    End Sub

    Private Sub CmbPays_de_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbPays_de.SelectedIndexChanged
        'Dim Temp0, Temp As Decimal
        Try
            If CmbPays_de.SelectedIndex > -1 Then
                Dim LibelZ As String = CmbPays_de.Text
                CorrectionChaine(LibelZ)
                query = "select CodeZone, IndicZone, CodeDevise, TVA from T_ZoneGeo where LibelleZone='" & EnleverApost(LibelZ) & "'"
                Dim dt1 As DataTable = ExcecuteSelectQuery(query)
                For Each rw As DataRow In dt1.Rows

                    TxtCodeZoneMereCache.Text = rw(0)
                    TxtIndicatifZone.Text = rw(1)
                    TxtIndicatifZone.Enabled = False
                    TxtCodeDeviseCache.Text = rw(2)
                    TxtTva.Text = rw(3)
                    TxtTva.Enabled = False
                    Dim LibD As String = ""

                    'Recherche de la devise***************
                    query = "select LibelleDevise from T_Devise where CodeDevise='" & rw(2) & "'"
                    Dim dt2 = ExcecuteSelectQuery(query)
                    For Each rw1 As DataRow In dt2.Rows
                        LibD = rw1(0)
                        RestaurerChaine(LibD)
                    Next
                    CmbDevise.Text = LibD
                    ActualiserDevise.Enabled = False
                Next
                'query = "select CodeZone, LibelleZone from T_ZoneGeo WHERE CodeZone = '" & IdPays(CmbPays_de.SelectedIndex) & "'"
                'Dim dt0 As DataTable = ExcecuteSelectQuery(query)
                'If dt0.Rows.Count = 0 Then
                '    Initialiser()
                '    Exit Sub
                'End If

                'Dim rwa As DataRow = dt0.Rows(0)
                'Temp0 = rwa(0)
                'TxtNiveauStrCache.Text = Temp0.ToString
                'Temp = Temp0 - 1

                'If Temp > 0 Then
                '    query = "select NiveauStr, LibelleZone from T_ZoneGeo WHERE NiveauStr <= '" & Temp & "';"
                '    If (Temp0 = 5) Then
                '        query = "select NiveauStr, LibelleZone from T_ZoneGeo WHERE NiveauStr >= '2' and NiveauStr <= '4'"
                '    End If
                '    If (Temp0 = 6) Then
                '        query = "select NiveauStr, LibelleZone from T_ZoneGeo WHERE NiveauStr >= '4' and NiveauStr <= '5'"
                '    End If
                '    If (Temp0 = 7) Then
                '        query = "select NiveauStr, LibelleZone from T_ZoneGeo WHERE NiveauStr >= '4' and NiveauStr <= '6'"
                '    End If
                '    If (Temp0 = 8) Then
                '        query = "select NiveauStr, LibelleZone from T_ZoneGeo WHERE NiveauStr = '4' or NiveauStr = '6'"
                '    End If
                'Else
                '    'Temp = 1

                'End If

                Dim Temp = IdType(CmbTypZone.SelectedIndex) - 1

                If Temp >= 3 Then
                    Dim Options As String = String.Empty
                    For i = (Temp - 1) To 1 Step -1
                        If i <> 1 Then
                            Options &= " in (Select CodeZone FROM T_ZoneGeo WHERE NiveauStr='" & i & "' AND CodeZoneMere"
                        Else
                            Options &= " in (Select CodeZone FROM T_ZoneGeo WHERE NiveauStr='" & i & "' AND CodeZone='" & IdPays(CmbPays_de.SelectedIndex) & "'"

                        End If
                    Next
                    For i = (Temp - 1) To 1 Step -1
                        Options &= ")"
                    Next
                    query = "select NiveauStr, LibelleZone from T_ZoneGeo WHERE CodeZoneMere " & Options
                Else
                    query = "select NiveauStr, LibelleZone from T_ZoneGeo WHERE NiveauStr = '" & Temp & "' AND CodeZoneMere='" & IdPays(CmbPays_de.SelectedIndex) & "'"
                End If

                Dim dt0 = ExcecuteSelectQuery(query)

                CmbIssu_de.Properties.Items.Clear()
                CmbIssu_de.Text = ""
                For Each rw As DataRow In dt0.Rows
                    CmbIssu_de.Properties.Items.Add(MettreApost(rw(1).ToString))
                Next

                'If CmbTypZone.Text.ToLower = "pays" Then
                '    TxtIndicatifZone.Enabled = True
                '    CmbDevise.Enabled = True
                '    TxtTva.Enabled = True
                '    CmbIssu_de.Enabled = False
                '    CmbIssu_de.Size = New Point(420, 26)
                '    CmbPays_de.Enabled = False
                '    TxtIndicatifZone.Enabled = True
                '    CmbDevise.Enabled = True
                '    ActualiserDevise.Enabled = True
                'ElseIf CmbTypZone.Text.ToLower = "district" Then
                '    CmbIssu_de.Enabled = False
                '    CmbPays_de.Size = New Point(420, 26)
                '    CmbPays_de.Enabled = True
                '    CmbDevise.Enabled = False

                'ElseIf CmbTypZone.Text.ToLower = "region" Or CmbTypZone.Text.ToLower = "departement" Or CmbTypZone.Text.ToLower = "sous-prefecture" Or CmbTypZone.Text.ToLower = "commune" Or CmbTypZone.Text.ToLower = "village" Then
                '    CmbIssu_de.Enabled = True
                '    CmbIssu_de.Size = New Point(202, 26)
                '    CmbPays_de.Enabled = True
                '    CmbPays_de.Size = New Point(214, 26)
                '    CmbDevise.Enabled = False
                'Else
                '    TxtIndicatifZone.Enabled = False
                '    CmbDevise.Enabled = False
                '    CmbIssu_de.Enabled = True
                '    CmbPays_de.Enabled = True
                '    TxtTva.Enabled = False
                '    TxtIndicatifZone.Enabled = False
                '    CmbDevise.Enabled = False
                '    ActualiserDevise.Enabled = False
                'End If
                'TxtNomZone.Enabled = True
                'TxtAbrege.Enabled = True
                'TxtTva.Enabled = True
                'Else
                'Initialiser()
            End If
        Catch ex As Exception
            FailMsg("Code Erreur 0X0004 " & vbNewLine & ex.ToString())
        End Try


        'Try
        '    Dim LibelZ As String = CmbPays_de.Text
        '    CorrectionChaine(LibelZ)
        '    query = "select CodeZone, IndicZone, CodeDevise, TVA from T_ZoneGeo where LibelleZone='" & EnleverApost(LibelZ) & "'"
        '    Dim dt0 As DataTable = ExcecuteSelectQuery(query)
        '    For Each rw As DataRow In dt0.Rows

        '        TxtCodeZoneMereCache.Text = rw(0)
        '        TxtIndicatifZone.Text = rw(1)
        '        TxtCodeDeviseCache.Text = rw(2)
        '        TxtTva.Text = rw(3)
        '        Dim LibD As String = ""

        '        'Recherche de la devise***************
        '        query = "select LibelleDevise from T_Devise where CodeDevise='" & rw(2) & "'"
        '        Dim dt1 = ExcecuteSelectQuery(query)
        '        For Each rw1 As DataRow In dt1.Rows
        '            LibD = rw1(0)
        '            RestaurerChaine(LibD)
        '        Next
        '        CmbDevise.Text = LibD
        '        ActualiserDevise.Enabled = False
        '    Next
        'Catch ex As Exception
        '    FailMsg("Code Erreur 0XIT_Z_MERE0001 " & vbNewLine & ex.ToString())
        'End Try




    End Sub

    Private Sub TreeList1_DoubleClick(sender As Object, e As EventArgs) Handles TreeList1.DoubleClick
        'If TreeList1.Nodes.Count > 0 Then
        '    'Dim code As Integer
        '    Dim codeParent As Integer
        '    'code = TreeList1.FocusedNode.GetValue("CodeZone")
        '    'If Not TreeList1.FocusedNode.RootNode.Selected Then
        '    '    codeParent = TreeList1.FocusedNode.ParentNode.GetValue("CodeZone")

        '    'End If
        '    'query = "SELECT * FROM t_zonegeo WHERE CodeZone='" & code & "'"
        '    'Dim dt = ExcecuteSelectQuery(query)
        '    'For Each rw In dt.Rows
        '    '    TxtNomZone.Text = MettreApost(rw("LibelleZone").ToString)
        '    '    TxtIndicatifZone.Text = rw("IndicZone").ToString
        '    '    TxtTva.Text = rw("TVA").ToString
        '    '    TxtAbrege.Text = rw("AbregeZone").ToString
        '    '    If rw("NiveauStr").ToString = "1" Then
        '    '        query = "Select LibelleStr from t_structgeo where NiveauStr= 1"
        '    '        CmbTypZone.Text = MettreApost(ExecuteScallar(query))
        '    '        CmbPays_de.Text = ""
        '    '        CmbPays_de.Enabled = False
        '    '    ElseIf rw("NiveauStr").ToString = "2" Then
        '    '        CmbPays_de.Text = ""
        '    '        CmbPays_de.Enabled = True
        '    '        query = "Select LibelleStr from t_structgeo where NiveauStr= 2"
        '    '        CmbTypZone.Text = MettreApost(ExecuteScallar(query))
        '    '        query = "SELECT LibelleZone FROM t_zonegeo WHERE CodeZone='" & codeParent & "'"
        '    '        CmbPays_de.Text = MettreApost(ExecuteScallar(query))
        '    '    Else


        '    '    End If
        '    'Next
        '    CmbTypZone.Text = TreeList1.FocusedNode.GetValue("Type")
        '    TxtNomZone.Text = TreeList1.FocusedNode.GetValue("NomZone")
        '    TxtIndicatifZone.Text = TreeList1.FocusedNode.GetValue("Indicatif")
        '    TxtAbrege.Text = TreeList1.FocusedNode.GetValue("Code")
        '    TxtTva.Text = TreeList1.FocusedNode.GetValue("Tva")
        '    CmbDevise.Text = TreeList1.FocusedNode.GetValue("Devise")
        '    If Not TreeList1.FocusedNode.RootNode.Selected Then
        '        codeParent = TreeList1.FocusedNode.ParentNode.GetValue("CodeZone")

        '    End If
        '    If TreeList1.FocusedNode.GetValue("Type") = "PAYS" Then
        '        CmbPays_de.Text = ""
        '        CmbPays_de.Enabled = False
        '    ElseIf TreeList1.FocusedNode.GetValue("Type") = "DISTRICT" Then
        '        CmbPays_de.Enabled = True
        '        query = "SELECT LibelleZone FROM t_zonegeo WHERE CodeZone='" & codeParent & "'"
        '        CmbPays_de.Text = MettreApost(ExecuteScallar(query))
        '    Else
        '        Dim CodePays As Integer
        '        CodePays = TreeList1.FocusedNode.RootNode.GetValue("CodeZone")
        '        query = "SELECT LibelleZone FROM t_zonegeo WHERE CodeZone='" & CodePays & "'"
        '        CmbPays_de.Text = MettreApost(ExecuteScallar(query))
        '        Dim codeParent1 As Integer
        '        codeParent1 = TreeList1.FocusedNode.ParentNode.GetValue("CodeZone")
        '        query = "SELECT LibelleZone FROM t_zonegeo WHERE CodeZone='" & codeParent1 & "'"
        '        CmbIssu_de.Text = MettreApost(ExecuteScallar(query))
        '    End If
        'End If
        'Modifie = True
    End Sub

    Private Sub ModifierToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ModifierToolStripMenuItem.Click
        If TreeList1.Nodes.Count > 0 Then
            Dim codeParent As Integer
            CmbTypZone.Text = TreeList1.FocusedNode.GetValue("Type")
            TxtNomZone.Text = TreeList1.FocusedNode.GetValue("NomZone")
            TxtIndicatifZone.Text = TreeList1.FocusedNode.GetValue("Indicatif")
            TxtAbrege.Text = TreeList1.FocusedNode.GetValue("Code")
            TxtTva.Text = TreeList1.FocusedNode.GetValue("Tva")
            CmbDevise.Text = TreeList1.FocusedNode.GetValue("Devises")
            If Not TreeList1.FocusedNode.RootNode.Selected Then
                codeParent = TreeList1.FocusedNode.ParentNode.GetValue("CodeZone")
            End If
            If TreeList1.FocusedNode.GetValue("Type") = "PAYS" Then
                CmbPays_de.Text = ""
                CmbPays_de.Enabled = False
                CmbTypZone.Enabled = True
            ElseIf TreeList1.FocusedNode.GetValue("Type") = "DISTRICT" Then
                TxtIndicatifZone.Enabled = False
                TxtTva.Enabled = False
                CmbPays_de.Enabled = True
                CmbTypZone.Enabled = False
                query = "SELECT LibelleZone FROM t_zonegeo WHERE CodeZone='" & codeParent & "'"
                CmbPays_de.Text = MettreApost(ExecuteScallar(query))
            Else
                TxtIndicatifZone.Enabled = False
                TxtTva.Enabled = False
                CmbTypZone.Enabled = False
                Dim CodePays As Integer
                CodePays = TreeList1.FocusedNode.RootNode.GetValue("CodeZone")
                query = "SELECT LibelleZone FROM t_zonegeo WHERE CodeZone='" & CodePays & "'"
                CmbPays_de.Text = MettreApost(ExecuteScallar(query))
                Dim codeParent1 As Integer
                codeParent1 = TreeList1.FocusedNode.ParentNode.GetValue("CodeZone")
                query = "SELECT LibelleZone FROM t_zonegeo WHERE CodeZone='" & codeParent1 & "'"
                CmbIssu_de.Text = MettreApost(ExecuteScallar(query))
            End If
        End If
        Modifie = True
    End Sub

End Class