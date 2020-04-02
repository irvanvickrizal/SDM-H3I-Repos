Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections.Generic

Public Class SOACController
    Inherits BaseController

#Region "Get Site Attribute"
    Public Function GetSiteDetail_WPID(ByVal packageid As String, ByVal userid As Integer) As SiteInfo
        Dim info As New SiteInfo
        Command = New SqlCommand("uspSOAC_GetSiteAttribute", Connection)
        Command.CommandType = CommandType.StoredProcedure
        Dim prmWPID As New SqlParameter("@wpid", SqlDbType.VarChar, 50)
        Dim prmUserId As New SqlParameter("@usrid", SqlDbType.Int)
        prmWPID.Value = packageid
        prmUserId.Value = userid
        Command.Parameters.Add(prmWPID)
        Command.Parameters.Add(prmUserId)
        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read
                    info.SiteNo = Convert.ToString(DataReader.Item("SiteNo"))
                    info.SiteName = Convert.ToString(DataReader.Item("SiteName"))
                    info.POName = Convert.ToString(DataReader.Item("POName"))
                    info.PONO = Convert.ToString(DataReader.Item("PONO"))
                    info.SiteIdPO = Convert.ToString(DataReader.Item("SiteIdPO"))
                    info.SiteNamePO = Convert.ToString(DataReader.Item("SiteNamePO"))
                    info.FLDType = Convert.ToString(DataReader.Item("FldType"))
                    info.Scope = Convert.ToString(DataReader.Item("Scope"))
                    info.ProjectID = Convert.ToString(DataReader.Item("TselProjectID"))
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

    Public Function GetPODate_OnAirDate(ByVal soacid As Int32) As ODSOACInfo
        Dim info As New ODSOACInfo
        Dim podate As System.Nullable(Of DateTime) = Nothing
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("uspSOAC_GetPODate_GetOnAirDate", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmSOACID As New SqlParameter("@soacid", SqlDbType.BigInt)
        prmSOACID.Value = soacid
        Command.Parameters.Add(prmSOACID)

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read
                    If Not String.IsNullOrEmpty(Convert.ToString(DataReader.Item("PODate"))) Then
                        info.PORefNoDate = Convert.ToDateTime(DataReader.Item("PODate"))
                    End If

                    If Not String.IsNullOrEmpty(Convert.ToString(DataReader.Item("ACT_9250"))) Then
                        info.OnAirDate = Convert.ToDateTime(DataReader.Item("ACT_9250"))
                    End If
                End While
            End If
        Catch ex As Exception
            strErrMessage = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try

        If Not String.IsNullOrEmpty(strErrMessage) Then
            ErrorLogInsert(20, "GetSOACPODate", strErrMessage, "uspSOAC_GetPODate")
        End If
        Return info
    End Function

    Public Function GetSOAC_PONO(ByVal userid As Integer) As List(Of SiteInfo)
        Dim list As New List(Of SiteInfo)
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("uspSOAC_GetPONo", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmUserId As New SqlParameter("@usrid", SqlDbType.Int)
        prmUserId.Value = userid
        Command.Parameters.Add(prmUserId)
        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read
                    Dim info As New SiteInfo
                    info.PONO = Convert.ToString(DataReader.Item("PoNo"))
                    list.Add(info)
                End While
            End If
        Catch ex As Exception
            strErrMessage = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try

        If Not String.IsNullOrEmpty(strErrMessage) Then
            ErrorLogInsert(20, "GetSOAC_PONO", strErrMessage, "uspSOAC_GetPONo")
        End If

        Return list
    End Function

    Public Function GetSOAC_SiteListCreatedBasePONO(ByVal pono As String, ByVal userid As Integer) As List(Of SiteInfo)
        Dim list As New List(Of SiteInfo)
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("uspSOAC_GetSiteCreated_BasePONO", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmUserId As New SqlParameter("@usrid", SqlDbType.Int)
        Dim prmPONO As New SqlParameter("@pono", SqlDbType.VarChar, 50)
        prmUserId.Value = userid
        prmPONO.Value = pono
        Command.Parameters.Add(prmUserId)
        Command.Parameters.Add(prmPONO)
        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read
                    Dim info As New SiteInfo
                    info.SiteNo = Convert.ToString(DataReader.Item("SiteNo")) & ", " & Convert.ToString(DataReader.Item("Sitename")) & ", " & Convert.ToString(DataReader.Item("Scope")) & ", " & Convert.ToString(DataReader.Item("workpackageid"))
                    info.PackageId = Convert.ToString(DataReader.Item("workpackageid"))
                    list.Add(info)
                End While
            End If
        Catch ex As Exception
            strErrMessage = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try

        If Not String.IsNullOrEmpty(strErrMessage) Then
            ErrorLogInsert(20, "GetSOAC_SiteCreatedBasePONO", strErrMessage, "uspSOAC_GetSiteCreated_BasePONO")
        End If

        Return list
    End Function
#End Region

#Region "SOAC Creation, Transaction, Deletion"
    Public Function ODSOAC_IU(ByVal info As ODSOACInfo, ByVal roleid As Integer) As Int32
        Dim soacid As Int32 = 0
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("uspSOAC_SOAC_IU", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmSiteOnAirDatedOn As New SqlParameter("@siteonairdateon", SqlDbType.VarChar, 200)
        Dim prmOnAirDate As New SqlParameter("@onairdate", SqlDbType.DateTime)
        Dim prmRolloutAgreement As New SqlParameter("@rolloutagreement", SqlDbType.VarChar, 200)
        Dim prmRolloutAgreementDate As New SqlParameter("@rolloutagreementdate", SqlDbType.DateTime)
        Dim prmPORefNo As New SqlParameter("@porefno", SqlDbType.VarChar, 200)
        Dim prmPORefNoDate As New SqlParameter("@porefnodate", SqlDbType.DateTime)
        Dim prmFinalCO As New SqlParameter("@finalco", SqlDbType.VarChar, 200)
        Dim prmFinalCODate As New SqlParameter("@finalcodate", SqlDbType.DateTime)
        Dim prmNoticeRefNo As New SqlParameter("@noticerefno", SqlDbType.VarChar, 200)
        Dim prmNoticeRefNoDate As New SqlParameter("@noticerefnodate", SqlDbType.DateTime)
        Dim prmWFID As New SqlParameter("@wfid", SqlDbType.Int)
        Dim prmLMBY As New SqlParameter("@lmby", SqlDbType.Int)
        Dim prmDocPath As New SqlParameter("@docpath", SqlDbType.VarChar, 2000)
        Dim prmOrgDocpath As New SqlParameter("@orgdocpath", SqlDbType.VarChar, 2000)
        Dim prmIsUpload As New SqlParameter("@isuploaded", SqlDbType.Bit)
        Dim prmIsRejected As New SqlParameter("@isrejected", SqlDbType.Bit)
        Dim prmRemarksofrejection As New SqlParameter("@remarksofrejection", SqlDbType.VarChar, 500)
        Dim prmRejectionUser As New SqlParameter("@rejectionuser", SqlDbType.Int)
        Dim prmGetSOACID As New SqlParameter("@getsoacid", SqlDbType.BigInt)
        Dim prmSOACID As New SqlParameter("@soacid", SqlDbType.BigInt)
        Dim prmRoleID As New SqlParameter("@roleid", SqlDbType.Int)

        prmSiteOnAirDatedOn.Value = info.SiteOnAirDate
        prmOnAirDate.Value = info.OnAirDate
        prmRoleID.Value = roleid
        prmRolloutAgreement.Value = info.RolloutAgreement
        prmRolloutAgreementDate.Value = info.RolloutAgreementDate
        prmPORefNo.Value = info.PORefNo
        prmPORefNoDate.Value = info.PORefNoDate
        prmFinalCO.Value = info.FinalCO
        prmFinalCODate.Value = info.FinalCODate
        prmNoticeRefNo.Value = info.NoticeRefNo
        prmNoticeRefNoDate.Value = info.NoticeRefNoDate
        prmWFID.Value = info.WFID
        prmLMBY.Value = info.LMBY
        prmDocPath.Value = info.DocPath
        prmOrgDocpath.Value = info.OrgDocPath
        prmIsRejected.Value = info.IsRejected
        prmIsUpload.Value = info.IsUploaded
        prmRejectionUser.Value = info.RejectionUser
        prmRemarksofrejection.Value = info.RemarksOfRejection
        prmSOACID.Value = info.SOACID
        prmGetSOACID.Direction = ParameterDirection.Output

        Command.Parameters.Add(prmSiteOnAirDatedOn)
        Command.Parameters.Add(prmOnAirDate)
        Command.Parameters.Add(prmRoleID)
        Command.Parameters.Add(prmRolloutAgreement)
        Command.Parameters.Add(prmRolloutAgreementDate)
        Command.Parameters.Add(prmPORefNo)
        Command.Parameters.Add(prmPORefNoDate)
        Command.Parameters.Add(prmFinalCO)
        Command.Parameters.Add(prmFinalCODate)
        Command.Parameters.Add(prmNoticeRefNo)
        Command.Parameters.Add(prmNoticeRefNoDate)
        Command.Parameters.Add(prmWFID)
        Command.Parameters.Add(prmLMBY)
        Command.Parameters.Add(prmDocPath)
        Command.Parameters.Add(prmOrgDocpath)
        Command.Parameters.Add(prmIsRejected)
        Command.Parameters.Add(prmIsUpload)
        Command.Parameters.Add(prmRejectionUser)
        Command.Parameters.Add(prmRemarksofrejection)
        Command.Parameters.Add(prmSOACID)
        Command.Parameters.Add(prmGetSOACID)

        Try
            Connection.Open()
            Command.ExecuteNonQuery()
            soacid = Convert.ToInt32(prmGetSOACID.Value)
        Catch ex As Exception
            soacid = 0
            strErrMessage = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try

        If Not String.IsNullOrEmpty(strErrMessage) Then
            ErrorLogInsert(20, "ODSOAC_IU", strErrMessage, "uspSOAC_SOAC_IU")
        End If
        Return soacid
    End Function

    Public Function GetSOACDetail(ByVal soacid As Int32) As ODSOACInfo
        Dim info As New ODSOACInfo
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("uspSOAC_GetSOAC", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmSOACID As New SqlParameter("@soacid", SqlDbType.BigInt)
        prmSOACID.Value = soacid
        Command.Parameters.Add(prmSOACID)

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read
                    info.SOACID = Convert.ToInt32(DataReader.Item("SOAC_ID"))
                    info.SiteOnAirDate = Convert.ToString(DataReader.Item("SiteOnAirDateOn"))
                    If Not String.IsNullOrEmpty(Convert.ToString(DataReader.Item("onair_date"))) Then
                        info.OnAirDate = Convert.ToDateTime(DataReader.Item("onair_date"))
                    End If
                    info.RolloutAgreement = Convert.ToString(DataReader.Item("rollout_agreement"))
                    If Not String.IsNullOrEmpty(Convert.ToString(DataReader.Item("rollout_agreement_date"))) Then
                        info.RolloutAgreementDate = Convert.ToDateTime(DataReader.Item("rollout_agreement_date"))
                    End If
                    info.PORefNo = Convert.ToString(DataReader.Item("PO_RefNo"))
                    If Not String.IsNullOrEmpty(Convert.ToString(DataReader.Item("PO_RefNo_date"))) Then
                        info.PORefNoDate = Convert.ToDateTime(DataReader.Item("PO_RefNo_date"))
                    End If

                    info.FinalCO = Convert.ToString(DataReader.Item("Final_CO"))
                    If Not String.IsNullOrEmpty(Convert.ToString(DataReader.Item("Final_CO_date"))) Then
                        info.FinalCODate = Convert.ToDateTime(DataReader.Item("Final_CO_Date"))
                    End If
                    info.NoticeRefNo = Convert.ToString(DataReader.Item("Notice_RefNo"))
                    If Not String.IsNullOrEmpty(Convert.ToString(DataReader.Item("Notice_RefNo_Date"))) Then
                        info.NoticeRefNoDate = Convert.ToDateTime(DataReader.Item("Notice_RefNo_Date"))
                    End If
                    If Not String.IsNullOrEmpty(Convert.ToString(DataReader.Item("WFID"))) Then
                        info.WFID = Integer.Parse(DataReader.Item("WFID").ToString())
                    Else
                        info.WFID = 0
                    End If

                    info.LMBY = Integer.Parse(DataReader.Item("LMBY").ToString())
                    info.LMDT = Convert.ToDateTime(DataReader.Item("LMDT"))
                    info.DocPath = Convert.ToString(DataReader.Item("docpath"))
                    info.OrgDocPath = Convert.ToString(DataReader.Item("orgdocpath"))
                    If Not String.IsNullOrEmpty(Convert.ToString(DataReader.Item("isUploaded"))) Then
                        info.IsUploaded = Convert.ToBoolean(DataReader.Item("isUploaded"))
                    Else
                        info.IsUploaded = False
                    End If


                    If Not String.IsNullOrEmpty(Convert.ToString(DataReader.Item("isRejected"))) Then
                        info.IsRejected = Convert.ToBoolean(DataReader.Item("isRejected"))
                    Else
                        info.IsRejected = False
                    End If

                    info.RemarksOfRejection = Convert.ToString(DataReader.Item("remarksofrejection"))
                    If Not String.IsNullOrEmpty(Convert.ToString(DataReader.Item("rejectionuser"))) Then
                        info.RejectionUserId = Integer.Parse(DataReader.Item("rejectionuser").ToString())
                    Else
                        info.RejectionUserId = 0
                    End If
                    info.ModifiedUser = Convert.ToString(DataReader.Item("modifieduser"))
                    info.RejectionUser = Convert.ToString(DataReader.Item("rejectionusername"))
                End While
            Else
                info = Nothing
            End If
        Catch ex As Exception
            strErrMessage = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try

        If Not String.IsNullOrEmpty(strErrMessage) Then
            ErrorLogInsert(20, "GetSOACDetail", strErrMessage, "uspSOAC_GetSOAC")
        End If

        Return info
    End Function

    Public Function GetSOACStatusDetail(ByVal soacid As Int32) As List(Of ODSOACInfo)
        Dim list As New List(Of ODSOACInfo)
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("uspSOAC_GetSOACStatusDetail", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmSOACID As New SqlParameter("@soacid", SqlDbType.BigInt)
        prmSOACID.Value = soacid
        Command.Parameters.Add(prmSOACID)

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read()
                    Dim info As New ODSOACInfo
                    info.SOACID = Convert.ToInt32(DataReader.Item("soac_id"))
                    If Not String.IsNullOrEmpty(Convert.ToString(DataReader.Item("OnAir_Date"))) Then
                        info.OnAirDate = Convert.ToDateTime(DataReader.Item("OnAir_Date"))
                    End If
                    If Not String.IsNullOrEmpty(Convert.ToString(DataReader.Item("PO_RefNo_Date"))) Then
                        info.PORefNoDate = Convert.ToDateTime(DataReader.Item("PO_RefNo_Date"))
                    End If
                    info.ModifiedUser = Convert.ToString(DataReader.Item("ModifiedUser"))
                    info.LMDT = Convert.ToDateTime(DataReader.Item("LMDT"))
                    info.PORefNo = Convert.ToString(DataReader.Item("PO_RefNo"))
                    info.SiteInf.SiteNo = Convert.ToString(DataReader.Item("SiteNo"))
                    info.SiteInf.SiteName = Convert.ToString(DataReader.Item("SiteName"))
                    info.SiteInf.Scope = Convert.ToString(DataReader.Item("Scope"))
                    info.SiteInf.PONO = Convert.ToString(DataReader.Item("PONO"))
                    info.SiteInf.PackageId = Convert.ToString(DataReader.Item("Workpackageid"))
                    info.SOACStatus = Convert.ToString(DataReader.Item("SOACStatus"))
                    list.Add(info)
                End While
            End If
        Catch ex As Exception
            strErrMessage = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try

        If Not String.IsNullOrEmpty(strErrMessage) Then
            ErrorLogInsert(20, "GetSOACStatusDetail", strErrMessage, "uspSOAC_GetSOACStatusDetail")
        End If

        Return list
    End Function

    Public Function ODSOACPreparation(ByVal lmby As Integer, ByVal roleid As Integer) As Int32
        Dim soacid As Int32 = 0
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("uspSOAC_SOAC_Prep", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmLMBY As New SqlParameter("@lmby", SqlDbType.Int)
        Dim prmRoleId As New SqlParameter("@roleid", SqlDbType.Int)
        Dim prmGetSOACID As New SqlParameter("@getsoacid", SqlDbType.BigInt)

        prmLMBY.Value = lmby
        prmRoleId.Value = roleid
        prmGetSOACID.Direction = ParameterDirection.Output

        Command.Parameters.Add(prmLMBY)
        Command.Parameters.Add(prmRoleId)
        Command.Parameters.Add(prmGetSOACID)

        Try
            Connection.Open()
            Command.ExecuteNonQuery()
            soacid = Convert.ToInt32(prmGetSOACID.Value)
        Catch ex As Exception
            strErrMessage = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try

        If soacid = 0 Then
            ErrorLogInsert(20, "ODSOACPreparation", strErrMessage, "uspSOAC_SOAC_Prep")
        End If

        Return soacid
    End Function

    Public Function ODSOAC_D(ByVal soacid As Int32) As Boolean
        Dim isSucceed As Boolean = True
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("delete ODSOAC where soac_id=" & Convert.ToString(soacid), Connection)
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
            ErrorLogInsert(20, "ODSOAC_D", strErrMessage, "CommandType.Text")
        End If
        Return isSucceed
    End Function

    Public Function Transaction_I(ByVal info As SOACTransactionInfo) As Boolean
        Dim isSucceed As Boolean = True
        Dim strErrMessage As String = String.Empty

        Command = New SqlCommand("uspSOAC_Transaction_I", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmDOCID As New SqlParameter("@docid", SqlDbType.Int)
        Dim prmSOACID As New SqlParameter("@soacid", SqlDbType.BigInt)
        Dim prmWFID As New SqlParameter("@wfid", SqlDbType.Int)
        Dim prmTSKID As New SqlParameter("@tskid", SqlDbType.Int)
        Dim prmRoleID As New SqlParameter("@roleid", SqlDbType.Int)
        Dim prmStartDateTime As New SqlParameter("@startdatetime", SqlDbType.DateTime)
        Dim prmEndDateTime As New SqlParameter("@enddatetime", SqlDbType.DateTime)
        Dim prmStatus As New SqlParameter("@status", SqlDbType.Int)
        Dim prmRStatus As New SqlParameter("@rstatus", SqlDbType.Int)
        Dim prmLMBY As New SqlParameter("@lmby", SqlDbType.Int)
        Dim prmLMDT As New SqlParameter("@lmdt", SqlDbType.DateTime)
        Dim prmXVal As New SqlParameter("@xval", SqlDbType.Int)
        Dim prmYVal As New SqlParameter("@yval", SqlDbType.Int)
        Dim prmUGPID As New SqlParameter("@ugpid", SqlDbType.Int)
        Dim prmPageNo As New SqlParameter("@pageno", SqlDbType.Int)

        prmDOCID.Value = info.DocInf.DocId
        prmSOACID.Value = info.SOACInfo.SOACID
        prmWFID.Value = info.SOACInfo.WFID
        prmTSKID.Value = info.TaskId
        prmRoleID.Value = info.RoleInf.RoleId
        prmStartDateTime.Value = info.StartDateTime
        prmEndDateTime.Value = info.EndDateTime
        prmRStatus.Value = info.RStatus
        prmStatus.Value = info.Status
        prmLMBY.Value = info.CMAInfo.LMBY
        prmLMDT.Value = info.CMAInfo.LMDT
        prmXVal.Value = info.Xval
        prmYVal.Value = info.Yval
        prmUGPID.Value = info.UGPID
        prmPageNo.Value = info.PageNo

        Command.Parameters.Add(prmDOCID)
        Command.Parameters.Add(prmSOACID)
        Command.Parameters.Add(prmWFID)
        Command.Parameters.Add(prmTSKID)
        Command.Parameters.Add(prmRoleID)
        Command.Parameters.Add(prmStartDateTime)
        Command.Parameters.Add(prmEndDateTime)
        Command.Parameters.Add(prmStatus)
        Command.Parameters.Add(prmRStatus)
        Command.Parameters.Add(prmLMBY)
        Command.Parameters.Add(prmLMDT)
        Command.Parameters.Add(prmXVal)
        Command.Parameters.Add(prmYVal)
        Command.Parameters.Add(prmUGPID)
        Command.Parameters.Add(prmPageNo)

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
            ErrorLogInsert(20, "Transaction_I", strErrMessage, "uspSOAC_Transaction_I")
        End If

        Return isSucceed
    End Function

    Public Sub Transaction_D(ByVal docid As Integer, ByVal soacid As Int32)
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("uspSOAC_Transaction_D", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmSOACID As New SqlParameter("@soacid", SqlDbType.BigInt)
        Dim prmDOCID As New SqlParameter("@docid", SqlDbType.Int)
        prmSOACID.Value = soacid
        prmDOCID.Value = docid
        Command.Parameters.Add(prmSOACID)
        Command.Parameters.Add(prmDOCID)

        Try
            Connection.Open()
            Command.ExecuteNonQuery()
        Catch ex As Exception
            strErrMessage = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try

        If Not String.IsNullOrEmpty(strErrMessage) Then
            ErrorLogInsert(20, "Transaction_D", strErrMessage, "uspSOAC_Transaction_D")
        End If
    End Sub

    Public Sub UpdateChildDocTransaction(ByVal docid As Integer, ByVal soacid As Int32, ByVal rstatus As Integer)
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("uspSOAC_UpdateChildDocStatusTransaction", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmDOCID As New SqlParameter("@docid", SqlDbType.Int)
        Dim prmSOACID As New SqlParameter("@soacid", SqlDbType.BigInt)
        Dim prmRStatus As New SqlParameter("@rstatus", SqlDbType.Int)

        prmDOCID.Value = docid
        prmSOACID.Value = soacid
        prmRStatus.Value = rstatus

        Command.Parameters.Add(prmDOCID)
        Command.Parameters.Add(prmSOACID)
        Command.Parameters.Add(prmRStatus)

        Try
            Connection.Open()
            Command.ExecuteNonQuery()
        Catch ex As Exception
            strErrMessage = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try

        If Not String.IsNullOrEmpty(strErrMessage) Then
            ErrorLogInsert(20, "UpdateChildDocTransaction", strErrMessage, "uspSOAC_UpdateChildDocStatusTransaction")
        End If
    End Sub

    Public Function GetSOACTransaction(ByVal userid As Integer) As List(Of SOACTransactionInfo)
        Dim list As New List(Of SOACTransactionInfo)
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("uspSOAC_GetTaskPending", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmUserID As New SqlParameter("@usrid", SqlDbType.Int)
        prmUserID.Value = userid
        Command.Parameters.Add(prmUserID)

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read()
                    Dim info As New SOACTransactionInfo
                    info.SOACInfo.SOACID = Convert.ToInt32(DataReader.Item("SOAC_ID"))
                    If Not String.IsNullOrEmpty(Convert.ToString(DataReader.Item("PODate"))) Then
                        info.SOACInfo.PORefNoDate = Convert.ToDateTime(DataReader.Item("PODate"))
                    End If

                    If Not String.IsNullOrEmpty(Convert.ToString(DataReader.Item("OnAirDate"))) Then
                        info.SOACInfo.OnAirDate = Convert.ToDateTime(DataReader.Item("OnAirDate"))
                    End If

                    info.SOACInfo.SiteInf.PONO = Convert.ToString(DataReader.Item("PONO"))
                    info.SOACInfo.SiteInf.POName = Convert.ToString(DataReader.Item("POName"))
                    info.SOACInfo.SiteInf.SiteNo = Convert.ToString(DataReader.Item("SiteNo"))
                    info.SOACInfo.SiteInf.SiteName = Convert.ToString(DataReader.Item("SiteName"))
                    info.SOACInfo.SiteInf.SiteIdPO = Convert.ToString(DataReader.Item("SiteIdPO"))
                    info.SOACInfo.SiteInf.SiteNamePO = Convert.ToString(DataReader.Item("SiteNamePO"))
                    info.SOACInfo.SiteInf.Scope = Convert.ToString(DataReader.Item("Scope"))
                    info.SOACInfo.SiteInf.AreaName = Convert.ToString(DataReader.Item("Areaname"))
                    info.SOACInfo.SiteInf.RegionName = Convert.ToString(DataReader.Item("RgnName"))
                    info.SOACInfo.SiteInf.PackageId = Convert.ToString(DataReader.Item("Workpackageid"))
                    If Not String.IsNullOrEmpty(Convert.ToString(DataReader.Item("UploadDate"))) Then
                        info.SOACMSInfo.UploadDate = Convert.ToDateTime(DataReader.Item("UploadDate"))
                    End If
                    If Not String.IsNullOrEmpty(Convert.ToString(DataReader.Item("SubmitDate"))) Then
                        info.SOACMSInfo.SubmitDate = Convert.ToDateTime(DataReader.Item("SubmitDate"))
                    End If
                    'info.SOACMSInfo.UploadDate = 
                    list.Add(info)
                End While
            End If
        Catch ex As Exception
            strErrMessage = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try

        If Not String.IsNullOrEmpty(strErrMessage) Then
            ErrorLogInsert(20, "GetSOACTransaction", strErrMessage, "uspSOAC_GetTaskPending")
        End If

        Return list
    End Function

    Public Function GetSOACTransactionDetail(ByVal sno As Int32) As SOACTransactionInfo
        Dim info As New SOACTransactionInfo
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("uspSOAC_GetTransactionDetail", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmSNO As New SqlParameter("@sno", SqlDbType.BigInt)
        prmSNO.Value = sno
        Command.Parameters.Add(prmSNO)

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read()
                    info.SNO = Convert.ToInt32(DataReader.Item("SNO"))
                    info.DocInf.DocId = Integer.Parse(DataReader.Item("doc_Id").ToString())
                    info.SOACInfo.SOACID = Convert.ToInt32(DataReader.Item("SOAC_ID"))
                    info.RoleInf.RoleId = Integer.Parse(DataReader.Item("Role_Id").ToString())
                    info.TaskId = Integer.Parse(DataReader.Item("Tsk_Id").ToString())
                    info.WFID = Integer.Parse(DataReader.Item("WF_ID").ToString())
                    If Not String.IsNullOrEmpty(Convert.ToString(DataReader.Item("StartDateTime"))) Then
                        info.StartDateTime = Convert.ToDateTime(DataReader.Item("StartDateTime"))
                    End If
                    If Not String.IsNullOrEmpty(Convert.ToString(DataReader.Item("EndDateTime"))) Then
                        info.EndDateTime = Convert.ToDateTime(DataReader.Item("EndDateTime"))
                    End If
                    info.Status = Integer.Parse(DataReader.Item("Status").ToString())
                    info.RStatus = Integer.Parse(DataReader.Item("RStatus").ToString())
                    info.CMAInfo.LMBY = Integer.Parse(DataReader.Item("LMBY").ToString())
                    If Not String.IsNullOrEmpty(Convert.ToString(DataReader.Item("LMDT"))) Then
                        info.CMAInfo.LMDT = Convert.ToDateTime(DataReader.Item("LMDT"))
                    End If
                    info.Xval = Integer.Parse(DataReader.Item("xval").ToString())
                    info.Yval = Integer.Parse(DataReader.Item("yval").ToString())
                    info.UGPID = Integer.Parse(DataReader.Item("UGP_ID").ToString())
                    info.PageNo = Integer.Parse(DataReader.Item("Page_No").ToString())
                End While
            End If
        Catch ex As Exception
            strErrMessage = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try

        If Not String.IsNullOrEmpty(strErrMessage) Then
            ErrorLogInsert(20, "GetSOACTransactionDetail", strErrMessage, "uspSOAC_GetTransactionDetail")
        End If

        Return info
    End Function

    Public Function GetSOACLastTransactionReviewed(ByVal soacid As Int32, ByVal docid As Integer, ByVal ugpid As Integer, ByVal taskdesc As String) As List(Of SOACTransactionInfo)
        Dim list As New List(Of SOACTransactionInfo)
        Command = New SqlCommand("uspSOAC_GetLastReviewBaseOnTask", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmSOACID As New SqlParameter("@soacid", SqlDbType.BigInt)
        Dim prmDocID As New SqlParameter("@docid", SqlDbType.Int)
        Dim prmTaskDesc As New SqlParameter("@taskdesc", SqlDbType.VarChar, 50)
        Dim prmUPGID As New SqlParameter("@ugpid", SqlDbType.Int)

        prmSOACID.Value = soacid
        prmDocID.Value = docid
        prmTaskDesc.Value = taskdesc
        prmUPGID.Value = ugpid

        Command.Parameters.Add(prmSOACID)
        Command.Parameters.Add(prmDocID)
        Command.Parameters.Add(prmTaskDesc)
        Command.Parameters.Add(prmUPGID)

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read
                    Dim info As New SOACTransactionInfo
                    info.CMAInfo.LMBY = Integer.Parse(DataReader.Item("lmby").ToString())
                    info.CMAInfo.LMDT = Convert.ToDateTime(DataReader.Item("lmdt"))
                    info.CMAInfo.ModifiedUser = Convert.ToString(DataReader.Item("name"))
                    info.RoleInf.RoleName = Convert.ToString(DataReader.Item("SignTitle"))
                    list.Add(info)
                End While
            End If
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try
        Return list
    End Function

    Public Function DocApproved(ByVal info As SOACTransactionInfo) As Boolean
        Dim isSucceed As Boolean = True
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("uspSOAC_DocApproved", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmSNO As New SqlParameter("@sno", SqlDbType.BigInt)
        Dim prmSOACID As New SqlParameter("@soacid", SqlDbType.BigInt)
        Dim prmWFID As New SqlParameter("@wfid", SqlDbType.Int)
        Dim prmTSKID As New SqlParameter("@tskid", SqlDbType.Int)
        Dim prmRoleID As New SqlParameter("@roleid", SqlDbType.Int)
        Dim prmUserID As New SqlParameter("@userid", SqlDbType.Int)
        Dim prmDOCID As New SqlParameter("@docid", SqlDbType.Int)

        prmSNO.Value = info.SNO
        prmSOACID.Value = info.SOACInfo.SOACID
        prmWFID.Value = info.WFID
        prmTSKID.Value = info.TaskId
        prmRoleID.Value = info.RoleInf.RoleId
        prmUserID.Value = info.CMAInfo.LMBY
        prmDOCID.Value = info.DocInf.DocId

        Command.Parameters.Add(prmSNO)
        Command.Parameters.Add(prmSOACID)
        Command.Parameters.Add(prmWFID)
        Command.Parameters.Add(prmTSKID)
        Command.Parameters.Add(prmRoleID)
        Command.Parameters.Add(prmUserID)
        Command.Parameters.Add(prmDOCID)

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
            ErrorLogInsert(20, "SOAC_DocApproved", strErrMessage, "uspSOAC_DocApproved")
        End If

        Return isSucceed
    End Function

    Public Function DocRejected(ByVal info As SOACTransactionInfo) As Boolean
        Dim isSucceed As Boolean = True
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("uspSOAC_DocRejected", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmSOACID As New SqlParameter("@soacid", SqlDbType.Int)
        Dim prmUserID As New SqlParameter("@usrid", SqlDbType.Int)
        Dim prmRoleID As New SqlParameter("@roleid", SqlDbType.Int)
        Dim prmRemarks As New SqlParameter("@remarks", SqlDbType.VarChar, 2000)

        prmSOACID.Value = info.SOACInfo.SOACID
        prmUserID.Value = info.SOACInfo.LMBY
        prmRoleID.Value = info.RoleInf.RoleId
        prmRemarks.Value = info.SOACInfo.RemarksOfRejection

        Command.Parameters.Add(prmSOACID)
        Command.Parameters.Add(prmUserID)
        Command.Parameters.Add(prmRoleID)
        Command.Parameters.Add(prmRemarks)

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
            ErrorLogInsert(20, "DocRejected", strErrMessage, "uspSOAC_DocRejected")
        End If

        Return isSucceed
    End Function

    Public Function GetSOACDone(ByVal userid As Integer, ByVal startdatetime As System.Nullable(Of DateTime), ByVal enddatetime As System.Nullable(Of DateTime)) As List(Of ODSOACMilestoneInfo)
        Dim list As New List(Of ODSOACMilestoneInfo)
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("uspSOAC_GetSOACDone", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmStartDateTime As New SqlParameter("@startdatetime", SqlDbType.DateTime)
        Dim prmEndDateTime As New SqlParameter("@enddatetime", SqlDbType.DateTime)
        Dim prmUserId As New SqlParameter("@usrid", SqlDbType.Int)

        prmStartDateTime.Value = startdatetime
        prmEndDateTime.Value = enddatetime
        prmUserId.Value = userid


        Command.Parameters.Add(prmStartDateTime)
        Command.Parameters.Add(prmEndDateTime)
        Command.Parameters.Add(prmUserId)

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read
                    Dim info As New ODSOACMilestoneInfo
                    info.SOACInfo.SOACID = Convert.ToInt32(DataReader.Item("SOAC_ID"))
                    info.SOACInfo.SiteInf.SiteNo = Convert.ToString(DataReader.Item("SiteNo"))
                    info.SOACInfo.SiteInf.SiteName = Convert.ToString(DataReader.Item("SiteName"))
                    info.SOACInfo.SiteInf.PONO = Convert.ToString(DataReader.Item("PONO"))
                    info.SOACInfo.SiteInf.PackageId = Convert.ToString(DataReader.Item("workpackageid"))
                    info.SOACInfo.SiteInf.POName = Convert.ToString(DataReader.Item("POName"))
                    If Not String.IsNullOrEmpty(Convert.ToString(DataReader.Item("Upload_Date"))) Then
                        info.UploadDate = Convert.ToDateTime(DataReader.Item("Upload_Date"))
                    End If
                    If Not String.IsNullOrEmpty(Convert.ToString(DataReader.Item("Submit_Date"))) Then
                        info.SubmitDate = Convert.ToDateTime(DataReader.Item("Submit_Date"))
                    End If
                    If Not String.IsNullOrEmpty(Convert.ToString(DataReader.Item("Approved_Date"))) Then
                        info.ApprovedDate = Convert.ToDateTime(DataReader.Item("Approved_Date"))
                    End If
                    info.SOACInfo.SiteInf.RegionName = Convert.ToString(DataReader.Item("rgnname"))
                    info.SOACInfo.SiteInf.AreaName = Convert.ToString(DataReader.Item("areaname"))
                    info.SOACInfo.CreatedUser = Convert.ToString(DataReader.Item("nsnapprover"))
                    info.SOACInfo.ModifiedUser = Convert.ToString(DataReader.Item("tselapprover"))
                    list.Add(info)
                End While
            End If
        Catch ex As Exception
            strErrMessage = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try

        If Not String.IsNullOrEmpty(strErrMessage) Then
            ErrorLogInsert(10, "GetSOACDone", strErrMessage, "uspSOAC_GetSOACDone")
        End If

        Return list
    End Function

    Public Function GetSOACReadyCreation(ByVal userid As Integer, ByVal notificationtype As String, ByVal searchid As Integer, ByVal search As String) As List(Of ODSOACInfo)
        Dim list As New List(Of ODSOACInfo)
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("uspSOAC_SOACReadyCreation", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmUserID As New SqlParameter("@usrid", SqlDbType.Int)
        Dim prmNotificationType As New SqlParameter("@notificationtype", SqlDbType.VarChar, 200)
        Dim prmSearchId As New SqlParameter("@searchid", SqlDbType.Int)
        Dim prmSearch As New SqlParameter("@search", SqlDbType.VarChar, 200)


        prmUserID.Value = userid
        prmNotificationType.Value = notificationtype
        prmSearchId.Value = searchid
        prmSearch.Value = search

        Command.Parameters.Add(prmUserID)
        Command.Parameters.Add(prmNotificationType)
        Command.Parameters.Add(prmSearch)
        Command.Parameters.Add(prmSearchId)

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read
                    Dim info As New ODSOACInfo
                    info.SiteInf.SiteNo = Convert.ToString(DataReader.Item("SiteNo"))
                    info.SiteInf.PONO = Convert.ToString(DataReader.Item("PoNo"))
                    info.SiteInf.SiteName = Convert.ToString(DataReader.Item("SiteName"))
                    info.SiteInf.SiteIdPO = Convert.ToString(DataReader.Item("SiteIDPO"))
                    info.SiteInf.SiteNamePO = Convert.ToString(DataReader.Item("SiteNamePO"))
                    info.SiteInf.PackageId = Convert.ToString(DataReader.Item("workpkgid"))
                    info.SiteInf.RegionName = Convert.ToString(DataReader.Item("rgnname"))
                    info.SiteInf.AreaName = Convert.ToString(DataReader.Item("ara_name"))
                    info.DelayDay = Integer.Parse(DataReader.Item("delays").ToString())
                    If Not String.IsNullOrEmpty(Convert.ToString(DataReader.Item("PODate"))) Then
                        info.SiteInf.PODate = Convert.ToDateTime(DataReader.Item("PODate"))
                    End If
                    If Not String.IsNullOrEmpty(Convert.ToString(DataReader.Item("OnAirDate"))) Then
                        info.OnAirDate = Convert.ToDateTime(DataReader.Item("OnAirDate"))
                    End If

                    list.Add(info)
                End While
            End If
        Catch ex As Exception
            strErrMessage = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try

        If Not String.IsNullOrEmpty(strErrMessage) Then
            ErrorLogInsert(20, "GetSOACReadyCreationByPIC", strErrMessage, "uspGeneral_SOACReadyCreation")
        End If
        Return list
    End Function

    Public Function GetSOACRejection(ByVal userid As Integer) As List(Of ODSOACInfo)
        Dim list As New List(Of ODSOACInfo)
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("uspSOAC_SOACRejection", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmUserID As New SqlParameter("@usrid", SqlDbType.Int)
        prmUserID.Value = userid
        Command.Parameters.Add(prmUserID)

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read
                    Dim info As New ODSOACInfo
                    info.SiteInf.SiteNo = Convert.ToString(DataReader.Item("SiteNo"))
                    info.SiteInf.PONO = Convert.ToString(DataReader.Item("PoNo"))
                    info.SiteInf.SiteName = Convert.ToString(DataReader.Item("SiteName"))
                    info.SiteInf.SiteIdPO = Convert.ToString(DataReader.Item("SiteIDPO"))
                    info.SiteInf.SiteNamePO = Convert.ToString(DataReader.Item("SiteNamePO"))
                    info.SiteInf.PackageId = Convert.ToString(DataReader.Item("workpkgid"))
                    info.SiteInf.RegionName = Convert.ToString(DataReader.Item("rgnname"))
                    info.SiteInf.AreaName = Convert.ToString(DataReader.Item("ara_name"))
                    info.DelayDay = Integer.Parse(DataReader.Item("delays").ToString())
                    If Not String.IsNullOrEmpty(Convert.ToString(DataReader.Item("PODate"))) Then
                        info.SiteInf.PODate = Convert.ToDateTime(DataReader.Item("PODate"))
                    End If
                    If Not String.IsNullOrEmpty(Convert.ToString(DataReader.Item("OnAirDate"))) Then
                        info.OnAirDate = Convert.ToDateTime(DataReader.Item("OnAirDate"))
                    End If
                    info.RemarksOfRejection = Convert.ToString(DataReader.Item("RemarksofRejection"))
                    info.RejectionUser = Convert.ToString(DataReader.Item("RejectionName"))
                    If Not String.IsNullOrEmpty(DataReader.Item("RejectionDate")) Then
                        info.RejectionDate = Convert.ToDateTime(DataReader.Item("RejectionDate"))
                    End If
                    list.Add(info)
                End While
            End If
        Catch ex As Exception
            strErrMessage = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try

        If Not String.IsNullOrEmpty(strErrMessage) Then
            ErrorLogInsert(20, "GetSOACRejection", strErrMessage, "uspSOAC_SOACRejection")
        End If

        Return list
    End Function

#End Region

#Region "SOAC Value"
    Public Function GetSOACValue_DS(ByVal soacid As Int32) As DataSet
        Dim ds As New DataSet
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("uspSOAC_GetValue", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmSOACID As New SqlParameter("@soacid", SqlDbType.BigInt)
        prmSOACID.Value = soacid
        Command.Parameters.Add(prmSOACID)

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
            ErrorLogInsert(20, "SOACGetValue_DS", strErrMessage, "uspSOAC_GetValue")
        End If

        Return ds
    End Function

    Public Function GetSOACValue(ByVal soacid As Int32) As List(Of ODSOACValueInfo)
        Dim list As New List(Of ODSOACValueInfo)
        Command = New SqlCommand("uspSOAC_GetValue", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmSOACID As New SqlParameter("@soacid", SqlDbType.BigInt)
        prmSOACID.Value = soacid
        Command.Parameters.Add(prmSOACID)

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read
                    Dim info As New ODSOACValueInfo
                    info.SOACID = Convert.ToInt32(DataReader.Item("SOAC_ID"))
                    info.PONO = Convert.ToString(DataReader.Item("PO_No"))
                    info.DescInfo.ValueDescription = Convert.ToString(DataReader.Item("Description"))
                    info.POUSD = Convert.ToDouble(DataReader.Item("PO_USD"))
                    info.POIDR = Convert.ToDouble(DataReader.Item("PO_IDR"))
                    info.ImplUSD = Convert.ToDouble(DataReader.Item("Impl_USD"))
                    info.ImplIDR = Convert.ToDouble(DataReader.Item("Impl_IDR"))
                    info.BASTUSD = Convert.ToDouble(DataReader.Item("Bast_USD"))
                    info.BASTIDR = Convert.ToDouble(DataReader.Item("Bast_IDR"))
                    info.DeltaUSD = Convert.ToDouble(DataReader.Item("Delta_USD"))
                    info.DeltaIDR = Convert.ToDouble(DataReader.Item("Delta_IDR"))
                    list.Add(info)
                End While
            End If
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try

        Return list
    End Function

    Public Function GetSOACValueTotal(ByVal soacid As Int32) As ODSOACValueInfo
        Dim info As New ODSOACValueInfo
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("uspSOAC_GetValueTotal", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmSOACID As New SqlParameter("@soacid", SqlDbType.BigInt)
        prmSOACID.Value = soacid
        Command.Parameters.Add(prmSOACID)

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read
                    info.BastIDRTotal = Convert.ToDouble(DataReader.Item("BASTIDRTotal"))
                    info.BastUSDTotal = Convert.ToDouble(DataReader.Item("BASTUSDTotal"))
                    info.POIDRTotal = Convert.ToDouble(DataReader.Item("POIDRTotal"))
                    info.POUSDTotal = Convert.ToDouble(DataReader.Item("POUSDTotal"))
                    info.ImpIDRTotal = Convert.ToDouble(DataReader.Item("ImpIDRTotal"))
                    info.ImpUSDTotal = Convert.ToDouble(DataReader.Item("ImpUSDTotal"))
                    info.DeltaIDRTotal = Convert.ToDouble(DataReader.Item("DeltaIDRTotal"))
                    info.DeltaUSDTotal = Convert.ToDouble(DataReader.Item("DeltaUSDTotal"))
                End While
            Else
                info = Nothing
            End If
        Catch ex As Exception
            strErrMessage = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try

        If Not String.IsNullOrEmpty(strErrMessage) Then
            ErrorLogInsert(20, "GetSOACValueTotal", strErrMessage, "uspSOAC_GetValueTotal")
        End If

        Return info
    End Function

    Public Function ODSOACValue_IU(ByVal info As ODSOACValueInfo) As Boolean
        Dim isSucceed As Boolean = True
        Dim strErrMessage As String = String.Empty

        Command = New SqlCommand("uspSOAC_Value_IU", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmPONO As New SqlParameter("@pono", SqlDbType.VarChar, 50)
        Dim prmDesc As New SqlParameter("@desc", SqlDbType.VarChar, 100)
        Dim prmPOEuro As New SqlParameter("@poeuro", SqlDbType.Decimal)
        Dim prmPOUSD As New SqlParameter("@pousd", SqlDbType.Decimal)
        Dim prmPOIDR As New SqlParameter("@poidr", SqlDbType.Decimal)
        Dim prmImpEuro As New SqlParameter("@impeuro", SqlDbType.Decimal)
        Dim prmImpUSD As New SqlParameter("@impusd", SqlDbType.Decimal)
        Dim prmImpIDR As New SqlParameter("@impidr", SqlDbType.Decimal)
        Dim prmBastEuro As New SqlParameter("@basteuro", SqlDbType.Decimal)
        Dim prmBastUSD As New SqlParameter("@bastusd", SqlDbType.Decimal)
        Dim prmBastIDR As New SqlParameter("@bastidr", SqlDbType.Decimal)
        Dim prmDeltaEuro As New SqlParameter("@deltaeuro", SqlDbType.Decimal)
        Dim prmDeltaUSD As New SqlParameter("@deltausd", SqlDbType.Decimal)
        Dim prmDeltaIDR As New SqlParameter("@deltaidr", SqlDbType.Decimal)
        Dim prmSOACID As New SqlParameter("@soacid", SqlDbType.BigInt)
        Dim prmValueID As New SqlParameter("@valueid", SqlDbType.BigInt)
        Dim prmLMBY As New SqlParameter("@lmby", SqlDbType.Int)

        prmPONO.Value = info.PONO
        prmDesc.Value = info.ValueDesc
        prmPOEuro.Value = info.POEuro
        prmPOUSD.Value = info.POUSD
        prmPOIDR.Value = info.POIDR
        prmImpEuro.Value = info.ImplEuro
        prmImpIDR.Value = info.ImplIDR
        prmImpUSD.Value = info.ImplUSD
        prmBastEuro.Value = info.BASTEuro
        prmBastIDR.Value = info.BASTIDR
        prmBastUSD.Value = info.BASTUSD
        prmDeltaEuro.Value = info.DeltaEURO
        prmDeltaIDR.Value = info.DeltaIDR
        prmDeltaUSD.Value = info.DeltaUSD
        prmSOACID.Value = info.SOACID
        prmValueID.Value = info.ValueId
        prmLMBY.Value = info.LMBY

        Command.Parameters.Add(prmBastEuro)
        Command.Parameters.Add(prmBastIDR)
        Command.Parameters.Add(prmBastUSD)
        Command.Parameters.Add(prmDeltaEuro)
        Command.Parameters.Add(prmDeltaIDR)
        Command.Parameters.Add(prmDeltaUSD)
        Command.Parameters.Add(prmDesc)
        Command.Parameters.Add(prmImpEuro)
        Command.Parameters.Add(prmImpIDR)
        Command.Parameters.Add(prmImpUSD)
        Command.Parameters.Add(prmLMBY)
        Command.Parameters.Add(prmPOEuro)
        Command.Parameters.Add(prmPOIDR)
        Command.Parameters.Add(prmPOUSD)
        Command.Parameters.Add(prmPONO)
        Command.Parameters.Add(prmSOACID)
        Command.Parameters.Add(prmValueID)

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
            ErrorLogInsert(20, "ODSOACValue_IU", strErrMessage, "uspSOAC_Value_IU")
        End If

        Return isSucceed
    End Function

    Public Function ODSOACValue_D(ByVal valueid As Int32) As Boolean
        Dim isSucceed As Boolean = True
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("uspSOAC_SOACValue_D", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmValueId As New SqlParameter("@valueid", SqlDbType.BigInt)
        prmValueId.Value = valueid
        Command.Parameters.Add(prmValueId)

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
            ErrorLogInsert(20, "ODSOACValue_D", strErrMessage, "uspSOAC_SOACValue_D")
        End If

        Return isSucceed
    End Function

    Public Function GetODSOACValueDescription(ByVal soacid As Int32) As List(Of ODSOACValueDescriptionInfo)
        Dim list As New List(Of ODSOACValueDescriptionInfo)
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("uspSOAC_GetValueDescription", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmSOACID As New SqlParameter("@soacid", SqlDbType.BigInt)
        prmSOACID.Value = soacid
        Command.Parameters.Add(prmSOACID)

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read
                    Dim info As New ODSOACValueDescriptionInfo
                    info.DescriptionId = Integer.Parse(DataReader.Item("desc_id").ToString())
                    info.ValueDescription = Convert.ToString(DataReader.Item("SOAC_Value_desc"))
                    list.Add(info)
                End While
            Else
                list = Nothing
            End If
        Catch ex As Exception
            strErrMessage = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try

        If Not String.IsNullOrEmpty(strErrMessage) Then
            ErrorLogInsert(20, "GetODSOACValueDescription", strErrMessage, "uspSOAC_GetValueDescription")
        End If

        Return list
    End Function

#End Region

#Region "SOAC WPID Creation,Recorded, Deletion"
    Public Function ODSOACWPIDGroup_I(ByVal wpid As String, ByVal soacid As Int32, ByVal lmby As Integer) As Boolean
        Dim isSucceed As Boolean = True
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("uspSOAC_SOACWPID_I", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmWPID As New SqlParameter("@wpid", SqlDbType.VarChar, 20)
        Dim prmLMBY As New SqlParameter("@lmby", SqlDbType.Int)
        Dim prmSOACID As New SqlParameter("@SOACID", SqlDbType.BigInt)

        prmWPID.Value = wpid
        prmLMBY.Value = lmby
        prmSOACID.Value = soacid

        Command.Parameters.Add(prmWPID)
        Command.Parameters.Add(prmLMBY)
        Command.Parameters.Add(prmSOACID)

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
            ErrorLogInsert(20, "ODSOACWPIDGroup_I", strErrMessage, "uspSOAC_SOACWPID_I")
        End If

        Return isSucceed
    End Function

    Public Function CheckingWPIDNYCreated(ByVal wpid As String) As Boolean
        Dim NYCreated As Boolean = True
        Dim strErrMessage As String = String.Empty

        Command = New SqlCommand("uspSOAC_IsWPIDNotYetCreated", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmWPID As New SqlParameter("@wpid", SqlDbType.VarChar, 20)
        Dim prmNYCreated As New SqlParameter("@isNYCreated", SqlDbType.Bit)

        prmWPID.Value = wpid
        prmNYCreated.Direction = ParameterDirection.Output

        Command.Parameters.Add(prmWPID)
        Command.Parameters.Add(prmNYCreated)

        Try
            Connection.Open()
            NYCreated = Convert.ToBoolean(prmNYCreated.Value)
        Catch ex As Exception
            strErrMessage = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try

        If Not String.IsNullOrEmpty(strErrMessage) Then
            ErrorLogInsert(20, "CheckingWPIDNYCreated", strErrMessage, "uspSOAC_IsWPIDNotYetCreated")
        End If

        Return NYCreated
    End Function

    Public Function GetWPIDGroup(ByVal soacid As Int32) As List(Of ODSOACWPIDGroupInfo)
        Dim list As New List(Of ODSOACWPIDGroupInfo)
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("uspSOAC_GetWPIDGroup", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmSOACID As New SqlParameter("@soacid", SqlDbType.BigInt)
        prmSOACID.Value = soacid
        Command.Parameters.Add(prmSOACID)
        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read
                    Dim info As New ODSOACWPIDGroupInfo
                    info.SOACID = Convert.ToInt32(DataReader.Item("SOAC_ID"))
                    info.PackageId = Convert.ToString(DataReader.Item("Package_Id"))
                    info.LMBY = Integer.Parse(DataReader.Item("LMBY").ToString())
                    info.LMDT = Convert.ToDateTime(DataReader.Item("LMDT"))
                    info.ModifiedUser = Convert.ToString(DataReader.Item("modifieduser"))
                    list.Add(info)
                End While
            End If
        Catch ex As Exception
            strErrMessage = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try

        If Not String.IsNullOrEmpty(strErrMessage) Then
            ErrorLogInsert(20, "GetWPIDGroup", strErrMessage, "uspSOAC_GetWPIDGroup")
        End If

        Return list
    End Function

    Public Function ODSOACWPIDGroup_D(ByVal soacid As Int32) As Boolean
        Dim isSucceed As Boolean = True
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("Delete ODSOAC_WPID_Group where soac_id=" & Convert.ToString(soacid), Connection)
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
            ErrorLogInsert(20, "ODSOACWPIDGroup_D", strErrMessage, "CommandType.Text")
        End If

        Return isSucceed
    End Function

    Public Function GetSOACID_ByWPIDGroup(ByVal packageid As String) As Int32
        Dim soacid As Int32 = 0
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("uspSOAC_GetSOACByWPIDGroup", Connection)
        Command.CommandType = CommandType.StoredProcedure
        Dim prmPackageId As New SqlParameter("@packageid", SqlDbType.VarChar, 20)
        Dim prmSOACID As New SqlParameter("@soacid", SqlDbType.BigInt)
        prmPackageId.Value = packageid
        prmSOACID.Direction = ParameterDirection.Output
        Command.Parameters.Add(prmPackageId)
        Command.Parameters.Add(prmSOACID)

        Try
            Connection.Open()
            Command.ExecuteNonQuery()
            soacid = Convert.ToInt32(prmSOACID.Value)
        Catch ex As Exception
            strErrMessage = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try

        If Not String.IsNullOrEmpty(strErrMessage) Then
            ErrorLogInsert(20, "GetSOACID_ByWPIDGroup", strErrMessage, "uspSOAC_GetSOACByWPIDGroup")
        End If

        Return soacid
    End Function
#End Region

#Region "SOAC Approval Status"
    Public Function SOACApprovalStatus(ByVal userid As Integer) As List(Of SOACSiteApprovalStatusInfo)
        Dim list As New List(Of SOACSiteApprovalStatusInfo)
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("uspSOAC_GetSiteApprovalStatus", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmUsrID As New SqlParameter("@usrid", SqlDbType.Int)
        prmUsrID.Value = userid
        Command.Parameters.Add(prmUsrID)

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read()
                    Dim info As New SOACSiteApprovalStatusInfo
                    If Not String.IsNullOrEmpty(Convert.ToString(DataReader.Item("OnAir_Date"))) Then
                        info.SOACInfo.OnAirDate = Convert.ToDateTime(DataReader.Item("OnAir_Date"))
                    End If
                    If Not String.IsNullOrEmpty(Convert.ToString(DataReader.Item("PO_RefNo_Date"))) Then
                        info.SOACInfo.PORefNoDate = Convert.ToDateTime(DataReader.Item("PO_RefNo_Date"))
                    End If
                    info.SiteInf.SiteNo = Convert.ToString(DataReader.Item("siteno"))
                    info.SiteInf.SiteName = Convert.ToString(DataReader.Item("Sitename"))
                    info.SiteInf.SiteIdPO = Convert.ToString(DataReader.Item("SiteIdPO"))
                    info.SiteInf.SiteNamePO = Convert.ToString(DataReader.Item("SitenamePO"))
                    info.SiteInf.PONO = Convert.ToString(DataReader.Item("PONO"))
                    info.SiteInf.Scope = Convert.ToString(DataReader.Item("Scope"))
                    info.SiteInf.PackageId = Convert.ToString(DataReader.Item("Workpackageid"))
                    If Not String.IsNullOrEmpty(Convert.ToString(DataReader.Item("upload_date"))) Then
                        info.SOACMilestoneInfo.UploadDate = Convert.ToDateTime(DataReader.Item("upload_Date"))
                    End If

                    If Not String.IsNullOrEmpty(Convert.ToString(DataReader.Item("submit_date"))) Then
                        info.SOACMilestoneInfo.SubmitDate = Convert.ToDateTime(DataReader.Item("submit_date"))
                    End If

                    If Not String.IsNullOrEmpty(Convert.ToString(DataReader.Item("approved_date"))) Then
                        info.SOACMilestoneInfo.ApprovedDate = Convert.ToDateTime(DataReader.Item("approved_date"))
                    End If
                    info.UserLocation = Convert.ToString(DataReader.Item("UserLocation"))
                    info.TaskDesc = Convert.ToString(DataReader.Item("TaskDesc"))
                    info.DocInfo.DocName = Convert.ToString(DataReader.Item("DocName"))
                    info.CMAInfo.ModifiedUser = Convert.ToString(DataReader.Item("modifieduser"))
                    info.RegionName = Convert.ToString(DataReader.Item("RgnName"))
                    info.AreaName = Convert.ToString(DataReader.Item("AreaName"))
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
            ErrorLogInsert(20, "SOACApprovalStatus", strErrMessage, "uspSOAC_GetSiteApprovalStatus")
        End If

        Return list
    End Function

    Public Function SOACGetTaskPendingCount(ByVal userid As Integer) As Integer
        Dim rowcount As Integer = 0
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("uspSOAC_GetTaskPendingCount", Connection)
        Command.CommandType = CommandType.StoredProcedure
        Dim prmUserID As New SqlParameter("@usrid", SqlDbType.Int)
        Dim prmRowCount As New SqlParameter("@rowcount", SqlDbType.Int)

        prmUserID.Value = userid
        prmRowCount.Direction = ParameterDirection.Output
        Command.Parameters.Add(prmUserID)
        Command.Parameters.Add(prmRowCount)

        Try
            Connection.Open()
            Command.ExecuteNonQuery()
            rowcount = Integer.Parse(prmRowCount.Value.ToString())
        Catch ex As Exception
            strErrMessage = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try

        If Not String.IsNullOrEmpty(strErrMessage) Then
            ErrorLogInsert(20, "SOACGetTaskPendingCount", strErrMessage, "uspSOAC_GetTaskPendingCount")
        End If

        Return rowcount
    End Function

    Public Function SOACinTaskPending(ByVal soacid As Int32, ByVal docid As Integer) As Boolean
        Dim isTaskpending As Boolean = False
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("uspSOAC_IsInTaskPending", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmSOACID As New SqlParameter("@soacid", SqlDbType.BigInt)
        Dim prmInTaskPending As New SqlParameter("@intaskpending", SqlDbType.Bit)
        Dim prmDocId As New SqlParameter("@docid", SqlDbType.Int)

        prmSOACID.Value = soacid
        prmInTaskPending.Direction = ParameterDirection.Output
        prmDocId.Value = docid

        Command.Parameters.Add(prmSOACID)
        Command.Parameters.Add(prmInTaskPending)
        Command.Parameters.Add(prmDocId)

        Try
            Connection.Open()
            Command.ExecuteNonQuery()
            isTaskpending = Convert.ToBoolean(prmInTaskPending.Value)
        Catch ex As Exception
            strErrMessage = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try

        If Not String.IsNullOrEmpty(strErrMessage) Then
            ErrorLogInsert(20, "SOACinTaskPending", strErrMessage, "uspSOAC_IsInTaskPending")
        End If

        Return isTaskpending
    End Function
#End Region

#Region "audittrail"
    Public Function SOACAuditTrail_I(ByVal info As SOACAuditTrailInfo) As Boolean
        Dim isSucceed As Boolean = True
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("uspSOAC_AuditTrail_I", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmSOACID As New SqlParameter("@soacid", SqlDbType.BigInt)
        Dim prmUserId As New SqlParameter("@usrid", SqlDbType.Int)
        Dim prmRoleId As New SqlParameter("@roleid", SqlDbType.Int)
        Dim prmEventStartTime As New SqlParameter("@eventstarttime", SqlDbType.DateTime)
        Dim prmEventEndTime As New SqlParameter("@eventendtime", SqlDbType.DateTime)
        Dim prmRemarks As New SqlParameter("@remarks", SqlDbType.VarChar, 2000)
        Dim prmCategory As New SqlParameter("@category", SqlDbType.VarChar, 2000)
        Dim prmTskId As New SqlParameter("@tskid", SqlDbType.Int)
        Dim prmDocId As New SqlParameter("@docid", SqlDbType.Int)

        prmSOACID.Value = info.SOACID
        prmUserId.Value = info.UserInfo.UserId
        prmRoleId.Value = info.UserInfo.RoleId
        prmEventStartTime.Value = info.EventStartTime
        prmEventEndTime.Value = info.EventEndTime
        prmRemarks.Value = info.Remarks
        prmCategory.Value = info.Category
        prmTskId.Value = info.TskId
        prmDocId.Value = info.DocInfo.DocId


        Command.Parameters.Add(prmSOACID)
        Command.Parameters.Add(prmUserId)
        Command.Parameters.Add(prmRoleId)
        Command.Parameters.Add(prmEventEndTime)
        Command.Parameters.Add(prmEventStartTime)
        Command.Parameters.Add(prmRemarks)
        Command.Parameters.Add(prmCategory)
        Command.Parameters.Add(prmTskId)
        Command.Parameters.Add(prmDocId)

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
            ErrorLogInsert(20, "SOACAuditTrail_I", strErrMessage, "uspSOAC_AuditTrail_I")
        End If

        Return isSucceed
    End Function

    Public Sub AuditTrail_D(ByVal soacid As Int32)
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("delete soacaudittrail where soac_id=" & Convert.ToString(soacid), Connection)
        Command.CommandType = CommandType.Text

        Try
            Connection.Open()
        Catch ex As Exception
            strErrMessage = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try
        If Not String.IsNullOrEmpty(strErrMessage) Then
            ErrorLogInsert(20, "AuditTrail_D", strErrMessage, "CommandType.Text")
        End If
    End Sub

    Public Function GetSOACAuditTrail(ByVal soacid As Int32, ByVal docid As Integer) As List(Of SOACAuditTrailInfo)
        Dim list As New List(Of SOACAuditTrailInfo)
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("uspSOAC_GetAuditTrail", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmDOCID As New SqlParameter("@docid", SqlDbType.Int)
        Dim prmSOACID As New SqlParameter("@soacid", SqlDbType.BigInt)

        prmDOCID.Value = docid
        prmSOACID.Value = soacid

        Command.Parameters.Add(prmDOCID)
        Command.Parameters.Add(prmSOACID)

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read
                    Dim info As New SOACAuditTrailInfo
                    info.Category = Convert.ToString(DataReader.Item("Category"))
                    info.Remarks = Convert.ToString(DataReader.Item("Remarks"))
                    info.TaskEvent = Convert.ToString(DataReader.Item("taskevent"))
                    info.LogId = Convert.ToInt32(DataReader.Item("Log_Id"))
                    info.UserInfo.Fullname = Convert.ToString(DataReader.Item("name"))
                    info.UserInfo.SignTitle = Convert.ToString(DataReader.Item("signtitle"))
                    info.RoleInf.RoleName = Convert.ToString(DataReader.Item("RoleDesc"))
                    info.EventStartTime = Convert.ToDateTime(DataReader.Item("eventstarttime"))
                    info.EventEndTime = Convert.ToDateTime(DataReader.Item("eventendtime"))
                    list.Add(info)
                End While
            End If
        Catch ex As Exception
            strErrMessage = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try
        If Not String.IsNullOrEmpty(strErrMessage) Then
            ErrorLogInsert(20, "GetSOACAuditTrail", strErrMessage, "uspSOAC_GetAuditTrail")
        End If
        Return list
    End Function
#End Region

#Region "SOAC Milestone"
    Public Function SOACMilestone_IU(ByVal soacid As Int32, ByVal docid As Integer, ByVal uploaddate As Nullable(Of DateTime), ByVal submitdate As Nullable(Of DateTime), ByVal approveddate As Nullable(Of DateTime)) As Boolean
        Dim isSucceed As Boolean = True
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("uspSOAC_Milestone_IU", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmSOACID As New SqlParameter("@soacid", SqlDbType.BigInt)
        Dim prmUploadDate As New SqlParameter("@uploaddate", SqlDbType.DateTime)
        Dim prmSubmitDate As New SqlParameter("@submitdate", SqlDbType.DateTime)
        Dim prmApprovedDate As New SqlParameter("@approveddate", SqlDbType.DateTime)
        Dim prmDocId As New SqlParameter("@docid", SqlDbType.Int)

        prmSOACID.Value = soacid
        prmUploadDate.Value = uploaddate
        prmSubmitDate.Value = submitdate
        prmApprovedDate.Value = approveddate
        prmDocId.Value = docid

        Command.Parameters.Add(prmSOACID)
        Command.Parameters.Add(prmUploadDate)
        Command.Parameters.Add(prmSubmitDate)
        Command.Parameters.Add(prmApprovedDate)
        Command.Parameters.Add(prmDocId)

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
            ErrorLogInsert(20, "SOACMilestone_IU", strErrMessage, "uspSOAC_Milestone_IU")
        End If

        Return isSucceed
    End Function

    Public Function SOACMilestone_D(ByVal docid As Integer, ByVal soacid As Int32) As Boolean
        Dim isSucceed As Boolean = True
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("uspSOAC_Milestone_D", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmSOACID As New SqlParameter("@soacid", SqlDbType.BigInt)
        Dim prmDOCID As New SqlParameter("@docid", SqlDbType.Int)

        prmSOACID.Value = soacid
        prmDOCID.Value = docid

        Command.Parameters.Add(prmSOACID)
        Command.Parameters.Add(prmDOCID)

        Try
            Connection.Open()
            Command.ExecuteNonQuery()
        Catch ex As Exception
            strErrMessage = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try

        If Not String.IsNullOrEmpty(strErrMessage) Then
            ErrorLogInsert(20, "SOACMilestone_D", strErrMessage, "uspSOAC_Milestone_D")
        End If

        Return isSucceed
    End Function
#End Region

#Region "Historical Rejection"
    Public Sub HistoricalRejection_I(ByVal info As SOACAuditTrailInfo)
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("uspSOAC_HistoricalRejection_I", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmSOACID As New SqlParameter("@soacid", SqlDbType.BigInt)
        Dim prmDOCID As New SqlParameter("@docid", SqlDbType.Int)
        Dim prmUserID As New SqlParameter("@usrid", SqlDbType.Int)
        Dim prmRoleID As New SqlParameter("@roleid", SqlDbType.Int)
        Dim prmRemarks As New SqlParameter("@remarks", SqlDbType.VarChar, 2000)
        Dim prmCategory As New SqlParameter("@category", SqlDbType.VarChar, 200)

        prmSOACID.Value = info.SOACID
        prmDOCID.Value = info.DocInfo.DocId
        prmUserID.Value = info.UserInfo.UserId
        prmRoleID.Value = info.RoleInf.RoleId
        prmRemarks.Value = info.Remarks
        prmCategory.Value = info.Category

        Command.Parameters.Add(prmSOACID)
        Command.Parameters.Add(prmDOCID)
        Command.Parameters.Add(prmUserID)
        Command.Parameters.Add(prmRoleID)
        Command.Parameters.Add(prmRemarks)
        Command.Parameters.Add(prmCategory)

        Try
            Connection.Open()
            Command.ExecuteNonQuery()
        Catch ex As Exception
            strErrMessage = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try

        If Not String.IsNullOrEmpty(strErrMessage) Then
            ErrorLogInsert(20, "HistoricalRejection_I", strErrMessage, "uspSOAC_HistoricalRejection_I")
        End If

    End Sub

    Public Function GetHistoricalRejection(ByVal soacid As Int32) As List(Of SOACAuditTrailInfo)
        Dim list As New List(Of SOACAuditTrailInfo)
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("uspSOAC_GetHistoricalRejection", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmSOACID As New SqlParameter("@soacid", SqlDbType.BigInt)
        prmSOACID.Value = soacid
        Command.Parameters.Add(prmSOACID)

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read
                    Dim info As New SOACAuditTrailInfo
                    info.SOACID = Convert.ToInt32(DataReader.Item("SOAC_ID"))
                    info.DocInfo.DocId = Integer.Parse(DataReader.Item("Doc_Id").ToString())
                    info.DocInfo.DocName = Convert.ToString(DataReader.Item("DocName"))
                    info.LMDT = Convert.ToDateTime(DataReader.Item("RejectionDate"))
                    info.Remarks = Convert.ToString(DataReader.Item("Remarks"))
                    info.Category = Convert.ToString(DataReader.Item("Category"))
                    info.RoleInf.RoleId = Integer.Parse(DataReader.Item("Role_Id").ToString())
                    info.RoleInf.RoleName = Convert.ToString(DataReader.Item("RoleDesc"))
                    info.UserInfo.SignTitle = Convert.ToString(DataReader.Item("SignTitle"))
                    info.UserInfo.Fullname = Convert.ToString(DataReader.Item("name"))
                    list.Add(info)
                End While
            End If
        Catch ex As Exception
            strErrMessage = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try

        If Not String.IsNullOrEmpty(strErrMessage) Then
            ErrorLogInsert(20, "GetHistoricalRejection", strErrMessage, "uspSOAC_GetHistoricalRejection")
        End If

        Return list
    End Function
#End Region

#Region "Others"
    Public Function GetFinalAppRolePerCompany(ByVal wfid As Integer, ByVal grpid As Integer, ByVal taskdesc As String) As Integer
        Dim roleid As Integer = 0
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("uspGeneral_GetFinalApprovalRoleAsPerCompany", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmTaskDesc As New SqlParameter("@taskdesc", SqlDbType.VarChar, 20)
        Dim prmGRPID As New SqlParameter("@grpid", SqlDbType.Int)
        Dim prmWFID As New SqlParameter("@wfid", SqlDbType.Int)
        Dim prmFinalAppRole As New SqlParameter("@finalapprole", SqlDbType.Int)

        prmTaskDesc.Value = taskdesc
        prmGRPID.Value = grpid
        prmWFID.Value = wfid
        prmFinalAppRole.Direction = ParameterDirection.Output

        Command.Parameters.Add(prmTaskDesc)
        Command.Parameters.Add(prmGRPID)
        Command.Parameters.Add(prmWFID)
        Command.Parameters.Add(prmFinalAppRole)

        Try
            Connection.Open()
            Command.ExecuteNonQuery()
            roleid = Integer.Parse(prmFinalAppRole.Value.ToString())
        Catch ex As Exception
            strErrMessage = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try

        If Not String.IsNullOrEmpty(strErrMessage) Then
            ErrorLogInsert(20, "GetFinalAppRolePerCompany", strErrMessage, "uspGeneral_GetFinalApprovalRoleAsPerCompany")
        End If

        Return roleid
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
