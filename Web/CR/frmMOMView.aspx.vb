Imports System.Data
Imports BusinessLogic
Imports Common
Partial Class frmMOMView
    Inherits System.Web.UI.Page

    Dim dt As DataTable
    Dim objBL As New BOMOM

    Dim objDB As New DBUtil

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Page_size") Is Nothing Then Response.Redirect("~/SessionTimeout.aspx")
        If Not Page.IsPostBack Then
            BindData()
        End If

    End Sub

    Sub BindData()
        'If txtSearch.Text.Trim() <> "" Then
        '    dt = objBL.uspMOMList(ddlselect.SelectedValue, txtSearch.Text.Trim(), 0, hdnSort.Value)
        'Else
        '    dt = objBL.uspMOMList(Nothing, Nothing, 0, hdnSort.Value)
        'End If

        Dim strSql As String = "EXEC [uspMOMView] 0,'" & ddlselect.SelectedValue & "','" & txtSearch.Text.Trim() & "','" & hdnSort.Value & "','" & ddlStatus.SelectedValue & "'"
        dt = objDB.ExeQueryDT(strSql, "MOM")
        ' dt = objBL.uspMOMList(0, ddlselect.SelectedValue, txtSearch.Text.Trim(), hdnSort.Value, ddlStatus.SelectedValue)
        GrdMOM.DataSource = dt
        GrdMOM.DataBind()

    End Sub

    'Protected Sub GrdMOM_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GrdMOM.RowCreated
    '    If e.Row.RowType = DataControlRowType.DataRow Then
    '        e.Row.Cells(1).Visible = False
    '    ElseIf e.Row.RowType = DataControlRowType.Header Then
    '        e.Row.Cells(1).Visible = False
    '    End If
    'End Sub

    Protected Sub GrdMOM_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GrdMOM.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lbl As Label = CType(e.Row.FindControl("lblNo"), Label)
            lbl.Text = (GrdMOM.PageIndex * GrdMOM.PageSize) + (e.Row.RowIndex + 1) & ".&nbsp;"
        End If
    End Sub



    Protected Sub GrdMOM_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles GrdMOM.Sorting

        If hdnSort.Value = "" Or hdnSort.Value = e.SortExpression & " Desc" Then
            hdnSort.Value = e.SortExpression & " Asc"
        Else
            hdnSort.Value = e.SortExpression & " Desc"
        End If
        BindData()
    End Sub

    Protected Sub GrdMOM_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GrdMOM.PageIndexChanging
        GrdMOM.PageIndex = e.NewPageIndex
        BindData()

    End Sub

    Protected Sub BtnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSearch.Click
        BindData()
    End Sub

    Protected Sub ddlStatus_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlStatus.SelectedIndexChanged
        BindData()
    End Sub


End Class
