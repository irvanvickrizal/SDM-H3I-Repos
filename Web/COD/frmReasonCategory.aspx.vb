Imports BusinessLogic
Imports Entities
Imports Common
Imports System.Data
Partial Class COD_frmReasonCategory
    Inherits System.Web.UI.Page
    Dim objET As New ETReasonCategory
    Dim objBO As New BOReasonCategory
    Dim dt As New DataTable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        btnSave.Attributes.Add("onclick", "javascript:return checkIsEmpty();")
        btnDelete.Attributes.Add("onclick", "javascript:return confirm('Are you sure you want to delete this record?');")
        Session("User_Name") = "hari"
        Session("Page_size") = 11
        If Page.IsPostBack = False Then
            binddata()
            If Not Request.QueryString("id") Is Nothing Then
                addrow.InnerText = "Edit"
                btnSave.Text = "Update"
                btnNewGroup.Disabled = True
                Binddetails()
            End If
        End If
    End Sub
    Protected Sub btnNewGroup_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNewGroup.ServerClick
        tblReasonCategory.Visible = True
    End Sub
    Sub filldetails()
        objET.RCCode = txtRCCode.Value
        objET.RCDesc = txtRCDesc.Value
        objET.AT.RStatus = Constants.STATUS_ACTIVE
        objET.AT.LMBY = Session("User_Name")
    End Sub
    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        filldetails()
        If Request.QueryString("id") Is Nothing Then
            BOcommon.result(objBO.uspReasonCategoryIU(objET), True, "frmReasonCategory.aspx", "RCCode", Constants._INSERT)
        Else
            objET.PK_ReasonCategory = Request.QueryString("id")
            BOcommon.result(objBO.uspReasonCategoryIU(objET), True, "frmReasonCategory.aspx", "RCCode", Constants._UPDATE)
        End If
    End Sub
    Protected Sub btnCanel_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCanel.ServerClick
        Response.Redirect("frmReasonCategory.aspx")
    End Sub
    Sub binddata()
        dt = objBO.uspReasonCategoryLD(0, hdnSort.Value)
        grdReasonCategory.DataSource = dt
        grdReasonCategory.PageSize = Session("Page_size")
        grdReasonCategory.DataBind()
    End Sub
    Sub Binddetails()
        dt = objBO.uspReasonCategoryLD(Request.QueryString("id"))
        If dt.Rows.Count > 0 Then
            txtRCCode.Value = dt.Rows(0).Item("RCCode").ToString.Replace("'", "''")
            txtRCDesc.Value = dt.Rows(0).Item("RCDesc").ToString.Replace("'", "''")
            tblReasonCategory.Visible = True
            btnDelete.Visible = True
        End If
    End Sub
    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        BOcommon.result(objBO.uspDelete("ReasonCategory", "PK_ReasonCategory", Request.QueryString("id")), True, "frmReasonCategory.aspx", " ", Constants._DELETE)
    End Sub
    Protected Sub grdReasonCategory_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdReasonCategory.Sorting
        If hdnSort.Value = "" Or hdnSort.Value = e.SortExpression & " Desc" Then
            hdnSort.Value = e.SortExpression & " Asc"
        Else
            hdnSort.Value = e.SortExpression & " Desc"
        End If
        binddata()
    End Sub
    Protected Sub grdReasonCategory_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdReasonCategory.RowDataBound
        Select Case e.Row.RowType
            Case DataControlRowType.DataRow
                Dim lbl As Label = CType(e.Row.FindControl("lblNo"), Label)
                lbl.Text = (grdReasonCategory.PageIndex * grdReasonCategory.PageSize) + (e.Row.RowIndex + 1) & ".&nbsp;"
        End Select
    End Sub
    Protected Sub grdReasonCategory_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdReasonCategory.PageIndexChanging
        grdReasonCategory.PageIndex = e.NewPageIndex
        binddata()
    End Sub
End Class
