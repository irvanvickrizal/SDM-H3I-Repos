Imports Microsoft.VisualBasic
Imports System.data.SqlClient
Imports System.Data
Imports System.Collections.Generic



Public Class PackageNameController
    Inherits BaseController



    Public Function PackageName_IU(ByVal info As PackageNameInfo) As Boolean
        Dim isSucceed As Boolean = True
        Command = New SqlCommand("uspCOD_PackageName_IU", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmPackageNameId As New SqlParameter("@packagenameid", SqlDbType.Int)
        Dim prmPackageName As New SqlParameter("@packagename", SqlDbType.VarChar, 500)
        Dim prmDescription As New SqlParameter("@description", SqlDbType.VarChar, 500)
        Dim prmIsActive As New SqlParameter("@isActive", SqlDbType.Bit)
        Dim prmLMBY As New SqlParameter("@lmby", SqlDbType.VarChar, 250)
        Dim prmDScopeId As New SqlParameter("@dscopeid", SqlDbType.Int)

        prmPackageNameId.Value = info.PackageNameId
        prmPackageName.Value = info.PackageName
        prmDescription.Value = info.Description
        prmIsActive.Value = info.IsActive
        prmLMBY.Value = info.LMBY
        prmDScopeId.Value = info.DScopeId

        Command.Parameters.Add(prmPackageNameId)
        Command.Parameters.Add(prmPackageName)
        Command.Parameters.Add(prmDescription)
        Command.Parameters.Add(prmIsActive)
        Command.Parameters.Add(prmLMBY)
        Command.Parameters.Add(prmDScopeId)

        Try
            Connection.Open()
            Command.ExecuteNonQuery()
        Catch ex As Exception
            isSucceed = False
        Finally
            Connection.Close()
        End Try
        Return isSucceed
    End Function

    Public Function GetPackageNames(ByVal isActive As Boolean) As List(Of PackageNameInfo)
        Dim list As New List(Of PackageNameInfo)
        Command = New SqlCommand("uspCOD_GetPackageNames", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmIsActive As New SqlParameter("@isActive", SqlDbType.Bit)
        prmIsActive.Value = isActive
        Command.Parameters.Add(prmIsActive)

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read()
                    Dim info As New PackageNameInfo
                    info.PackageNameId = Integer.Parse(DataReader.Item("PackageName_Id").ToString())
                    info.PackageName = Convert.ToString(DataReader.Item("Package_name"))
                    info.IsActive = Convert.ToBoolean(DataReader.Item("IsActive"))
                    info.LMBY = Convert.ToString(DataReader.Item("LMBY"))
                    info.LMDT = Convert.ToDateTime(DataReader.Item("LMDT"))
                    info.Description = Convert.ToString(DataReader.Item("Description"))
                    info.DScopeId = Integer.Parse(DataReader.Item("DScope_Id").ToString())
                    info.DScopeName = Convert.ToString(DataReader.Item("DScope_Name"))
                    info.MScopeName = Convert.ToString(DataReader.Item("MasterScope"))
                    list.Add(info)
                End While
            End If
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try
        Return list
    End Function
    Public Sub DeletePackageNameTemp(ByVal packagenameid As Integer)
        Command = New SqlCommand("uspCOD_DeletePackageName_Temp", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmPackageNameId As New SqlParameter("@packagenameid", SqlDbType.Int)
        prmPackageNameId.Value = packagenameid
        Command.Parameters.Add(prmPackageNameId)


        Try
            Connection.Open()
            Command.ExecuteNonQuery()
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try
    End Sub
End Class
