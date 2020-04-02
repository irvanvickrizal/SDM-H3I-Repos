Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections.Generic

Public Class DeemApprovalController
    Inherits BaseController

#Region "Deem Approval"
    Public Function DeemApproval_IU(ByVal info As DeemApprovalInfo) As Boolean
        Dim isSucceed As Boolean = True
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("uspDeemApproval_IU", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmDAID As New SqlParameter("@oddaid", SqlDbType.Int)
        Dim prmDocId As New SqlParameter("@docid", SqlDbType.Int)
        Dim prmTaskGroup As New SqlParameter("@taskgroup", SqlDbType.VarChar, 100)
        Dim prmUgpID As New SqlParameter("@ugpid", SqlDbType.Int)
        Dim prmTotalDoc As New SqlParameter("@totaldoc", SqlDbType.Int)
        Dim prmLMBY As New SqlParameter("@lmby", SqlDbType.VarChar, 300)
        Dim prmTransactionTypeId As New SqlParameter("@transactiontypeid", SqlDbType.Int)
        Dim prmSLADoc As New SqlParameter("@sladoc", SqlDbType.Int)
        Dim prmSLAWarningDay As New SqlParameter("@slawarningday", SqlDbType.Int)
        Dim prmSLAExecuteDay As New SqlParameter("@executesladay", SqlDbType.Int)
        Dim prmHasDocGroup As New SqlParameter("@hasdocgroup", SqlDbType.Bit)
        Dim prmDocWithGroup As New SqlParameter("@docwithgroup", SqlDbType.Int)

        prmDAID.Value = info.ODDAID
        prmDocId.Value = info.DocInf.DocId
        prmTaskGroup.Value = info.TaskGroup
        prmUgpID.Value = info.USRTypeInfo.UserTypeId
        prmTotalDoc.Value = info.TotalDoc
        prmLMBY.Value = info.CMAInfo.ModifiedUser
        prmTransactionTypeId.Value = info.TransInfo.TransId
        prmSLADoc.Value = info.SLADoc
        prmSLAWarningDay.Value = info.WarningSLANotifDay
        prmSLAExecuteDay.Value = info.AutoExeAfterSLA
        prmHasDocGroup.Value = info.HasDocGroup
        prmDocWithGroup.Value = info.DocGroupInfo.DocId

        Command.Parameters.Add(prmDAID)
        Command.Parameters.Add(prmDocId)
        Command.Parameters.Add(prmTaskGroup)
        Command.Parameters.Add(prmUgpID)
        Command.Parameters.Add(prmTotalDoc)
        Command.Parameters.Add(prmLMBY)
        Command.Parameters.Add(prmTransactionTypeId)
        Command.Parameters.Add(prmSLADoc)
        Command.Parameters.Add(prmSLAWarningDay)
        Command.Parameters.Add(prmSLAExecuteDay)
        Command.Parameters.Add(prmHasDocGroup)
        Command.Parameters.Add(prmDocWithGroup)

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
            ErrorLogInsert(14, "DeemApproval_IU", strErrMessage, "uspDeemApproval_IU")
        End If

        Return isSucceed
    End Function

    Public Function GetDeemApprovals_DS() As DataSet
        Dim ds As New DataSet
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("uspDeemApproval_LD", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Try
            Connection.Open()
            Dim adapter As New SqlDataAdapter(Command)
            adapter.Fill(ds)
        Catch ex As Exception
            strErrMessage = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try

        If Not String.IsNullOrEmpty(strErrMessage) Then
            ErrorLogInsert(14, "GetDeemApprovals", strErrMessage, "uspDeemApproval_LD")
        End If

        Return ds
    End Function

    Public Function GetDeemApprovals() As List(Of DeemApprovalInfo)
        Dim list As New List(Of DeemApprovalInfo)
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("uspDeemApproval_LD", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read()
                    Dim info As New DeemApprovalInfo
                    info.DocInf.DocId = Integer.Parse(DataReader.Item("doc_id").ToString())
                    info.ODDAID = Integer.Parse(DataReader.Item("ODDA_Id").ToString())
                    info.TaskGroup = Convert.ToString(DataReader.Item("Taskgroup"))
                    info.TotalDoc = Integer.Parse(DataReader.Item("total_doc").ToString())
                    info.TransInfo.TransactionType = Convert.ToString(DataReader.Item("Transaction_Type"))
                    info.TransInfo.TransId = Integer.Parse(DataReader.Item("Transaction_Type_Id").ToString())
                    info.USRTypeInfo.UserType = Convert.ToString(DataReader.Item("GrpDesc"))
                    info.HasDocGroup = Convert.ToBoolean(DataReader.Item("HasDocGroup"))
                    info.DocGroupInfo.DocId = Integer.Parse(DataReader.Item("DocWithGroup").ToString())
                    list.Add(info)
                End While
            End If
        Catch ex As Exception
            strErrMessage = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try

        If Not String.IsNullOrEmpty(strErrMessage) Then
            ErrorLogInsert(14, "GetDeemApprovals", strErrMessage, "uspDeemApproval_LD")
        End If

        Return list
    End Function

    Public Function GetDeemApproval(ByVal oddaid As Integer) As DeemApprovalInfo
        Dim info As New DeemApprovalInfo
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("uspDeemApproval_Detail", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmODDAID As New SqlParameter("@oddaid", SqlDbType.Int)
        prmODDAID.Value = oddaid
        Command.Parameters.Add(prmODDAID)

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read()
                    info.DocInf.DocId = Integer.Parse(DataReader.Item("doc_id").ToString())
                    info.ODDAID = Integer.Parse(DataReader.Item("ODDA_Id").ToString())
                    info.TaskGroup = Convert.ToString(DataReader.Item("Taskgroup"))
                    info.TotalDoc = Integer.Parse(DataReader.Item("total_doc").ToString())
                    info.TransInfo.TransactionType = Convert.ToString(DataReader.Item("Transaction_Type"))
                    info.TransInfo.TransId = Integer.Parse(DataReader.Item("Transaction_Type_Id").ToString())
                    info.USRTypeInfo.UserType = Convert.ToString(DataReader.Item("GrpDesc"))
                    info.USRTypeInfo.UserTypeId = Integer.Parse(DataReader.Item("Ugp_id").ToString())
                End While
            End If
        Catch ex As Exception
            strErrMessage = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try

        If Not String.IsNullOrEmpty(strErrMessage) Then
            ErrorLogInsert(14, "GetDeemApproval", strErrMessage, "uspDeemApproval_Detail")
        End If

        Return info
    End Function

    Public Function DeemApproval_D(ByVal oddaid As Integer) As Boolean
        Dim isSucceed As Boolean = True
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("uspDeemApproval_D", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmODDAID As New SqlParameter("@oddaid", SqlDbType.Int)
        prmODDAID.Value = oddaid
        Command.Parameters.Add(prmODDAID)
        Try
            Connection.Open()
            Command.ExecuteNonQuery()
        Catch ex As Exception
            strErrMessage = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try

        If Not String.IsNullOrEmpty(strErrMessage) Then
            ErrorLogInsert(14, "DeemApproval_D", strErrMessage, "DeemApproval_D")
        End If

        Return isSucceed
    End Function

    Public Function DeemApproval_CheckConfigIsExist(ByVal info As DeemApprovalInfo) As Boolean
        Dim isExist As Boolean = False
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("uspDeemApproval_CheckConfigIsAvailable", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmDocID As New SqlParameter("@docid", SqlDbType.Int)
        Dim prmTaskGroup As New SqlParameter("@taskgroup", SqlDbType.VarChar, 100)
        Dim prmUGPID As New SqlParameter("@ugpid", SqlDbType.Int)
        Dim prmTransId As New SqlParameter("@transactiontypeid", SqlDbType.Int)
        Dim prmIsExist As New SqlParameter("@isexist", SqlDbType.Bit)

        prmDocID.Value = info.DocInf.DocId
        prmTaskGroup.Value = info.TaskGroup
        prmUGPID.Value = info.USRTypeInfo.UserTypeId
        prmTransId.Value = info.TransInfo.TransId
        prmIsExist.Direction = ParameterDirection.Output

        Command.Parameters.Add(prmDocID)
        Command.Parameters.Add(prmTaskGroup)
        Command.Parameters.Add(prmUGPID)
        Command.Parameters.Add(prmTransId)
        Command.Parameters.Add(prmIsExist)

        Try
            Connection.Open()
            Command.ExecuteNonQuery()
            isExist = Convert.ToBoolean(prmIsExist.Value)
        Catch ex As Exception
            isExist = True
            strErrMessage = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try
        If Not String.IsNullOrEmpty(strErrMessage) Then
            ErrorLogInsert(14, "DeemApproval_CheckConfigIsAvailable", strErrMessage, "uspDeemApproval_CheckConfigIsAvailable")
        End If
        Return isExist
    End Function
#End Region

#Region "Transaction Type"
    Public Function GetTransactionTypes_DS() As DataSet
        Dim ds As New DataSet
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("uspTrans_GetAllTransactionTypes", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Try
            Connection.Open()
            Dim adapter As New SqlDataAdapter(Command)
            adapter.Fill(ds)
        Catch ex As Exception
            strErrMessage = ex.Message.ToString()
            ds = Nothing
        Finally
            Connection.Close()
        End Try
        If Not String.IsNullOrEmpty(strErrMessage) Then
            ErrorLogInsert(14, "GetTransactionTypes_DS", strErrMessage, "uspTrans_GetAllTransactionTypes")
        End If

        Return ds
    End Function

    Public Function GetTransactionTypes() As List(Of TransactionTypeInfo)
        Dim list As New List(Of TransactionTypeInfo)
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("uspTrans_GetAllTransactionTypes", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read
                    Dim info As New TransactionTypeInfo
                    info.TransId = Integer.Parse(DataReader.Item("trans_id").ToString())
                    info.DocParentId = Integer.Parse(DataReader.Item("Doc_Parent_Id").ToString())
                    info.TransactionType = Convert.ToString(DataReader.Item("Transaction_Type"))
                    info.Description = Convert.ToString(DataReader.Item("Trans_Description"))
                    info.CodDocTable = Convert.ToString(DataReader.Item("Coddoc_Table"))
                    info.TransTable = Convert.ToString(DataReader.Item("Trans_Table"))
                    info.CMAInfo.ModifiedUser = Convert.ToString(DataReader.Item("LMBY"))
                    info.CMAInfo.LMDT = Convert.ToDateTime(DataReader.Item("LMDT"))
                    list.Add(info)
                End While
            End If
        Catch ex As Exception
            strErrMessage = ex.Message.ToString()
            list = Nothing
        Finally
            Connection.Close()
        End Try
        If Not String.IsNullOrEmpty(strErrMessage) Then
            ErrorLogInsert(14, "GetTransactionTypes", strErrMessage, "uspTrans_GetAllTransactionTypes")
        End If

        Return list
    End Function

    Public Function GetTransactionTypeBaseId(ByVal transid As Integer) As TransactionTypeInfo
        Dim info As New TransactionTypeInfo
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("uspTrans_GetTransactionType", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmTransId As New SqlParameter("@transid", SqlDbType.Int)
        prmTransId.Value = transid
        Command.Parameters.Add(prmTransId)

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read
                    info.TransId = transid
                    info.DocParentId = Integer.Parse(DataReader.Item("Doc_Parent_Id").ToString())
                    info.TransactionType = Convert.ToString(DataReader.Item("Transaction_Type"))
                    info.Description = Convert.ToString(DataReader.Item("Trans_Description"))
                    info.CodDocTable = Convert.ToString(DataReader.Item("Coddoc_Table"))
                    info.TransTable = Convert.ToString(DataReader.Item("Trans_Table"))
                    info.CMAInfo.ModifiedUser = Convert.ToString(DataReader.Item("LMBY"))
                    info.CMAInfo.LMDT = Convert.ToDateTime(DataReader.Item("LMDT"))
                End While
            End If
        Catch ex As Exception
            strErrMessage = ex.Message.ToString()

        Finally
            Connection.Close()
        End Try

        If Not String.IsNullOrEmpty(strErrMessage) Then
            ErrorLogInsert(14, "GetTransactionTypeBaseId", strErrMessage, "uspTrans_GetTransactionType")
        End If

        Return info
    End Function

    Public Function TransactionType_IU(ByVal info As TransactionTypeInfo) As Boolean
        Dim isSucceed As Boolean = True
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("uspTrans_TransactionType_IU", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmTransId As New SqlParameter("@transid", SqlDbType.Int)
        Dim prmTransactionType As New SqlParameter("@transactiontype", SqlDbType.VarChar, 100)
        Dim prmTransactionDesc As New SqlParameter("@description", SqlDbType.VarChar, 500)
        Dim prmLMBY As New SqlParameter("@lmby", SqlDbType.VarChar, 300)
        Dim prmTransTable As New SqlParameter("@transtable", SqlDbType.VarChar, 100)
        Dim prmCoddocTable As New SqlParameter("@coddoctable", SqlDbType.VarChar, 100)
        Dim prmDocParentId As New SqlParameter("@docparentid", SqlDbType.Int)

        prmTransId.Value = info.TransId
        prmTransactionType.Value = info.TransactionType
        prmTransactionDesc.Value = info.Description
        prmLMBY.Value = info.CMAInfo.ModifiedUser
        prmTransTable.Value = info.TransTable
        prmCoddocTable.Value = info.CodDocTable
        prmDocParentId.Value = info.DocParentId

        Command.Parameters.Add(prmTransId)
        Command.Parameters.Add(prmTransactionType)
        Command.Parameters.Add(prmTransactionDesc)
        Command.Parameters.Add(prmTransTable)
        Command.Parameters.Add(prmLMBY)
        Command.Parameters.Add(prmCoddocTable)
        Command.Parameters.Add(prmDocParentId)

        Try
            Connection.Open()
            Command.ExecuteNonQuery()
        Catch ex As Exception
            strErrMessage = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try

        If Not String.IsNullOrEmpty(strErrMessage) Then
            isSucceed = False
            ErrorLogInsert(14, "TransactionType_IU", strErrMessage, "uspTrans_TransactionType_IU")
        End If

        Return isSucceed
    End Function

    Public Function TransactionType_D(ByVal transid As Integer) As Boolean
        Dim isSucceed As Boolean = True
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("uspTrans_TransactionType_D", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmTransId As New SqlParameter("@transid", SqlDbType.Int)
        prmTransId.Value = transid
        Command.Parameters.Add(prmTransId)

        Try
            Connection.Open()
            Command.ExecuteNonQuery()
        Catch ex As Exception
            strErrMessage = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try

        If Not String.IsNullOrEmpty(strErrMessage) Then
            isSucceed = False
            ErrorLogInsert(14, "TransactionType_D", strErrMessage, "uspTrans_TransactionType_D")
        End If

        Return isSucceed
    End Function
#End Region

#Region "Deem Approval Reporting"
    Public Function GetAllTrans(ByVal exestartdate As Nullable(Of DateTime), ByVal exeenddate As Nullable(Of DateTime), ByVal dastatus As String) As List(Of DeemApprovalInfo)
        Dim list As New List(Of DeemApprovalInfo)
        Command = New SqlCommand("uspDeemApproval_GetAllTrans", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmExeStartDate As New SqlParameter("@exestartdate", SqlDbType.DateTime)
        Dim prmExeEndDate As New SqlParameter("@exeenddate", SqlDbType.DateTime)
        Dim prmDAStatus As New SqlParameter("dastatus", SqlDbType.VarChar, 200)

        Command.Parameters.Add(prmExeStartDate)
        Command.Parameters.Add(prmExeEndDate)
        Command.Parameters.Add(prmDAStatus)

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read
                    Dim info As New DeemApprovalInfo

                End While
            End If
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try

        Return list
    End Function

    Public Function GetReports(ByVal exestartdate As Nullable(Of DateTime), ByVal exeenddate As Nullable(Of DateTime), ByVal dastatus As String, ByVal userid As Integer) As List(Of DeemApprovalInfo)
        Dim list As New List(Of DeemApprovalInfo)
        Command = New SqlCommand("uspDeemApproval_GetReport", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmExeStartDate As New SqlParameter("@startdate", SqlDbType.DateTime)
        Dim prmExeEndDate As New SqlParameter("@enddate", SqlDbType.DateTime)
        Dim prmDAStatus As New SqlParameter("@dastatus", SqlDbType.VarChar, 200)
        Dim prmUserID As New SqlParameter("@usrid", SqlDbType.Int)

        prmExeStartDate.Value = exestartdate
        If exeenddate.HasValue Then
            prmExeEndDate.Value = exeenddate.Value
        Else
            prmExeEndDate.Value = Nothing
        End If

        If String.IsNullOrEmpty(dastatus) Then
            prmDAStatus.Value = Nothing
        Else
            prmDAStatus.Value = dastatus
        End If

        prmUserID.Value = userid

        Command.Parameters.Add(prmExeStartDate)
        Command.Parameters.Add(prmExeEndDate)
        Command.Parameters.Add(prmDAStatus)
        Command.Parameters.Add(prmUserID)

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read
                    Dim info As New DeemApprovalInfo
                    info.DocInf.DocName = Convert.ToString(DataReader.Item("DocName"))
                    'info.
                End While
            End If
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try

        Return list
    End Function
#End Region

#Region "Document Master"
    Public Function GetDocumentBaseQuery(ByVal strQuery As String) As List(Of DocInfo)
        Dim list As New List(Of DocInfo)
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand(strQuery, Connection)
        Command.CommandType = CommandType.Text

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read()
                    Dim info As New DocInfo
                    info.DocId = Integer.Parse(DataReader.Item("doc_id").ToString())
                    info.DocName = Convert.ToString(DataReader.Item("docname"))
                    info.DocType = Convert.ToString(DataReader.Item("doctype"))
                    list.Add(info)
                End While
            End If
        Catch ex As Exception
            strErrMessage = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try

        If Not String.IsNullOrEmpty(strErrMessage) Then
            ErrorLogInsert(14, "GetDocumentBaseQuery", strErrMessage, "Command.Text")
        End If

        Return list
    End Function

    Public Function GetDocumentDetailBaseQuery(ByVal strQuery As String) As DocInfo
        Dim info As New DocInfo
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand(strQuery, Connection)
        Command.CommandType = CommandType.Text

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read()
                    info.DocId = Integer.Parse(DataReader.Item("doc_id").ToString())
                    info.DocName = Convert.ToString(DataReader.Item("docname"))
                    info.DocType = Convert.ToString(DataReader.Item("doctype"))
                End While
            End If
        Catch ex As Exception
            strErrMessage = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try

        If Not String.IsNullOrEmpty(strErrMessage) Then
            ErrorLogInsert(14, "GetDocumentBaseQuery", strErrMessage, "Command.Text")
        End If

        Return info
    End Function
#End Region

#Region "User Type"
    Public Function GetUserTypes() As List(Of RoleInfo)
        Dim list As New List(Of RoleInfo)
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("select * from tusertype where rstatus = 2 order by grpid asc", Connection)
        Command.CommandType = CommandType.Text

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read
                    Dim info As New RoleInfo
                    info.RoleId = Integer.Parse(DataReader.Item("GrpId").ToString())
                    info.RoleName = Convert.ToString(DataReader.Item("GrpDesc"))
                    list.Add(info)
                End While
            End If
        Catch ex As Exception
            strErrMessage = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try

        If Not String.IsNullOrEmpty(strErrMessage) Then
            ErrorLogInsert(14, "GetUserTypes", strErrMessage, "CommandType.Text")
        End If

        Return list
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
