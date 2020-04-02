Imports BusinessLogic
Imports Entities
Imports Common
Imports System.Data
Partial Class COD_frmCODReason
    Inherits System.Web.UI.Page
    Dim objET As New ETCODReason
    Dim objBO As New BOCODReason
    Dim objdl As New BODDLs
    Dim dt As New DataTable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        btnSave.Attributes.Add("onclick", "javascript:return checkIsEmpty();")
        btnDelete.Attributes.Add("onclick", "javascript:return confirm('Are you sure you want to delete this record?');")
        Session("User_Name") = "hari"
        Session("Page_size") = 11
        If Page.IsPostBack = False Then
            objdl.fillDDL(ddlReasonCategory, "ReasonCategory", True, Constants._DDL_Default_Select)
            binddata()
            If Not Request.QueryString("id") Is Nothing Then
                addrow.InnerText = "Edit"
                btnSave.Text = "Update"
                btnNewGroup.Disabled = True
                Binddetails()
            End If
        End If
    End Sub
    Sub filldetails()
        objET.FK_ReasonCategory = ddlReasonCategory.SelectedValue
        objET.Reason = txtReason.Value
        objET.Remark = txtRemark.Value
        objET.AddRemarks = txtAddRemark.Value
        objET.AT.RStatus = Constants.STATUS_ACTIVE
        objET.AT.LMBY = Session("User_Name")
    End Sub
    Sub binddata()
        dt = objBO.uspReasonLD(0, hdnSort.Value)
        grdReason.DataSource = dt
        grdReason.PageSize = Session("Page_size")
        grdReason.DataBind()
    End Sub
    Sub Binddetails()
        dt = objBO.uspReasonLD(Request.QueryString("id"))
        If dt.Rows.Count > 0 Then
            ddlReasonCategory.SelectedValue = dt.Rows(0).Item("FK_ReasonCategory")
            txtReason.Value = dt.Rows(0).Item("Reason").ToString.Replace("'", "''")
            txtRemark.Value = dt.Rows(0).Item("Remark").ToString.Replace("'", "''")
            txtAddRemark.Value = dt.Rows(0).Item("AddRemarks").ToString.Replace("'", "''")
            tblReason.Visible = True
            btnDelete.Visible = True
        End If
    End Sub
    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        filldetails()
        If Request.QueryString("id") Is Nothing Then
            BOcommon.result(objBO.uspReasonIU(objET), True, "frmCODReason.aspx", "Reason", Constants._INSERT)
        Else
            objET.PK_Reason = Request.QueryString("id")
            BOcommon.result(objBO.uspReasonIU(objET), True, "frmCODReason.aspx", "Reason", Constants._UPDATE)
        End If
    End Sub
    Protected Sub btnNewGroup_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNewGroup.ServerClick
        tblReason.Visible = True
    End Sub
    Protected Sub btnCanel_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCanel.ServerClick
        Response.Redirect("frmCODReason.aspx")
    End Sub
    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        BOcommon.result(objBO.uspDelete("Reason", "PK_Reason", Request.QueryString("id")), True, "frmCODReason.aspx", " ", Constants._DELETE)
    End Sub
    Protected Sub grdReason_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdReason.Sorting
        If hdnSort.Value = "" Or hdnSort.Value = e.SortExpression & " Desc" Then
            hdnSort.Value = e.SortExpression & " Asc"
        Else
            hdnSort.Value = e.SortExpression & " Desc"
        End If
        binddata()
    End Sub
    Protected Sub grdReason_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdReason.RowDataBound
        Select Case e.Row.RowType
            Case DataControlRowType.DataRow
                Dim lbl As Label = CType(e.Row.FindControl("lblNo"), Label)
                lbl.Text = (grdReason.PageIndex * grdReason.PageSize) + (e.Row.RowIndex + 1) & ".&nbsp;"
        End Select
    End Sub
    Protected Sub grdReason_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdReason.PageIndexChanging
        grdReason.PageIndex = e.NewPageIndex
        binddata()
    End Sub
End Class
