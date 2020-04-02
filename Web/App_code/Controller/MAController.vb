Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections.Generic

Public Class MAController
    Inherits BaseController

    Public Function GetAllMultipleApprovalProcess() As List(Of MAPipelineInfo)
        Dim list As New List(Of MAPipelineInfo)
        Command = New SqlCommand("uspGetAllPendingDocMultipleApproval_BGProcess_Admin", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read()
                    Dim info As New MAPipelineInfo
                    info.SNO = Convert.ToInt32(DataReader.Item("sno"))
                    info.PONO = Convert.ToString(DataReader.Item("PONO"))
                    info.SiteId = Convert.ToInt32(DataReader.Item("site_id"))
                    info.SiteVersion = Integer.Parse(DataReader.Item("SiteVersion").ToString())
                    info.SiteName = Convert.ToString(DataReader.Item("SiteName"))
                    info.SiteNo = Convert.ToString(DataReader.Item("SiteNo"))
                    info.PackageId = Convert.ToString(DataReader.Item("WorkPackageId"))
                    info.Scope = Convert.ToString(DataReader.Item("Scope"))
                    info.DocPath = Convert.ToString(DataReader.Item("DocPath"))
                    info.OrgDocPath = Convert.ToString(DataReader.Item("OrgDocPath"))
                    info.DocId = Integer.Parse(DataReader.Item("docid").ToString())
                    info.DocName = Convert.ToString(DataReader.Item("DocName"))
                    info.SWID = Convert.ToInt32(DataReader.Item("SW_ID"))
                    info.XVal = Integer.Parse(DataReader.Item("xval").ToString())
                    info.YVal = Integer.Parse(DataReader.Item("yval").ToString())
                    info.PageNo = Integer.Parse(DataReader.Item("pageno").ToString())
                    info.StartMultipleDate = Convert.ToDateTime(DataReader.Item("multipleapprovaldate"))
                    info.USERID = Integer.Parse(DataReader.Item("userapprovedid").ToString())
                    info.RoleId = Integer.Parse(DataReader.Item("Roleid").ToString())
                    info.UserLogin = Convert.ToString(DataReader.Item("usrLogin"))
                    info.UserName = Convert.ToString(DataReader.Item("username"))
                    info.SubmitDate = Convert.ToDateTime(DataReader.Item("SubmitDate"))
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
