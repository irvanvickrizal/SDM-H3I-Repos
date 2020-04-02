Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Collections.Generic
Imports System.Data.SqlClient

Public Class OnAirDateController
    Inherits BaseController

    Public Function OnAirDateMilestone_IU(ByVal info As OnAirDateMilestoneInfo) As Boolean
        Dim isSucceed As Boolean = True
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("uspGeneral_OnAirDateMilestone_IU", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmSiteNo As New SqlParameter("@siteno", SqlDbType.VarChar, 300)
        Dim prmPoNo As New SqlParameter("@pono", SqlDbType.VarChar, 50)
        Dim prmSNO As New SqlParameter("@sno", SqlDbType.BigInt)
        Dim prmPackageId As New SqlParameter("@packageid", SqlDbType.VarChar, 50)
        Dim prmLMBY As New SqlParameter("@lmby", SqlDbType.Int)
        Dim prmOnAirDate As New SqlParameter("@onairdate", SqlDbType.DateTime)

        prmSiteNo.Value = info.SiteInf.SiteNo
        prmPoNo.Value = info.SiteInf.PONO
        prmSNO.Value = info.SNO
        prmPackageId.Value = info.SiteInf.PackageId
        prmLMBY.Value = info.LMBY
        prmOnAirDate.Value = info.OnAirDate

        Command.Parameters.Add(prmSiteNo)
        Command.Parameters.Add(prmPoNo)
        Command.Parameters.Add(prmSNO)
        Command.Parameters.Add(prmPackageId)
        Command.Parameters.Add(prmLMBY)
        Command.Parameters.Add(prmOnAirDate)

        Try
            Connection.Open()
            Command.ExecuteNonQuery()
        Catch ex As Exception
            strErrMessage = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try

        If Not String.IsNullOrEmpty(strErrMessage) Then
            isSucceed = False
            ErrorLogInsert(15, "OnAirDateMilestone_IU", strErrMessage, "uspGeneral_OnAirDateMilestone_IU")
        End If

        Return isSucceed
    End Function

    Public Function OnAirDateMilestone_U(ByVal packageid As String, ByVal onairdate As DateTime, ByVal pono As String, ByVal lmby As String) As Boolean
        Dim isSucceed As Boolean = True
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("uspGeneral_OnAirDate_Milestone_U", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmPackageID As New SqlParameter("@packageid", SqlDbType.VarChar, 50)
        Dim prmOnAirDate As New SqlParameter("@onairdate", SqlDbType.DateTime)
        Dim prmPONO As New SqlParameter("@pono", SqlDbType.VarChar, 50)

        prmPackageID.Value = packageid
        prmOnAirDate.Value = onairdate
        prmPONO.Value = pono

        Command.Parameters.Add(prmPackageID)
        Command.Parameters.Add(prmOnAirDate)
        Command.Parameters.Add(prmPONO)
        Try
            Connection.Open()
            Command.ExecuteNonQuery()
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try

        If Not String.IsNullOrEmpty(strErrMessage) Then
            ErrorLogInsert(15, "OnAirDateMilestone_U", strErrMessage, "uspGeneral_OnAirDate_Milestone_U")
        End If

        Return isSucceed
    End Function

    Public Function OnAirDateMilestone_D(ByVal sno As Int32) As Boolean
        Dim isSucceed As Boolean = True
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("uspGeneral_OnAirDate_Milestone_D", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmSNO As New SqlParameter("@sno", SqlDbType.BigInt)
        prmSNO.Value = sno
        Command.Parameters.Add(prmSNO)

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
            ErrorLogInsert(15, "OnAirDateMilestone_D", strErrMessage, "uspGeneral_OnAirDate_Milestone_D")
        End If
        Return isSucceed
    End Function

    Public Function OnAirDateMilestoneDuplicate_D(ByVal sno As Int32) As Boolean
        Dim isSucceed As Boolean = True
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("uspGeneral_OnAirDate_Milestone_Duplicate_D", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmSNO As New SqlParameter("@sno", SqlDbType.BigInt)
        prmSNO.Value = sno
        Command.Parameters.Add(prmSNO)

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
            ErrorLogInsert(15, "OnAirDateMilestoneDuplicate_D", strErrMessage, "uspGeneral_OnAirDate_Milestone_Duplicate_D")
        End If
        Return isSucceed
    End Function

    Public Function GetOnAirDateMilestone(ByVal packageid As String, ByVal pono As String) As List(Of OnAirDateMilestoneInfo)
        Dim list As New List(Of OnAirDateMilestoneInfo)
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("uspGeneral_GetOnAirDateMilestone", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmPONO As New SqlParameter("@pono", SqlDbType.VarChar, 50)
        Dim prmPackageId As New SqlParameter("@packageid", SqlDbType.VarChar, 50)

        prmPONO.Value = pono
        prmPackageId.Value = packageid

        Command.Parameters.Add(prmPONO)
        Command.Parameters.Add(prmPackageId)

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read
                    Dim info As New OnAirDateMilestoneInfo
                    info.SNO = Convert.ToInt32(DataReader.Item("SNO"))
                    info.OnAirDate = Convert.ToDateTime(DataReader.Item("OnAirDate"))
                    info.SiteInf.SiteNo = Convert.ToString(DataReader.Item("SiteNo"))
                    info.SiteInf.PONO = Convert.ToString(DataReader.Item("PoNo"))
                    info.SiteInf.PackageId = Convert.ToString(DataReader.Item("Package_Id"))
                    info.LMBY = Integer.Parse(DataReader.Item("LMBY").ToString())
                    info.ModifiedUser = Convert.ToString(DataReader.Item("ModifiedUser"))
                    info.LMDT = Convert.ToDateTime(DataReader.Item("LMDT"))
                    info.SignTitle = Convert.ToString(DataReader.Item("SignTitle"))
                    info.RoleInf.RoleName = Convert.ToString(DataReader.Item("RoleDesc"))
                    info.LockStatus = Convert.ToString(DataReader.Item("LockStatus"))
                    list.Add(info)
                End While
            End If
        Catch ex As Exception
            strErrMessage = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try

        If Not String.IsNullOrEmpty(strErrMessage) Then
            ErrorLogInsert(15, "GetOnAirDateMilestone", strErrMessage, "uspGeneral_GetOnAirDateMilestone")
        End If

        Return list
    End Function

    Public Function GetOnAirDateMilestone_DS(ByVal packageid As String, ByVal pono As String) As DataSet
        Dim ds As New DataSet
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("uspGeneral_GetOnAirDateMilestone", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmPONO As New SqlParameter("@pono", SqlDbType.VarChar, 50)
        Dim prmPackageId As New SqlParameter("@packageid", SqlDbType.VarChar, 50)

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
        Command.Parameters.Add(prmPONO)
        Command.Parameters.Add(prmPackageId)

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
            ErrorLogInsert(15, "GetOnAirDateMilestone", strErrMessage, "uspGeneral_GetOnAirDateMilestone")
        End If

        Return ds
    End Function

    Public Function GetOnAirDateMilestoneDuplicate(ByVal packageid As String, ByVal pono As String) As List(Of OnAirDateMilestoneInfo)
        Dim list As New List(Of OnAirDateMilestoneInfo)
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("uspGeneral_GetOnAirDateMilestone_Duplicate", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmPONO As New SqlParameter("@pono", SqlDbType.VarChar, 50)
        Dim prmPackageId As New SqlParameter("@packageid", SqlDbType.VarChar, 50)

        If String.IsNullOrEmpty(packageid) Then
            prmPackageId.Value = Nothing
        Else
            prmPackageId.Value = packageid
        End If

        If String.IsNullOrEmpty(pono) Then
            prmPONO.Value = Nothing
        Else
            prmPONO.Value = pono
        End If
        
        Command.Parameters.Add(prmPONO)
        Command.Parameters.Add(prmPackageId)

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read
                    Dim info As New OnAirDateMilestoneInfo
                    info.SNO = Convert.ToInt32(DataReader.Item("SNO"))
                    info.OnAirDate = Convert.ToDateTime(DataReader.Item("OnAirDate"))
                    info.SiteInf.SiteNo = Convert.ToString(DataReader.Item("SiteNo"))
                    info.SiteInf.PONO = Convert.ToString(DataReader.Item("PoNo"))
                    info.SiteInf.PackageId = Convert.ToString(DataReader.Item("Package_Id"))
                    info.LMBY = Integer.Parse(DataReader.Item("LMBY").ToString())
                    info.ModifiedUser = Convert.ToString(DataReader.Item("name"))
                    info.LMDT = Convert.ToDateTime(DataReader.Item("LMDT"))
                    info.SignTitle = Convert.ToString(DataReader.Item("SignTitle"))
                    info.RoleInf.RoleName = Convert.ToString(DataReader.Item("RoleDesc"))
                    info.LockStatus = Convert.ToString(DataReader.Item("LockStatus"))
                    list.Add(info)
                End While
            End If
        Catch ex As Exception
            strErrMessage = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try

        If Not String.IsNullOrEmpty(strErrMessage) Then
            ErrorLogInsert(15, "GetOnAirDateMilestoneDuplicate", strErrMessage, "uspGeneral_GetOnAirDateMilestone_Duplicate")
        End If

        Return list
    End Function

    Public Function IsHaveDataDuplication() As Boolean
        Dim istrue As Boolean = False
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("uspGeneral_IsHaveDataDuplication", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmIsTrue As New SqlParameter("@istrue", SqlDbType.Bit)
        prmIsTrue.Direction = ParameterDirection.Output
        Command.Parameters.Add(prmIsTrue)
        Try
            Connection.Open()
            Command.ExecuteNonQuery()
            istrue = Convert.ToBoolean(prmIsTrue.Value)
        Catch ex As Exception
            istrue = True
            strErrMessage = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try

        If Not String.IsNullOrEmpty(strErrMessage) Then
            ErrorLogInsert(15, "IsHaveDataDuplication", strErrMessage, "uspGeneral_IsHaveDataDuplication")
        End If

        Return istrue
    End Function

    Public Function SOACIsAlreadySubmitted(ByVal packageid As String) As Boolean
        Dim isSubmitted As Boolean = True
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("uspSOAC_CheckIsSubmitted", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmPackageId As New SqlParameter("@packageid", SqlDbType.VarChar, 50)
        Dim prmIsSubmitted As New SqlParameter("@issubmitted", SqlDbType.Bit)

        prmPackageId.Value = packageid
        prmIsSubmitted.Direction = ParameterDirection.Output

        Command.Parameters.Add(prmPackageId)
        Command.Parameters.Add(prmIsSubmitted)

        Try
            Connection.Open()
            Command.ExecuteNonQuery()
            isSubmitted = Convert.ToBoolean(prmIsSubmitted.Value)
        Catch ex As Exception
            strErrMessage = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try

        If Not String.IsNullOrEmpty(strErrMessage) Then
            ErrorLogInsert(15, "SOACIsAlreadySubmitted", strErrMessage, "uspSOAC_CheckIsSubmitted")
        End If

        Return isSubmitted
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
