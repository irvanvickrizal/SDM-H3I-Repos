Imports BusinessLogic
Imports Common
Imports System.Data
Partial Class PO_frmPOSiteList
    Inherits System.Web.UI.Page
    Dim dt As DataTable
    Dim objbo As New BOPODetails
    Dim objdl As New BODDLs
    Dim objUtil As New DBUtil
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        If Page.IsPostBack = False Then
            objdl.fillDDL(ddlPO, "PODetails", False, Constants._DDL_Default_Select)
            bindData()
        End If
    End Sub
    Sub bindData()
        'grdPOrawdata.Columns(3).Visible = True
        dt = objUtil.ExeQueryDT("Exec uspPODetailsList '" & ddlPO.SelectedValue & "','" & ddlSelect.SelectedValue & "','" & txtSearch.Text.Replace("'", "''") & "'," & hdnDisp.Value & ",'" & hdnSort.Value & "'", "PoDetails")
        'dt = objbo.uspPODetailsList(ddlPO.SelectedValue, ddlSelect.SelectedValue, txtSearch.Text.Replace("'", "''"), hdnDisp.Value)
        grdPOrawdata.DataSource = dt
        grdPOrawdata.PageSize = Session("Page_size")
        grdPOrawdata.DataBind()
        'grdPOrawdata.Columns(3).Visible = False
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        'If Request.QueryString("type") = "P" Then
        '    bindData(0, "P")
        'Else
        '    bindData(0, "D")
        'End If
        bindData()
    End Sub

    Protected Sub ddlPO_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPO.SelectedIndexChanged
        hdnDisp.Value = 1
        bindData()
    End Sub

    Protected Sub lnkAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkAll.Click
        hdnDisp.Value = 0
        bindData()
    End Sub

    Protected Sub grdPOrawdata_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdPOrawdata.PageIndexChanging
        grdPOrawdata.PageIndex = e.NewPageIndex
        bindData()
    End Sub
    Protected Sub grdPOrawdata_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdPOrawdata.Sorting
        If hdnSort.Value = "" Or hdnSort.Value = e.SortExpression & " Desc" Then
            hdnSort.Value = e.SortExpression & " Asc"
        Else
            hdnSort.Value = e.SortExpression & " Desc"
        End If
        bindData()
    End Sub
End Class
