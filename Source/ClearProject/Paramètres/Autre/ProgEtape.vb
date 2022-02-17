Imports System.Windows.Forms
Imports MySql.Data.MySqlClient

Public Class ProgEtape

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        If (CmbRespoEtpe.Text <> "") Then
            DateEtpPlan = DateDebutEtape.Text
            Me.Close()
        End If

    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        DateEtpPlan = ""
        Me.Close()
    End Sub

    Private Sub ProgEtape_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)  Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        If (DateEtpPlan = "") Then
            DateDebutEtape.Value = Now.ToShortDateString
        Else
            DateDebutEtape.Value = CDate(DateEtpPlan).ToShortDateString
        End If
        If (TitreEtpPlan <> "") Then
            TitreEtape.Text = TitreEtpPlan
        End If
        DureeEtape.Text = DureeEtpPlan
        DateEtpPlan = ""
        'If (DateDebutEtape.Enabled = False) Then
        '    OK_Button_Click(Me, e)
        'Else
        ChargerRespo()
        'End If
        'If (DateDebutEtape.Enabled = False And CmbRespoEtpe.Text <> "") Then
        '    OK_Button_Click(Me, e)
        'End If
    End Sub
    Private Sub ChargerRespo()

        query = "select CodeOperateur,NomOperateur,PrenOperateur from T_Operateur where CodeProjet='" & ProjetEnCours & "' order by NomOperateur"
        CmbRespoEtpe.Items.Clear()
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            CmbRespoEtpe.Items.Add(MettreApost(rw(0).ToString & " - " & rw(1).ToString & " " & rw(2).ToString))
        Next

    End Sub

    Private Sub CmbRespoEtpe_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbRespoEtpe.SelectedValueChanged
        Dim Resp() As String = CmbRespoEtpe.Text.Split(" "c)
        TxtRespoCache.Text = Resp(0)
        RespoEtape = CInt(TxtRespoCache.Text)
    End Sub
End Class