Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections.Generic

Public Class KPIController
    Inherits BaseController

#Region "Get Site Attribute"
    Public Function GetSiteDetail_WPID(ByVal packageid As String, ByVal userid As Integer) As SiteInfo
        Dim info As New SiteInfo
        Command = New SqlCommand("HCPT_uspGeneral_GetSiteAttribute", Connection)
        Command.CommandType = CommandType.StoredProcedure
        Dim prmWPID As New SqlParameter("@wpid", SqlDbType.VarChar, 50)
        Dim prmUserId As New SqlParameter("@usrid", SqlDbType.Int)
        prmWPID.Value = packageid
        prmUserId.Value = userid
        Command.Parameters.Add(prmWPID)
        Command.Parameters.Add(prmUserId)
        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read
                    info.SiteID = Convert.ToInt32(DataReader.Item("Site_ID"))
                    info.SiteVersion = Integer.Parse(DataReader.Item("SiteVersion").ToString())
                    info.SiteNo = Convert.ToString(DataReader.Item("SiteNo"))
                    info.SiteName = Convert.ToString(DataReader.Item("SiteName"))
                    info.POName = Convert.ToString(DataReader.Item("POName"))
                    info.PONO = Convert.ToString(DataReader.Item("PONO"))
                    info.SiteIdPO = Convert.ToString(DataReader.Item("SiteIdPO"))
                    info.SiteNamePO = Convert.ToString(DataReader.Item("SiteNamePO"))
                    info.FLDType = Convert.ToString(DataReader.Item("FldType"))
                    info.Scope = Convert.ToString(DataReader.Item("Scope"))
                    info.ProjectID = Convert.ToString(DataReader.Item("TselProjectID"))
                    info.RegionName = Convert.ToString(DataReader.Item("RgnName"))
					info.POID = Convert.ToInt32(DataReader.Item("po_id"))
                End While
            Else
                info = Nothing
            End If
        Catch ex As Exception
            info = Nothing
        Finally
            Connection.Close()
        End Try

        Return info
    End Function
#End Region

