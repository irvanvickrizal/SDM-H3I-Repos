Imports Common
Imports System.Data
Imports BusinessLogic
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.IO
Imports System.IO.StreamWriter

Partial Class RPT_frmDocReport
    Inherits System.Web.UI.Page
    Dim objBOD As New BODDLs
    Dim dt As New DataTable
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

    Protected Sub btnReport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReport.Click
        trPono.Visible = False
        trRpt.Visible = False
        trBack.Visible = True
        Dim con As New SqlConnection(ConfigurationManager.AppSettings("conn"))
        Dim Conninfo As New ConnectionInfo
        Dim cmd As New SqlCommand() '"uspRPTDoc", con)
        cmd.CommandText = "Exec uspRPTDoc '" & ddlPO.SelectedItem.Text & "','d'"
        cmd.Connection = con
        Dim da As New SqlDataAdapter(cmd)
        Dim ds As New DocReport
        da.Fill(ds, "DocReport")
        dt.Columns.Add("pono", Type.GetType("System.String"))
        dt.Columns.Add("siteno", Type.GetType("System.String"))
        dt.Columns.Add("docname", Type.GetType("System.String"))
        dt.Columns.Add("UUser", Type.GetType("System.String"))
        dt.Columns.Add("UType", Type.GetType("System.String"))
        dt.Columns.Add("URole", Type.GetType("System.String"))
        dt.Columns.Add("UDate", Type.GetType("System.String"))
        dt.Columns.Add("RUser", Type.GetType("System.String"))
        dt.Columns.Add("RType", Type.GetType("System.String"))
        dt.Columns.Add("RRole", Type.GetType("System.String"))
        dt.Columns.Add("RDate", Type.GetType("System.String"))
        dt.Columns.Add("AUser", Type.GetType("System.String"))
        dt.Columns.Add("AType", Type.GetType("System.String"))
        dt.Columns.Add("ARole", Type.GetType("System.String"))
        dt.Columns.Add("ADate", Type.GetType("System.String"))
        dt.Columns.Add("IntegrationDate", Type.GetType("System.String"))
        dt.Columns.Add("wfid", Type.GetType("System.Int32"))
        dt.Columns.Add("fldtype", Type.GetType("System.String"))
        For Each drow As DataRow In ds.Tables("DocReport").Rows
            Dim myrow As DataRow
            myrow = dt.NewRow
            myrow("pono") = drow.Item("pono")
            myrow("siteno") = drow.Item("siteno")
            'myrow("fldtype") = drow.Item("fldtype")
            If IsDBNull(drow.Item("siteintegration")) Then
                myrow("IntegrationDate") = "01 Jan 1900"
            Else
                myrow("IntegrationDate") = drow.Item("siteintegration")
            End If
            myrow("docname") = drow.Item("docname")
            If myrow("docname") = "" Then
                myrow("wfid") = 0
            Else
                myrow("wfid") = drow.Item("wfid")
            End If
            Dim strUploader As String = drow.Item("Uploader")
            If Len(strUploader) > 0 Then
                Dim Uploader() As String = Split(strUploader, "^^^^^^", )
                myrow("UUser") = Uploader(0)
                myrow("UType") = Uploader(1)
                myrow("URole") = Uploader(2)
                myrow("UDate") = Uploader(3)
            Else
                myrow("UDate") = "01 Jan 1900"
            End If
            Dim strReviewer As String = drow.Item("Reviewer")
            If Len(strReviewer) > 0 Then
                Dim Reviewer() As String = Split(strReviewer, "^^^^^^", )
                myrow("RUser") = Reviewer(0)
                myrow("RType") = Reviewer(1)
                myrow("RRole") = Reviewer(2)
                myrow("RDate") = Reviewer(3)
            Else
                myrow("RDate") = "01 Jan 1900"
            End If
            Dim strApprover As String = drow.Item("Approver")
            If Len(strApprover) > 0 Then
                Dim Approver() As String = Split(strApprover, "^^^^^^", )
                myrow("AUser") = Approver(0)
                myrow("AType") = Approver(1)
                myrow("ARole") = Approver(2)
                myrow("ADate") = Approver(3)
            Else
                myrow("ADate") = "01 Jan 1900"
            End If
            If myrow("wfid") = 0 And myrow("UDate") <> "01 Jan 1900" Then
                myrow("RDate") = myrow("UDate")
                myrow("ADate") = myrow("UDate")
            End If
            If myrow("wfid") = 0 Then
                myrow("RUser") = myrow("UUser")
                myrow("AUser") = myrow("UUser")
            End If
            dt.Rows.Add(myrow)
        Next
        Try
            rpt.Load(Server.MapPath("DocReport.rpt"))
        Catch ex As Exception
            Response.Write(ex.Message)
            Response.Write(ex.InnerException)
        End Try
        'If dt.Rows.Count = 0 Then
        '    Response.Write("No Record Found")
        '    Return
        'End If
        Conninfo.ServerName = ConfigurationManager.AppSettings("SName")
        Conninfo.DatabaseName = ConfigurationManager.AppSettings("RDB")
        Conninfo.UserID = ConfigurationManager.AppSettings("RUsrName")
        Conninfo.Password = ConfigurationManager.AppSettings("RUsrPwd")
        SetDBLogonForReport(Conninfo, rpt)
        rpt.SetDataSource(dt)
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
        Response.Redirect("frmDocReport.aspx")
    End Sub
End Class
