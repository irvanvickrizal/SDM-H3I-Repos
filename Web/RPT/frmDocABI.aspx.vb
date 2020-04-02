Imports Common
Imports System.Data
Imports BusinessLogic
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.Data.SqlClient
Imports System.Configuration

Partial Class RPT_frmDocABI
    Inherits System.Web.UI.Page
    Dim rpt As New ReportDocument

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Page_size") Is Nothing Then Response.Redirect("~\SessionTimeout.aspx")
        If Not IsPostBack Then
            Session("rpt") = Nothing
            Dim con As New SqlConnection(ConfigurationManager.AppSettings("conn"))
            Dim Conninfo As New ConnectionInfo
            Dim cmd As New SqlCommand("uspRPTDocABI", con)
            cmd.CommandText = "Exec uspRPTDocABI "
            Dim da As New SqlDataAdapter(cmd)
            Dim ds As New EPMReport
            da.Fill(ds, "DocABI")
            rpt.Load(Server.MapPath("DOCABI.rpt"))
            Conninfo.ServerName = ConfigurationManager.AppSettings("SName")
            Conninfo.DatabaseName = ConfigurationManager.AppSettings("RDB")
            Conninfo.UserID = ConfigurationManager.AppSettings("RUsrName")
            Conninfo.Password = ConfigurationManager.AppSettings("RUsrPwd")
            SetDBLogonForReport(Conninfo, rpt)
            rpt.SetDataSource(ds)
            Session("rpt") = rpt
            CrystalReportViewer1.ReportSource = rpt
            CrystalReportViewer1.DataBind()
            rpt.Refresh()
        Else

            rpt = Session("rpt")
            CrystalReportViewer1.ReportSource = rpt
            rpt.Refresh()
        End If
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
