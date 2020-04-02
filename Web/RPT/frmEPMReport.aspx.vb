Imports Common
Imports System.Data
Imports BusinessLogic
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.Data.SqlClient
Imports System.Configuration
Partial Class RPT_frmEPMReport
    Inherits System.Web.UI.Page
    Dim objBOD As New BODDLs
    Dim rpt As New ReportDocument
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Page_size") Is Nothing Then Response.Redirect("~\SessionTimeout.aspx")
        btnReport.Attributes.Add("onclick", "javascript:return checkIsEmpty();")
        If Page.IsPostBack = False Then
            Session("rpt") = Nothing
            objBOD.fillDDL(ddlPO, "PODetails", True, Constants._DDL_Default_Select)
            trBack.Visible = False
        Else
            If Not Session("rpt") Is Nothing Then
                rpt = Session("rpt")
                CrystalReportViewer1.ReportSource = rpt
            End If
        End If
    End Sub
    Protected Sub ddlPO_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPO.SelectedIndexChanged
        ddlSite.Items.Clear()
        Dim dt As New DataTable
        Dim objd As New DBUtil
        dt = objd.ExeQueryDT("SELECT DISTINCT convert(varchar(10),B.Site_ID)+ '-'+ convert(varchar(20),a.PO_Id) AS [VAL], A.SiteNo + '-'+ A.FldType AS [txt] FROM   PODetails  AS A   " & _
                     "INNER JOIN CODSite AS B ON A.SiteNo = B.Site_No WHERE A.RStatus = 2 AND A.Status='Active' and  A.PoNo = '" & ddlPO.SelectedItem.Text & "' ORDER BY [txt]", "chklist")
        ddlSite.DataSource = dt
        ddlSite.DataTextField = "txt"
        ddlSite.DataValueField = "val"
        ddlSite.DataBind()
        ddlSite.Items.Insert(0, "--Select--")
        ddlSite.Items(0).Value = 0
    End Sub
    Protected Sub btnReport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReport.Click
        trPono.Visible = False
        trSiteno.Visible = False
        trRpt.Visible = False
        trBack.Visible = True
        Dim con As New SqlConnection(ConfigurationManager.AppSettings("conn"))
        Dim Conninfo As New ConnectionInfo
        Dim stval() As String = ddlSite.SelectedItem.Text.Split("-")
        Dim cmd As New SqlCommand("Exec uspRPTEPM '" & ddlPO.SelectedItem.Text & "','" & stval(0) & "','" & stval(1) & "'", con)
        Dim da As New SqlDataAdapter(cmd)
        Dim ds As New EPMReport
        da.Fill(ds, "EPMReport")
        If ds.Tables(0).Rows(0).Item("momrefno").ToString = "@@" Then
            rpt.Load(Server.MapPath("EPMReport.rpt"))
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
            da.Fill(ds, "EPMReport")
            rpt.Load(Server.MapPath("EPMReport1.rpt"))
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
        Response.Redirect("frmEPMReport.aspx")
    End Sub
End Class
