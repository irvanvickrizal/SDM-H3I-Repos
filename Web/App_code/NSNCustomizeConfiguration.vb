Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Collections.Generic
Public Class NSNCustomizeConfiguration
    Private Shared objutil As New Common.DBUtil
    Private Shared mycon As SqlConnection = New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("conn").ToString())
    Private Shared reader As SqlDataReader

    Public Shared Function TaskPendingApproverCount(ByVal usrId As Integer) As Integer
        Dim tskPending As Integer = 0
        Dim command As New SqlCommand("uspSitePendingApproverCount", mycon)
        command.CommandType = CommandType.StoredProcedure
        Dim prmUsrId As New SqlParameter("@usrid", SqlDbType.BigInt)
        Dim prmCountTaskPending As New SqlParameter("@totalRow", SqlDbType.Int)
        prmUsrId.Value = usrId
        prmCountTaskPending.Direction = ParameterDirection.Output
        command.Parameters.Add(prmUsrId)
        command.Parameters.Add(prmCountTaskPending)
        Try
            mycon.Open()
            command.ExecuteNonQuery()
            tskPending = Convert.ToInt16(prmCountTaskPending.Value)
        Catch ex As Exception
        End Try
        mycon.Close()
        Return tskPending
    End Function

    Public Shared Function TaskPendingReviewerCount(ByVal usrId As Integer) As Integer
        Dim tskPending As Integer = 0
        Dim command As New SqlCommand("uspSitePendingReviewerCount", mycon)
        command.CommandType = CommandType.StoredProcedure
        Dim prmUsrId As New SqlParameter("@usrid", SqlDbType.BigInt)
        Dim prmCountTaskPending As New SqlParameter("@totalRow", SqlDbType.Int)
        prmUsrId.Value = usrId
        prmCountTaskPending.Direction = ParameterDirection.Output
        command.Parameters.Add(prmUsrId)
        command.Parameters.Add(prmCountTaskPending)
        Try
            mycon.Open()
            command.ExecuteNonQuery()
            tskPending = Convert.ToInt16(prmCountTaskPending.Value)
        Catch ex As Exception
        End Try
        mycon.Close()
        Return tskPending
    End Function

    Public Shared Function GetMailByUsername(ByVal usrLogin As String) As String
        Dim strEmail As String = String.Empty
        Dim command As New SqlCommand("uspGetEmailByUsername", mycon)
        command.CommandType = CommandType.StoredProcedure
        Dim prmUserLogin As New SqlParameter("@username", SqlDbType.NVarChar, 20)
        Dim prmEmail As New SqlParameter("@email", SqlDbType.NVarChar, 50)
        Dim prmClientname As New SqlParameter("@clientname", SqlDbType.NVarChar, 50)
        Dim prmUserId As New SqlParameter("@userid", SqlDbType.BigInt)
        Dim prmApproved As New SqlParameter("@approved", SqlDbType.Bit)
        Dim prmAccStatus As New SqlParameter("@acc_status", SqlDbType.Char, 1)
        prmUserLogin.Value = usrLogin
        prmEmail.Direction = ParameterDirection.Output
        prmClientname.Direction = ParameterDirection.Output
        prmUserId.Direction = ParameterDirection.Output
        prmApproved.Direction = ParameterDirection.Output
        prmAccStatus.Direction = ParameterDirection.Output
        command.Parameters.Add(prmUserLogin)
        command.Parameters.Add(prmEmail)
        command.Parameters.Add(prmClientname)
        command.Parameters.Add(prmUserId)
        command.Parameters.Add(prmApproved)
        command.Parameters.Add(prmAccStatus)
        Try
            mycon.Open()
            command.ExecuteNonQuery()
            strEmail = Convert.ToString(prmEmail.Value) & "-" & Convert.ToString(prmClientname.Value) & "-" & Convert.ToString(prmUserId.Value) & "-" & Convert.ToBoolean(prmApproved.Value) & "-" & Convert.ToString(prmAccStatus.Value)
        Catch ex As Exception
            Dim err As String = ex.Message.ToString()
        End Try
        mycon.Close()
        Return strEmail
    End Function

    Public Shared Function GetUserByATPFlow(ByVal siteid As Integer, ByVal siteversion As Integer, ByVal docATPid As Integer) As Integer
        Dim userid As Integer = 0
        Dim command As New SqlCommand("uspGetUserIdByATPFlow", mycon)
        command.CommandType = CommandType.StoredProcedure
        Dim prmSiteId As New SqlParameter("@siteid", SqlDbType.Int)
        Dim prmSiteVersion As New SqlParameter("@siteversion", SqlDbType.Int)
        Dim prmDocATPId As New SqlParameter("@docatpid", SqlDbType.Int)
        Dim prmUserid As New SqlParameter("@userid", SqlDbType.Int)

        prmSiteId.Value = siteid
        prmSiteVersion.Value = siteversion
        prmDocATPId.Value = docATPid
        prmUserid.Direction = ParameterDirection.Output

        command.Parameters.Add(prmSiteId)
        command.Parameters.Add(prmSiteVersion)
        command.Parameters.Add(prmDocATPId)
        command.Parameters.Add(prmUserid)

        Try
            mycon.Open()
            command.ExecuteNonQuery()
            userid = prmUserid.Value
        Catch ex As Exception

        End Try
        mycon.Close()

        Return userid
    End Function

    Public Shared Function IsATPAlreadyDone(ByVal siteid As Int32, ByVal siteversion As Integer) As Integer
        Dim isAlreadyDone As Integer = 0
        Dim command As New SqlCommand("uspCheckATPAlreadyDone", mycon)
        command.CommandType = CommandType.StoredProcedure

        Dim prmSiteid As New SqlParameter("@siteid", SqlDbType.BigInt)
        Dim prmSiteVersion As New SqlParameter("@siteversion", SqlDbType.Int)
        Dim prmIsAlreadyApproved As New SqlParameter("@atpalreadydone", SqlDbType.Int)

        prmSiteid.Value = siteid
        prmSiteVersion.Value = siteversion
        prmIsAlreadyApproved.Direction = ParameterDirection.Output

        command.Parameters.Add(prmSiteid)
        command.Parameters.Add(prmSiteVersion)
        command.Parameters.Add(prmIsAlreadyApproved)

        Try
            mycon.Open()
            command.ExecuteNonQuery()
            isAlreadyDone = Convert.ToInt16(prmIsAlreadyApproved.Value)
        Catch ex As Exception
        Finally
            mycon.Close()
        End Try
        Return isAlreadyDone
    End Function

