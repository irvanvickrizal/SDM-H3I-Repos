Imports BusinessLogic
Imports Common
Imports system.Data
Partial Class frmWFDoc
    Inherits System.Web.UI.Page
    Dim objBL As New BODDLs
    Dim objBo As New BOWTDoc
    Dim dt As DataTable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            btnChange.Visible = True
            objBL.fillDDL(ddlWF, "TWorkFlow", False, Constants._DDL_Default_Select)
            BindData()
        End If
    End Sub
    Sub BindData()
        grdWF.Columns(1).Visible = True
        dt = objBo.uspWFDocList(ddlWF.SelectedValue, hdnSort.Value)
        'grdWF.PageSize = Session("Page_size")
        grdWF.DataSource = dt
        grdWF.DataBind()
        grdWF.Columns(1).Visible = False
    End Sub
    Protected Sub ddlWF_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlWF.SelectedIndexChanged
        If ddlWF.SelectedValue > 0 Then btnChange.Visible = True
        BindData()
    End Sub
    Protected Sub grdWF_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdWF.PageIndexChanging
        grdWF.PageIndex = e.NewPageIndex
        BindData()
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
        BindData()
    End Sub
    Protected Sub btnChange_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnChange.ServerClick
        Response.Redirect("frmWorkFlowDocSetup.aspx?id=" & ddlWF.SelectedValue)
    End Sub
End Class
