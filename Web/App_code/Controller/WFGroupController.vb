Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections.Generic
Public Class WFGroupController
    Inherits BaseController

    Public Function GetWFGroups() As List(Of WorkflowGroupInfo)
        Dim list As New List(Of WorkflowGroupInfo)
        Dim strErrMessage As String = String.Empty

        Command = New SqlCommand("uspGeneral_GetWFGroups", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read
                    Dim info As New WorkflowGroupInfo
                    info.WFGRPID = Integer.Parse(DataReader.Item("WFGRP_ID").ToString())
                    info.WFID = Integer.Parse(DataReader.Item("WFID").ToString())
                    info.WFName = Convert.ToString(DataReader.Item("WFDESC"))
                    info.ModifiedUser = Convert.ToString(DataReader.Item("LMBY"))
                    info.LMDT = Convert.ToDateTime(DataReader.Item("LMDT"))
                    list.Add(info)
                End While
            End If
        Catch ex As Exception
            strErrMessage = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try

        If Not String.IsNullOrEmpty(strErrMessage) Then
            ErrorLogInsert(3, "GetWFGroups", strErrMessage, "uspGeneral_GetWFGroups")
        End If

        Return list
    End Function

    Public Function GetWFGroupsBaseOnFormType(ByVal formtype As String) As List(Of WorkflowGroupInfo)
        Dim list As New List(Of WorkflowGroupInfo)
        Dim strErrMessage As String = String.Empty

        Command = New SqlCommand("uspGeneral_GetWFGroups_BaseOnFormType", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmFormType As New SqlParameter("@formtype", SqlDbType.VarChar, 50)
        prmFormType.Value = formtype
        Command.Parameters.Add(prmFormType)

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read
                    Dim info As New WorkflowGroupInfo
                    info.WFGRPID = Integer.Parse(DataReader.Item("WFGRP_ID").ToString())
                    info.FormType = Convert.ToString(DataReader.Item("FORM_TYPE"))
                    info.WFID = Integer.Parse(DataReader.Item("WFID").ToString())
                    info.WFName = Convert.ToString(DataReader.Item("WFDESC"))
                    info.ModifiedUser = Convert.ToString(DataReader.Item("LMBY"))
                    info.LMDT = Convert.ToDateTime(DataReader.Item("LMDT"))
                    list.Add(info)
                End While
            End If
        Catch ex As Exception
            strErrMessage = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try

        If Not String.IsNullOrEmpty(strErrMessage) Then
            ErrorLogInsert(3, "GetWFGroupsBaseOnFormType", strErrMessage, "uspGeneral_GetWFGroups_BaseOnFormType")
        End If

        Return list
    End Function

    Public Function WFGrouping_IU(ByVal info As WorkflowGroupInfo) As String
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("uspGeneral_WFGROUP_IU", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmWFGRPID As New SqlParameter("@wfgrpid", SqlDbType.Int)
        Dim prmFormType As New SqlParameter("@FormType", SqlDbType.VarChar, 50)
        Dim prmLMBY As New SqlParameter("@lmby", SqlDbType.VarChar, 300)
        Dim prmWFID As New SqlParameter("@WFID", SqlDbType.Int)

        prmWFGRPID.Value = info.WFGRPID
        prmFormType.Value = info.FormType
        prmLMBY.Value = info.ModifiedUser
        prmWFID.Value = info.WFID

        Command.Parameters.Add(prmWFGRPID)
        Command.Parameters.Add(prmFormType)
        Command.Parameters.Add(prmLMBY)
        Command.Parameters.Add(prmWFID)

        Try
            Connection.Open()
            Command.ExecuteNonQuery()
        Catch ex As Exception
            strErrMessage = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try

        If Not String.IsNullOrEmpty(strErrMessage) Then
            ErrorLogInsert(3, "WFGrouping_IU", strErrMessage, "uspGeneral_WFGRoup_IU")
        End If
        Return strErrMessage
    End Function
    Public Function TimestampDoc_IU(ByVal info As TimestampDocInfo) As String
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("uspGeneral_WFGROUP_IU", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmDocid As New SqlParameter("@Docid", SqlDbType.Int)
        Dim prmDocName As New SqlParameter("@DocName", SqlDbType.VarChar, 50)
        Dim prmLMBY As New SqlParameter("@lmby", SqlDbType.VarChar, 300)
        Dim prmWFID As New SqlParameter("@WFID", SqlDbType.Int)

        prmDocid.Value = info.Docid
        prmDocName.Value = info.DocName
        prmLMBY.Value = info.ModifiedUser
        prmWFID.Value = info.WFID

        Command.Parameters.Add(prmDocid)
        Command.Parameters.Add(prmDocName)
        Command.Parameters.Add(prmLMBY)
        Command.Parameters.Add(prmWFID)

        Try
            Connection.Open()
            Command.ExecuteNonQuery()
        Catch ex As Exception
            strErrMessage = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try

        'If Not String.IsNullOrEmpty(strErrMessage) Then
        '    ErrorLogInsert(3, "WFGrouping_IU", strErrMessage, "uspGeneral_WFGRoup_IU")
        'End If
        Return strErrMessage
    End Function
    Public Function WorkflowGroup_D(ByVal wfgrpid As Integer) As String
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("Delete tworkflowgroup where wfgrp_id=" & wfgrpid, Connection)
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
            ErrorLogInsert(3, "WorkflowGroup_D", strErrMessage, "CommandType.Text")
        End If
        Return strErrMessage
    End Function

    Public Function GetWorkflowNotInWorkflowGrouping(ByVal formtype As String) As List(Of WCCFlowGroupingInfo)
        Dim isSucceed As Boolean = True
        Dim strErrMessage As String = String.Empty
        Dim list As New List(Of WCCFlowGroupingInfo)
        Command = New SqlCommand("select * from tworkflow where wfid not in(select wfid from tworkflowgroup where form_type = '" & formtype & "') and rstatus = 2 order by lmdt desc", Connection)
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
    Public Function GetTimestampDoc(ByVal DocName As String) As List(Of TimestampDocInfo)
        Dim isSucceed As Boolean = True
        Dim strErrMessage As String = String.Empty
        Dim list As New List(Of TimestampDocInfo)
        Command = New SqlCommand("select cd.doc_id as 'Timestamp Doc' from coddoc cd inner join CODWFDoc cw on cd.doc_id = cw.doc_id and cd.doctype = 'D'", Connection)
        Command.CommandType = CommandType.Text

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read
                    Dim info As New TimestampDocInfo
                    info.Docid = Integer.Parse(DataReader.Item("Docid").ToString())
                    info.DocName = Convert.ToString(DataReader.Item("DocName"))
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


    Public Function GetFinalApprovers(ByVal packageid As String, ByVal grpid1 As Integer, ByVal grpid2 As Integer, ByVal grpid3 As Integer, ByVal wfid As Integer, ByVal taskdesc As String) As List(Of UserProfile)
        Dim list As New List(Of UserProfile)
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("uspGeneral_GetFinalApprover", Connection)
        Command.CommandType = CommandType.StoredProcedure
        Dim prmPackageId As New SqlParameter("@packageid", SqlDbType.VarChar, 40)
        Dim prmGRPID1 As New SqlParameter("@grpid1", SqlDbType.Int)
        Dim prmGRPID2 As New SqlParameter("@grpid2", SqlDbType.Int)
        Dim prmGRPID3 As New SqlParameter("@grpid3", SqlDbType.Int)
        Dim prmWFID As New SqlParameter("@wfid", SqlDbType.Int)
        Dim prmTaskDesc As New SqlParameter("@taskdesc", SqlDbType.VarChar, 20)

        prmPackageId.Value = packageid
        prmGRPID1.Value = grpid1
        prmGRPID2.Value = grpid2
        prmGRPID3.Value = grpid3
        prmWFID.Value = wfid
        prmTaskDesc.Value = taskdesc

        Command.Parameters.Add(prmPackageId)
        Command.Parameters.Add(prmGRPID1)
        Command.Parameters.Add(prmGRPID2)
        Command.Parameters.Add(prmGRPID3)
        Command.Parameters.Add(prmWFID)
        Command.Parameters.Add(prmTaskDesc)

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read
                    Dim info As New UserProfile
                    info.UserId = Integer.Parse(DataReader.Item("usr_id").ToString())
                    info.Fullname = Convert.ToString(DataReader.Item("approvername"))
                    info.SignTitle = Convert.ToString(DataReader.Item("SignTitle"))
                    info.UserType = Convert.ToString(DataReader.Item("usrtype"))
                    info.CompanyName = Convert.ToString(DataReader.Item("CompanyName"))
                    list.Add(info)
                End While
            End If

        Catch ex As Exception
            strErrMessage = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try

        If Not String.IsNullOrEmpty(strErrMessage) Then
            ErrorLogInsert(3, "GetFinalApprovers", strErrMessage, "uspGeneral_GetFinalApprover")
        End If

        Return list
    End Function

    Public Function IsTaskApprover(ByVal tskid As Integer) As Boolean
        Dim isapprover As Boolean = True
        Command = New SqlCommand("uspGeneral_isApprover", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmTSKID As New SqlParameter("@tskid", SqlDbType.Int)
        Dim prmIsApprover As New SqlParameter("@isapprover", SqlDbType.Bit)

        prmTSKID.Value = tskid
        prmIsApprover.Direction = ParameterDirection.Output

        Command.Parameters.Add(prmTSKID)
        Command.Parameters.Add(prmIsApprover)

        Try
            Connection.Open()
            Command.ExecuteNonQuery()
            isapprover = Convert.ToBoolean(prmIsApprover.Value)
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try

        Return isapprover
    End Function

#Region "Others"
    Public Function TaskPendingResponsibilityChecking(ByVal userid As Integer, ByVal formtype As String) As Boolean
        Dim isResponsible As Boolean = True
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("uspGeneral_CheckTaskPendingResponsibility", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmUserID As New SqlParameter("@usrid", SqlDbType.Int)
        Dim prmFormType As New SqlParameter("@formtype", SqlDbType.VarChar, 50)
        Dim prmIsResponsible As New SqlParameter("@isresponsible", SqlDbType.Bit)

        prmUserID.Value = userid
        prmFormType.Value = formtype
        prmIsResponsible.Direction = ParameterDirection.Output

        Command.Parameters.Add(prmUserID)
        Command.Parameters.Add(prmFormType)
        Command.Parameters.Add(prmIsResponsible)

        Try
            Connection.Open()
            Command.ExecuteNonQuery()
            isResponsible = Convert.ToBoolean(prmIsResponsible.Value)
        Catch ex As Exception
            strErrMessage = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try

        If Not String.IsNullOrEmpty(strErrMessage) Then
            isResponsible = False
            ErrorLogInsert(3, "TaskPendingResponsibilityChecking", strErrMessage, "uspGeneral_CheckTaskPendingResponsibility")
        End If

        Return isResponsible
    End Function
#End Region

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