#Region "Modernization Value"

    Public Shared Function IsAlreadyExistRecordedInModernizationValue(ByVal siteno As String, ByVal version As Integer) As Boolean
        Dim isRecorded As Boolean = False
        Dim command As New SqlCommand("uspCountRecordedModernizationValue", mycon)
        command.CommandType = CommandType.StoredProcedure
        Dim prmSiteno As New SqlParameter("@Siteno", SqlDbType.NChar, 50)
        Dim prmVersion As New SqlParameter("@Version", SqlDbType.Int)
        Dim prmRowCount As New SqlParameter("@rowCount", SqlDbType.Int)

        prmSiteno.Value = siteno
        prmVersion.Value = version
        prmRowCount.Direction = ParameterDirection.Output

        command.Parameters.Add(prmSiteno)
        command.Parameters.Add(prmVersion)
        command.Parameters.Add(prmRowCount)

        Try
            mycon.Open()
            command.ExecuteNonQuery()
            Dim rowCount As Integer = Convert.ToInt16(prmRowCount.Value)
            If (rowCount > 0) Then
                isRecorded = True
            End If
        Catch ex As Exception
        End Try
        mycon.Close()
        Return isRecorded
    End Function

    Public Shared Sub InsertModernizationValue(ByVal info As ModernizationInfo)
        Dim command As New SqlCommand("uspInsertModernizationValue", mycon)
        command.CommandType = CommandType.StoredProcedure
        Dim prmSiteno As New SqlParameter("@Siteno", SqlDbType.NChar, 50)
        Dim prmVersion As New SqlParameter("@Version", SqlDbType.Int)
        Dim prmBBHValue As New SqlParameter("@BBHValue", SqlDbType.Decimal)
        Dim prmBBSValue As New SqlParameter("@BBSValue", SqlDbType.Decimal)
        prmSiteno.Value = info.SiteNo
        prmVersion.Value = info.Version
        prmBBSValue.Value = info.BBSValue
        prmBBHValue.Value = info.BBHValue
        command.Parameters.Add(prmSiteno)
        command.Parameters.Add(prmVersion)
        command.Parameters.Add(prmBBHValue)
        command.Parameters.Add(prmBBSValue)
        Try
            mycon.Open()
            command.ExecuteNonQuery()
        Catch ex As Exception
        End Try
        mycon.Close()
    End Sub

    Public Shared Sub UpdateModernizationValue(ByVal info As ModernizationInfo)
        Dim command As New SqlCommand("uspUpdateModernizationValue", mycon)
        command.CommandType = CommandType.StoredProcedure
        Dim prmSiteno As New SqlParameter("@Siteno", SqlDbType.NChar, 50)
        Dim prmVersion As New SqlParameter("@Version", SqlDbType.Int)
        Dim prmBBHValue As New SqlParameter("@BBHValue", SqlDbType.Decimal)
        Dim prmBBSValue As New SqlParameter("@BBSValue", SqlDbType.Decimal)
        prmSiteno.Value = info.SiteNo
        prmVersion.Value = info.Version
        prmBBSValue.Value = info.BBSValue
        prmBBHValue.Value = info.BBHValue
        command.Parameters.Add(prmSiteno)
        command.Parameters.Add(prmVersion)
        command.Parameters.Add(prmBBHValue)
        command.Parameters.Add(prmBBSValue)
        Try
            mycon.Open()
            command.ExecuteNonQuery()
        Catch ex As Exception
        End Try
        mycon.Close()
    End Sub

    Public Shared Sub DeleteModernizationValue(ByVal siteno As String, ByVal version As Integer)
        Dim command As New SqlCommand("uspDeleteRecordedModernizationValue", mycon)
        command.CommandType = CommandType.StoredProcedure
        Dim prmSiteno As New SqlParameter("@Siteno", SqlDbType.NChar, 50)
        Dim prmVersion As New SqlParameter("@Version", SqlDbType.Int)
        prmSiteno.Value = siteno
        prmVersion.Value = version
        command.Parameters.Add(prmSiteno)
        command.Parameters.Add(prmVersion)
        Try
            mycon.Open()
            command.ExecuteNonQuery()
        Catch ex As Exception
        End Try
        mycon.Close()
    End Sub

    Public Shared Function GetModernizationValue(ByVal siteno As String, ByVal version As Integer) As ModernizationInfo
        Dim minfo As New ModernizationInfo
        Dim command As New SqlCommand("uspGetRecordedModernizationValue", mycon)
        command.CommandType = CommandType.StoredProcedure
        Dim prmSiteno As New SqlParameter("@Siteno", SqlDbType.NChar, 50)
        Dim prmVersion As New SqlParameter("@Version", SqlDbType.Int)
        prmSiteno.Value = siteno
        prmVersion.Value = version
        command.Parameters.Add(prmSiteno)
        command.Parameters.Add(prmVersion)
        Try
            mycon.Open()
            reader = command.ExecuteReader()
            While (reader.Read())
                minfo.Sno = reader.Item("Sno")
                minfo.SiteNo = reader.Item("Siteno")
                minfo.Version = reader.Item("Version")
                minfo.BBHValue = reader.Item("BBHValue")
                minfo.BBSValue = reader.Item("BBSValue")
            End While
        Catch ex As Exception
        End Try
        mycon.Close()

        Return minfo
    End Function
