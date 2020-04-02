Imports Microsoft.VisualBasic
Imports System.Configuration
Imports System.Collections.Generic
Imports System.Data.SqlClient
Imports System.Data

Public Class CODBCCConfigController
    Inherits BaseController

    Public Function GetBCCConfiguration(ByVal bcctype As String) As List(Of CODEmailBCCInfo)
        Dim list As New List(Of CODEmailBCCInfo)
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("uspGeneral_GetEmailBCCConfiguration", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmEmailDocType As New SqlParameter("@emaildoctype", SqlDbType.VarChar, 300)
        If String.IsNullOrEmpty(bcctype) Then
            prmEmailDocType.Value = Nothing
        Else
            prmEmailDocType.Value = bcctype
        End If
        Command.Parameters.Add(prmEmailDocType)

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read()
                    Dim info As New CODEmailBCCInfo
                    info.SNO = Integer.Parse(DataReader.Item("EmailConf_Id").ToString())
                    info.EmailDocType = Convert.ToString(DataReader.Item("Email_Doc_Type"))
                    info.ModifiedUser = Convert.ToString(DataReader.Item("LMBY"))
                    info.CDT = Convert.ToDateTime(DataReader.Item("LMDT"))
                    info.UserInfo.UserId = Integer.Parse(DataReader.Item("usr_id")).ToString()
                    info.UserInfo.Fullname = Convert.ToString(DataReader.Item("name"))
                    info.UserInfo.Email = Convert.ToString(DataReader.Item("Email"))
                    info.UserInfo.PhoneNo = Convert.ToString(DataReader.Item("PhoneNo"))
                    info.RoleInf.RoleName = Convert.ToString(DataReader.Item("RoleDesc"))
                    list.Add(info)
                End While
            End If
        Catch ex As Exception
            strErrMessage = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try

        If Not String.IsNullOrEmpty(strErrMessage) Then
            ErrorLogInsert(101, "GetBCCConfiguration", strErrMessage, "uspGeneral_GetEmailBCCConfiguration")
        End If

        Return list
    End Function

    Public Function GetRoles() As List(Of RoleInfo)
        Dim list As New List(Of RoleInfo)
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("select * from TRole where rstatus = 2 order by GRPID asc", Connection)
        Command.CommandType = CommandType.Text

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read
                    Dim info As New RoleInfo
                    info.RoleId = Integer.Parse(DataReader.Item("RoleID").ToString())
                    Dim strCompanyName As String = String.Empty
                    If Integer.Parse(DataReader.Item("GRPID")) = 1 Then
                        strCompanyName = "NSN"
                    ElseIf Integer.Parse(DataReader.Item("GRPID")) = 2 Then
                        strCompanyName = "Subcon"
                    Else
                        strCompanyName = "Tsel"
                    End If
                    info.RoleName = strCompanyName & "-" & Convert.ToString(DataReader.Item("RoleDesc"))
                    list.Add(info)
                End While
            End If
        Catch ex As Exception
            strErrMessage = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try

        If Not String.IsNullOrEmpty(strErrMessage) Then
            ErrorLogInsert(101, "GetRoles", strErrMessage, "CommandType.text")
        End If

        Return list
    End Function

    Public Function BCCConfiguration_I(ByVal emaildoctype As String, ByVal usrid As Integer, ByVal modifieduser As String) As Boolean
        Dim isSucceed As Boolean = True
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("uspGeneral_BCCEmailConfiguration_I", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmEmailDocType As New SqlParameter("@emaildoctype", SqlDbType.VarChar, 300)
        Dim prmModifiedUser As New SqlParameter("@modifieduser", SqlDbType.VarChar, 300)
        Dim prmUsrID As New SqlParameter("@usrid", SqlDbType.Int)

        prmEmailDocType.Value = emaildoctype
        prmModifiedUser.Value = modifieduser
        prmUsrID.Value = usrid

        Command.Parameters.Add(prmEmailDocType)
        Command.Parameters.Add(prmModifiedUser)
        Command.Parameters.Add(prmUsrID)
        Try
            Connection.Open()
            Command.ExecuteNonQuery()
        Catch ex As Exception
            strErrMessage = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try
        If Not String.IsNullOrEmpty(strErrMessage) Then
            ErrorLogInsert(101, "BCCConfiguration_I", strErrMessage, "uspGeneral_BCCEmailConfiguration_I")
            isSucceed = False
        End If

        Return isSucceed
    End Function

    Public Function BCCConfiguration_D(ByVal sno As Integer) As Boolean
        Dim isSucceed As Boolean = True
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("delete CODEmailBCC_Configuration where emailconf_id=" & sno.ToString(), Connection)
        Command.CommandType = CommandType.Text

        Try
            Connection.Open()
            Command.ExecuteNonQuery()
        Catch ex As Exception
            strErrMessage = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try

        If Not String.IsNullOrEmpty(strErrMessage) Then
            ErrorLogInsert(101, "BCCConfiguration_D", strErrMessage, "CommandType.Text")
            isSucceed = False
        End If

        Return isSucceed
    End Function

    Public Function GetUserMappingBCCConfig(ByVal fullname As String, ByVal roleid As Integer, ByVal emaildoctype As String) As List(Of UserProfileInfo)
        Dim list As New List(Of UserProfileInfo)
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("uspGeneral_GetUserMappingBCCConfig", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmFullname As New SqlParameter("@fullname", SqlDbType.VarChar, 300)
        Dim prmEmailDocType As New SqlParameter("@emaildoctype", SqlDbType.VarChar, 300)
        Dim prmRoleId As New SqlParameter("@roleid", SqlDbType.Int)

        If String.IsNullOrEmpty(fullname) Then
            prmFullname.Value = Nothing
        Else
            prmFullname.Value = fullname
        End If
        prmRoleId.Value = roleid
        prmEmailDocType.Value = emaildoctype
        Command.Parameters.Add(prmFullname)
        Command.Parameters.Add(prmEmailDocType)
        Command.Parameters.Add(prmRoleId)

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read
                    Dim info As New UserProfileInfo
                    info.UserId = Integer.Parse(DataReader.Item("usr_id").ToString())
                    info.Fullname = Convert.ToString(DataReader.Item("name"))
                    info.Email = Convert.ToString(DataReader.Item("email"))
                    info.PhoneNo = Convert.ToString(DataReader.Item("phoneno"))
                    If Convert.ToString(DataReader.Item("UsrType").Equals("N")) Then
                        info.UserType = "NSN"
                    ElseIf Convert.ToString(DataReader.Item("usrType").Equals("S")) Then
                        info.UserType = "Subcon"
                    Else
                        info.UserType = "Telkomsel"
                    End If
                    info.RoleInf.RoleName = Convert.ToString(DataReader.Item("RoleDesc"))
                    list.Add(info)
                End While
            End If
        Catch ex As Exception
            strErrMessage = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try

        If Not String.IsNullOrEmpty(strErrMessage) Then
            ErrorLogInsert(102, "GetUserMappingBCCConfig", strErrMessage, "uspGeneral_GetUserMappingBCCConfig")
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
