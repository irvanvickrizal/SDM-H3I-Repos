Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections.Generic


Public Class MSFIController
    Inherits BaseController

    Public Function MSFI_GetID(ByVal wpid As String, ByVal lmby As Integer, ByVal potype As String) As Int32
        Dim getid As Int32 = 0
        Command = New SqlCommand("uspMSFI_GetID", Connection)
        Command.CommandType = CommandType.StoredProcedure
        Dim prmWPID As New SqlParameter("@wpid", SqlDbType.VarChar, 30)
        Dim prmLMBY As New SqlParameter("@lmby", SqlDbType.Int)
        Dim prmPOType As New SqlParameter("@potype", SqlDbType.VarChar, 10)
        Dim prmMSFID As New SqlParameter("@getmsfiid", SqlDbType.BigInt)
        prmWPID.Value = wpid
        prmLMBY.Value = lmby
        prmPOType.Value = potype
        prmMSFID.Direction = ParameterDirection.Output
        Command.Parameters.Add(prmWPID)
        Command.Parameters.Add(prmLMBY)
        Command.Parameters.Add(prmPOType)
        Command.Parameters.Add(prmMSFID)
        Try
            Connection.Open()
            Command.ExecuteNonQuery()
            getid = Convert.ToInt32(prmMSFID.Value)
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try

        Return getid
    End Function

    Public Function ODmsfi_IU(ByVal info As MSFIInfo) As Boolean
        Dim isSucceed As Boolean = True
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("HCPT_uspmsfi_IU", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmWPID As New SqlParameter("@wpid", SqlDbType.VarChar, 50)
        Dim prmIsPassed As New SqlParameter("@ispassed", SqlDbType.Bit)
        Dim prmIsNotPassed As New SqlParameter("@isnotpassed", SqlDbType.Bit)
        Dim prmLMBY As New SqlParameter("@lmby", SqlDbType.Int)
        Dim prmPOType As New SqlParameter("@potype", SqlDbType.VarChar, 20)

        prmWPID.Value = info.SiteInf.PackageId
        prmIsNotPassed.Value = info.IsNotPassed
        prmIsPassed.Value = info.IsPassed
        prmLMBY.Value = info.CMAInfo.LMBY
        prmPOType.Value = info.POType

        Command.Parameters.Add(prmWPID)
        Command.Parameters.Add(prmIsNotPassed)
        Command.Parameters.Add(prmIsPassed)
        Command.Parameters.Add(prmLMBY)
        Command.Parameters.Add(prmPOType)


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
            ErrorLogInsert(150, "ODmsfi_IU", strErrMessage, "HCPT_uspmsfi_IU")
        End If

        Return isSucceed
    End Function

    Public Sub Parent_MSFI(ByVal list_id As Integer)
        Command = New SqlCommand("select sn,docname from codmasterlist where parent_id =0", Connection)
        Command.CommandType = CommandType.Text

        Try
            Connection.Open()
            Command.ExecuteNonQuery()
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try
    End Sub

    Public Function ODmsfi_LD(ByVal wpid As String) As MSFIInfo
        Dim info As New MSFIInfo
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("HCPT_uspmsfi_LD", Connection)
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
                    info.msfiID = Convert.ToInt32(DataReader.Item("msfi_ID"))
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
            ErrorLogInsert(150, "ODmsfi_LD", strErrMessage, "HCPT_uspmsfi_LD")
        End If

        Return info
    End Function

    Public Function MSFIDetail_I(ByVal listid As Integer, ByVal msfiid As Int32, ByVal lmby As Integer) As Boolean
        Dim isSucceed As Boolean = True
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("uspMSFIDetail_IU", Connection)
        Command.CommandType = CommandType.StoredProcedure
        Dim prmListID As New SqlParameter("@listid", SqlDbType.Int)
        Dim prmLMBY As New SqlParameter("@lmby", SqlDbType.Int)
        Dim prmMSFID As New SqlParameter("@msfid", SqlDbType.BigInt)
        prmListID.Value = listid
        prmLMBY.Value = lmby
        prmMSFID.Value = msfiid
        Command.Parameters.Add(prmListID)
        Command.Parameters.Add(prmLMBY)
        Command.Parameters.Add(prmMSFID)
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
            ErrorLogInsert(150, "MSFIController:MSFIDetail_I", strErrMessage, "uspMSFIDetail_IU")
        End If

        Return isSucceed
    End Function

    Public Function MSFIDetail_Category_U(ByVal msfidetid As Int32, ByVal iscopy As Boolean, ByVal isoriginal As Boolean, ByVal isna As Boolean, ByVal remarks As String, ByVal lmby As Integer) As Boolean
        Dim isSucceed As Boolean = True
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("uspMSFIDetail_Category_U", Connection)
        Command.CommandType = CommandType.StoredProcedure
        Dim prmMSFIDetID As New SqlParameter("@msfidetid", SqlDbType.BigInt)
        Dim prmIsCopy As New SqlParameter("@iscopy", SqlDbType.Bit)
        Dim prmIsOriginal As New SqlParameter("@isoriginal", SqlDbType.Bit)
        Dim prmIsNa As New SqlParameter("@isna", SqlDbType.Bit)
        Dim prmLMBY As New SqlParameter("@lmby", SqlDbType.Int)
        Dim prmRemarks As New SqlParameter("@remarks", SqlDbType.VarChar, 200)
        
        prmMSFIDetID.Value = msfidetid
        prmIsCopy.Value = iscopy
        prmIsOriginal.Value = isoriginal
        prmIsNa.Value = isna
        prmLMBY.Value = lmby

        Command.Parameters.Add(prmMSFIDetID)
        Command.Parameters.Add(prmIsCopy)
        Command.Parameters.Add(prmIsOriginal)
        Command.Parameters.Add(prmIsNa)
        Command.Parameters.Add(prmLMBY)

        If (Not String.IsNullOrEmpty(remarks)) Then
            prmRemarks.Value = remarks
            Command.Parameters.Add(prmRemarks)
        End If

        Try
            Connection.Open()
            Command.ExecuteNonQuery()
        Catch ex As Exception
            strErrMessage = ex.Message.ToString()
            isSucceed = False
        Finally
            Connection.Close()
        End Try

        If isSucceed = False Then
            ErrorLogInsert(150, "MSFIController:MSFIDetail_Category_U", "error: " & strErrMessage, "uspMSFIDetail_Category_U")
        End If
        Return isSucceed
    End Function

    Public Function MSFIDetail_D(ByVal msfiid As Int32, ByVal listid As Integer) As Boolean
        Dim isSucceed As Boolean = True
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("uspMSFIDetail_D", Connection)
        Command.CommandType = CommandType.StoredProcedure
        Dim prmMSFIID As New SqlParameter("@msfiid", SqlDbType.BigInt)
        Dim prmListID As New SqlParameter("@listid", SqlDbType.Int)
        prmMSFIID.Value = msfiid
        prmListID.Value = listid
        Command.Parameters.Add(prmMSFIID)
        Command.Parameters.Add(prmListID)
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
            ErrorLogInsert(150, "MSFIController:MSFIDetail_D", strErrMessage, "uspMSFIDetail_D")
        End If

        Return isSucceed
    End Function

    Public Function MSFIDetail_GetList(ByVal msfiid As Int32) As DataTable
        Dim dtResult As New DataTable
        Command = New SqlCommand("uspMSFIDetail_GetList", Connection)
        Command.CommandType = CommandType.StoredProcedure
        Dim prmMSFIID As New SqlParameter("@msfiid", SqlDbType.BigInt)
        prmMSFIID.Value = msfiid
        Command.Parameters.Add(prmMSFIID)
        Try
            Connection.Open()
            DataReader = Command.ExecuteReader
            If DataReader.HasRows Then
                dtResult.Load(DataReader)
            End If
        Catch ex As Exception

        Finally
            Connection.Close()
        End Try
        Return dtResult
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
