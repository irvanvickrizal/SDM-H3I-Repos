Imports System.Data

Partial Class DashBoard_frmSiteDocCount_WCC
    Inherits System.Web.UI.Page
    Private controller As New WCCController
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        If (Not Page.IsPostBack) Then
            BindData(CommonSite.UserId, False)
        End If
    End Sub

    Protected Sub GrdDocCount_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles GrdDocCount.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            Dim LblWCCID As Label = CType(e.Row.FindControl("LblWCCID"), Label)
            If Not LblWCCID Is Nothing Then
                Dim viewlog As HtmlAnchor = CType(e.Row.FindControl("viewlog"), HtmlAnchor)
                If Not viewlog Is Nothing Then
                    viewlog.HRef = "../fancybox_Form/fb_WCCViewLog.aspx?wid=" & LblWCCID.Text & "&docid=" & ConfigurationManager.AppSettings("WCCDOCID") & "&doctype=WCC"
                End If
            End If
        End If
    End Sub

    Protected Sub GrdDocCount_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs) Handles GrdDocCount.RowCommand
        If (e.CommandName.Equals("checkdoc")) Then
            Dim row As GridViewRow = DirectCast(DirectCast(e.CommandSource, LinkButton).NamingContainer, GridViewRow)
            Dim LblSNO As Label = DirectCast(row.Cells(0).FindControl("LblSNO"), Label)
            Response.Redirect("~/WCCApproval/WCC_Approval_Form.aspx?wid=" & e.CommandArgument.ToString() & "&sno=" & LblSNO.Text)
        End If
    End Sub

    Protected Sub BtnGoToDashboard_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnGoToDashboard.Click
        Response.Redirect("~/Welcome.aspx")
    End Sub


#Region "Custom Methods"
    Private Sub BindData(ByVal userid As Integer, ByVal isViewAll As Boolean)
        GrdDocCount.DataSource = controller.WCCTransaction_Pending(userid, isViewAll)
        GrdDocCount.DataBind()
    End Sub
#End Region

End Class
