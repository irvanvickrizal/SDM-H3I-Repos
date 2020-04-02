Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections.Generic

Public Class UnManageDocController
    Inherits BaseController

    Public Function GetUnmanageDocs(ByVal parentdoctype As String) As List(Of UnManageDocInfo)
        Dim list As New List(Of UnManageDocInfo)
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("uspCOD_GetUnmanageDocs", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmParentDocType As New SqlParameter("@parentdoctype", SqlDbType.VarChar, 200)
        prmParentDocType.Value = parentdoctype
        Command.Parameters.Add(prmParentDocType)

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read
                    Dim info As New UnManageDocInfo
                    info.UnDocId = Integer.Parse(DataReader.Item("UnDoc_Id").ToString())
                    info.DocId = Integer.Parse(DataReader.Item("Doc_Id").ToString())
                    info.DocName = Convert.ToString(DataReader.Item("DocName"))
                    info.ParentDocType = Convert.ToString(DataReader.Item("Parent_Doc_Type"))
                    info.ModifiedUser = Convert.ToString(DataReader.Item("LMBY"))
                    info.LMDT = Convert.ToDateTime(DataReader.Item("LMDT"))
                    list.Add(info)
                End While
            End If
        Catch ex As Exception
            strErrMessage = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try

        If Not String.IsNullOrEmpty(strErrMessage) Then
            ErrorLogInsert(4, "GetUnmanageDocs", strErrMessage, "uspCOD_GetUnmanageDocs")
        End If

        Return list
    End Function

    Public Function GetDocumentsBaseonParentDocType(ByVal parentdoctype As String) As List(Of CODDocumentInfo)
        Dim list As New List(Of CODDocumentInfo)
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("uspCOD_GetDocuments_BaseonParentDocType", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmParentDocType As New SqlParameter("@parentdoctype", SqlDbType.VarChar, 200)
        prmParentDocType.Value = parentdoctype
        Command.Parameters.Add(prmParentDocType)

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read
                    Dim info As New CODDocumentInfo
                    info.DocId = Integer.Parse(DataReader.Item("doc_id").ToString())
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
            ErrorLogInsert(4, "GetDocumentsBaseonParentDocType", strErrMessage, "uspCOD_GetDocuments_BaseonParentDocType")
        End If
        Return list
    End Function

    Public Function UnManageDoc_I(ByVal info As UnManageDocInfo) As Boolean
        Dim isSucceed As Boolean = True
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("uspCOD_UnmanageDoc_I", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmDocId As New SqlParameter("@docid", SqlDbType.Int)
        Dim prmParentDocType As New SqlParameter("@parentdoctype", SqlDbType.VarChar, 200)
        Dim prmLMBY As New SqlParameter("@lmby", SqlDbType.VarChar, 300)

        prmDocId.Value = info.DocId
        prmParentDocType.Value = info.ParentDocType
        prmLMBY.Value = info.ModifiedUser

        Command.Parameters.Add(prmDocId)
        Command.Parameters.Add(prmParentDocType)
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

        If isSucceed = False Then
            ErrorLogInsert(4, "UnManageDoc_I", strErrMessage, "uspCOD_UnmanageDoc_I")
        End If

        Return isSucceed
    End Function

    Public Function UnManageDoc_D(ByVal undocid As Integer) As Boolean
        Dim isSucceed As Boolean = True
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("uspCOD_UnmanageDoc_D", Connection)
        Command.CommandType = CommandType.StoredProcedure
        Dim prmUnDocId As New SqlParameter("@undocid", SqlDbType.Int)
        prmUnDocId.Value = undocid
        Command.Parameters.Add(prmUnDocId)

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
            ErrorLogInsert(4, "UnManageDoc_D", strErrMessage, "uspCOD_UnmanageDoc_D")
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
