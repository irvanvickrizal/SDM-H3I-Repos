Imports Common
Imports System.Data
Imports BusinessLogic
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.IO
Imports System.IO.StreamWriter
Partial Class RPT_frmSiteReport
    Inherits System.Web.UI.Page
    Dim rpt As New ReportDocument
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Page_size") Is Nothing Then Response.Redirect("~\SessionTimeout.aspx")
        btnReport.Attributes.Add("onclick", "javascript:return checkIsEmpty();")
        If Not IsPostBack Then
            Session("rpt") = Nothing
            trBack.Visible = False
        Else
            If Not Session("rpt") Is Nothing Then
                rpt = Session("rpt")
                CrystalReportViewer1.ReportSource = rpt
            End If
        End If
        txtSSName.Value = hdName.Value
    End Sub
    Protected Sub btnReport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReport.Click
        trSup.Visible = False
        trRpt.Visible = False
        trBack.Visible = True
        Dim con As New SqlConnection(ConfigurationManager.AppSettings("conn"))
        Dim cmd As New SqlCommand()
        cmd.CommandText = "Exec uspRPTSiteList " & hdnSupId.Value
        cmd.Connection = con
        Dim da As New SqlDataAdapter(cmd)
        Dim Conninfo As New ConnectionInfo
        Dim ds As New SiteList
        da.Fill(ds, "SiteList")
        rpt.Load(Server.MapPath("SiteListReport.rpt"))
        Conninfo.ServerName = ConfigurationManager.AppSettings("SName")
        Conninfo.DatabaseName = ConfigurationManager.AppSettings("RDB")
        Conninfo.UserID = ConfigurationManager.AppSettings("RUsrName")
        Conninfo.Password = ConfigurationManager.AppSettings("RUsrPwd")
        SetDBLogonForReport(Conninfo, rpt)
        rpt.SetDataSource(ds)
        Session("rpt") = rpt
        CrystalReportViewer1.ReportSource = rpt
        CrystalReportViewer1.DataBind()
    End Sub
    Public Sub SetDBLogonForReport(ByVal ConnectionInfo As ConnectionInfo, ByVal reportDocument As ReportDocument)
        Dim rpttables As Tables = ReportDocument.Database.Tables
        Dim tab As CrystalDecisions.CrystalReports.Engine.Table
        For Each tab In rpttables
            Dim rpttableslogoninfo As TableLogOnInfo = tab.LogOnInfo
            tab.LogOnInfo.ConnectionInfo = ConnectionInfo
            tab.ApplyLogOnInfo(tab.LogOnInfo)
        Next
    End Sub

    Protected Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Session("rpt") = Nothing
        Response.Redirect("frmSiteReport.aspx")
    End Sub
End Class
