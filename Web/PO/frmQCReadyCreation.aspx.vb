Imports Common
Imports BusinessLogic
Imports System.Data
Imports System.IO

Partial Class PO_frmQCReadyCreation
    Inherits System.Web.UI.Page
    Dim objutil As New DBUtil
    Dim dtDocs As New DataTable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        If Not IsPostBack Then
            BindData()
        End If
    End Sub

    Protected Sub BtnExpt_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnExportExcel.Click
        If GvQCReport.Rows.Count() > 0 Then
            Dim tw As New StringWriter()
            Dim strFilename As String = "KPIAcpReadyCreation_" + DateTime.Now.ToShortDateString + ".xls"
            Dim hw As New System.Web.UI.HtmlTextWriter(tw)
            Dim frm As HtmlForm = New HtmlForm()
            Response.ContentType = "application/vnd.ms-excel"
            Response.AddHeader("content-disposition", "attachment;filename=" + strFilename)
            Response.Charset = ""
            EnableViewState = False
            Controls.Add(frm)
            GvQCReport.AllowPaging = False
            BindData()
            frm.Controls.Add(GvQCReport)
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

    Protected Sub GvQCReport_PageIndexChanging(ByVal sender As Object, ByVal e As GridViewPageEventArgs) Handles GvQCReport.PageIndexChanging
        GvQCReport.PageIndex = e.NewPageIndex
        BindData()
    End Sub

#Region "custom methods"
    Private Sub BindData()
        dtDocs = objutil.ExeQueryDT("exec uspGetQCReadyCreation " & CommonSite.UserId & ", " & ConfigurationManager.AppSettings("QCID"), "dt")
        GvQCReport.DataSource = dtDocs
        GvQCReport.DataBind()
    End Sub
#End Region
End Class
