Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections.Generic

Public Class SOACDocController
    Inherits BaseController

    Public Function GetMasterAdditionalDocuments(ByVal isDeleted As Boolean) As List(Of CODDocumentInfo)
        Dim list As New List(Of CODDocumentInfo)
        Dim isSucceed As Boolean = True
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("uspCODSOAC_GetAdditionalDocuments", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmIsDeleted As New SqlParameter("@isDeleted", SqlDbType.Bit)
        prmIsDeleted.Value = isDeleted
        Command.Parameters.Add(prmIsDeleted)

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read
                    Dim info As New CODDocumentInfo
                    info.DocId = Integer.Parse(DataReader.Item("doc_id").ToString())
                    info.DocName = Convert.ToString(DataReader.Item("doc_name"))
                    info.DocType = Convert.ToString(DataReader.Item("Doc_Type"))
                    info.DocURL = Convert.ToString(DataReader.Item("Doc_URL"))
                    info.ParentDocName = Convert.ToString(DataReader.Item("parentdocname"))
                    info.Description = Convert.ToString(DataReader.Item("doc_desc"))
                    info.LMBY = Integer.Parse(DataReader.Item("LMBY").ToString())
                    info.LMDT = Convert.ToDateTime(DataReader.Item("LMDT"))
                    info.IsDeleted = Convert.ToBoolean(DataReader.Item("IsDeleted"))
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
            ErrorLogInsert(20, "getMasterAdditionalDocuments", strErrMessage, "uspCODSOAC_GetAdditionalDocuments")
        End If

        Return list
    End Function

    Public Function GetMasterAdditionalDocumentsNotIncludedItSelf(ByVal isDeleted As Boolean, ByVal docid As Integer) As List(Of CODDocumentInfo)
        Dim list As New List(Of CODDocumentInfo)
        Dim isSucceed As Boolean = True
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("uspCODSOAC_GetAdditionalDocumentsNotIncludedItSelf", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmIsDeleted As New SqlParameter("@isDeleted", SqlDbType.Bit)
        Dim prmDocId As New SqlParameter("@docid", SqlDbType.Int)
        prmIsDeleted.Value = isDeleted
        prmDocId.Value = docid
        Command.Parameters.Add(prmIsDeleted)
        Command.Parameters.Add(prmDocId)
        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read
                    Dim info As New CODDocumentInfo
                    info.DocId = Integer.Parse(DataReader.Item("doc_id").ToString())
                    info.DocName = Convert.ToString(DataReader.Item("doc_name"))
                    info.DocType = Convert.ToString(DataReader.Item("doc_type"))
                    info.DocURL = Convert.ToString(DataReader.Item("doc_url"))
                    info.ParentDocName = Convert.ToString(DataReader.Item("parentdocname"))
                    info.Description = Convert.ToString(DataReader.Item("doc_desc"))
                    info.LMBY = Integer.Parse(DataReader.Item("LMBY").ToString())
                    info.LMDT = Convert.ToDateTime(DataReader.Item("LMDT"))
                    info.IsDeleted = Convert.ToBoolean(DataReader.Item("IsDeleted"))
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
            ErrorLogInsert(20, "getMasterAdditionalDocuments", strErrMessage, "uspCODSOAC_GetAdditionalDocuments")
        End If

        Return list
    End Function

    Public Function GetMasterAdditionalDocumentsByParentId(ByVal parentid As Integer, ByVal isDeleted As Boolean) As List(Of CODDocumentInfo)
        Dim list As New List(Of CODDocumentInfo)
        Dim isSucceed As Boolean = True
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("uspCODSOAC_GetAdditionalDocumentsByParentId", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmParentId As New SqlParameter("@parentdocid", SqlDbType.Int)
        Dim prmIsDeleted As New SqlParameter("@isdeleted", SqlDbType.Bit)

        prmIsDeleted.Value = isDeleted
        prmParentId.Value = parentid

        Command.Parameters.Add(prmParentId)
        Command.Parameters.Add(prmIsDeleted)

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read
                    Dim info As New CODDocumentInfo
                    info.DocId = Integer.Parse(DataReader.Item("doc_id").ToString())
                    info.DocName = Convert.ToString(DataReader.Item("doc_name"))
                    info.DocType = Convert.ToString(DataReader.Item("doc_type"))
                    info.DocURL = Convert.ToString(DataReader.Item("doc_url"))
                    info.ParentDocName = Convert.ToString(DataReader.Item("parentdocname"))
                    info.Description = Convert.ToString(DataReader.Item("doc_desc"))
                    info.LMBY = Integer.Parse(DataReader.Item("LMBY").ToString())
                    info.LMDT = Convert.ToDateTime(DataReader.Item("LMDT"))
                    info.IsDeleted = Convert.ToBoolean(DataReader.Item("IsDeleted"))
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
            ErrorLogInsert(20, "GetMasterAdditionalDocumentsByParentId", strErrMessage, "uspCODSOAC_GetAdditionalDocumentsByParentId")
        End If

        Return list
    End Function

    Public Function GetMasterAdditionalDocumentsByParentId_DS(ByVal parentid As Integer, ByVal isDeleted As Boolean) As DataSet
        Dim isSucceed As Boolean = True
        Dim strErrMessage As String = String.Empty
        Dim ds As New DataSet
        Command = New SqlCommand("uspCODSOAC_GetAdditionalDocumentsByParentId", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmParentId As New SqlParameter("@parentdocid", SqlDbType.Int)
        Dim prmIsDeleted As New SqlParameter("@isdeleted", SqlDbType.Bit)

        prmIsDeleted.Value = isDeleted
        prmParentId.Value = parentid

        Command.Parameters.Add(prmParentId)
        Command.Parameters.Add(prmIsDeleted)

        Try
            Connection.Open()
            Dim adapter As New SqlDataAdapter(Command)
            adapter.Fill(ds)
        Catch ex As Exception
            isSucceed = False
            strErrMessage = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try

        If isSucceed = False Then
            ErrorLogInsert(20, "GetMasterAdditionalDocumentsByParentId", strErrMessage, "uspCODSOAC_GetAdditionalDocumentsByParentId")
        End If

        Return ds
    End Function

    Public Function MasterAdditionaldocument_IU(ByVal info As CODDocumentInfo) As Boolean
        Dim isSucceed As Boolean = True
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("uspCODSOAC_AdditionalDocument_IU", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmDocId As New SqlParameter("@docid", SqlDbType.Int)
        Dim prmDocName As New SqlParameter("@docname", SqlDbType.VarChar, 250)
        Dim prmDocDes As New SqlParameter("@docdesc", SqlDbType.VarChar, 500)
        Dim prmParentId As New SqlParameter("@parentdocid", SqlDbType.Int)
        Dim prmLMBY As New SqlParameter("@lmby", SqlDbType.Int)
        Dim prmIsDeleted As New SqlParameter("@isDeleted", SqlDbType.Bit)
        Dim prmDocType As New SqlParameter("@doctype", SqlDbType.VarChar, 2)
        Dim prmDocURL As New SqlParameter("@docurl", SqlDbType.VarChar, 500)

        prmDocId.Value = info.DocId
        prmDocName.Value = info.DocName
        prmDocDes.Value = info.Description
        prmParentId.Value = info.ParentId
        prmLMBY.Value = info.LMBY
        prmIsDeleted.Value = info.IsDeleted
        prmDocType.Value = info.DocType
        prmDocURL.Value = info.DocURL

        Command.Parameters.Add(prmDocId)
        Command.Parameters.Add(prmDocName)
        Command.Parameters.Add(prmDocDes)
        Command.Parameters.Add(prmParentId)
        Command.Parameters.Add(prmLMBY)
        Command.Parameters.Add(prmIsDeleted)
        Command.Parameters.Add(prmDocURL)
        Command.Parameters.Add(prmDocType)

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
            ErrorLogInsert(20, "MasterAdditionalDocument_IU", strErrMessage, "uspCODSOAC_AdditionalDocument_IU")
        End If

        Return isSucceed
    End Function

    Public Function MasterAdditioanDocument_D(ByVal docid As String) As Boolean
        Dim isSucceed As Boolean = True
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("update CODDocument_SOAC set isDeleted='true' where doc_id =" & docid, Connection)
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
            ErrorLogInsert(20, "MasterAdditioanDocument_D", strErrMessage, "CommandType.Text")
        End If
        Return isSucceed
    End Function

    Public Function GetDocBaseSOACLog(ByVal soacid As Int32) As List(Of DocInfo)
        Dim list As New List(Of DocInfo)
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("uspSOAC_GetListDocBaseLog", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmSOACID As New SqlParameter("@soacid", SqlDbType.BigInt)
        prmSOACID.Value = soacid
        Command.Parameters.Add(prmSOACID)

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read
                    Dim info As New DocInfo
                    info.DocId = Integer.Parse(DataReader.Item("docid").ToString())
                    info.DocName = Convert.ToString(DataReader.Item("docname"))
                    list.Add(info)
                End While
            End If
        Catch ex As Exception
            strErrMessage = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try

        If Not String.IsNullOrEmpty(strErrMessage) Then
            ErrorLogInsert(20, "GetDocBaseSOACLog", strErrMessage, "uspSOAC_GetListDocBaseLog")
        End If

        Return list
    End Function

    Public Function GetDocBaseOnParent(ByVal soacid As Int32, ByVal docid As Integer, ByVal doctype As String) As List(Of DocInfo)
        Dim list As New List(Of DocInfo)
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("uspSOAC_GetDocId_BaseOnParent", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmSOACID As New SqlParameter("@soacid", SqlDbType.BigInt)
        Dim prmDocType As New SqlParameter("@doctype", SqlDbType.VarChar, 2)
        Dim prmDocID As New SqlParameter("@docid", SqlDbType.Int)

        prmSOACID.Value = soacid
        prmDocType.Value = doctype
        prmDocID.Value = docid

        Command.Parameters.Add(prmSOACID)
        Command.Parameters.Add(prmDocType)
        Command.Parameters.Add(prmDocID)

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read
                    Dim info As New DocInfo
                    info.DocId = Integer.Parse(DataReader.Item("doc_id").ToString())
                    list.Add(info)
                End While
            End If
        Catch ex As Exception
            strErrMessage = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try

        If Not String.IsNullOrEmpty(strErrMessage) Then
            ErrorLogInsert(20, "GetDocBaseParent", strErrMessage, "uspSOAC_GetDocId_BaseOnParent")
        End If

        Return list
    End Function

    Public Function SOACFormGenerated(ByVal soacid As Int32, ByVal docpath As String, ByVal orgdocpath As String, ByVal lmby As Integer) As Boolean
        Dim isSucceed As Boolean = True
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("uspSOAC_FormGenerated", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmSOACID As New SqlParameter("@soacid", SqlDbType.BigInt)
        Dim prmDocPath As New SqlParameter("@docpath", SqlDbType.VarChar, 2000)
        Dim prmOrgDocPath As New SqlParameter("@orgdocpath", SqlDbType.VarChar, 2000)
        Dim prmLMBY As New SqlParameter("@lmby", SqlDbType.Int)

        prmSOACID.Value = soacid
        prmDocPath.Value = docpath
        prmOrgDocPath.Value = orgdocpath
        prmLMBY.Value = lmby

        Command.Parameters.Add(prmSOACID)
        Command.Parameters.Add(prmDocPath)
        Command.Parameters.Add(prmOrgDocPath)
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

        If Not String.IsNullOrEmpty(strErrMessage) Then
            ErrorLogInsert(20, "SOACFormGenerated", strErrMessage, "uspSOAC_FormGenerated")
        End If

        Return isSucceed
    End Function

    Public Function SOACFormRemoved(ByVal docid As Integer, ByVal soacid As Int32, ByVal lmby As Integer) As Boolean
        Dim isSucceed As Boolean = True
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("uspSOAC_FormRemove", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmSOACID As New SqlParameter("@soacid", SqlDbType.BigInt)
        Dim prmDocID As New SqlParameter("@docid", SqlDbType.Int)
        Dim prmLMBY As New SqlParameter("@lmby", SqlDbType.Int)

        prmSOACID.Value = soacid
        prmDocID.Value = docid
        prmLMBY.Value = lmby

        Command.Parameters.Add(prmSOACID)
        Command.Parameters.Add(prmDocID)
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

        If Not String.IsNullOrEmpty(strErrMessage) Then
            ErrorLogInsert(20, "SOACFormRemoved", strErrMessage, "uspSOAC_FormRemove")
        End If

        Return isSucceed
    End Function

    Public Function RemoveChildDocUpload(ByVal docid As Integer, ByVal soacid As Int32) As Boolean
        Dim isSucceed As Boolean = True
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("uspSOAC_RemoveChildDocsUploaded", Connection)
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
            isSucceed = False
        Finally
            Connection.Close()
        End Try

        Return isSucceed
    End Function

    Public Function GetSOACDocumentBasePackageID(ByVal packageid As String, ByVal userid As Integer) As List(Of SOACTransactionInfo)
        Dim list As New List(Of SOACTransactionInfo)
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("uspSOAC_GetDocument_BasePackageID", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmUserID As New SqlParameter("@usrid", SqlDbType.Int)
        Dim prmPackageID As New SqlParameter("@packageid", SqlDbType.VarChar, 50)

        prmUserID.Value = userid
        prmPackageID.Value = packageid

        Command.Parameters.Add(prmUserID)
        Command.Parameters.Add(prmPackageID)

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read
                    Dim info As New SOACTransactionInfo
                    info.SOACInfo.SOACID = Convert.ToInt32(DataReader.Item("SOAC_id"))
                    If Not String.IsNullOrEmpty(Convert.ToString(DataReader.Item("OnAir_date"))) Then
                        info.SOACInfo.SiteOnAirDate = Convert.ToDateTime(DataReader.Item("OnAir_Date"))
                    End If
                    If Not String.IsNullOrEmpty(Convert.ToString(DataReader.Item("PODate"))) Then
                        info.SOACInfo.PORefNoDate = Convert.ToDateTime(DataReader.Item("PODate"))
                    End If
                    info.SOACInfo.SiteInf.SiteNo = Convert.ToString(DataReader.Item("SiteNo"))
                    info.SOACInfo.SiteInf.SiteName = Convert.ToString(DataReader.Item("SiteName"))
                    info.SOACInfo.SiteInf.PONO = Convert.ToString(DataReader.Item("PoNO"))
                    info.SOACInfo.SiteInf.PackageId = Convert.ToString(DataReader.Item("Package_Id"))
                    info.SOACInfo.SiteInf.Scope = Convert.ToString(DataReader.Item("Scope"))
                    info.DocInf.DocId = Integer.Parse(DataReader.Item("DocId").ToString())
                    info.DocInf.DocName = Convert.ToString(DataReader.Item("DocName"))

                    If Not String.IsNullOrEmpty(Convert.ToString(DataReader.Item("upload_date"))) Then
                        info.SOACMSInfo.UploadDate = Convert.ToDateTime(DataReader.Item("Upload_Date"))
                    End If

                    If Not String.IsNullOrEmpty(Convert.ToString(DataReader.Item("submit_date"))) Then
                        info.SOACMSInfo.SubmitDate = Convert.ToDateTime(DataReader.Item("Submit_Date"))
                    End If

                    info.SOACInfo.DocPath = Convert.ToString(DataReader.Item("DocPath"))
                    info.SOACInfo.OrgDocPath = Convert.ToString(DataReader.Item("OrgDocPath"))
                    info.SOACInfo.IsUploaded = Convert.ToBoolean(DataReader.Item("isUploaded"))
                    list.Add(info)
                End While
            End If

        Catch ex As Exception
            strErrMessage = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try

        If Not String.IsNullOrEmpty(strErrMessage) Then
            ErrorLogInsert(20, "GetSOACDocumentBasePackageID", strErrMessage, "uspSOAC_GetDocument_BasePackageID")
        End If

        Return list
    End Function

    Public Function GetSOACDocumentDetail(ByVal soacid As Int32, ByVal docid As Integer) As ODSOACDocumentInfo
        Dim info As New ODSOACDocumentInfo
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("uspSOAC_GetDocumentDetail", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmSOACID As New SqlParameter("soacid", SqlDbType.BigInt)
        Dim prmDOCID As New SqlParameter("@docid", SqlDbType.Int)

        prmSOACID.Value = soacid
        prmDOCID.Value = docid

        Command.Parameters.Add(prmSOACID)
        Command.Parameters.Add(prmDOCID)

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read
                    info.SOACID = soacid
                    info.DocInfo.DocId = docid
                    info.DocInfo.DocName = Convert.ToString(DataReader.Item("docname"))
                    info.DocPath = Convert.ToString(DataReader.Item("DocPath"))
                    info.OrgDocPath = Convert.ToString(DataReader.Item("OrgDocPath"))
                    info.LMBY = Integer.Parse(DataReader.Item("LMBY").ToString())
                    info.LMDT = Convert.ToDateTime(DataReader.Item("LMDT"))
                End While
            End If
        Catch ex As Exception
            strErrMessage = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try

        If Not String.IsNullOrEmpty(strErrMessage) Then
            ErrorLogInsert(20, "GetSOACDocumentDetail", strErrMessage, "uspSOAC_GetDocumentDetail")
        End If

        Return info
    End Function

    Public Function GetDOCParentID(ByVal docid As Integer) As Integer
        Dim parentdocid As Integer = 0
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("uspSOAC_GetDocParentID", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmDOCID As New SqlParameter("@docid", SqlDbType.Int)
        Dim prmParentdocid As New SqlParameter("@parentdocid", SqlDbType.Int)
        prmDOCID.Value = docid
        prmParentdocid.Direction = ParameterDirection.Output
        Command.Parameters.Add(prmDOCID)
        Command.Parameters.Add(prmParentdocid)

        Try
            Connection.Open()
            Command.ExecuteNonQuery()
            parentdocid = Integer.Parse(prmParentdocid.Value.ToString())
        Catch ex As Exception
            strErrMessage = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try

        If Not String.IsNullOrEmpty(strErrMessage) Then
            ErrorLogInsert(20, "GetDOCParentID", strErrMessage, "uspSOAC_GetDocParentID")
        End If

        Return parentdocid
    End Function

#Region "SOAC Child Document Get, Add, Modify"

    Public Function SOACChildDocument_IU(ByVal info As ODSOACDocumentInfo) As Boolean
        Dim isSucceed As Boolean = True
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("uspSOAC_Document_IU", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmSOACID As New SqlParameter("@soacid", SqlDbType.BigInt)
        Dim prmSNO As New SqlParameter("@sno", SqlDbType.BigInt)
        Dim prmDocId As New SqlParameter("@docid", SqlDbType.Int)
        Dim prmDocPath As New SqlParameter("@docpath", SqlDbType.VarChar, 2000)
        Dim prmOrgDocPath As New SqlParameter("@orgdocpath", SqlDbType.VarChar, 2000)
        Dim prmLMBY As New SqlParameter("@lmby", SqlDbType.Int)
        Dim prmIsUploaded As New SqlParameter("@isUploaded", SqlDbType.Bit)
        Dim prmIsDeleted As New SqlParameter("@isdeleted", SqlDbType.Bit)

        prmSOACID.Value = info.SOACID
        prmSNO.Value = info.SNO
        prmDocPath.Value = info.DocPath
        prmDocId.Value = info.DocInfo.DocId
        prmOrgDocPath.Value = info.OrgDocPath
        prmLMBY.Value = info.LMBY
        prmIsDeleted.Value = info.IsDeleted
        prmIsUploaded.Value = info.IsUploaded

        Command.Parameters.Add(prmSOACID)
        Command.Parameters.Add(prmDocId)
        Command.Parameters.Add(prmSNO)
        Command.Parameters.Add(prmDocPath)
        Command.Parameters.Add(prmOrgDocPath)
        Command.Parameters.Add(prmLMBY)
        Command.Parameters.Add(prmIsUploaded)
        Command.Parameters.Add(prmIsDeleted)

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
            ErrorLogInsert(20, "SOACChildDocument_IU", strErrMessage, "uspSOAC_Document_IU")
        End If
        Return isSucceed
    End Function

    Public Function GetSOACChildDocuments(ByVal soacid As Int32, ByVal docid As Integer, ByVal isDeleted As Boolean) As List(Of ODSOACDocumentInfo)
        Dim list As New List(Of ODSOACDocumentInfo)
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("uspSOAC_GetChildDocuments", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmSOACID As New SqlParameter("@soacid", SqlDbType.BigInt)
        Dim prmDOCID As New SqlParameter("@docid", SqlDbType.Int)
        prmDOCID.Value = docid
        Dim prmIsDeleted As New SqlParameter("@isdeleted", SqlDbType.Bit)
        prmSOACID.Value = soacid
        prmIsDeleted.Value = isDeleted
        Command.Parameters.Add(prmDOCID)
        Command.Parameters.Add(prmSOACID)
        Command.Parameters.Add(prmIsDeleted)
        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read
                    Dim info As New ODSOACDocumentInfo
                    info.SNO = Convert.ToInt32(DataReader.Item("sno"))
                    info.DocInfo.DocId = Integer.Parse(DataReader.Item("doc_id").ToString())
                    info.DocInfo.DocName = Convert.ToString(DataReader.Item("doc_name"))
                    info.DocPath = Convert.ToString(DataReader.Item("docpath"))
                    info.OrgDocPath = Convert.ToString(DataReader.Item("orgdocpath"))
                    info.LMBY = Integer.Parse(DataReader.Item("lmby").ToString())
                    info.ModifiedUser = Convert.ToString(DataReader.Item("modifieduser"))
                    info.LMDT = Convert.ToDateTime(DataReader.Item("LMDT"))
                    info.IsUploaded = Convert.ToBoolean(DataReader.Item("isUploaded"))
                    info.IsDeleted = Convert.ToBoolean(DataReader.Item("isDeleted"))
                    info.SOACID = Convert.ToInt32(DataReader.Item("soac_id"))
                    info.DocInfo.DocType = Convert.ToString(DataReader.Item("doc_type"))
                    info.DocInfo.DocURL = Convert.ToString(DataReader.Item("doc_url"))
                    list.Add(info)
                End While
            End If
        Catch ex As Exception
            strErrMessage = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try

        If Not String.IsNullOrEmpty(strErrMessage) Then
            ErrorLogInsert(20, "GetSOACChildDocuments", strErrMessage, "uspSOAC_GetChildDocuments")
        End If

        Return list
    End Function

    Public Function GetDocumentsNotInSOACChildDoc(ByVal soacid As Int32) As List(Of CODDocumentInfo)
        Dim list As New List(Of CODDocumentInfo)
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("uspCODSOAC_GetDocuments", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmSOACID As New SqlParameter("@soacid", SqlDbType.BigInt)
        prmSOACID.Value = soacid
        Command.Parameters.Add(prmSOACID)

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read
                    Dim info As New CODDocumentInfo
                    info.DocId = Integer.Parse(DataReader.Item("doc_id").ToString())
                    info.DocName = Convert.ToString(DataReader.Item("doc_name"))
                    info.DocType = Convert.ToString(DataReader.Item("doc_type"))
                    info.DocURL = Convert.ToString(DataReader.Item("doc_url"))
                    info.Description = Convert.ToString(DataReader.Item("doc_desc"))
                    info.LMBY = Integer.Parse(DataReader.Item("LMBY").ToString())
                    info.LMDT = Convert.ToDateTime(DataReader.Item("LMDT"))
                    info.IsDeleted = Convert.ToBoolean(DataReader.Item("IsDeleted"))
                    list.Add(info)
                End While
            End If
        Catch ex As Exception
            strErrMessage = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try

        If Not String.IsNullOrEmpty(strErrMessage) Then
            ErrorLogInsert(20, "GetDocumentsNotInSOACChildDoc", strErrMessage, "uspCODSOAC_GetDocuments")
        End If

        Return list
    End Function

    Public Function SOACChildDocument_D(ByVal sno As Int32) As Boolean
        Dim isSucceed As Boolean = True
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("uspSOAC_ChildDocument_D", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmSNO As New SqlParameter("@sno", SqlDbType.BigInt)
        prmSNO.Value = sno
        Command.Parameters.Add(prmSNO)

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
            ErrorLogInsert(20, "SOACChildDocument_D", strErrMessage, "uspSOAC_ChildDocument_D")
        End If

        Return isSucceed
    End Function

    Public Function RemoveAttachementDocUploaded(ByVal sno As Int32, ByVal lmby As Integer) As Boolean
        Dim isSucceed As Boolean = True
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("uspSOAC_RemoveAttachmentDocUpload", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmLMBY As New SqlParameter("@lmby", SqlDbType.Int)
        Dim prmSNO As New SqlParameter("@sno", SqlDbType.BigInt)

        prmLMBY.Value = lmby
        prmSNO.Value = sno

        Command.Parameters.Add(prmLMBY)
        Command.Parameters.Add(prmSNO)
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
            ErrorLogInsert(20, "RemoveAttachmentDocUploaded", strErrMessage, "uspSOAC_RemoveAttachmentDocUpload")
        End If

        Return isSucceed
    End Function

    Public Function IsCompleteChildDocumentChecking(ByVal soacid As Int32) As Boolean
        Dim isComplete As Boolean = True
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("uspSOAC_CompleteAttachmentDocChecking", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmSOACID As New SqlParameter("@soacid", SqlDbType.BigInt)
        Dim prmIsComplete As New SqlParameter("@iscomplete", SqlDbType.Bit)
        prmSOACID.Value = soacid
        prmIsComplete.Direction = ParameterDirection.Output
        Command.Parameters.Add(prmSOACID)
        Command.Parameters.Add(prmIsComplete)

        Try
            Connection.Open()
            Command.ExecuteNonQuery()
            isComplete = Convert.ToBoolean(prmIsComplete.Value)
        Catch ex As Exception
            strErrMessage = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try

        If Not String.IsNullOrEmpty(strErrMessage) Then
            ErrorLogInsert(20, "IsCompleteChildDocumentChecking", strErrMessage, "uspSOAC_CompleteAttachmentDocChecking")
        End If

        Return isComplete
    End Function
#End Region

#Region "SOAC Doc Count"
    Public Function GetSOACDocCount(ByVal userid As Integer, ByVal soacid As Int32) As Integer
        Dim doccount As Integer = 0
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("uspSOAC_DocCount", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmSOACID As New SqlParameter("@soacid", SqlDbType.BigInt)
        Dim prmUserID As New SqlParameter("@usrid", SqlDbType.Int)
        Dim prmRowCount As New SqlParameter("@rowcount", SqlDbType.Int)

        prmSOACID.Value = soacid
        prmUserID.Value = userid
        prmRowCount.Direction = ParameterDirection.Output

        Command.Parameters.Add(prmSOACID)
        Command.Parameters.Add(prmUserID)
        Command.Parameters.Add(prmRowCount)

        Try
            Connection.Open()
            Command.ExecuteNonQuery()
            doccount = Integer.Parse(prmRowCount.Value.ToString())
        Catch ex As Exception
            strErrMessage = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try

        If Not String.IsNullOrEmpty(strErrMessage) Then
            ErrorLogInsert(20, "GetSOACDocCount", strErrMessage, "uspSOAC_DocCount")
        End If

        Return doccount
    End Function

    Public Function GetSOACPendingDocBaseonUserTask(ByVal usertask As String, ByVal soacid As Int32, ByVal userid As Integer) As List(Of SOACTransactionInfo)
        Dim list As New List(Of SOACTransactionInfo)
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("uspSOAC_GetDocumentPending_BaseUserTask", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmUserID As New SqlParameter("@usrid", SqlDbType.Int)
        Dim prmSOACID As New SqlParameter("@soacid", SqlDbType.BigInt)
        Dim prmUserTask As New SqlParameter("@usertask", SqlDbType.VarChar, 20)

        prmUserID.Value = userid
        prmSOACID.Value = soacid
        prmUserTask.Value = usertask

        Command.Parameters.Add(prmUserID)
        Command.Parameters.Add(prmSOACID)
        Command.Parameters.Add(prmUserTask)

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read
                    Dim info As New SOACTransactionInfo
                    info.SNO = Convert.ToInt32(DataReader.Item("sno"))
                    info.SOACInfo.SiteInf.SiteNo = Convert.ToString(DataReader.Item("SiteNo"))
                    info.SOACInfo.SiteInf.SiteName = Convert.ToString(DataReader.Item("SiteName"))
                    info.SOACInfo.SiteInf.SiteIdPO = Convert.ToString(DataReader.Item("SiteIDPO"))
                    info.SOACInfo.SiteInf.SiteNamePO = Convert.ToString(DataReader.Item("SitenamePO"))
                    info.SOACInfo.SiteInf.PackageId = Convert.ToString(DataReader.Item("workpackageid"))
                    info.SOACInfo.SiteInf.Scope = Convert.ToString(DataReader.Item("Scope"))
                    info.SOACInfo.SiteInf.PONO = Convert.ToString(DataReader.Item("PONO"))
                    info.SOACInfo.SiteInf.POName = Convert.ToString(DataReader.Item("POName"))
                    info.SOACInfo.SOACID = Convert.ToInt32(DataReader.Item("SOAC_ID"))
                    info.DocInf.DocId = Integer.Parse(DataReader.Item("Doc_Id").ToString())
                    info.DocInf.DocName = Convert.ToString(DataReader.Item("DocName"))
                    If Not String.IsNullOrEmpty(Convert.ToString(DataReader.Item("Submit_date"))) Then
                        info.SOACMSInfo.SubmitDate = Convert.ToDateTime(DataReader.Item("Submit_Date"))
                    End If

                    If Not String.IsNullOrEmpty(Convert.ToString(DataReader.Item("podate"))) Then
                        info.SOACInfo.PORefNoDate = Convert.ToDateTime(DataReader.Item("podate"))
                    End If

                    If Not String.IsNullOrEmpty(Convert.ToString(DataReader.Item("onairdate"))) Then
                        info.SOACInfo.OnAirDate = Convert.ToDateTime(DataReader.Item("onairdate"))
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
            ErrorLogInsert(20, "GetSOACPendingDocBaseonUserTask", strErrMessage, "uspSOAC_GetDocumentPending_BaseUserTask")
        End If

        Return list
    End Function

    Public Function GetSOACApprovedDocBaseonUserTask(ByVal soacid As Int32, ByVal userid As Integer) As List(Of SOACTransactionInfo)
        Dim list As New List(Of SOACTransactionInfo)
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("uspSOAC_GetDocumentApproved_BaseUserTask", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmUserID As New SqlParameter("@usrid", SqlDbType.Int)
        Dim prmSOACID As New SqlParameter("@soacid", SqlDbType.BigInt)

        prmUserID.Value = userid
        prmSOACID.Value = soacid

        Command.Parameters.Add(prmUserID)
        Command.Parameters.Add(prmSOACID)

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read
                    Dim info As New SOACTransactionInfo
                    info.SNO = Convert.ToInt32(DataReader.Item("sno"))
                    info.SOACInfo.SiteInf.SiteNo = Convert.ToString(DataReader.Item("SiteNo"))
                    info.SOACInfo.SiteInf.SiteName = Convert.ToString(DataReader.Item("SiteName"))
                    info.SOACInfo.SiteInf.SiteIdPO = Convert.ToString(DataReader.Item("SiteIDPO"))
                    info.SOACInfo.SiteInf.SiteNamePO = Convert.ToString(DataReader.Item("SitenamePO"))
                    info.SOACInfo.SiteInf.Scope = Convert.ToString(DataReader.Item("Scope"))
                    info.SOACInfo.SiteInf.PONO = Convert.ToString(DataReader.Item("PONO"))
                    info.SOACInfo.SiteInf.POName = Convert.ToString(DataReader.Item("POName"))
                    info.SOACInfo.SiteInf.PackageId = Convert.ToString(DataReader.Item("workpackageid"))
                    info.SOACInfo.SOACID = Convert.ToInt32(DataReader.Item("SOAC_ID"))
                    info.DocInf.DocId = Integer.Parse(DataReader.Item("Doc_Id").ToString())
                    info.DocInf.DocName = Convert.ToString(DataReader.Item("DocName"))
                    If Not String.IsNullOrEmpty(Convert.ToString(DataReader.Item("Submit_date"))) Then
                        info.SOACMSInfo.SubmitDate = Convert.ToDateTime(DataReader.Item("Submit_Date"))
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
            ErrorLogInsert(20, "GetSOACApprovedDocBaseonUserTask", strErrMessage, "uspSOAC_GetDocumentApproved_BaseUserTask")
        End If

        Return list
    End Function

    Public Function ParentIsUploaded(ByVal parentdocid As Integer, ByVal soacid As Int32) As Boolean
        Dim isUploaded As Boolean = True
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("uspSOAC_IsParentIsUploaded", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmSOACID As New SqlParameter("@soacid", SqlDbType.BigInt)
        Dim prmDOCID As New SqlParameter("@parentdocid", SqlDbType.Int)
        Dim prmIsUploaded As New SqlParameter("@isuploaded", SqlDbType.Bit)

        prmSOACID.Value = soacid
        prmDOCID.Value = parentdocid
        prmIsUploaded.Direction = ParameterDirection.Output

        Command.Parameters.Add(prmSOACID)
        Command.Parameters.Add(prmDOCID)
        Command.Parameters.Add(prmIsUploaded)

        Try
            Connection.Open()
            Command.ExecuteNonQuery()
            isUploaded = Convert.ToBoolean(prmIsUploaded.Value)
        Catch ex As Exception
            strErrMessage = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try

        If Not String.IsNullOrEmpty(strErrMessage) Then
            ErrorLogInsert(20, "ParentIsUploaded", strErrMessage, "uspSOAC_IsParentIsUploaded")
        End If

        Return isUploaded
    End Function

#End Region

#Region "Digital Signature Doc for SOAC"
    Public Function DGPassword_IU(ByVal info As DigitalSignInfo) As Boolean
        Dim isSucceed As Boolean = True
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("uspSOAC_DGPassword_IU", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmUserId As New SqlParameter("@usrid", SqlDbType.Int)
        Dim prmPassword As New SqlParameter("@password", SqlDbType.VarChar, 6)

        prmUserId.Value = info.UserInfo.UserId
        prmPassword.Value = info.DGPassword

        Command.Parameters.Add(prmUserId)
        Command.Parameters.Add(prmPassword)

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
            ErrorLogInsert(20, "DGPassword_IU", strErrMessage, "uspSOAC_DGPassword_IU")
        End If

        Return isSucceed
    End Function

    Public Function DGPassword_CheckIsExp(ByVal usrid As Integer) As Boolean
        Dim isExpired As Boolean = True
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("uspSOAC_DGPassword_CheckExpDate", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmUserId As New SqlParameter("@usrid", SqlDbType.Int)
        Dim prmIsExpired As New SqlParameter("@isexpired", SqlDbType.Bit)

        prmUserId.Value = usrid
        prmIsExpired.Direction = ParameterDirection.Output

        Command.Parameters.Add(prmUserId)
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
            isExpired = False
            ErrorLogInsert(20, "DGPassword_CheckIsExp", strErrMessage, "uspSOAC_DGPassword_CheckExpDate")
        End If

        Return isExpired
    End Function

    Public Function DGPassword_Validation(ByVal userid As Integer, ByVal password As String) As String
        Dim usrLogin As String = String.Empty
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("uspSOAC_DGPassword_Validation", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmUserID As New SqlParameter("@usrid", SqlDbType.Int)
        Dim prmPassword As New SqlParameter("@password", SqlDbType.VarChar, 6)
        Dim prmUsrLogin As New SqlParameter("@usrLogin", SqlDbType.VarChar, 200)

        prmUserID.Value = userid
        prmPassword.Value = password
        prmUsrLogin.Direction = ParameterDirection.Output

        Command.Parameters.Add(prmUserID)
        Command.Parameters.Add(prmPassword)
        Command.Parameters.Add(prmUsrLogin)

        Try
            Connection.Open()
            Command.ExecuteNonQuery()
            If Not String.IsNullOrEmpty(Convert.ToString(prmUsrLogin.Value)) Then
                usrLogin = Convert.ToString(prmUsrLogin.Value)
            End If
        Catch ex As Exception
            strErrMessage = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try

        If Not String.IsNullOrEmpty(strErrMessage) Then
            ErrorLogInsert(20, "DGPassword_Validation", strErrMessage, "uspSOAC_DGPassword_Validation")
        End If

        Return usrLogin
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