#End Region

#Region "Get Approval Status Site Bases"
    Public Shared Function GetRolesByArea(ByVal ara_id As Integer) As List(Of UserProfile)
        Dim list As New List(Of UserProfile)
        Dim command As New SqlCommand("uspGetUserApproverReviewerByArea_NSNFramework", mycon)
        command.CommandType = CommandType.StoredProcedure
        Dim prmAraId As New SqlParameter("@ara_id", SqlDbType.Int)
        Dim prmWfId As New SqlParameter("@wfid", SqlDbType.NVarChar, 100)

        prmAraId.Value = ara_id
        prmWfId.Value = (System.Configuration.ConfigurationManager.AppSettings("AppRevWF").ToString())

        command.Parameters.Add(prmAraId)
        command.Parameters.Add(prmWfId)

        Try
            mycon.Open()
            reader = command.ExecuteReader()

            While (reader.Read())
                Dim usrInfo As New UserProfile
                usrInfo.RoleId = reader.Item("usrRole")
                usrInfo.UserId = reader.Item("usr_Id")
                usrInfo.Username = reader.Item("name")
                list.Add(usrInfo)
            End While
        Catch ex As Exception
            Dim err As String = ex.Message.ToString()
        Finally
            mycon.Close()
        End Try
        Return list
    End Function

    Public Shared Function GetRolesByArea_AdminOnly() As List(Of UserProfile)
        Dim list As New List(Of UserProfile)
        Dim command As New SqlCommand("uspGetUserApproverReviewerByArea_AdminOnly_NSNFramework", mycon)
        command.CommandType = CommandType.StoredProcedure
        Dim prmWfId As New SqlParameter("@wfid", SqlDbType.NVarChar, 100)

        prmWfId.Value = (System.Configuration.ConfigurationManager.AppSettings("AppRevWF").ToString())
        command.Parameters.Add(prmWfId)

        Try
            mycon.Open()
            reader = command.ExecuteReader()

            While (reader.Read())
                Dim usrInfo As New UserProfile
                usrInfo.RoleId = reader.Item("usrRole")
                usrInfo.UserId = reader.Item("usr_Id")
                usrInfo.Username = reader.Item("name")
                list.Add(usrInfo)
            End While
        Catch ex As Exception
            Dim err As String = ex.Message.ToString()
        Finally
            mycon.Close()
        End Try
        Return list
    End Function

    
