Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections.Generic

Public Class WFController
    Inherits BaseController

    Public Function Workflow_IU(ByVal info As WorkflowInfo) As Integer
        Dim wfid As Integer = 0
        Command = New SqlCommand("uspTWorkFlowIU", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmWFID As New SqlParameter("@wfid", SqlDbType.BigInt)
        Dim prmWFCode As New SqlParameter("@WFCode", SqlDbType.VarChar, 10)
        Dim prmWFDesc As New SqlParameter("@wfdesc", SqlDbType.VarChar, 15)
        Dim prmKPITime As New SqlParameter("@KPITime", SqlDbType.Int)
        Dim prmDSID As New SqlParameter("@ds_id", SqlDbType.Int)
        Dim prmRstatus As New SqlParameter("@rstatus", SqlDbType.Int)
        Dim prmLMBY As New SqlParameter("@lmby", SqlDbType.VarChar, 200)

        prmWFID.Value = info.WFID
        prmWFDesc.Value = info.WFName
        prmWFCode.Value = info.WFCode
        prmKPITime.Value = info.SLATotal
        prmDSID.Value = info.DscopeInfo.ScopeId
        prmRstatus.Value = info.RStatus
        prmLMBY.Value = info.CMAInfo.ModifiedUser

        Command.Parameters.Add(prmWFID)
        Command.Parameters.Add(prmWFCode)
        Command.Parameters.Add(prmWFDesc)
        Command.Parameters.Add(prmKPITime)
        Command.Parameters.Add(prmDSID)
        Command.Parameters.Add(prmRstatus)
        Command.Parameters.Add(prmLMBY)

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read
                    wfid = Integer.Parse(DataReader.Item("status").ToString())
                End While
            End If
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try

        Return wfid
    End Function

    Public Function Workflow_D(ByVal wfid As Integer, ByVal fieldname As String, ByVal fieldvalue As String, ByVal sort As String) As WorkflowInfo
        Dim info As New WorkflowInfo
        Command = New SqlCommand("uspTWorkFlowLD", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmWFID As New SqlParameter("@wfid", SqlDbType.Int)
        Dim prmFieldName As New SqlParameter("@fieldname", SqlDbType.VarChar, 15)
        Dim prmFieldValue As New SqlParameter("@fieldvalue", SqlDbType.VarChar, 20)
        Dim prmSort As New SqlParameter("@strsort", SqlDbType.VarChar, 20)

        prmWFID.Value = wfid
        prmFieldName.Value = fieldname
        prmFieldValue.Value = fieldvalue
        prmSort.Value = sort

        Command.Parameters.Add(prmWFID)
        Command.Parameters.Add(prmFieldName)
        Command.Parameters.Add(prmFieldValue)
        Command.Parameters.Add(prmSort)

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read
                    info.WFID = Integer.Parse(DataReader.Item("WFID").ToString())
                    info.WFName = Convert.ToString(DataReader.Item("WFDesc"))
                    info.WFCode = Convert.ToString(DataReader.Item("WFCode"))
                    info.SLATotal = Integer.Parse(DataReader.Item("KPITime").ToString())
                End While
            Else
                info = Nothing
            End If
        Catch ex As Exception
            info = Nothing
        Finally
            Connection.Close()
        End Try

        Return info
    End Function

End Class
