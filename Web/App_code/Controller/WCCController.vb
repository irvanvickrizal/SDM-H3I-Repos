Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections.Generic

Public Class WCCController
    Inherits BaseController

#Region "COD Document"
    Public Sub WCCDocumentIU(ByVal info As WCCCODDocumentInfo)
        Command = New SqlCommand("uspWCC_CODDocumentIU", Connection)
        command.CommandType = CommandType.StoredProcedure

        Dim prmWCCDocId As New SqlParameter("@WCCDocumentId", SqlDbType.Int)
        Dim prmDocName As New SqlParameter("@DocName", SqlDbType.VarChar, 250)
        Dim prmDocDesc As New SqlParameter("@DocDesc", SqlDbType.VarChar, 250)
        Dim prmLMBY As New SqlParameter("@lmby", SqlDbType.VarChar, 250)
        Dim prmParentId As New SqlParameter("@parentid", SqlDbType.Int)
        Dim prmDocType As New SqlParameter("@docType", SqlDbType.Char, 10)

        prmWCCDocId.Value = info.DocId
        prmDocName.Value = info.DocName
        prmDocDesc.Value = info.DocDesc
        prmLMBY.Value = info.WCCDocLMBY
        prmParentId.Value = info.ParentId
        prmDocType.Value = info.DocType

        command.Parameters.Add(prmWCCDocId)
        command.Parameters.Add(prmDocName)
        command.Parameters.Add(prmDocDesc)
        Command.Parameters.Add(prmLMBY)
        Command.Parameters.Add(prmParentId)
        Command.Parameters.Add(prmDocType)

        Try
            Connection.Open()
            command.ExecuteNonQuery()
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try
    End Sub

    Public Sub WCCDocumentDeleteTemp(ByVal docid As Integer)
        'Modified by Fauzan, 23 Dec 2018
        'Command = New SqlCommand("uspWCC_CODDocumentDelete_Temp", Connection)
        Command = New SqlCommand("uspWCC_CODDocumentDelete", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmDocId As New SqlParameter("@WCCDocumentId", SqlDbType.Int)
        prmDocId.Value = docid
        Command.Parameters.Add(prmDocId)

        Try
            Connection.Open()
            Command.ExecuteNonQuery()
        Catch ex As Exception

        Finally
            Connection.Close()
        End Try
    End Sub

    Public Function GetWCCDocument(ByVal isDeleted As Boolean) As List(Of WCCCODDocumentInfo)
        Dim list As New List(Of WCCCODDocumentInfo)

        Command = New SqlCommand("uspWCC_GetCODDocument", Connection)
        command.CommandType = CommandType.StoredProcedure

        Dim prmIsDeleted As New SqlParameter("@isDeleted", SqlDbType.Bit)
        prmIsDeleted.Value = isDeleted
        command.Parameters.Add(prmIsDeleted)

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read()
                    Dim info As New WCCCODDocumentInfo
                    info.DocId = Integer.Parse(DataReader.Item("WCCDocument_Id").ToString())
                    info.DocName = Convert.ToString(DataReader.Item("DocName"))
                    info.DocDesc = Convert.ToString(DataReader.Item("DocDesc"))
                    info.ParentId = Integer.Parse(DataReader.Item("Parent_Id"))
                    info.DocType = Convert.ToString(DataReader.Item("DocType"))
                    info.WCCDocLMBY = Convert.ToString(DataReader.Item("LMBY"))
                    info.WCCDocLMDT = Convert.ToDateTime(DataReader.Item("LMDT"))
                    list.Add(info)
                End While
            End If
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try
        Return list
    End Function

    Public Function GetWCCParentDocument(ByVal isDeleted As Boolean, ByVal currentdocid As Integer) As List(Of WCCCODDocumentInfo)
        Dim list As New List(Of WCCCODDocumentInfo)

        Command = New SqlCommand("uspWCC_GetCODParentDocument", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmIsDeleted As New SqlParameter("@isDeleted", SqlDbType.Bit)
        Dim prmCurrentDocId As New SqlParameter("@currentdocid", SqlDbType.Int)
        prmIsDeleted.Value = isDeleted
        prmCurrentDocId.Value = currentdocid
        Command.Parameters.Add(prmIsDeleted)
        Command.Parameters.Add(prmCurrentDocId)

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read()
                    Dim info As New WCCCODDocumentInfo
                    info.DocId = Integer.Parse(DataReader.Item("WCCDocument_Id").ToString())
                    info.DocName = Convert.ToString(DataReader.Item("DocName"))
                    info.DocDesc = Convert.ToString(DataReader.Item("DocDesc"))
                    info.ParentId = Integer.Parse(DataReader.Item("Parent_Id"))
                    info.DocType = Convert.ToString(DataReader.Item("DocType"))
                    info.WCCDocLMBY = Convert.ToString(DataReader.Item("LMBY"))
                    info.WCCDocLMDT = Convert.ToDateTime(DataReader.Item("LMDT"))
                    list.Add(info)
                End While
            End If
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try
        Return list
    End Function

    Public Function GetWCCDocument_DS(ByVal isDeleted As Boolean) As DataSet
        Dim ds As New DataSet

        Command = New SqlCommand("uspWCC_GetCODDocument", Connection)
        command.CommandType = CommandType.StoredProcedure

        Dim prmIsDeleted As New SqlParameter("@isDeleted", SqlDbType.Bit)
        prmIsDeleted.Value = isDeleted
        command.Parameters.Add(prmIsDeleted)

        Try
            Connection.Open()
            Dim adapter As New SqlDataAdapter(command)
            adapter.Fill(ds)
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try
        Return ds
    End Function

    Public Function WCCDocumentAuthority_I(ByVal info As WCCDocAuthorityInfo) As Boolean
        Dim isSucceed As Boolean = True
        Command = New SqlCommand("uspWCC_DocAuthorty_I", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmRoleId As New SqlParameter("@roleid", SqlDbType.Int)
        Dim prmUserId As New SqlParameter("@userid", SqlDbType.Int)
        Dim prmLMBY As New SqlParameter("@lmby", SqlDbType.VarChar, 250)

        prmRoleId.Value = info.RoleId
        prmUserId.Value = info.UserId
        prmLMBY.Value = info.LMBY

        Command.Parameters.Add(prmRoleId)
        Command.Parameters.Add(prmUserId)
        Command.Parameters.Add(prmLMBY)

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

    Public Function GetRolesContraintDocAuthority(ByVal usertypeid As Integer) As List(Of RoleInfo)
        Dim list As New List(Of RoleInfo)

        Command = New SqlCommand("uspWCC_GetRolesConstraintWCCAuthority", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmUserTypeId As New SqlParameter("@usertypeid", SqlDbType.Int)
        prmUserTypeId.Value = usertypeid
        Command.Parameters.Add(prmUserTypeId)

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read
                    Dim info As New RoleInfo
                    info.RoleId = Integer.Parse(DataReader.Item("roleid").ToString())
                    info.RoleName = Convert.ToString(DataReader.Item("roledesc"))
                    list.Add(info)
                End While
            End If
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try

        Return list
    End Function

    Public Function GetWCCCreators() As List(Of WCCDocAuthorityInfo)
        Dim list As New List(Of WCCDocAuthorityInfo)

        Command = New SqlCommand("uspWCC_GetDocCreator_Authorties", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read
                    Dim info As New WCCDocAuthorityInfo
                    info.WCCAuthortyId = Integer.Parse(DataReader.Item("WCCAuthorty_Id").ToString())
                    info.UserId = Integer.Parse(DataReader.Item("user_id").ToString())
                    info.UserName = Convert.ToString(DataReader.Item("name"))
                    info.RoleId = Integer.Parse(DataReader.Item("Role_Id").ToString())
                    info.RoleName = Convert.ToString(DataReader.Item("RoleDesc"))
                    info.LMBY = Convert.ToString(DataReader.Item("LMBY"))
                    info.LMDT = Convert.ToDateTime(DataReader.Item("LMDT"))
                    list.Add(info)
                End While
            End If
        Catch ex As Exception

        Finally
            Connection.Close()
        End Try

        Return list
    End Function

    Public Sub DeleteWCCCreators(ByVal authid As Integer)
        Command = New SqlCommand("uspWCC_DeleteDocCreator", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmAuthId As New SqlParameter("@wccauthorityid", SqlDbType.Int)
        prmAuthId.Value = authid
        Command.Parameters.Add(prmAuthId)

        Try
            Connection.Open()
            Command.ExecuteNonQuery()
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try

    End Sub



#End Region

#Region "Document additional Grouping"
    Public Function GetWccDocumentGrouping(ByVal dscopeid As Integer, ByVal isDeleted As Boolean, ByVal activityid As Integer) As List(Of WCCDocumentInfo)
        Dim list As New List(Of WCCDocumentInfo)
        Command = New SqlCommand("uspWCC_GetWCCDocumentByDetailScope", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmDScopeId As New SqlParameter("@dscopeid", SqlDbType.Int)
        Dim prmIsDeleted As New SqlParameter("@isDeleted", SqlDbType.Bit)
        Dim prmActivityId As New SqlParameter("@activityid", SqlDbType.Int)

        prmDScopeId.Value = dscopeid
        prmIsDeleted.Value = isDeleted
        prmActivityId.Value = activityid

        Command.Parameters.Add(prmDScopeId)
        Command.Parameters.Add(prmIsDeleted)
        Command.Parameters.Add(prmActivityId)

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read()
                    Dim info As New WCCDocumentInfo
                    info.DocId = Integer.Parse(DataReader.Item("doc_id").ToString())
                    info.DScopeId = Integer.Parse(DataReader.Item("DScope_Id").ToString())
                    info.WCCDOCId = Convert.ToInt32(DataReader.Item("WCCDOC_Id"))
                    info.DScopeName = Convert.ToString(DataReader.Item("DScope_Name"))
                    info.DOCWCCLMBY = Convert.ToString(DataReader.Item("docGroupLMBY"))
                    info.DOCWCCLMDT = Convert.ToDateTime(DataReader.Item("docGroupLMDT"))
                    info.DocName = Convert.ToString(DataReader.Item("docname"))
                    info.IsMandatory = Convert.ToBoolean(DataReader.Item("IsMandatory"))
                    list.Add(info)
                End While
            End If
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try
        Return list
    End Function

    Public Function GetDocumentNotInWccDocGrouping(ByVal parentid As Integer, ByVal dscopeid As Integer) As List(Of WCCDocumentInfo)
        Dim list As New List(Of WCCDocumentInfo)
        Command = New SqlCommand("uspWCC_GetDocumentNotInWCCGroupingDoc", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmParentId As New SqlParameter("@parentid", SqlDbType.Int)
        Dim prmDScopeId As New SqlParameter("@dscopeid", SqlDbType.Int)

        prmParentId.Value = parentid
        prmDScopeId.Value = dscopeid

        Command.Parameters.Add(prmParentId)
        Command.Parameters.Add(prmDScopeId)

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read()
                    Dim info As New WCCDocumentInfo
                    info.WCCDOCId = Convert.ToInt32(DataReader.Item("WCCDoc_Id"))
                    info.DocId = Integer.Parse(DataReader.Item("doc_id"))
                    info.DocName = Convert.ToString(DataReader.Item("docname"))

                    list.Add(info)
                End While
            End If
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try
        Return list
    End Function

    Public Sub WCCDocumentGroupingIU(ByVal info As WCCDocumentInfo)
        Command = New SqlCommand("uspWCC_DocumentGrouping_IU", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmWCCDOCId As New SqlParameter("@wccdocid", SqlDbType.BigInt)
        Dim prmDScopeId As New SqlParameter("@dscopeid", SqlDbType.Int)
        Dim prmIsMandatory As New SqlParameter("@isMandatory", SqlDbType.Bit)
        Dim prmLMBY As New SqlParameter("@lmby", SqlDbType.VarChar, 250)
        Dim prmDocId As New SqlParameter("@docid", SqlDbType.Int)
        Dim prmParentDocType As New SqlParameter("@ParentDocType", SqlDbType.VarChar, 50)

        prmWCCDOCId.Value = info.WCCDOCId
        prmDScopeId.Value = info.DScopeId
        prmIsMandatory.Value = info.IsMandatory
        prmLMBY.Value = info.DOCWCCLMBY
        prmDocId.Value = info.DocId
        prmParentDocType.Value = info.ParentDocType

        Command.Parameters.Add(prmWCCDOCId)
        Command.Parameters.Add(prmDScopeId)
        Command.Parameters.Add(prmIsMandatory)
        Command.Parameters.Add(prmLMBY)
        Command.Parameters.Add(prmDocId)
        Command.Parameters.Add(prmParentDocType)

        Try
            Connection.Open()
            Command.ExecuteNonQuery()
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try
    End Sub

    Public Sub WCCDocumentDeleteTemp(ByVal wccdocid As Int32, ByVal lmby As String)
        Command = New SqlCommand("uspWCC_DocumentGroupingDeleted_Temp", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmWCCDocId As New SqlParameter("@WCCDOCId", SqlDbType.BigInt)
        Dim prmLMBY As New SqlParameter("@lmby", SqlDbType.VarChar, 250)

        prmWCCDocId.Value = wccdocid
        prmLMBY.Value = lmby

        Command.Parameters.Add(prmWCCDocId)
        Command.Parameters.Add(prmLMBY)

        Try
            Connection.Open()
            Command.ExecuteNonQuery()
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try
    End Sub

    Public Sub WCCDocumentGroupingDelete(ByVal wccdocid As Int32)
        Command = New SqlCommand("uspWCC_DeleteGroupingDocumentWCC", Connection)
        Command.CommandType = CommandType.StoredProcedure
        Dim prmWCCDocId As New SqlParameter("@wccdocid", SqlDbType.BigInt)
        prmWCCDocId.Value = wccdocid
        Command.Parameters.Add(prmWCCDocId)
        Try
            Connection.Open()
            Command.ExecuteNonQuery()
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try
    End Sub

    Public Function GetDocWCCAdditionalDocument(ByVal dscopeid As Integer, ByVal isDeleted As Boolean, ByVal wccdocid As Integer, ByVal packageid As String, ByVal activityid As Integer) As List(Of WCCSitedocInfo)
        Dim list As New List(Of WCCSitedocInfo)
        Command = New SqlCommand("uspWCC_GetDocAdditionalWCCScopeBase", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmDScopeId As New SqlParameter("@dscopeid", SqlDbType.Int)
        Dim prmIsDeleted As New SqlParameter("@IsDeleted", SqlDbType.Bit)
        Dim prmWCCDocId As New SqlParameter("@wccdocid", SqlDbType.Int)
        Dim prmPackageId As New SqlParameter("@packageid", SqlDbType.VarChar, 50)
        Dim prmActivityId As New SqlParameter("@activityid", SqlDbType.Int)

        prmDScopeId.Value = dscopeid
        prmIsDeleted.Value = isDeleted
        prmWCCDocId.Value = wccdocid
        prmPackageId.Value = packageid
        prmActivityId.Value = activityid

        Command.Parameters.Add(prmDScopeId)
        Command.Parameters.Add(prmIsDeleted)
        Command.Parameters.Add(prmWCCDocId)
        Command.Parameters.Add(prmPackageId)
        Command.Parameters.Add(prmActivityId)

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If (DataReader.HasRows) Then
                While (DataReader.Read())
                    Dim info As New WCCSitedocInfo
                    info.DocId = Integer.Parse(DataReader.Item("doc_id").ToString())
                    info.DocName = Convert.ToString(DataReader.Item("docname"))
                    info.DocPath = Convert.ToString(DataReader.Item("docpath"))
                    info.OrgDocPath = Convert.ToString(DataReader.Item("orgdocpath"))
                    info.CanDeleted = Convert.ToBoolean(DataReader.Item("CanDeleted"))
                    info.IsUploaded = Convert.ToBoolean(DataReader.Item("IsUploaded"))
                    info.ParentDocType = Convert.ToString(DataReader.Item("ParentDocType"))
                    list.Add(info)
                End While
            End If
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try
        Return list
    End Function

    Public Sub WCCDeleteAdditionalDocument(ByVal swid As Int32)
        Command = New SqlCommand("uspWCC_SupportingDocument_Rejected", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmSWID As New SqlParameter("@swid", SqlDbType.BigInt)
        prmSWID.Value = swid
        Command.Parameters.Add(prmSWID)

        Try
            Connection.Open()
            Command.ExecuteNonQuery()
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try

    End Sub

    Public Function GetOthersDocWCCAdditionalDocument(ByVal dscopeid As Integer, ByVal isDeleted As Boolean, ByVal wccdocid As Integer, ByVal packageid As String, ByVal activityid As Integer, ByVal wccid As Int32) As List(Of WCCSitedocInfo)
        Dim list As New List(Of WCCSitedocInfo)
        Command = New SqlCommand("uspWCC_GetOthersDocAdditional", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmDScopeId As New SqlParameter("@dscopeid", SqlDbType.Int)
        Dim prmIsDeleted As New SqlParameter("@IsDeleted", SqlDbType.Bit)
        Dim prmWCCDocId As New SqlParameter("@wccdocid", SqlDbType.Int)
        Dim prmPackageId As New SqlParameter("@packageid", SqlDbType.VarChar, 50)
        Dim prmActivityId As New SqlParameter("@activityid", SqlDbType.Int)
        Dim prmWCCID As New SqlParameter("@wccid", SqlDbType.BigInt)

        prmDScopeId.Value = dscopeid
        prmIsDeleted.Value = isDeleted
        prmWCCDocId.Value = wccdocid
        prmPackageId.Value = packageid
        prmActivityId.Value = activityid
        prmWCCID.Value = wccid

        Command.Parameters.Add(prmDScopeId)
        Command.Parameters.Add(prmIsDeleted)
        Command.Parameters.Add(prmWCCDocId)
        Command.Parameters.Add(prmPackageId)
        Command.Parameters.Add(prmActivityId)
        Command.Parameters.Add(prmWCCID)

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If (DataReader.HasRows) Then
                While (DataReader.Read())
                    Dim info As New WCCSitedocInfo
                    info.DocId = Integer.Parse(DataReader.Item("doc_id").ToString())
                    info.DocName = Convert.ToString(DataReader.Item("docname"))
                    info.DocPath = Convert.ToString(DataReader.Item("docpath"))
                    info.OrgDocPath = Convert.ToString(DataReader.Item("orgdocpath"))
                    info.CanDeleted = Convert.ToBoolean(DataReader.Item("CanDeleted"))
                    info.ParentDocType = Convert.ToString(DataReader.Item("ParentDocType"))
                    list.Add(info)
                End While
            End If
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try
        Return list
    End Function

    
#End Region

#Region "WCC"
    Public Function GetODWCCBaseId(ByVal wccid As Int32) As WCCInfo
        Dim info As New WCCInfo
        Command = New SqlCommand("uspWCC_GetODWCC", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmWCCID As New SqlParameter("@wccid", SqlDbType.BigInt)
        prmWCCID.Value = wccid
        Command.Parameters.Add(prmWCCID)

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If (DataReader.HasRows) Then
                While (DataReader.Read())

                    info.WCCID = Convert.ToInt32(DataReader.Item("WCC_ID"))
                    info.SCONID = Integer.Parse(DataReader.Item("Scon_Id").ToString())
                    info.SubconName = Convert.ToString(DataReader.Item("Scon_Name"))
                    info.WCTRDate = Convert.ToDateTime(DataReader.Item("WCTR_Date"))
                    info.POSubcontractor = Convert.ToString(DataReader.Item("PO_Subcontractor"))
                    info.PackageId = Convert.ToString(DataReader.Item("Package_Id"))
                    info.LMDT = Convert.ToDateTime(DataReader.Item("LMDT"))
                    info.LMBY = Convert.ToString(DataReader.Item("name"))
                    If (Not String.IsNullOrEmpty(Convert.ToString(DataReader.Item("Issuance_date")))) Then
                        info.IssuanceDate = Convert.ToDateTime(DataReader.Item("Issuance_Date"))
                    End If
                    info.GScopeId = Integer.Parse(DataReader.Item("GScope_Id").ToString())
                    info.DScopeID = Integer.Parse(DataReader.Item("DScope_Id").ToString())
                    info.ScopeName = Convert.ToString(DataReader.Item("DScope_Name"))
                    info.CertificateNumber = Convert.ToString(DataReader.Item("Certificate_Number"))
                    info.CDT = Convert.ToDateTime(DataReader.Item("CDT"))
                    If (Not String.IsNullOrEmpty(Convert.ToString(DataReader.Item("BAUT_BAST_Date")))) Then
                        info.BAUTDate = Convert.ToDateTime(DataReader.Item("BAUT_BAST_Date"))
                    End If
                    info.SiteName = Convert.ToString(DataReader.Item("SiteName"))
                    info.SiteNo = Convert.ToString(DataReader.Item("SiteNo"))
                    info.PONO = Convert.ToString(DataReader.Item("PONO"))
                    info.POName = Convert.ToString(DataReader.Item("POName"))
                    info.Scope = Convert.ToString(DataReader.Item("Scope"))
                    info.WorkflowId = Integer.Parse(DataReader.Item("Workflow_Id").ToString())
                    info.ActivityId = Integer.Parse(DataReader.Item("Activity_Id").ToString())
                End While
            End If
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try
        Return info
    End Function

    Public Function GetODWCCBaseOnPackageSubconId(ByVal packageid As String, ByVal sconid As Integer, ByVal roleid As Integer) As List(Of WCCInfo)
        Dim list As New List(Of WCCInfo)
        Command = New SqlCommand("uspWCC_GetODWCCBase_PackageId_SubconId", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmPackageid As New SqlParameter("@packageid", SqlDbType.VarChar, 50)
        Dim prmSubconId As New SqlParameter("@subconid", SqlDbType.Int)
        Dim prmRoleId As New SqlParameter("@roleid", SqlDbType.Int)

        prmPackageid.Value = packageid
        prmSubconId.Value = sconid
        prmRoleId.Value = roleid

        Command.Parameters.Add(prmPackageid)
        Command.Parameters.Add(prmSubconId)
        Command.Parameters.Add(prmRoleId)

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While (DataReader.Read())
                    Dim info As New WCCInfo
                    info.WCCID = Convert.ToInt32(DataReader.Item("WCC_ID"))
                    info.SCONID = Integer.Parse(DataReader.Item("Scon_Id").ToString())
                    info.SubconName = Convert.ToString(DataReader.Item("Scon_Name"))
                    info.WCTRDate = Convert.ToDateTime(DataReader.Item("WCTR_Date"))
                    info.POSubcontractor = Convert.ToString(DataReader.Item("PO_Subcontractor"))
                    info.PackageId = Convert.ToString(DataReader.Item("Package_Id"))
                    info.LMDT = Convert.ToDateTime(DataReader.Item("LMDT"))
                    info.LMBY = Convert.ToString(DataReader.Item("name"))
                    If (Not String.IsNullOrEmpty(Convert.ToString(DataReader.Item("Issuance_date")))) Then
                        info.IssuanceDate = Convert.ToDateTime(DataReader.Item("Issuance_Date"))
                    End If
                    info.DScopeID = Integer.Parse(DataReader.Item("DScope_Id").ToString())
                    info.ScopeName = Convert.ToString(DataReader.Item("DScope_Name"))
                    info.CertificateNumber = Convert.ToString(DataReader.Item("Certificate_Number"))
                    info.CDT = Convert.ToDateTime(DataReader.Item("CDT"))
                    If (Not String.IsNullOrEmpty(Convert.ToString(DataReader.Item("BAUT_BAST_Date")))) Then
                        info.BAUTDate = Convert.ToDateTime(DataReader.Item("BAUT_BAST_Date"))
                    End If

                    info.WCCStatus = Convert.ToString(DataReader.Item("WCCStatus"))
                    info.WorkflowId = Integer.Parse(DataReader.Item("Workflow_Id").ToString())
                    info.ActivityId = Integer.Parse(DataReader.Item("Activity_Id").ToString())
                    list.Add(info)
                End While
            End If

        Catch ex As Exception

        Finally
            Connection.Close()
        End Try

        Return list
    End Function

    Public Function GetODWCCBaseOnPackageId(ByVal packageid As String) As List(Of WCCInfo)
        Dim list As New List(Of WCCInfo)
        Command = New SqlCommand("uspWCC_GetODWCCBase_PackageId", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmPackageid As New SqlParameter("@packageid", SqlDbType.VarChar, 50)
        prmPackageid.Value = packageid
        Command.Parameters.Add(prmPackageid)
        

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While (DataReader.Read())
                    Dim info As New WCCInfo
                    info.WCCID = Convert.ToInt32(DataReader.Item("WCC_ID"))
                    info.SCONID = Integer.Parse(DataReader.Item("Scon_Id").ToString())
                    info.SubconName = Convert.ToString(DataReader.Item("Scon_Name"))
                    info.WCTRDate = Convert.ToDateTime(DataReader.Item("WCTR_Date"))
                    info.POSubcontractor = Convert.ToString(DataReader.Item("PO_Subcontractor"))
                    info.PackageId = Convert.ToString(DataReader.Item("Package_Id"))
                    info.LMDT = Convert.ToDateTime(DataReader.Item("LMDT"))
                    info.LMBY = Convert.ToString(DataReader.Item("name"))
                    If (Not String.IsNullOrEmpty(Convert.ToString(DataReader.Item("Issuance_date")))) Then
                        info.IssuanceDate = Convert.ToDateTime(DataReader.Item("Issuance_Date"))
                    End If
                    info.DScopeID = Integer.Parse(DataReader.Item("DScope_Id").ToString())
                    info.ScopeName = Convert.ToString(DataReader.Item("DScope_Name"))
                    info.CertificateNumber = Convert.ToString(DataReader.Item("Certificate_Number"))
                    info.CDT = Convert.ToDateTime(DataReader.Item("CDT"))
                    If (Not String.IsNullOrEmpty(Convert.ToString(DataReader.Item("BAUT_BAST_Date")))) Then
                        info.BAUTDate = Convert.ToDateTime(DataReader.Item("BAUT_BAST_Date"))
                    End If

                    info.WCCStatus = Convert.ToString(DataReader.Item("WCCStatus"))
                    info.WorkflowId = Integer.Parse(DataReader.Item("Workflow_Id").ToString())
                    info.ActivityId = Integer.Parse(DataReader.Item("Activity_Id").ToString())
                    list.Add(info)
                End While
            End If

        Catch ex As Exception

        Finally
            Connection.Close()
        End Try

        Return list
    End Function

    Public Function GetWCCPreparation(ByVal userid As Integer) As List(Of WCCInfo)
        Dim list As New List(Of WCCInfo)
        Command = New SqlCommand("uspWCC_GetWCCPreparation", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmUserid As New SqlParameter("@usrid", SqlDbType.Int)
        prmUserid.Value = userid
        Command.Parameters.Add(prmUserid)
        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While (DataReader.Read())
                    Dim info As New WCCInfo
                    info.WCCID = Convert.ToInt32(DataReader.Item("WCC_ID"))
                    info.SCONID = Integer.Parse(DataReader.Item("Scon_Id").ToString())
                    info.SubconName = Convert.ToString(DataReader.Item("Scon_Name"))
                    info.WCTRDate = Convert.ToDateTime(DataReader.Item("WCTR_Date"))
                    info.PONO = Convert.ToString(DataReader.Item("PONO"))
                    info.POSubcontractor = Convert.ToString(DataReader.Item("PO_Subcontractor"))
                    info.PackageId = Convert.ToString(DataReader.Item("Package_Id"))
                    info.SiteNo = Convert.ToString(DataReader.Item("SiteNo"))
                    info.SiteName = Convert.ToString(DataReader.Item("SiteName"))
                    info.LMDT = Convert.ToDateTime(DataReader.Item("LMDT"))
                    info.LMBY = Convert.ToString(DataReader.Item("name"))
                    If (Not String.IsNullOrEmpty(Convert.ToString(DataReader.Item("Issuance_date")))) Then
                        info.IssuanceDate = Convert.ToDateTime(DataReader.Item("Issuance_Date"))
                    End If
                    info.DScopeID = Integer.Parse(DataReader.Item("DScope_Id").ToString())
                    info.ScopeName = Convert.ToString(DataReader.Item("DScope_Name"))
                    info.CertificateNumber = Convert.ToString(DataReader.Item("Certificate_Number"))
                    info.CDT = Convert.ToDateTime(DataReader.Item("CDT"))
                    info.BAUTDate = Convert.ToDateTime(DataReader.Item("BAUT_BAST_Date"))
                    info.WCCStatus = Convert.ToString(DataReader.Item("WCCStatus"))
                    info.WorkflowId = Integer.Parse(DataReader.Item("Workflow_Id").ToString())
                    list.Add(info)
                End While
            End If

        Catch ex As Exception

        Finally
            Connection.Close()
        End Try

        Return list
    End Function

    Public Function GetWCCReadyCreation(ByVal userid As Integer) As List(Of WCCInfo)
        Dim list As New List(Of WCCInfo)
        Command = New SqlCommand("uspWCC_GetWCCReadyCreation", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmUserid As New SqlParameter("@usrid", SqlDbType.Int)
        prmUserid.Value = userid
        Command.Parameters.Add(prmUserid)
        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While (DataReader.Read())
                    Dim info As New WCCInfo
                    info.WCCID = Convert.ToInt32(DataReader.Item("WCC_ID"))
                    info.SCONID = Integer.Parse(DataReader.Item("Scon_Id").ToString())
                    info.SubconName = Convert.ToString(DataReader.Item("Scon_Name"))
                    info.WCTRDate = Convert.ToDateTime(DataReader.Item("WCTR_Date"))
                    info.POSubcontractor = Convert.ToString(DataReader.Item("PO_Subcontractor"))
                    info.PackageId = Convert.ToString(DataReader.Item("Package_Id"))
                    info.PONO = Convert.ToString(DataReader.Item("PONO"))
                    info.SiteNo = Convert.ToString(DataReader.Item("SiteNo"))
                    info.SiteName = Convert.ToString(DataReader.Item("SiteName"))
                    info.LMDT = Convert.ToDateTime(DataReader.Item("LMDT"))
                    info.LMBY = Convert.ToString(DataReader.Item("name"))
                    If (Not String.IsNullOrEmpty(Convert.ToString(DataReader.Item("Issuance_date")))) Then
                        info.IssuanceDate = Convert.ToDateTime(DataReader.Item("Issuance_Date"))
                    End If
                    info.DScopeID = Integer.Parse(DataReader.Item("DScope_Id").ToString())
                    info.ScopeName = Convert.ToString(DataReader.Item("DScope_Name"))
                    info.CertificateNumber = Convert.ToString(DataReader.Item("Certificate_Number"))
                    info.CDT = Convert.ToDateTime(DataReader.Item("CDT"))
                    info.BAUTDate = Convert.ToDateTime(DataReader.Item("BAUT_BAST_Date"))
                    info.WCCStatus = Convert.ToString(DataReader.Item("WCCStatus"))
                    info.WorkflowId = Integer.Parse(DataReader.Item("Workflow_Id").ToString())
                    list.Add(info)
                End While
            End If

        Catch ex As Exception

        Finally
            Connection.Close()
        End Try

        Return list
    End Function

    Public Function GetWCCRejection(ByVal userid As Integer) As List(Of WCCInfo)
        Dim list As New List(Of WCCInfo)
        Command = New SqlCommand("uspWCC_GetDocumentRejection", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmUserid As New SqlParameter("@usrid", SqlDbType.Int)
        prmUserid.Value = userid
        Command.Parameters.Add(prmUserid)
        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While (DataReader.Read())
                    Dim info As New WCCInfo
                    info.WCCID = Convert.ToInt32(DataReader.Item("WCC_ID"))
                    info.SCONID = Integer.Parse(DataReader.Item("Scon_Id").ToString())
                    info.SubconName = Convert.ToString(DataReader.Item("Scon_Name"))
                    info.WCTRDate = Convert.ToDateTime(DataReader.Item("WCTR_Date"))
                    info.POSubcontractor = Convert.ToString(DataReader.Item("PO_Subcontractor"))
                    info.PackageId = Convert.ToString(DataReader.Item("Package_Id"))
                    info.PONO = Convert.ToString(DataReader.Item("PONO"))
                    info.SiteNo = Convert.ToString(DataReader.Item("SiteNo"))
                    info.SiteName = Convert.ToString(DataReader.Item("SiteName"))
                    info.LMDT = Convert.ToDateTime(DataReader.Item("LMDT"))
                    info.LMBY = Convert.ToString(DataReader.Item("name"))
                    info.RemarksOfRejection = Convert.ToString(DataReader.Item("remarksofrejection"))
                    If (Not String.IsNullOrEmpty(Convert.ToString(DataReader.Item("Issuance_date")))) Then
                        info.IssuanceDate = Convert.ToDateTime(DataReader.Item("Issuance_Date"))
                    End If
                    info.DScopeID = Integer.Parse(DataReader.Item("DScope_Id").ToString())
                    info.ScopeName = Convert.ToString(DataReader.Item("DScope_Name"))
                    info.CertificateNumber = Convert.ToString(DataReader.Item("Certificate_Number"))
                    info.CDT = Convert.ToDateTime(DataReader.Item("CDT"))
                    info.BAUTDate = Convert.ToDateTime(DataReader.Item("BAUT_BAST_Date"))
                    info.WCCStatus = Convert.ToString(DataReader.Item("WCCStatus"))
                    info.WorkflowId = Integer.Parse(DataReader.Item("Workflow_Id").ToString())
                    info.RejectionUser = Convert.ToString(DataReader.Item("rejectionuser"))
                    info.RejectionDate = Convert.ToDateTime(DataReader.Item("RejectionDate"))
                    list.Add(info)
                End While
            End If

        Catch ex As Exception

        Finally
            Connection.Close()
        End Try

        Return list
    End Function

    Public Function GetWCCPreparation_Subcon(ByVal userid As Integer) As List(Of WCCInfo)
        Dim list As New List(Of WCCInfo)
        Command = New SqlCommand("uspWCC_GetWCCPreparation_Subcon", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmUserid As New SqlParameter("@usrid", SqlDbType.Int)
        prmUserid.Value = userid
        Command.Parameters.Add(prmUserid)
        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While (DataReader.Read())
                    Dim info As New WCCInfo
                    info.WCCID = Convert.ToInt32(DataReader.Item("WCC_ID"))
                    info.SCONID = Integer.Parse(DataReader.Item("Scon_Id").ToString())
                    info.SubconName = Convert.ToString(DataReader.Item("Scon_Name"))
                    info.WCTRDate = Convert.ToDateTime(DataReader.Item("WCTR_Date"))
                    info.PONO = Convert.ToString(DataReader.Item("PONO"))
                    info.POSubcontractor = Convert.ToString(DataReader.Item("PO_Subcontractor"))
                    info.PackageId = Convert.ToString(DataReader.Item("Package_Id"))
                    info.SiteNo = Convert.ToString(DataReader.Item("SiteNo"))
                    info.SiteName = Convert.ToString(DataReader.Item("SiteName"))
                    info.LMDT = Convert.ToDateTime(DataReader.Item("LMDT"))
                    info.LMBY = Convert.ToString(DataReader.Item("name"))
                    If (Not String.IsNullOrEmpty(Convert.ToString(DataReader.Item("Issuance_date")))) Then
                        info.IssuanceDate = Convert.ToDateTime(DataReader.Item("Issuance_Date"))
                    End If
                    info.DScopeID = Integer.Parse(DataReader.Item("DScope_Id").ToString())
                    info.ScopeName = Convert.ToString(DataReader.Item("DScope_Name"))
                    info.CertificateNumber = Convert.ToString(DataReader.Item("Certificate_Number"))
                    info.CDT = Convert.ToDateTime(DataReader.Item("CDT"))
                    If (Not String.IsNullOrEmpty(Convert.ToString(DataReader.Item("BAUT_BAST_Date")))) Then
                        info.BAUTDate = Convert.ToDateTime(DataReader.Item("BAUT_BAST_Date"))
                    End If

                    info.WCCStatus = Convert.ToString(DataReader.Item("WCCStatus"))
                    info.WorkflowId = Integer.Parse(DataReader.Item("Workflow_Id").ToString())
                    list.Add(info)
                End While
            End If

        Catch ex As Exception

        Finally
            Connection.Close()
        End Try

        Return list
    End Function

    Public Function GetWCCReadyCreation_Subcon(ByVal userid As Integer) As List(Of WCCInfo)
        Dim list As New List(Of WCCInfo)
        Command = New SqlCommand("uspWCC_GetWCCReadyCreation_Subcon", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmUserid As New SqlParameter("@usrid", SqlDbType.Int)
        prmUserid.Value = userid
        Command.Parameters.Add(prmUserid)
        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While (DataReader.Read())
                    Dim info As New WCCInfo
                    info.WCCID = Convert.ToInt32(DataReader.Item("WCC_ID"))
                    info.SCONID = Integer.Parse(DataReader.Item("Scon_Id").ToString())
                    info.SubconName = Convert.ToString(DataReader.Item("Scon_Name"))
                    info.WCTRDate = Convert.ToDateTime(DataReader.Item("WCTR_Date"))
                    info.PONO = Convert.ToString(DataReader.Item("PONO"))
                    info.POSubcontractor = Convert.ToString(DataReader.Item("PO_Subcontractor"))
                    info.PackageId = Convert.ToString(DataReader.Item("Package_Id"))
                    info.SiteNo = Convert.ToString(DataReader.Item("SiteNo"))
                    info.SiteName = Convert.ToString(DataReader.Item("SiteName"))
                    info.LMDT = Convert.ToDateTime(DataReader.Item("LMDT"))
                    info.LMBY = Convert.ToString(DataReader.Item("name"))
                    If (Not String.IsNullOrEmpty(Convert.ToString(DataReader.Item("Issuance_date")))) Then
                        info.IssuanceDate = Convert.ToDateTime(DataReader.Item("Issuance_Date"))
                    End If
                    info.DScopeID = Integer.Parse(DataReader.Item("DScope_Id").ToString())
                    info.ScopeName = Convert.ToString(DataReader.Item("DScope_Name"))
                    info.CertificateNumber = Convert.ToString(DataReader.Item("Certificate_Number"))
                    info.CDT = Convert.ToDateTime(DataReader.Item("CDT"))
                    info.BAUTDate = Convert.ToDateTime(DataReader.Item("BAUT_BAST_Date"))
                    info.WCCStatus = Convert.ToString(DataReader.Item("WCCStatus"))
                    info.WorkflowId = Integer.Parse(DataReader.Item("Workflow_Id").ToString())
                    list.Add(info)
                End While
            End If

        Catch ex As Exception

        Finally
            Connection.Close()
        End Try

        Return list
    End Function

    Public Function GetWCCRejection_Subcon(ByVal userid As Integer) As List(Of WCCInfo)
        Dim list As New List(Of WCCInfo)
        Command = New SqlCommand("uspWCC_GetDocumentRejection_Subcon", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmUserid As New SqlParameter("@usrid", SqlDbType.Int)
        prmUserid.Value = userid
        Command.Parameters.Add(prmUserid)
        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While (DataReader.Read())
                    Dim info As New WCCInfo
                    info.WCCID = Convert.ToInt32(DataReader.Item("WCC_ID"))
                    info.SCONID = Integer.Parse(DataReader.Item("Scon_Id").ToString())
                    info.SubconName = Convert.ToString(DataReader.Item("Scon_Name"))
                    info.WCTRDate = Convert.ToDateTime(DataReader.Item("WCTR_Date"))
                    info.POSubcontractor = Convert.ToString(DataReader.Item("PO_Subcontractor"))
                    info.PackageId = Convert.ToString(DataReader.Item("Package_Id"))
                    info.PONO = Convert.ToString(DataReader.Item("PONO"))
                    info.SiteNo = Convert.ToString(DataReader.Item("SiteNo"))
                    info.SiteName = Convert.ToString(DataReader.Item("SiteName"))
                    info.LMDT = Convert.ToDateTime(DataReader.Item("LMDT"))
                    info.LMBY = Convert.ToString(DataReader.Item("name"))
                    info.RemarksOfRejection = Convert.ToString(DataReader.Item("remarksofrejection"))
                    If (Not String.IsNullOrEmpty(Convert.ToString(DataReader.Item("Issuance_date")))) Then
                        info.IssuanceDate = Convert.ToDateTime(DataReader.Item("Issuance_Date"))
                    End If
                    info.DScopeID = Integer.Parse(DataReader.Item("DScope_Id").ToString())
                    info.ScopeName = Convert.ToString(DataReader.Item("DScope_Name"))
                    info.CertificateNumber = Convert.ToString(DataReader.Item("Certificate_Number"))
                    info.CDT = Convert.ToDateTime(DataReader.Item("CDT"))
                    info.BAUTDate = Convert.ToDateTime(DataReader.Item("BAUT_BAST_Date"))
                    info.WCCStatus = Convert.ToString(DataReader.Item("WCCStatus"))
                    info.WorkflowId = Integer.Parse(DataReader.Item("Workflow_Id").ToString())
                    info.RejectionUser = Convert.ToString(DataReader.Item("rejectionuser"))
                    info.RejectionDate = Convert.ToDateTime(DataReader.Item("RejectionDate"))

                    list.Add(info)
                End While
            End If

        Catch ex As Exception

        Finally
            Connection.Close()
        End Try

        Return list
    End Function

    Public Function IsBAUTApproved(ByVal bautdocid As Integer, ByVal packageid As String) As Boolean
        Dim isApproved As Boolean = False

        Command = New SqlCommand("uspWCC_CheckBautApproved", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmPackageId As New SqlParameter("@packageid", SqlDbType.VarChar)
        Dim prmBAUTDocId As New SqlParameter("@bautdocid", SqlDbType.Int)
        Dim prmRowCount As New SqlParameter("@rowCount", SqlDbType.Int)

        prmPackageId.Value = packageid
        prmBAUTDocId.Value = bautdocid
        prmRowCount.Direction = ParameterDirection.Output

        Command.Parameters.Add(prmPackageId)
        Command.Parameters.Add(prmBAUTDocId)
        Command.Parameters.Add(prmRowCount)

        Try
            Connection.Open()
            Command.ExecuteNonQuery()
            isApproved = IIf(Integer.Parse(prmRowCount.Value.ToString()) > 0, True, False)
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try

        Return isApproved
    End Function

    Public Function ODWCC_I(ByVal info As WCCInfo) As Int32
        Dim wccid As Int32 = 0

        Command = New SqlCommand("uspWCC_ODWCC_IU", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmWCCID_I As New SqlParameter("@wccid_I", SqlDbType.BigInt)
        Dim prmSCONID As New SqlParameter("@sconid", SqlDbType.Int)
        Dim prmPackageId As New SqlParameter("@packageid", SqlDbType.VarChar, 50)
        Dim prmBAUTBASTDone As New SqlParameter("@BautBastDate", SqlDbType.DateTime)
        Dim prmPOSubcon As New SqlParameter("@POSubcontractor", SqlDbType.VarChar, 50)
        Dim prmWCTRDate As New SqlParameter("@wctrdate", SqlDbType.DateTime)
        Dim prmLMBY As New SqlParameter("@lmby", SqlDbType.Int)
        Dim prmDScopeID As New SqlParameter("@dscopeid", SqlDbType.Int)
        Dim prmWCCID As New SqlParameter("@wccid", SqlDbType.BigInt)
        Dim prmActivityId As New SqlParameter("@activityid", SqlDbType.Int)

        prmWCCID_I.Value = info.WCCID
        prmSCONID.Value = info.SCONID
        prmPackageId.Value = info.PackageId
        prmBAUTBASTDone.Value = info.BAUTDate
        prmWCTRDate.Value = info.WCTRDate
        prmLMBY.Value = info.LMBY
        prmDScopeID.Value = info.DScopeID
        prmWCCID.Direction = ParameterDirection.Output
        prmPOSubcon.Value = info.POSubcontractor
        prmActivityId.Value = info.ActivityId

        Command.Parameters.Add(prmWCCID_I)
        Command.Parameters.Add(prmSCONID)
        Command.Parameters.Add(prmPackageId)
        Command.Parameters.Add(prmBAUTBASTDone)
        Command.Parameters.Add(prmWCTRDate)
        Command.Parameters.Add(prmLMBY)
        Command.Parameters.Add(prmDScopeID)
        Command.Parameters.Add(prmWCCID)
        Command.Parameters.Add(prmPOSubcon)
        Command.Parameters.Add(prmActivityId)

        Try
            Connection.Open()
            Command.ExecuteNonQuery()
            wccid = Convert.ToInt32(prmWCCID.Value)

        Catch ex As Exception
            ErrorLogInsert(0, "ODWCC_I", ex.Message.ToString(), "uspWCC_ODWCC_IU")
        Finally
            Connection.Close()
        End Try
        Return wccid
    End Function

    Public Sub WCCConfigIU(ByVal info As WCCConfigInfo)
        Command = New SqlCommand("uspWCC_ConfigIU", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmWCCConfId As New SqlParameter("@WCCConfId", SqlDbType.BigInt)
        Dim prmPackageId As New SqlParameter("@packageid", SqlDbType.VarChar, 50)
        Dim prmATPRequired As New SqlParameter("@ATPRequired", SqlDbType.Bit)
        Dim prmQCRequired As New SqlParameter("@QCRequired", SqlDbType.Bit)
        Dim prmSubconId As New SqlParameter("@subconid", SqlDbType.Int)
        Dim prmDScopeId As New SqlParameter("@dscopeid", SqlDbType.Int)
        Dim prmLMBY As New SqlParameter("@lmby", SqlDbType.Int)
        Dim prmDocRequiredId As New SqlParameter("@DocRequiredId", SqlDbType.Int)

        prmWCCConfId.Value = info.WCCConfigId
        prmPackageId.Value = info.PackageId
        prmATPRequired.Value = info.ATPRequired
        prmQCRequired.Value = info.QCRequired
        prmSubconId.Value = info.SubconId
        prmDScopeId.Value = info.DScopeId
        prmLMBY.Value = info.LMBY
        prmDocRequiredId.Value = info.DocRequiredId

        Command.Parameters.Add(prmWCCConfId)
        Command.Parameters.Add(prmPackageId)
        Command.Parameters.Add(prmATPRequired)
        Command.Parameters.Add(prmQCRequired)
        Command.Parameters.Add(prmSubconId)
        Command.Parameters.Add(prmDScopeId)
        Command.Parameters.Add(prmLMBY)
        Command.Parameters.Add(prmDocRequiredId)

        Try
            Connection.Open()
            Command.ExecuteNonQuery()
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try

    End Sub

    Public Sub WCCDetailWork_I(ByVal info As WCCDetailWorkInfo)
        Command = New SqlCommand("uspWCC_ODWCCDetailWork_IU", Connection)
        Command.CommandType = CommandType.StoredProcedure
        Dim prmWCCID As New SqlParameter("@wccid", SqlDbType.BigInt)
        Dim prm_SISSES_SISorSitac As New SqlParameter("@sis_ses_SISorSITAC", SqlDbType.Bit)
        Dim prm_PKS_AJB_50_SISorSITAC As New SqlParameter("@PKS_AJB_50_SISorSITAC", SqlDbType.Bit)
        Dim prm_IMB_50_SISorSITAC As New SqlParameter("@IMB_50_SISorSITAC", SqlDbType.Bit)
        Dim prm_CA_LC_100_SISorSITAC As New SqlParameter("@CA_LC_100_SISorSITAC", SqlDbType.Bit)
        Dim prm_SITAC_Permitting_SISorSitac As New SqlParameter("@SITAC_Permitting_SISorSitac", SqlDbType.Bit)
        Dim prm_BAUT_2G_3G_CME As New SqlParameter("@BAUT_2G_3G_CME", SqlDbType.Bit)
        Dim prm_SDH_PDH_CME As New SqlParameter("@SDH_PDH_CME", SqlDbType.Bit)
        Dim prm_CME_BAST_2G_CME As New SqlParameter("@CME_BAST_2G_CME", SqlDbType.Bit)
        Dim prm_Additional_CME As New SqlParameter("@Additional_CME", SqlDbType.Bit)
        Dim prm_Survey_TI As New SqlParameter("@Survey_TI", SqlDbType.Bit)
        Dim prm_Dismantling_TI As New SqlParameter("@Dismantling_TI", SqlDbType.Bit)
        Dim prm_Reconfig_TI As New SqlParameter("@Reconfig_TI", SqlDbType.Bit)
        Dim prm_Enclosure_TI As New SqlParameter("@Enclosure_TI", SqlDbType.Bit)
        Dim prm_Services_TI As New SqlParameter("@Services_TI", SqlDbType.Bit)
        Dim prm_Frequency_License_TI As New SqlParameter("@Frequency_License_TI", SqlDbType.Bit)
        Dim prm_Initial_Tuning_NPO As New SqlParameter("@Initial_Tuning_NPO", SqlDbType.Bit)
        Dim prm_Cluster_Tuning_NPO As New SqlParameter("@Cluster_Tuning_NPO", SqlDbType.Bit)
        Dim prm_IBC_NPO As New SqlParameter("@IBC_NPO", SqlDbType.Bit)
        Dim prm_Optimization_NPO As New SqlParameter("@Optimization_NPO", SqlDbType.Bit)
        Dim prm_Site_Verification_NPO As New SqlParameter("@Site_Verification_NPO", SqlDbType.Bit)
        Dim prm_Detailed_RF_Covered_NPO As New SqlParameter("@Detailed_RF_Covered_NPO", SqlDbType.Bit)
        Dim prm_Change_Request_NPO As New SqlParameter("@Change_Request_NPO", SqlDbType.Bit)
        Dim prm_Design_for_MW_Access_MW_NPO As New SqlParameter("@Design_for_MW_Access_MW_NPO", SqlDbType.Bit)
        Dim prm_SDH_PDH_NPO As New SqlParameter("@SDH_PDH_NPO", SqlDbType.Bit)
        Dim prm_SIS_SES_NPO As New SqlParameter("@SIS_SES_NPO", SqlDbType.Bit)
        Dim prm_HICAP_BSC_COLO_DCS_NPO As New SqlParameter("@HICAP_BSC_COLO_DCS_NPO", SqlDbType.Bit)


        prmWCCID.Value = info.WCCID
        prm_SISSES_SISorSitac.Value = info.SISorSES_SISorSITAC
        prm_PKS_AJB_50_SISorSITAC.Value = info.PKSorAJB50Perc_SISorSITAC
        prm_IMB_50_SISorSITAC.Value = info.IMB50Perc_SISorSITAC
        prm_CA_LC_100_SISorSITAC.Value = info.CAorLC100Perc_SISorSITAC
        prm_SITAC_Permitting_SISorSitac.Value = info.SITACPermitting_SISorSITAC
        prm_BAUT_2G_3G_CME.Value = info.BAUT2G3G_CME
        prm_SDH_PDH_CME.Value = info.SDHPDH_CME
        prm_CME_BAST_2G_CME.Value = info.CMEorBAST2G_CME
        prm_Additional_CME.Value = info.Additional_CME
        prm_Survey_TI.Value = info.Survey_TI
        prm_Dismantling_TI.Value = info.Dismantling_TI
        prm_Reconfig_TI.Value = info.Reconfig_TI
        prm_Enclosure_TI.Value = info.Enclosure_TI
        prm_Services_TI.Value = info.Services_TI
        prm_Frequency_License_TI.Value = info.FrequencyLicense_TI
        prm_Initial_Tuning_NPO.Value = info.InitialTuning_NPO
        prm_Cluster_Tuning_NPO.Value = info.ClusterTuning_NPO
        prm_IBC_NPO.Value = info.IBC_NPO
        prm_Optimization_NPO.Value = info.Optimization_NPO
        prm_Site_Verification_NPO.Value = info.SiteVerification_NPO
        prm_Detailed_RF_Covered_NPO.Value = info.DetailRFCovered_NPO
        prm_Change_Request_NPO.Value = info.ChangeRequest_NPO
        prm_Design_for_MW_Access_MW_NPO.Value = info.DesignForMW_NPO
        prm_SDH_PDH_NPO.Value = info.SDHPDH_NPO
        prm_SIS_SES_NPO.Value = info.SISSES_NPO
        prm_HICAP_BSC_COLO_DCS_NPO.Value = info.HICAP_BSC_COLO_DCS_NPO

        Command.Parameters.Add(prmWCCID)
        Command.Parameters.Add(prm_SISSES_SISorSitac)
        Command.Parameters.Add(prm_PKS_AJB_50_SISorSITAC)
        Command.Parameters.Add(prm_IMB_50_SISorSITAC)
        Command.Parameters.Add(prm_CA_LC_100_SISorSITAC)
        Command.Parameters.Add(prm_SITAC_Permitting_SISorSitac)
        Command.Parameters.Add(prm_BAUT_2G_3G_CME)
        Command.Parameters.Add(prm_SDH_PDH_CME)
        Command.Parameters.Add(prm_CME_BAST_2G_CME)
        Command.Parameters.Add(prm_Additional_CME)
        Command.Parameters.Add(prm_Survey_TI)
        Command.Parameters.Add(prm_Dismantling_TI)
        Command.Parameters.Add(prm_Reconfig_TI)
        Command.Parameters.Add(prm_Enclosure_TI)
        Command.Parameters.Add(prm_Services_TI)
        Command.Parameters.Add(prm_Frequency_License_TI)
        Command.Parameters.Add(prm_Initial_Tuning_NPO)
        Command.Parameters.Add(prm_Cluster_Tuning_NPO)
        Command.Parameters.Add(prm_IBC_NPO)
        Command.Parameters.Add(prm_Optimization_NPO)
        Command.Parameters.Add(prm_Site_Verification_NPO)
        Command.Parameters.Add(prm_Detailed_RF_Covered_NPO)
        Command.Parameters.Add(prm_Change_Request_NPO)
        Command.Parameters.Add(prm_Design_for_MW_Access_MW_NPO)
        Command.Parameters.Add(prm_SDH_PDH_NPO)
        Command.Parameters.Add(prm_SIS_SES_NPO)
        Command.Parameters.Add(prm_HICAP_BSC_COLO_DCS_NPO)

        Try
            Connection.Open()
            Command.ExecuteNonQuery()
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try




    End Sub

    Public Function IsATPAvailable(ByVal packageid As String, ByVal subconid As Integer) As Boolean
        Dim isAvailable As Boolean = False
        Command = New SqlCommand("uspWCC_CheckingATPRequired", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmSubconId As New SqlParameter("@subconid", SqlDbType.Int)
        Dim prmPackageId As New SqlParameter("@packageid", SqlDbType.VarChar, 50)
        Dim prmIsATPAvailable As New SqlParameter("@IsATPAvailable", SqlDbType.Bit)

        prmSubconId.Value = subconid
        prmPackageId.Value = packageid
        prmIsATPAvailable.Direction = ParameterDirection.Output

        Command.Parameters.Add(prmSubconId)
        Command.Parameters.Add(prmPackageId)
        Command.Parameters.Add(prmIsATPAvailable)

        Try
            Connection.Open()
            Command.ExecuteNonQuery()
            isAvailable = Convert.ToBoolean(prmIsATPAvailable.Value)
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try
        Return isAvailable
    End Function

    Public Function IsQCAvailable(ByVal packageid As String, ByVal subconid As Integer) As Boolean
        Dim isAvailable As Boolean = False
        Command = New SqlCommand("uspWCC_CheckingQCRequired", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmSubconId As New SqlParameter("@subconid", SqlDbType.Int)
        Dim prmPackageId As New SqlParameter("@packageid", SqlDbType.VarChar, 50)
        Dim prmIsQCAvailable As New SqlParameter("@IsQCAvailable", SqlDbType.Bit)

        prmSubconId.Value = subconid
        prmPackageId.Value = packageid
        prmIsQCAvailable.Direction = ParameterDirection.Output

        Command.Parameters.Add(prmSubconId)
        Command.Parameters.Add(prmPackageId)
        Command.Parameters.Add(prmIsQCAvailable)

        Try
            Connection.Open()
            Command.ExecuteNonQuery()
            isAvailable = Convert.ToBoolean(prmIsQCAvailable.Value)
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try
        Return isAvailable
    End Function

    Public Function IsATPorQCDone(ByVal docid As Integer, ByVal packageid As String) As Boolean
        Dim isDone As Boolean = False

        Command = New SqlCommand("uspWCC_CheckDocumentATP_or_QCDone", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmDocId As New SqlParameter("@DocId", SqlDbType.Int)
        Dim prmPackageId As New SqlParameter("@packageid", SqlDbType.VarChar, 50)
        Dim prmIsDone As New SqlParameter("@IsDone", SqlDbType.Bit)

        prmDocId.Value = docid
        prmPackageId.Value = packageid
        prmIsDone.Direction = ParameterDirection.Output

        Command.Parameters.Add(prmDocId)
        Command.Parameters.Add(prmPackageId)
        Command.Parameters.Add(prmIsDone)

        Try
            Connection.Open()
            Command.ExecuteNonQuery()
            isDone = Convert.ToBoolean(prmIsDone.Value)
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try
        Return isDone
    End Function

    Public Function GetRequiredDocWCC(ByVal packageid As String, ByVal subconid As Integer) As List(Of DocRequiredInfo)
        Dim list As New List(Of DocRequiredInfo)
        Command = New SqlCommand("uspWCC_GetRequiredDoc", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmPackageId As New SqlParameter("@packageid", SqlDbType.VarChar, 50)
        Dim prmSubconId As New SqlParameter("@subconid", SqlDbType.Int)

        prmPackageId.Value = packageid
        prmSubconId.Value = subconid

        Command.Parameters.Add(prmPackageId)
        Command.Parameters.Add(prmSubconId)

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read()
                    Dim info As New DocRequiredInfo
                    info.DocReqId = Integer.Parse(DataReader.Item("DocReq_Id"))
                    info.RequiredDoc = Convert.ToString(DataReader.Item("RequestDoc"))
                    list.Add(info)
                End While
            End If
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try
        Return list
    End Function

    Public Function GetRequiredDocIdByPackageid(ByVal packageid As String) As Integer
        Dim requiredDocId As Integer = 0
        Command = New SqlCommand("uspWCC_GetDocRequiredId", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmPackageId As New SqlParameter("@packageid", SqlDbType.VarChar, 50)
        Dim prmDocRequiredId As New SqlParameter("@docrequiredid", SqlDbType.Int)

        prmPackageId.Value = packageid
        prmDocRequiredId.Direction = ParameterDirection.Output

        Command.Parameters.Add(prmPackageId)
        Command.Parameters.Add(prmDocRequiredId)

        Try
            Connection.Open()
            Command.ExecuteNonQuery()
            requiredDocId = Integer.Parse(prmDocRequiredId.Value.ToString())
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try
        Return requiredDocId
    End Function

    Public Sub WorkflowGrouping_I(ByVal info As WCCFlowGroupingInfo)
        Command = New SqlCommand("uspWCC_FlowGrouping_I", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmWFID As New SqlParameter("@WFID", SqlDbType.Int)
        Dim prmLMBY As New SqlParameter("@lmby", SqlDbType.VarChar, 250)

        prmWFID.Value = info.WFID
        prmLMBY.Value = info.LMBY

        Command.Parameters.Add(prmWFID)
        Command.Parameters.Add(prmLMBY)

        Try
            Connection.Open()
            Command.ExecuteNonQuery()
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try
    End Sub

    Public Function GetWorkflows() As List(Of WCCFlowGroupingInfo)
        Dim list As New List(Of WCCFlowGroupingInfo)
        Command = New SqlCommand("uspWCC_GetWorkFlow", Connection)
        Command.CommandType = CommandType.StoredProcedure
        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read()
                    Dim info As New WCCFlowGroupingInfo
                    info.WFID = Integer.Parse(DataReader.Item("WFID").ToString())
                    info.FlowName = Convert.ToString(DataReader.Item("WFDesc"))
                    list.Add(info)
                End While
            End If
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try
        Return list
    End Function

    Public Function GetWCCWorkflowGrouping() As List(Of WCCFlowGroupingInfo)
        Dim list As New List(Of WCCFlowGroupingInfo)
        Command = New SqlCommand("uspWCC_GetWCCWorkFlowGrouping", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read
                    Dim info As New WCCFlowGroupingInfo
                    info.FlowGroupingId = Integer.Parse(DataReader.Item("WFGrouping_Id").ToString())
                    info.FlowName = Convert.ToString(DataReader.Item("WFDesc"))
                    info.WFID = Integer.Parse(DataReader.Item("WFID").ToString())
                    info.LMBY = Convert.ToString(DataReader.Item("LMBY"))
                    info.LMDT = Convert.ToDateTime(DataReader.Item("LMDT"))
                    list.Add(info)
                End While
            End If
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try

        Return list
    End Function

    Public Function GetWCCBautApproved(ByVal packageid As String, ByVal bautdocid As Integer) As DateTime
        Dim bautapproveddate As New DateTime
        Command = New SqlCommand("uspWCC_GetBAUTApproved", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmBautDocId As New SqlParameter("@bautdocid", SqlDbType.Int)
        Dim prmPackageId As New SqlParameter("@packageid", SqlDbType.VarChar, 50)
        Dim prmBautApprovedDate As New SqlParameter("@bautapproveddate", SqlDbType.DateTime)

        prmBautDocId.Value = bautdocid
        prmPackageId.Value = packageid
        prmBautApprovedDate.Direction = ParameterDirection.Output

        Command.Parameters.Add(prmBautDocId)
        Command.Parameters.Add(prmPackageId)
        Command.Parameters.Add(prmBautApprovedDate)

        Try
            Connection.Open()
            Command.ExecuteNonQuery()
            bautapproveddate = Convert.ToDateTime(prmBautApprovedDate.Value)
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try

        Return bautapproveddate
    End Function

    Public Function GetWCCDetailWork(ByVal wccid As Int32) As WCCDetailWorkInfo
        Dim info As New WCCDetailWorkInfo

        Command = New SqlCommand("uspWCC_GetDetailWork", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmWCCID As New SqlParameter("@wccid", SqlDbType.BigInt)
        prmWCCID.Value = wccid
        Command.Parameters.Add(prmWCCID)

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If (DataReader.HasRows) Then
                While DataReader.Read()
                    info.Additional_CME = Convert.ToBoolean(DataReader.Item("Additional_CME"))
                    info.BAUT2G3G_CME = Convert.ToBoolean(DataReader.Item("BAUT_2G_3G_CME"))
                    info.CAorLC100Perc_SISorSITAC = Convert.ToBoolean(DataReader.Item("CA_LC_100Perc_SISorSITAC"))
                    info.ChangeRequest_NPO = Convert.ToBoolean(DataReader.Item("Change_Request_NPO"))
                    info.ClusterTuning_NPO = Convert.ToBoolean(DataReader.Item("Cluster_Tuning_NPO"))
                    info.CMEorBAST2G_CME = Convert.ToBoolean(DataReader.Item("CME_BAST_2G_CME"))
                    info.DesignForMW_NPO = Convert.ToBoolean(DataReader.Item("Design_For_MW_Access_MW_NPO"))
                    info.DetailRFCovered_NPO = Convert.ToBoolean(DataReader.Item("Detailed_RF_Covered_NPO"))
                    info.Dismantling_TI = Convert.ToBoolean(DataReader.Item("Dismantling_TI"))
                    info.Enclosure_TI = Convert.ToBoolean(DataReader.Item("Enclosure_TI"))
                    info.FrequencyLicense_TI = Convert.ToBoolean(DataReader.Item("Frequency_License_TI"))
                    info.HICAP_BSC_COLO_DCS_NPO = Convert.ToBoolean(DataReader.Item("HICAP_BSC_COLO_DCS_NPO"))
                    info.IBC_NPO = Convert.ToBoolean(DataReader.Item("IBC_NPO"))
                    info.IMB50Perc_SISorSITAC = Convert.ToBoolean(DataReader.Item("IMB_50Perc_SISorSITAC"))
                    info.InitialTuning_NPO = Convert.ToBoolean(DataReader.Item("Initial_Tuning_NPO"))
                    info.Optimization_NPO = Convert.ToBoolean(DataReader.Item("Optimization_NPO"))
                    info.PKSorAJB50Perc_SISorSITAC = Convert.ToBoolean(DataReader.Item("PKS_AJB_50Perc_SISorSITAC"))
                    info.Reconfig_TI = Convert.ToBoolean(DataReader.Item("Reconfig_TI"))
                    info.SDHPDH_CME = Convert.ToBoolean(DataReader.Item("SDH_PDH_CME"))
                    info.SDHPDH_NPO = Convert.ToBoolean(DataReader.Item("SDH_PDH_NPO"))
                    info.Services_TI = Convert.ToBoolean(DataReader.Item("Services_TI"))
                    info.SISorSES_SISorSITAC = Convert.ToBoolean(DataReader.Item("SIS_SES_SISorSITAC"))
                    info.SISSES_NPO = Convert.ToBoolean(DataReader.Item("SIS_SES_NPO"))
                    info.SITACPermitting_SISorSITAC = Convert.ToBoolean(DataReader.Item("SITAC_PERMITTING_SISorSITAC"))
                    info.SiteVerification_NPO = Convert.ToBoolean(DataReader.Item("Site_Verification_NPO"))
                    info.Survey_TI = Convert.ToBoolean(DataReader.Item("Survey_TI"))
                    info.WCCDetailWorkId = Convert.ToInt32(DataReader.Item("WCCDetailWork_Id"))
                    info.WCCID = Convert.ToInt32(DataReader.Item("WCC_ID"))

                End While
            End If
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try

        Return info
    End Function

    Public Function IsDocUploadCompleted(ByVal wccid As Int32) As Boolean
        Dim isCompleting As Boolean = True
        Command = New SqlCommand("uspWCC_isCompletingDocUpload", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmWCCID As New SqlParameter("@wccid", SqlDbType.BigInt)
        Dim prmIsCompleted As New SqlParameter("@isCompleted", SqlDbType.Bit)

        prmWCCID.Value = wccid
        prmIsCompleted.Direction = ParameterDirection.Output

        Command.Parameters.Add(prmWCCID)
        Command.Parameters.Add(prmIsCompleted)

        Try
            Connection.Open()
            Command.ExecuteNonQuery()
            isCompleting = Convert.ToBoolean(prmIsCompleted.Value)
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try
        Return isCompleting
    End Function

    Public Sub WCCSubmitDocument(ByVal wccid As Int32)
        Command = New SqlCommand("uspWCC_SubmitWccDocument", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmWCCID As New SqlParameter("@wccid", SqlDbType.BigInt)
        prmWCCID.Value = wccid
        Command.Parameters.Add(prmWCCID)

        Try
            Connection.Open()
            Command.ExecuteNonQuery()
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try
    End Sub

    Public Sub WCCSubmitRollBack(ByVal wccid As Int32, ByVal docid As Integer)
        Command = New SqlCommand("uspWCC_SubmitWccRollback", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmWCCID As New SqlParameter("@wccid", SqlDbType.BigInt)
        Dim prmDocId As New SqlParameter("@docid", SqlDbType.Int)

        prmWCCID.Value = wccid
        prmDocId.Value = docid

        Command.Parameters.Add(prmWCCID)
        Command.Parameters.Add(prmDocId)

        Try
            Connection.Open()
            Command.ExecuteNonQuery()
        Catch ex As Exception

        Finally
            Connection.Close()

        End Try
    End Sub

    Public Function GetSubconInsideSiteDocumentBaseWPID(ByVal packageid As String) As List(Of WCCInfo)
        Dim list As New List(Of WCCInfo)
        Command = New SqlCommand("uspWCC_GetSubcon_SiteDoc", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmPackageId As New SqlParameter("@packageid", SqlDbType.VarChar, 50)
        prmPackageId.Value = packageid
        Command.Parameters.Add(prmPackageId)

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If (DataReader.HasRows) Then
                While (DataReader.Read())
                    Dim info As New WCCInfo
                    info.SCONID = Integer.Parse(DataReader.Item("SCON_ID").ToString())
                    info.SubconName = Convert.ToString(DataReader.Item("SCon_Name"))
                    info.SiteNo = Convert.ToString(DataReader.Item("SiteNo"))
                    info.SiteName = Convert.ToString(DataReader.Item("SiteName"))
                    info.PONO = Convert.ToString(DataReader.Item("PONO"))
                    info.POSubcontractor = Convert.ToString(DataReader.Item("POSubcontractor"))
                    info.PackageId = Convert.ToString(DataReader.Item("Workpackageid"))
                    info.ScopeName = Convert.ToString(DataReader.Item("Dscope_Name"))
                    info.DScopeID = Integer.Parse(DataReader.Item("DScope_Id").ToString())
                    list.Add(info)
                End While
            End If
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try

        Return list
    End Function

    Public Sub WCCUpdateIssuanceDate(ByVal wccid As Int32)
        Command = New SqlCommand("uspWCC_UpdateIssuanceDate", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmWCCID As New SqlParameter("@wccid", SqlDbType.BigInt)
        prmWCCID.Value = wccid
        Command.Parameters.Add(prmWCCID)

        Try
            Connection.Open()
            Command.ExecuteNonQuery()
        Catch ex As Exception

        Finally
            Connection.Close()
        End Try
    End Sub

    Public Function CheckingAvailableWPID(ByVal packageid As String, ByVal userid As Integer) As Integer
        Dim rowCount As Integer = 0
        Command = New SqlCommand("uspWCC_CreationChecking", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmPackageId As New SqlParameter("@packageid", SqlDbType.VarChar, 50)
        Dim prmUsrId As New SqlParameter("@usrid", SqlDbType.Int)
        Dim prmRowCount As New SqlParameter("@rowcount", SqlDbType.Int)

        prmPackageId.Value = packageid
        prmUsrId.Value = userid
        prmRowCount.Direction = ParameterDirection.Output

        Command.Parameters.Add(prmPackageId)
        Command.Parameters.Add(prmUsrId)
        Command.Parameters.Add(prmRowCount)

        Try
            Connection.Open()
            Command.ExecuteNonQuery()
            rowCount = Integer.Parse(prmRowCount.Value.ToString())
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try

        Return rowCount
    End Function

    Public Function GetDocAcceptanceDate(ByVal packageid As String, ByVal docid As Integer) As System.Nullable(Of DateTime)
        Dim acceptancedate As System.Nullable(Of DateTime) = Nothing
        Command = New SqlCommand("uspWCC_GetDocApproved", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmDocId As New SqlParameter("@docid", SqlDbType.Int)
        Dim prmPackageId As New SqlParameter("@packageid", SqlDbType.VarChar, 50)
        Dim prmRowCount As New SqlParameter("@rowcount", SqlDbType.Int)
        Dim prmAcceptanceDate As New SqlParameter("@acceptancedate", SqlDbType.DateTime)

        prmDocId.Value = docid
        prmPackageId.Value = packageid
        prmRowCount.Direction = ParameterDirection.Output
        prmAcceptanceDate.Direction = ParameterDirection.Output

        Command.Parameters.Add(prmDocId)
        Command.Parameters.Add(prmPackageId)
        Command.Parameters.Add(prmRowCount)
        Command.Parameters.Add(prmAcceptanceDate)

        Try
            Connection.Open()
            Command.ExecuteNonQuery()
            Dim rowcount As Integer = Integer.Parse(prmRowCount.Value.ToString())
            If rowcount > 0 Then
                acceptancedate = Convert.ToDateTime(prmAcceptanceDate.Value.ToString())
            End If
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try

        Return acceptancedate
    End Function

    Public Function UpdateDocPath(ByVal wccid As Int32, ByVal docpath As String, ByVal orgdocpath As String) As Boolean
        Dim isSucceed As Boolean = True

        Command = New SqlCommand("uspWCC_UpdateDocPath", Connection)
        Command.CommandType = CommandType.StoredProcedure


        Dim prmWCCID As New SqlParameter("@wccid", SqlDbType.BigInt)
        Dim prmDocPath As New SqlParameter("@docpath", SqlDbType.VarChar, 2000)
        Dim prmOrgDocPath As New SqlParameter("@orgdocpath", SqlDbType.VarChar, 2000)

        prmWCCID.Value = wccid
        prmDocPath.Value = docpath
        prmOrgDocPath.Value = orgdocpath

        Command.Parameters.Add(prmWCCID)
        Command.Parameters.Add(prmOrgDocPath)
        Command.Parameters.Add(prmDocPath)

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

    Public Function GetWCCDocPath(ByVal wccid As Int32) As String
        Dim docpath As String = String.Empty
        Command = New SqlCommand("uspWCC_GetWCCDocPath", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmWCCID As New SqlParameter("@wccid", SqlDbType.BigInt)
        Dim prmDocPath As New SqlParameter("@docpath", SqlDbType.VarChar, 2000)

        prmWCCID.Value = wccid
        prmDocPath.Direction = ParameterDirection.Output

        Command.Parameters.Add(prmWCCID)
        Command.Parameters.Add(prmDocPath)

        Try
            Connection.Open()
            Command.ExecuteNonQuery()
            docpath = Convert.ToString(prmDocPath.Value)
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try

        Return docpath
    End Function

    Public Function CheckingPOSubconIsAlreadyCreated(ByVal posubcon As String) As String
        Dim packageid As String = String.Empty
        Command = New SqlCommand("uspWCC_IsPOSubconAlreadyCreated", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmPOSubcon As New SqlParameter("@posubcon", SqlDbType.VarChar, 200)
        Dim prmPackageId As New SqlParameter("@packageid", SqlDbType.VarChar, 50)
        Dim prmIsAlready As New SqlParameter("@IsAlready", SqlDbType.Bit)

        prmPOSubcon.Value = posubcon
        prmPackageId.Direction = ParameterDirection.Output
        prmIsAlready.Direction = ParameterDirection.Output

        Command.Parameters.Add(prmPOSubcon)
        Command.Parameters.Add(prmPackageId)
        Command.Parameters.Add(prmIsAlready)

        Try
            Connection.Open()
            Command.ExecuteNonQuery()
            Dim isAlreadyCreated As Boolean = Convert.ToBoolean(prmIsAlready.Value)
            If isAlreadyCreated = True Then
                packageid = Convert.toString(prmPackageId.Value)
            End If
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try

        Return packageid
    End Function

    Public Function IsWCCDone(ByVal wccid As Int32) As Boolean
        Dim isDone As Boolean = False
        Command = New SqlCommand("uspWCC_IsWCCDone", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmWCCID As New SqlParameter("@wccid", SqlDbType.BigInt)
        Dim prmIsDone As New SqlParameter("@isDone", SqlDbType.Bit)

        prmWCCID.Value = wccid
        prmIsDone.Direction = ParameterDirection.Output

        Command.Parameters.Add(prmWCCID)
        Command.Parameters.Add(prmIsDone)

        Try
            Connection.Open()
            Command.ExecuteNonQuery()
            isDone = Convert.ToBoolean(prmIsDone.Value)
        Catch ex As Exception

        Finally
            Connection.Close()
        End Try

        Return isDone
    End Function

#End Region

#Region "WCC Transaction"
    Public Function WCCTransaction_I(ByVal info As WCCTransactionInfo) As Boolean
        Dim isSucceed As Boolean = True
        Command = New SqlCommand("uspWCC_WFWCCTransaction_I", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmWCCID As New SqlParameter("@WCCID", SqlDbType.BigInt)
        Dim prmWFID As New SqlParameter("@WFID", SqlDbType.Int)
        Dim prmTSKID As New SqlParameter("@TSKID", SqlDbType.Int)
        Dim prmRoleID As New SqlParameter("@RoleID", SqlDbType.Int)
        Dim prmStartTime As New SqlParameter("@StartTime", SqlDbType.DateTime)
        Dim prmEndTime As New SqlParameter("@EndTime", SqlDbType.DateTime)
        Dim prmStatus As New SqlParameter("@Status", SqlDbType.Int)
        Dim prmRstatus As New SqlParameter("@Rstatus", SqlDbType.Int)
        Dim prmLMBY As New SqlParameter("@LMBY", SqlDbType.VarChar, 20)
        Dim prmLMDT As New SqlParameter("@LMDT", SqlDbType.DateTime)
        Dim prmXVal As New SqlParameter("@XVal", SqlDbType.Int)
        Dim prmYVal As New SqlParameter("@YVal", SqlDbType.Int)
        Dim prmUGPID As New SqlParameter("@UGPID", SqlDbType.Int)
        Dim prmPageNo As New SqlParameter("@PageNo", SqlDbType.Int)
        Dim prmSorderNo As New SqlParameter("@sorderno", SqlDbType.Int)

        prmWCCID.Value = info.WCCID
        prmWFID.Value = info.WFID
        prmTSKID.Value = info.TSKID
        prmRoleID.Value = info.RoleId
        prmStartTime.Value = info.StartDateTime
        prmEndTime.Value = info.EndDateTime
        prmStatus.Value = info.Status
        prmRstatus.Value = info.RStatus
        prmLMBY.Value = info.LMBY
        prmXVal.Value = info.XVal
        prmYVal.Value = info.YVal
        prmUGPID.Value = info.UGPId
        prmPageNo.Value = info.PageNo
        prmSorderNo.Value = info.SOrderNo

        Command.Parameters.Add(prmWCCID)
        Command.Parameters.Add(prmWFID)
        Command.Parameters.Add(prmTSKID)
        Command.Parameters.Add(prmRoleID)
        Command.Parameters.Add(prmStartTime)
        Command.Parameters.Add(prmEndTime)
        Command.Parameters.Add(prmStatus)
        Command.Parameters.Add(prmRstatus)
        Command.Parameters.Add(prmLMBY)
        Command.Parameters.Add(prmXVal)
        Command.Parameters.Add(prmYVal)
        Command.Parameters.Add(prmUGPID)
        Command.Parameters.Add(prmPageNo)
        Command.Parameters.Add(prmSorderNo)

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

    Public Sub DeleteWCCTransaction(ByVal wccid As Int32)
        Command = New SqlCommand("uspWCC_DeleteWCCTransaction", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmWCCID As New SqlParameter("@wccid", SqlDbType.BigInt)
        prmWCCID.Value = wccid
        Command.Parameters.Add(prmWCCID)

        Try
            Connection.Open()
            Command.ExecuteNonQuery()
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try
    End Sub

    Public Function WCCCheckingProcess(ByVal wccid As Int32) As Integer
        Dim status As Integer = 0
        Command = New SqlCommand("uspWCC_CheckingProcess", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmWCCID As New SqlParameter("@wccid", SqlDbType.BigInt)
        Dim prmStatus As New SqlParameter("@status", SqlDbType.Int)

        prmWCCID.Value = wccid
        prmStatus.Direction = ParameterDirection.Output

        Command.Parameters.Add(prmWCCID)
        Command.Parameters.Add(prmStatus)

        Try
            Connection.Open()
            Command.ExecuteNonQuery()
            status = Integer.Parse(prmStatus.Value.ToString())
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try
        Return status
    End Function

    Public Sub WCCIssuanceDate_U(ByVal wccid As Int32, ByVal issuancedate As Nullable(Of DateTime))
        Command = New SqlCommand("uspWCC_IssuanceDate_U", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmWCCID As New SqlParameter("@wccid", SqlDbType.BigInt)
        Dim prmIssuanceDate As New SqlParameter("@issuancedate", SqlDbType.DateTime)

        prmWCCID.Value = wccid
        prmIssuanceDate.Value = issuancedate

        Command.Parameters.Add(prmWCCID)
        Command.Parameters.Add(prmIssuanceDate)

        Try
            Connection.Open()
            Command.ExecuteNonQuery()
        Catch ex As Exception

        Finally
            Connection.Close()
        End Try

    End Sub

    Public Function WCCTransaction_Pending(ByVal userid As Integer, ByVal isViewAll As Boolean) As List(Of WCCTransactionInfo)
        Dim list As New List(Of WCCTransactionInfo)

        If isViewAll = True Then
            Command = New SqlCommand("uspDashboardAgendaSiteDocCount_WCCApproverReviewer_All", Connection)
        Else
            Command = New SqlCommand("uspDashboardAgendaSiteDocCount_WCCApproverReviewer", Connection)
        End If

        Command.CommandType = CommandType.StoredProcedure

        Dim prmUserId As New SqlParameter("@usrid", SqlDbType.Int)
        prmUserId.Value = userid
        Command.Parameters.Add(prmUserId)

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If (DataReader.HasRows) Then
                While (DataReader.Read())
                    Dim info As New WCCTransactionInfo
                    info.SNO = Convert.ToInt32(DataReader.Item("sno"))
                    info.WCCID = Convert.ToInt32(DataReader.Item("wcc_id"))
                    If Not String.IsNullOrEmpty(Convert.ToString(DataReader.Item("Issuance_Date"))) Then
                        info.IssuanceDate = Convert.ToDateTime(DataReader.Item("Issuance_Date"))
                    End If

                    info.SiteNo = Convert.ToString(DataReader.Item("SiteNo"))
                    info.SiteName = Convert.ToString(DataReader.Item("SiteName"))
                    info.PackageId = Convert.ToString(DataReader.Item("workpackageid"))
                    info.POName = Convert.ToString(DataReader.Item("POName"))
                    info.PONO = Convert.ToString(DataReader.Item("PONO"))
                    info.POSubcontractor = Convert.ToString(DataReader.Item("PO_Subcontractor"))
                    info.SubconName = Convert.ToString(DataReader.Item("SCon_Name"))
                    If Not String.IsNullOrEmpty(Convert.ToString(DataReader.Item("BAUT_BAST_Date"))) Then
                        info.BAUTDate = Convert.ToDateTime(DataReader.Item("BAUT_BAST_Date"))
                    End If

                    info.WCTRDate = Convert.ToDateTime(DataReader.Item("WCTR_Date"))
                    info.ScopeName = Convert.ToString(DataReader.Item("DScope_Name"))
                    info.Scope = Convert.ToString(DataReader.Item("Scope_Name"))
                    info.ActivityId = Integer.Parse(DataReader.Item("Activity_Id").ToString())
                    list.Add(info)
                End While
            End If
        Catch ex As Exception

        End Try

        Return list
    End Function

    Public Function WCCDashboardAgenda(ByVal userid As Integer) As DataTable
        Dim dtWCCDashboard As New DataTable
        Command = New SqlCommand("uspWCC_GetDashboard", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmUserId As New SqlParameter("@usrid", SqlDbType.Int)
        prmUserId.Value = userid
        Command.Parameters.Add(prmUserId)

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                dtWCCDashboard.Load(DataReader)
            End If
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try

        Return dtWCCDashboard
    End Function

    Public Function WCCDashboardSubconAgenda(ByVal userid As Integer) As DataTable
        Dim dtWCCDashboard As New DataTable
        Command = New SqlCommand("uspWCC_GetDashboard_Subcon", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmUserId As New SqlParameter("@usrid", SqlDbType.Int)
        prmUserId.Value = userid
        Command.Parameters.Add(prmUserId)

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                dtWCCDashboard.Load(DataReader)
            End If
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try

        Return dtWCCDashboard
    End Function

    Public Function GetWCCTaskPendingCount(ByVal userid As Integer) As Integer
        Dim rowCount As Integer = 0

        Command = New SqlCommand("uspDashboardAgendaSiteDocCount_WCCApproverReviewer_count", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmUserId As New SqlParameter("@usrid", SqlDbType.BigInt)
        Dim prmRowCount As New SqlParameter("@rowCount", SqlDbType.Int)

        prmUserId.Value = userid
        prmRowCount.Direction = ParameterDirection.Output

        Command.Parameters.Add(prmUserId)
        Command.Parameters.Add(prmRowCount)

        Try
            Connection.Open()
            Command.ExecuteNonQuery()
            rowCount = Integer.Parse(prmRowCount.Value.ToString())
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try
        Return rowCount
    End Function

    Public Function WCCDocApproved(ByVal wccid As Int32, ByVal userid As Integer, ByVal docid As Integer) As Boolean
        Dim isSucceed As Boolean = True

        Command = New SqlCommand("uspWCC_DocApproved", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmWCCID As New SqlParameter("@wccid", SqlDbType.BigInt)
        Dim prmUserID As New SqlParameter("@userid", SqlDbType.Int)
        Dim prmDocId As New SqlParameter("@docid", SqlDbType.Int)

        prmWCCID.Value = wccid
        prmUserID.Value = userid
        prmDocId.Value = docid

        Command.Parameters.Add(prmWCCID)
        Command.Parameters.Add(prmUserID)
        Command.Parameters.Add(prmDocId)

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

    Public Function WCCDocRejected(ByVal wccid As Int32, ByVal userid As Integer, ByVal docid As Integer, ByVal categories As String, ByVal remarks As String) As Boolean
        Dim isSucceed As Boolean = True

        Command = New SqlCommand("uspWCC_DocRejected", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmWCCID As New SqlParameter("@wccid", SqlDbType.BigInt)
        Dim prmUserID As New SqlParameter("@userid", SqlDbType.Int)
        Dim prmDocId As New SqlParameter("@docid", SqlDbType.Int)
        Dim prmRemarks As New SqlParameter("@remarks", SqlDbType.VarChar, 2000)
        Dim prmCategories As New SqlParameter("@categories", SqlDbType.VarChar, 400)

        prmWCCID.Value = wccid
        prmUserID.Value = userid
        prmDocId.Value = docid
        prmRemarks.Value = remarks
        prmCategories.Value = categories

        Command.Parameters.Add(prmWCCID)
        Command.Parameters.Add(prmUserID)
        Command.Parameters.Add(prmDocId)
        Command.Parameters.Add(prmRemarks)
        Command.Parameters.Add(prmCategories)

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

    Public Function WCCGetApprovalDocument(ByVal wccid As Int32) As DataTable
        Dim dtApproval As New DataTable
        Command = New SqlCommand("uspWCC_GetApprovalDocument", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmWCCID As New SqlParameter("@wccid", SqlDbType.BigInt)
        prmWCCID.Value = wccid
        Command.Parameters.Add(prmWCCID)

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                dtApproval.Load(DataReader)
            End If
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try

        Return dtApproval
    End Function

    Public Function CheckIsWCCDone(ByVal wccid As Int32) As Boolean
        Dim isDone As Boolean = False
        Command = New SqlCommand("uspWCC_CheckIsWCCDone", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmWCCID As New SqlParameter("@wccid", SqlDbType.BigInt)
        Dim prmIsDone As New SqlParameter("@isDone", SqlDbType.Bit)

        prmWCCID.Value = wccid
        prmIsDone.Direction = ParameterDirection.Output

        Command.Parameters.Add(prmWCCID)
        Command.Parameters.Add(prmIsDone)

        Try
            Connection.Open()
            Command.ExecuteNonQuery()
            isDone = Convert.ToBoolean(prmIsDone.Value)
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try
        Return isDone
    End Function

    Public Function WCCGetUserLocation(ByVal userid As Integer) As List(Of WCCPendingInfo)
        Dim list As New List(Of WCCPendingInfo)

        Command = New SqlCommand("uspWCC_UserUnderSignature", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmUserId As New SqlParameter("@usrid", SqlDbType.Int)
        prmUserId.Value = userid
        Command.Parameters.Add(prmUserId)

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If (DataReader.HasRows) Then
                While DataReader.Read()
                    Dim info As New WCCPendingInfo
                    info.SiteName = Convert.ToString(DataReader.Item("SiteName"))
                    info.SiteNo = Convert.ToString(DataReader.Item("SiteNo"))
                    info.PONO = Convert.ToString(DataReader.Item("PONo"))
                    info.POName = Convert.ToString(DataReader.Item("POName"))
                    info.PackageId = Convert.ToString(DataReader.Item("workpackageid"))
                    info.SubmitDate = Convert.ToDateTime(DataReader.Item("SubmitDate"))
                    info.UserLocation = Convert.ToString(DataReader.Item("userLocation"))
                    info.POSubcontractor = Convert.ToString(DataReader.Item("PO_Subcontractor"))
                    info.SubconName = Convert.ToString(DataReader.Item("Scon_Name"))
                    info.ScopeName = Convert.ToString(DataReader.Item("DScope_Name"))
                    list.Add(info)
                End While
            End If
        Catch ex As Exception

        End Try

        Return list
    End Function

    Public Function WCCGetUserLocation_Subcon(ByVal userid As Integer) As List(Of WCCPendingInfo)
        Dim list As New List(Of WCCPendingInfo)

        Command = New SqlCommand("uspWCC_UserUnderSignature_Subcon", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmUserId As New SqlParameter("@usrid", SqlDbType.Int)
        prmUserId.Value = userid
        Command.Parameters.Add(prmUserId)

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If (DataReader.HasRows) Then
                While DataReader.Read()
                    Dim info As New WCCPendingInfo
                    info.SiteName = Convert.ToString(DataReader.Item("SiteName"))
                    info.SiteNo = Convert.ToString(DataReader.Item("SiteNo"))
                    info.PONO = Convert.ToString(DataReader.Item("PONo"))
                    info.POName = Convert.ToString(DataReader.Item("POName"))
                    info.PackageId = Convert.ToString(DataReader.Item("workpackageid"))
                    info.SubmitDate = Convert.ToDateTime(DataReader.Item("SubmitDate"))
                    info.UserLocation = Convert.ToString(DataReader.Item("userLocation"))
                    info.POSubcontractor = Convert.ToString(DataReader.Item("PO_Subcontractor"))
                    info.SubconName = Convert.ToString(DataReader.Item("Scon_Name"))
                    info.ScopeName = Convert.ToString(DataReader.Item("DScope_Name"))
                    list.Add(info)
                End While
            End If
        Catch ex As Exception

        End Try

        Return list
    End Function

    Public Function GetWCCDone(ByVal userid As Integer, ByVal isSubcon As Boolean) As List(Of WCCDoneInfo)
        Dim list As New List(Of WCCDoneInfo)

        If isSubcon = True Then
            Command = New SqlCommand("uspWCC_GetWCCDone_Subcon", Connection)
        Else
            Command = New SqlCommand("uspWCC_GetWCCDone", Connection)
        End If

        Command.CommandType = CommandType.StoredProcedure

        Dim prmUserId As New SqlParameter("@usrid", SqlDbType.Int)
        prmUserId.Value = userid
        Command.Parameters.Add(prmUserId)

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If (DataReader.HasRows) Then
                While (DataReader.Read())
                    Dim info As New WCCDoneInfo
                    info.WCCID = Convert.ToInt32(DataReader.Item("WCC_ID"))
                    info.CertificateNumber = Convert.ToString(DataReader.Item("Certificate_Number"))
                    info.SiteNo = Convert.ToString(DataReader.Item("SiteNo"))
                    info.PONO = Convert.ToString(DataReader.Item("PoNo"))
                    info.POName = Convert.ToString(DataReader.Item("POName"))
                    info.PackageId = Convert.ToString(DataReader.Item("WorkPackageId"))
                    info.SiteName = Convert.ToString(DataReader.Item("SiteName"))
                    info.ScopeName = Convert.ToString(DataReader.Item("dscope_name"))
                    info.ApproverName = Convert.ToString(DataReader.Item("Approver"))
                    info.ApproverDate = Convert.ToDateTime(DataReader.Item("ApprovalDate"))
                    info.SubconName = Convert.ToString(DataReader.Item("scon_name"))
                    info.POSubcontractor = Convert.ToString(DataReader.Item("PO_Subcontractor"))
                    info.IssuanceDate = Convert.ToDateTime(DataReader.Item("Issuance_Date"))
                    info.LMBY = Convert.ToString(DataReader.Item("InitiatorName"))
                    info.SubmitDate = Convert.ToDateTime(DataReader.Item("SubmitDate"))
                    list.Add(info)
                End While
            End If
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try

        Return list
    End Function

    Public Function GetWCCDone_AdvanceSearch(ByVal userid As Integer, ByVal startDate As Nullable(Of DateTime), ByVal endDate As Nullable(Of DateTime), ByVal dscopeid As Integer, ByVal isSubcon As Boolean) As List(Of WCCDoneInfo)
        Dim list As New List(Of WCCDoneInfo)

        If isSubcon = True Then
            Command = New SqlCommand("uspWCC_GetWCCDone_Subcon_AdvanceSearch", Connection)
        Else
            Command = New SqlCommand("uspWCC_GetWCCDone_AdvanceSearch", Connection)
        End If

        Command.CommandType = CommandType.StoredProcedure

        Dim prmUserId As New SqlParameter("@usrid", SqlDbType.Int)
        Dim prmStartTime As New SqlParameter("@starttime", SqlDbType.DateTime)
        Dim prmEndTime As New SqlParameter("@endtime", SqlDbType.DateTime)
        Dim prmDScopeId As New SqlParameter("@dscopeid", SqlDbType.Int)
        prmUserId.Value = userid
        prmStartTime.Value = startDate
        prmEndTime.Value = endDate
        prmDScopeId.Value = dscopeid
        Command.Parameters.Add(prmUserId)
        Command.Parameters.Add(prmStartTime)
        Command.Parameters.Add(prmEndTime)
        Command.Parameters.Add(prmDScopeId)

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If (DataReader.HasRows) Then
                While (DataReader.Read())
                    Dim info As New WCCDoneInfo
                    info.WCCID = Convert.ToInt32(DataReader.Item("WCC_ID"))
                    info.CertificateNumber = Convert.ToString(DataReader.Item("Certificate_Number"))
                    info.SiteNo = Convert.ToString(DataReader.Item("SiteNo"))
                    info.PONO = Convert.ToString(DataReader.Item("PoNo"))
                    info.POName = Convert.ToString(DataReader.Item("POName"))
                    info.PackageId = Convert.ToString(DataReader.Item("WorkPackageId"))
                    info.SiteName = Convert.ToString(DataReader.Item("SiteName"))
                    info.ScopeName = Convert.ToString(DataReader.Item("dscope_name"))
                    info.ApproverName = Convert.ToString(DataReader.Item("Approver"))
                    info.ApproverDate = Convert.ToDateTime(DataReader.Item("ApprovalDate"))
                    info.SubconName = Convert.ToString(DataReader.Item("scon_name"))
                    info.POSubcontractor = Convert.ToString(DataReader.Item("PO_Subcontractor"))
                    info.IssuanceDate = Convert.ToDateTime(DataReader.Item("Issuance_Date"))
                    info.LMBY = Convert.ToString(DataReader.Item("InitiatorName"))
                    info.SubmitDate = Convert.ToDateTime(DataReader.Item("SubmitDate"))
                    list.Add(info)
                End While
            End If
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try

        Return list
    End Function

    Public Function GetWCCTaskPending_Mail(ByVal sno As Int32, ByVal appstatus As String) As List(Of CRFramework.EmailInfo)
        Dim list As New List(Of CRFramework.EmailInfo)
        Command = New SqlCommand("uspWCC_GetEmailTaskPending", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmSNO As New SqlParameter("@sno", SqlDbType.BigInt)
        Dim prmDocStatus As New SqlParameter("@docstatus", SqlDbType.VarChar, 20)

        prmSNO.Value = sno
        prmDocStatus.Value = appstatus

        Command.Parameters.Add(prmSNO)
        Command.Parameters.Add(prmDocStatus)

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read()
                    Dim info As New CRFramework.EmailInfo
                    info.Userid = Integer.Parse(DataReader.Item("usr_id"))
                    info.Username = Convert.ToString(DataReader.Item("name"))
                    info.PhoneNo = Convert.ToString(DataReader.Item("PhoneNo"))
                    info.Email = Convert.ToString(DataReader.Item("Email"))
                    info.StatusType = Convert.ToString(DataReader.Item("StatusType"))
                    info.ExecuteBy = Convert.ToString(DataReader.Item("ExecuteBy"))
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

    Public Function GetSNOInitiator(ByVal wccid As Int32) As Int32
        Dim sno As Int32 = 0
        Command = New SqlCommand("uspWCC_GetSNOInitiator", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmWCCID As New SqlParameter("@wccid", SqlDbType.BigInt)
        Dim prmSNO As New SqlParameter("@sno", SqlDbType.BigInt)

        prmWCCID.Value = wccid
        prmSNO.Direction = ParameterDirection.Output

        Command.Parameters.Add(prmWCCID)
        Command.Parameters.Add(prmSNO)

        Try
            Connection.Open()
            Command.ExecuteNonQuery()
            sno = Convert.ToInt32(prmSNO.Value)
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try

        Return sno
    End Function

#End Region

#Region "WCC Audit Trail"
    Public Function WCCAuditTrail_I(ByVal auditinfo As WCCAuditInfo) As Boolean
        Dim isSucceed As Boolean = True
        Command = New SqlCommand("uspWCC_AuditTrail_I", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmWCCId As New SqlParameter("@wccid", SqlDbType.BigInt)
        Dim prmDocId As New SqlParameter("@docid", SqlDbType.Int)
        Dim prmUserId As New SqlParameter("@userid", SqlDbType.Int)
        Dim prmRoleId As New SqlParameter("@roleid", SqlDbType.Int)
        Dim prmTaskId As New SqlParameter("@taskid", SqlDbType.Int)
        Dim prmRemarks As New SqlParameter("@remarks", SqlDbType.VarChar, 2000)
        Dim prmCategories As New SqlParameter("@Categories", SqlDbType.VarChar, 250)
        Dim prmEventStartTime As New SqlParameter("@eventstarttime", SqlDbType.DateTime)
        Dim prmEventEndTime As New SqlParameter("@eventendtime", SqlDbType.DateTime)

        prmWCCId.Value = auditinfo.WCCID
        prmDocId.Value = auditinfo.DocId
        prmUserId.Value = auditinfo.UserId
        prmRoleId.Value = auditinfo.RoleId
        prmTaskId.Value = auditinfo.Task
        prmRemarks.Value = auditinfo.Remarks
        prmCategories.Value = auditinfo.Categories
        prmEventStartTime.Value = auditinfo.EvenStartTime
        prmEventEndTime.Value = auditinfo.EventEndTime

        Command.Parameters.Add(prmWCCId)
        Command.Parameters.Add(prmDocId)
        Command.Parameters.Add(prmUserId)
        Command.Parameters.Add(prmRoleId)
        Command.Parameters.Add(prmTaskId)
        Command.Parameters.Add(prmRemarks)
        Command.Parameters.Add(prmCategories)
        Command.Parameters.Add(prmEventStartTime)
        Command.Parameters.Add(prmEventEndTime)

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

    Public Function WCCAuditTrail_Get(ByVal wccid As Int32, ByVal docid As Integer, ByVal docparenttype As String) As List(Of WCCAuditInfo)
        Dim list As New List(Of WCCAuditInfo)

        Command = New SqlCommand("uspWCC_GetAuditTrail", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmWCCID As New SqlParameter("@wccid", SqlDbType.BigInt)
        Dim prmDocId As New SqlParameter("@docid", SqlDbType.Int)
        Dim prmDocParentType As New SqlParameter("@docparenttype", SqlDbType.VarChar, 20)

        prmWCCID.Value = wccid
        prmDocId.Value = docid
        prmDocParentType.Value = docparenttype

        Command.Parameters.Add(prmWCCID)
        Command.Parameters.Add(prmDocId)
        Command.Parameters.Add(prmDocParentType)

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If (DataReader.HasRows) Then
                While DataReader.Read()
                    Dim info As New WCCAuditInfo
                    info.WCCAuditId = Convert.ToInt32(DataReader.Item("sno"))
                    info.Task = Integer.Parse(DataReader.Item("Task").ToString())
                    info.UserId = Integer.Parse(DataReader.Item("userid").ToString())
                    info.RoleId = Integer.Parse(DataReader.Item("RoleId").ToString())
                    info.EvenStartTime = Convert.ToDateTime(DataReader.Item("EventStartTime"))
                    info.EventEndTime = Convert.ToDateTime(DataReader.Item("EventEndTime"))
                    info.Remarks = Convert.ToString(DataReader.Item("Remarks"))
                    info.Categories = Convert.ToString(DataReader.Item("Categories"))
                    info.WCCID = Convert.ToInt32(DataReader.Item("WCC_ID"))
                    info.DocId = Integer.Parse(DataReader.Item("Doc_Id").ToString())
                    info.DocName = Convert.ToString(DataReader.Item("DocName"))
                    info.LMBY = Convert.ToString(DataReader.Item("name"))
                    info.TaskDesc = Convert.ToString(DataReader.Item("TaskDesc"))
                    info.TaskEvent = Convert.ToString(DataReader.Item("TaskEvent"))
                    info.SignTitle = Convert.ToString(DataReader.Item("SignTitle"))
                    list.Add(info)
                End While
            End If
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try
        Return list
    End Function

    Public Function WCCAuditTrail_BasePackageId(ByVal docid As Integer, ByVal packageid As String) As List(Of WCCAuditInfo)
        Dim list As New List(Of WCCAuditInfo)

        Command = New SqlCommand("uspAuditTrail_PackageIDBase", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmDocId As New SqlParameter("@docid", SqlDbType.Int)
        Dim prmPackageId As New SqlParameter("@workpkgid", SqlDbType.VarChar, 50)

        prmDocId.Value = docid
        prmPackageId.Value = packageid

        Command.Parameters.Add(prmDocId)
        Command.Parameters.Add(prmPackageId)

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If (DataReader.HasRows) Then
                While DataReader.Read()
                    Dim info As New WCCAuditInfo
                    info.WCCAuditId = Convert.ToInt32(DataReader.Item("sno"))
                    'info.Task = Integer.Parse(DataReader.Item("Task").ToString())
                    info.UserId = Integer.Parse(DataReader.Item("userid").ToString())
                    info.EvenStartTime = Convert.ToDateTime(DataReader.Item("EventStartTime"))
                    info.EventEndTime = Convert.ToDateTime(DataReader.Item("EventEndTime"))
                    info.Remarks = Convert.ToString(DataReader.Item("Remarks"))
                    info.Categories = Convert.ToString(DataReader.Item("Categories"))
                    'info.WCCID = Convert.ToInt32(DataReader.Item("WCC_ID"))
                    info.DocId = Integer.Parse(DataReader.Item("DocId").ToString())
                    info.DocName = Convert.ToString(DataReader.Item("DocName"))
                    info.LMBY = Convert.ToString(DataReader.Item("user"))
                    info.TaskDesc = Convert.ToString(DataReader.Item("TaskDesc"))
                    info.TaskEvent = Convert.ToString(DataReader.Item("Task"))
                    info.SignTitle = Convert.ToString(DataReader.Item("SignTitle"))
                    list.Add(info)
                End While
            End If
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try
        Return list
    End Function

#End Region

#Region "WCC Site Folder"
    Public Function SiteFolder_I(ByVal info As WCCSitedocInfo) As Boolean
        Dim isSucceed As Boolean = True
        Command = New SqlCommand("uspWCC_SiteFolder_I", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmWCCID As New SqlParameter("@wccid", SqlDbType.BigInt)
        Dim prmDocId As New SqlParameter("@docid", SqlDbType.Int)
        Dim prmDocPath As New SqlParameter("@DocPath", SqlDbType.VarChar, 2000)
        Dim prmOrgDocPath As New SqlParameter("@OrgDocPath", SqlDbType.VarChar, 2000)
        Dim prmLMBY As New SqlParameter("@lmby", SqlDbType.VarChar, 50)
        Dim prmParentDocType As New SqlParameter("@ParentDocType", SqlDbType.VarChar, 50)
        Dim prmIsUploaded As New SqlParameter("@isUploaded", SqlDbType.Bit)
        Dim prmRStatus As New SqlParameter("@rstatus", SqlDbType.Int)

        prmWCCID.Value = info.WCCID
        prmDocId.Value = info.DocId
        prmDocPath.Value = info.DocPath
        prmOrgDocPath.Value = info.OrgDocPath
        prmLMBY.Value = info.LMBY
        prmParentDocType.Value = info.ParentDocType
        prmIsUploaded.Value = info.IsUploaded
        prmRStatus.Value = info.Rstatus

        Command.Parameters.Add(prmWCCID)
        Command.Parameters.Add(prmDocId)
        Command.Parameters.Add(prmDocPath)
        Command.Parameters.Add(prmOrgDocPath)
        Command.Parameters.Add(prmLMBY)
        Command.Parameters.Add(prmParentDocType)
        Command.Parameters.Add(prmIsUploaded)
        Command.Parameters.Add(prmRStatus)

        Try
            Connection.Open()
            Command.ExecuteNonQuery()
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try

        Return isSucceed
    End Function

    Public Function GetSiteDocuments(ByVal wccid As Int32, ByVal wccdocid As Integer) As List(Of WCCSitedocInfo)
        Dim list As New List(Of WCCSitedocInfo)
        Command = New SqlCommand("uspWCC_GetSiteDoc", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmWCCID As New SqlParameter("@wccid", SqlDbType.BigInt)
        Dim prmWCCDocId As New SqlParameter("@wccdocid", SqlDbType.Int)
        prmWCCID.Value = wccid
        prmWCCDocId.Value = wccdocid
        Command.Parameters.Add(prmWCCID)
        Command.Parameters.Add(prmWCCDocId)

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If (DataReader.HasRows) Then
                While DataReader.Read()
                    Dim info As New WCCSitedocInfo
                    info.WCCID = Convert.ToInt32(DataReader.Item("WCC_ID"))
                    info.WCCSiteDocId = Convert.ToInt32(DataReader.Item("WCCSW_Id"))
                    info.DocId = Integer.Parse(DataReader.Item("WCCDoc_Id").ToString())
                    info.DocPath = Convert.ToString(DataReader.Item("DocPath"))
                    info.DocName = Convert.ToString(DataReader.Item("DocName"))
                    info.OrgDocPath = Convert.ToString(DataReader.Item("OrgDocPath"))
                    info.LMBY = Convert.ToString(DataReader.Item("LMBY"))
                    info.LMDT = Convert.ToDateTime(DataReader.Item("LMDT"))
                    info.IsUploaded = Convert.ToBoolean(DataReader.Item("IsUploaded"))
                    info.ParentDocType = Convert.ToString(DataReader.Item("Parent_Doc_Type"))
                    info.Rstatus = Integer.Parse(DataReader.Item("rstatus").ToString())
                    list.Add(info)
                End While
            End If
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try

        Return list
    End Function

    Public Function GetSiteDocumentsBaseWPIDSubcon(ByVal packageid As String, ByVal userid As Integer) As List(Of WCCSitedocInfo)
        Dim list As New List(Of WCCSitedocInfo)
        Command = New SqlCommand("uspWCC_GetSiteDocBaseWPIDSubcon", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmPackageId As New SqlParameter("@packageid", SqlDbType.VarChar, 50)
        Dim prmUserId As New SqlParameter("@usrid", SqlDbType.Int)


        prmPackageId.Value = packageid
        prmUserId.Value = userid

        Command.Parameters.Add(prmPackageId)
        Command.Parameters.Add(prmUserId)

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If (DataReader.HasRows) Then
                While DataReader.Read()
                    Dim info As New WCCSitedocInfo
                    info.WCCID = Convert.ToInt32(DataReader.Item("WCC_ID"))
                    info.WCCSiteDocId = Convert.ToInt32(DataReader.Item("WCCSW_Id"))
                    info.DocId = Integer.Parse(DataReader.Item("WCCDoc_Id").ToString())
                    info.DocPath = Convert.ToString(DataReader.Item("DocPath"))
                    info.DocName = Convert.ToString(DataReader.Item("DocName"))
                    info.OrgDocPath = Convert.ToString(DataReader.Item("OrgDocPath"))
                    info.LMBY = Convert.ToString(DataReader.Item("LMBY"))
                    info.LMDT = Convert.ToDateTime(DataReader.Item("LMDT"))
                    info.IsUploaded = Convert.ToBoolean(DataReader.Item("IsUploaded"))
                    info.ParentDocType = Convert.ToString(DataReader.Item("Parent_Doc_Type"))
                    info.Rstatus = Integer.Parse(DataReader.Item("rstatus").ToString())
                    info.SiteNo = Convert.ToString(DataReader.Item("SiteNo"))
                    info.SiteName = Convert.ToString(DataReader.Item("SiteName"))
                    info.PoNo = Convert.ToString(DataReader.Item("PoNo"))
                    info.POSubcon = Convert.ToString(DataReader.Item("PO_Subcontractor"))
                    info.ScopeName = Convert.ToString(DataReader.Item("DScope_Name"))
                    info.PackageId = Convert.ToString(DataReader.Item("Workpackageid"))
                    list.Add(info)
                End While
            End If
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try

        Return list
    End Function

    Public Function GetSiteDocumentsBasePOSubcon(ByVal posubcon As String, ByVal userid As Integer) As List(Of WCCSitedocInfo)
        Dim list As New List(Of WCCSitedocInfo)
        Command = New SqlCommand("uspWCC_GetSiteDocBasePOSubcon", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmPOSubcon As New SqlParameter("@posubcon", SqlDbType.VarChar, 50)
        Dim prmUserId As New SqlParameter("@usrid", SqlDbType.Int)

        prmPOSubcon.Value = posubcon
        prmUserId.Value = userid

        Command.Parameters.Add(prmPOSubcon)
        Command.Parameters.Add(prmUserId)

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If (DataReader.HasRows) Then
                While DataReader.Read()
                    Dim info As New WCCSitedocInfo
                    info.WCCID = Convert.ToInt32(DataReader.Item("WCC_ID"))
                    info.WCCSiteDocId = Convert.ToInt32(DataReader.Item("WCCSW_Id"))
                    info.DocId = Integer.Parse(DataReader.Item("WCCDoc_Id").ToString())
                    info.DocPath = Convert.ToString(DataReader.Item("DocPath"))
                    info.DocName = Convert.ToString(DataReader.Item("DocName"))
                    info.OrgDocPath = Convert.ToString(DataReader.Item("OrgDocPath"))
                    info.LMBY = Convert.ToString(DataReader.Item("LMBY"))
                    info.LMDT = Convert.ToDateTime(DataReader.Item("LMDT"))
                    info.IsUploaded = Convert.ToBoolean(DataReader.Item("IsUploaded"))
                    info.ParentDocType = Convert.ToString(DataReader.Item("Parent_Doc_Type"))
                    info.Rstatus = Integer.Parse(DataReader.Item("rstatus").ToString())
                    info.SiteNo = Convert.ToString(DataReader.Item("SiteNo"))
                    info.SiteName = Convert.ToString(DataReader.Item("SiteName"))
                    info.PoNo = Convert.ToString(DataReader.Item("PoNo"))
                    info.POSubcon = Convert.ToString(DataReader.Item("PO_Subcontractor"))
                    info.ScopeName = Convert.ToString(DataReader.Item("DScope_Name"))
                    info.PackageId = Convert.ToString(DataReader.Item("Workpackageid"))
                    list.Add(info)
                End While
            End If
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try

        Return list
    End Function

    Public Function GetSiteDocBaseSWID(ByVal swid As Int32) As WCCSitedocInfo
        Dim info As New WCCSitedocInfo

        Command = New SqlCommand("uspWCC_GetSitedocBaseSWID", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmSWID As New SqlParameter("@swid", SqlDbType.BigInt)
        prmSWID.Value = swid
        Command.Parameters.Add(prmSWID)

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If (DataReader.HasRows) Then
                While DataReader.Read()
                    info.WCCSiteDocId = Convert.ToInt32(DataReader.Item("WCCSW_Id"))
                    info.DocId = Integer.Parse(DataReader.Item("wccdoc_id").ToString())
                    info.DocName = Convert.ToString(DataReader.Item("docname"))
                    info.DocPath = Convert.ToString(DataReader.Item("docpath"))
                    info.OrgDocPath = Convert.ToString(DataReader.Item("orgdocpath"))
                    info.IsUploaded = Convert.ToBoolean(DataReader.Item("IsUploaded"))
                    info.ParentDocType = Convert.ToString(DataReader.Item("Parent_Doc_Type"))
                End While
            End If
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try

        Return info
    End Function

    Public Function UploadSiteDocument(ByVal docpath As String, ByVal orgdocpath As String, ByVal lmby As String, ByVal sitedocid As Int32) As Boolean
        Dim isSucceed As Boolean = True
        Command = New SqlCommand("uspWCC_UploadSiteDocument", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmSiteDocId As New SqlParameter("@sitedocid", SqlDbType.BigInt)
        Dim prmDocPath As New SqlParameter("@docpath", SqlDbType.VarChar, 2000)
        Dim prmOrgDocPath As New SqlParameter("@orgdocpath", SqlDbType.VarChar, 2000)
        Dim prmLMBY As New SqlParameter("@lmby", SqlDbType.VarChar, 10)

        prmSiteDocId.Value = sitedocid
        prmDocPath.Value = docpath
        prmOrgDocPath.Value = orgdocpath
        prmLMBY.Value = lmby

        Command.Parameters.Add(prmSiteDocId)
        Command.Parameters.Add(prmDocPath)
        Command.Parameters.Add(prmOrgDocPath)
        Command.Parameters.Add(prmLMBY)
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

    Public Function DeleteSiteDocument(ByVal sitedocid As Int32, ByVal lmby As Integer) As Boolean
        Dim isSucceed As Boolean = True
        Command = New SqlCommand("uspWCC_DeleteSiteDocument", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmSiteDocId As New SqlParameter("@sitedocid", SqlDbType.BigInt)
        Dim prmLMBY As New SqlParameter("@lmby", SqlDbType.Int)

        prmSiteDocId.Value = sitedocid
        prmLMBY.Value = lmby

        Command.Parameters.Add(prmSiteDocId)
        Command.Parameters.Add(prmLMBY)

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

    Public Sub DeleteSiteFolderDocument(ByVal sitedocid As Int32)
        Dim isSucceed As Boolean = True
        Dim strErr As String = String.Empty
        Command = New SqlCommand("delete wcc_sitedoc where wccsw_id =" & sitedocid.ToString(), Connection)
        Command.CommandType = CommandType.Text

        Try
            Connection.Open()
            Command.ExecuteNonQuery()
        Catch ex As Exception
            isSucceed = False
            strErr = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try
        If isSucceed = False Then
            ErrorLogInsert(0, "DeleteSiteDocument", strErr, "CommandTypeText")
        End If
    End Sub
#End Region

#Region "WCC Approver or Reviewer"

    Public Function GetApprovalByWorkflow(ByVal packageid As String, ByVal wfid As Integer) As List(Of UserProfile)
        Dim list As New List(Of UserProfile)

        Command = New SqlCommand("uspGeneral_GetApproverByGrouping", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmPackageId As New SqlParameter("@packageid", SqlDbType.VarChar, 50)
        Dim prmWFID As New SqlParameter("@wfid", SqlDbType.Int)

        prmPackageId.Value = packageid
        prmWFID.Value = wfid

        Command.Parameters.Add(prmPackageId)
        Command.Parameters.Add(prmWFID)

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If (DataReader.HasRows) Then
                While (DataReader.Read())
                    Dim info As New UserProfile
                    info.UserId = Integer.Parse(DataReader.Item("usr_id").ToString())
                    info.Username = Convert.ToString(DataReader.Item("name"))
                    info.RoleId = Integer.Parse(DataReader.Item("usrRole").ToString())
                    info.SignTitle = Convert.ToString(DataReader.Item("SignTitle"))
                    list.Add(info)
                End While

            End If
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try

        Return list
    End Function

    Public Function IsValidWCCWorkflow(ByVal wccid As Int32) As Boolean
        Dim isValid As Boolean = True

        Command = New SqlCommand("uspWCC_WorkflowChecking", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmWCCID As New SqlParameter("@wccid", SqlDbType.BigInt)
        Dim prmIsValid As New SqlParameter("@isValid", SqlDbType.Bit)

        prmWCCID.Value = wccid
        prmIsValid.Direction = ParameterDirection.Output

        Command.Parameters.Add(prmWCCID)
        Command.Parameters.Add(prmIsValid)

        Try
            Connection.Open()
            Command.ExecuteNonQuery()
            isValid = Convert.ToBoolean(prmIsValid.Value)
        Catch ex As Exception
            isValid = False
        Finally
            Connection.Close()
        End Try

        Return isValid
    End Function

    Public Sub UpdateWCCWorkflow(ByVal wccid As Int32, ByVal wfid As Integer)
        Command = New SqlCommand("uspWCC_UpdateWokflow", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmWCCID As New SqlParameter("@wccid", SqlDbType.BigInt)
        Dim prmWFID As New SqlParameter("@WFID", SqlDbType.Int)

        prmWCCID.Value = wccid
        prmWFID.Value = wfid

        Command.Parameters.Add(prmWCCID)
        Command.Parameters.Add(prmWFID)

        Try
            Connection.Open()
            Command.ExecuteNonQuery()
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try

    End Sub
#End Region

#Region "Rejection Transaction Historical Log"
    Public Function WCCRejectionHistoricalLog_I(ByVal info As WCCHistoricalRejectionInfo) As Boolean
        Dim isSucceed As Boolean = True
        Command = New SqlCommand("uspWCC_Historical_Rejection_Log_I", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmDocId As New SqlParameter("@docid", SqlDbType.Int)
        Dim prmWCCId As New SqlParameter("@wccid", SqlDbType.BigInt)
        Dim prmDocType As New SqlParameter("@DocType", SqlDbType.VarChar, 20)
        Dim prmRemarks As New SqlParameter("@remarks", SqlDbType.VarChar, 2000)
        Dim prmCategories As New SqlParameter("@categories", SqlDbType.VarChar, 200)
        Dim prmRejectionDate As New SqlParameter("@rejectiondate", SqlDbType.DateTime)
        Dim prmRejectionUser As New SqlParameter("@rejectionuser", SqlDbType.Int)
        Dim prmInitiatorUser As New SqlParameter("@initiatoruser", SqlDbType.Int)

        prmDocId.Value = info.DocId
        prmWCCId.Value = info.WCCID
        prmDocType.Value = info.DocType
        prmRemarks.Value = info.Remarks
        prmCategories.Value = info.Categories
        prmRejectionDate.Value = info.RejectionDate
        prmInitiatorUser.Value = info.InitiatorUser
        prmRejectionUser.Value = info.RejectionUser

        Command.Parameters.Add(prmDocId)
        Command.Parameters.Add(prmWCCId)
        Command.Parameters.Add(prmDocType)
        Command.Parameters.Add(prmRemarks)
        Command.Parameters.Add(prmCategories)
        Command.Parameters.Add(prmRejectionDate)
        Command.Parameters.Add(prmInitiatorUser)
        Command.Parameters.Add(prmRejectionUser)

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

    Public Function WCCRejectionHistoricalLog_Reupload(ByVal info As WCCHistoricalRejectionInfo) As Boolean
        Dim isSucceed As Boolean = True

        Command = New SqlCommand("uspWCC_Historical_Rejection_Log_reupload", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmDocId As New SqlParameter("@docid", SqlDbType.Int)
        Dim prmWCCId As New SqlParameter("@wccid", SqlDbType.BigInt)
        Dim prmReuploadUser As New SqlParameter("@reuploaduser", SqlDbType.Int)

        prmDocId.Value = info.DocId
        prmWCCId.Value = info.WCCID
        prmReuploadUser.Value = info.UploadUser

        Command.Parameters.Add(prmDocId)
        Command.Parameters.Add(prmWCCId)
        Command.Parameters.Add(prmReuploadUser)

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
#End Region

#Region "WCC Numbering"
    Public Function GetSeqNumbering(ByVal years As Integer, ByVal packageid As String) As String
        Dim seqNumbering As String = String.Empty
        Command = New SqlCommand("uspWCC_GetWCCNo", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmYears As New SqlParameter("@year", SqlDbType.Int)
        Dim prmPackageId As New SqlParameter("@PackageId", SqlDbType.VarChar, 50)
        Dim prmSeqNo As New SqlParameter("@seqNoOutput", SqlDbType.Int)

        prmYears.Value = years
        prmPackageId.Value = packageid
        prmSeqNo.Direction = ParameterDirection.Output

        Command.Parameters.Add(prmYears)
        Command.Parameters.Add(prmPackageId)
        Command.Parameters.Add(prmSeqNo)

        Try
            Connection.Open()
            Command.ExecuteNonQuery()
            Dim seqNo As Integer = Integer.Parse(prmSeqNo.Value.ToString())
            If seqNo > 0 And seqNo < 10 Then
                seqNumbering = "0000" + seqNo
            ElseIf seqNo > 9 And seqNo < 100 Then
                seqNumbering = "000" + seqNo
            ElseIf seqNo > 99 And seqNo < 1000 Then
                seqNumbering = "00" + seqNo
            ElseIf seqNo > 999 And seqNo < 10000 Then
                seqNumbering = "0" + seqNo
            Else
                seqNumbering = seqNo
            End If
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try
        Return seqNumbering
    End Function

    Public Sub UpdateWCCCertificateNo(ByVal wccid As Int32, ByVal certificateNo As String)
        Command = New SqlCommand("uspWCC_UpdateWCCNumbering", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmWCCID As New SqlParameter("@wccid", SqlDbType.BigInt)
        Dim prmCertificateNo As New SqlParameter("@CertificateNo", SqlDbType.VarChar, 200)

        prmWCCID.Value = wccid
        prmCertificateNo.Value = certificateNo

        Command.Parameters.Add(prmWCCID)
        Command.Parameters.Add(prmCertificateNo)

        Try
            Connection.Open()
            Command.ExecuteNonQuery()
        Catch ex As Exception

        Finally
            Connection.Close()
        End Try


    End Sub

#End Region

#Region "WCC Deletion"
    Public Sub WCCDeletionFlag(ByVal wccid As Int32, ByVal remarks As String, ByVal isDeleted As Boolean, ByVal lmby As Integer)
        Dim isSucceed As Boolean = True
        Dim strError As String = String.Empty

        Command = New SqlCommand("uspWCC_Deleted", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmWCCId As New SqlParameter("@wccid", SqlDbType.BigInt)
        Dim prmRemarks As New SqlParameter("@remarks", SqlDbType.VarChar, 2000)
        Dim prmLMBY As New SqlParameter("@lmby", SqlDbType.Int)
        Dim prmIsDeleted As New SqlParameter("@isDeleted", SqlDbType.Bit)

        prmWCCId.Value = wccid
        prmRemarks.Value = remarks
        prmLMBY.Value = lmby
        prmIsDeleted.Value = isDeleted

        Command.Parameters.Add(prmWCCId)
        Command.Parameters.Add(prmRemarks)
        Command.Parameters.Add(prmLMBY)
        Command.Parameters.Add(prmIsDeleted)

        Try
            Connection.Open()
            Command.ExecuteNonQuery()
        Catch ex As Exception
            isSucceed = False
            strError = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try
        If isSucceed = False Then
            ErrorLogInsert(10, "WCCDeletionFlag", strError, "uspWCC_Deleted")
        End If
    End Sub

    Public Function GetWCCHistoricalDeletion(ByVal packageid As String, ByVal posubcontractor As String) As List(Of WCCDeletionInfo)
        Dim list As New List(Of WCCDeletionInfo)
        Command = New SqlCommand("uspWCC_GetHistoricalDeletion", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmPackageId As New SqlParameter("@packageid", SqlDbType.VarChar, 50)
        Dim prmPOSubcontractor As New SqlParameter("@posubcontractor", SqlDbType.VarChar, 100)

        prmPackageId.Value = packageid
        prmPOSubcontractor.Value = posubcontractor

        Command.Parameters.Add(prmPackageId)
        Command.Parameters.Add(prmPOSubcontractor)

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read
                    Dim info As New WCCDeletionInfo
                    info.WCCDeletedId = Convert.ToInt32(DataReader.Item("WCCDel_Id"))
                    info.WCCDeletedDate = Convert.ToDateTime(DataReader.Item("lmdt"))
                    info.UserDeletion = Convert.ToString(DataReader.Item("userdeletion"))
                    info.UserDeletionId = Integer.Parse(DataReader.Item("lmby"))
                    info.SiteNo = Convert.ToString(DataReader.Item("SiteNo"))
                    info.SiteName = Convert.ToString(DataReader.Item("SiteName"))
                    info.PONO = Convert.ToString(DataReader.Item("PONO"))
                    info.PackageId = Convert.ToString(DataReader.Item("WorkpackageId"))
                    info.WCCDeletionRemarks = Convert.ToString(DataReader.Item("remarks"))
                    info.CertificateNumber = Convert.ToString(DataReader.Item("Certificate_Number"))
                    If Not String.IsNullOrEmpty(Convert.ToString(DataReader.Item("Issuance_date"))) Then
                        info.IssuanceDate = Convert.ToDateTime(DataReader.Item("Issuance_date"))
                    End If
                    info.WCCID = Convert.ToInt32(DataReader.Item("WCC_ID"))
                    info.SubconName = Convert.ToString(DataReader.Item("SCON_Name"))
                    info.Scope = Convert.ToString(DataReader.Item("DScope_Name"))
                    info.CDT = Convert.ToDateTime(DataReader.Item("CDT"))
                    info.POSubcontractor = Convert.ToString(DataReader.Item("PO_Subcontractor"))
                    info.LMDT = Convert.ToDateTime(DataReader.Item("UploadDate"))
                    list.Add(info)
                End While
            End If
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try

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

#Region "Update WCC Attribute"
    Public Sub UpdatePOSubcon(ByVal posubcon As String, ByVal wccid As Int32)
        Command = New SqlCommand("uspWCC_POSubcon_Update", Connection)
        Command.CommandType = CommandType.StoredProcedure
        Dim prmWCCId As New SqlParameter("@wccid", SqlDbType.BigInt)
        Dim prmPOSubcon As New SqlParameter("@posubcon", SqlDbType.VarChar, 50)
        prmWCCId.Value = wccid
        prmPOSubcon.Value = posubcon
        Command.Parameters.Add(prmWCCId)
        Command.Parameters.Add(prmPOSubcon)

        Try
            Connection.Open()
            Command.ExecuteNonQuery()
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try

    End Sub

    Public Sub UpdateWCTRDate(ByVal wctrdate As DateTime, ByVal wccid As Int32)
        Command = New SqlCommand("uspWCC_WCTRDate_Update", Connection)
        Command.CommandType = CommandType.StoredProcedure
        Dim prmWCCId As New SqlParameter("@wccid", SqlDbType.BigInt)
        Dim prmWCTRDate As New SqlParameter("@wctrdate", SqlDbType.DateTime)
        prmWCCId.Value = wccid
        prmWCTRDate.Value = wctrdate
        Command.Parameters.Add(prmWCCId)
        Command.Parameters.Add(prmWCTRDate)

        Try
            Connection.Open()
            Command.ExecuteNonQuery()
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try

    End Sub

    Public Sub UpdateAcceptanceDate(ByVal acceptancedate As DateTime, ByVal wccid As Int32)
        Command = New SqlCommand("uspWCC_AcceptanceDate_Update", Connection)
        Command.CommandType = CommandType.StoredProcedure
        Dim prmWCCId As New SqlParameter("@wccid", SqlDbType.BigInt)
        Dim prmBAUTBASTDate As New SqlParameter("@bautbastdate", SqlDbType.DateTime)
        prmWCCId.Value = wccid
        prmBAUTBASTDate.Value = acceptancedate
        Command.Parameters.Add(prmWCCId)
        Command.Parameters.Add(prmBAUTBASTDate)

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
