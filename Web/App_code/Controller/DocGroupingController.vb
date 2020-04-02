Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections.Generic


Public Class DocGroupingController
    Inherits BaseController

    Public Function CODDOC_ScopeGrouping_IU(ByVal info As DocScopeGroupingInfo) As Boolean
        Dim isSucceed As Boolean = True
        Command = New SqlCommand("uspDoc_CODocScopeGrouping_IU", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmDocGroupId As New SqlParameter("@docgroupid", SqlDbType.BigInt)
        Dim prmDocId As New SqlParameter("@docid", SqlDbType.Int)
        Dim prmDScopeId As New SqlParameter("@dscopeid", SqlDbType.Int)
        Dim prmLmby As New SqlParameter("@lmby", SqlDbType.VarChar, 250)

        prmDocGroupId.Value = info.DocGroupId
        prmDocId.Value = info.DocId
        prmDScopeId.Value = info.DScopeId
        prmLmby.Value = info.LMBY

        Command.Parameters.Add(prmDocGroupId)
        Command.Parameters.Add(prmDocId)
        Command.Parameters.Add(prmDScopeId)
        Command.Parameters.Add(prmLmby)

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

    Public Function GetCODDocuments_ScopeGrouping() As List(Of DocScopeGroupingInfo)
        Dim list As New List(Of DocScopeGroupingInfo)
        Command = New SqlCommand("uspCOD_GetDoc_ScopeGrouping", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read
                    Dim info As New DocScopeGroupingInfo
                    info.DocGroupId = Convert.ToInt32(DataReader.Item("docgrouping_Id"))
                    info.DocId = Integer.Parse(DataReader.Item("Doc_id").ToString())
                    info.DocName = Convert.ToString(DataReader.Item("docname"))
                    info.DScopeId = Integer.Parse(DataReader.Item("DScope_Id").ToString())
                    info.DScopeName = Convert.ToString(DataReader.Item("Dscope_Name"))
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

    ''' <summary>
    ''' Created by Fauzan, 24 Dec 2018
    ''' To cater execute non query operation for CODDoc_ScopeGrouping table
    ''' </summary>
    ''' <param name="info"></param>
    ''' <param name="operation">insert/update/delete</param>
    ''' <returns>Boolean</returns>
    Public Function CODDOC_ScopeGrouping_ExecuteNonQuery(ByVal info As DocScopeGroupingInfo, ByVal operation As String) As Boolean
        Dim query As String
        Select Case operation.ToLower()
            Case "insert"
                query = "insert CODDoc_ScopeGrouping(Doc_Id, DScope_Id, LMBY, LMDT) values(" & info.DocId & "," & info.DScopeId & ", '" & info.LMBY & "', getdate())"
            Case "delete"
                query = "delete from CODDoc_ScopeGrouping where DocGrouping_Id = " & info.DocGroupId & " "
            Case Else
                query = ""
        End Select
        Command = New SqlCommand(query, Connection)
        Command.CommandType = CommandType.Text
        Try
            Connection.Open()
            Command.ExecuteNonQuery()
        Catch ex As Exception
            Connection.Close()
            Return False
        Finally
            Connection.Close()
        End Try

        Return True
    End Function

End Class
