Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data
Imports System.Collections.Generic


Public Class HCPTController
    Inherits BaseController

#Region "General"
    Public Function GetDOCIDBaseSNO(ByVal sno As Int32) As Integer
        Dim docid As Integer = 0
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("HCPT_uspGeneral_GetDOCID_BaseSNO", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmDOCID As New SqlParameter("@docid", SqlDbType.Int)
        Dim prmSNO As New SqlParameter("@sno", SqlDbType.BigInt)

        prmDOCID.Direction = ParameterDirection.Output
        prmSNO.Value = sno

        Command.Parameters.Add(prmDOCID)
        Command.Parameters.Add(prmSNO)

        Try
            Connection.Open()
            Command.ExecuteNonQuery()
            docid = Integer.Parse(prmDOCID.Value.ToString())
        Catch ex As Exception
            strErrMessage = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try

        If Not String.IsNullOrEmpty(strErrMessage) Then
            ErrorLogInsert(150, "GetDOCIDBaseSNO", strErrMessage, "HCPT_uspGeneral_GetDOCID_BaseSNO")
        End If

        Return docid
    End Function

    Public Function HCPT_GetDocuments(ByVal parentid As Integer, ByVal docid As Integer) As List(Of HCPT_DocInfo)
        Dim list As New List(Of HCPT_DocInfo)
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("HCPT_uspDocument_LD", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmDocID As New SqlParameter("@docid", SqlDbType.Int)
        Dim prmParentID As New SqlParameter("@parentid", SqlDbType.Int)

        prmDocID.Value = docid
        prmParentID.Value = parentid

        Command.Parameters.Add(prmDocID)
        Command.Parameters.Add(prmParentID)

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read
                    Dim info As New HCPT_DocInfo
                    info.DocId = Integer.Parse(DataReader.Item("Doc_Id").ToString())
                    info.ParentId = Integer.Parse(DataReader.Item("Parent_Id").ToString())
                    info.DocName = Convert.ToString(DataReader.Item("DocName"))
                    info.DocCode = Convert.ToString(DataReader.Item("DocCode"))
                    info.DocType = Convert.ToString(DataReader.Item("DocType"))
                    info.AppRequired = Convert.ToBoolean(DataReader.Item("Appr_Required"))
                    info.AllowB4Integration = Convert.ToBoolean(DataReader.Item("Allow_Before_Integration"))
                    info.RStatus = Integer.Parse(DataReader.Item("RStatus").ToString())
                    info.CMAInfo.ModifiedUser = Convert.ToString(DataReader.Item("name"))
                    info.CMAInfo.LMDT = Convert.ToDateTime(DataReader.Item("LMDT"))
                    list.Add(info)
                End While
            End If
        Catch ex As Exception
            strErrMessage = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try

        If Not String.IsNullOrEmpty(strErrMessage) Then
            ErrorLogInsert(150, "HCPT_GetDocuments", strErrMessage, "HCPT_uspDocument_LD")
        End If

        Return list
    End Function

    Public Function GetSiteInfoDetail(ByVal wpid As String) As SiteInfo
        Dim info As New SiteInfo
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("HCPT_uspGeneral_GetSiteInfoDetails", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmWPID As New SqlParameter("@wpid", SqlDbType.VarChar, 50)
        prmWPID.Value = wpid
        Command.Parameters.Add(prmWPID)

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read()
                    info.SiteNo = Convert.ToString(DataReader.Item("SiteNo"))
                    info.SiteName = Convert.ToString(DataReader.Item("SiteName"))
                    info.PONO = Convert.ToString(DataReader.Item("PoNo"))
                    info.POName = Convert.ToString(DataReader.Item("PoName"))
                    info.WorkpackageName = Convert.ToString(DataReader.Item("workpackagename"))
                    info.SiteIdPO = Convert.ToString(DataReader.Item("SiteIDPo"))
                    info.SiteNamePO = Convert.ToString(DataReader.Item("SiteNamePO"))
                    info.Scope = Convert.ToString(DataReader.Item("Scope"))
                    info.PackageId = Convert.ToString(DataReader.Item("Workpackageid"))
                    info.ProjectID = Convert.ToString(DataReader.Item("TSELProjectID"))
                    info.HOTAsPO = Convert.ToString(DataReader.Item("hotasperpo"))
                End While
            End If
        Catch ex As Exception
            strErrMessage = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try

        If Not String.IsNullOrEmpty(strErrMessage) Then
            ErrorLogInsert(150, "GetSiteInfoDetail", strErrMessage, "HCPT_uspGeneral_GetSiteInfoDetails")
        End If

        Return info
    End Function

    Public Function GetLastTaskBaseWorkflowGrp(ByVal wfid As Integer, ByVal grpid As Integer) As Integer
        Dim taskid As Integer = 0
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("HCPT_uspGeneral_IsTheLastTaskBaseUsrGRP", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmWFID As New SqlParameter("@wfid", SqlDbType.Int)
        Dim prmUGPId As New SqlParameter("@ugpid", SqlDbType.Int)
        Dim prmTskId As New SqlParameter("@tskid", SqlDbType.Int)

        prmWFID.Value = wfid
        prmUGPId.Value = grpid
        prmTskId.Direction = ParameterDirection.Output

        Command.Parameters.Add(prmWFID)
        Command.Parameters.Add(prmUGPId)
        Command.Parameters.Add(prmTskId)

        Try
            Connection.Open()
            Command.ExecuteNonQuery()
            taskid = Integer.Parse(prmTskId.Value.ToString())
        Catch ex As Exception
            strErrMessage = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try

        If Not String.IsNullOrEmpty(strErrMessage) Then
            ErrorLogInsert(150, "GetLastTaskBaseWorkflowGrp", strErrMessage, "HCPT_uspGeneral_IsTheLastTaskBaseUsrGRP")
        End If

        Return taskid
    End Function

    Public Function GetDocDetail(ByVal docid As Integer) As DocInfo
        Dim info As New DocInfo
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("HCPT_uspDoc_LD", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmDocId As New SqlParameter("@docid", SqlDbType.Int)
        prmDocId.Value = docid
        Command.Parameters.Add(prmDocId)

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read
                    info.DocId = docid
                    info.DocName = Convert.ToString(DataReader.Item("DocName"))
                End While
            Else
                info = Nothing
            End If
        Catch ex As Exception
            info = Nothing
            strErrMessage = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try

        If Not String.IsNullOrEmpty(strErrMessage) Then
            ErrorLogInsert(150, "GetDocDetail", strErrMessage, "HCPT_uspDoc_LD")
        End If
        Return info
    End Function

    Public Sub ATPDocCompleted_U(ByVal docid As Integer, ByVal wpid As String, ByVal docpath As String, ByVal atpreportdoc As Integer)
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("HCPT_uspGeneral_UpdateDocPath_ATP", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmDocID As New SqlParameter("@docid", SqlDbType.Int)
        Dim prmWPID As New SqlParameter("@wpid", SqlDbType.VarChar, 50)
        Dim prmDocPath As New SqlParameter("@docpath", SqlDbType.VarChar, 2000)
        Dim prmATPReportDoc As New SqlParameter("@atpreportdoc", SqlDbType.Int)

        prmDocID.Value = docid
        prmWPID.Value = wpid
        prmDocPath.Value = docpath
        prmATPReportDoc.Value = atpreportdoc

        Command.Parameters.Add(prmDocID)
        Command.Parameters.Add(prmWPID)
        Command.Parameters.Add(prmDocPath)
        Command.Parameters.Add(prmATPReportDoc)

        Try
            Connection.Open()
            Command.ExecuteNonQuery()
        Catch ex As Exception
            strErrMessage = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try

        If Not String.IsNullOrEmpty(strErrMessage) Then
            ErrorLogInsert(150, "ATPDocCompleted_U", strErrMessage, "HCPT_uspGeneral_UpdateDocPath_ATP")
        End If

    End Sub

    Public Function GetDocuments(ByVal doctype As String) As List(Of DocInfo)
        Dim list As New List(Of DocInfo)
        Command = New SqlCommand("select * from coddoc where doctype='" & doctype & "' order by serialno asc", Connection)
        Command.CommandType = CommandType.Text

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read
                    Dim info As New DocInfo
                    info.DocId = Integer.Parse(DataReader.Item("doc_id").ToString())
                    info.DocName = Convert.ToString(DataReader.Item("docname"))
                    list.Add(info)
                End While
            Else
                list = Nothing
            End If

        Catch ex As Exception
            list = Nothing
        Finally
            Connection.Close()
        End Try

        Return list
    End Function

    Public Function GetUserTask() As List(Of UserTypeInfo)
        Dim list As New List(Of UserTypeInfo)
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("HCPT_uspGeneral_GetUserType", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read
                    Dim info As New UserTypeInfo
                    info.UserTypeId = Integer.Parse(DataReader.Item("grpid").ToString())
                    info.UserType = Convert.ToString(DataReader.Item("grpcode"))
                    info.UserCompany = Convert.ToString(DataReader.Item("companyname"))
                    list.Add(info)
                End While
            Else
                list = Nothing
            End If
        Catch ex As Exception
            list = Nothing
            strErrMessage = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try

        Return list
    End Function

    Public Function GetUserPending(ByVal usrtype As String) As List(Of UserProfileInfo)
        Dim list As New List(Of UserProfileInfo)
        Command = New SqlCommand("HCPT_uspGeneral_GetUserBaseTask", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmUsrType As New SqlParameter("@usrtype", SqlDbType.VarChar, 2)
        If String.IsNullOrEmpty(usrtype) Then
            prmUsrType.Value = Nothing
        Else
            prmUsrType.Value = usrtype
        End If
        Command.Parameters.Add(prmUsrType)

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read
                    Dim info As New UserProfileInfo
                    info.UserId = Integer.Parse(DataReader.Item("usr_id"))
                    info.Fullname = Convert.ToString(DataReader.Item("CompanyName")) & "|" & Convert.ToString(DataReader.Item("name")) & "|" & Convert.ToString(DataReader.Item("SignTitle"))
                    info.SignTitle = Convert.ToString(DataReader.Item("SignTitle"))
                    info.SubconId = Integer.Parse(DataReader.Item("usrRole").ToString())
                    list.Add(info)
                End While
            End If
        Catch ex As Exception
            list = Nothing
        Finally
            Connection.Close()
        End Try

        Return list
    End Function

#End Region

#Region "Milestone Update"
    Public Function Milestone_IU(ByVal wpid As String, ByVal docid As Integer, ByVal submitdate As Nullable(Of DateTime), ByVal approveddate As Nullable(Of DateTime)) As Boolean
        Dim isSucceed As Boolean = True
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("HCPT_uspDashMS_IU", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmWPID As New SqlParameter("@wpid", SqlDbType.VarChar, 50)
        Dim prmDocID As New SqlParameter("@docid", SqlDbType.Int)
        Dim prmSubmitDate As New SqlParameter("@submitdate", SqlDbType.DateTime)
        Dim prmApprovedDate As New SqlParameter("@approveddate", SqlDbType.DateTime)

        prmWPID.Value = wpid
        prmDocID.Value = docid
        prmSubmitDate.Value = submitdate
        prmApprovedDate.Value = approveddate

        Command.Parameters.Add(prmWPID)
        Command.Parameters.Add(prmDocID)
        Command.Parameters.Add(prmSubmitDate)
        Command.Parameters.Add(prmApprovedDate)

        Try
            Connection.Open()
            Command.ExecuteNonQuery()
        Catch ex As Exception
            strErrMessage = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try

        If Not String.IsNullOrEmpty(strErrMessage) Then
            ErrorLogInsert(150, "Milestone_IU", strErrMessage, "HCPT_uspDashMS_IU")
        End If

        Return isSucceed
    End Function
#End Region


    Public Function HCPT_Document_IU(ByVal info As HCPT_DocInfo) As Boolean
        Dim isSuceed As Boolean = True
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("HCPT_uspCODDocumentIU", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmDocID As New SqlParameter("@docid", SqlDbType.Int)
        Dim prmParentID As New SqlParameter("@parentid", SqlDbType.Int)
        Dim prmDocType As New SqlParameter("@doctype", SqlDbType.VarChar, 2)
        Dim prmDocName As New SqlParameter("@docname", SqlDbType.VarChar, 300)
        Dim prmApprRequired As New SqlParameter("@Appr_Required", SqlDbType.Bit)
        Dim prmAllowB4Integration As New SqlParameter("@Allow_Before_Integration", SqlDbType.Bit)
        Dim prmRStatus As New SqlParameter("@rstatus", SqlDbType.Int)
        Dim prmLMBY As New SqlParameter("@lmby", SqlDbType.Int)
        Dim prmLMDT As New SqlParameter("@lmdt", SqlDbType.DateTime)
        Dim prmSerialNo As New SqlParameter("@serialno", SqlDbType.Int)

        prmDocID.Value = info.DocId
        prmParentID.Value = info.ParentId
        prmDocType.Value = info.DocType
        prmDocName.Value = info.DocName
        prmApprRequired.Value = info.AppRequired
        prmAllowB4Integration.Value = info.AllowB4Integration
        prmRStatus.Value = info.RStatus
        prmLMBY.Value = info.CMAInfo.LMBY
        prmLMDT.Value = info.CMAInfo.LMDT
        prmSerialNo.Value = info.SerialNo

        Command.Parameters.Add(prmDocID)
        Command.Parameters.Add(prmParentID)
        Command.Parameters.Add(prmDocType)
        Command.Parameters.Add(prmDocName)
        Command.Parameters.Add(prmApprRequired)
        Command.Parameters.Add(prmAllowB4Integration)
        Command.Parameters.Add(prmRStatus)
        Command.Parameters.Add(prmLMBY)
        Command.Parameters.Add(prmLMDT)
        Command.Parameters.Add(prmSerialNo)

        Try
            Connection.Open()
            Command.ExecuteNonQuery()
        Catch ex As Exception
            isSuceed = False
            strErrMessage = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try

        If Not String.IsNullOrEmpty(strErrMessage) Then
            ErrorLogInsert(150, "HCPT_Document_IU", strErrMessage, "HCPT_uspCODDocumentIU")
        End If

        Return isSuceed
    End Function

    Public Function HCPT_Document_D(ByVal docid As Integer, ByVal istemp As Boolean) As Boolean
        Dim isSucceed As Boolean = True
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("HCPT_uspDocument_D", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmDocId As New SqlParameter("@docid", SqlDbType.Int)
        Dim prmTempDeletion As New SqlParameter("@istempdeletion", SqlDbType.Bit)

        prmDocId.Value = docid
        prmTempDeletion.Value = istemp

        Command.Parameters.Add(prmDocId)
        Command.Parameters.Add(prmTempDeletion)

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
            ErrorLogInsert(150, "HCPT_Document_D", strErrMessage, "HCPT_uspDocument_D")
        End If

        Return isSucceed
    End Function

    Public Function GetFinalApprovers(ByVal packageid As String, ByVal grpid1 As Integer, ByVal grpid2 As Integer, ByVal grpid3 As Integer, ByVal wfid As Integer, ByVal taskdesc As String) As List(Of UserProfileInfo)
        Dim list As New List(Of UserProfileInfo)
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("HCPT_uspGeneral_GetFinalApprover", Connection)
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
                    Dim info As New UserProfileInfo
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

    Public Function UploadRejectOtherDocument(ByVal wrkpkgid As Integer, ByVal guid As String, ByVal DocPath As String, ByVal UserID As Integer, ByVal docid As Integer) As Boolean
        Dim strErrMessage As String = String.Empty
        Dim isSucceed As Boolean = True
        Command = New SqlCommand("HCPT_AdditionalDocument_I", Connection)
        Command.CommandType = CommandType.StoredProcedure


        Dim prmwrkpkgid As New SqlParameter("@wrkpkgid", SqlDbType.Int)
        Dim prmDocPath As New SqlParameter("@DOCPATH", SqlDbType.VarChar, 1000)
        Dim prmUserID As New SqlParameter("@USERID", SqlDbType.Int)
        Dim prmGuid As New SqlParameter("@Guid", SqlDbType.VarChar, 100)
        Dim prmdocid As New SqlParameter("@docid", SqlDbType.VarChar, 100)


        prmwrkpkgid.Value = wrkpkgid
        prmDocPath.Value = DocPath
        prmUserID.Value = UserID
        prmGuid.Value = guid
        prmdocid.Value = docid


        Command.Parameters.Add(prmwrkpkgid)
        Command.Parameters.Add(prmDocPath)
        Command.Parameters.Add(prmGuid)
        Command.Parameters.Add(prmUserID)
        Command.Parameters.Add(prmdocid)


        Try
            Connection.Open()
            Command.ExecuteNonQuery()
        Catch ex As Exception
            strErrMessage = ex.Message.ToString()
            isSucceed = False
        Finally
            Connection.Close()
        End Try
        If Not String.IsNullOrEmpty(strErrMessage) Then
            ErrorLogInsert(150, "UploadRejectOtherDocument", strErrMessage, "HCPT_AdditionalDocument_I")
        End If

        Return isSucceed
    End Function

#Region "WFTransaction"
    Public Function WFTransaction_D(ByVal wpid As String, ByVal docid As Integer) As Boolean
        Dim isSucceed As Boolean = True
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("HCPT_uspTrans_D", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmWPID As New SqlParameter("@wpid", SqlDbType.VarChar, 50)
        Dim prmDocID As New SqlParameter("@docid", SqlDbType.Int)

        prmWPID.Value = wpid
        prmDocID.Value = docid

        Command.Parameters.Add(prmWPID)
        Command.Parameters.Add(prmDocID)

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
            ErrorLogInsert(150, "WFTransaction_D", strErrMessage, "HCPT_uspTrans_D")
        End If

        Return isSucceed
    End Function

    Public Function WFTransaction_I(ByVal info As DOCTransactionInfo) As Boolean
        Dim isSucceed As Boolean = True
        Dim strErrMessage As String = String.Empty

        Command = New SqlCommand("HCPT_uspTrans_I", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmDOCID As New SqlParameter("@docid", SqlDbType.Int)
        Dim prmWPID As New SqlParameter("@wpid", SqlDbType.VarChar, 50)
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
        prmWPID.Value = info.SiteInf.PackageId
        prmWFID.Value = info.WFID
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
        Command.Parameters.Add(prmWPID)
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
            ErrorLogInsert(150, "WFTransaction_I", strErrMessage, "HCPT_uspTrans_I")
        End If

        Return isSucceed
    End Function

    Public Function WFTransaction_LD(ByVal sno As Int32) As DOCTransactionInfo
        Dim info As New DOCTransactionInfo
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("HCPT_uspTrans_GetDetail", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmSNO As New SqlParameter("@sno", SqlDbType.BigInt)
        prmSNO.Value = sno
        Command.Parameters.Add(prmSNO)

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read
                    info.SNO = Convert.ToInt32(DataReader.Item("SNO"))
                    info.DocInf.DocId = Integer.Parse(DataReader.Item("Doc_Id").ToString())
                    info.SiteInf.PackageId = Convert.ToString(DataReader.Item("WPID"))
                    info.WFID = Integer.Parse(DataReader.Item("WF_ID").ToString())
                    info.TaskId = Integer.Parse(DataReader.Item("Tsk_Id").ToString())
                    info.RoleInf.RoleId = Integer.Parse(DataReader.Item("Role_id").ToString())
                    If Not String.IsNullOrEmpty(Convert.ToString(DataReader.Item("StartDateTime"))) Then
                        info.StartDateTime = Convert.ToDateTime(DataReader.Item("StartDateTime"))
                    End If
                    If Not String.IsNullOrEmpty(Convert.ToString(DataReader.Item("EndDateTime"))) Then
                        info.EndDateTime = Convert.ToDateTime(DataReader.Item("EndDateTime"))
                    End If
                    info.Status = Integer.Parse(DataReader.Item("Status").ToString())
                    info.RStatus = Integer.Parse(DataReader.Item("Rstatus").ToString())
                    info.CMAInfo.LMBY = Integer.Parse(DataReader.Item("LMBY").ToString())
                    info.CMAInfo.LMDT = Convert.ToDateTime(DataReader.Item("LMDT"))
                    info.UGPID = Integer.Parse(DataReader.Item("UGP_Id").ToString())
                    info.Xval = Integer.Parse(DataReader.Item("xVal").ToString())
                    info.Yval = Integer.Parse(DataReader.Item("yVal").ToString())
                    info.PageNo = Integer.Parse(DataReader.Item("Page_No").ToString())
                End While
            Else
                info = Nothing
            End If
        Catch ex As Exception
            info = Nothing
            strErrMessage = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try

        If Not String.IsNullOrEmpty(strErrMessage) Then
            ErrorLogInsert(150, "WFTransaction_LD", strErrMessage, "HCPT_uspTrans_GetDetail")
        End If

        Return info
    End Function

    Public Function GetWFTransaction(ByVal userid As Integer) As List(Of DOCTransactionInfo)
        Dim list As New List(Of DOCTransactionInfo)
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("HCPT_uspTrans_GetTaskPending", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmUserID As New SqlParameter("@usrid", SqlDbType.Int)
        prmUserID.Value = userid
        Command.Parameters.Add(prmUserID)

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read()
                    Dim info As New DOCTransactionInfo
                    info.SiteInf.PONO = Convert.ToString(DataReader.Item("PONO"))
                    info.SiteInf.POName = Convert.ToString(DataReader.Item("POName"))
                    info.SiteInf.SiteNo = Convert.ToString(DataReader.Item("SiteNo"))
                    info.SiteInf.SiteName = Convert.ToString(DataReader.Item("SiteName"))
                    info.SiteInf.SiteIdPO = Convert.ToString(DataReader.Item("SiteIdPO"))
                    info.SiteInf.SiteNamePO = Convert.ToString(DataReader.Item("SiteNamePO"))
                    info.SiteInf.Scope = Convert.ToString(DataReader.Item("Scope"))
                    info.SiteInf.AreaName = Convert.ToString(DataReader.Item("Areaname"))
                    info.SiteInf.RegionName = Convert.ToString(DataReader.Item("RgnName"))
                    info.SiteInf.PackageId = Convert.ToString(DataReader.Item("Workpackageid"))
                    If Not String.IsNullOrEmpty(Convert.ToString(DataReader.Item("SubmitDate"))) Then
                        info.SubmitDate = Convert.ToDateTime(DataReader.Item("SubmitDate"))
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
            ErrorLogInsert(150, "GetWFTransaction", strErrMessage, "HCPT_uspTrans_GetTaskPending")
        End If

        Return list
    End Function

    Public Function DocApproved(ByVal info As DOCTransactionInfo) As Boolean
        Dim isSucceed As Boolean = True
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("HCPT_uspTrans_DocApproved", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmSNO As New SqlParameter("@sno", SqlDbType.BigInt)
        Dim prmWPID As New SqlParameter("@wpid", SqlDbType.VarChar, 50)
        Dim prmWFID As New SqlParameter("@wfid", SqlDbType.Int)
        Dim prmTskID As New SqlParameter("@tskid", SqlDbType.Int)
        Dim prmRoleID As New SqlParameter("@roleid", SqlDbType.Int)
        Dim prmUsrID As New SqlParameter("@userid", SqlDbType.Int)
        Dim prmMedia As New SqlParameter("@media", SqlDbType.NVarChar)      'Added by Fauzan, 28 Nov 2018. To differentiate between email approval or web approval


        prmSNO.Value = info.SNO
        prmWPID.Value = info.SiteInf.PackageId
        prmWFID.Value = info.WFID
        prmTskID.Value = info.TaskId
        prmRoleID.Value = info.RoleInf.RoleId
        prmUsrID.Value = info.UserInf.UserId
        prmMedia.Value = info.Media

        Command.Parameters.Add(prmSNO)
        Command.Parameters.Add(prmWPID)
        Command.Parameters.Add(prmWFID)
        Command.Parameters.Add(prmTskID)
        Command.Parameters.Add(prmRoleID)
        Command.Parameters.Add(prmUsrID)
        Command.Parameters.Add(prmMedia)

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
            ErrorLogInsert(150, "DocApproved", strErrMessage, "HCPT_uspTrans_DocApproved")
        End If

        Return isSucceed
    End Function

    Public Function DocRejected(ByVal info As DOCTransactionInfo, ByVal remarks As String, ByVal categories As String) As Boolean
        Dim isSucceed As Boolean = True
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("HCPT_uspTrans_DocRejected", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmSNO As New SqlParameter("@sno", SqlDbType.BigInt)
        Dim prmWPID As New SqlParameter("@wpid", SqlDbType.VarChar, 50)
        Dim prmRoleID As New SqlParameter("@roleid", SqlDbType.Int)
        Dim prmUsrID As New SqlParameter("@usrid", SqlDbType.Int)
        Dim prmRemarks As New SqlParameter("@remarks", SqlDbType.VarChar, 2000)
        Dim prmCategories As New SqlParameter("@categories", SqlDbType.VarChar, 500)

        prmSNO.Value = info.SNO
        prmWPID.Value = info.SiteInf.PackageId
        prmRoleID.Value = info.RoleInf.RoleId
        prmUsrID.Value = info.UserInf.UserId
        prmRemarks.Value = remarks
        prmCategories.Value = categories

        Command.Parameters.Add(prmSNO)
        Command.Parameters.Add(prmWPID)
        Command.Parameters.Add(prmRoleID)
        Command.Parameters.Add(prmUsrID)
        Command.Parameters.Add(prmRemarks)
        Command.Parameters.Add(prmCategories)

        Try
            Connection.Open()
            Command.ExecuteNonQuery()
        Catch ex As Exception
            strErrMessage = ex.Message.ToString()
            isSucceed = False
        Finally
            Connection.Close()
        End Try

        If Not String.IsNullOrEmpty(strErrMessage) Then
            ErrorLogInsert(150, "DocRejected", strErrMessage, "HCPT_uspTrans_DocRejected")
        End If

        Return isSucceed
    End Function

    Public Function DocRejected_Attachment(ByVal info As DOCTransactionInfo, ByVal remarks As String, ByVal categories As String) As Boolean
        Dim isSucceed As Boolean = True
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("HCPT_uspTrans_DocRejected_Attachment", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmDocId As New SqlParameter("@docid", SqlDbType.BigInt)
        Dim prmWPID As New SqlParameter("@wpid", SqlDbType.VarChar, 50)
        Dim prmRoleID As New SqlParameter("@roleid", SqlDbType.Int)
        Dim prmUsrID As New SqlParameter("@usrid", SqlDbType.Int)
        Dim prmRemarks As New SqlParameter("@remarks", SqlDbType.VarChar, 2000)
        Dim prmCategories As New SqlParameter("@categories", SqlDbType.VarChar, 500)
        Dim prmWFID As New SqlParameter("@wfid", SqlDbType.Int)

        prmDocId.Value = info.DocInf.DocId
        prmWPID.Value = info.SiteInf.PackageId
        prmRoleID.Value = info.RoleInf.RoleId
        prmUsrID.Value = info.UserInf.UserId
        prmRemarks.Value = remarks
        prmCategories.Value = categories
        prmWFID.Value = info.WFID

        Command.Parameters.Add(prmDocId)
        Command.Parameters.Add(prmWPID)
        Command.Parameters.Add(prmRoleID)
        Command.Parameters.Add(prmUsrID)
        Command.Parameters.Add(prmRemarks)
        Command.Parameters.Add(prmCategories)
        Command.Parameters.Add(prmWFID)

        Try
            Connection.Open()
            Command.ExecuteNonQuery()
        Catch ex As Exception
            strErrMessage = ex.Message.ToString()
            isSucceed = False
        Finally
            Connection.Close()
        End Try

        If Not String.IsNullOrEmpty(strErrMessage) Then
            ErrorLogInsert(150, "DocRejected", strErrMessage, "HCPT_uspTrans_DocRejected")
        End If

        Return isSucceed
    End Function
    Public Function DocRemarks(ByVal wpid As Integer, ByVal guid As String, ByVal remarks As String, ByVal userid As Integer, ByVal docid As Integer) As Boolean
        Dim isSucceed As Boolean = True
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("HCPT_Accept_Remarks_I", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmwpid As New SqlParameter("@wrkpackageid", SqlDbType.Int)
        Dim prmGuid As New SqlParameter("@RGuid", SqlDbType.VarChar, 100)
        Dim prmRemarks As New SqlParameter("@remarks", SqlDbType.VarChar, 1000)
        Dim prmUsrID As New SqlParameter("@userid", SqlDbType.Int)
        Dim prmDocID As New SqlParameter("@docid", SqlDbType.Int)



        prmwpid.Value = wpid
        prmGuid.Value = guid
        prmRemarks.Value = remarks
        prmUsrID.Value = userid
        prmDocID.Value = docid


        Command.Parameters.Add(prmwpid)
        Command.Parameters.Add(prmGuid)
        Command.Parameters.Add(prmRemarks)
        Command.Parameters.Add(prmUsrID)
        Command.Parameters.Add(prmDocID)

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
            ErrorLogInsert(150, "DocRemarks", strErrMessage, "HCPT_Remarks_I")
        End If

        Return isSucceed
    End Function
    Public Function DocRemarksReject(ByVal wpid As Integer, ByVal guid As String, ByVal remarks As String, ByVal userid As Integer, ByVal docid As Integer) As Boolean
        Dim isSucceed As Boolean = True
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("HCPT_Reject_Remarks_I", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmwpid As New SqlParameter("@wrkpackageid", SqlDbType.Int)
        Dim prmGuid As New SqlParameter("@RGuid", SqlDbType.VarChar, 100)
        Dim prmRemarks As New SqlParameter("@remarks", SqlDbType.VarChar, 1000)
        Dim prmUsrID As New SqlParameter("@userid", SqlDbType.Int)
        Dim prmDocID As New SqlParameter("@docid", SqlDbType.Int)



        prmwpid.Value = wpid
        prmGuid.Value = guid
        prmRemarks.Value = remarks
        prmUsrID.Value = userid
        prmDocID.Value = docid


        Command.Parameters.Add(prmwpid)
        Command.Parameters.Add(prmGuid)
        Command.Parameters.Add(prmRemarks)
        Command.Parameters.Add(prmUsrID)
        Command.Parameters.Add(prmDocID)

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
            ErrorLogInsert(150, "DocRemarks", strErrMessage, "HCPT_Remarks_I")
        End If

        Return isSucceed
    End Function
    Public Function GetDocReviewerLog(ByVal docid As Integer, ByVal wpid As String) As List(Of DOCTransactionInfo)
        Dim list As New List(Of DOCTransactionInfo)
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("HCPT_uspTrans_GetDocReviewBaseOnTask", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmDocId As New SqlParameter("@docid", SqlDbType.Int)
        Dim prmWPID As New SqlParameter("@wpid", SqlDbType.VarChar, 50)

        prmDocId.Value = docid
        prmWPID.Value = wpid

        Command.Parameters.Add(prmDocId)
        Command.Parameters.Add(prmWPID)

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read
                    Dim info As New DOCTransactionInfo
                    info.UserInf.Username = Convert.ToString(DataReader.Item("name"))
                    info.UserInf.SignTitle = Convert.ToString(DataReader.Item("SignTitle"))
                    info.EndDateTime = Convert.ToDateTime(DataReader.Item("LMDT"))
                    info.UserInf.UserId = Integer.Parse(DataReader.Item("LMBY"))
                    info.UserInf.UserType = Convert.ToString(DataReader.Item("usrtype"))
                    info.TaskId = Integer.Parse(DataReader.Item("tsk_id").ToString())
                    info.SNO = Convert.ToInt32(DataReader.Item("sno"))
                    list.Add(info)
                End While
            End If
        Catch ex As Exception
            strErrMessage = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try

        If Not String.IsNullOrEmpty(strErrMessage) Then
            ErrorLogInsert(150, "GetReviewTransactionLog", strErrMessage, "HCPT_uspTrans_GetLastReviewBaseOnTask")
        End If

        Return list
    End Function

    Public Function GetReviewTransactionLog(ByVal docid As Integer, ByVal wpid As String, ByVal taskdesc As String) As List(Of DOCTransactionInfo)
        Dim list As New List(Of DOCTransactionInfo)
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("HCPT_uspTrans_GetLastReviewBaseOnTask", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmSNO As New SqlParameter("@sno", SqlDbType.BigInt)
        Dim prmWPID As New SqlParameter("@wpid", SqlDbType.VarChar, 50)
        Dim prmTaskDesc As New SqlParameter("@taskdesc", SqlDbType.VarChar, 50)

        prmSNO.Value = docid
        prmWPID.Value = wpid
        prmTaskDesc.Value = taskdesc

        Command.Parameters.Add(prmSNO)
        Command.Parameters.Add(prmWPID)
        Command.Parameters.Add(prmTaskDesc)

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read
                    Dim info As New DOCTransactionInfo
                    info.UserInf.Username = Convert.ToString(DataReader.Item("name"))
                    info.UserInf.SignTitle = Convert.ToString(DataReader.Item("SignTitle"))
                    info.EndDateTime = Convert.ToDateTime(DataReader.Item("LMDT"))
                    info.UserInf.UserId = Integer.Parse(DataReader.Item("LMBY"))
                    info.SNO = Convert.ToInt32(DataReader.Item("sno"))
                    list.Add(info)
                End While
            End If
        Catch ex As Exception
            strErrMessage = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try

        If Not String.IsNullOrEmpty(strErrMessage) Then
            ErrorLogInsert(150, "GetReviewTransactionLog", strErrMessage, "HCPT_uspTrans_GetLastReviewBaseOnTask")
        End If

        Return list
    End Function

    Public Function WFTransaction_LastRollback(ByVal sno As Int32, ByVal roleid As Integer, ByVal usrid As Integer) As Boolean
        Dim isSucceed As Boolean = True
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("HCPT_uspTrans_RollbackLastTansaction", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmSNO As New SqlParameter("@sno", SqlDbType.BigInt)
        Dim prmRoleID As New SqlParameter("@roleid", SqlDbType.Int)
        Dim prmUserID As New SqlParameter("@usrid", SqlDbType.Int)

        'Modified by Fauzan 29 Nov 2018. Bugs
        prmSNO.Value = sno
        prmRoleID.Value = roleid
        prmUserID.Value = usrid

        Command.Parameters.Add(prmSNO)
        Command.Parameters.Add(prmRoleID)
        Command.Parameters.Add(prmUserID)

        Try
            Connection.Open()
            Command.ExecuteNonQuery()
        Catch ex As Exception
            strErrMessage = ex.Message.ToString()
            isSucceed = False
        Finally
            Connection.Close()
        End Try

        If Not String.IsNullOrEmpty(strErrMessage) Then
            ErrorLogInsert(150, "WFTransaction_LastRollback", strErrMessage, "HCPT_uspTrans_RollbackLastTansaction")
        End If

        Return isSucceed
    End Function

    Public Function GetNextPIC(ByVal wpid As String, ByVal docid As Integer) As List(Of UserProfileInfo)
        Dim list As New List(Of UserProfileInfo)
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("HCPT_uspTrans_GetNextPIC", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmWPID As New SqlParameter("@wpid", SqlDbType.VarChar, 50)
        Dim prmDocId As New SqlParameter("@docid", SqlDbType.Int)


        prmWPID.Value = wpid
        prmDocId.Value = docid

        Command.Parameters.Add(prmWPID)
        Command.Parameters.Add(prmDocId)

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read
                    Dim info As New UserProfileInfo
                    info.Fullname = Convert.ToString(DataReader.Item("name"))
                    info.SignTitle = Convert.ToString(DataReader.Item("SignTitle"))
                    info.UserId = Integer.Parse(DataReader.Item("usr_id"))
                    info.PhoneNo = Convert.ToString(DataReader.Item("PhoneNo"))
                    info.Email = Convert.ToString(DataReader.Item("Email"))
                    info.RoleInf.RoleId = DataReader.Item("USRRole").ToString     'Added by Fauzan, 27 Nov 2018. Get User Type to validate email approval
                    info.UserType = DataReader.Item("USRType").ToString     'Added by Fauzan, 27 Nov 2018. Get User Type to validate email approval
                    list.Add(info)
                End While
            Else
                list = Nothing
            End If
        Catch ex As Exception
            list = Nothing
            strErrMessage = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try

        If Not String.IsNullOrEmpty(strErrMessage) Then
            ErrorLogInsert(150, "", strErrMessage, "HCPT_uspTrans_GetNextPIC")
        End If

        Return list
    End Function

#End Region

#Region "Task Description"
    Public Function GetTaskDesc(ByVal tskid As Integer) As String
        Dim strTaskDesc As String = String.Empty
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("HCPT_uspGeneral_GetTaskDesc", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmTskID As New SqlParameter("@tskid", SqlDbType.Int)
        Dim prmTaskDesc As New SqlParameter("@taskdesc", SqlDbType.VarChar, 20)

        prmTskID.Value = tskid
        prmTaskDesc.Direction = ParameterDirection.Output

        Command.Parameters.Add(prmTskID)
        Command.Parameters.Add(prmTaskDesc)

        Try
            Connection.Open()
            Command.ExecuteNonQuery()
            strTaskDesc = Convert.ToString(prmTaskDesc.Value)
        Catch ex As Exception
            strErrMessage = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try

        If Not String.IsNullOrEmpty(strErrMessage) Then
            ErrorLogInsert(150, "GetTaskDesc", strErrMessage, "HCPT_uspGeneral_GetTaskDesc")
        End If

        Return strTaskDesc
    End Function
#End Region

#Region "DG Password"
    Public Function DGPassword_CheckIsExpired(ByVal usrid As Integer) As Boolean
        Dim isExpired As Boolean = True
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("HCPT_uspDGPas_CheckExpired", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmUSRID As New SqlParameter("@usrid", SqlDbType.Int)
        Dim prmIsExpired As New SqlParameter("@isexpired", SqlDbType.Bit)

        prmUSRID.Value = usrid
        prmIsExpired.Direction = ParameterDirection.Output

        Command.Parameters.Add(prmUSRID)
        Command.Parameters.Add(prmIsExpired)

        Try
            Connection.Open()
            Command.ExecuteNonQuery()
            isExpired = Convert.ToBoolean(prmIsExpired.Value)
        Catch ex As Exception
            strErrMessage = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try

        If Not String.IsNullOrEmpty(strErrMessage) Then
            ErrorLogInsert(150, "DGPassword_CheckIsExpired", strErrMessage, "HCPT_uspDGPas_CheckExpired")
        End If

        Return isExpired
    End Function

    Public Function DGPassword_I(ByVal info As DGPassInfo) As Boolean
        Dim isSucceed As Boolean = True
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("HCPT_uspDGPas_I", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmUSRID As New SqlParameter("@usrid", SqlDbType.Int)
        Dim prmNewPass As New SqlParameter("@newpass", SqlDbType.VarChar, 10)

        prmUSRID.Value = info.UserInfo.UserId
        prmNewPass.Value = info.NewPassword

        Command.Parameters.Add(prmUSRID)
        Command.Parameters.Add(prmNewPass)

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
            ErrorLogInsert(150, "DGPassword_I", strErrMessage, "HCPT_uspDGPas_I")
        End If

        Return isSucceed
    End Function

    Public Function DGPassword_Validation(ByVal usrid As Integer, ByVal pass As String) As String
        Dim getUserlogin As String = String.Empty
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("HCPT_uspDGPas_validation", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmUSRID As New SqlParameter("@usrid", SqlDbType.Int)
        Dim prmNewPass As New SqlParameter("@newpass", SqlDbType.VarChar, 20)
        Dim prmUsrLogin As New SqlParameter("@usrLogin", SqlDbType.VarChar, 500)

        prmUSRID.Value = usrid
        prmNewPass.Value = pass
        prmUsrLogin.Direction = ParameterDirection.Output

        Command.Parameters.Add(prmUSRID)
        Command.Parameters.Add(prmNewPass)
        Command.Parameters.Add(prmUsrLogin)

        Try
            Connection.Open()
            Command.ExecuteNonQuery()
            getUserlogin = Convert.ToString(prmUsrLogin.Value)
        Catch ex As Exception
            getUserlogin = String.Empty
            strErrMessage = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try

        If Not String.IsNullOrEmpty(strErrMessage) Then
            ErrorLogInsert(150, "DGPassword_Validation", strErrMessage, "HCPT_uspDGPas_validation")
        End If

        Return getUserlogin
    End Function
#End Region

#Region "Audit Trail"
    Public Function AuditTrail_I(ByVal info As HCPTAuditTrailInfo) As Boolean
        Dim isSucceed As Boolean = True
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("HCPT_uspAuditTrail_I", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmPONO As New SqlParameter("@pono", SqlDbType.VarChar, 50)
        Dim prmSiteID As New SqlParameter("@siteid", SqlDbType.BigInt)
        Dim prmVersion As New SqlParameter("@version", SqlDbType.Int)
        Dim prmTask As New SqlParameter("@task", SqlDbType.Int)
        Dim prmStatus As New SqlParameter("@status", SqlDbType.Int)
        Dim prmUSRID As New SqlParameter("@usrid", SqlDbType.Int)
        Dim prmRoleID As New SqlParameter("@roleid", SqlDbType.Int)
        Dim prmEventStartTime As New SqlParameter("@eventstarttime", SqlDbType.DateTime)
        Dim prmEventEndTime As New SqlParameter("@eventendtime", SqlDbType.DateTime)
        Dim prmRemarks As New SqlParameter("@remarks", SqlDbType.VarChar, 1000)
        Dim prmCategories As New SqlParameter("@categories", SqlDbType.VarChar, 500)
        Dim prmWPID As New SqlParameter("@wpid", SqlDbType.VarChar, 50)

        prmPONO.Value = info.SiteInf.PONO
        prmSiteID.Value = info.SiteInf.SiteID
        prmVersion.Value = info.SiteInf.SiteVersion
        prmTask.Value = info.TaskInf.TskId
        prmStatus.Value = info.Status
        prmUSRID.Value = info.UserInf.UserId
        prmRoleID.Value = info.UserInf.RoleId
        prmEventStartTime.Value = info.EventStartTime
        prmEventEndTime.Value = info.EventEndTime
        prmRemarks.Value = info.Remarks
        prmCategories.Value = info.Categories
        prmWPID.Value = info.SiteInf.PackageId

        Command.Parameters.Add(prmPONO)
        Command.Parameters.Add(prmSiteID)
        Command.Parameters.Add(prmVersion)
        Command.Parameters.Add(prmTask)
        Command.Parameters.Add(prmStatus)
        Command.Parameters.Add(prmUSRID)
        Command.Parameters.Add(prmRoleID)
        Command.Parameters.Add(prmEventEndTime)
        Command.Parameters.Add(prmEventStartTime)
        Command.Parameters.Add(prmRemarks)
        Command.Parameters.Add(prmCategories)
        Command.Parameters.Add(prmWPID)

        Try
            Connection.Open()
            Command.ExecuteNonQuery()
        Catch ex As Exception
            strErrMessage = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try

        If Not String.IsNullOrEmpty(strErrMessage) Then
            ErrorLogInsert(150, "AuditTrail_I", strErrMessage, "HCPT_uspAuditTrail_I")
        End If

        Return isSucceed
    End Function
#End Region

#Region "User Activity Log"
    Public Function GetUserIDBaseUserLogin(ByVal usrLogin As String) As Integer
        Dim usrid As Integer = 0
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("HCPT_uspActivity_GetUserID", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmUsrId As New SqlParameter("@usrid", SqlDbType.Int)
        Dim prmUsrLogin As New SqlParameter("@usrLogin", SqlDbType.VarChar, 300)

        prmUsrId.Direction = ParameterDirection.Output
        prmUsrLogin.Value = usrLogin

        Command.Parameters.Add(prmUsrId)
        Command.Parameters.Add(prmUsrLogin)

        Try
            Connection.Open()
            Command.ExecuteNonQuery()
            usrid = Integer.Parse(prmUsrId.Value.ToString())
        Catch ex As Exception
            usrid = 0
            strErrMessage = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try

        If Not String.IsNullOrEmpty(strErrMessage) Then
            ErrorLogInsert(150, "GetUserIDBaseUserLogin", strErrMessage, "HCPT_uspActivity_GetUserID")
        End If

        Return usrid
    End Function

    Public Function UserLogActivity_I(ByVal info As UserActivityLogInfo) As Boolean
        Dim isSucceed As Boolean = True
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("HCPT_uspLog_UserActivity_I", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmUserID As New SqlParameter("@usrid", SqlDbType.Int)
        Dim prmDesc As New SqlParameter("@description", SqlDbType.VarChar, 2000)
        Dim prmIPAddress As New SqlParameter("@ipaddress", SqlDbType.VarChar, 2000)


        prmUserID.Value = info.UserId
        prmDesc.Value = info.Description
        prmIPAddress.Value = info.IPAddress

        Command.Parameters.Add(prmUserID)
        Command.Parameters.Add(prmDesc)
        Command.Parameters.Add(prmIPAddress)

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
            ErrorLogInsert(150, "UserLogActivity_I", strErrMessage, "HCPT_uspLog_UserActivity_I")
        End If

        Return isSucceed
    End Function

    Public Function UserLogActivity_LD(ByVal userid As Integer) As List(Of UserActivityLogInfo)
        Dim list As New List(Of UserActivityLogInfo)
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("HCPT_uspActivity_GetLogs", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmUserID As New SqlParameter("@userid", SqlDbType.Int)
        prmUserID.Value = userid
        Command.Parameters.Add(prmUserID)
        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read
                    Dim info As New UserActivityLogInfo
                    info.IPAddress = Convert.ToString(DataReader.Item("IPAddress"))
                    info.ActivityDate = Convert.ToDateTime(DataReader.Item("Activity_Date"))
                    info.Description = Convert.ToString(DataReader.Item("Description"))
                    list.Add(info)
                End While
            End If
        Catch ex As Exception
            strErrMessage = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try

        If Not String.IsNullOrEmpty(strErrMessage) Then
            ErrorLogInsert(150, "UserLogActivity_LD", strErrMessage, "HCPT_uspActivity_GetLogs")
        End If

        Return list
    End Function
#End Region

#Region "Mail Notification"
    Public Function GetPICRejectionRelated(ByVal docid As Integer, ByVal wpid As String) As List(Of UserProfileInfo)
        Dim list As New List(Of UserProfileInfo)
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("HCPT_uspTrans_GetPICRejectionRelated", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmWPID As New SqlParameter("@wpid", SqlDbType.VarChar, 50)
        Dim prmDocId As New SqlParameter("@docid", SqlDbType.Int)

        prmWPID.Value = wpid
        prmDocId.Value = docid

        Command.Parameters.Add(prmWPID)
        Command.Parameters.Add(prmDocId)

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read
                    Dim info As New UserProfileInfo
                    info.UserId = Integer.Parse(DataReader.Item("usr_id").ToString())
                    info.Fullname = Convert.ToString(DataReader.Item("name"))
                    info.SignTitle = Convert.ToString(DataReader.Item("SignTitle"))
                    info.PhoneNo = Convert.ToString(DataReader.Item("PhoneNO"))
                    info.Email = Convert.ToString(DataReader.Item("Email"))
                    list.Add(info)
                End While
            End If
        Catch ex As Exception
            strErrMessage = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try

        If Not String.IsNullOrEmpty(strErrMessage) Then
            ErrorLogInsert(150, "GetPICRejectionRelated", strErrMessage, "HCPT_uspTrans_GetPICRejectionRelated")
        End If

        Return list
    End Function
	
	Public Function HCPT_UpdateDocUpload(ByVal info As Entities.ETSiteDoc) As Boolean
        Dim isSucceed As Boolean = True
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("uspDocUploadUpdate", Connection)
        Command.CommandType = CommandType.StoredProcedure
        Dim prmSiteID As New SqlParameter("@siteid", SqlDbType.BigInt)
        Dim prmDocID As New SqlParameter("@docid", SqlDbType.Int)
        Dim prmIsuploaded As New SqlParameter("@isuploaded", SqlDbType.Int)
        Dim prmDocpath As New SqlParameter("@docpath", SqlDbType.VarChar, 2000)
        Dim prmVersion As New SqlParameter("@version", SqlDbType.Int)
        Dim prmKeyval As New SqlParameter("@keyval", SqlDbType.Int)
        Dim prmRStatus As New SqlParameter("@Rstatus", SqlDbType.Int)
        Dim prmLMBY As New SqlParameter("@lmby", SqlDbType.VarChar, 20)
        Dim prmOrgDocpath As New SqlParameter("@orgdocpath", SqlDbType.VarChar, 500)
        Dim prmPONO As New SqlParameter("@pono", SqlDbType.VarChar, 100)

        prmSiteID.Value = info.SiteID
        prmDocID.Value = info.DocId
        prmIsuploaded.Value = info.IsUploded
        prmDocpath.Value = info.DocPath
        prmVersion.Value = info.Version
        prmKeyval.Value = info.keyval
        prmRStatus.Value = info.AT.RStatus
        prmLMBY.Value = info.AT.LMBY
        prmOrgDocpath.Value = info.orgDocPath
        prmPONO.Value = info.PONo

        Command.Parameters.Add(prmSiteID)
        Command.Parameters.Add(prmDocID)
        Command.Parameters.Add(prmIsuploaded)
        Command.Parameters.Add(prmDocpath)
        Command.Parameters.Add(prmVersion)
        Command.Parameters.Add(prmKeyval)
        Command.Parameters.Add(prmRStatus)
        Command.Parameters.Add(prmLMBY)
        Command.Parameters.Add(prmOrgDocpath)
        Command.Parameters.Add(prmPONO)

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
            ErrorLogInsert(150, "HCPTController:HCPT_UpdateDocUpload", strErrMessage, "uspDocUploadUpdate")
        End If
        Return isSucceed
    End Function
	
	Public Function HPCT_GetChildDoc_All(ByVal siteid As String, ByVal poid As Integer, ByVal pono As String, ByVal wpid As String, ByVal wctrid As Integer) As DataTable
        Dim dtResult As New DataTable
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("HCPT_uspSiteDocUploadTree_GetChildDoc", Connection)
        Command.CommandType = CommandType.StoredProcedure
        Dim prmSiteID As New SqlParameter("@siteid", SqlDbType.VarChar, 500)
        Dim prmPOID As New SqlParameter("@po_id", SqlDbType.Int)
        Dim prmPONO As New SqlParameter("@pono", SqlDbType.VarChar, 200)
        Dim prmWPID As New SqlParameter("@wpid", SqlDbType.VarChar, 50)
        Dim prmWCTRID As New SqlParameter("@wctrid", SqlDbType.Int)
        prmSiteID.Value = siteid
        prmPOID.Value = poid
        prmPONO.Value = pono
        prmWPID.Value = wpid
        prmWCTRID.Value = wctrid
        Command.Parameters.Add(prmSiteID)
        Command.Parameters.Add(prmPOID)
        Command.Parameters.Add(prmPONO)
        Command.Parameters.Add(prmWPID)
        Command.Parameters.Add(prmWCTRID)

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader
            If DataReader.HasRows Then
                dtResult.Load(DataReader)
            End If
        Catch ex As Exception
            strErrMessage = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try

        If (Not String.IsNullOrEmpty(strErrMessage)) Then
            ErrorLogInsert(150, "HCPTController:HPCT_GetChildDoc_All", strErrMessage, "HCPT_uspSiteDocUploadTree_GetChildDoc")
        End If
        Return dtResult
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
