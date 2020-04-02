Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections.Generic

Public Class PermissionController
    Inherits BaseController

    Public Function ODPermission_IU(ByVal info As PermissionInfo) As Boolean
        Dim isSucceed As Boolean = True
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("uspODPermission_IU", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmPermissionId As New SqlParameter("@permissionid", SqlDbType.Int)
        Dim prmPermissionType As New SqlParameter("@permissiontype", SqlDbType.VarChar, 50)
        Dim prmPermissionCategory As New SqlParameter("@permissioncategory", SqlDbType.VarChar, 50)
        Dim prmRoleId As New SqlParameter("@roleid", SqlDbType.Int)
        Dim prmLMBY As New SqlParameter("@lmby", SqlDbType.Int)

        prmPermissionId.Value = info.PermissionId
        prmPermissionType.Value = info.PermissionType
        prmPermissionCategory.Value = info.PermissionCategory
        prmRoleId.Value = info.RoleId
        prmLMBY.Value = info.LMBY

        Command.Parameters.Add(prmPermissionId)
        Command.Parameters.Add(prmPermissionType)
        Command.Parameters.Add(prmPermissionCategory)
        Command.Parameters.Add(prmRoleId)
        Command.Parameters.Add(prmLMBY)

        Try
            Connection.Open()
            Command.ExecuteNonQuery()
        Catch ex As Exception
            isSucceed = False
            strErrMessage = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try

        If isSucceed = False Then
            ErrorLogInsert(5, "ODPermission_IU", strErrMessage, "uspODPermission_IU")
        End If

        Return isSucceed
    End Function

    Public Function GetODPermissions() As List(Of PermissionInfo)
        Dim list As New List(Of PermissionInfo)
        Dim isSucceed As Boolean = True
        Dim strErrMessage As String = String.Empty

        Command = New SqlCommand("uspODPermission_GetPermission", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read
                    Dim info As New PermissionInfo
                    info.PermissionId = Integer.Parse(DataReader.Item("Permission_Id").ToString())
                    info.PermissionType = Convert.ToString(DataReader.Item("Permission_Type"))
                    info.PermissionCategory = Convert.ToString(DataReader.Item("Permission_Category"))
                    info.RoleId = Integer.Parse(DataReader.Item("Role_Id").ToString())
                    info.RoleName = Convert.ToString(DataReader.Item("RoleDesc"))
                    info.LMBY = Integer.Parse(DataReader.Item("LMBY").ToString())
                    info.ModifiedUser = Convert.ToString(DataReader.Item("modifieduser"))
                    info.LMDT = Convert.ToDateTime(DataReader.Item("LMDT"))
                    list.Add(info)
                End While
            End If
        Catch ex As Exception
            isSucceed = False
            strErrMessage = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try

        If isSucceed = False Then
            ErrorLogInsert(5, "GetODPermissions", strErrMessage, "uspODPermission_GetPermission")
        End If

        Return list
    End Function

    Public Function IsAvailablePermission(ByVal roleid As Integer, ByVal permissioncategory As String, ByVal permissiontype As String) As Boolean
        Dim info As New PermissionInfo
        Dim isAvailable As Boolean = False
        Dim isSucceed As Boolean = True
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("uspODPermission_CheckIsAvailable", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmPermissionType As New SqlParameter("@permissiontype", SqlDbType.VarChar, 50)
        Dim prmPermissionCategory As New SqlParameter("@permissioncategory", SqlDbType.VarChar, 50)
        Dim prmRoleId As New SqlParameter("@roleid", SqlDbType.Int)
        Dim prmIsAvailable As New SqlParameter("@isAvailable", SqlDbType.Bit)

        prmPermissionCategory.Value = permissioncategory
        prmPermissionType.Value = permissiontype
        prmRoleId.Value = roleid
        prmIsAvailable.Direction = ParameterDirection.Output

        Command.Parameters.Add(prmPermissionCategory)
        Command.Parameters.Add(prmPermissionType)
        Command.Parameters.Add(prmRoleId)
        Command.Parameters.Add(prmIsAvailable)

        Try
            Connection.Open()
            Command.ExecuteNonQuery()
            isAvailable = Convert.ToBoolean(prmIsAvailable.Value)
        Catch ex As Exception
            isSucceed = False
            strErrMessage = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try

        If isSucceed = False Then
            ErrorLogInsert(5, "IsAvailablePermission", strErrMessage, "uspODPermission_CheckIsAvailable")
        End If

        Return isAvailable
    End Function

    Public Function ODPermission_D(ByVal permissionid As Integer) As Boolean
        Dim isSucceed As Boolean = True
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("delete ODPermission_Feature where permission_id=" & permissionid, Connection)
        Command.CommandType = CommandType.Text

        Try
            Connection.Open()
            Command.ExecuteNonQuery()
        Catch ex As Exception
            isSucceed = False
            strErrMessage = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try

        If isSucceed = False Then
            ErrorLogInsert(5, "ODPermission_D", strErrMessage, "CommandType.Text")
        End If

        Return isSucceed
    End Function

    Public Function GetRoles() As List(Of RoleInfo)
        Dim list As New List(Of RoleInfo)
        Dim isSucceed As Boolean = True
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("select rol.*, utype.grpdesc,(case LvlCode when 'A' then 'Area' when 'R' then 'Region' else 'National'	 end) 'LvlCodeDesc' from TRole rol inner join TUserType utype on rol.grpid = utype.grpid where(rol.rstatus = 2) order by rol.grpid,rol.lvlcode,rol.roledesc asc", Connection)
        Command.CommandType = CommandType.Text

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read()
                    Dim info As New RoleInfo
                    info.RoleId = Integer.Parse(DataReader.Item("RoleId").ToString())
                    info.RoleName = Convert.ToString(DataReader.Item("GrpDesc") & "-" & Convert.ToString(DataReader.Item("LvlCodeDesc")) & "-" & Convert.ToString(DataReader.Item("RoleDesc")))
                    list.Add(info)
                End While
            End If
        Catch ex As Exception
            isSucceed = False
            strErrMessage = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try
        If isSucceed = False Then
            ErrorLogInsert(5, "GetRoles", strErrMessage, "CommandType.Text")
        End If

        Return list
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
