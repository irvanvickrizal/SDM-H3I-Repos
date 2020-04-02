Imports System.IO

Partial Class Dashboard_WCC_WCCDashboard_Rejection_Subcon
    Inherits System.Web.UI.Page

    Private controller As New WCCController
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        If Not Page.IsPostBack Then
            BindData(CommonSite.UserId)
        End If
    End Sub

    Protected Sub GVWCCList_ItemCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs) Handles GvWCCList.RowCommand
        If e.CommandName.Equals("EditWCC") Then
            Response.Redirect("../WCC/frmODWCCFinal.aspx?wid=" & e.CommandArgument.ToString())
        End If
    End Sub

    Protected Sub GvWCCList_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles GvWCCList.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            Dim opennewframe As HtmlAnchor = CType(e.Row.FindControl("opennewframe"), HtmlAnchor)
            Dim LblWCCID As Label = CType(e.Row.FindControl("LblWCCID"), Label)
            Dim LblPackageId As Label = CType(e.Row.FindControl("LblPackageId"), Label)
            opennewframe.HRef = "~/fancybox_Form/fb_SiteInformation.aspx?pid=" & LblPackageId.Text
            If Not LblWCCID Is Nothing Then
                Dim viewlog As HtmlAnchor = CType(e.Row.FindControl("viewlog"), HtmlAnchor)
                If Not viewlog Is Nothing Then
                    viewlog.HRef = "../fancybox_Form/fb_WCCViewLog.aspx?wid=" & LblWCCID.Text & "&docid=" & ConfigurationManager.AppSettings("WCCDOCID") & "&doctype=WCC"
                End If
            End If
        End If
    End Sub

    Protected Sub LbtExport_Click(ByVal sender As Object, ByVal e As EventArgs) Handles LbtExport.Click
        ExportToExcel(GvWCCList)
    End Sub

#Region "Custom Methods"
    Private Sub BindData(ByVal userid As Integer)
        GvWCCList.DataSource = controller.GetWCCRejection_Subcon(userid)
        GvWCCList.DataBind()
    End Sub

    Private Sub ExportToExcel(ByVal gv As GridView)
        If (gv.Rows.Count() > 0) Then
            Dim tw As New StringWriter()
            Dim strFilename As String = "WCCRejection_" + DateTime.Now.ToShortDateString + ".xls"
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
