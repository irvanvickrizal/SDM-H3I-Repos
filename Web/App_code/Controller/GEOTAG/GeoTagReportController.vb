Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections.Generic

Public Class GeoTagReportController
    Inherits BaseController

    Public Function GetAllEngineersRegistered() As List(Of GeoTagReportInfo)
        Dim list As New List(Of GeoTagReportInfo)
        Command = New SqlCommand("uspGTT_GetEngineerRegistered", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read()
                    Dim info As New GeoTagReportInfo
                    info.PMInfo.Fullname = Convert.ToString(DataReader.Item("Fullname"))
                    info.PMInfo.PhoneNo = Convert.ToString(DataReader.Item("PhoneNo"))
                    info.PMInfo.Email = Convert.ToString(DataReader.Item("Email"))
                    info.PMInfo.SubconName = Convert.ToString(DataReader.Item("scon_Name"))
                    info.RegionalInfo.RgnName = Convert.ToString(DataReader.Item("Rgnname"))
                    info.EngineerInfo.Fullname = Convert.ToString(DataReader.Item("engineername"))
                    info.EngineerInfo.Email = Convert.ToString(DataReader.Item("engineeremail"))
                    info.EngineerInfo.PhoneNo = Convert.ToString(DataReader.Item("engineerphoneno"))
                    If Not String.IsNullOrEmpty(Convert.ToString(DataReader.Item("registrationdate"))) Then
                        info.LMDT = Convert.ToDateTime(DataReader.Item("registrationdate"))
                    End If
                    info.PhoneType = Convert.ToString(DataReader.Item("PhoneType"))
                    info.Compatible = Convert.ToString(DataReader.Item("Compatibility"))
                    info.TrialStatus = Convert.ToString(DataReader.Item("IsTrialAlready"))
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
