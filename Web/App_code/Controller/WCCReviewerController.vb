Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections.Generic


Public Class WCCReviewerController
    Inherits BaseController

    Public Function WCCReviewer_I(ByVal info As WCCReviewerInfo) As Boolean
        Dim isValid As Boolean = True

        Command = New SqlCommand("uspWCC_RegisterReviewer_I", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmUserid As New SqlParameter("@usrid", SqlDbType.Int)
        Dim prmLMBY As New SqlParameter("@lmby", SqlDbType.Int)

        prmUserid.Value = info.UserId
        prmLMBY.Value = info.LMBY

        Command.Parameters.Add(prmUserid)
        Command.Parameters.Add(prmLMBY)

        Try
            Connection.Open()
            Command.ExecuteNonQuery()
        Catch ex As Exception
            isValid = False
        Finally
            Connection.Close()
        End Try
        Return isValid
    End Function

    Public Sub WCCReviewer_D(ByVal reguserid As Integer)
        Command = New SqlCommand("delete WCC_UserReviewer where UserReg_id=" & reguserid, Connection)
        Command.CommandType = CommandType.Text

        Try
            Connection.Open()
            Command.ExecuteNonQuery()
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try
    End Sub

    Public Function GetWCCReviewer(ByVal roleid As Integer) As List(Of WCCReviewerInfo)
        Dim list As New List(Of WCCReviewerInfo)
        Command = New SqlCommand("uspWCC_getWCCReviewers", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmRoleId As New SqlParameter("@roleid", SqlDbType.Int)
        prmRoleId.Value = roleid
        Command.Parameters.Add(prmRoleId)

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read
                    Dim info As New WCCReviewerInfo
                    info.UserId = Integer.Parse(DataReader.Item("usr_id").ToString())
                    info.Fullname = Convert.ToString(DataReader.Item("name"))
                    info.ModifiedUser = Convert.ToString(DataReader.Item("ModifiedUser"))
                    info.RoleName = Convert.ToString(DataReader.Item("RoleDesc"))
                    info.CDT = Convert.ToDateTime(DataReader.Item("CDT"))
                    info.RegisterUserId = Integer.Parse(DataReader.Item("UserReg_Id").ToString())
                    list.Add(info)
                End While
            End If
        Catch ex As Exception

        Finally
            Connection.Close()
        End Try

        Return list
    End Function

    Public Function GetUserByRoleId(ByVal roleid As Integer) As List(Of UserProfile)
        Dim list As New List(Of UserProfile)
        Command = New SqlCommand("select * from ebastusers_1 where usrRole =" & roleid & " and acc_status = 'A' " & _
                                    " and usr_id not in(select usr_id from WCCUserReviewer)", Connection)

        Command.CommandType = CommandType.Text

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read
                    Dim info As New UserProfile
                    info.UserId = Integer.Parse(DataReader.Item("usr_id").ToString())
                    info.Fullname = Convert.ToString(DataReader.Item("name"))
                    info.SignTitle = Convert.ToString(DataReader.Item("SignTitle"))
                    list.Add(info)
                End While
            End If
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try

        Return list
    End Function

    Public Function GetRoleInWCCGrouping() As List(Of RoleInfo)
        Dim list As New List(Of RoleInfo)
        Command = New SqlCommand("uspWCC_GetRoleWCCByWCCFlowGrouping", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read
                    Dim info As New RoleInfo
                    info.RoleId = Integer.Parse(DataReader.Item("RoleId").ToString())
                    info.RoleName = Convert.ToString(DataReader.Item("RoleDesc")) & "-" & Convert.ToString(DataReader.Item("LvlCode"))

                    list.Add(info)
                End While
            End If
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try

        Return list
    End Function

    Public Function GetAreaRegion(ByVal lvlcode As String) As List(Of RoleInfo)
        Dim list As New List(Of RoleInfo)
        If lvlcode.Equals("R") Then
            Command = New SqlCommand("select * from CodRegion where RStatus =2", Connection)
        ElseIf lvlcode.Equals("A") Then
            Command = New SqlCommand("select * from CodArea where RStatus =2", Connection)
        End If
        Command.CommandType = CommandType.Text

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read()
                    Dim info As New RoleInfo
                    If lvlcode.Equals("R") Then
                        info.RoleId = Integer.Parse(DataReader.Item("Rgn_Id").ToString())
                        info.RoleName = Convert.ToString(DataReader.Item("RgnName"))
                    ElseIf lvlcode.Equals("A") Then
                        info.RoleId = Integer.Parse(DataReader.Item("ARA_Id").ToString())
                        info.RoleName = Convert.ToString(DataReader.Item("ARA_Name"))
                    End If
                    list.Add(info)
                End While
            End If
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try
        Return list
    End Function

    Public Function GetUserByRole(ByVal roleid As Integer, ByVal areacode As Integer, ByVal regioncode As Integer) As List(Of UserProfile)
        Dim list As New List(Of UserProfile)
        If areacode = 0 And regioncode = 0 Then
            Command = New SqlCommand("select distinct(usr.usr_id),usr.name from ebastusers_1 usr inner join ebastuserrole rol on usr.usr_id = rol.usr_id where usr.rstatus = 2 and usr.usrRole =" & roleid & " and usr.acc_status = 'A' and usr.usr_id not in(select usr_id from WCC_UserReviewer)", Connection)
        Else
            If areacode > 0 Then
                Command = New SqlCommand("select distinct(usr.usr_id),usr.name from ebastusers_1 usr inner join ebastuserrole rol on usr.usr_id = rol.usr_id where usr.rstatus = 2 and usr.usrRole =" & roleid & " and usr.acc_status = 'A' and usr.usr_id not in(select usr_id from WCC_UserReviewer) and ara_id=" & areacode, Connection)
            End If
            If regioncode > 0 Then
                Command = New SqlCommand("select distinct(usr.usr_id),usr.name from ebastusers_1 usr inner join ebastuserrole rol on usr.usr_id = rol.usr_id where usr.rstatus = 2 and usr.usrRole =" & roleid & " and usr.acc_status = 'A' and usr.usr_id not in(select usr_id from WCC_UserReviewer) and rgn_id=" & regioncode, Connection)
            End If
        End If
        Command.CommandType = CommandType.Text

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read
                    Dim info As New UserProfile
                    info.UserId = Integer.Parse(DataReader.Item("usr_id").ToString())
                    info.Fullname = Convert.ToString(DataReader.Item("name"))
                    list.Add(info)
                End While
            End If
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try

        Return list
    End Function

End Class
