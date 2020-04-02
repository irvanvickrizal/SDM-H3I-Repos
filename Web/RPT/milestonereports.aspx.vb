Imports Common
Imports System.Data
Imports BusinessLogic
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.IO
Imports System.IO.StreamWriter
Partial Class RPT_milestonereports
    Inherits System.Web.UI.Page
    Dim objBOD As New BODDLs
    Dim objBO As New BOSiteDocs
    Dim rpt As New ReportDocument

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ' If Session("Page_size") Is Nothing Then Response.Redirect("~\SessionTimeout.aspx")

        'If Not Session("rpt") Is Nothing Then
        '    rpt = Session("rpt")
        '    CrystalReportViewer1.ReportSource = rpt
        'Else
        Dim con As New SqlConnection(ConfigurationManager.AppSettings("conn"))
        Dim Conninfo As New ConnectionInfo
        Dim cmd As New SqlCommand("DashboardReportReports", con)

        cmd.CommandText = "Exec dbo.DashboardReportReports "

        Dim da As New SqlDataAdapter(cmd)
        Dim ds As New Milstone

        da.Fill(ds, "DashboardReportReports")
        rpt.Load(Server.MapPath("Milstonereport.rpt"))
        Conninfo.ServerName = ConfigurationManager.AppSettings("SName")
        Conninfo.DatabaseName = ConfigurationManager.AppSettings("RDB")
        Conninfo.UserID = ConfigurationManager.AppSettings("RUsrName")
        Conninfo.Password = ConfigurationManager.AppSettings("RUsrPwd")
        SetDBLogonForReport(Conninfo, rpt)
        rpt.SetDataSource(ds)
        Session("rpt") = rpt
        CrystalReportViewer1.ReportSource = rpt
        CrystalReportViewer1.DataBind()
        'End If
    End Sub

    Public Sub SetDBLogonForReport(ByVal ConnectionInfo As ConnectionInfo, ByVal reportDocument As ReportDocument)
        Dim rpttables As Tables = reportDocument.Database.Tables
        Dim tab As CrystalDecisions.CrystalReports.Engine.Table
        For Each tab In rpttables
            Dim rpttableslogoninfo As TableLogOnInfo = tab.LogOnInfo
            tab.LogOnInfo.ConnectionInfo = ConnectionInfo
            tab.ApplyLogOnInfo(tab.LogOnInfo)
        Next
    End Sub
End Class