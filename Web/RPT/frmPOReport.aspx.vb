Imports Common
Imports System.Data
Imports BusinessLogic
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.IO
Imports System.IO.StreamWriter
Partial Class RPT_frmPOReport
    Inherits System.Web.UI.Page
    Dim objBOD As New BODDLs
    Dim objBO As New BOSiteDocs
    Dim rpt As New ReportDocument
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Page_size") Is Nothing Then Response.Redirect("~\SessionTimeout.aspx")
        btnReport.Attributes.Add("onclick", "javascript:return checkIsEmpty();")
        If Page.IsPostBack = False Then
            Session("rpt") = Nothing
            objBOD.fillDDL(ddlPO, "PoNo", True, Constants._DDL_Default_Select)
            trBack.Visible = False
        Else
            If Not Session("rpt") Is Nothing Then
                rpt = Session("rpt")
                CrystalReportViewer1.ReportSource = rpt
            End If
        End If
    End Sub
    Protected Sub ddlPO_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPO.SelectedIndexChanged
        If ddlPO.SelectedIndex > 0 Then
            Dim lvlcode As String = Session("lvlcode")
            Dim area, region, zone, site As Integer
            area = Session("Area_Id")
            region = Session("Region_Id")
            zone = Session("Zone_Id")
            site = Session("Site_Id")
            Dim ddldt As DataTable
            ddldt = objBO.uspDDLPOSiteNoByUser2(ddlPO.SelectedItem.Value, lvlcode, area, region, zone, site, Session("User_Id"))
            If ddldt.Rows.Count > 0 Then
                ddlSite.DataSource = ddldt
                ddlSite.DataTextField = "txt"
                ddlSite.DataValueField = "VAL"
                ddlSite.DataBind()
                ddlSite.Items.Insert(0, "--All--")
            End If
        End If
    End Sub

    Protected Sub btnReport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReport.Click
        Dim con As New SqlConnection(ConfigurationManager.AppSettings("conn"))
        Dim Conninfo As New ConnectionInfo
        Dim cmd As New SqlCommand("uspRPTPO", con)
        trPono.Visible = False
        trSiteno.Visible = False
        trBtn.Visible = False
        trBack.Visible = True
        If ddlSite.SelectedIndex = 0 Then
            cmd.CommandText = "Exec uspRPTPO '" & ddlPO.SelectedItem.Text & "'"
            Dim da As New SqlDataAdapter(cmd)
            Dim ds As New POReport
            da.Fill(ds, "POReport")
            rpt.Load(Server.MapPath("POReport.rpt"))
            Conninfo.ServerName = ConfigurationManager.AppSettings("SName")
            Conninfo.DatabaseName = ConfigurationManager.AppSettings("RDB")
            Conninfo.UserID = ConfigurationManager.AppSettings("RUsrName")
            Conninfo.Password = ConfigurationManager.AppSettings("RUsrPwd")
            SetDBLogonForReport(Conninfo, rpt)
            rpt.SetDataSource(ds)
            Session("rpt") = rpt
            CrystalReportViewer1.ReportSource = rpt
            CrystalReportViewer1.DataBind()
        Else
            Dim stval() As String = ddlSite.SelectedItem.Text.Split("-")

            cmd.CommandText = "Exec uspRPTPO '" & ddlPO.SelectedItem.Text & "','" & stval(0) & "','" & stval(1) & "'"
            Dim da As New SqlDataAdapter(cmd)
            Dim ds As New SiteReport
            da.Fill(ds, "SiteReport")
            rpt.Load(Server.MapPath("SiteReport.rpt"))
            Conninfo.ServerName = ConfigurationManager.AppSettings("SName")
            Conninfo.DatabaseName = ConfigurationManager.AppSettings("RDB")
            Conninfo.UserID = ConfigurationManager.AppSettings("RUsrName")
            Conninfo.Password = ConfigurationManager.AppSettings("RUsrPwd")
            SetDBLogonForReport(Conninfo, rpt)
            rpt.SetDataSource(ds)
            Session("rpt") = rpt
            CrystalReportViewer1.ReportSource = rpt
            CrystalReportViewer1.DataBind()
        End If
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
        Response.Redirect("frmPOReport.aspx")
    End Sub
End Class
