Imports BusinessLogic
Imports Entities
Imports Common
Imports System.Data
Partial Class COD_frmSiteReason
    Inherits System.Web.UI.Page
    Dim objET As New ETSiteReason
    Dim objBO As New BOSiteReason
    Dim objdl As New BODDLs
    Dim objUtil As New DBUtil
    Dim dt As New DataTable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        Session("Page_size") = 10
        If Page.IsPostBack = False Then
            objdl.fillDDL(ddlPONo, "PODetails", True, Constants._DDL_Default_Select)
        End If
        If Request.QueryString("id") <> "" And Request.Form("ddlReasonCategory") = "" And Request.Form("ddlReason") = "" Then
            ddlPONo.SelectedItem.Text = Request.QueryString("id")
            filldetails()
            binddata()
        End If
    End Sub
    Sub filldetails()
        tblReason.Visible = True
        lblPONo.Text = ddlPONo.SelectedItem.Text
        lblSiteNo.Text = Request.QueryString("SId")
        lblVersion.Text = Request.QueryString("version")
        objdl.fillDDL(ddlReasonCategory, "ReasonCategory", True, Constants._DDL_Default_Select)
    End Sub
    Sub binddata()
        dt = objUtil.ExeQueryDT("Exec uspSiteLD '" & ddlPONo.SelectedItem.Text & "','" & ddlSelect.SelectedValue & "','" & txtSearch.Text.Replace("'", "''") & "'," & hdnDisp.Value & ",'" & hdnSort.Value & "'", "SiteReason")
        If dt.Rows.Count > 0 Then
            grdSiteReason.DataSource = dt
            grdSiteReason.PageSize = Session("Page_size")
            grdSiteReason.DataBind()
        Else
            DataBind()
        End If
    End Sub
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        binddata()
    End Sub
    Protected Sub ddlPONo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPONo.SelectedIndexChanged
        binddata()
        lblPONo.Text = ""
        lblSiteNo.Text = ""
        lblVersion.Text = ""
        ddlReasonCategory.SelectedIndex = -1
        ddlReason.ClearSelection()
        txtRemark.Value = ""
        txtAddRemark.Value = ""
        txtNoofdays.Value = ""
    End Sub
    Protected Sub btnNewGroup_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNewGroup.ServerClick
        With objET
            .PONo = lblPONo.Text
            .SiteID = lblSiteNo.Text
            .Version = lblVersion.Text
            .ReasoncatID = ddlReasonCategory.SelectedItem.Value
            .ReasonID = ddlReason.SelectedItem.Value
            .NoOfDays = txtNoofdays.Value
            .Remark = txtRemark.Value
            .AddRemarks = txtAddRemark.Value
        End With
        BOcommon.result(objBO.uspSiteReasonIU(objET), True, "frmSiteReasonListing.aspx", "SiteReason", Constants._INSERT)
    End Sub
    Protected Sub btnCanel_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCanel.ServerClick
        Response.Redirect("frmSiteReason.aspx")
    End Sub
    Protected Sub grdSiteReason_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdSiteReason.Sorting
        If hdnSort.Value = "" Or hdnSort.Value = e.SortExpression & " Desc" Then
            hdnSort.Value = e.SortExpression & " Asc"
        Else
            hdnSort.Value = e.SortExpression & " Desc"
        End If
        binddata()
    End Sub
    Protected Sub grdSiteReason_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdSiteReason.RowDataBound
        Select Case e.Row.RowType
            Case DataControlRowType.DataRow
                Dim lbl As Label = CType(e.Row.FindControl("lblNo"), Label)
                lbl.Text = (grdSiteReason.PageIndex * grdSiteReason.PageSize) + (e.Row.RowIndex + 1) & ".&nbsp;"
        End Select
    End Sub
    Protected Sub grdSiteReason_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdSiteReason.PageIndexChanging
        grdSiteReason.PageIndex = e.NewPageIndex
        binddata()
    End Sub
    Protected Sub lnkAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkAll.Click
        dt = objUtil.ExeQueryDT("Exec uspSiteList", "GSR")
        grdSiteReason.DataSource = dt
        grdSiteReason.DataBind()
    End Sub
    Protected Sub ddlReasonCategory_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlReasonCategory.SelectedIndexChanged
        objdl.fillDDL(ddlReason, "Reason", ddlReasonCategory.SelectedValue, True, Constants._DDL_Default_Select)
    End Sub
    Protected Sub ddlReason_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlReason.SelectedIndexChanged
        dt = objBO.uspReasonDetails(ddlReason.SelectedValue)
        txtRemark.Value = dt.Rows(0).Item("Remark").ToString.Replace("'", "''")
        txtAddRemark.Value = dt.Rows(0).Item("AddRemarks").ToString.Replace("'", "''")
    End Sub
End Class
