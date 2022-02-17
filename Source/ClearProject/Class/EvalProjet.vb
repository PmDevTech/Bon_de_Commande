Imports System.Math
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Public Class EvalProjet
    Public Shared Function GetDotationActivite(ByVal CodePartition As Decimal) As Decimal
        Dim Dotation As Decimal = 0
        query = "SELECT SUM(`QteNature`*`PUNature`) as Total FROM `t_besoinpartition` WHERE `CodePartition`='" & CodePartition & "' AND CodeProjet='" & ProjetEnCours & "'"
        Dotation = Val(ExecuteScallar(query))
        Return Dotation
    End Function
    Public Shared Function GetDotationCompoAndSousCompo(ByVal LibelleCourt As String, DateDebut As Date, DateFin As Date) As Decimal
        Dim Dotation As Decimal = 0
        query = "SELECT codepartition FROM t_partition WHERE LibelleCourt LIKE '" & LibelleCourt & "%' AND DateDebutPartition<='" & dateconvert(DateDebut) & "' AND DateFinPartition>='" & dateconvert(DateFin) & "' AND CodeClassePartition='5' AND CodeProjet='" & ProjetEnCours & "'"
        Dim dtActivites As DataTable = ExcecuteSelectQuery(query)
        For Each rwActivite As DataRow In dtActivites.Rows
            query = "SELECT SUM(`QteNature`*`PUNature`) as Total FROM `t_besoinpartition` WHERE `CodePartition`='" & rwActivite("codepartition") & "' AND CodeProjet='" & ProjetEnCours & "'"
            Dotation += Val(ExecuteScallar(query))
        Next
        Return Dotation
    End Function

    Public Shared Function GetDotationActiviteMethode2(ByVal CodePartition As Decimal, DateDebut As Date, DateFin As Date) As Decimal
        Dim Dotation As Decimal = 0
        query = "SELECT SUM(`MontantEcheance`) as Total FROM `t_echeanceactivite` WHERE `CodePartition`='" & CodePartition & "' AND STR_TO_DATE(`DateEcheance`,'%d/%m/%Y')>='" & dateconvert(DateDebut) & "' AND STR_TO_DATE(`DateEcheance`,'%d/%m/%Y')<='" & dateconvert(DateFin) & "'"
        Dotation = Val(ExecuteScallar(query))
        Return Dotation
    End Function
    Public Shared Function GetDotationCompoAndSousCompoMethode2(ByVal LibelleCourt As String, DateDebutActivité As Date, DateFinActivité As Date, DateDebut As Date, DateFin As Date) As Decimal
        Dim Dotation As Decimal = 0
        query = "SELECT codepartition FROM t_partition WHERE LibelleCourt LIKE '" & LibelleCourt & "%' AND DateDebutPartition>='" & dateconvert(DateDebutActivité) & "' AND DateFinPartition<='" & dateconvert(DateFinActivité) & "' AND CodeClassePartition='5' AND CodeProjet='" & ProjetEnCours & "'"
        Dim dtActivites As DataTable = ExcecuteSelectQuery(query)
        For Each rwActivite As DataRow In dtActivites.Rows
            query = "SELECT SUM(`MontantEcheance`) as Total FROM `t_echeanceactivite` WHERE `CodePartition`='" & rwActivite("codepartition") & "' AND STR_TO_DATE(`DateEcheance`,'%d/%m/%Y')>='" & dateconvert(DateDebut) & "' AND STR_TO_DATE(`DateEcheance`,'%d/%m/%Y')<='" & dateconvert(DateFin) & "'"
            Dotation += Val(ExecuteScallar(query))
        Next
        Return Dotation
    End Function
End Class