#End Region

#Region "Get BAST, BAUT, ATP Done"
    Public Shared Function GetBASTDoneReportByDate(ByVal startdate As DateTime, ByVal enddate As DateTime, ByVal usrid As Int16, ByVal bautdocid As Integer, ByVal bastdocid As Integer) As DataTable
        Dim dtResults As New DataTable
        Dim strSpType As String = String.Empty
        If usrid > 0 Then
            strSpType = "uspGetEBASTDone2"
        Else
            strSpType = "uspGetEBASTDone"
        End If
        Dim command As New SqlCommand(strSpType, mycon)
        command.CommandType = CommandType.StoredProcedure


        Dim prmBAUTID As New SqlParameter("@BAUTID", SqlDbType.Int)
        Dim prmBASTID As New SqlParameter("@BASTID", SqlDbType.Int)
        Dim prmStartDate As New SqlParameter("@StartDate", SqlDbType.DateTime)
        Dim prmEndDate As New SqlParameter("@Enddate", SqlDbType.DateTime)

        If usrid > 0 Then
            Dim prmUserid As New SqlParameter("@usrid", SqlDbType.BigInt)
            prmUserid.Value = usrid
            command.Parameters.Add(prmUserid)
        End If

        prmBAUTID.Value = bautdocid
        prmBASTID.Value = bastdocid
        prmStartDate.Value = startdate
        prmEndDate.Value = enddate

        command.Parameters.Add(prmBAUTID)
        command.Parameters.Add(prmBASTID)
        command.Parameters.Add(prmStartDate)
        command.Parameters.Add(prmEndDate)

        Try
            mycon.Open()
            reader = command.ExecuteReader()
            dtResults.Load(reader)
        Catch ex As Exception

        Finally
            mycon.Close()
        End Try

        Return dtResults
    End Function

    Public Shared Function GetATPDoneByDate(ByVal userid As Int16, ByVal startdate As DateTime, ByVal enddate As DateTime) As DataTable
        Dim dtResults As New DataTable

        Dim command As New SqlCommand("uspGetATPDoneByDate", mycon)
        command.CommandType = CommandType.StoredProcedure


        Dim prmUserId As New SqlParameter("@usrid", SqlDbType.BigInt)
        Dim prmStartDate As New SqlParameter("@startdate", SqlDbType.DateTime)
        Dim prmEndDate As New SqlParameter("@enddate", SqlDbType.DateTime)

        prmUserId.Value = userid
        prmStartDate.Value = startdate
        prmEndDate.Value = enddate

        command.Parameters.Add(prmUserId)
        command.Parameters.Add(prmStartDate)
        command.Parameters.Add(prmEndDate)

        Try
            mycon.Open()
            reader = command.ExecuteReader()
            dtResults.Load(reader)

        Catch ex As Exception
            objutil.ExeNonQuery("exec uspErrLog '', 'Report ATP Done','" & ex.Message.ToString.Replace("'", "''") & "','ReportATPDoneByDate'")
        Finally
            mycon.Close()
        End Try
        Return dtResults
    End Function

    Public Shared Function GetCACDoneByDate(ByVal userid As Int16, ByVal startdate As DateTime, ByVal enddate As DateTime) As DataTable
        Dim dtResults As New DataTable

        Dim command As New SqlCommand("uspGetCACDoneByDate", mycon)
        command.CommandType = CommandType.StoredProcedure


        Dim prmUserId As New SqlParameter("@usrid", SqlDbType.BigInt)
        Dim prmStartDate As New SqlParameter("@startdate", SqlDbType.DateTime)
        Dim prmEndDate As New SqlParameter("@enddate", SqlDbType.DateTime)

        prmUserId.Value = userid
        prmStartDate.Value = startdate
        prmEndDate.Value = enddate

        command.Parameters.Add(prmUserId)
        command.Parameters.Add(prmStartDate)
        command.Parameters.Add(prmEndDate)

        Try
            mycon.Open()
            reader = command.ExecuteReader()
            dtResults.Load(reader)

        Catch ex As Exception
            objutil.ExeNonQuery("exec uspErrLog '', 'Report ATP Done','" & ex.Message.ToString.Replace("'", "''") & "','ReportATPDoneByDate'")
        Finally
            mycon.Close()
        End Try
        Return dtResults
    End Function

    Public Shared Function GetQCDoneByDate(ByVal userid As Int16, ByVal startdate As DateTime, ByVal enddate As DateTime, ByVal docid As Integer) As DataTable
        Dim dtResults As New DataTable

        Dim command As New SqlCommand("uspGetQCDoneByDate", mycon)
        command.CommandType = CommandType.StoredProcedure


        Dim prmUserId As New SqlParameter("@usrid", SqlDbType.BigInt)
        Dim prmStartDate As New SqlParameter("@startdate", SqlDbType.DateTime)
        Dim prmEndDate As New SqlParameter("@enddate", SqlDbType.DateTime)
        Dim prmDocid As New SqlParameter("@docid", SqlDbType.Int)

        prmUserId.Value = userid
        prmStartDate.Value = startdate
        prmEndDate.Value = enddate
        prmDocid.Value = docid

        command.Parameters.Add(prmUserId)
        command.Parameters.Add(prmStartDate)
        command.Parameters.Add(prmEndDate)
        command.Parameters.Add(prmDocid)

        Try
            mycon.Open()
            reader = command.ExecuteReader()
            dtResults.Load(reader)
        Catch ex As Exception
            objutil.ExeNonQuery("exec uspErrLog 02, 'Report QC Done','" & ex.Message.ToString.Replace("'", "''") & "','ReportQCDoneByDate'")
        Finally
            mycon.Close()
        End Try
        Return dtResults
    End Function

    Public Shared Function GetQCDone(ByVal userid As Int16, ByVal docid As Integer) As DataTable
        Dim dtResults As New DataTable

        Dim command As New SqlCommand("uspGetQCDone", mycon)
        command.CommandType = CommandType.StoredProcedure
        'command.CommandTimeout = 3000

        Dim prmUserId As New SqlParameter("@usrid", SqlDbType.BigInt)
        Dim prmDocid As New SqlParameter("@docid", SqlDbType.Int)

        prmUserId.Value = userid
        prmDocid.Value = docid

        command.Parameters.Add(prmUserId)
        command.Parameters.Add(prmDocid)

        Try
            mycon.Open()
            reader = command.ExecuteReader()
            dtResults.Load(reader)
        Catch ex As Exception
            objutil.ExeNonQuery("exec uspErrLog 02, 'Report QC Done','" & ex.Message.ToString.Replace("'", "''") & "','ReportQCDoneByDate'")
        Finally
            mycon.Close()
        End Try
        Return dtResults
    End Function

    Public Shared Function GetQCDoneByApprovedDate(ByVal userid As Int16, ByVal startdate As DateTime, ByVal docid As Integer) As DataTable
        Dim dtResults As New DataTable

        Dim command As New SqlCommand("uspGetQCDoneByApprovedDate", mycon)
        command.CommandType = CommandType.StoredProcedure
        'command.CommandTimeout = 3000

        Dim prmUserId As New SqlParameter("@usrid", SqlDbType.BigInt)
        Dim prmStartDate As New SqlParameter("@startdate", SqlDbType.DateTime)
        Dim prmDocid As New SqlParameter("@docid", SqlDbType.Int)

        prmUserId.Value = userid
        prmStartDate.Value = startdate
        prmDocid.Value = docid

        command.Parameters.Add(prmUserId)
        command.Parameters.Add(prmStartDate)
        command.Parameters.Add(prmDocid)

        Try
            mycon.Open()
            reader = command.ExecuteReader()
            If reader.HasRows Then
                dtResults.Load(reader)
            End If

        Catch ex As Exception
            objutil.ExeNonQuery("exec uspErrLog 02, 'Report QC Done','" & ex.Message.ToString.Replace("'", "''") & "','ReportQCDoneByDate'")
        Finally
            mycon.Close()
        End Try
        Return dtResults
    End Function

    Public Shared Function GetQCDoneByApprovedDateRegion(ByVal userid As Int16, ByVal startdate As DateTime, ByVal rgnid As Integer, ByVal docid As Integer) As DataTable
        Dim dtResults As New DataTable

        Dim command As New SqlCommand("uspGetQCDoneByApprovedDate", mycon)
        command.CommandType = CommandType.StoredProcedure
        'command.CommandTimeout = 3000

        Dim prmUserId As New SqlParameter("@usrid", SqlDbType.BigInt)
        Dim prmStartDate As New SqlParameter("@startdate", SqlDbType.DateTime)
        Dim prmRgnId As New SqlParameter("@rgnid", SqlDbType.Int)
        Dim prmDocid As New SqlParameter("@docid", SqlDbType.Int)

        prmUserId.Value = userid
        prmStartDate.Value = startdate
        prmRgnId.Value = rgnid
        prmDocid.Value = docid

        command.Parameters.Add(prmUserId)
        command.Parameters.Add(prmStartDate)
        command.Parameters.Add(prmRgnId)
        command.Parameters.Add(prmDocid)

        Try
            mycon.Open()
            reader = command.ExecuteReader()
            If reader.HasRows Then
                dtResults.Load(reader)
            End If
        Catch ex As Exception
            objutil.ExeNonQuery("exec uspErrLog 02, 'Report QC Done','" & ex.Message.ToString.Replace("'", "''") & "','ReportQCDoneByDate'")
        Finally
            mycon.Close()
        End Try
        Return dtResults
    End Function

    Public Shared Function GetQCDoneByRegion(ByVal userid As Int16, ByVal rgnid As Integer, ByVal docid As Integer) As DataTable
        Dim dtResults As New DataTable

        Dim command As New SqlCommand("uspGetQCDoneByRegion", mycon)
        command.CommandType = CommandType.StoredProcedure
        'command.CommandTimeout = 3000

        Dim prmUserId As New SqlParameter("@usrid", SqlDbType.BigInt)
        Dim prmRgnId As New SqlParameter("@rgnid", SqlDbType.Int)
        Dim prmDocid As New SqlParameter("@docid", SqlDbType.Int)

        prmUserId.Value = userid
        prmRgnId.Value = rgnid
        prmDocid.Value = docid

        command.Parameters.Add(prmUserId)
        command.Parameters.Add(prmRgnId)
        command.Parameters.Add(prmDocid)

        Try
            mycon.Open()
            reader = command.ExecuteReader()
            If reader.HasRows Then
                dtResults.Load(reader)
            End If
        Catch ex As Exception
            objutil.ExeNonQuery("exec uspErrLog 02, 'Report QC Done','" & ex.Message.ToString.Replace("'", "''") & "','ReportQCDoneByDate'")
        Finally
            mycon.Close()
        End Try
        Return dtResults
    End Function

    Public Shared Function GetQCDoneBySearch(ByVal userid As Int32, ByVal txtSearch As String, ByVal docid As Integer) As DataTable
        Dim dtResults As New DataTable

        Dim command As New SqlCommand("uspGetQCDoneBySearch", mycon)
        command.CommandType = CommandType.StoredProcedure
        'command.CommandTimeout = 3000

        Dim prmUserId As New SqlParameter("@usrid", SqlDbType.BigInt)
        Dim prmSearch As New SqlParameter("@txtSearch", SqlDbType.VarChar, 2000)
        Dim prmDocid As New SqlParameter("@docid", SqlDbType.Int)

        prmUserId.Value = userid
        prmSearch.Value = txtSearch
        prmDocid.Value = docid

        command.Parameters.Add(prmUserId)
        command.Parameters.Add(prmSearch)
        command.Parameters.Add(prmDocid)

        Try
            mycon.Open()
            reader = command.ExecuteReader()
            If reader.HasRows Then
                dtResults.Load(reader)
            End If
        Catch ex As Exception
            objutil.ExeNonQuery("exec uspErrLog 02, 'Report QC Done','" & ex.Message.ToString.Replace("'", "''") & "','ReportQCDoneByDate'")
        Finally
            mycon.Close()
        End Try
        Return dtResults
    End Function

    Public Shared Function GetDocDoneReportByDate(ByVal startdate As Nullable(Of DateTime), ByVal enddate As Nullable(Of DateTime), ByVal usrid As Integer, ByVal docid As Integer) As DataTable
        Dim dtResults As New DataTable
        Dim strErrMessage As String = String.Empty
        
        Dim command As New SqlCommand("HCPT_uspRPT_GetDocDone", mycon)
        command.CommandType = CommandType.StoredProcedure


        Dim prmDocID As New SqlParameter("@DocID", SqlDbType.Int)
        Dim prmStartDate As New SqlParameter("@StartTime", SqlDbType.DateTime)
        Dim prmEndDate As New SqlParameter("@EndTime", SqlDbType.DateTime)
        Dim prmUserID As New SqlParameter("@usrid", SqlDbType.Int)

        prmUserID.Value = usrid
        prmDocID.Value = docid
        prmStartDate.Value = startdate
        prmEndDate.Value = enddate

        command.Parameters.Add(prmUserID)
        command.Parameters.Add(prmDocID)
        command.Parameters.Add(prmStartDate)
        command.Parameters.Add(prmEndDate)

        Try
            mycon.Open()
            reader = command.ExecuteReader()
            dtResults.Load(reader)
        Catch ex As Exception
            strErrMessage = ex.Message.ToString()
        Finally
            mycon.Close()
        End Try

        If Not String.IsNullOrEmpty(strErrMessage) Then
            Dim hacontrol As New HCPTController
            hacontrol.ErrorLogInsert(150, "GetDOCDoneReportByDate", strErrMessage, "HCPT_uspRPT_GetDocDone")
        End If

        Return dtResults
    End Function

    Public Shared Function GetDocOnlineDoneByDate(ByVal docid As Integer, ByVal startdate As Nullable(Of DateTime), ByVal enddate As Nullable(Of DateTime), ByVal usrid As Integer) As DataTable
        Dim dtResults As New DataTable
        Dim strErrMessage As String = String.Empty

        Dim command As New SqlCommand("HCPT_uspGetDefaultOnlineFormDoneByDate", mycon)
        command.CommandType = CommandType.StoredProcedure


        Dim prmDocID As New SqlParameter("@docid", SqlDbType.Int)
        Dim prmStartDate As New SqlParameter("@startdate", SqlDbType.DateTime)
        Dim prmEndDate As New SqlParameter("@enddate", SqlDbType.DateTime)
        Dim prmUserID As New SqlParameter("@usrid", SqlDbType.Int)

        prmUserID.Value = usrid
        prmDocID.Value = docid
        prmStartDate.Value = startdate
        prmEndDate.Value = enddate

        command.Parameters.Add(prmUserID)
        command.Parameters.Add(prmDocID)
        command.Parameters.Add(prmStartDate)
        command.Parameters.Add(prmEndDate)

        Try
            mycon.Open()
            reader = command.ExecuteReader()
            dtResults.Load(reader)
        Catch ex As Exception
            strErrMessage = ex.Message.ToString()
        Finally
            mycon.Close()
        End Try

        If Not String.IsNullOrEmpty(strErrMessage) Then
            Dim hacontrol As New HCPTController
            hacontrol.ErrorLogInsert(150, "NSNCustomizeConfiguration:GetDocOnlineDoneByDate", strErrMessage, "HCPT_uspGetDefaultOnlineFormDoneByDate")
        End If

        Return dtResults
    End Function

