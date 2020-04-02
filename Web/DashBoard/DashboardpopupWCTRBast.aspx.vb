Imports BusinessLogic
Imports Common
Imports System.Data
Partial Class DashboardpopupWCTRBast
    Inherits System.Web.UI.Page

    Dim objBO As New BODashBoard
    Dim objUtil As New DBUtil
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        CreateBaut()
    End Sub
    Sub CreateBaut()
        Dim iMainTable As New HtmlTable

        Dim dtStatus As New DataTable
        Dim strquery1 As String = "exec [uspDashBoardWCTRBast]  '" + CommonSite.UserType().ToString() + "'," + CommonSite.UserId().ToString
        dtStatus = objUtil.ExeQueryDT(strquery1, "MilestoneTrackCount")


        grdDocuments.DataSource = dtStatus
        grdDocuments.DataBind()
        If (CommonSite.UserType().ToLower() = "n") Then
            grdDocuments.Columns(2).Visible = False
            grdDocuments.Columns(3).Visible = True

        Else
            grdDocuments.Columns(2).Visible = False
            grdDocuments.Columns(3).Visible = True

        End If

    End Sub
    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        Return
    End Sub

    Protected Sub grdDocuments_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdDocuments.RowDataBound
        Select Case e.Row.RowType
            Case DataControlRowType.DataRow
                Dim lbl As Label = CType(e.Row.FindControl("lblNo"), Label)
                lbl.Text = (grdDocuments.PageIndex * grdDocuments.PageSize) + (e.Row.RowIndex + 1) & ".&nbsp;"
        End Select
    End Sub

    Protected Sub BtnClose_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnClose.ServerClick
        Response.Redirect("../frmdashboard.aspx")
    End Sub
    Protected Sub grdDocuments_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdDocuments.PageIndexChanging
        grdDocuments.PageIndex = e.NewPageIndex
        grdDocuments.DataBind()
    End Sub
End Class
