Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections.Generic

Public Class BDController
    Inherits BaseController

    Public Function GetAllBusinessDay_DS() As DataSet
        Dim ds As New DataSet
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("uspCODBD_GetAllBusinessDay", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Try
            Connection.Open()
            Dim adapter As New SqlDataAdapter(Command)
            adapter.Fill(ds)
        Catch ex As Exception
            ds = Nothing
            strErrMessage = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try

        If Not String.IsNullOrEmpty(strErrMessage) Then
            ErrorLogInsert(101, "GetAllBusinessDay_DS", strErrMessage, "uspCODBD_GetAllBusinessDay")
        End If

        Return ds
    End Function

    Public Function BussinessDay_IU(ByVal info As BusinessDayInfo) As Boolean
        Dim isSucceed As Boolean = True
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("uspCODBD_IU", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmBDID As New SqlParameter("@bdid", SqlDbType.BigInt)
        Dim prmOFFDate As New SqlParameter("@offdate", SqlDbType.DateTime)
        Dim prmDesc As New SqlParameter("@desc", SqlDbType.VarChar, 400)
        Dim prmLMBY As New SqlParameter("@lmby", SqlDbType.VarChar, 300)

        prmBDID.Value = info.BDID
        prmOFFDate.Value = info.OffDate
        prmDesc.Value = info.Description
        prmLMBY.Value = info.CMAInfo.ModifiedUser

        Command.Parameters.Add(prmBDID)
        Command.Parameters.Add(prmOFFDate)
        Command.Parameters.Add(prmDesc)
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
            ErrorLogInsert(101, "BussinessDay_IU", strErrMessage, "uspCODBD_IU")
        End If
        Return isSucceed
    End Function

    Public Function BusinessDay_D(ByVal bdid As Int32) As Boolean
        Dim isSucceed As Boolean = True
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("uspCODBD_D", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmBDID As New SqlParameter("@bdid", SqlDbType.BigInt)
        prmBDID.Value = bdid
        Command.Parameters.Add(prmBDID)
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
            ErrorLogInsert(101, "BusinessDay_D", strErrMessage, "uspCODBD_D")
        End If

        Return isSucceed
    End Function

    Public Function BusinessDay_IsExist(ByVal offdate As DateTime) As Boolean
        Dim isexist As Boolean = False
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("uspCODBD_IsExist", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmIsExist As New SqlParameter("@isexist", SqlDbType.Bit)
        Dim prmOffDate As New SqlParameter("@offdate", SqlDbType.DateTime)

        prmIsExist.Direction = ParameterDirection.Output
        prmOffDate.Value = offdate

        Command.Parameters.Add(prmIsExist)
        Command.Parameters.Add(prmOffDate)

        Try
            Connection.Open()
            Command.ExecuteNonQuery()
            isexist = Convert.ToBoolean(prmIsExist.Value)
        Catch ex As Exception
            strErrMessage = ex.Message.ToString()
            isexist = True
        Finally
            Connection.Close()
        End Try

        If Not String.IsNullOrEmpty(strErrMessage) Then
            ErrorLogInsert(101, "BusinessDay_isExist", strErrMessage, "uspCODBD_IsExist")
        End If
        Return isexist
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
