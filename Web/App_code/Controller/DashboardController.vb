Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data
Imports System.Collections.Generic

Public Class DashboardController
    Inherits BaseController

    Public Function GetDashboardMaster() As List(Of DashboardInfo)
        Dim list As New List(Of DashboardInfo)
        Command = New SqlCommand("uspDashboard_GetDashboardType", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read
                    Dim info As New DashboardInfo
                    info.MDashboardId = Integer.Parse(DataReader.Item("MDashboard_Id"))
                    info.IsDefault = Convert.ToBoolean(DataReader.Item("IsDefaultTaskAgenda"))
                    info.FormName = Convert.ToString(DataReader.Item("Form_Name"))
                    info.DashboardName = Convert.ToString(DataReader.Item("Dashboard_Name"))
                    info.DashboardDesc = Convert.ToString(DataReader.Item("Description"))
                    info.LMBY = Convert.ToString(DataReader.Item("LMBY"))
                    info.LMDT = Convert.ToDateTime(DataReader.Item("LMDT"))
                    list.Add(info)
                End While
            End If
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try

        Return list
    End Function

    Public Function GetDashboardConfigs() As DataTable
        Dim dtDashboardConfigs As New DataTable
        Command = New SqlCommand("uspDashboard_GetDashboardConfigs", Connection)
        Command.CommandType = CommandType.StoredProcedure
        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                dtDashboardConfigs.Load(DataReader)
            End If
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try
        Return dtDashboardConfigs
    End Function

    Public Function GetDashboardConfigByRole(ByVal roleid As Integer) As DataTable
        Dim dtDashboardConfigs As New DataTable
        Command = New SqlCommand("uspDashboard_GetDashboardConfigRole", Connection)
        Command.CommandType = CommandType.StoredProcedure
        Dim prmRoleId As New SqlParameter("@roleid", SqlDbType.Int)
        prmRoleId.Value = roleid
        Command.Parameters.Add(prmRoleId)
        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                dtDashboardConfigs.Load(DataReader)
            End If
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try
        Return dtDashboardConfigs
    End Function

    Public Function DashboardMaster_IU(ByVal info As DashboardInfo) As Boolean
        Dim isSucceed As Boolean = True
        Command = New SqlCommand("uspCOD_DashboardMaster_IU", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmDashboardId As New SqlParameter("@dashboardid", SqlDbType.Int)
        Dim prmDashboardName As New SqlParameter("@dashboardname", SqlDbType.VarChar, 50)
        Dim prmDescription As New SqlParameter("@description", SqlDbType.VarChar, 250)
        Dim prmIsDefaultTaskAgenda As New SqlParameter("@isdefaulttaskagenda", SqlDbType.Bit)
        Dim prmURLForm As New SqlParameter("@urlform", SqlDbType.VarChar, 1000)
        Dim prmLMBY As New SqlParameter("@lmby", SqlDbType.VarChar, 250)

        prmDashboardId.Value = info.MDashboardId
        prmDashboardName.Value = info.DashboardName
        prmDescription.Value = info.DashboardDesc
        prmIsDefaultTaskAgenda.Value = info.IsDefault
        prmURLForm.Value = info.FormName
        prmLMBY.Value = info.LMBY

        Command.Parameters.Add(prmDashboardId)
        Command.Parameters.Add(prmDashboardName)
        Command.Parameters.Add(prmDescription)
        Command.Parameters.Add(prmIsDefaultTaskAgenda)
        Command.Parameters.Add(prmURLForm)
        Command.Parameters.Add(prmLMBY)

        Try
            Connection.Open()
            Command.ExecuteNonQuery()
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try

        Return isSucceed
    End Function

    Public Sub DashboardMaster_Delete(ByVal dashboardid As Integer)
        Command = New SqlCommand("delete dashboardmaster where mdashboard_id=" & dashboardid, Connection)
        Command.CommandType = CommandType.Text

        Try
            Connection.Open()
            Command.ExecuteNonQuery()
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try


    End Sub


End Class
