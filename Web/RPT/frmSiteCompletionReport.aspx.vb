Imports System
Imports Common
Imports System.Data
Imports BusinessLogic
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.Data.SqlClient
Partial Class frmSiteCompletionReport
    Inherits System.Web.UI.Page
    Dim objBo As New BOSiteDocs
    Dim objbod As New BODDLs
    Dim rpt As New ReportDocument
    Dim cst As New Constants
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Page_size") Is Nothing Then
            Response.Redirect("~\SessionTimeout.aspx")
        End If
        btnReport.Attributes.Add("onClick", "javascript:return checkIsEmpty();")
        btnSiteCreateFDate.Attributes.Add("onclick", "javascript:popUpCalendar(this,txtSiteCreateF,'dd/mm/yyyy');return false;")
        btnSiteCreateTDate.Attributes.Add("onclick", "javascript:popUpCalendar(this,txtSiteCreateT,'dd/mm/yyyy');return false;")
        btnSiteChkFDate.Attributes.Add("onclick", "javascript:popUpCalendar(this,txtDocChkF,'dd/mm/yyyy');return false;")
        btnSiteChkTDate.Attributes.Add("onclick", "javascript:popUpCalendar(this,txtDocChkT,'dd/mm/yyyy');return false;")
        btnSiteIntFDate.Attributes.Add("onclick", "javascript:popUpCalendar(this,txtSiteIntF,'dd/mm/yyyy');return false;")
        btnSiteIntTDate.Attributes.Add("onclick", "javascript:popUpCalendar(this,txtSiteIntT,'dd/mm/yyyy');return false;")
        If Page.IsPostBack = False Then
            Session("rpt") = Nothing
            objbod.fillDDL(ddlPO, "PoNo1", False, "")
            ddlPO_SelectedIndexChanged(Nothing, Nothing)
            btnBack.Visible = False
        Else
            If Not Session("rpt") Is Nothing Then
                rpt = Session("rpt")
                CrystalReportViewer1.ReportSource = rpt
            End If
        End If
    End Sub
    Protected Sub btnReport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReport.Click
        porpt.Visible = False
        siterpt.Visible = False
        btnReport.Visible = False
        title.Visible = False
        siteselect.Visible = False
        sitedocselect.Visible = False
        siteintselect.Visible = False
        btnBack.Visible = True
        Dim Conninfo As New ConnectionInfo
        Dim ds As New SiteCMP
        Dim con As New SqlConnection(ConfigurationManager.AppSettings("conn"))
        Dim cmd As New SqlCommand("uspRPT", con)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("PONo", SqlDbType.VarChar).Value = ddlPO.SelectedItem.Text
        Dim stval() As String = ddlSite.SelectedItem.Text.Split("-")

        If ddlSite.SelectedIndex <> 0 Then cmd.Parameters.Add("Site_No", SqlDbType.VarChar).Value = stval(0)
        If txtSiteCreateF.Value <> "" Then cmd.Parameters.Add("SiteCreateFdate", SqlDbType.VarChar).Value = cst.formatDDMMYYYY(txtSiteCreateF.Value)
        If txtSiteCreateT.Value <> "" Then cmd.Parameters.Add("SiteCreateTdate", SqlDbType.VarChar).Value = cst.formatDDMMYYYY(txtSiteCreateT.Value)
        If txtDocChkF.Value <> "" Then cmd.Parameters.Add("DocChkFdate", SqlDbType.VarChar).Value = cst.formatDDMMYYYY(txtDocChkF.Value)
        If txtDocChkT.Value <> "" Then cmd.Parameters.Add("DocChkTdate", SqlDbType.VarChar).Value = cst.formatDDMMYYYY(txtDocChkT.Value)
        If txtSiteIntF.Value <> "" Then cmd.Parameters.Add("SiteIntFdate", SqlDbType.VarChar).Value = cst.formatDDMMYYYY(txtSiteIntF.Value)
        If txtSiteIntT.Value <> "" Then cmd.Parameters.Add("SiteIntTdate", SqlDbType.VarChar).Value = cst.formatDDMMYYYY(txtSiteIntT.Value)
        Dim da As New SqlDataAdapter(cmd)
        da.Fill(ds, "RPTSiteCMPDetails")
        rpt.Load(Server.MapPath("SiteActivity.rpt"))
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
    Protected Sub ddlPO_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPO.SelectedIndexChanged
        If ddlPO.SelectedIndex >= 0 Then
            'here  we have to show only sites for which BAST process not finished.
            Dim lvlcode As String = Session("lvlcode")
            Dim area, region, zone, site As Integer
            area = Session("Area_Id")
            region = Session("Region_Id")
            zone = Session("Zone_Id")
            site = Session("Site_Id")
            Dim ddldt As DataTable
            ddldt = objBo.uspDDLPOSiteNoByUser3(ddlPO.SelectedItem.Value, lvlcode, area, region, zone, site, Session("User_Id"))
            If ddldt.Rows.Count > 0 Then
                ddlSite.DataSource = ddldt
                ddlSite.DataTextField = "txt"
                ddlSite.DataValueField = "VAL"
                ddlSite.DataBind()
                ddlSite.Items.Insert(0, "--All--")
                'ddlSite_SelectedIndexChanged(Nothing, Nothing)
            End If
        End If
    End Sub
    Protected Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Session("rpt") = Nothing
        Response.Redirect("frmSiteCompletionReport.aspx")
    End Sub
End Class