#End Region

    Public Shared Function TaskPendingQCApproverCount(ByVal usrId As Integer) As Integer
        Dim tskPending As Integer = 0
        Dim command As New SqlCommand("uspDashBoardAgendaQC_task", mycon)
        command.CommandType = CommandType.StoredProcedure
        Dim prmUsrId As New SqlParameter("@usrid", SqlDbType.BigInt)
        Dim prmRowCount As New SqlParameter("@rowCount", SqlDbType.Int)
        prmUsrId.Value = usrId
        prmRowCount.Direction = ParameterDirection.Output
        command.Parameters.Add(prmUsrId)
        command.Parameters.Add(prmRowCount)

        Try
            mycon.Open()
            command.ExecuteNonQuery()
            tskPending = Convert.ToInt16(prmRowCount.Value)
        Catch ex As Exception
        Finally
            mycon.Close()
        End Try
        Return tskPending
    End Function

    Public Shared Sub InsertAuditApproveWithRemarks(ByVal sno As Int32, ByVal docid As Integer, ByVal task As Integer, ByVal rstatus As Integer, ByVal userid As Integer, ByVal roleid As Integer, ByVal wpid As String, ByVal remarks As String)

        Dim command As New SqlCommand("uspAuditTrailATPApprovedWithRemarks_New", mycon)
        command.CommandType = CommandType.StoredProcedure
        Dim prmSNO As New SqlParameter("@sno", SqlDbType.BigInt)
        Dim prmStrDocid As New SqlParameter("@strdocid", SqlDbType.VarChar, 50)
        Dim prmTask As New SqlParameter("@Task", SqlDbType.Int)
        Dim prmStatus As New SqlParameter("@Status", SqlDbType.Int)
        Dim prmUserid As New SqlParameter("@userid", SqlDbType.Int)
        Dim prmRoleid As New SqlParameter("@Roleid", SqlDbType.Int)
        Dim prmWPId As New SqlParameter("@wpid", SqlDbType.VarChar, 30)
        Dim prmRemarks As New SqlParameter("@remarks", SqlDbType.VarChar, 2000)

        prmSNO.Value = sno
        prmStrDocid.Value = Convert.ToString(docid)
        prmTask.Value = task
        prmStatus.Value = 1
        prmUserid.Value = userid
        prmRoleid.Value = roleid
        prmWPId.Value = wpid
        prmRemarks.Value = remarks

        command.Parameters.Add(prmSNO)
        command.Parameters.Add(prmStrDocid)
        command.Parameters.Add(prmTask)
        command.Parameters.Add(prmStatus)
        command.Parameters.Add(prmUserid)
        command.Parameters.Add(prmRoleid)
        command.Parameters.Add(prmWPId)
        command.Parameters.Add(prmRemarks)

        Try
            mycon.Open()
            command.ExecuteNonQuery()
        Catch ex As Exception
        Finally
            mycon.Close()
        End Try
    End Sub
