Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Partial Class RPT_frmCODDocReport
    Inherits System.Web.UI.Page
    Dim Conninfo As New ConnectionInfo
    Dim rpt As New ReportDocument
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Page_size") Is Nothing Then
            Response.Redirect("~\SessionTimeout.aspx")
        End If
        If Page.IsPostBack = False Then
            Dim ds As New CODDoc
            Dim con As New SqlConnection(ConfigurationManager.AppSettings("conn"))
            Dim cmd As New SqlCommand("uspCODDocReport", con)
            cmd.CommandType = CommandType.StoredProcedure
            Dim da As New SqlDataAdapter(cmd)
            da.Fill(ds, "CODDocument")
            rpt.Load(Server.MapPath("CODDoc.rpt"))
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
            If Not Session("rpt") Is Nothing Then
                Session("rpt") = rpt
                CrystalReportViewer1.ReportSource = rpt
            End If
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
