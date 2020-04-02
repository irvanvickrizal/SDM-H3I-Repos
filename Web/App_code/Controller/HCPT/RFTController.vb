Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections.Generic

Public Class RFTController
    Inherits BaseController

    Public Function ODRFT_IU(ByVal info As RFTInfo) As Boolean
        Dim isSucceed As Boolean = True
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("HCPT_uspRFT_IU", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmWPID As New SqlParameter("@wpid", SqlDbType.VarChar, 50)
        Dim prmIsPassed As New SqlParameter("@ispassed", SqlDbType.Bit)
        Dim prmIsNotPassed As New SqlParameter("@isnotpassed", SqlDbType.Bit)
        Dim prmLMBY As New SqlParameter("@lmby", SqlDbType.Int)

        prmWPID.Value = info.SiteInf.PackageId
        prmIsNotPassed.Value = info.IsNotPassed
        prmIsPassed.Value = info.IsPassed
        prmLMBY.Value = info.CMAInfo.LMBY

        Command.Parameters.Add(prmWPID)
        Command.Parameters.Add(prmIsNotPassed)
        Command.Parameters.Add(prmIsPassed)
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
            ErrorLogInsert(150, "ODRFT_IU", strErrMessage, "HCPT_uspRFT_IU")
        End If

        Return isSucceed
    End Function

    Public Function ODRFT_LD(ByVal wpid As String) As RFTInfo
        Dim info As New RFTInfo
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("HCPT_uspRFT_LD", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmWPID As New SqlParameter("@wpid", SqlDbType.VarChar, 50)
        prmWPID.Value = wpid
        Command.Parameters.Add(prmWPID)

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read
                    info.SiteInf.PackageId = wpid
                    info.RFTID = Convert.ToInt32(DataReader.Item("RFT_ID"))
                    info.IsPassed = Convert.ToBoolean(DataReader.Item("IsPassed"))
                    info.IsNotPassed = Convert.ToBoolean(DataReader.Item("IsNotPassed"))
                    info.CMAInfo.LMBY = Integer.Parse(DataReader.Item("LMBY").ToString())
                    info.CMAInfo.ModifiedUser = Convert.ToString(DataReader.Item("name"))
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
            ErrorLogInsert(150, "ODRFT_LD", strErrMessage, "HCPT_uspRFT_LD")
        End If

        Return info
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
