Imports Microsoft.VisualBasic
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient

Public Class RoleController
    Inherits BaseController
    Public Function GetUserTypes() As List(Of RoleInfo)
        Dim list As New List(Of RoleInfo)
        Command = New SqlCommand("select * from TUserType where rstatus=2 order by lmdt desc", Connection)
        Command.CommandType = CommandType.Text

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read()
                    Dim info As New RoleInfo
                    info.UserType = Convert.ToString(DataReader.Item("GrpDesc"))
                    info.UserTypeId = Integer.Parse(DataReader.Item("GrpId").ToString())
                    list.Add(info)
                End While
            End If
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try

        Return list
    End Function

    Public Function GetRolesByUserType(ByVal grpid As Integer) As List(Of RoleInfo)
        Dim list As New List(Of RoleInfo)

        Command = New SqlCommand("select * from trole where rstatus = 2 and grpid =" & grpid & " and roleid not in(select roleid from CODGroupingRoleActivity)", Connection)
        Command.CommandType = CommandType.Text

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While (DataReader.Read())
                    Dim info As New RoleInfo
                    info.RoleId = Integer.Parse(DataReader.Item("RoleId").ToString())
                    info.RoleName = Convert.ToString(DataReader.Item("RoleDesc"))
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

        Return list
    End Function
End Class
