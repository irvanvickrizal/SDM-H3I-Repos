Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections.Generic


Public Class DelegationController
    Inherits BaseController

    Public Sub ErrLog_I(ByVal errcode As Integer, ByVal erroperation As String, ByVal errdesc As String, ByVal errsp As String)
        Command = New SqlCommand("uspErrLog", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmErrCode As New SqlParameter("@errcode", SqlDbType.Int)
        Dim prmErrOperation As New SqlParameter("@erroperation", SqlDbType.VarChar, 250)
        Dim prmErrDesc As New SqlParameter("@errDesc", SqlDbType.VarChar, 1000)
        Dim prmErrSP As New SqlParameter("@errSP", SqlDbType.VarChar, 50)

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

    Public Function Delegation_IU(ByVal info As DelegationInfo) As Boolean
        Dim isSucceed As Boolean = True
        Dim strError As String = String.Empty
        Command = New SqlCommand("uspCOD_Delegation_IU", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmUserId As New SqlParameter("@userid", SqlDbType.Int)
        Dim prmUserDelegationId As New SqlParameter("@userdelegationid", SqlDbType.Int)
        Dim prmStatus As New SqlParameter("@status", SqlDbType.VarChar, 50)
        Dim prmLMBY As New SqlParameter("@lmby", SqlDbType.Int)

        prmUserId.Value = info.UserId
        prmUserDelegationId.Value = info.UserDelegationId
        prmStatus.Value = info.Status
        prmLMBY.Value = info.LMBY

        Command.Parameters.Add(prmUserId)
        Command.Parameters.Add(prmUserDelegationId)
        Command.Parameters.Add(prmStatus)
        Command.Parameters.Add(prmLMBY)

        Try
            Connection.Open()
            Command.ExecuteNonQuery()
        Catch ex As Exception
            Return isSucceed = False
            strError = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try
        If isSucceed = False Then
            ErrLog_I(0, "Delegation_IU", strError, "uspCOD_Delegation_IU")
        End If

        Return isSucceed
    End Function

    Public Function GetSupervisorUser(ByVal userid As Integer) As List(Of UserProfile)
        Dim list As New List(Of UserProfile)
        Dim isSucceed As Boolean = True
        Dim strError As String = String.Empty
        Command = New SqlCommand("uspGetSupervisorUsers", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmUserId As New SqlParameter("@userid", SqlDbType.Int)
        prmUserId.Value = userid
        Command.Parameters.Add(prmUserId)

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read()
                    Dim info As New UserProfile
                    info.Username = Convert.ToString(DataReader.Item("name"))
                    info.RoleId = Integer.Parse(DataReader.Item("usrRole").ToString())
                    info.SignTitle = Convert.ToString(DataReader.Item("SignTitle"))
                    info.UserId = Integer.Parse(DataReader.Item("usr_id").ToString())
                    list.Add(info)
                End While
            End If
        Catch ex As Exception
            isSucceed = False
            strError = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try

        If isSucceed = False Then
            ErrLog_I(0, "GetSupervisorUser", strError, "uspGetSupervisorUsers")
        End If

        Return list
    End Function

    Public Function GetDelegationUser(ByVal userid As Integer) As List(Of DelegationInfo)
        Dim list As New List(Of DelegationInfo)
        Dim isSucceed As Boolean = True
        Dim strError As String = String.Empty
        Command = New SqlCommand("uspCOD_GetDelegation", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmUserId As New SqlParameter("@userid", SqlDbType.Int)
        prmUserId.Value = userid
        Command.Parameters.Add(prmUserId)

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read
                    Dim info As New DelegationInfo
                    info.DelegationId = Convert.ToInt32(DataReader.Item("Delegation_Id"))
                    info.UserId = Integer.Parse(DataReader.Item("userid").ToString())
                    info.UserDelegationId = Integer.Parse(DataReader.Item("userdelegationid").ToString())
                    info.UserDelegationName = Convert.ToString(DataReader.Item("userdelegationame"))
                    info.Status = Convert.ToString(DataReader.Item("Status"))
                    info.LMBY = Integer.Parse(DataReader.Item("lmby").ToString())
                    info.CDT = Convert.ToDateTime(DataReader.Item("CDT"))
                    If (Not info.Status.ToLower().Equals("active")) Then
                        info.LMDT = Convert.ToDateTime(DataReader.Item("LMDT"))
                    End If
                    list.Add(info)
                End While
            End If
        Catch ex As Exception
            isSucceed = False
            strError = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try

        If isSucceed = False Then
            ErrLog_I(0, "GetDelegationUser", strError, "uspCOD_GetDelegation")
        End If
        Return list
    End Function

    Public Function GetDelegationFrom(ByVal userid As Integer, ByVal status As String) As List(Of DelegationInfo)
        Dim list As New List(Of DelegationInfo)
        Dim isSucceed As Boolean = True
        Dim strError As String = String.Empty
        Command = New SqlCommand("uspCOD_GetDelegationFrom", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmUserId As New SqlParameter("@userid", SqlDbType.Int)
        Dim prmStatus As New SqlParameter("@status", SqlDbType.VarChar, 50)
        prmUserId.Value = userid
        prmStatus.Value = status
        Command.Parameters.Add(prmUserId)
        Command.Parameters.Add(prmStatus)

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read
                    Dim info As New DelegationInfo
                    info.DelegationId = Convert.ToInt32(DataReader.Item("Delegation_Id"))
                    info.UserId = Integer.Parse(DataReader.Item("userid").ToString())
                    info.UserDelegationId = Integer.Parse(DataReader.Item("userdelegationid").ToString())
                    info.UserDelegationName = Convert.ToString(DataReader.Item("userdelegationame"))
                    info.Status = Convert.ToString(DataReader.Item("Status"))
                    info.LMBY = Integer.Parse(DataReader.Item("lmby").ToString())
                    info.CDT = Convert.ToDateTime(DataReader.Item("CDT"))
                    If (Not info.Status.ToLower().Equals("active")) Then
                        info.LMDT = Convert.ToDateTime(DataReader.Item("LMDT"))
                    End If
                    list.Add(info)
                End While
            End If
        Catch ex As Exception
            isSucceed = False
            strError = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try

        If isSucceed = False Then
            ErrLog_I(0, "GetDelegationFrom", strError, "uspCOD_GetDelegationFrom")
        End If
        Return list
    End Function

    Public Function IsInDelegation(ByVal userid As Integer) As Boolean
        Dim rowcount As Integer = 0
        Dim isValid As Boolean = False

        Command = New SqlCommand("select count(*) from COD_Delegation where userid=" & userid & " and status='active'", Connection)
        Command.CommandType = CommandType.Text

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If (DataReader.HasRows) Then
                While DataReader.Read()
                    rowcount = Integer.Parse(DataReader.Item(0).ToString())
                End While
            End If
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try

        If rowcount > 0 Then
            isValid = True
        End If
        Return isValid
    End Function

    Public Function GetUserDelegation(ByVal userid As Integer) As UserProfile
        Dim info As New UserProfile

        Command = New SqlCommand("select * from ebastusers_1 where usr_id=" & userid, Connection)
        Command.CommandType = CommandType.Text

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read()
                    info.UserId = Integer.Parse(DataReader.Item("usr_id").ToString())
                    info.Fullname = Convert.ToString(DataReader.Item("name"))
                    info.Username = Convert.ToString(DataReader.Item("usrLogin"))
                    info.RoleId = Integer.Parse(DataReader.Item("usrRole"))
                    info.Email = Convert.ToString(DataReader.Item("email"))
                    info.SignTitle = Convert.ToString(DataReader.Item("SignTitle"))
                End While
            End If
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try


        Return info
    End Function

End Class
