Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient

Public Class BAUTMasterController
    Inherits BaseController
    Function GetBAUTDoneMaster_DS(ByVal pono As String, ByVal packageid As String, ByVal userid As Integer) As DataSet
        Dim ds As New DataSet
        Command = New SqlCommand("uspBAUT_GetBAUTDoneWithReferenceNo", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmPONO As New SqlParameter("@pono", SqlDbType.VarChar, 50)
        Dim prmPackageId As New SqlParameter("@packageid", SqlDbType.VarChar, 50)
        Dim prmUserId As New SqlParameter("@usrid", SqlDbType.Int)

        If String.IsNullOrEmpty(pono) Then
            prmPONO.Value = Nothing
        Else
            prmPONO.Value = pono
        End If

        If String.IsNullOrEmpty(packageid) Then
            prmPackageId.Value = Nothing
        Else
            prmPackageId.Value = packageid
        End If

        prmUserId.Value = userid

        Command.Parameters.Add(prmPONO)
        Command.Parameters.Add(prmPackageId)
        Command.Parameters.Add(prmUserId)

        Try
            Connection.Open()
            Dim adapter As New SqlDataAdapter(Command)
            adapter.Fill(ds)
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try

        Return ds
    End Function

    Function GetPoNoBaseRole(ByVal userid As Integer) As DataTable
        Dim dt As New DataTable
        Command = New SqlCommand("uspPO_GetPoBaseRole", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmUserid As New SqlParameter("usrid", SqlDbType.Int)
        prmUserid.Value = userid
        Command.Parameters.Add(prmUserid)
        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                dt.Load(DataReader)
            End If
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try
        Return dt
    End Function

    Public Function UpdateBautRefNo(ByVal siteid As Int32, ByVal version As Integer, ByVal pono As String, ByVal wpid As String, ByVal refno As String, ByVal bautid As Integer, ByVal pstatus As Boolean) As Boolean
        Dim isSucceed As Boolean = True
        Command = New SqlCommand("uspBAUT_UpdateRefNo", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmSiteId As New SqlParameter("@siteid", SqlDbType.BigInt)
        Dim prmVersion As New SqlParameter("@version", SqlDbType.Int)
        Dim prmPONO As New SqlParameter("@pono", SqlDbType.VarChar, 50)
        Dim prmWPID As New SqlParameter("@wpid", SqlDbType.VarChar, 50)
        Dim prmRefNo As New SqlParameter("@refno", SqlDbType.VarChar, 250)
        Dim prmBAUTID As New SqlParameter("@BAUTID", SqlDbType.Int)
        Dim prmPStatus As New SqlParameter("@pstatus", SqlDbType.Bit)

        prmSiteId.Value = siteid
        prmVersion.Value = version
        prmPONO.Value = pono
        prmWPID.Value = wpid
        prmRefNo.Value = refno
        prmBAUTID.Value = bautid
        prmPStatus.Value = pstatus

        Command.Parameters.Add(prmSiteId)
        Command.Parameters.Add(prmVersion)
        Command.Parameters.Add(prmPONO)
        Command.Parameters.Add(prmWPID)
        Command.Parameters.Add(prmRefNo)
        Command.Parameters.Add(prmBAUTID)
        Command.Parameters.Add(prmPStatus)

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

    Public Function GetDetailBAUT(ByVal siteid As Int32, ByVal version As Integer, ByVal bautid As Integer) As BAUTMasterInfo
        Dim info As New BAUTMasterInfo
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("uspGeneral_GetOnAirDateReferToBAUT", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmSiteID As New SqlParameter("@siteid", SqlDbType.BigInt)
        Dim prmVersion As New SqlParameter("@version", SqlDbType.Int)
        Dim prmBAUTID As New SqlParameter("@bautid", SqlDbType.Int)

        prmSiteID.Value = siteid
        prmVersion.Value = version
        prmBAUTID.Value = bautid

        Command.Parameters.Add(prmSiteID)
        Command.Parameters.Add(prmVersion)
        Command.Parameters.Add(prmBAUTID)

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read()
                    info.SiteInf.SiteNo = Convert.ToString(DataReader.Item("SiteNo"))
                    info.SiteInf.PONO = Convert.ToString(DataReader.Item("Pono"))
                    info.SiteVersion = version
                    info.SiteId = siteid
                    If Not String.IsNullOrEmpty(Convert.ToString(DataReader.Item("OnAirDated"))) Then
                        info.OnAirDateBAUT = Convert.ToDateTime(DataReader.Item("OnAirDateBAUT"))
                    End If
                    If Not String.IsNullOrEmpty(Convert.ToString(DataReader.Item("kpiDate"))) Then
                        info.KPIDateBAUT = Convert.ToDateTime(DataReader.Item("KPIDate"))
                    End If
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
            ErrorLogInsert(250, "GetDetailBAUT", strErrMessage, "uspGeneral_GetOnAirDateReferToBAUT")
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
