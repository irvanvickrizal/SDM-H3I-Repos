Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections.Generic

Public Class SOACWCTRController
    Inherits BaseController

    Public Function GetWCTRSNOBaseOnSOACID(ByVal soacid As Int32) As Int32
        Dim sno As Int32 = 0
        Dim strErrMessage As String = String.Empty

        Command = New SqlCommand("uspSOAC_GetWCTRSNO_BaseOnSOACID", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmSOACID As New SqlParameter("@soacid", SqlDbType.BigInt)
        Dim prmSNO As New SqlParameter("@sno", SqlDbType.BigInt)

        prmSOACID.Value = soacid
        prmSNO.Direction = ParameterDirection.Output

        Command.Parameters.Add(prmSOACID)
        Command.Parameters.Add(prmSNO)

        Try
            Connection.Open()
            Command.ExecuteNonQuery()
            sno = Convert.ToInt32(prmSNO.Value)
        Catch ex As Exception
            strErrMessage = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try

        If Not String.IsNullOrEmpty(strErrMessage) Then
            ErrorLogInsert(20, "GetWCTRSNOBaseOnSOACID", strErrMessage, "uspSOAC_GetWCTRSNO_BaseOnSOACID")
        End If

        Return sno
    End Function

    Public Function WCTRSOAC_IU(ByVal info As ODSOACWCTRInfo) As Boolean
        Dim isSucceed As Boolean = True
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("uspSOAC_WCTR_IU", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmSNO As New SqlParameter("@sno", SqlDbType.BigInt)
        Dim prmSOACID As New SqlParameter("@soacid", SqlDbType.BigInt)
        Dim prmWDay As New SqlParameter("@wday", SqlDbType.VarChar, 20)
        Dim prmWDate As New SqlParameter("@wdate", SqlDbType.Int)
        Dim prmWMonth As New SqlParameter("@wmonth", SqlDbType.Int)
        Dim prmWYear As New SqlParameter("@wyear", SqlDbType.Int)
        Dim prmWFID As New SqlParameter("@wfid", SqlDbType.Int)
        Dim prmDurationExec As New SqlParameter("@durationexec", SqlDbType.Int)
        Dim prmWStart As New SqlParameter("@wstart", SqlDbType.DateTime)
        Dim prmWSFinish As New SqlParameter("@wsFinish", SqlDbType.DateTime)
        Dim prmWHFinish As New SqlParameter("@whFinish", SqlDbType.DateTime)
        Dim prmActualExec As New SqlParameter("@actualexec", SqlDbType.Int)
        Dim prmTotalA As New SqlParameter("@totala", SqlDbType.Int)
        Dim prmTotalB As New SqlParameter("@totalb", SqlDbType.Int)
        Dim prmTotalC As New SqlParameter("@totalc", SqlDbType.Int)
        Dim prmTotalD As New SqlParameter("@totald", SqlDbType.Int)
        Dim prmLMBY As New SqlParameter("@lmby", SqlDbType.Int)
        Dim prmDOtherR1 As New SqlParameter("@dotherR1", SqlDbType.VarChar, 200)
        Dim prmDOtherR2 As New SqlParameter("@dotherR2", SqlDbType.VarChar, 200)
        Dim prmDOtherR3 As New SqlParameter("@dotherR3", SqlDbType.VarChar, 200)
        Dim prmDOtherR1Days As New SqlParameter("@dotherR1days", SqlDbType.Int)
        Dim prmDOtherR2Days As New SqlParameter("@dotherR2days", SqlDbType.Int)
        Dim prmDOtherR3Days As New SqlParameter("@dotherR3days", SqlDbType.Int)
        Dim prmWSKPIAccepted As New SqlParameter("@wskpiaccepted", SqlDbType.DateTime)

        prmSNO.Value = info.SNO
        prmSOACID.Value = info.SOACInfo.SOACID
        prmWDay.Value = info.WDay
        prmWDate.Value = info.WDate
        prmWMonth.Value = info.WMonth
        prmWYear.Value = info.WYear
        prmWFID.Value = info.WFID
        prmDurationExec.Value = info.DurataionExec
        prmWStart.Value = info.WStart
        prmWSFinish.Value = info.WSFinish
        prmWHFinish.Value = info.WHFinish
        prmActualExec.Value = info.ActualExec
        prmTotalA.Value = info.TotalA
        prmTotalB.Value = info.TotalB
        prmTotalC.Value = info.TotalC
        prmTotalD.Value = info.TotalD
        prmLMBY.Value = info.LMBY
        prmDOtherR1.Value = info.DOtherR1
        prmDOtherR2.Value = info.DOtherR2
        prmDOtherR3.Value = info.DOtherR3
        prmDOtherR1Days.Value = info.DOtherR1Days
        prmDOtherR2Days.Value = info.DOtherR2Days
        prmDOtherR3Days.Value = info.DOtherR3Days
        prmWSKPIAccepted.Value = info.SOACInfo.OnAirDate

        Command.Parameters.Add(prmSNO)
        Command.Parameters.Add(prmSOACID)
        Command.Parameters.Add(prmWDay)
        Command.Parameters.Add(prmWDate)
        Command.Parameters.Add(prmWMonth)
        Command.Parameters.Add(prmWYear)
        Command.Parameters.Add(prmWFID)
        Command.Parameters.Add(prmDurationExec)
        Command.Parameters.Add(prmWStart)
        Command.Parameters.Add(prmWSFinish)
        Command.Parameters.Add(prmWHFinish)
        Command.Parameters.Add(prmActualExec)
        Command.Parameters.Add(prmTotalA)
        Command.Parameters.Add(prmTotalB)
        Command.Parameters.Add(prmTotalC)
        Command.Parameters.Add(prmTotalD)
        Command.Parameters.Add(prmDOtherR1)
        Command.Parameters.Add(prmDOtherR2)
        Command.Parameters.Add(prmDOtherR3)
        Command.Parameters.Add(prmDOtherR1Days)
        Command.Parameters.Add(prmDOtherR2Days)
        Command.Parameters.Add(prmDOtherR3Days)
        Command.Parameters.Add(prmLMBY)
        Command.Parameters.Add(prmWSKPIAccepted)

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
            isSucceed = False
            ErrorLogInsert(20, "WCTRSOAC_IU", strErrMessage, "uspSOAC_WCTR_IU")
        End If

        Return isSucceed
    End Function

    Public Function GetWCTRSOACDetail(ByVal sno As Int32) As ODSOACWCTRInfo
        Dim info As New ODSOACWCTRInfo
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("uspSOAC_GetDetailWCTR", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmSNO As New SqlParameter("@sno", SqlDbType.BigInt)
        prmSNO.Value = sno
        Command.Parameters.Add(prmSNO)

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read
                    info.SOACInfo.SOACID = Convert.ToInt32(DataReader.Item("soac_id"))
                    If Not String.IsNullOrEmpty(Convert.ToString(DataReader.Item("WSKPIApproval"))) Then
                        info.SOACInfo.OnAirDate = Convert.ToDateTime(DataReader.Item("WSKPIApproval"))
                    End If
                    info.SNO = Convert.ToInt32(DataReader.Item("sno"))
                    info.WFID = Integer.Parse(DataReader.Item("WF_ID").ToString())
                    info.WDay = Convert.ToString(DataReader.Item("WDay"))
                    info.WDate = Integer.Parse(DataReader.Item("WDate").ToString())
                    info.WMonth = Integer.Parse(DataReader.Item("WMonth").ToString())
                    info.WYear = Integer.Parse(DataReader.Item("WYear").ToString())
                    info.DurataionExec = Integer.Parse(DataReader.Item("durationexec").ToString())
                    If Not String.IsNullOrEmpty(Convert.ToString(DataReader.Item("wstart"))) Then
                        info.WStart = Convert.ToDateTime(DataReader.Item("wstart"))
                    End If
                    If Not String.IsNullOrEmpty(Convert.ToString(DataReader.Item("wsfinish"))) Then
                        info.WSFinish = Convert.ToDateTime(DataReader.Item("wsfinish"))
                    End If
                    If Not String.IsNullOrEmpty(Convert.ToString(DataReader.Item("whfinish"))) Then
                        info.WHFinish = Convert.ToDateTime(DataReader.Item("whfinish"))
                    End If

                    info.ActualExec = Integer.Parse(DataReader.Item("actualexec").ToString())
                    info.TotalA = Integer.Parse(DataReader.Item("totalA").ToString())
                    info.TotalB = Integer.Parse(DataReader.Item("totalB").ToString())
                    info.TotalC = Integer.Parse(DataReader.Item("totalC").ToString())
                    info.TotalD = Integer.Parse(DataReader.Item("totalD").ToString())

                    info.DOtherR1 = Convert.ToString(DataReader.Item("DOtherR1"))
                    info.DOtherR2 = Convert.ToString(DataReader.Item("DOtherR2"))
                    info.DOtherR3 = Convert.ToString(DataReader.Item("DOtherR3"))

                    info.DOtherR1Days = Integer.Parse(DataReader.Item("DOtherR1days").ToString())
                    info.DOtherR2Days = Integer.Parse(DataReader.Item("DOtherR2days").ToString())
                    info.DOtherR3Days = Integer.Parse(DataReader.Item("DOtherR3days").ToString())

                    info.LMBY = Integer.Parse(DataReader.Item("lmby").ToString())
                    info.LMDT = Convert.ToDateTime(DataReader.Item("LMDT"))

                End While
            End If
        Catch ex As Exception
            strErrMessage = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try

        If Not String.IsNullOrEmpty(strErrMessage) Then
            ErrorLogInsert(20, "GetWCTRSOACDetail", strErrMessage, "uspSOAC_GetDetailWCTR")
        End If

        Return info
    End Function

    Public Function GetTotalDelay(ByVal soacid As Int32) As Integer
        Dim totald As Integer = 0
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("uspSOAC_GetTotalDelay", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmSOACID As New SqlParameter("@soacid", SqlDbType.BigInt)
        Dim prmTotalD As New SqlParameter("@totald", SqlDbType.Int)

        prmSOACID.Value = soacid
        prmTotalD.Direction = ParameterDirection.Output

        Command.Parameters.Add(prmSOACID)
        Command.Parameters.Add(prmTotalD)

        Try
            Connection.Open()
            Command.ExecuteNonQuery()
            totald = Integer.Parse(prmTotalD.Value.ToString())
        Catch ex As Exception
            strErrMessage = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try

        If Not String.IsNullOrEmpty(strErrMessage) Then
            ErrorLogInsert(20, "GetTotalDelay", strErrMessage, "uspSOAC_GetTotalDelay")
        End If

        Return totald
    End Function

#Region "Others"
    Public Function GetPOReceivedDate(ByVal pono As String) As Nullable(Of DateTime)
        Dim poreceiveddate As Nullable(Of DateTime) = Nothing
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("uspGeneral_GetPOReceivedDate", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmPONO As New SqlParameter("@pono", SqlDbType.VarChar, 50)
        Dim prmPOReceivedDate As New SqlParameter("@poreceiveddate", SqlDbType.DateTime)

        prmPONO.Value = pono
        prmPOReceivedDate.Direction = ParameterDirection.Output

        Command.Parameters.Add(prmPONO)
        Command.Parameters.Add(prmPOReceivedDate)

        Try
            Connection.Open()
            Command.ExecuteNonQuery()
            If Not String.IsNullOrEmpty(Convert.ToString(prmPOReceivedDate.Value)) Then
                poreceiveddate = Convert.ToDateTime(prmPOReceivedDate.Value)
            End If
        Catch ex As Exception
            strErrMessage = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try

        If Not String.IsNullOrEmpty(strErrMessage) Then
            ErrorLogInsert(20, "GetPOReceivedDate", strErrMessage, "uspGeneral_GetPOReceivedDate")
        End If

        Return poreceiveddate
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

End Class
