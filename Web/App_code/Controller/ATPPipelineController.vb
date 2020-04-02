Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data

Public Class ATPPipelineController
    Inherits BaseController

    Public Sub ATPPipelineInsert(ByVal info As ATPPipelineInfo)
        Command = New SqlCommand("uspATP_InsertQueingDoc", Connection)
        Command.CommandType = CommandType.StoredProcedure
        Dim prmOriginalFilename As New SqlParameter("@originalfilename", SqlDbType.VarChar, 500)
        Dim prmOriginalPath As New SqlParameter("@originalpath", SqlDbType.VarChar, 1000)
        Dim prmPathFolder As New SqlParameter("@pathfolder", SqlDbType.VarChar, 1000)
        Dim prmPackageId As New SqlParameter("@packageid", SqlDbType.VarChar, 50)
        Dim prmStatus As New SqlParameter("@status", SqlDbType.VarChar, 50)
        prmOriginalFilename.Value = info.OriginalFilename
        prmOriginalPath.Value = info.OriginalPath
        prmPathFolder.Value = info.PathFolder
        prmPackageId.Value = info.PackageId
        prmStatus.Value = info.Status
        Command.Parameters.Add(prmOriginalFilename)
        Command.Parameters.Add(prmOriginalPath)
        Command.Parameters.Add(prmPathFolder)
        Command.Parameters.Add(prmPackageId)
        Command.Parameters.Add(prmStatus)

        Try
            Connection.Open()
            Command.ExecuteNonQuery()
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try

    End Sub

    Public Function GetATPPipelineProcessById(ByVal taskpendingid As Int32) As ATPPipelineInfo
        Dim info As New ATPPipelineInfo
        Command = New SqlCommand("uspATP_GetPipelineById", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmTaskPendingId As New SqlParameter("@taskpendingid", SqlDbType.BigInt)
        prmTaskPendingId.Value = taskpendingid
        Command.Parameters.Add(prmTaskPendingId)

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()

            If DataReader.HasRows Then
                While (DataReader.Read())
                    info.TaskPendingId = Convert.ToInt32(DataReader.Item("TaskPending_Id"))
                    info.OriginalFilename = Convert.ToString(DataReader.Item("Original_Filename"))
                    info.OriginalPath = Convert.ToString(DataReader.Item("Original_Path"))
                    info.PathFolder = Convert.ToString(DataReader.Item("Path_Folder"))
                    info.PackageId = Convert.ToString(DataReader.Item("Package_Id"))
                    info.Status = Convert.ToString(DataReader.Item("Status"))
                End While
            End If
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try
        Return info
    End Function

    Public Sub UpdateATPQueing(ByVal status As String, ByVal taskpendingid As Int32)
        Command = New SqlCommand("uspATP_UpdateQueingStatus", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmStatus As New SqlParameter("@status", SqlDbType.VarChar, 50)
        Dim prmTaskPendingId As New SqlParameter("@taskpendingid", SqlDbType.BigInt)
        prmStatus.Value = status
        prmTaskPendingId.Value = taskpendingid
        Command.Parameters.Add(prmStatus)
        Command.Parameters.Add(prmTaskPendingId)
        Try
            Connection.Open()
            Command.ExecuteNonQuery()
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try


    End Sub

    Public Sub UpdateATPPath(ByVal packageid As String, ByVal path As String, ByVal docid As Integer)
        Command = New SqlCommand("uspATP_UpdatePathSiteDoc", Connection)
        Command.CommandType = CommandType.StoredProcedure
        Dim prmPackageId As New SqlParameter("@Packageid", SqlDbType.VarChar, 50)
        Dim prmPath As New SqlParameter("@path", SqlDbType.VarChar, 1000)
        Dim prmDocId As New SqlParameter("@docid", SqlDbType.Int)

        prmPackageId.Value = packageid
        prmPath.Value = path
        prmDocId.Value = docid

        Command.Parameters.Add(prmPackageId)
        Command.Parameters.Add(prmPath)
        Command.Parameters.Add(prmDocId)

        Try
            Connection.Open()
            Command.ExecuteNonQuery()
        Catch ex As Exception

        Finally
            Connection.Close()
        End Try

    End Sub
End Class
