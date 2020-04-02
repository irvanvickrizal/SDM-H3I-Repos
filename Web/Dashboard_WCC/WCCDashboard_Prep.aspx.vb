Imports System.IO

Partial Class Dashboard_WCC_WCCDashboard_Prep
    Inherits System.Web.UI.Page
    Dim controller As New WCCController
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        If (Not Page.IsPostBack) Then
            BindData()
        End If
    End Sub

    Protected Sub GvWCCList_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles GvWCCList.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            Dim opennewframe As HtmlAnchor = CType(e.Row.FindControl("opennewframe"), HtmlAnchor)
            Dim LblPackageId As Label = CType(e.Row.FindControl("LblPackageId"), Label)
            opennewframe.HRef = "~/fancybox_Form/fb_SiteInformation.aspx?pid=" & LblPackageId.Text
        End If
    End Sub

    Protected Sub GvWCCList_ItemCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs) Handles GvWCCList.RowCommand
        If e.CommandName.Equals("EditWCC") Then
            If controller.WCCCheckingProcess(Convert.ToInt32(e.CommandArgument.ToString())) = 1 Then
                Response.Redirect("~/WCC/frmODWCCFinal.aspx?wid=" & e.CommandArgument.ToString())
            Else
                Response.Redirect("~/WCC/frmWCCPreparation.aspx?wid=" & e.CommandArgument.ToString())
            End If
        End If
    End Sub

    Protected Sub LbtExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LbtExport.Click
        ExportToExcel(GvWCCList)
    End Sub

#Region "Custom Methods"
    Private Sub BindData()
        GvWCCList.DataSource = controller.GetWCCPreparation_Subcon(CommonSite.UserId)
        GvWCCList.DataBind()
    End Sub
    Private Sub ExportToExcel(ByVal gv As GridView)
        If (gv.Rows.Count() > 0) Then
            Dim tw As New StringWriter()
            Dim strFilename As String = "WCCPreparation_" + DateTime.Now.ToShortDateString + ".xls"
            Dim hw As New System.Web.UI.HtmlTextWriter(tw)
            Dim frm As HtmlForm = New HtmlForm()
            Response.ContentType = "application/vnd.ms-excel"
            Response.AddHeader("content-disposition", "attachment;filename=" + strFilename)
            Response.Charset = ""
            EnableViewState = False
            Controls.Add(frm)
            frm.Controls.Add(gv)
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
#End Region

End Class
