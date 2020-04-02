Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Collections.Generic

Public Class CODActivityController
    Inherits BaseController

    Public Function GetCODActivities(ByVal isdeleted As Boolean) As List(Of CODActivityInfo)
        Dim list As New List(Of CODActivityInfo)

        Command = New SqlCommand("uspCOD_GetActivities", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmIsDeleted As New SqlParameter("@isDeleted", SqlDbType.Bit)
        prmIsDeleted.Value = isdeleted
        Command.Parameters.Add(prmIsDeleted)
        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If (DataReader.HasRows) Then
                While (DataReader.Read())
                    Dim info As New CODActivityInfo
                    info.ActivityId = Integer.Parse(DataReader.Item("Activity_Id").ToString())
                    info.ActivityName = Convert.ToString(DataReader.Item("Activity_Name"))
                    info.ActivityDesc = Convert.ToString(DataReader.Item("Activity_Desc"))
                    info.LMBY = Integer.Parse(DataReader.Item("LMBY").ToString())
                    info.Lmdt = Convert.ToDateTime(DataReader.Item("LMDT"))
                    info.ModifiedUser = Convert.ToString(DataReader.Item("name"))
                    info.CDT = Convert.ToDateTime(DataReader.Item("CDT"))
                    list.Add(info)
                End While
            End If
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try
        Return list
    End Function

    Public Function GetCODActivity(ByVal activityid As Integer) As CODActivityInfo
        Dim info As New CODActivityInfo

        Command = New SqlCommand("uspCOD_GetActivity", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmActivityId As New SqlParameter("@activityid", SqlDbType.Int)
        prmActivityId.Value = activityid
        Command.Parameters.Add(prmActivityId)

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While (DataReader.Read())
                    info.ActivityId = Integer.Parse(DataReader.Item("Activity_Id").ToString())
                    info.ActivityName = Convert.ToString(DataReader.Item("Activity_Name").ToString())
                    info.LMBY = Integer.Parse(DataReader.Item("LMBY").ToString())
                    info.Lmdt = Convert.ToDateTime(DataReader.Item("LMDT"))
                    info.CDT = Convert.ToDateTime(DataReader.Item("CDT"))
                End While
            End If
        Catch ex As Exception

        Finally
            Connection.Close()
        End Try

        Return info
    End Function

    Public Function GetDocActivityNotUsedBaseOnDocId(ByVal docid As Integer, ByVal docparenttype As String) As List(Of CODActivityInfo)
        Dim list As New List(Of CODActivityInfo)

        Command = New SqlCommand("uspCOD_DocActivity_NotInUsed", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmDocId As New SqlParameter("@docid", SqlDbType.Int)
        Dim prmDocParentType As New SqlParameter("@parentdoctype", SqlDbType.VarChar, 50)

        prmDocId.Value = docid
        prmDocParentType.Value = docparenttype

        Command.Parameters.Add(prmDocId)
        Command.Parameters.Add(prmDocParentType)

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While (DataReader.Read())
                    Dim info As New CODActivityInfo
                    info.ActivityId = Integer.Parse(DataReader.Item("Activity_Id").ToString())
                    info.ActivityName = Convert.ToString(DataReader.Item("Activity_Name").ToString())
                    info.LMBY = Integer.Parse(DataReader.Item("LMBY").ToString())
                    info.Lmdt = Convert.ToDateTime(DataReader.Item("LMDT"))
                    info.CDT = Convert.ToDateTime(DataReader.Item("CDT"))
                    list.Add(info)
                End While
            End If
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try

        Return list
    End Function

    Public Function CODActivity_IU(ByVal info As CODActivityInfo) As Boolean
        Dim isSucceed As Boolean = True
        Command = New SqlCommand("uspCOD_Activity_IU", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmActivityId As New SqlParameter("@activityid", SqlDbType.Int)
        Dim prmActivityName As New SqlParameter("@activityname", SqlDbType.VarChar, 250)
        Dim prmActivityDesc As New SqlParameter("@activitydesc", SqlDbType.VarChar, 2000)
        Dim prmLMBY As New SqlParameter("@lmby", SqlDbType.Int)
        Dim prmIsDeleted As New SqlParameter("@isDeleted", SqlDbType.Bit)

        prmActivityId.Value = info.ActivityId
        prmActivityName.Value = info.ActivityName
        prmActivityDesc.Value = info.ActivityDesc
        prmLMBY.Value = info.LMBY
        prmIsDeleted.Value = info.IsDeleted

        Command.Parameters.Add(prmActivityId)
        Command.Parameters.Add(prmActivityName)
        Command.Parameters.Add(prmActivityDesc)
        Command.Parameters.Add(prmLMBY)
        Command.Parameters.Add(prmIsDeleted)

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

    Public Sub CODActivity_TempDeleted(ByVal activityid As Integer)
        Command = New SqlCommand("update CODActivity set isDeleted=1 where activity_Id=" & activityid, Connection)
        Command.CommandType = CommandType.Text

        Try
            Connection.Open()
            Command.ExecuteNonQuery()
        Catch ex As Exception

        Finally
            Connection.Close()
        End Try

    End Sub

    Public Function GroupingWCCDocActivity_I(ByVal info As CODDocActivityGroupingInfo) As Boolean
        Dim isSucceed As Boolean = True

        Command = New SqlCommand("uspCOD_GroupingWCCDocActivity_I", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmActivityId As New SqlParameter("@activityId", SqlDbType.Int)
        Dim prmDocId As New SqlParameter("@DocId", SqlDbType.Int)
        Dim prmParentDocType As New SqlParameter("@parentdoctype", SqlDbType.VarChar, 50)
        Dim prmLMBY As New SqlParameter("@lmby", SqlDbType.Int)

        prmActivityId.Value = info.ActivityId
        prmDocId.Value = info.DocId
        prmParentDocType.Value = info.ParentDocType
        prmLMBY.Value = info.LMBY

        Command.Parameters.Add(prmActivityId)
        Command.Parameters.Add(prmDocId)
        Command.Parameters.Add(prmParentDocType)
        Command.Parameters.Add(prmLMBY)

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

    Public Function GetGroupingWCCDocActivity() As List(Of CODDocActivityGroupingInfo)
        Dim list As New List(Of CODDocActivityGroupingInfo)
        Command = New SqlCommand("uspCOD_GetGroupingWCCDocActivity", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader
            If DataReader.HasRows Then
                While (DataReader.Read())
                    Dim info As New CODDocActivityGroupingInfo
                    info.DocActivityId = Convert.ToInt32(DataReader.Item("DocActivity_Id"))
                    info.ActivityId = Integer.Parse(DataReader.Item("Activity_Id"))
                    info.ActivityName = Convert.ToString(DataReader.Item("Activity_Name"))
                    info.DocId = Integer.Parse(DataReader.Item("doc_id").ToString())
                    info.DocName = Convert.ToString(DataReader.Item("DocName"))
                    info.ParentDocType = Convert.ToString(DataReader.Item("ParentDocType"))
                    info.LMDT = Convert.ToDateTime(DataReader.Item("LMDT"))
                    info.LMBY = Integer.Parse(DataReader.Item("LMBY").ToString())
                    info.ModifiedUser = Convert.ToString(DataReader.Item("ModifiedUser").ToString())
                    info.CDT = Convert.ToDateTime(DataReader.Item("CDT"))
                    list.Add(info)
                End While
            End If
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try

        Return list
    End Function

    Public Function GroupingRoleActivity_I(ByVal roleid As Integer, ByVal activityid As Integer, ByVal userid As Integer) As Boolean
        Dim isSucceed As Boolean = True
        Command = New SqlCommand("uspCOD_GroupingRoleActivity_I", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmRoleId As New SqlParameter("@roleid", SqlDbType.Int)
        Dim prmActivityId As New SqlParameter("@activityid", SqlDbType.Int)
        Dim prmLMBY As New SqlParameter("@lmby", SqlDbType.Int)

        prmRoleId.Value = roleid
        prmActivityId.Value = activityid
        prmLMBY.Value = userid

        Command.Parameters.Add(prmRoleId)
        Command.Parameters.Add(prmActivityId)
        Command.Parameters.Add(prmLMBY)

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

    Public Function GetGroupingRoleActivities() As List(Of CODActivityRoleGroupingInfo)
        Dim list As New List(Of CODActivityRoleGroupingInfo)
        Command = New SqlCommand("uspCOD_GetGroupingRoleActivity", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read()
                    Dim info As New CODActivityRoleGroupingInfo
                    info.RoleActivityId = Integer.Parse(DataReader.Item("RoleActivity_Id").ToString())
                    info.RoleId = Integer.Parse(DataReader.Item("RoleId").ToString())
                    info.RoleName = Convert.ToString(DataReader.Item("RoleDesc"))
                    info.UserType = Convert.ToString(DataReader.Item("GrpDesc"))
                    info.CDT = Convert.ToDateTime(DataReader.Item("CDT"))
                    info.LMDT = Convert.ToDateTime(DataReader.Item("LMDT"))
                    info.LMBY = Integer.Parse(DataReader.Item("LMBY").ToString())
                    info.ActivityName = Convert.ToString(DataReader.Item("Activity_Name"))
                    info.ModifiedUser = Convert.ToString(DataReader.Item("ModifiedUser"))
                    list.Add(info)
                End While
            End If
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try

        Return list
    End Function

    Public Sub GroupinRoleActivity_D(ByVal roleactivityid As Integer)
        Command = New SqlCommand("delete CODGroupingRoleActivity where RoleActivity_Id=" & roleactivityid, Connection)
        Command.CommandType = CommandType.Text

        Try
            Connection.Open()
            Command.ExecuteNonQuery()
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try

    End Sub

    Public Function GetActivityIdBaseRole(ByVal roleid As Integer) As Integer
        Dim activityid As Integer = 0
        Command = New SqlCommand("uspCOD_GetActivityBaseRole", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmActivityId As New SqlParameter("@activityid", SqlDbType.Int)
        Dim prmRoleId As New SqlParameter("@roleid", SqlDbType.Int)

        prmActivityId.Direction = ParameterDirection.Output
        prmRoleId.Value = roleid

        Command.Parameters.Add(prmActivityId)
        Command.Parameters.Add(prmRoleId)

        Try
            Connection.Open()
            Command.ExecuteNonQuery()
            activityid = Integer.Parse(prmActivityId.Value.ToString())
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try


        Return activityid
    End Function

    Public Function DocActivityLock_I(ByVal activityid As Integer, ByVal docid As Integer, ByVal parentdoctype As String, ByVal userid As Integer) As Boolean
        Dim isSucceed As Boolean = True

        Command = New SqlCommand("uspCOD_DocActivityLock_I", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmDocId As New SqlParameter("@docid", SqlDbType.Int)
        Dim prmActivityId As New SqlParameter("@activityid", SqlDbType.Int)
        Dim prmParentDocType As New SqlParameter("@parentdoctype", SqlDbType.VarChar, 50)

        prmDocId.Value = docid
        prmActivityId.Value = activityid
        prmParentDocType.Value = parentdoctype

        Command.Parameters.Add(prmDocId)
        Command.Parameters.Add(prmActivityId)
        Command.Parameters.Add(prmParentDocType)

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

    Public Function GetDocActivityLockBaseActivity(ByVal activityid As Integer) As CODDocActivityLockInfo
        Dim info As New CODDocActivityLockInfo

        Command = New SqlCommand("uspCOD_GetDocActivityLock_BaseActivity", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmActivityId As New SqlParameter("@activityid", SqlDbType.Int)
        prmActivityId.Value = activityid
        Command.Parameters.Add(prmActivityId)

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read
                    info.DocActivityLockId = Integer.Parse(DataReader.Item("DocActivityLock_Id").ToString())
                    info.ActivityId = Integer.Parse(DataReader.Item("Activity_Id").ToString())
                    info.DocId = Integer.Parse(DataReader.Item("doc_id").ToString())
                    info.ParentDocType = Convert.ToString(DataReader.Item("ParentDocType"))
                    info.Disclaimer = Convert.ToString(DataReader.Item("Disclaimer"))
                End While
            End If
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try

        Return info
    End Function

    Public Function IsDocIsInActivityRole(ByVal docid As Integer, ByVal activityid As Integer, ByVal parentdoctype As String) As Boolean
        Dim isValid As Boolean = True
        Dim isError As Boolean = False
        Dim strError As String = String.Empty

        Command = New SqlCommand("uspWCC_IsDocIsInActivityRole", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmDocId As New SqlParameter("@docid", SqlDbType.Int)
        Dim prmActivityId As New SqlParameter("@activityId", SqlDbType.Int)
        Dim prmParentDocType As New SqlParameter("@parentdoctype", SqlDbType.VarChar, 10)
        Dim prmIsTrue As New SqlParameter("@isTrue", SqlDbType.Bit)

        prmDocId.Value = docid
        prmActivityId.Value = activityid
        prmParentDocType.Value = parentdoctype
        prmIsTrue.Direction = ParameterDirection.Output

        Command.Parameters.Add(prmDocId)
        Command.Parameters.Add(prmActivityId)
        Command.Parameters.Add(prmParentDocType)
        Command.Parameters.Add(prmIsTrue)

        Try
            Connection.Open()
            Command.ExecuteNonQuery()
            isValid = Convert.ToBoolean(prmIsTrue.Value)
        Catch ex As Exception
            isError = True
            strError = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try

        If (isError = True) Then
            ErrorLogInsert(11, "IsDocIsInActivityRole", strError, "uspWCC_IsDocIsInActivityRole")
        End If

        Return isValid
    End Function

    Public Function GetDocIDBaseActivityRole(ByVal activityid As Integer) As Integer
        Dim doclockid As Integer = 0
        Command = New SqlCommand("uspCOD_GetDocId_BaseActivity", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmDocId As New SqlParameter("@docid", SqlDbType.Int)
        Dim prmActivityId As New SqlParameter("@activityid", SqlDbType.Int)

        prmDocId.Direction = ParameterDirection.Output
        prmActivityId.Value = activityid

        Command.Parameters.Add(prmDocId)
        Command.Parameters.Add(prmActivityId)

        Try
            Connection.Open()
            Command.ExecuteNonQuery()
            doclockid = Integer.Parse(prmDocId.Value.ToString())
        Catch ex As Exception

        Finally
            Connection.Close()
        End Try

        Return doclockid
    End Function

    ''' <summary>
    ''' Created by Fauzan 24 Dec 2018
    ''' Delete WCC Document grouping activity
    ''' </summary>
    ''' <param name="docActivityId"></param>
    ''' <returns>Boolean</returns>
    Public Function GroupingWCCDocActivity_D(ByVal docActivityId As Integer) As Boolean
        Command = New SqlCommand("delete from CODGroupingWCCDocActivity where DocActivity_Id=" & docActivityId, Connection)
        Command.CommandType = CommandType.Text
        Try
            Connection.Open()
            Command.ExecuteNonQuery()
        Catch ex As Exception
            Connection.Close()
            Return False
        Finally
            Connection.Close()
        End Try

        Return True
    End Function

#Region "Error Log"
    Public Sub ErrorLogInsert(ByVal errcode As Integer, ByVal erroperation As String, ByVal errdesc As String, ByVal errsp As String)
        Command = New SqlCommand("uspErrLog", Connection)
        Command.CommandType = CommandType.StoredProcedure
        Dim prmErrCode As New SqlParameter("@errcode", SqlDbType.Int)
        Dim prmErrOperation As New SqlParameter("@errOperation", SqlDbType.VarChar, 100)
        Dim prmErrDesc As New SqlParameter("@errdesc", SqlDbType.VarChar, 1000)
        Dim prmErrSP As New SqlParameter("@errsp", SqlDbType.VarChar, 50)

        prmErrCode.Value = errcode
        prmErrOperation.Value = erroperation
        prmErrDesc.Value = errdesc
        prmErrSP.Value = errsp

        Command.Parameters.Add(prmErrCode)
        Command.Parameters.Add(prmErrOperation)
        Command.Parameters.Add(prmErrDesc)
        Command.Parameters.Add(prmErrSP)


        Try
            Connection.Open()
            Command.ExecuteNonQuery()
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try

    End Sub
#End Region
End Class
