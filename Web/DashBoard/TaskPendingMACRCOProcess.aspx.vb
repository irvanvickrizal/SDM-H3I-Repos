
Partial Class DashBoard_TaskPendingMACRCOProcess
    Inherits System.Web.UI.Page

    Dim controller As New CRCOMultipleController
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        If Not Page.IsPostBack Then
            BindData(CommonSite.UserId, Integer.Parse(ConfigurationManager.AppSettings("codocid")))
        End If
    End Sub

    Protected Sub GvDocuments_PageIndexing(ByVal sender As Object, ByVal e As GridViewPageEventArgs) Handles GvDocuments.PageIndexChanging
        GvDocuments.PageIndex = e.NewPageIndex
        BindData(CommonSite.UserId, Integer.Parse(ConfigurationManager.AppSettings("codocid")))
    End Sub

    Protected Sub GvDocuments_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles GvDocuments.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim doctype As Label = CType(e.Row.FindControl("LblDocType"), Label)
            If doctype IsNot Nothing Then
                Dim pnlCRDocView As HtmlGenericControl = CType(e.Row.FindControl("pnlCRDocView"), HtmlGenericControl)
                Dim pnlCODocView As HtmlGenericControl = CType(e.Row.FindControl("pnlCODocView"), HtmlGenericControl)
                If pnlCRDocView IsNot Nothing And pnlCODocView IsNot Nothing Then
                    pnlCRDocView.Visible = False
                    pnlCODocView.Visible = False
                    If doctype.Text.ToLower().Equals("co online") Then
                        pnlCODocView.Visible = True
                    Else
                        pnlCRDocView.Visible = True
                    End If
                End If
            End If
        End If
    End Sub

#Region "Custom Methods"
    Private Sub BindData(ByVal userid As Integer, ByVal codocid As Integer)
        GvDocuments.DataSource = controller.GetCRCOTaskPendings_MultiplePendingProcess(userid, codocid)
        GvDocuments.DataBind()
    End Sub
#End Region

End Class
