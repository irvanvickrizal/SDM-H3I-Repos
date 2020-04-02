Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections.Generic
Imports Microsoft.VisualBasic

Public Class RegistrationController
    Inherits BaseController

#Region "Online registration"

    Public Function CreateUserLogin(ByVal info As UserProfileInfo) As Integer
        Dim newuserid As Integer = 0
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("uspGTT_CreateUserLogin", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmFullname As New SqlParameter("@fullname", SqlDbType.VarChar, 300)
        Dim prmSCONID As New SqlParameter("@sconid", SqlDbType.Int)
        Dim prmEmail As New SqlParameter("@email", SqlDbType.VarChar, 2000)
        Dim prmUsrLogin As New SqlParameter("@usrlogin", SqlDbType.VarChar, 300)
        Dim prmUsrPassword As New SqlParameter("@usrPassword", SqlDbType.VarChar, 400)
        Dim prmLMBY As New SqlParameter("@lmby", SqlDbType.VarChar, 300)
        Dim prmUserId As New SqlParameter("@usrid", SqlDbType.Int)
        Dim prmGetUserId As New SqlParameter("@getusrid", SqlDbType.Int)
        Dim prmPhoneNo As New SqlParameter("@phoneno", SqlDbType.VarChar, 50)

        prmFullname.Value = info.Fullname
        prmSCONID.Value = info.SubconId
        prmEmail.Value = info.Email
        prmUsrLogin.Value = info.UserLogin
        prmUsrPassword.Value = FormsAuthentication.HashPasswordForStoringInConfigFile(info.UsrPassword & "NSN", "MD5")
        prmLMBY.Value = info.CMAInfo.ModifiedUser
        prmUserId.Value = info.UserId
        prmGetUserId.Direction = ParameterDirection.Output
        prmPhoneNo.Value = info.PhoneNo

        Command.Parameters.Add(prmFullname)
        Command.Parameters.Add(prmSCONID)
        Command.Parameters.Add(prmEmail)
        Command.Parameters.Add(prmUsrLogin)
        Command.Parameters.Add(prmUsrPassword)
        Command.Parameters.Add(prmLMBY)
        Command.Parameters.Add(prmUserId)
        Command.Parameters.Add(prmGetUserId)
        Command.Parameters.Add(prmPhoneNo)

        Try
            Connection.Open()
            Command.ExecuteNonQuery()
            newuserid = Integer.Parse(prmGetUserId.Value.ToString())
        Catch ex As Exception
            strErrMessage = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try

        If Not String.IsNullOrEmpty(strErrMessage) Then
            ErrLog_I("CreateUserLogin", strErrMessage, "uspGTT_CreateUserLogin")
        End If

        Return newuserid
    End Function

    Public Sub DeleteUserLogin(ByVal userid As String)
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("delete GTT_user where userid = " & userid, Connection)
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
            ErrLog_I("DeleteUserLogin", strErrMessage, "CommandType.Text")
        End If
    End Sub

    Public Function UserRegionalGroup_IU(ByVal info As RegionalUserGroupInfo) As Boolean
        Dim isSucceed As Boolean = True
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("uspGTT_RegionalUserGroup_IU", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmGRPID As New SqlParameter("@grpid", SqlDbType.Int)
        Dim prmRgnID As New SqlParameter("@rgnid", SqlDbType.Int)
        Dim prmUsrID As New SqlParameter("@usrid", SqlDbType.Int)
        Dim prmLMBY As New SqlParameter("@lmby", SqlDbType.VarChar, 300)

        prmGRPID.Value = info.GrpId
        prmRgnID.Value = info.RgnInfo.RgnId
        prmUsrID.Value = info.UsrInfo.UserId
        prmLMBY.Value = info.ModifiedUser

        Command.Parameters.Add(prmGRPID)
        Command.Parameters.Add(prmRgnID)
        Command.Parameters.Add(prmUsrID)
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

        If Not String.IsNullOrEmpty(strErrMessage) Then
            ErrLog_I("UserRegionalGroup_IU", strErrMessage, "uspGTT_RegionalUserGroup_IU")
        End If

        Return isSucceed
    End Function

    Public Sub DeleteRegionalGroup(ByVal userid As String)
        Dim strErrMessagea As String = String.Empty
        Command = New SqlCommand("delete GTT_RegionalUserGroup where userid = " & userid, Connection)
        Command.CommandType = CommandType.Text

        Try
            Connection.Open()
            Command.ExecuteNonQuery()
        Catch ex As Exception
            strErrMessagea = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try

        If Not String.IsNullOrEmpty(strErrMessagea) Then
            ErrLog_I("DeleteRegionalGroup", strErrMessagea, "CommandType.text")
        End If
    End Sub

    Public Function Checking_UserLoginIsExist(ByVal usrLogin As String) As Boolean
        Dim isExist As Boolean = False
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("uspGTT_UserLoginIsExisting", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmUsrLogin As New SqlParameter("@usrlogin", SqlDbType.VarChar, 300)
        Dim prmIsExist As New SqlParameter("@isexist", SqlDbType.Bit)

        prmUsrLogin.Value = usrLogin
        prmIsExist.Direction = ParameterDirection.Output

        Command.Parameters.Add(prmUsrLogin)
        Command.Parameters.Add(prmIsExist)

        Try
            Connection.Open()
            Command.ExecuteNonQuery()
            isExist = Convert.ToBoolean(prmIsExist.Value)
        Catch ex As Exception
            strErrMessage = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try

        If Not String.IsNullOrEmpty(strErrMessage) Then
            ErrLog_I("Checking_UserLoginIsExist", strErrMessage, "uspGTT_UserLoginIsExisting")
        End If

        Return isExist
    End Function

    Public Function GetAccountUsers() As List(Of UserProfileInfo)
        Dim list As New List(Of UserProfileInfo)
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("uspGTT_GetAllLoginAccounts", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read
                    Dim info As New UserProfileInfo
                    info.UserId = Integer.Parse(DataReader.Item("UserID").ToString())
                    info.Fullname = Convert.ToString(DataReader.Item("Fullname"))
                    info.PhoneNo = Convert.ToString(DataReader.Item("PhoneNo"))
                    info.UserLogin = Convert.ToString(DataReader.Item("USR_Login"))
                    info.SubconId = Integer.Parse(DataReader.Item("scon_id").ToString())
                    info.Email = Convert.ToString(DataReader.Item("Email"))
                    info.CMAInfo.ModifiedUser = Convert.ToString(DataReader.Item("lmby"))
                    info.CMAInfo.LMDT = Convert.ToDateTime(DataReader.Item("LMDT"))
                    list.Add(info)
                End While
            End If
        Catch ex As Exception
            strErrMessage = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try

        If Not String.IsNullOrEmpty(strErrMessage) Then
            ErrLog_I("GetAccountUsers", strErrMessage, "uspGTT_GetAllLoginAccounts")
        End If

        Return list
    End Function

    Public Function GetPMAccountDetail(ByVal userid As Integer) As UserProfileInfo
        Dim info As New UserProfileInfo
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("uspGTT_GetUserAccount", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmUserID As New SqlParameter("@userid", SqlDbType.Int)
        prmUserID.Value = userid
        Command.Parameters.Add(prmUserID)

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read
                    info.UserId = Integer.Parse(DataReader.Item("UserID").ToString())
                    info.Fullname = Convert.ToString(DataReader.Item("Fullname"))
                    info.PhoneNo = Convert.ToString(DataReader.Item("PhoneNo"))
                    info.UserLogin = Convert.ToString(DataReader.Item("USR_Login"))
                    info.SubconId = Integer.Parse(DataReader.Item("scon_id").ToString())
                    info.Email = Convert.ToString(DataReader.Item("Email"))
                    info.CMAInfo.ModifiedUser = Convert.ToString(DataReader.Item("lmby"))
                    info.CMAInfo.LMDT = Convert.ToDateTime(DataReader.Item("LMDT"))
                End While
            Else
                info = Nothing
            End If
        Catch ex As Exception
            info = Nothing
            strErrMessage = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try

        If Not String.IsNullOrEmpty(strErrMessage) Then
            ErrLog_I("GetPMAccountDetail", strErrMessage, "uspGTT_GetUserAccount")
        End If

        Return info
    End Function

    Public Function PMAccount_U(ByVal info As UserProfileInfo) As Boolean
        Dim isSucceed As Boolean = True
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("uspGTT_UserAccount_U", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmUserID As New SqlParameter("@userid", SqlDbType.Int)
        Dim prmFullname As New SqlParameter("@fullname", SqlDbType.VarChar, 350)
        Dim prmEmail As New SqlParameter("@email", SqlDbType.VarChar, 300)
        Dim prmPhoneNo As New SqlParameter("@phoneno", SqlDbType.VarChar, 100)
        Dim prmLMBY As New SqlParameter("@lmby", SqlDbType.VarChar, 300)
        Dim prmSconId As New SqlParameter("@sconid", SqlDbType.Int)

        prmUserID.Value = info.UserId
        prmFullname.Value = info.Fullname
        prmPhoneNo.Value = info.PhoneNo
        prmLMBY.Value = info.LMBY
        prmSconId.Value = info.SubconId

        Command.Parameters.Add(prmUserID)
        Command.Parameters.Add(prmFullname)
        Command.Parameters.Add(prmEmail)
        Command.Parameters.Add(prmPhoneNo)
        Command.Parameters.Add(prmLMBY)
        Command.Parameters.Add(prmSconId)

        Try
            Connection.Open()
            Command.ExecuteNonQuery()
        Catch ex As Exception
            isSucceed = False
            strErrMessage = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try

        If Not String.IsNullOrEmpty(strErrMessage) Then
            ErrLog_I("PMAccount_U", strErrMessage, "uspGTT_UserAccount_U")
        End If

        Return isSucceed
    End Function

    Public Function PMChangePassword(ByVal userid As Integer, ByVal newpassword As String) As Boolean
        Dim isSucceed As Boolean = True
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("uspGTT_ChangePassword", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmUserID As New SqlParameter("@userid", SqlDbType.Int)
        Dim prmUsrPassword As New SqlParameter("@usrpassword", SqlDbType.VarChar, 500)

        prmUserID.Value = userid
        prmUsrPassword.Value = FormsAuthentication.HashPasswordForStoringInConfigFile(newpassword & "NSN", "MD5")

        Command.Parameters.Add(prmUserID)
        Command.Parameters.Add(prmUsrPassword)

        Try
            Connection.Open()
        Catch ex As Exception
            isSucceed = False
            strErrMessage = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try

        If Not String.IsNullOrEmpty(strErrMessage) Then
            ErrLog_I("PMChangePassword", strErrMessage, "uspGTT_ChangePassword")
        End If

        Return isSucceed
    End Function

#End Region

#Region "Others Data Required"
    Public Function GetSubcons() As List(Of SubconInfo)
        Dim list As New List(Of SubconInfo)
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("select * from subcon where rstatus = 2", Connection)
        Command.CommandType = CommandType.Text

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read
                    Dim info As New SubconInfo
                    info.SubconId = Integer.Parse(DataReader.Item("scon_id").ToString())
                    info.SubconName = Convert.ToString(DataReader.Item("scon_name"))
                    list.Add(info)
                End While
            End If
        Catch ex As Exception
            strErrMessage = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try

        If Not String.IsNullOrEmpty(strErrMessage) Then
            ErrLog_I("GetSubcons", strErrMessage, "CommandType.Text")
        End If

        Return list
    End Function

    Public Function GetRegions() As List(Of RegionInfo)
        Dim list As New List(Of RegionInfo)
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("select * from codregion where rstatus = 2 order by Rgn_id asc", Connection)
        Command.CommandType = CommandType.Text

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read
                    Dim info As New RegionInfo
                    info.RgnId = Integer.Parse(DataReader.Item("rgn_id").ToString())
                    info.RgnName = Convert.ToString(DataReader.Item("RgnName"))
                    info.RgnCode = Convert.ToString(DataReader.Item("RgnCode"))
                    list.Add(info)
                End While
            End If
        Catch ex As Exception
            strErrMessage = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try

        If Not String.IsNullOrEmpty(strErrMessage) Then
            ErrLog_I("GetRegions", strErrMessage, "CommandType.Text")
        End If


        Return list
    End Function
#End Region

#Region "Error Log"
    Public Sub ErrLog_I(ByVal errfunction As String, ByVal errdesc As String, ByVal errsp As String)
        Command = New SqlCommand("uspGTT_ErrLog_I", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmErrFunction As New SqlParameter("@errfunction", SqlDbType.VarChar, 400)
        Dim prmErrDesc As New SqlParameter("@errdesc", SqlDbType.VarChar, 500)
        Dim prmErrSP As New SqlParameter("@errsp", SqlDbType.VarChar, 400)

        prmErrDesc.Value = errdesc
        prmErrSP.Value = errsp
        prmErrFunction.Value = errfunction

        Command.Parameters.Add(prmErrFunction)
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
