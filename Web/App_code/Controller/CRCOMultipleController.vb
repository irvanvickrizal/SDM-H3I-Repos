Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data
Imports System.Collections.Generic

Public Class CRCOMultipleController
    Inherits BaseController

    Public Function GetCRCOTaskPendings_Multiple(ByVal doctype As String, ByVal userid As Integer, ByVal codocid As Integer, ByVal taskdesc As String, ByVal indicativeprice As Integer) As List(Of CRCOMultipleApprovalInfo)
        Dim list As New List(Of CRCOMultipleApprovalInfo)
        Command = New SqlCommand("uspDashboardAgendaSiteDocCount_CRCOApproverReviewer_Multiple", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmUserId As New SqlParameter("@usrid", SqlDbType.BigInt)
        Dim prmCODocId As New SqlParameter("@codocid", SqlDbType.Int)
        Dim prmDocType As New SqlParameter("@doctype", SqlDbType.VarChar, 100)
        Dim prmIndicativePrice As New SqlParameter("@indicativePrice", SqlDbType.Int)
        Dim prmMultipleType As New SqlParameter("@multipletype", SqlDbType.VarChar, 100)

        prmUserId.Value = userid
        prmCODocId.Value = codocid
        prmDocType.Value = doctype
        prmIndicativePrice.Value = indicativeprice
        prmMultipleType.Value = taskdesc

        Command.Parameters.Add(prmUserId)
        Command.Parameters.Add(prmCODocId)
        Command.Parameters.Add(prmDocType)
        Command.Parameters.Add(prmIndicativePrice)
        Command.Parameters.Add(prmMultipleType)

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read
                    Dim info As New CRCOMultipleApprovalInfo
                    info.SNO = Convert.ToInt32(DataReader.Item("sno"))
                    info.DocType = Convert.ToString(DataReader.Item("DocType"))
                    info.SiteName = Convert.ToString(DataReader.Item("Sitename"))
                    info.SiteNo = Convert.ToString(DataReader.Item("SiteNo"))
                    info.PONO = Convert.ToString(DataReader.Item("Pono"))
                    info.EOName = Convert.ToString(DataReader.Item("POName"))
                    info.TASKWFCR = Integer.Parse(DataReader.Item("Tsk_Id").ToString())
                    info.PackageId = Convert.ToString(DataReader.Item("workpackageid"))
                    info.SubmitDate = Convert.ToDateTime(DataReader.Item("SubmitDate"))
                    info.IndicativePriceCostIDR = Convert.ToDouble(DataReader.Item("IndicativePriceCost_IDR"))
                    info.IndicativePriceCostUSD = Convert.ToDouble(DataReader.Item("IndicativePriceCost_USD"))
                    list.Add(info)
                End While
            End If
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try

        Return list
    End Function

    Public Function GetCRCOTaskPendings_MultiplePendingProcess(ByVal userid As Integer, ByVal codocid As Integer) As List(Of CRCOMultipleApprovalInfo)
        Dim list As New List(Of CRCOMultipleApprovalInfo)
        Command = New SqlCommand("uspDashboardAgendaSiteDocCount_CRCOApproverReviewer_MultiplePendingProcess", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmUserId As New SqlParameter("@usrid", SqlDbType.BigInt)
        Dim prmCODocId As New SqlParameter("@codocid", SqlDbType.Int)

        prmUserId.Value = userid
        prmCODocId.Value = codocid

        Command.Parameters.Add(prmUserId)
        Command.Parameters.Add(prmCODocId)

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read
                    Dim info As New CRCOMultipleApprovalInfo
                    info.SNO = Convert.ToInt32(DataReader.Item("sno"))
                    info.DocType = Convert.ToString(DataReader.Item("DocType"))
                    info.SiteName = Convert.ToString(DataReader.Item("Sitename"))
                    info.SiteNo = Convert.ToString(DataReader.Item("SiteNo"))
                    info.PONO = Convert.ToString(DataReader.Item("Pono"))
                    info.EOName = Convert.ToString(DataReader.Item("POName"))
                    info.TASKWFCR = Integer.Parse(DataReader.Item("Tsk_Id").ToString())
                    info.PackageId = Convert.ToString(DataReader.Item("workpackageid"))
                    info.SubmitDate = Convert.ToDateTime(DataReader.Item("SubmitDate"))
                    info.IndicativePriceCostIDR = Convert.ToDouble(DataReader.Item("IndicativePriceCost_IDR"))
                    info.IndicativePriceCostUSD = Convert.ToDouble(DataReader.Item("IndicativePriceCost_USD"))
                    info.ModifiedTransactionDate = Convert.ToDateTime(DataReader.Item("ModifiedTransactionDate"))
                    list.Add(info)
                End While
            End If
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try

        Return list
    End Function

    Public Function COMultipleApproval_Process(ByVal userid As Integer, ByVal sno As Int32) As Boolean
        Dim isSucceed As Boolean = True

        Command = New SqlCommand("uspCO_MultipleApproval", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmUserId As New SqlParameter("@userid", SqlDbType.Int)
        Dim prmSNO As New SqlParameter("@sno", SqlDbType.BigInt)

        prmUserId.Value = userid
        prmSNO.Value = sno

        Command.Parameters.Add(prmUserId)
        Command.Parameters.Add(prmSNO)

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

    Public Function CRMultipleApproval_Process(ByVal userid As Integer, ByVal sno As Int32) As Boolean
        Dim isSucceed As Boolean = True

        Command = New SqlCommand("uspCR_MultipleApproval", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmUserId As New SqlParameter("@userid", SqlDbType.Int)
        Dim prmSNO As New SqlParameter("@sno", SqlDbType.BigInt)

        prmUserId.Value = userid
        prmSNO.Value = sno

        Command.Parameters.Add(prmUserId)
        Command.Parameters.Add(prmSNO)

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

End Class
