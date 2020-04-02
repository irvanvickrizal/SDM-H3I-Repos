Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Collections.Generic


Public Class DocController
    Inherits BaseController

    Public Function GetParentDocuments() As List(Of DocInfo)
        Dim list As New List(Of DocInfo)

        Command = New SqlCommand("uspDOC_GetParentDoc", Connection)
        Command.CommandType = Data.CommandType.StoredProcedure

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read()
                    Dim info As New DocInfo
                    info.DocId = Integer.Parse(DataReader.Item("Doc_Id").ToString())
                    info.DocName = Convert.ToString(DataReader.Item("DocName"))
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

    Public Function ChangeDocument_I(ByVal info As DocChangeInfo) As Boolean
        Dim isSucceed As Boolean = True

        Command = New SqlCommand("uspCOD_ChangeDocument_I", Connection)
        Command.CommandType = Data.CommandType.StoredProcedure

        Dim prmDocId As New SqlParameter("@docid", Data.SqlDbType.Int)
        Dim prmDocChangeId As New SqlParameter("@docchangeid", Data.SqlDbType.Int)
        Dim prmLMBY As New SqlParameter("@lmby", Data.SqlDbType.VarChar, 250)

        prmDocId.Value = info.DocId
        prmDocChangeId.Value = info.DocChangeId
        prmLMBY.Value = info.LastModifiedBy

        Command.Parameters.Add(prmDocId)
        Command.Parameters.Add(prmDocChangeId)
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

    Public Function GetSubtituteDocuments(ByVal docid As Integer) As List(Of DocChangeInfo)
        Dim list As New List(Of DocChangeInfo)
        Command = New SqlCommand("uspCOD_GetChangeDocuments", Connection)
        Command.CommandType = Data.CommandType.StoredProcedure

        Dim prmDocId As New SqlParameter("@docid", Data.SqlDbType.Int)
        prmDocId.Value = docid
        Command.Parameters.Add(prmDocId)

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While (DataReader.Read())
                    Dim info As New DocChangeInfo
                    info.DocSubtituteId = Integer.Parse(DataReader.Item("DocSubtitute_Id").ToString())
                    info.DocId = Integer.Parse(DataReader.Item("Doc_Id").ToString())
                    info.DocName = Convert.ToString(DataReader.Item("DocName").ToString())
                    info.DocChangeId = Integer.Parse(DataReader.Item("DOc_Change_Id").ToString())
                    info.DocNameChange = Convert.ToString(DataReader.Item("docsubtitutename"))
                    info.LastModifiedBy = Convert.ToString(DataReader.Item("lmby"))
                    info.LastModifiedDate = Convert.ToDateTime(DataReader.Item("LMDT"))
                    list.Add(info)
                End While
            End If
        Catch ex As Exception

        Finally
            Connection.Close()
        End Try

        Return list
    End Function

    Public Function GetDocuments(ByVal doctype As String) As List(Of DocInfo)

        Dim list As New List(Of DocInfo)
        Command = New SqlCommand("uspCOD_GetDocuments", Connection)
        Command.CommandType = Data.CommandType.StoredProcedure

        Dim prmDocType As New SqlParameter("@doctype", Data.SqlDbType.VarChar, 10)
        prmDocType.Value = doctype
        Command.Parameters.Add(prmDocType)

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If (DataReader.HasRows) Then
                While (DataReader.Read())
                    Dim info As New DocInfo
                    info.DocId = Integer.Parse(DataReader.Item("Doc_Id").ToString())
                    info.DocName = Convert.ToString(DataReader.Item("DocName"))
                    list.Add(info)
                End While
            End If
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try
        Return list
    End Function

    Public Function GetSubtituteDocuments(ByVal doctype As String) As List(Of DocInfo)

        Dim list As New List(Of DocInfo)
        Command = New SqlCommand("uspCOD_GetSubtituteDocuments", Connection)
        Command.CommandType = Data.CommandType.StoredProcedure

        Dim prmDocType As New SqlParameter("@doctype", Data.SqlDbType.VarChar, 10)
        prmDocType.Value = doctype
        Command.Parameters.Add(prmDocType)

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If (DataReader.HasRows) Then
                While (DataReader.Read())
                    Dim info As New DocInfo
                    info.DocId = Integer.Parse(DataReader.Item("Doc_Id").ToString())
                    info.DocName = Convert.ToString(DataReader.Item("DocName"))
                    list.Add(info)
                End While
            End If
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try
        Return list
    End Function

    Public Sub DeleteSubtituteDocument(ByVal docsubtituteid As Integer)
        Command = New SqlCommand("uspCOD_DeleteSubtituteDocument", Connection)
        Command.CommandType = Data.CommandType.StoredProcedure

        Dim prmDocSubtituteId As New SqlParameter("@docsubtituteid", Data.SqlDbType.Int)
        prmDocSubtituteId.Value = docsubtituteid
        Command.Parameters.Add(prmDocSubtituteId)

        Try
            Connection.Open()
            Command.ExecuteNonQuery()
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try

    End Sub

    Public Function GetDocPathBaseFb(ByVal packageid As String, ByVal docid As Integer) As WCCSitedocInfo
        Dim docinfo As New WCCSitedocInfo

        Command = New SqlCommand("uspGeneral_ViewDocument_Basefb", Connection)
        Command.CommandType = Data.CommandType.StoredProcedure

        Dim prmPackageId As New SqlParameter("@packageid", Data.SqlDbType.VarChar, 50)
        Dim prmDocId As New SqlParameter("@docid", Data.SqlDbType.Int)
        Dim prmDocPath As New SqlParameter("@docpath", Data.SqlDbType.VarChar, 2000)

        prmPackageId.Value = packageid
        prmDocId.Value = docid
        prmDocPath.Direction = Data.ParameterDirection.Output

        Command.Parameters.Add(prmPackageId)
        Command.Parameters.Add(prmDocId)
        Command.Parameters.Add(prmDocPath)

        Try
            Connection.Open()
            'Command.ExecuteNonQuery()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read()
                    docinfo.DocName = Convert.ToString(DataReader.Item("docname"))
                    docinfo.DocPath = Convert.ToString(DataReader.Item("docpath"))
                End While
            End If

        Catch ex As Exception
        Finally
            Connection.Close()
        End Try
        Return docinfo
    End Function

    Public Function GetDocIDByDocName(ByVal docname As String) As Integer
        Dim docid As Integer = 0
        Command = New SqlCommand("select top 1 doc_id from coddoc where docname like'" & docname & "%'", Connection)
        Command.CommandType = Data.CommandType.Text

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read()
                    docid = Integer.Parse(DataReader.Item("doc_id").ToString())
                End While
            End If
        Catch ex As Exception
            docid = 0
        Finally
            Connection.Close()
        End Try
        Return docid
    End Function

    Public Function IsInDocCRCOTransaction(ByVal docname As String, ByVal wpid As String) As Boolean
        Dim isTrue As Boolean = False
        Command = New SqlCommand("uspCRCO_CheckDocIsInTransaction", Connection)
        Command.CommandType = Data.CommandType.StoredProcedure

        Dim prmDocName As New SqlParameter("@docname", Data.SqlDbType.VarChar, 50)
        Dim prmPackageId As New SqlParameter("@packageid", Data.SqlDbType.VarChar, 50)
        Dim prmIsTrue As New SqlParameter("@isTrue", Data.SqlDbType.Bit)

        prmDocName.Value = docname
        prmPackageId.Value = wpid
        prmIsTrue.Direction = Data.ParameterDirection.Output

        Command.Parameters.Add(prmDocName)
        Command.Parameters.Add(prmPackageId)
        Command.Parameters.Add(prmIsTrue)

        Try
            Connection.Open()
            Command.ExecuteNonQuery()
            isTrue = Convert.ToBoolean(prmIsTrue.Value)
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try
        Return isTrue
    End Function

    Public Function IsParentAlreadyUploaded(ByVal siteid As int32, ByVal version As Integer, ByVal childdocid As Integer) As Boolean
        Dim isuploaded As Boolean = False
        Command = New SqlCommand("uspGeneral_CheckIsParentAlreadyUploaded", Connection)
        Command.CommandType = System.Data.CommandType.StoredProcedure

        Dim prmSiteID As New SqlParameter("@siteid", System.Data.SqlDbType.BigInt)
        Dim prmVersion As New SqlParameter("@version", System.Data.SqlDbType.Int)
        Dim prmChildDocId As New SqlParameter("@childdocid", System.Data.SqlDbType.Int)
        Dim prmisparentuploaded As New SqlParameter("@isparentuploaded", Data.SqlDbType.Bit)

        prmSiteID.Value = siteid
        prmVersion.Value = version
        prmChildDocId.Value = childdocid
        prmisparentuploaded.Direction = Data.ParameterDirection.Output

        Command.Parameters.Add(prmSiteID)
        Command.Parameters.Add(prmVersion)
        Command.Parameters.Add(prmChildDocId)
        Command.Parameters.Add(prmisparentuploaded)

        Try
            Connection.Open()
            Command.ExecuteNonQuery()
            isuploaded = Convert.ToBoolean(prmisparentuploaded.Value)
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try
        Return isuploaded
    End Function


End Class
