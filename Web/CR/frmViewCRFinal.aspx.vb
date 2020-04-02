Imports CRFramework
Imports Common
Imports System.Data
Imports System.Collections.Generic
Imports System.IO

Partial Class CR_frmViewCRFinal
    Inherits System.Web.UI.Page
    Dim controller As New CRController
    Dim objdb As New DBUtil
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            BindSiteAtt()
            GetPathDocument()
            BindListCRApproved()
            GetCRLogHistorical()
        End If
    End Sub

    Protected Sub BtnExportListCRApprovedClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnExportListCRApproved.Click
        If (GvListCR.Rows.Count() > 0) Then
            Dim tw As New StringWriter()
            Dim strFilename As String = "ListCRApproved_" + DateTime.Now.ToShortDateString + ".xls"
            Dim hw As New System.Web.UI.HtmlTextWriter(tw)
            Dim frm As HtmlForm = New HtmlForm()
            Response.ContentType = "application/vnd.ms-excel"
            Response.AddHeader("content-disposition", "attachment;filename=" + strFilename)
            Response.Charset = ""
            EnableViewState = False
            Controls.Add(frm)
            frm.Controls.Add(GvListCR)
            frm.RenderControl(hw)
            Response.Write(tw.ToString())
            Response.End()
        Else
            If (Not ClientScript.IsStartupScriptRegistered("alert")) Then
                Page.ClientScript.RegisterStartupScript _
                (Me.GetType(), "alert", "invalidExportToExcel();", True)
            End If
        End If
    End Sub

    Protected Sub BtnCRLogExportClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnCRLogExport.Click
        If (GvSummaryViewLog.Rows.Count() > 0) Then
            Dim tw As New StringWriter()
            Dim strFilename As String = "HistoricalCRLog_" + DateTime.Now.ToShortDateString + ".xls"
            Dim hw As New System.Web.UI.HtmlTextWriter(tw)
            Dim frm As HtmlForm = New HtmlForm()
            Response.ContentType = "application/vnd.ms-excel"
            Response.AddHeader("content-disposition", "attachment;filename=" + strFilename)
            Response.Charset = ""
            EnableViewState = False
            Controls.Add(frm)
            frm.Controls.Add(GvSummaryViewLog)
            frm.RenderControl(hw)
            Response.Write(tw.ToString())
            Response.End()
        Else
            If (Not ClientScript.IsStartupScriptRegistered("alert")) Then
                Page.ClientScript.RegisterStartupScript _
                (Me.GetType(), "alert", "invalidExportToExcel();", True)
            End If
        End If
    End Sub


#Region "custom methods"
    Private Sub GetPathDocument()
        docView.Attributes.Clear()
        Dim docPath As String = objdb.ExeQueryScalarString("select top 1 docpath from odcrfinal where package_id='" & GetWPID() & "' and isUploaded='true'")
        BindDocumentView(docpath)
    End Sub

    Private Sub BindDocumentView(ByVal strPath As String)
        docView.Attributes.Add("height", "450px")
        docView.Attributes.Add("width", "100%")
        docView.Attributes.Add("src", ConfigurationManager.AppSettings("Vpath") + strPath.Replace("\", "/"))
    End Sub
    Private Sub BindSiteAtt()
        Dim getSiteAttributeQuery As String = "select  TselProjectID,SiteNo, PoNo,POName, Workpackageid,Scope,sitename from poepmsitenew where workpackageid = '" & GetWPID() & "'"
        Dim dtSiteAtt As DataTable = objdb.ExeQueryDT(getSiteAttributeQuery, "siteAtt")
        If dtSiteAtt.Rows.Count > 0 Then
            tdpono.InnerText = dtSiteAtt.Rows(0).Item(2)
            tdponame.InnerText = dtSiteAtt.Rows(0).Item(3)
            tdsiteno.InnerText = dtSiteAtt.Rows(0).Item(1)
            tdsitename.InnerText = dtSiteAtt.Rows(0).Item(6)
            tdscope.InnerText = dtSiteAtt.Rows(0).Item(5)
            tdwpid.InnerText = dtSiteAtt.Rows(0).Item(4)
            tdprojectId.InnerText = dtSiteAtt.Rows(0).Item(0)
        End If
    End Sub

    Private Sub BindListCRApproved()
        GvListCR.DataSource = controller.GetListCR(GetWPID())
        GvListCR.DataBind()
    End Sub

    Private Sub GetCRLogHistorical()
        GvSummaryViewLog.DataSource = controller.GetFinalCRLogSummary(GetWPID())
        GvSummaryViewLog.DataBind()
    End Sub


    Private Function GetWPID() As String
        Return Request.QueryString("wpid")
    End Function
#End Region

End Class
