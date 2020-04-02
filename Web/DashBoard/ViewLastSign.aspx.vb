Imports BusinessLogic
Imports Common
Imports Entities
Imports System.Data
Imports DGSignPWD
Partial Class DashBoard_ViewLastSign
    Inherits System.Web.UI.Page
    Dim objUtil As New DBUtil
    Dim strSql As String
    Dim dt As DataTable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        bind()
    End Sub
    Sub bind()
        strSql = "Exec uspLastSignDoc " & CommonSite.UserId()
        dt = objUtil.ExeQueryDT(strSql, "SiteDoc")
        grdDocuments.PageSize = Session("Page_size")
        grdDocuments.DataSource = dt
        grdDocuments.DataBind()
    End Sub
    Protected Sub grdDocuments_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdDocuments.RowDataBound
        Select Case e.Row.RowType
            Case DataControlRowType.DataRow
                Dim lbl As Label = CType(e.Row.FindControl("lblnoSec"), Label)
                lbl.Text = (grdDocuments.PageIndex * grdDocuments.PageSize) + (e.Row.RowIndex + 1) & ".&nbsp;"
        End Select
    End Sub
    Protected Sub grdDocuments_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdDocuments.PageIndexChanging
        grdDocuments.PageIndex = e.NewPageIndex
        bind()
    End Sub
End Class
