Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient

Public Class BORNController
    Inherits BaseController

    Public Function BORN_CheckHasIntegrated(ByVal docid As Integer) As Boolean
        Dim hasIntegrated As Boolean = False
        Dim strErrMessage As String = String.Empty
        Dim isSucceed As Boolean = True
        Command = New SqlCommand("uspBORN_HasBeenIntegrated", Connection)
        Command.CommandType = CommandType.StoredProcedure
        Dim prmDocID As New SqlParameter("@docid", SqlDbType.Int)
        Dim prmHasIntegrated As New SqlParameter("@hasintegrated", SqlDbType.Bit)
        prmDocID.Value = docid
        prmHasIntegrated.Direction = ParameterDirection.Output
        Command.Parameters.Add(prmDocID)
        Command.Parameters.Add(prmHasIntegrated)
        Try
            Connection.Open()
            Command.ExecuteNonQuery()
            hasIntegrated = Convert.ToBoolean(prmHasIntegrated.Value)
        Catch ex As Exception
            strErrMessage = ex.Message.ToString()
            isSucceed = False
        Finally
            Connection.Close()
        End Try

        If isSucceed = False Then
            ErrorLogInsert(160, "BORN_CheckHasIntegrated:BORNController", strErrMessage, "uspBORN_HasBeenIntegrated")
        End If

        Return hasIntegrated
    End Function

    Public Function BORN_GetDocApproved(ByVal docid As Integer, ByVal workpackageid As String) As DataTable
        Dim dtResults As New DataTable
        Dim strErrMessage As String = String.Empty
        Dim isSucceed As Boolean = True
        Command = New SqlCommand("uspBORN_GetDocApproved", Connection)
        Command.CommandType = CommandType.StoredProcedure
        Dim prmDocID As New SqlParameter("@docid", SqlDbType.Int)
        Dim prmWorkpackageid As New SqlParameter("@workpackageid", SqlDbType.VarChar, 50)
        prmDocID.Value = docid
        prmWorkpackageid.Value = workpackageid
        Command.Parameters.Add(prmDocID)
        Command.Parameters.Add(prmWorkpackageid)
        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                dtResults.Load(DataReader)
            End If

        Catch ex As Exception
            strErrMessage = ex.Message.ToString()
            isSucceed = False
        Finally
            Connection.Close()
        End Try
        If isSucceed = False Then
            ErrorLogInsert(160, "BORN_GetDocApproved:BORNController", strErrMessage, "uspBORN_GetDocApproved")
        End If

        Return dtResults
    End Function

    Public Function BORN_GetApprovalLog(ByVal submissionsno As Int32) As DataTable
        Dim dtResults As New DataTable
        Dim strErrMessage As String = String.Empty
        Dim isSucceed As Boolean = True
        Command = New SqlCommand("uspBORN_GetApprovalLog", Connection)
        Command.CommandType = CommandType.StoredProcedure
        Dim prmSubmissionSNO As New SqlParameter("@submissionsno", SqlDbType.BigInt)
        prmSubmissionSNO.Value = submissionsno
        Command.Parameters.Add(prmSubmissionSNO)
        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                dtResults.Load(DataReader)
            End If
        Catch ex As Exception
            isSucceed = False
            strErrMessage = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try

        If isSucceed = False Then
            ErrorLogInsert(160, "BORN_GetApprovalLog:BORNController", strErrMessage, "uspBORN_GetApprovalLog")
        End If

        Return dtResults
    End Function

    Public Function BORN_GetSubPODetail(ByVal submissionsno As Int32, ByVal categorycode As String) As DataTable
        Dim dtResults As New DataTable
        Dim strErrMessage As String = String.Empty
        Dim isSucceed As Boolean = True
        Command = New SqlCommand("uspBORN_GetSubPODetails", Connection)
        Command.CommandType = CommandType.StoredProcedure
        Dim prmSubmissionSNO As New SqlParameter("@submissionsno", SqlDbType.BigInt)
        Dim prmCategoryCode As New SqlParameter("@categorycode", SqlDbType.VarChar, 10)
        prmSubmissionSNO.Value = submissionsno
        prmCategoryCode.Value = categorycode
        Command.Parameters.Add(prmSubmissionSNO)
        Command.Parameters.Add(prmCategoryCode)
        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                dtResults.Load(DataReader)
            End If

        Catch ex As Exception
            isSucceed = False
            strErrMessage = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try
        If isSucceed = False Then
            ErrorLogInsert(160, "BORN_GetSubPODetail:BORNController", strErrMessage, "uspBORN_GetSubPODetails")
        End If

        Return dtResults
    End Function

    Public Function BORN_GetBOQListDetail(ByVal submissionsno As Int32, ByVal categorycode As String) As DataTable
        Dim dtResults As New DataTable
        Dim isSucceed As Boolean = True
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("uspBORN_GetAsBuiltBOQ", Connection)
        Command.CommandType = CommandType.StoredProcedure
        Dim prmSubmissionSNO As New SqlParameter("@submissionsno", SqlDbType.BigInt)
        Dim prmCategoryCode As New SqlParameter("@categorycode", SqlDbType.VarChar, 10)
        prmSubmissionSNO.Value = submissionsno
        prmCategoryCode.Value = categorycode
        Command.Parameters.Add(prmSubmissionSNO)
        Command.Parameters.Add(prmCategoryCode)
        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                dtResults.Load(DataReader)
            End If

        Catch ex As Exception
            isSucceed = False
            strErrMessage = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try
        If isSucceed = False Then
            ErrorLogInsert(160, "BORN_GetBOQListDetail:BORNController", strErrMessage, "uspBORN_GetAsBuiltBOQ")
        End If

        Return dtResults
    End Function

    Public Function BORN_GetATPDetail(ByVal submissionsno As Int32) As DataTable
        Dim dtResults As New DataTable
        Dim isSucceed As Boolean = True
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("uspBORN_GetATPDetail", Connection)
        Command.CommandType = CommandType.StoredProcedure
        Dim prmSubmissionSNO As New SqlParameter("@submissionsno", SqlDbType.BigInt)
        prmSubmissionSNO.Value = submissionsno
        Command.Parameters.Add(prmSubmissionSNO)
        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                dtResults.Load(DataReader)
            End If

        Catch ex As Exception
            isSucceed = False
            strErrMessage = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try
        If isSucceed = False Then
            ErrorLogInsert(160, "BORN_GetATPDetail:BORNController", strErrMessage, "uspBORN_GetAsBuiltBOQ")
        End If

        Return dtResults
    End Function

    Public Function BORN_ATPMigration(ByVal submissionsno As Int32, ByVal docid As Integer, ByVal swid As Int32, ByVal siteid As Int32) As Boolean
        Dim isSucceed As Boolean = True
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("uspBORN_ATPMigration", Connection)
        Command.CommandType = CommandType.StoredProcedure
        Dim prmSSNO As New SqlParameter("@ssno", SqlDbType.BigInt)
        Dim prmDocID As New SqlParameter("@docid", SqlDbType.Int)
        Dim prmSWID As New SqlParameter("@swid", SqlDbType.BigInt)
        Dim prmSiteID As New SqlParameter("@siteid", SqlDbType.Int)
        prmSSNO.Value = submissionsno
        prmDocID.Value = docid
        prmSWID.Value = swid
        prmSiteID.Value = siteid
        Command.Parameters.Add(prmSSNO)
        Command.Parameters.Add(prmDocID)
        Command.Parameters.Add(prmSWID)
        Command.Parameters.Add(prmSiteID)
        Try
            Connection.Open()
            Command.ExecuteNonQuery()
        Catch ex As Exception
            strErrMessage = ex.Message.ToString()
            isSucceed = False
        Finally
            Connection.Close()
        End Try

        If isSucceed = False Then
            ErrorLogInsert(160, "BORN_ATPMigration:BORNController", strErrMessage, "uspBORN_ATPMigration")
        End If

        Return isSucceed
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
