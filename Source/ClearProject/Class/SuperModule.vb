Imports MySql.Data.MySqlClient
Imports System.IO
Imports System.Math
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports Microsoft.Office.Interop

Module SuperModule
    Public Function DateSansJourWeekEnd(ladate As Date, nbreJour As Decimal) As String
        If nbreJour > 0 Then
            If ladate.DayOfWeek = DayOfWeek.Saturday Then
                ladate = ladate.AddDays(1)
                nbreJour += 1
            ElseIf ladate.DayOfWeek = DayOfWeek.Sunday Then
                nbreJour += 1
            End If
            While (nbreJour <> 0)
                ladate = ladate.AddDays(1)
                If ladate.DayOfWeek <> DayOfWeek.Saturday And ladate.DayOfWeek <> DayOfWeek.Sunday Then
                    nbreJour -= 1
                End If
            End While
            Return ladate.ToShortDateString
        Else
            Return ladate.ToShortDateString()
        End If
    End Function
    Public Function NbreJourSansJourWeekEnd(date1 As Date, date2 As Date) As Decimal
        If date1 > date2 Then 'Date1 est superieur a Date2

            Dim cpte = 0
            If date2.DayOfWeek = DayOfWeek.Saturday Then
                date2 = date2.AddDays(1)
                cpte -= 1
            ElseIf date2.DayOfWeek = DayOfWeek.Sunday Then
                cpte -= 1
            End If

            While (date1 > date2)
                date2 = date2.AddDays(1)
                If date2.DayOfWeek <> DayOfWeek.Saturday And date2.DayOfWeek <> DayOfWeek.Sunday Then
                    cpte += 1
                End If
            End While
            Return cpte

        ElseIf date1 = date2 Then 'Date1 est egal a Date2

            Return 0

        Else 'Date2 est superieur a Date1

            Dim cpte = 0
            If date1.DayOfWeek = DayOfWeek.Saturday Then
                date1 = date1.AddDays(1)
                cpte -= 1
            ElseIf date1.DayOfWeek = DayOfWeek.Sunday Then
                cpte -= 1
            End If

            While (date2 > date1)
                date1 = date1.AddDays(1)
                If date1.DayOfWeek <> DayOfWeek.Saturday And date1.DayOfWeek <> DayOfWeek.Sunday Then
                    cpte += 1
                End If
            End While
            Return cpte

        End If
    End Function
    <System.Runtime.CompilerServices.Extension()>
    Public Sub Invoke(ByVal control As Control, ByVal action As Action)
        If control.InvokeRequired Then
            control.Invoke(New MethodInvoker(Sub() action()), Nothing)
        Else
            action.Invoke()
        End If
    End Sub
    <System.Runtime.CompilerServices.Extension()>
    Public Sub SetEnabled(ByVal ctl As Control, ByVal enabled As Boolean)
        If ctl.InvokeRequired Then
            ctl.BeginInvoke(New Action(Of Control, Boolean)(AddressOf SetEnabled), ctl, enabled)
        Else
            ctl.Enabled = enabled
        End If
    End Sub
    <System.Runtime.CompilerServices.Extension()>
    Public Sub SetVisible(ByVal ctl As Control, ByVal enabled As Boolean)
        If ctl.InvokeRequired Then
            ctl.BeginInvoke(New Action(Of Control, Boolean)(AddressOf SetEnabled), ctl, enabled)
        Else
            ctl.Visible = enabled
        End If
    End Sub
    <System.Runtime.CompilerServices.Extension()>
    Public Sub SetText(ByVal ctl As Control, ByVal Text As String)
        If ctl.InvokeRequired Then
            ctl.BeginInvoke(New Action(Of Control, String)(AddressOf SetText), ctl, Text)
        Else
            ctl.Text = Text
        End If
    End Sub

    Public Function IsBailleur(ByVal InitialeBailleur As String) As Boolean
        query = "SELECT COUNT(*) FROM t_bailleur WHERE InitialeBailleur='" & InitialeBailleur & "' AND CodeProjet='" & ProjetEnCours & "'"
        If Val(ExecuteScallar(query)) > 0 Then
            Return True
        End If
        Return False
    End Function
    <System.Runtime.CompilerServices.Extension()>
    Public Function IsRequiredControl(ByRef Control As Object, ByVal OutPutMsg As String) As Boolean
        'DevExpress Edit Conrols
        If TypeOf (Control) Is DevExpress.XtraEditors.TextEdit Then
            Dim Ctl = CType(Control, DevExpress.XtraEditors.TextEdit)
            If Ctl.Text.Trim() = String.Empty Then
                SuccesMsg(OutPutMsg)
                Ctl.Focus()
                Return True
            End If
        End If
        If TypeOf (Control) Is DevExpress.XtraEditors.MemoEdit Then
            Dim Ctl = CType(Control, DevExpress.XtraEditors.MemoEdit)
            If Ctl.Text.Trim() = String.Empty Then
                SuccesMsg(OutPutMsg)
                Ctl.Focus()
                Return True
            End If
        End If
        If TypeOf (Control) Is DevExpress.XtraEditors.ComboBoxEdit Then
            Dim Ctl = CType(Control, DevExpress.XtraEditors.ComboBoxEdit)
            If Ctl.SelectedIndex = -1 Then
                SuccesMsg(OutPutMsg)
                Ctl.Focus()
                Return True
            End If
        End If
        If TypeOf (Control) Is DevExpress.XtraEditors.DateEdit Then
            Dim Ctl = CType(Control, DevExpress.XtraEditors.DateEdit)
            If Ctl.Text.Trim() = String.Empty Then
                SuccesMsg(OutPutMsg)
                Ctl.Focus()
                Return True
            End If
        End If
        If TypeOf (Control) Is DevExpress.XtraEditors.TimeEdit Then
            Dim Ctl = CType(Control, DevExpress.XtraEditors.TimeEdit)
            If Ctl.Text.Trim() = String.Empty Then
                SuccesMsg(OutPutMsg)
                Ctl.Focus()
                Return True
            End If
        End If

        'Windows Forms Conrols
        If TypeOf (Control) Is TextBox Then
            Dim Ctl = CType(Control, TextBox)
            If Ctl.Text.Trim() = String.Empty Then
                SuccesMsg(OutPutMsg)
                Ctl.Focus()
                Return True
            End If
        End If
        If TypeOf (Control) Is ComboBox Then
            Dim Ctl = CType(Control, ComboBox)
            If Ctl.SelectedIndex = -1 Then
                SuccesMsg(OutPutMsg)
                Ctl.Focus()
                Return True
            End If
        End If
        If TypeOf (Control) Is DateTimePicker Then
            Dim Ctl = CType(Control, DateTimePicker)
            If Ctl.Text.Trim() = String.Empty Then
                SuccesMsg(OutPutMsg)
                Ctl.Focus()
                Return True
            End If
        End If
        Return False
    End Function
    <System.Runtime.CompilerServices.Extension()>
    Public Function EnleverApostrophe(ByVal Control As Object) As String
        'DevExpress Edit Conrols
        If TypeOf (Control) Is DevExpress.XtraEditors.TextEdit Then
            Dim Ctl = CType(Control, DevExpress.XtraEditors.TextEdit)
            Return CONNEXION.EnleverApost(Ctl.Text.Trim())
        End If
        If TypeOf (Control) Is DevExpress.XtraEditors.MemoEdit Then
            Dim Ctl = CType(Control, DevExpress.XtraEditors.MemoEdit)
            Return CONNEXION.EnleverApost(Ctl.Text.Trim())
        End If
        If TypeOf (Control) Is DevExpress.XtraEditors.ComboBoxEdit Then
            Dim Ctl = CType(Control, DevExpress.XtraEditors.ComboBoxEdit)
            Return CONNEXION.EnleverApost(Ctl.Text.Trim())
        End If

        'Windows Forms Conrols
        If TypeOf (Control) Is TextBox Then
            Dim Ctl = CType(Control, TextBox)
            Return CONNEXION.EnleverApost(Ctl.Text.Trim())
        End If
        If TypeOf (Control) Is ComboBox Then
            Dim Ctl = CType(Control, ComboBox)
            Return CONNEXION.EnleverApost(Ctl.Text.Trim())
        End If
        If TypeOf (Control) Is DateTimePicker Then
            Dim Ctl = CType(Control, DateTimePicker)
            Return CONNEXION.EnleverApost(Ctl.Text.Trim())
        End If

        Return CONNEXION.EnleverApost(Control)
    End Function
    <System.Runtime.CompilerServices.Extension()>
    Public Function MettreApostrophe(ByRef Control As Object) As String
        'DevExpress Edit Conrols
        If TypeOf (Control) Is DevExpress.XtraEditors.TextEdit Then
            Dim Ctl = CType(Control, DevExpress.XtraEditors.TextEdit)
            Return CONNEXION.MettreApost(Ctl.Text.Trim())
        End If
        If TypeOf (Control) Is DevExpress.XtraEditors.MemoEdit Then
            Dim Ctl = CType(Control, DevExpress.XtraEditors.MemoEdit)
            Return CONNEXION.MettreApost(Ctl.Text.Trim())
        End If
        If TypeOf (Control) Is DevExpress.XtraEditors.ComboBoxEdit Then
            Dim Ctl = CType(Control, DevExpress.XtraEditors.ComboBoxEdit)
            Return CONNEXION.MettreApost(Ctl.Text.Trim())
        End If

        'Windows Forms Conrols
        If TypeOf (Control) Is TextBox Then
            Dim Ctl = CType(Control, TextBox)
            Return CONNEXION.MettreApost(Ctl.Text.Trim())
        End If
        If TypeOf (Control) Is ComboBox Then
            Dim Ctl = CType(Control, ComboBox)
            Return CONNEXION.MettreApost(Ctl.Text.Trim())
        End If
        If TypeOf (Control) Is DateTimePicker Then
            Dim Ctl = CType(Control, DateTimePicker)
            Return CONNEXION.MettreApost(Ctl.Text.Trim())
        End If

        Return CONNEXION.MettreApost(Control)
    End Function

    Public Sub LoadLangues(ByRef ComboBox As Object)
        Dim LesLanges As String() = {"Abenaki", "Afrikaans", "Allemand", "Albanais", "Alsacien", "Amharique", "Anglais", "Arabe", "Araméen", "Arménien", "Assamais", "Azéri", "Bachkir", "Basque", "Bengali", "Berbère", "Bichelamar", "Biélorusse", "Birman", "Bosniaque", "Brahui", "Breton", "Bulgare", "Cambodge", "Carélien", "Catalan", "Cherokee", "Ciluba", "Comorien", "Coréen", "Cornique", "Créole", "Croate", "Dalmate", "Dari", "Danois", "Drehu", "Rdzong-kha", "Ecossais", "Edo", "Espagnol", "Espéranto", "Estonien", "Finnois", "Français", "Frioulan", "Frison", "Galicien", "Gallo", "Gallois", "Géorgien", "Gotique", "Grec ancien", "Grec moderne", "Guarani", "Gujarati", "Haoussa", "Hébreu", "Hindi", "Hittite", "Hongrois", "Ilokano", "Indonésien", "Interlingua", "Inuit", "Inuktitut", "Irlandais", "Islandais", "Italien", "Japonais", "Javanais", "Jersiais", "Judéo-espagnol", "Kannada", "Kashmiri", "Kazakh", "Khanty", "Khmer", "Kikai", "Kim", "Kirghiz", "Kunigami", "Kurde", "Ladin", "Langue des signes", "Laotien", "Lapon", "Latin", "Letton", "Lingala", "Lituanien", "Live", "Luxembourgeois", "Macédonien", "Malais", "Malayalam", "Malgache", "Mandé", "Mannois", "Mansi", "Marathi", "Mari", "Masana ou masa", "Maya", "Meitei", "Miyako", "Mongol", "Nahuatl", "Nauruan", "Néerlandais", "Népalais", "Néware", "Niçois", "Normand", "Norvégien", "Nushu", "Occitan", "Okinawais", "Oriya", "Ossète", "Ouïgour", "Ourdou", "Ouzbek", "Pâli", "Pashto", "Penjabi", "Persan", "Peul", "Picard", "Pijin", "Polonais", "Portugais", "Prâkrit", "Provençal", "Qiang", "Quechua", "Romanche", "Roumain", "Rromani", "Russe", "Same", "Sanskrit", "Sarde", "Scots", "Serbe", "Serbo-croate", "Sicilien", "Slovaque", "Slovène", "Sorabe", "Suédois", "Swahili", "Tadjik", "Tagalog", "Tahitien", "Tamoul", "Tangoute", "Taraon-digaru", "Tatar", "Tchèque", "Tchérémisse", "Tchétchène", "Tchiluba", "Télougou", "Thaï", "Tibétain", "Tigrinya", "Tokharien", "Tok pisin", "Toungouse", "Toupouri", "Turc", "Turkmène", "Ukrainien", "Vepse", "Vietnamien", "Volapük", "Vote", "Wallon", "Wolof", "Xhosa", "Yiddish", "Yonaguni", "Yoruba", "Zoulou"}
        If TypeOf (ComboBox) Is DevExpress.XtraEditors.ComboBoxEdit Then
            Dim cmb As DevExpress.XtraEditors.ComboBoxEdit = CType(ComboBox, DevExpress.XtraEditors.ComboBoxEdit)
            cmb.Properties.Items.Clear()
            For Each langue As String In LesLanges
                cmb.Properties.Items.Add(langue)
            Next
        ElseIf TypeOf (ComboBox) Is ComboBox Then
            Dim cmb As ComboBox = CType(ComboBox, ComboBox)
            cmb.Items.Clear()
            For Each langue As String In LesLanges
                cmb.Items.Add(langue)
            Next
        End If
    End Sub
    Public Function IsGridEditMod(GridView As DevExpress.XtraGrid.Views.Grid.GridView, ByVal EditColName As String) As Integer
        Dim index As Integer = -1
        Try
            For i = 0 To GridView.RowCount - 1
                If CBool(GridView.GetRowCellValue(i, EditColName)) Then
                    Return i
                End If
            Next
            Return index
        Catch ex As Exception
            Return index
        End Try
    End Function
    Public Sub CancelGridEditMode(GridView As DevExpress.XtraGrid.Views.Grid.GridView, ByVal EditColName As String)
        Try
            For i = 0 To GridView.RowCount - 1
                If CBool(GridView.GetRowCellValue(i, EditColName)) Then
                    GridView.SetRowCellValue(i, EditColName, False)
                End If
            Next
        Catch ex As Exception

        End Try
    End Sub

    Public Function IsSavedItemInGridView(Obj As String, GridView As DevExpress.XtraGrid.Views.Grid.GridView, NumColonne As String) As Integer
        For i = 0 To GridView.RowCount - 1
            If Obj = GridView.GetRowCellValue(i, NumColonne).ToString().ToLower Then
                Return i
            End If
        Next
        Return -1
    End Function

    Public Function AjouterNouvelleSectionDocument(ByRef Doc As Word.Document, ByRef CurrentRange As Word.Range) As Word.Section
        Dim myRange As Word.Range = Doc.Bookmarks.Item("\endofdoc").Range
        Dim NewSec As Word.Section = Doc.Sections.Add(myRange)
        CurrentRange = NewSec.Range
        Return NewSec
    End Function

    Sub NumBonCommande_Auto(ByVal montext As DevExpress.XtraEditors.TextEdit)
        Try
            query = "SELECT COUNT(ID_BC) as Nbre FROM t_boncommande WHERE Annee=YEAR(NOW())"
            Dim nbBonCommande As Decimal = 1
            Try
                nbBonCommande = Val(ExecuteScallar(query).ToString()) + 1
            Catch ex As Exception
            End Try
            montext.Text = ProjetEnCours & "/" & Now.Year & "/N°" & nbBonCommande
        Catch ex As Exception
            SuccesMsg("Information non disponible : " & ex.ToString())
        End Try
    End Sub

End Module