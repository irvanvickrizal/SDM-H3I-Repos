Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data
Imports System.Collections.Generic
Public Class WFSiteBaseGRPController
    Inherits BaseController

    Public Function WorkflowSiteBaseGrouping_I(ByVal wfid As Integer, ByVal lmby As Integer) As Boolean
        Dim isSucceed As Boolean = True
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("uspSiteBase_WorkflowGrouping_I", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmWFID As New SqlParameter("@wfid", SqlDbType.Int)
        Dim prmLMBY As New SqlParameter("@lmby", SqlDbType.Int)

        prmWFID.Value = wfid
        prmLMBY.Value = lmby

        Command.Parameters.Add(prmWFID)
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
            ErrorLogInsert(18, "WorkflowSiteBaseGrouping_I", strErrMessage, "uspSiteBase_WorkflowGrouping_I")
        End If

        Return isSucceed
    End Function
    Public Function Timestamp_I(ByVal docid As Integer, ByVal lmby As Integer) As Boolean
        Dim isSucceed As Boolean = True
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("uspTimestamp_I", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmdocid As New SqlParameter("@doc_id", SqlDbType.Int)
        Dim prmLMBY As New SqlParameter("@lmby", SqlDbType.Int)

        prmdocid.Value = docid
        prmLMBY.Value = lmby

        Command.Parameters.Add(prmdocid)
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

        'If isSucceed = False Then
        '    ErrorLogInsert(18, "WorkflowSiteBaseGrouping_I", strErrMessage, "uspSiteBase_WorkflowGrouping_I")
        'End If

        Return isSucceed
    End Function
    Public Function TimestampDoc_I(ByVal docid As Integer, ByVal lmby As Integer) As Boolean
        Dim isSucceed As Boolean = True
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("uspTimestampDoc_I", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmDocid As New SqlParameter("@Doc_id", SqlDbType.Int)
        Dim prmLMBY As New SqlParameter("@lmby", SqlDbType.Int)

        prmDocid.Value = docid
        prmLMBY.Value = lmby

        Command.Parameters.Add(prmDocid)
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

        'If isSucceed = False Then
        '    ErrorLogInsert(18, "WorkflowSiteBaseGrouping_I", strErrMessage, "uspSiteBase_WorkflowGrouping_I")
        'End If

        Return isSucceed
    End Function

    Public Function GetWFSiteBaseGrouping() As List(Of WFSiteBaseInfo)
        Dim isSucceed As Boolean = True
        Dim strErrMessage As String = String.Empty
        Dim list As New List(Of WFSiteBaseInfo)
        Command = New SqlCommand("uspSiteBase_GetWFSiteBaseGrouping", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()

            If DataReader.HasRows Then
                While DataReader.Read
                    Dim info As New WFSiteBaseInfo
                    info.SNO = Integer.Parse(DataReader.Item("SNO").ToString())
                    info.WFID = Integer.Parse(DataReader.Item("WFID").ToString())
                    info.LMBY = Integer.Parse(DataReader.Item("lmby").ToString())
                    info.LMDT = Convert.ToDateTime(DataReader.Item("lmdt"))
                    info.WFName = Convert.ToString(DataReader.Item("WFDesc"))
                    info.ModifiedUser = Convert.ToString(DataReader.Item("ModifiedUser"))
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
            ErrorLogInsert(18, "GetWFSiteBaseGrouping", strErrMessage, "uspSiteBase_GetWFSiteBaseGrouping")
        End If

        Return list
    End Function
    Public Function GetTimestampDoc_view() As List(Of Timestampapprovaldocinfo)
        Dim isSucceed As Boolean = True
        Dim strErrMessage As String = String.Empty
        Dim list As New List(Of Timestampapprovaldocinfo)
        Command = New SqlCommand("uspTimestampDoc_view", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()

            If DataReader.HasRows Then
                While DataReader.Read
                    Dim info As New Timestampapprovaldocinfo
                    info.TimestampDocID = Integer.Parse(DataReader.Item("TimestampDoc_ID").ToString())
                    info.docid = Integer.Parse(DataReader.Item("doc_id").ToString())
                    info.LMBY = Integer.Parse(DataReader.Item("lmby").ToString())
                    info.LMDT = Convert.ToDateTime(DataReader.Item("lmdt"))
                    info.docname = Convert.ToString(DataReader.Item("docname"))
                    info.ModifiedUser = Convert.ToString(DataReader.Item("ModifiedUser"))
                    list.Add(info)
                End While
            End If
        Catch ex As Exception
            isSucceed = False
            strErrMessage = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try

        'If isSucceed = False Then
        '    ErrorLogInsert(18, "GetWFSiteBaseGrouping", strErrMessage, "uspSiteBase_GetWFSiteBaseGrouping")
        'End If

        Return list
    End Function
    Public Function WFSiteBase_D(ByVal sno As Integer) As Boolean
        Dim isSucceed As Boolean = True
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("delete Workflow_SiteBase_Grouping where sno =" & sno, Connection)
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
            ErrorLogInsert(18, "WFSiteBase_D", strErrMessage, "CommandType.Text")
        End If

        Return isSucceed
    End Function
    Public Function TimestampDoc_D(ByVal TimestampDocID As Integer) As Boolean
        Dim isSucceed As Boolean = True
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("delete TimestampDoc where timestampdoc_id =" & TimestampDocID, Connection)
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

        'If isSucceed = False Then
        '    ErrorLogInsert(18, "WFSiteBase_D", strErrMessage, "CommandType.Text")
        'End If

        Return isSucceed
    End Function

    Public Function GetWorkflowNotInSiteBaseGrouping() As List(Of WCCFlowGroupingInfo)
        Dim isSucceed As Boolean = True
        Dim strErrMessage As String = String.Empty
        Dim list As New List(Of WCCFlowGroupingInfo)
        Command = New SqlCommand("select * from tworkflow where wfid not in(select wfid from Workflow_SiteBase_Grouping) and rstatus = 2 order by lmdt desc", Connection)
        Command.CommandType = CommandType.Text

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read
                    Dim info As New WCCFlowGroupingInfo
                    info.WFID = Integer.Parse(DataReader.Item("wfid").ToString())
                    info.FlowName = Convert.ToString(DataReader.Item("WFDesc"))
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
            ErrorLogInsert(18, "GetWorkflowNotInSiteBaseGrouping", strErrMessage, "CommandType.Text")
        End If

        Return list
    End Function
    Public Function GetTimestampDoc() As List(Of Timestampinfo)
        Dim isSucceed As Boolean = True
        Dim strErrMessage As String = String.Empty
        Dim list As New List(Of Timestampinfo)
        Command = New SqlCommand("select * from coddoc cd inner join CODWFDoc cw on cd.doc_id = cw.doc_id where cw.doc_id not in (select doc_id from TimestampDoc innerjoin) and cd.doctype = 'D'", Connection)
        Command.CommandType = CommandType.Text

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read
                    Dim info As New Timestampinfo
                    info.docid = Integer.Parse(DataReader.Item("doc_id").ToString())
                    info.TDocName = Convert.ToString(DataReader.Item("docname"))
                    list.Add(info)
                End While
            End If
        Catch ex As Exception
            isSucceed = False
            strErrMessage = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try

        'If isSucceed = False Then
        '    ErrorLogInsert(18, "GetWorkflowNotInSiteBaseGrouping", strErrMessage, "CommandType.Text")
        'End If

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
