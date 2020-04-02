Imports Common
Imports System.Data
Imports System.Text
Imports System.IO

Partial Class RPT_CRCOSiteApprovalStatus
    Inherits System.Web.UI.Page
    Dim objutil As New DBUtil
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        BindData()
    End Sub

    Protected Sub BtnExportExcelClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnExportExcel2.Click
        If (GvCRCOApprovalReport.Rows.Count() > 0) Then
            Dim tw As New StringWriter()
            Dim strFilename As String = "CRCOSiteApprovalStatusReport_" + DateTime.Now.ToShortDateString + ".xls"
            Dim hw As New System.Web.UI.HtmlTextWriter(tw)
            Dim frm As HtmlForm = New HtmlForm()
            Response.ContentType = "application/vnd.ms-excel"
            Response.AddHeader("content-disposition", "attachment;filename=" + strFilename)
            Response.Charset = ""
            EnableViewState = False
            Controls.Add(frm)
            frm.Controls.Add(GvCRCOApprovalReport)
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

#Region "Custom Methods"
    Private Sub BindData()
        Dim strsql As String = "exec uspGetCRCOApprovalStatus " & CommonSite.UserId
        Dim dtSiteApprovalStatus As DataTable = objutil.ExeQueryDT(strsql, "dtSiteAppStatus")
        If dtSiteApprovalStatus.Rows.Count > 0 Then
            GvCRCOApprovalReport.DataSource = dtSiteApprovalStatus
            GvCRCOApprovalReport.DataBind()
            BtnExportExcel2.Enabled = True
        Else
            BtnExportExcel2.Enabled = False
        End If
    End Sub
#End Region


End Class
