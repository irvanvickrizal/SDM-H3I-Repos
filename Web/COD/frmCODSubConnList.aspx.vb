Imports Common
Imports BusinessLogic
Imports Entities
Imports System.Data
Partial Class COD_frmCODSubConnList
    Inherits System.Web.UI.Page
    Dim objdl As New BODDLs
    Dim objdo As New ETCODSite
    Dim objbo As New BOCODSite
    Dim dt As New DataTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            objdl.fillDDL(DDLZone, "CODZone", True, Constants._DDL_Default_Select)
            BindData()
        End If
    End Sub
    Sub BindData()
        dt = objbo.uspCODSubConSiteList(, DDLZone.SelectedValue, ddlSelect.SelectedValue, txtSearch.Text, hdnSort.Value)
        grdSubCon.PageSize = Session("Page_size")
        grdSubCon.DataSource = dt
        grdSubCon.DataBind()
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        BindData()
    End Sub

    Protected Sub btnCancel_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.ServerClick
        Response.Redirect("frmCODSubConnList.aspx")
    End Sub

    Protected Sub grdSubCon_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdSubCon.RowDataBound
        Select Case e.Row.RowType
            Case DataControlRowType.DataRow
                Dim lbl As Label = CType(e.Row.Cells(0).FindControl("lblNo"), Label)
                lbl.Text = grdSubCon.PageIndex * grdSubCon.PageSize + (e.Row.RowIndex + 1) & ".&nbsp;"
        End Select
    End Sub

    Protected Sub btnNewGroup_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNewGroup.ServerClick
        Response.Redirect("frmCODSubCon.aspx")
    End Sub

    Protected Sub grdSubCon_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdSubCon.PageIndexChanging
        grdSubCon.PageIndex = e.NewPageIndex
        BindData()
    End Sub

    Protected Sub grdSubCon_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdSubCon.Sorting
        If hdnSort.Value = "" Or hdnSort.Value = e.SortExpression & " Desc" Then
            hdnSort.Value = e.SortExpression & " Asc"
        Else
            hdnSort.Value = e.SortExpression & " Desc"
        End If
        BindData()
    End Sub

    Protected Sub DDLZone_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DDLZone.SelectedIndexChanged
        BindData()
    End Sub
End Class