End Class
Public Class ModernizationInfo
    Private _sno As Long
    Private _siteno As String
    Private _version As Integer
    Private _bbhValue As Double
    Private _bbsValue As Double

    Public Property Sno() As Long
        Get
            Return _sno
        End Get
        Set(ByVal value As Long)
            _sno = value
        End Set
    End Property

    Public Property SiteNo() As String
        Get
            Return _siteno
        End Get
        Set(ByVal value As String)
            _siteno = value
        End Set
    End Property

    Public Property Version() As Integer
        Get
            Return _version
        End Get
        Set(ByVal value As Integer)
            _version = value
        End Set
    End Property

    Public Property BBHValue() As Double
        Get
            Return _bbhValue
        End Get
        Set(ByVal value As Double)
            _bbhValue = value
        End Set
    End Property

    Public Property BBSValue() As Double
        Get
            Return _bbsValue
        End Get
        Set(ByVal value As Double)
            _bbsValue = value
        End Set
    End Property

End Class

Public Class UserProfile

    Private _userid As Integer
    Public Property UserId() As Integer
        Get
            Return _userid
        End Get
        Set(ByVal value As Integer)
            _userid = value
        End Set
    End Property


    Private _username As String
    Public Property Username() As String
        Get
            Return _username
        End Get
        Set(ByVal value As String)
            _username = value
        End Set
    End Property


    Private _fullname As String
    Public Property Fullname() As String
        Get
            Return _fullname
        End Get
        Set(ByVal value As String)
            _fullname = value
        End Set
    End Property


    Private _email As String
    Public Property Email() As String
        Get
            Return _email
        End Get
        Set(ByVal value As String)
            _email = value
        End Set
    End Property



    Private _roleid As Integer
    Public Property RoleId() As Integer
        Get
            Return _roleid
        End Get
        Set(ByVal value As Integer)
            _roleid = value
        End Set
    End Property


    Private _signTitle As String
    Public Property SignTitle() As String
        Get
            Return _signTitle
        End Get
        Set(ByVal value As String)
            _signTitle = value
        End Set
    End Property


    Private _usrType As String
    Public Property UserType() As String
        Get
            Return _usrType
        End Get
        Set(ByVal value As String)
            _usrType = value
        End Set
    End Property


    Private _companyname As String
    Public Property CompanyName() As String
        Get
            Return _companyname
        End Get
        Set(ByVal value As String)
            _companyname = value
        End Set
    End Property


End Class