Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections.Generic

Public Class POExtendedController
    Inherits BaseController

    Public Function GetPODataExtended_NullPODate() As List(Of PODataExtendedInfo)
        Dim list As New List(Of PODataExtendedInfo)
        Command = New SqlCommand("uspPO_GetDataExtended_NullPODate", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read
                    Dim info As New PODataExtendedInfo
                    info.PONO = Convert.ToString(DataReader.Item("PONO"))
                    If Not String.IsNullOrEmpty(Convert.ToString(DataReader.Item("PODate"))) Then
                        info.PODate = Convert.ToDateTime(DataReader.Item("PODate"))
                    End If
                    list.Add(info)
                End While
            End If
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try

        Return list
    End Function

    Public Function GetPODataExtended_DS(ByVal pono As String) As DataSet
        Dim ds As New DataSet
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("uspPO_GetDataExtended", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmPONO As New SqlParameter("@pono", SqlDbType.VarChar, 50)
        prmPONO.Value = pono
        Command.Parameters.Add(prmPONO)

        Try
            Connection.Open()
            Dim adapter As New SqlDataAdapter(Command)
            adapter.Fill(ds)
        Catch ex As Exception
            strErrMessage = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try

        If Not String.IsNullOrEmpty(strErrMessage) Then
            ErrorLogInsert(21, "GetPODataExtended_DS", strErrMessage, "uspPO_GetDataExtended")
        End If

        Return ds
    End Function

    Public Function PODataExtended_IU(ByVal info As PODataExtendedInfo) As Boolean
        Dim isSucceed As Boolean = True
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("uspPO_DataExtended_IU", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmPOID As New SqlParameter("@poid", SqlDbType.BigInt)
        Dim prmPONO As New SqlParameter("@pono", SqlDbType.VarChar, 50)
        Dim prmPODate As New SqlParameter("@podate", SqlDbType.DateTime)
        Dim prmLMBY As New SqlParameter("@lmby", SqlDbType.VarChar, 300)

        prmPOID.Value = info.POID
        prmPONO.Value = info.PONO
        prmPODate.Value = info.PODate
        prmLMBY.Value = info.ModifiedUser

        Command.Parameters.Add(prmPOID)
        Command.Parameters.Add(prmPONO)
        Command.Parameters.Add(prmPODate)
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
            ErrorLogInsert(21, "PODataExtended_IU", strErrMessage, "uspPO_DataExtended_IU")
        End If

        Return isSucceed
    End Function

    Public Function GetPODataRawExtended() As List(Of PODataExtendedInfo)
        Dim list As New List(Of PODataExtendedInfo)
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("uspPO_GetRawDataExtended", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read
                    Dim info As New PODataExtendedInfo
                    info.POID = Convert.ToInt32(DataReader.Item("popoid"))
                    info.POIDRaw = Convert.ToInt32(DataReader.Item("prepoid"))
                    info.PONO = Convert.ToString(DataReader.Item("pono"))
                    info.PODate = Convert.ToDateTime(DataReader.Item("existingpodate"))
                    info.NewPODate = Convert.ToDateTime(DataReader.Item("newpodate"))
                    info.NewPODateString = String.Format("{0:dd-MMMM-yyyy}", Convert.ToDateTime(DataReader.Item("newpodate")))
                    info.CDT = Convert.ToDateTime(DataReader.Item("CDT"))
                    info.ModifiedUser = Convert.ToString(DataReader.Item("CDB"))
                    list.Add(info)
                End While
            Else
                list = Nothing
            End If
        Catch ex As Exception
            strErrMessage = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try


        If Not String.IsNullOrEmpty(strErrMessage) Then
            ErrorLogInsert(21, "GetPODataRawExtended", strErrMessage, "uspPO_GetRawDataExtended")
        End If

        Return list
    End Function

    Public Function PODataRawExtended_D(ByVal poid As Int32) As Boolean
        Dim isSucceed As Boolean = True
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("Delete PODataRaw_Extended where po_id = " & poid.ToString(), Connection)
        Command.CommandType = CommandType.Text

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
            ErrorLogInsert(21, "PODataRawExtended_D", strErrMessage, "CommandType.Text")
        End If

        Return isSucceed
    End Function

    Public Function GetPODate(ByVal pono As String) As System.Nullable(Of DateTime)
        Dim podate As New System.Nullable(Of DateTime)
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("uspPO_GetPODate", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmPONO As New SqlParameter("@pono", SqlDbType.VarChar, 50)
        Dim prmPODate As New SqlParameter("@podate", SqlDbType.DateTime)
        prmPONO.Value = pono
        prmPODate.Direction = ParameterDirection.Output
        Command.Parameters.Add(prmPONO)
        Command.Parameters.Add(prmPODate)

        Try
            Connection.Open()
            Command.ExecuteNonQuery()
            If Not String.IsNullOrEmpty(Convert.ToString(prmPODate.Value)) Then
                podate = Convert.ToDateTime(prmPODate.Value)
            End If
        Catch ex As Exception
            strErrMessage = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try

        If Not String.IsNullOrEmpty(strErrMessage) Then
            ErrorLogInsert(21, "GetPODate", strErrMessage, "uspPO_GetPODate")
        End If

        Return podate
    End Function

    Public Function POMissMatch_IU(ByVal msquery As String, ByVal siteno As String, ByVal packageid As String) As Boolean
        Dim isSucceed As Boolean = True
        Dim strErrMessage As String = String.Empty

        Command = New SqlCommand("uspPO_Mismatch_IU", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmMSQuery As New SqlParameter("@msquery", SqlDbType.VarChar, 200)
        Dim prmSiteID As New SqlParameter("@siteid", SqlDbType.VarChar, 50)
        Dim prmPackageId As New SqlParameter("@packageid", SqlDbType.VarChar, 50)

        prmMSQuery.Value = msquery
        prmSiteID.Value = siteno
        prmPackageId.Value = packageid

        Command.Parameters.Add(prmMSQuery)
        Command.Parameters.Add(prmSiteID)
        Command.Parameters.Add(prmPackageId)

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
            ErrorLogInsert(2, "POMissMatch_IU", strErrMessage, "uspPO_Mismatch_IU")
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
