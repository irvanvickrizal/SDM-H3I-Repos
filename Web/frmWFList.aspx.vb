Imports BusinessLogic
Imports System.Data
Partial Class frmWFList
    Inherits System.Web.UI.Page
    Dim objbo As New BOTWorkFlow
    Dim dt As DataTable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Page_size") Is Nothing Then Response.Redirect("~\SessionTimeout.aspx")
        If Page.IsPostBack = False Then
            binddata()
        End If
    End Sub
    Sub binddata()
        dt = objbo.uspTWorkflowLD(, ddlSelect.SelectedValue, txtSearch.Value.Replace("'", "''"), hdnSort.Value)
        grdWF.DataSource = dt
        grdWF.PageSize = Session("Page_size")
        grdWF.DataBind()
    End Sub
    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Response.Redirect("frmWFSetup.aspx")
    End Sub

    Protected Sub grdWF_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdWF.PageIndexChanging
        grdWF.PageIndex = e.NewPageIndex
        binddata()
    End Sub

    Protected Sub grdWF_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdWF.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lbl As Label = CType(e.Row.FindControl("lblNo"), Label)
            lbl.Text = (grdWF.PageIndex * grdWF.PageSize) + (e.Row.RowIndex + 1) & ".&nbsp;"
        End If
    End Sub

    Protected Sub grdWF_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdWF.Sorting
        If hdnSort.Value = "" Or hdnSort.Value = e.SortExpression & " Desc" Then
            hdnSort.Value = e.SortExpression & " Asc"
        Else
            hdnSort.Value = e.SortExpression & " Desc"
        End If
        binddata()
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        binddata()
    End Sub
End Class
