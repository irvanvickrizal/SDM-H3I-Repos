Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections.Generic

Public Class CODInjectionController
    Inherits BaseController

    Public Function Injection_IU(ByVal info As CODInjectionTypeInfo) As String
        Dim isSucceed As String = "Succeed"

        Command = New SqlCommand("uspCOD_Injection_IU", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmInjectionID As New SqlParameter("@injectionid", SqlDbType.Int)
        Dim prmInjectionName As New SqlParameter("@injectionname", SqlDbType.VarChar, 100)
        Dim prmInjectionDesc As New SqlParameter("@injectionDesc", SqlDbType.VarChar, 500)
        Dim prmLMBY As New SqlParameter("@lmby", SqlDbType.Int)
        Dim prmParentDocID As New SqlParameter("@parentdocid", SqlDbType.Int)
        Dim prmIsDeleted As New SqlParameter("@isDeleted", SqlDbType.Bit)

        prmInjectionID.Value = info.InjectionId
        prmInjectionName.Value = info.InjectionName
        prmInjectionDesc.Value = info.InjectionDesc
        prmLMBY.Value = info.LMBY
        prmParentDocID.Value = info.ParentDocId
        prmIsDeleted.Value = info.IsDeleted

        Command.Parameters.Add(prmInjectionID)
        Command.Parameters.Add(prmInjectionName)
        Command.Parameters.Add(prmInjectionDesc)
        Command.Parameters.Add(prmLMBY)
        Command.Parameters.Add(prmParentDocID)
        Command.Parameters.Add(prmIsDeleted)

        Try
            Connection.Open()
            Command.ExecuteNonQuery()
        Catch ex As Exception
            isSucceed = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try

        Return isSucceed
    End Function

    Public Function GetInjections(ByVal isDeleted As Boolean) As List(Of CODInjectionTypeInfo)
        Dim list As New List(Of CODInjectionTypeInfo)
        Dim strQuery As String = "select inj.*,usr.name from CODInjectionType inj inner join ebastusers_1 usr on usr.usr_id = inj.lmby where isDeleted='" & isDeleted & "'"
        Command = New SqlCommand(strQuery, Connection)
        Command.CommandType = CommandType.Text

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read
                    Dim info As New CODInjectionTypeInfo
                    info.InjectionName = Convert.ToString(DataReader.Item("Injection_Name"))
                    info.InjectionDesc = Convert.ToString(DataReader.Item("Injection_Desc"))
                    info.InjectionId = Integer.Parse(DataReader.Item("Injection_Id").ToString())
                    info.IsDeleted = Convert.ToBoolean(DataReader.Item("IsDeleted"))
                    info.LMBY = Integer.Parse(DataReader.Item("LMBY").ToString())
                    info.LMDT = Convert.ToDateTime(DataReader.Item("LMDT").ToString())
                    info.CDT = Convert.ToDateTime(DataReader.Item("LMDT").ToString())
                    info.ParentDocId = Integer.Parse(DataReader.Item("ParentDoc_Id").ToString())
                    info.DdlInjectionId = info.InjectionId & "-" & info.ParentDocId
                    If info.ParentDocId = 1031 Then
                        info.ParentDocName = "BAUT"
                    Else
                        info.ParentDocName = "BAST"
                    End If
                    info.ModifiedUser = Convert.ToString(DataReader.Item("name"))
                    list.Add(info)
                End While
            End If
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try
        Return list
    End Function

    Public Function GetInjectionTypeIDBaseParentID(ByVal parentid As Integer) As Integer
        Dim injectionid As Integer = 0
        Command = New SqlCommand("uspCOD_GetInjectionType_BaseParentID", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmParentID As New SqlParameter("@parentid", SqlDbType.Int)
        Dim prmInjectionID As New SqlParameter("@injectionid", SqlDbType.Int)

        prmParentID.Value = parentid
        prmInjectionID.Direction = ParameterDirection.Output

        Command.Parameters.Add(prmParentID)
        Command.Parameters.Add(prmInjectionID)

        Try
            Connection.Open()
            Command.ExecuteNonQuery()
            injectionid = Integer.Parse(prmInjectionID.Value.ToString())
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try
        Return injectionid
    End Function

    Public Function Injection_D(ByVal injectionid As Integer) As String
        Dim message As String = "succeed"
        Command = New SqlCommand("update CODInjectionType set isDeleted=1 where injection_id=" & injectionid, Connection)
        Command.CommandType = CommandType.Text

        Try
            Connection.Open()
            Command.ExecuteNonQuery()
        Catch ex As Exception
            message = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try
        Return message
    End Function

    Public Function InjectionDoc_I(ByVal info As CODInjectionDocInfo) As String
        Dim result As String = "succeed"

        Command = New SqlCommand("uspCOD_DocInjection_I", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmPackageId As New SqlParameter("@packageid", SqlDbType.VarChar, 50)
        Dim prmDocId As New SqlParameter("@docid", SqlDbType.Int)
        Dim prmInjectionType As New SqlParameter("@injectiontype", SqlDbType.Int)
        Dim prmLMBY As New SqlParameter("@lmby", SqlDbType.Int)
        Dim prmRemarks As New SqlParameter("@remarks", SqlDbType.VarChar, 400)

        prmPackageId.Value = info.PackageId
        prmDocId.Value = info.Docid
        prmInjectionType.Value = info.InjectionId
        prmLMBY.Value = info.LMBY
        prmRemarks.Value = info.Remarks

        Command.Parameters.Add(prmPackageId)
        Command.Parameters.Add(prmDocId)
        Command.Parameters.Add(prmInjectionType)
        Command.Parameters.Add(prmLMBY)
        Command.Parameters.Add(prmRemarks)

        Try
            Connection.Open()
            Command.ExecuteNonQuery()
        Catch ex As Exception
            result = "Error :" & ex.Message.ToString()
        Finally
            Connection.Close()
        End Try

        Return result

    End Function

    Public Function InjectionDoc_D(ByVal docid As Integer, ByVal packageid As String) As Boolean
        Dim isSucceed As Boolean = True
        Command = New SqlCommand("delete DocNotMandatory where doc_id =" & docid & " and package_id ='" & packageid & "'", Connection)
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

    Public Function GetInjectionDocs() As List(Of CODInjectionDocInfo)
        Dim list As New List(Of CODInjectionDocInfo)
        Command = New SqlCommand("uspCOD_GetInjectionDocs", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read()
                    Dim info As New CODInjectionDocInfo
                    info.InjectionId = Integer.Parse(DataReader.Item("Injection_Id").ToString())
                    info.InjectionName = Convert.ToString(DataReader.Item("Injection_Name"))
                    info.InjectionDesc = Convert.ToString(DataReader.Item("Injection_Desc"))
                    info.Docid = Integer.Parse(DataReader.Item("doc_id").ToString())
                    info.Docname = Convert.ToString(DataReader.Item("docname"))
                    info.CDT = Convert.ToDateTime(DataReader.Item("CDT"))
                    info.ModifiedUser = Convert.ToString(DataReader.Item("modifieduser"))
                    info.PackageId = Convert.ToString(DataReader.Item("Package_Id"))
                    info.Remarks = Convert.ToString(DataReader.Item("remarks"))
                    list.Add(info)
                End While
            End If
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try

        Return list
    End Function

    Public Function IsAvalaibleDocInjection(ByVal docid As Integer, ByVal packageid As String) As Boolean
        Dim isAvailable As Boolean = False
        Command = New SqlCommand("select count(*) as 'rowCount' from DocNotMandatory where package_id ='" & packageid & "' and doc_id=" & docid, Connection)
        Command.CommandType = CommandType.Text

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                Dim rowCount As Integer = 0
                While DataReader.Read()
                    rowCount = Integer.Parse(DataReader.Item("rowCount").ToString())
                End While
                If rowCount > 0 Then
                    isAvailable = True
                End If
            End If
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try

        Return isAvailable
    End Function

    Public Function IsAvalaiblePackageId(ByVal packageid As String) As Boolean
        Dim isAvailable As Boolean = False
        Command = New SqlCommand("select count(*) as 'rowCount' from podetails where workpkgid ='" & packageid & "'", Connection)
        Command.CommandType = CommandType.Text

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                Dim rowCount As Integer = 0
                While DataReader.Read()
                    rowCount = Integer.Parse(DataReader.Item("rowCount").ToString())
                End While
                If rowCount > 0 Then
                    isAvailable = True
                End If
            End If
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try

        Return isAvailable
    End Function

    Public Function GetDocumentsByParentId(ByVal parentid As Integer) As List(Of DocInfo)
        Dim list As New List(Of DocInfo)

        Command = New SqlCommand("select * from CODDOC where rstatus = 2 and parent_id=" & parentid, Connection)
        Command.CommandType = CommandType.Text

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read()
                    Dim info As New DocInfo
                    info.DocId = Integer.Parse(DataReader.Item("doc_id").ToString())
                    info.DocName = Convert.ToString(DataReader.Item("Docname"))
                    info.DocType = Convert.ToString(DataReader.Item("DocType"))
                    list.Add(info)
                End While
            End If
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try

        Return list
    End Function

    Public Function GetDocDetail(ByVal docid As Integer) As DocInfo
        Dim info As New DocInfo
        Command = New SqlCommand("select top 1 docname,parent_id from coddoc where doc_id =" & docid, Connection)
        Command.CommandType = CommandType.Text
        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read()
                    info.DocId = docid
                    info.DocName = Convert.ToString(DataReader.Item("docname"))
                    info.ParentId = Integer.Parse(DataReader.Item("parent_id").ToString())
                End While
            Else
                info = Nothing
            End If
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try
        Return info
    End Function

End Class
