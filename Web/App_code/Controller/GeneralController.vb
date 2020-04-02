Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections.Generic

Public Class GeneralController
    Inherits BaseController

    Public Function GetUserBaseOnWFTransactionPending(ByVal docid As Integer, ByVal wpid As String, ByVal wfid As Integer) As List(Of UserProfileInfo)
        Dim list As New List(Of UserProfileInfo)
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("uspGeneral_GetUserGroupBaseOn_WFDefinition", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmDocId As New SqlParameter("@docid", SqlDbType.Int)
        Dim prmWFID As New SqlParameter("@wfid", SqlDbType.Int)
        Dim prmWPID As New SqlParameter("@wpid", SqlDbType.VarChar, 50)

        prmDocId.Value = docid
        prmWPID.Value = wpid
        prmWFID.Value = wfid

        Command.Parameters.Add(prmDocId)
        Command.Parameters.Add(prmWPID)
        Command.Parameters.Add(prmWFID)

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read
                    Dim info As New UserProfileInfo
                    info.UserId = Integer.Parse(DataReader.Item("usr_id").ToString())
                    info.Fullname = Convert.ToString(DataReader.Item("Fullname"))
                    'info.= Integer.Parse(DataReader.Item("usrRole").ToString())
                    info.Email = Convert.ToString(DataReader.Item("Email"))
                    info.PhoneNo = Convert.ToString(DataReader.Item("PhoneNo"))
                    info.UserType = Convert.ToString(DataReader.Item("usrType"))
                    list.Add(info)
                End While
            End If
        Catch ex As Exception
            strErrMessage = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try

        If Not String.IsNullOrEmpty(strErrMessage) Then
            ErrorLogInsert(3, "GetUserBaseOnWFTransactionPending", strErrMessage, "uspGeneral_GetUserGroupBaseOn_WFDefinition")
        End If

        Return list
    End Function
	
	Protected Function GetTotalSiteActives() As Integer
        Dim getTotal As Integer = 0
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("uspGeneral_GetActiveSites", Connection)
        Command.CommandType = CommandType.StoredProcedure
        Dim prmTotalSites As New SqlParameter("@getsitetotal", SqlDbType.Int)
        prmTotalSites.Direction = ParameterDirection.Output
        Command.Parameters.Add(prmTotalSites)
        Try
            Connection.Open()
            Command.ExecuteNonQuery()
            getTotal = Integer.Parse(prmTotalSites.Value)
        Catch ex As Exception
            strErrMessage = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try

        Return getTotal
    End Function

    Public Function GetSummaryDone(ByVal userid As Integer) As DataTable
        Dim dtResult As New DataTable
        Command = New SqlCommand("uspGeneral_GetSummaryDone", Connection)
        Command.CommandType = CommandType.StoredProcedure
        Dim prmUserID As New SqlParameter("@userid", SqlDbType.Int)
        prmUserID.Value = userid
        Command.Parameters.Add(prmUserID)
        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                dtResult.Load(DataReader)
            End If
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try
        Return dtResult
    End Function

    Public Function GetSummaryProgress(ByVal pono As String, ByVal siteno As String) As DataTable
        Dim dtResult As New DataTable
        Command = New SqlCommand("uspGeneral_GetSummaryProgressDoc", Connection)
        Command.CommandType = CommandType.StoredProcedure
        Dim prmPONO As New SqlParameter("@pono", SqlDbType.VarChar, 150)
        Dim prmSiteNo As New SqlParameter("@siteno", SqlDbType.VarChar, 150)
        prmPONO.Value = pono
        prmSiteNo.Value = siteno
        Command.Parameters.Add(prmPONO)
        Command.Parameters.Add(prmSiteNo)        
        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                dtResult.Load(DataReader)
            End If
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try
        Return dtResult
    End Function

    Public Function GetAllPONO() As DataTable
        Dim dtResult As New DataTable
        Command = New SqlCommand("uspGeneral_GetAllPono", Connection)
        Command.CommandType = CommandType.StoredProcedure
        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                dtResult.Load(DataReader)
            End If
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try
        Return dtResult
    End Function
	
	
    Public Function Document_ReadyToUploadCount(ByVal docid As Integer, ByVal userid As Integer) As Integer
        Dim rowcount As Integer = 0
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("HCPT_uspDocumentReadytoUpload_Count", Connection)
        Command.CommandType = CommandType.StoredProcedure
        Dim prmDocID As New SqlParameter("@docid", SqlDbType.Int)
        Dim prmUserID As New SqlParameter("@userid", SqlDbType.Int)
        Dim prmRowCount As New SqlParameter("@rowcount", SqlDbType.Int)
        prmDocID.Value = docid
        prmUserID.Value = userid
        prmRowCount.Direction = ParameterDirection.Output
        Command.Parameters.Add(prmDocID)
        Command.Parameters.Add(prmUserID)
        Command.Parameters.Add(prmRowCount)
        Try
            Connection.Open()
            Command.ExecuteNonQuery()
            rowcount = Integer.Parse(prmRowCount.Value)
        Catch ex As Exception
            strErrMessage = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try
        If (Not String.IsNullOrEmpty(strErrMessage)) Then
            ErrorLogInsert(3, "Document_ReadyToUploadCount:GeneralController", strErrMessage, "HCPT_uspDocumentReadytoUpload_Count")
        End If
        Return rowcount
    End Function

    Public Function Document_ReadyToUploadDetail(ByVal docid As Integer, ByVal userid As Integer) As DataTable
        Dim dtResult As New DataTable
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("HCPT_uspDocumentReadytoUpload_Detail", Connection)
        Command.CommandType = CommandType.StoredProcedure
        Dim prmDocID As New SqlParameter("@docid", SqlDbType.Int)
        Dim prmUserID As New SqlParameter("@userid", SqlDbType.Int)
        prmDocID.Value = docid
        prmUserID.Value = userid
        Command.Parameters.Add(prmDocID)
        Command.Parameters.Add(prmUserID)
        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                dtResult.Load(DataReader)
            End If
        Catch ex As Exception
            strErrMessage = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try

        If (Not String.IsNullOrEmpty(strErrMessage)) Then
            ErrorLogInsert(3, "Document_ReadyToUploadDetail:GeneralController", strErrMessage, "HCPT_uspDocumentReadytoUpload_Detail")
        End If

        Return dtResult
    End Function

    Public Function DocumentNPO_GetApprovalStatus(ByVal userid As Integer) As DataTable
        Dim dtResult As New DataTable
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("HCPT_uspDashboard_SiteAcceptanceStatus_NPO", Connection)
        Command.CommandType = CommandType.StoredProcedure
        Dim prmUserID As New SqlParameter("@usrid", SqlDbType.Int)
        prmUserID.Value = userid
        Command.Parameters.Add(prmUserID)
        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                dtResult.Load(DataReader)
            End If
        Catch ex As Exception
            strErrMessage = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try

        If Not String.IsNullOrEmpty(strErrMessage) Then
            ErrorLogInsert(3, "DocumentNPO_GetApprovalStatus:GeneralController", strErrMessage, "HCPT_uspDashboard_SiteAcceptanceStatus_NPO")
        End If
        Return dtResult
    End Function

    Public Function RejectedDoc_HCPT(ByVal userid As Integer) As DataTable
        Dim dtResult As New DataTable
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("HCPT_uspDashBoardAgenda_NPO_RejectedDoc_HCPT", Connection)
        Command.CommandType = CommandType.StoredProcedure
        Dim prmUserID As New SqlParameter("@usrid", SqlDbType.Int)
        prmUserID.Value = userid
        Command.Parameters.Add(prmUserID)
        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                dtResult.Load(DataReader)
            End If
        Catch ex As Exception
            strErrMessage = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try

        If Not String.IsNullOrEmpty(strErrMessage) Then
            ErrorLogInsert(3, "RejectedDoc_HCPT:GeneralController", strErrMessage, "HCPT_uspDashBoardAgenda_NPO_RejectedDoc_HCPT")
        End If
        Return dtResult
    End Function

    Public Function RPT_EStorageProgressSummary(ByVal rgnid As Integer, ByVal pono As String) As DataTable
        Dim dtResult As New DataTable
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("HCPT_uspDocSummaryProgress_EStorage", Connection)
        Command.CommandType = CommandType.StoredProcedure
        Dim prmRgnID As New SqlParameter("@rgnid", SqlDbType.Int)
        Dim prmPONO As New SqlParameter("@hotasperpo", SqlDbType.VarChar, 100)
        prmRgnID.Value = rgnid
        Command.Parameters.Add(prmRgnID)
        If Not String.IsNullOrEmpty(pono) Then
            prmPONO.Value = pono
            Command.Parameters.Add(prmPONO)
        End If
        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                dtResult.Load(DataReader)
            End If
        Catch ex As Exception
            strErrMessage = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try

        If (Not String.IsNullOrEmpty(strErrMessage)) Then
            ErrorLogInsert(3, "RPT_EStorageProgressSummary:GeneralController", strErrMessage, "uspDocSummaryProgress_EStorage")
        End If

        Return dtResult
    End Function
    Public Function EStorageProgressSummary(ByVal rgnid As Integer, ByVal pono As String) As DataTable
        Dim dtResult As New DataTable
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("HCPT_uspDocSummaryProgress", Connection)
        Command.CommandType = CommandType.StoredProcedure
        Dim prmRgnID As New SqlParameter("@rgnid", SqlDbType.Int)
        Dim prmPONO As New SqlParameter("@hotasperpo", SqlDbType.VarChar, 100)
        prmRgnID.Value = rgnid
        Command.Parameters.Add(prmRgnID)
        If Not String.IsNullOrEmpty(pono) Then
            prmPONO.Value = pono
            Command.Parameters.Add(prmPONO)
        End If
        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                dtResult.Load(DataReader)
            End If
        Catch ex As Exception
            strErrMessage = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try

        If (Not String.IsNullOrEmpty(strErrMessage)) Then
            ErrorLogInsert(3, "EStorageProgressSummary:GeneralController", strErrMessage, "uspDocSummaryProgress")
        End If

        Return dtResult
    End Function

    Public Function RPT_EStorageProgressSummaryGetReport(ByVal rgnid As Integer, ByVal pono As String, ByVal downloadtype As String) As DataTable
        Dim dtResult As New DataTable
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("HCPT_uspDocSummaryProgress_EStorage_Rpt", Connection)
        Command.CommandType = CommandType.StoredProcedure
        Dim prmRgnID As New SqlParameter("@rgnid", SqlDbType.Int)
        Dim prmPONO As New SqlParameter("@hotasperpo", SqlDbType.VarChar, 100)
        Dim prmDownloadType As New SqlParameter("@downloadtype", SqlDbType.VarChar, 20)
        prmRgnID.Value = rgnid
        prmDownloadType.Value = downloadtype
        Command.Parameters.Add(prmDownloadType)
        Command.Parameters.Add(prmRgnID)
        If Not String.IsNullOrEmpty(pono) Then
            prmPONO.Value = pono
            Command.Parameters.Add(prmPONO)
        End If
        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                dtResult.Load(DataReader)
            End If
        Catch ex As Exception
            strErrMessage = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try

        If (Not String.IsNullOrEmpty(strErrMessage)) Then
            ErrorLogInsert(3, "RPT_EStorageProgressSummaryGetReport:GeneralController", strErrMessage, "HCPT_uspDocSummaryProgress_EStorage_Rpt")
        End If

        Return dtResult
    End Function

    Public Function GetAllHOTasPO() As DataTable
        Dim dtResult As New DataTable
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("HCPT_uspGetListHotasperpo", Connection)
        Command.CommandType = CommandType.StoredProcedure
        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                dtResult.Load(DataReader)
            End If
        Catch ex As Exception
            strErrMessage = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try

        If Not String.IsNullOrEmpty(strErrMessage) Then
            ErrorLogInsert(3, "GetAllHOTasPO:GeneralController", strErrMessage, "HCPT_uspGetListHotasperpo")
        End If
        Return dtResult
    End Function

    Public Function GetAllRegions() As DataTable
        Dim dtResult As New DataTable
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("HCPT_uspRegion_GetAllActives", Connection)
        Command.CommandType = CommandType.StoredProcedure
        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                dtResult.Load(DataReader)
            End If
        Catch ex As Exception
            strErrMessage = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try

        If Not String.IsNullOrEmpty(strErrMessage) Then
            ErrorLogInsert(3, "GetAllRegions:GeneralController", strErrMessage, "HCPT_uspRegion_GetAllActives")
        End If
        Return dtResult
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
