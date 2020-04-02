Imports Common
Imports System.IO
Imports System.Data

Partial Class RPT_frmCRApproved
    Inherits System.Web.UI.Page
    Dim objdb As New DBUtil
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        If Not Page.IsPostBack Then
            BindData(CommonSite.UserId)
        End If
    End Sub

    Protected Sub BtnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnExportExcel2.Click
        If (GvCRApprovedReport.Rows.Count() > 0) Then
            Dim tw As New StringWriter()
            Dim strFilename As String = "CRApprovedDoneReport_" + DateTime.Now.ToShortDateString + ".xls"
            Dim hw As New System.Web.UI.HtmlTextWriter(tw)
            Dim frm As HtmlForm = New HtmlForm()
            Response.ContentType = "application/vnd.ms-excel"
            Response.AddHeader("content-disposition", "attachment;filename=" + strFilename)
            Response.Charset = ""
            EnableViewState = False
            Controls.Add(frm)
            frm.Controls.Add(GvCRApprovedReport)
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
    Private Sub BindData(ByVal userid As Integer)
        Dim strsql As String = "exec uspCR_GetCRApproved_Report " & CommonSite.UserId
        Dim dtSiteApprovalStatus As DataTable = objdb.ExeQueryDT(strsql, "dtSiteAppStatus")
        If dtSiteApprovalStatus.Rows.Count > 0 Then
            GvCRApprovedReport.DataSource = dtSiteApprovalStatus
            GvCRApprovedReport.DataBind()
            BtnExportExcel2.Enabled = True
        Else
            BtnExportExcel2.Enabled = False
        End If
    End Sub
#End Region

End Class