#Region "KPI information"
    Public Function KPIInfo_IU(ByVal info As KPIInfo) As Boolean
        Dim isSucceed As Boolean = True
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("HCPT_uspKPI_IU", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmPackageID As New SqlParameter("@packageid", SqlDbType.VarChar, 50)
        Dim prmLMBY As New SqlParameter("@lmby", SqlDbType.Int)
        Dim prmKPIID As New SqlParameter("@kpiid", SqlDbType.BigInt)
        Dim prmLongitude As New SqlParameter("@longitude", SqlDbType.VarChar, 100)
        Dim prmLatitude As New SqlParameter("@latitude", SqlDbType.VarChar, 100)

        prmPackageID.Value = info.SiteInf.PackageId
        prmLMBY.Value = info.CMAInfo.LMBY
        prmKPIID.Value = info.KPIID
        prmLongitude.Value = info.Longitude
        prmLatitude.Value = info.Latitude

        Command.Parameters.Add(prmPackageID)
        Command.Parameters.Add(prmLMBY)
        Command.Parameters.Add(prmKPIID)
        Command.Parameters.Add(prmLongitude)
        Command.Parameters.Add(prmLatitude)

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
            Dim errcontroller As New HCPTController
            errcontroller.ErrorLogInsert(150, "KPIInfo_IU", strErrMessage, "HCPT_uspKPI_IU")
        End If

        Return isSucceed
    End Function
    Public Function KPISiteHistory(ByVal wpid As Integer) As DataSet
        Dim ds As New DataSet
        Dim strErrMessage As String = String.Empty

        Command = New SqlCommand("HCPT_SiteHistory", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmPackageID As New SqlParameter("@packageid", SqlDbType.VarChar, 50)

        prmPackageID.Value = wpid


        Command.Parameters.Add(prmPackageID)


        Try
            Connection.Open()
            Dim MyAdapter As New SqlDataAdapter(Command)
            MyAdapter.Fill(ds)
        Catch ex As Exception
            strErrMessage = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try

        Return ds
    End Function
    Public Function KPIViewDoc_SiteHistory(ByVal guid As String) As String
        'Dim docpath As String = String.Empty
        Dim dtResults As New DataTable
        Dim sResults As String = String.Empty

        Command = New SqlCommand("HCPT_Viewdoc_SiteHistory", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmwpid As New SqlParameter("@rguid", SqlDbType.VarChar, 50)
        'Dim prmDocPath As New SqlParameter("@docpath", SqlDbType.VarChar, 2000)

        prmwpid.Value = guid
        'prmDocPath.Direction = ParameterDirection.Output

        Command.Parameters.Add(prmwpid)
        'Command.Parameters.Add(prmDocPath)

        'Try
        '    Connection.Open()
        '    Command.ExecuteNonQuery()
        '    docpath = Convert.ToString(prmDocPath.Value.ToString())
        'Catch ex As Exception
        'Finally
        '    Connection.Close()
        'End Try

        'Return docpath

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                dtResults.Load(DataReader)
            End If
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try

        If dtResults.Rows.Count > 0 Then
            sResults = dtResults.Rows(0).Item(0)
        End If

        Return sResults
    End Function
    Public Function KPI_SiteHistory_Docpath(ByVal wpid As Integer, ByVal guid As String) As String
        Dim dtResults As New DataTable
        Dim sResults As String = Nothing

        Command = New SqlCommand("HCPT_GetPath_SiteHistory", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmwpid As New SqlParameter("@workpackageid", SqlDbType.Int)
        Dim prmaguid As New SqlParameter("@aguid", SqlDbType.VarChar, 100)


        prmwpid.Value = wpid
        prmaguid.Value = guid


        Command.Parameters.Add(prmwpid)
        Command.Parameters.Add(prmaguid)


        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                dtResults.Load(DataReader)
            End If
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try

        If dtResults.Rows.Count > 0 Then
            sResults = dtResults.Rows(0).Item(0)
        End If

        Return sResults
    End Function

    Public Function GetSiteHistoryDocPath(ByVal guid As String) As String
        Dim docpath As String = String.Empty
        Command = New SqlCommand("HCPT_GetSiteHistoryDocPath", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmguid As New SqlParameter("@rguid", SqlDbType.VarChar, 50)
        Dim prmDocPath As New SqlParameter("@docpath", SqlDbType.VarChar, 2000)

        prmguid.Value = guid
        prmDocPath.Direction = ParameterDirection.Output

        Command.Parameters.Add(prmguid)
        Command.Parameters.Add(prmDocPath)

        Try
            Connection.Open()
            Command.ExecuteNonQuery()
            docpath = Convert.ToString(prmDocPath.Value.ToString())
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try

        Return docpath
    End Function

    Public Function GetKPIInfo(ByVal workpackageid As String) As KPIInfo
        Dim info As New KPIInfo
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("HCPT_uspKPI_GetKPIInfo", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmPackageID As New SqlParameter("@packageid", SqlDbType.VarChar, 50)
        prmPackageID.Value = workpackageid
        Command.Parameters.Add(prmPackageID)

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read
                    info.KPIID = Convert.ToInt32(DataReader.Item("KPI_ID"))
                    info.SiteInf.PackageId = Convert.ToString(DataReader.Item("workpackageid"))
                    info.SiteInf.SiteNo = Convert.ToString(DataReader.Item("SiteNO"))
                    info.SiteInf.SiteName = Convert.ToString(DataReader.Item("SiteName"))
                    info.SiteInf.PONO = Convert.ToString(DataReader.Item("PoNO"))
                    info.SiteInf.RegionName = Convert.ToString(DataReader.Item("RgnName"))
                    info.Longitude = Convert.ToString(DataReader.Item("Longitude"))
                    info.Latitude = Convert.ToString(DataReader.Item("Latitude"))
                End While
            End If
        Catch ex As Exception
            info = Nothing
            strErrMessage = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try
        Return info
    End Function

    Public Function KPICoordinate_IU(ByVal info As KPIInfo) As Boolean
        Dim isSucceed As Boolean = True
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("HCPT_uspKPI_Coordinate_IU", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmWPID As New SqlParameter("@packageid", SqlDbType.VarChar, 50)
        Dim prmLongitude As New SqlParameter("@longitude", SqlDbType.VarChar, 100)
        Dim prmLatitude As New SqlParameter("@latitude", SqlDbType.VarChar, 100)
        Dim prmLMBY As New SqlParameter("@lmby", SqlDbType.Int)

        prmWPID.Value = info.SiteInf.PackageId
        prmLongitude.Value = info.Longitude
        prmLatitude.Value = info.Latitude
        prmLMBY.Value = info.CMAInfo.LMBY

        Command.Parameters.Add(prmWPID)
        Command.Parameters.Add(prmLongitude)
        Command.Parameters.Add(prmLatitude)
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
            Dim errcontroller As New HCPTController
            errcontroller.ErrorLogInsert(150, "KPICoordinate_IU", strErrMessage, "HCPT_uspKPI_Coordinate_IU")
        End If

        Return isSucceed
    End Function

#End Region

#Region "Site Detail Information"
    Public Function GetSiteDetailInformations(ByVal packageid As String, ByVal detailid As Int32) As List(Of KPISiteDetailInfo)
        Dim list As New List(Of KPISiteDetailInfo)
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("HCPT_uspKPI_SiteDetailInformation_LD", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmWPID As New SqlParameter("@packageid", SqlDbType.VarChar, 50)
        Dim prmDetailID As New SqlParameter("@detailid", SqlDbType.BigInt)
        prmWPID.Value = packageid
        prmDetailID.Value = detailid
        Command.Parameters.Add(prmWPID)
        Command.Parameters.Add(prmDetailID)

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read
                    Dim info As New KPISiteDetailInfo
                    info.DetailId = Convert.ToInt32(DataReader.Item("Detail_Id"))
                    info.SiteInf.PackageId = Convert.ToString(DataReader.Item("Package_Id"))
                    info.Sector = Integer.Parse(DataReader.Item("Sector").ToString())
                    info.CellId = Convert.ToString(DataReader.Item("Cell_Id"))
                    info.AntennaType = Convert.ToString(DataReader.Item("Antenna_Type"))
                    info.AntennaHeight = Integer.Parse(DataReader.Item("Antenna_Height").ToString())
                    info.Azimuth = Integer.Parse(DataReader.Item("Azimuth").ToString())
                    info.MechTilt = Integer.Parse(DataReader.Item("Mech_Tilt"))
                    info.ElecTilt = Integer.Parse(DataReader.Item("Elec_Tilt"))
                    list.Add(info)
                End While
            End If
        Catch ex As Exception
            strErrMessage = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try

        If Not String.IsNullOrEmpty(strErrMessage) Then
            Dim errcontroller As New HCPTController
            errcontroller.ErrorLogInsert(150, "GetSiteDetailInformations", strErrMessage, "HCPT_uspKPI_SiteDetailInformation_LD")
        End If

        Return list
    End Function

    Public Function GetSiteDetailInformations_DS(ByVal packgeid As String, ByVal detailid As Int32) As DataSet
        Dim ds As New DataSet
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("HCPT_uspKPI_SiteDetailInformation_LD", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmWPID As New SqlParameter("@packageid", SqlDbType.VarChar, 50)
        Dim prmDetailID As New SqlParameter("@detailid", SqlDbType.BigInt)
        prmWPID.Value = packgeid
        prmDetailID.Value = detailid
        Command.Parameters.Add(prmWPID)
        Command.Parameters.Add(prmDetailID)

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
            Dim errcontroller As New HCPTController
            errcontroller.ErrorLogInsert(150, "GetSiteDetailInformations_DS", strErrMessage, "HCPT_uspKPI_SiteDetailInformation_LD")
        End If

        Return ds
    End Function

    Public Function SiteDetailInformation_IU(ByVal info As KPISiteDetailInfo) As Boolean
        Dim isSucceed As Boolean = True
        Dim strErrMesssage As String = String.Empty
        Command = New SqlCommand("HCPT_uspKPI_SiteDetailInformation_IU", Connection)
        Command.CommandType = CommandType.StoredProcedure
        Dim prmDetailID As New SqlParameter("@detailid", SqlDbType.BigInt)
        Dim prmSector As New SqlParameter("@sector", SqlDbType.Int)
        Dim prmCellID As New SqlParameter("@cellid", SqlDbType.VarChar, 50)
        Dim prmAntennaType As New SqlParameter("@antennatype", SqlDbType.VarChar, 100)
        Dim prmAntennaHeight As New SqlParameter("@antennaheight", SqlDbType.Int)
        Dim prmAzimuth As New SqlParameter("@azimuth", SqlDbType.Int)
        Dim prmMechTilt As New SqlParameter("@mechtilt", SqlDbType.Int)
        Dim prmElecTilt As New SqlParameter("@electilt", SqlDbType.Int)
        Dim prmLMBY As New SqlParameter("@lmby", SqlDbType.Int)
        Dim prmWPID As New SqlParameter("@packageid", SqlDbType.VarChar, 50)

        prmDetailID.Value = info.DetailId
        prmSector.Value = info.Sector
        prmCellID.Value = info.CellId
        prmAntennaType.Value = info.AntennaType
        prmAntennaHeight.Value = info.AntennaHeight
        prmAzimuth.Value = info.Azimuth
        prmMechTilt.Value = info.MechTilt
        prmElecTilt.Value = info.ElecTilt
        prmLMBY.Value = info.CMAInfo.LMBY
        prmWPID.Value = info.SiteInf.PackageId

        Command.Parameters.Add(prmDetailID)
        Command.Parameters.Add(prmSector)
        Command.Parameters.Add(prmCellID)
        Command.Parameters.Add(prmAntennaType)
        Command.Parameters.Add(prmAntennaHeight)
        Command.Parameters.Add(prmAzimuth)
        Command.Parameters.Add(prmMechTilt)
        Command.Parameters.Add(prmElecTilt)
        Command.Parameters.Add(prmLMBY)
        Command.Parameters.Add(prmWPID)

        Try
            Connection.Open()
            Command.ExecuteNonQuery()
        Catch ex As Exception
            isSucceed = False
            strErrMesssage = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try

        If Not String.IsNullOrEmpty(strErrMesssage) Then
            Dim errcontroller As New HCPTController
            errcontroller.ErrorLogInsert(150, "SiteDetailInformation_IU", strErrMesssage, "HCPT_uspKPI_SiteDetailInformation_IU")
        End If

        Return isSucceed
    End Function

    Public Function SiteDetailInformation_D(ByVal detailid As Integer) As Boolean
        Dim isSucceed As Boolean = True
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("HCPT_uspKPI_SiteDetailInformation_Del", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmDetailID As New SqlParameter("@detailid", SqlDbType.BigInt)
        prmDetailID.Value = detailid
        Command.Parameters.Add(prmDetailID)

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
#End Region
  

End Class
