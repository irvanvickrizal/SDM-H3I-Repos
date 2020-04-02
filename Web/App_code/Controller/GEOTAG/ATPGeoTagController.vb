Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections.Generic

Public Class ATPGeoTagController
    Inherits BaseController

    Public Function GetListATPPhotoDocs(ByVal sconid As Integer, ByVal scope As String, ByVal atpdocid As Integer) As List(Of GeoTagInfo)
        Dim list As New List(Of GeoTagInfo)

        Command = New SqlCommand("uspGeo_GetAvailableATPDocs", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmScope As New SqlParameter("@scope", SqlDbType.VarChar, 200)
        Dim prmSconId As New SqlParameter("@sconid", SqlDbType.Int)
        Dim prmATPDOCID As New SqlParameter("@ATPDOCID", SqlDbType.Int)

        prmScope.Value = scope
        prmSconId.Value = sconid
        prmATPDOCID.Value = atpdocid

        Command.Parameters.Add(prmSconId)
        Command.Parameters.Add(prmScope)
        Command.Parameters.Add(prmATPDOCID)

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()

            If DataReader.HasRows Then
                While (DataReader.Read)
                    Dim info As New GeoTagInfo
                    info.ATPDocPhotoId = Convert.ToString(DataReader.Item("ATPPhotoDoc_id"))
                    If Not String.IsNullOrEmpty(Convert.ToString(DataReader.Item("readydate"))) Then
                        info.ReadyDate = Convert.ToDateTime(DataReader.Item("readydate"))
                    End If
                    info.ATPDOCPath = Convert.ToString(DataReader.Item("ATPPath"))
                    info.ATPHTMLDocPath = Convert.ToString(DataReader.Item("HTMLPath"))
                    info.SiteNo = Convert.ToString(DataReader.Item("SiteNo"))
                    info.SiteName = Convert.ToString(DataReader.Item("Sitename"))
                    info.PONO = Convert.ToString(DataReader.Item("PONO"))
                    info.PackageId = Convert.ToString(DataReader.Item("workpackageid"))
                    info.Scope = Convert.ToString(DataReader.Item("scope"))
                    info.EOName = Convert.ToString(DataReader.Item("POName"))
                    info.UserUploadId = Integer.Parse(DataReader.Item("usr_id").ToString())
                    info.UserUploadName = Convert.ToString(DataReader.Item("engineername"))
                    info.CompanyName = Convert.ToString(DataReader.Item("scon_name"))
                    info.SWID = Convert.ToInt32(DataReader.Item("SWID"))
                    info.SiteDocPath = Convert.ToString(DataReader.Item("docpath"))
                    info.SiteOrgDocPath = Convert.ToString(DataReader.Item("orgdocpath"))
                    list.Add(info)
                End While
            End If

        Catch ex As Exception
        Finally
            Connection.Close()
        End Try



        Return list
    End Function

    Public Function GetATPPhotoDocById(ByVal sconid As Integer, ByVal scope As String, ByVal atpdocid As Integer, ByVal atpphotodocid As String) As GeoTagInfo
        Dim info As New GeoTagInfo

        Command = New SqlCommand("uspGeo_GetAvailableATPDocById", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmScope As New SqlParameter("@scope", SqlDbType.VarChar, 200)
        Dim prmSconId As New SqlParameter("@sconid", SqlDbType.Int)
        Dim prmATPDOCID As New SqlParameter("@ATPDOCID", SqlDbType.Int)
        Dim prmATPPhotoDocId As New SqlParameter("@atpphotodocid", SqlDbType.VarChar, 100)

        prmScope.Value = scope
        prmSconId.Value = sconid
        prmATPDOCID.Value = atpdocid
        prmATPPhotoDocId.Value = atpphotodocid

        Command.Parameters.Add(prmSconId)
        Command.Parameters.Add(prmScope)
        Command.Parameters.Add(prmATPDOCID)
        Command.Parameters.Add(prmATPPhotoDocId)

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()

            If DataReader.HasRows Then
                While (DataReader.Read)
                    info.ATPDocPhotoId = Convert.ToString(DataReader.Item("ATPPhotoDoc_id"))
                    If Not String.IsNullOrEmpty(Convert.ToString(DataReader.Item("readydate"))) Then
                        info.ReadyDate = Convert.ToDateTime(DataReader.Item("readydate"))
                    End If
                    info.ATPDOCPath = Convert.ToString(DataReader.Item("ATPPath"))
                    info.ATPHTMLDocPath = Convert.ToString(DataReader.Item("HTMLPath"))
                    info.SiteNo = Convert.ToString(DataReader.Item("SiteNo"))
                    info.SiteName = Convert.ToString(DataReader.Item("Sitename"))
                    info.PONO = Convert.ToString(DataReader.Item("PONO"))
                    info.PackageId = Convert.ToString(DataReader.Item("workpackageid"))
                    info.Scope = Convert.ToString(DataReader.Item("scope"))
                    info.EOName = Convert.ToString(DataReader.Item("POName"))
                    info.UserUploadId = Integer.Parse(DataReader.Item("usr_id").ToString())
                    info.UserUploadName = Convert.ToString(DataReader.Item("engineername"))
                    info.CompanyName = Convert.ToString(DataReader.Item("scon_name"))
                    info.SWID = Convert.ToInt32(DataReader.Item("SW_ID"))
                    info.SiteDocPath = Convert.ToString(DataReader.Item("docpath"))
                    info.SiteOrgDocPath = Convert.ToString(DataReader.Item("orgdocpath"))
                End While
            End If

        Catch ex As Exception
        Finally
            Connection.Close()
        End Try

        Return info
    End Function

    Public Function GetATPPhotoDoc(ByVal id As String) As GeoTagInfo
        Dim info As New GeoTagInfo
        Command = New SqlCommand("uspGeo_GetATPDocPhotoById", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmID As New SqlParameter("@id", SqlDbType.VarChar, 500)
        prmID.Value = id
        Command.Parameters.Add(prmID)

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read
                    info.ATPDocPhotoId = Convert.ToString(DataReader.Item("id"))
                    info.ATPDOCPath = Convert.ToString(DataReader.Item("ATPPath"))
                    info.ATPHTMLDocPath = Convert.ToString(DataReader.Item("HTMLPath"))
                End While
            End If
        Catch ex As Exception

        Finally
            Connection.Close()
        End Try
        Return info

    End Function

    Public Function ATPDocumentChecklist_IU(ByVal atpphotodocid As String, ByVal originaldocpath As String, ByVal lmby As Integer, ByVal isuploaded As Boolean) As Boolean
        Dim isSucceed As Boolean = True
        Command = New SqlCommand("uspGeo_ATPChecklistDocument_IU", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmATPPhotoDocId As New SqlParameter("@atpphotodocid", SqlDbType.VarChar, 500)
        Dim prmOriginalDocPath As New SqlParameter("@originaldocpath", SqlDbType.VarChar, 2000)
        Dim prmLMBY As New SqlParameter("@lmby", SqlDbType.Int)
        Dim prmIsUploaded As New SqlParameter("@isuploaded", SqlDbType.Bit)

        prmATPPhotoDocId.Value = atpphotodocid
        prmOriginalDocPath.Value = originaldocpath
        prmLMBY.Value = lmby
        prmIsUploaded.Value = isuploaded

        Command.Parameters.Add(prmATPPhotoDocId)
        Command.Parameters.Add(prmOriginalDocPath)
        Command.Parameters.Add(prmLMBY)
        Command.Parameters.Add(prmIsUploaded)

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

    Public Function GetATPDocumentChecklist(ByVal atpphotodocid As String) As GeoTagMergeDocumentInfo
        Dim info As New GeoTagMergeDocumentInfo
        Command = New SqlCommand("uspGeo_GetATPMergeDocument", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmATPPhotoDocId As New SqlParameter("@atpphotodocid", SqlDbType.VarChar, 500)
        prmATPPhotoDocId.Value = atpphotodocid
        Command.Parameters.Add(prmATPPhotoDocId)

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read
                    info.PreparationId = Convert.ToInt32(DataReader.Item("Preparation_Id"))
                    info.ATPPhotoDocId = Convert.ToString(DataReader.Item("ATPPhotoDoc_Id"))
                    info.OriginalDocPath = Convert.ToString(DataReader.Item("OriginalDocPath"))
                    info.IsUploaded = Convert.ToBoolean(DataReader.Item("isUploaded"))
                    info.CreatedDate = Convert.ToDateTime(DataReader.Item("CDT"))
                    info.LMBY = Integer.Parse(DataReader.Item("LMBY").ToString())
                    info.LMDT = Convert.ToDateTime(DataReader.Item("LMDT"))
                End While
            End If
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try

        Return info

    End Function

    Public Function ATPDocChecklistIsAvailable(ByVal atpphotodocid As String) As Boolean
        Dim isAvailable As Boolean = True
        Command = New SqlCommand("uspGeo_IsATPDocChecklistIsAvailable", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmATPPhotoDocId As New SqlParameter("@atpphotodocid", SqlDbType.VarChar, 500)
        Dim prmIsAvailable As New SqlParameter("@isAvailable", SqlDbType.Bit)

        prmATPPhotoDocId.Value = atpphotodocid
        prmIsAvailable.Direction = ParameterDirection.Output

        Command.Parameters.Add(prmATPPhotoDocId)
        Command.Parameters.Add(prmIsAvailable)

        Try
            Connection.Open()
            Command.ExecuteNonQuery()
            isAvailable = Convert.ToBoolean(prmIsAvailable.Value)
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try

        Return isAvailable
    End Function

    Public Function ATPDocChecklistRemove(ByVal atpphotodocid As String) As Boolean
        Dim isSucceed As Boolean = True
        Command = New SqlCommand("update ATPMergeDocument set isUploaded='false' where atpphotodoc_id ='" & atpphotodocid & "'", Connection)
        Command.CommandType = CommandType.Text

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

    Public Function GetSiteInfo(ByVal packageid As String) As CRFramework.SiteInfo
        Dim info As New CRFramework.SiteInfo
        Command = New SqlCommand("select * from poepmsitenew where workpackageid ='" & packageid & "'", Connection)
        Command.CommandType = CommandType.Text
        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read
                    info.SiteId = Convert.ToInt32(DataReader.Item("SiteId"))
                    info.SiteNo = Convert.ToString(DataReader.Item("SiteNo"))
                    info.SiteName = Convert.ToString(DataReader.Item("sitename"))
                    info.PONO = Convert.ToString(DataReader.Item("Pono"))
                    info.Version = Integer.Parse(DataReader.Item("SiteVersion"))
                    info.Scope = Convert.ToString(DataReader.Item("Scope"))
                End While
            End If
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try

        Return info
    End Function

    Public Sub ATPMergeDocLog_I(ByVal info As GeoTagMergeDocLogInfo)
        Command = New SqlCommand("uspGeo_ATPMergeDocLog_I", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmATPPhotoDocId As New SqlParameter("@atpphotodocid", SqlDbType.VarChar, 500)
        Dim prmUserId As New SqlParameter("@userid", SqlDbType.Int)
        Dim prmRoleId As New SqlParameter("@RoleId", SqlDbType.Int)
        Dim prmStatus As New SqlParameter("@status", SqlDbType.VarChar, 500)

        prmATPPhotoDocId.Value = info.ATPPhotoDocId
        prmUserId.Value = info.UserId
        prmRoleId.Value = info.RoleId
        prmStatus.Value = info.PreparationStatus

        Command.Parameters.Add(prmATPPhotoDocId)
        Command.Parameters.Add(prmUserId)
        Command.Parameters.Add(prmRoleId)
        Command.Parameters.Add(prmStatus)

        Try
            Connection.Open()
            Command.ExecuteNonQuery()
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try
    End Sub

    Public Function GetATPMergeDocLog(ByVal atpphotodocid As String) As List(Of GeoTagMergeDocLogInfo)
        Dim list As New List(Of GeoTagMergeDocLogInfo)
        Command = New SqlCommand("uspGeo_GetMergeDocLog", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmATPPhotoDocId As New SqlParameter("@atpphotodocid", SqlDbType.VarChar, 500)
        prmATPPhotoDocId.Value = atpphotodocid
        Command.Parameters.Add(prmATPPhotoDocId)

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read
                    Dim info As New GeoTagMergeDocLogInfo
                    info.PreparationLogId = Convert.ToInt32(DataReader.Item("preparationlog_id"))
                    info.ATPPhotoDocId = Convert.ToString(DataReader.Item("ATPPhotoDoc_Id"))
                    info.UserId = Integer.Parse(DataReader.Item("userid").ToString())
                    info.RoleId = Integer.Parse(DataReader.Item("roleid").ToString())
                    info.Fullname = Convert.ToString(DataReader.Item("name").ToString())
                    info.Rolename = Convert.ToString(DataReader.Item("roledesc"))
                    info.PreparationStatus = Convert.ToString(DataReader.Item("Preparation_Status"))
                    info.ExecuteDate = Convert.ToDateTime(DataReader.Item("ExecuteDate"))
                    list.Add(info)
                End While
            End If
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try
        Return list
    End Function

    Public Function ATPMergeDoc_IU(ByVal info As ATPDocWithGeoTagMergeInfo) As Boolean
        Dim isSucceed As Boolean = True
        Command = New SqlCommand("uspGeo_MergeDoc_IU", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmMergeDocPath As New SqlParameter("@mergedocpath", SqlDbType.VarChar, 2000)
        Dim prmAtpPhotoDocId As New SqlParameter("@atpphotodocid", SqlDbType.VarChar, 500)
        Dim prmLMBY As New SqlParameter("@lmby", SqlDbType.Int)
        Dim prmIsUploaded As New SqlParameter("@isuploaded", SqlDbType.BigInt)
        Dim prmRoleId As New SqlParameter("@roleid", SqlDbType.Int)
        Dim prmSconId As New SqlParameter("@sconid", SqlDbType.Int)

        prmMergeDocPath.Value = info.MergeDocPath
        prmAtpPhotoDocId.Value = info.ATPPhotoDocId
        prmLMBY.Value = info.UserId
        prmIsUploaded.Value = info.IsUploaded
        prmRoleId.Value = info.RoleId
        prmSconId.Value = info.SCONID


        Command.Parameters.Add(prmMergeDocPath)
        Command.Parameters.Add(prmAtpPhotoDocId)
        Command.Parameters.Add(prmLMBY)
        Command.Parameters.Add(prmIsUploaded)
        Command.Parameters.Add(prmRoleId)
        Command.Parameters.Add(prmSconId)

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

    Public Function GetATPGeoTagMergeDoc(ByVal atpphotodocid As String) As ATPDocWithGeoTagMergeInfo
        Dim info As New ATPDocWithGeoTagMergeInfo
        Command = New SqlCommand("uspGeo_GetATPDocMerge", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmATPPhotoDocId As New SqlParameter("@atpphotodocid", SqlDbType.VarChar, 500)
        prmATPPhotoDocId.Value = atpphotodocid
        Command.Parameters.Add(prmATPPhotoDocId)

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read
                    info.MergedocId = Convert.ToInt32(DataReader.Item("MergeDoc_Id"))
                    info.MergeDocPath = Convert.ToString(DataReader.Item("MergeDocPath"))
                    info.ATPPhotoDocId = Convert.ToString(DataReader.Item("ATPPhotoDocId"))
                    info.LMDT = Convert.ToDateTime(DataReader.Item("LMDT"))
                    info.UserId = Integer.Parse(DataReader.Item("LMBY").ToString())
                    info.IsUploaded = Convert.ToBoolean(DataReader.Item("isUploaded"))
                    info.RoleId = Integer.Parse(DataReader.Item("RoleId").ToString())
                    info.Fullname = Convert.ToString(DataReader.Item("name"))
                    info.SignTitle = Convert.ToString(DataReader.Item("RoleDesc"))
                End While
            End If
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try
        Return info
    End Function

    Public Function GetHistoricalATPMergeDocBySubcon(ByVal sconid As Integer) As List(Of ATPDocWithGeoTagMergeInfo)
        Dim list As New List(Of ATPDocWithGeoTagMergeInfo)
        Command = New SqlCommand("uspGeo_GetATPDocMergeBySubcon", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmSubconId As New SqlParameter("@sconid", SqlDbType.Int)
        prmSubconId.Value = sconid
        Command.Parameters.Add(prmSubconId)

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read
                    Dim info As New ATPDocWithGeoTagMergeInfo
                    info.MergedocId = Convert.ToInt32(DataReader.Item("MergeDoc_Id"))
                    info.MergeDocPath = Convert.ToString(DataReader.Item("MergeDocPath"))
                    info.ATPPhotoDocId = Convert.ToString(DataReader.Item("ATPPhotoDocId"))
                    info.UserId = Integer.Parse(DataReader.Item("lmby").ToString())
                    info.Fullname = Convert.ToString(DataReader.Item("name"))
                    info.RoleId = Integer.Parse(DataReader.Item("RoleID"))
                    info.SignTitle = Convert.ToString(DataReader.Item("Roledesc"))
                    info.SiteNo = Convert.ToString(DataReader.Item("SiteNo"))
                    info.siteName = Convert.ToString(DataReader.Item("SiteName"))
                    info.PONO = Convert.ToString(DataReader.Item("PONO"))
                    info.Scope = Convert.ToString(DataReader.Item("Scope"))
                    info.PackageId = Convert.ToString(DataReader.Item("workpackageid"))
                    list.Add(info)
                End While
            End If
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try

        Return list
    End Function
    Public Function DocWithGeoTag_IU(ByVal docid As Integer, ByVal packageid As String, ByVal lmby As Integer, ByVal isgeotagsupport As Boolean) As Boolean
        Dim isSucceed As Boolean = True
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("uspGeo_DocWithGeoTag_IU", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmDocId As New SqlParameter("@docid", SqlDbType.Int)
        Dim prmPackageId As New SqlParameter("@packageid", SqlDbType.VarChar, 50)
        Dim prmLMBY As New SqlParameter("@lmby", SqlDbType.Int)
        Dim prmIsGeoTagSupport As New SqlParameter("@isgeotagsupport", SqlDbType.Bit)

        prmDocId.Value = docid
        prmPackageId.Value = packageid
        prmLMBY.Value = lmby
        prmIsGeoTagSupport.Value = isgeotagsupport

        Command.Parameters.Add(prmDocId)
        Command.Parameters.Add(prmPackageId)
        Command.Parameters.Add(prmLMBY)
        Command.Parameters.Add(prmIsGeoTagSupport)

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
            ErrorLogInsert(103, "DocWithGeoTag_IU", strErrMessage, "uspGeo_DocWithGeoTag_IU")
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
