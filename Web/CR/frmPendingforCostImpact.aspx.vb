Imports Entities
Imports BusinessLogic
Imports Common
Imports System.Data
Partial Class CR_frmPendingforCostImpact
    Inherits System.Web.UI.Page
    Dim dt As DataTable
    Dim objBo As New BOMOM
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Page_size") Is Nothing Then Response.Redirect("~/SessionTimeout.aspx")
        If Page.IsPostBack = False Then
            If Request.QueryString("Type") = "U" Then
                frmlist.Visible = True
            End If
            BindGrid()
        End If
    End Sub
    Sub BindGrid()
        If Request.QueryString("id") > 0 Then
            Session("momId") = Request.QueryString("id")
            dt = objBo.uspMOMPendingSiteList(Session("momId"))
            GrdPo.PageIndex = 10 'Session("Page_size")
            GrdPo.DataSource = dt
            GrdPo.DataBind()
        End If
        
    End Sub

    Protected Sub GrdPo_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GrdPo.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lbl As Label = CType(e.Row.FindControl("lblNo"), Label)
            lbl.Text = (GrdPo.PageIndex * GrdPo.PageSize) + (e.Row.RowIndex + 1) & ".&nbsp;"
        End If
    End Sub

    Protected Sub GrdPo_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GrdPo.PageIndexChanging
        GrdPo.PageIndex = e.NewPageIndex()
        BindGrid()
    End Sub

    Protected Sub frmlist_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles frmlist.Click
        Dim type As String = "L"
        Response.Redirect("frmChangeRequest.aspx?MId=" & Session("momId") & "&Type=" & type)
    End Sub
End Class
