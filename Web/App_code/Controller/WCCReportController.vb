Imports Microsoft.VisualBasic
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient

Public Class WCCReportController
    Inherits BaseController

    Public Function GetWCCDone(ByVal starttime As System.Nullable(Of DateTime), ByVal endtime As System.Nullable(Of DateTime), ByVal userid As Integer) As List(Of WCCReportInfo)
        Dim list As New List(Of WCCReportInfo)
        Command = New SqlCommand("uspWCC_GetWCCDone_Report", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmUserId As New SqlParameter("@usrid", SqlDbType.Int)
        Dim prmStartTime As New SqlParameter("@starttime", SqlDbType.DateTime)
        Dim prmEndTime As New SqlParameter("@endTime", SqlDbType.DateTime)

        prmUserId.Value = userid
        prmStartTime.Value = starttime
        prmEndTime.Value = endtime

        Command.Parameters.Add(prmUserId)
        Command.Parameters.Add(prmStartTime)
        Command.Parameters.Add(prmEndTime)

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read()
                    Dim info As New WCCReportInfo
                    If Not String.IsNullOrEmpty(Convert.ToString(DataReader.Item("ApprovalDate"))) Then
                        info.ApproverDate = Convert.ToDateTime(DataReader.Item("ApprovalDate"))
                    End If
                    info.ApproverName = Convert.ToString(DataReader.Item("approver"))
                    info.CertificateNo = Convert.ToString(DataReader.Item("certificate_number"))
                    info.CompanyName = Convert.ToString(DataReader.Item("scon_name"))
                    info.ScopeName = Convert.ToString(DataReader.Item("dscope_name"))
                    If Not String.IsNullOrEmpty(Convert.ToString(DataReader.Item("issuance_date"))) Then
                        info.IssuanceDate = Convert.ToDateTime(DataReader.Item("Issuance_Date"))
                    End If
                    info.PackageId = Convert.ToString(DataReader.Item("workpackageid"))
                    info.PONO = Convert.ToString(DataReader.Item("PONO"))
                    info.POSubcon = Convert.ToString(DataReader.Item("PO_Subcontractor"))
                    info.SiteName = Convert.ToString(DataReader.Item("sitename"))
                    info.SiteNo = Convert.ToString(DataReader.Item("SiteNo"))
                    info.SubconName = Convert.ToString(DataReader.Item("InitiatorName"))

                    If Not String.IsNullOrEmpty(Convert.ToString(DataReader.Item("submitdate"))) Then
                        info.SubmitDate = Convert.ToDateTime(DataReader.Item("SubmitDate"))
                    End If
                    info.WCCID = Convert.ToInt32(DataReader.Item("wcc_id"))
                    info.ActivityName = Convert.ToString(DataReader.Item("Activity_name"))
                    info.RejectionName = String.Empty
                    info.OnTaskPendingName = String.Empty
                    info.WCCStatus = "WCC Done"
                    info.RegionName = Convert.ToString(DataReader.Item("RgnName"))
                    info.AreaName = Convert.ToString(DataReader.Item("AreaName"))
                    info.RejectionRemarks = String.Empty
                    info.RejectionCategory = String.Empty
                    list.Add(info)
                End While
            End If
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try

        Return list
    End Function

    Public Function GetWCCOnTaskPending(ByVal starttime As System.Nullable(Of DateTime), ByVal endtime As System.Nullable(Of DateTime), ByVal userid As Integer) As List(Of WCCReportInfo)
        Dim list As New List(Of WCCReportInfo)
        Command = New SqlCommand("uspWCC_UserUnderSignature_Report", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmUserId As New SqlParameter("@usrid", SqlDbType.Int)
        Dim prmStartTime As New SqlParameter("@starttime", SqlDbType.DateTime)
        Dim prmEndTime As New SqlParameter("@endTime", SqlDbType.DateTime)

        prmUserId.Value = userid
        prmStartTime.Value = starttime
        prmEndTime.Value = endtime

        Command.Parameters.Add(prmUserId)
        Command.Parameters.Add(prmStartTime)
        Command.Parameters.Add(prmEndTime)

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read()
                    Dim info As New WCCReportInfo
                    info.ApproverName = String.Empty
                    info.CompanyName = Convert.ToString(DataReader.Item("scon_name"))
                    info.ScopeName = Convert.ToString(DataReader.Item("dscope_name"))
                    info.PackageId = Convert.ToString(DataReader.Item("workpackageid"))
                    info.PONO = Convert.ToString(DataReader.Item("PONO"))
                    info.POSubcon = Convert.ToString(DataReader.Item("PO_Subcontractor"))
                    info.SiteName = Convert.ToString(DataReader.Item("sitename"))
                    info.SiteNo = Convert.ToString(DataReader.Item("SiteNo"))
                    info.SubconName = Convert.ToString(DataReader.Item("InitiatorName"))
                    If Not String.IsNullOrEmpty(Convert.ToString(DataReader.Item("submitdate"))) Then
                        info.SubmitDate = Convert.ToDateTime(DataReader.Item("SubmitDate"))
                    End If
                    info.WCCID = Convert.ToInt32(DataReader.Item("wcc_id"))
                    info.ActivityName = Convert.ToString(DataReader.Item("Activity_name"))
                    info.RejectionName = String.Empty
                    info.OnTaskPendingName = Convert.ToString(DataReader.Item("UserLocation"))
                    info.WCCStatus = "WCC On Task Pending"
                    info.RegionName = Convert.ToString(DataReader.Item("RgnName"))
                    info.AreaName = Convert.ToString(DataReader.Item("AreaName"))
                    info.RejectionRemarks = String.Empty
                    info.RejectionCategory = String.Empty
                    list.Add(info)
                End While
            End If
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try

        Return list
    End Function

    Public Function GetWCCPreparation(ByVal starttime As System.Nullable(Of DateTime), ByVal endtime As System.Nullable(Of DateTime), ByVal userid As Integer) As List(Of WCCReportInfo)
        Dim list As New List(Of WCCReportInfo)
        Command = New SqlCommand("uspWCC_GetWCCPreparation_Report", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmUserId As New SqlParameter("@usrid", SqlDbType.Int)
        Dim prmStartTime As New SqlParameter("@starttime", SqlDbType.DateTime)
        Dim prmEndTime As New SqlParameter("@endTime", SqlDbType.DateTime)

        prmUserId.Value = userid
        prmStartTime.Value = starttime
        prmEndTime.Value = endtime

        Command.Parameters.Add(prmUserId)
        Command.Parameters.Add(prmStartTime)
        Command.Parameters.Add(prmEndTime)

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read()
                    Dim info As New WCCReportInfo
                    info.ApproverName = String.Empty
                    info.CertificateNo = Convert.ToString(DataReader.Item("certificate_number"))
                    info.CompanyName = Convert.ToString(DataReader.Item("scon_name"))
                    info.ScopeName = Convert.ToString(DataReader.Item("dscope_name"))
                    info.PackageId = Convert.ToString(DataReader.Item("workpackageid"))
                    info.PONO = Convert.ToString(DataReader.Item("PONO"))
                    info.POSubcon = Convert.ToString(DataReader.Item("PO_Subcontractor"))
                    info.SiteName = Convert.ToString(DataReader.Item("sitename"))
                    info.SiteNo = Convert.ToString(DataReader.Item("SiteNo"))
                    info.SubconName = Convert.ToString(DataReader.Item("InitiatorName"))

                    If Not String.IsNullOrEmpty(Convert.ToString(DataReader.Item("submitdate"))) Then
                        info.SubmitDate = Convert.ToDateTime(DataReader.Item("SubmitDate"))
                    End If
                    info.WCCID = Convert.ToInt32(DataReader.Item("wcc_id"))
                    info.ActivityName = Convert.ToString(DataReader.Item("Activity_name"))
                    info.RejectionName = String.Empty
                    info.OnTaskPendingName = String.Empty
                    info.WCCStatus = Convert.ToString(DataReader.Item("WCCStatus"))
                    info.RegionName = Convert.ToString(DataReader.Item("RgnName"))
                    info.AreaName = Convert.ToString(DataReader.Item("AreaName"))
                    info.RejectionRemarks = String.Empty
                    info.RejectionCategory = String.Empty
                    list.Add(info)
                End While
            End If
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try

        Return list
    End Function

    Public Function GetWCCRejection(ByVal starttime As System.Nullable(Of DateTime), ByVal endtime As System.Nullable(Of DateTime), ByVal userid As Integer) As List(Of WCCReportInfo)
        Dim list As New List(Of WCCReportInfo)
        Command = New SqlCommand("uspWCC_GetDocumentRejection_Report", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmUserId As New SqlParameter("@usrid", SqlDbType.Int)
        Dim prmStartTime As New SqlParameter("@starttime", SqlDbType.DateTime)
        Dim prmEndTime As New SqlParameter("@endTime", SqlDbType.DateTime)

        prmUserId.Value = userid
        prmStartTime.Value = starttime
        prmEndTime.Value = endtime

        Command.Parameters.Add(prmUserId)
        Command.Parameters.Add(prmStartTime)
        Command.Parameters.Add(prmEndTime)

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read()
                    Dim info As New WCCReportInfo
                    info.ApproverName = String.Empty
                    info.CompanyName = Convert.ToString(DataReader.Item("scon_name"))
                    info.ScopeName = Convert.ToString(DataReader.Item("dscope_name"))
                    info.PackageId = Convert.ToString(DataReader.Item("package_id"))
                    info.PONO = Convert.ToString(DataReader.Item("PONO"))
                    info.POSubcon = Convert.ToString(DataReader.Item("PO_Subcontractor"))
                    info.SiteName = Convert.ToString(DataReader.Item("sitename"))
                    info.SiteNo = Convert.ToString(DataReader.Item("SiteNo"))
                    info.SubconName = Convert.ToString(DataReader.Item("Name"))

                    If Not String.IsNullOrEmpty(Convert.ToString(DataReader.Item("submitdate"))) Then
                        info.SubmitDate = Convert.ToDateTime(DataReader.Item("SubmitDate"))
                    End If
                    info.WCCID = Convert.ToInt32(DataReader.Item("wcc_id"))
                    info.ActivityName = Convert.ToString(DataReader.Item("Activity_name"))
                    info.RejectionName = Convert.ToString(DataReader.Item("RejectionUser"))
                    If Not String.IsNullOrEmpty(Convert.ToString(DataReader.Item("RejectionDate"))) Then
                        info.RejectionDate = Convert.ToDateTime(DataReader.Item("RejectionDate"))
                    End If
                    info.OnTaskPendingName = String.Empty
                    info.WCCStatus = Convert.ToString(DataReader.Item("wccstatus"))
                    info.RegionName = Convert.ToString(DataReader.Item("RgnName"))
                    info.AreaName = Convert.ToString(DataReader.Item("AreaName"))
                    info.RejectionRemarks = Convert.ToString(DataReader.Item("Remarks"))
                    info.RejectionCategory = String.Empty
                    list.Add(info)
                End While
            End If
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try

        Return list
    End Function

    Public Function GetWCCHistoricalRejection(ByVal starttime As System.Nullable(Of DateTime), ByVal endtime As System.Nullable(Of DateTime), ByVal userid As Integer) As List(Of WCCReportInfo)
        Dim list As New List(Of WCCReportInfo)
        Command = New SqlCommand("uspWCC_GetHistoricalRejection_Report", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmUserId As New SqlParameter("@usrid", SqlDbType.Int)
        Dim prmStartTime As New SqlParameter("@starttime", SqlDbType.DateTime)
        Dim prmEndTime As New SqlParameter("@endTime", SqlDbType.DateTime)

        prmUserId.Value = userid
        prmStartTime.Value = starttime
        prmEndTime.Value = endtime

        Command.Parameters.Add(prmUserId)
        Command.Parameters.Add(prmStartTime)
        Command.Parameters.Add(prmEndTime)

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read()
                    Dim info As New WCCReportInfo
                    info.ApproverName = String.Empty
                    info.CompanyName = Convert.ToString(DataReader.Item("scon_name"))
                    info.ScopeName = Convert.ToString(DataReader.Item("dscope_name"))
                    info.PackageId = Convert.ToString(DataReader.Item("workpackageid"))
                    info.PONO = Convert.ToString(DataReader.Item("PONO"))
                    info.POSubcon = Convert.ToString(DataReader.Item("PO_Subcontractor"))
                    info.SiteName = Convert.ToString(DataReader.Item("sitename"))
                    info.SiteNo = Convert.ToString(DataReader.Item("SiteNo"))
                    info.SubconName = Convert.ToString(DataReader.Item("InitiatorName"))

                    If Not String.IsNullOrEmpty(Convert.ToString(DataReader.Item("submitdate"))) Then
                        info.SubmitDate = Convert.ToDateTime(DataReader.Item("SubmitDate"))
                    End If
                    info.WCCID = Convert.ToInt32(DataReader.Item("wcc_id"))
                    info.ActivityName = Convert.ToString(DataReader.Item("Activity_name"))
                    info.RejectionName = Convert.ToString(DataReader.Item("RejectionName"))
                    If Not String.IsNullOrEmpty(Convert.ToString(DataReader.Item("RejectionDate"))) Then
                        info.RejectionDate = Convert.ToDateTime(DataReader.Item("RejectionDate"))
                    End If
                    info.ReUploadName = Convert.ToString(DataReader.Item("UploadName"))
                    If Not String.IsNullOrEmpty(Convert.ToString(DataReader.Item("ReUploadDate"))) Then
                        info.ReUploadDate = Convert.ToDateTime(DataReader.Item("ReuploadDate"))
                    End If
                    info.OnTaskPendingName = String.Empty
                    info.WCCStatus = "Rejection Log"
                    info.RegionName = Convert.ToString(DataReader.Item("RgnName"))
                    info.AreaName = Convert.ToString(DataReader.Item("AreaName"))
                    info.RejectionRemarks = Convert.ToString(DataReader.Item("Remarks"))
                    info.RejectionCategory = Convert.ToString(DataReader.Item("categories"))
                    list.Add(info)
                End While
            End If
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try

        Return list
    End Function

End Class
