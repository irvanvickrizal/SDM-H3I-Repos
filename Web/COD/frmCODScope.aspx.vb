Imports BusinessLogic
Imports Entities
Imports Common
Imports System.Data
Partial Class COD_frmCODScope
    Inherits System.Web.UI.Page
    Dim objET As New ETCODScope
    Dim objbo As New BOCODScope
    Dim dt As New DataTable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        btnSave.Attributes.Add("onclick", "javascript:return checkIsEmpty();")
        If Page.IsPostBack = False Then
            binddata()
            If Not Request.QueryString("id") Is Nothing Then
                tdTitle.InnerText = ""
                addrow.InnerText = "Edit"
                btnSave.Text = "Update"
                btnNewGroup.Disabled = True
                Binddetails()
            End If
        End If
    End Sub
    Sub binddata()
        dt = objbo.uspCODScopeLD(, hdnSort.Value)
        grdScope.DataSource = dt
        grdScope.PageSize = Session("Page_size")
        grdScope.DataBind()
    End Sub
    Sub Binddetails()
        dt = objbo.uspCODScopeLD(Request.QueryString("id"))
        If dt.Rows.Count > 0 Then
            txtScope.Value = dt.Rows(0).Item("Scope").ToString.Replace("'", "''")
            txtAlias.Value = dt.Rows(0).Item("Alias").ToString.Replace("'", "''")
            tblScope.Visible = True
            btnDelete.Visible = True
        End If
    End Sub
    Sub savedata()
        objET.Scope = txtScope.Value
        objET.Aliass = txtAlias.Value
        objET.AT.RStatus = Constants.STATUS_ACTIVE
        objET.AT.LMBY = Session("User_Name")
    End Sub
    Protected Sub btnCanel_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCanel.ServerClick
        Response.Redirect("frmCODScope.aspx")
    End Sub
    Protected Sub btnNewGroup_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNewGroup.ServerClick
        tdTitle.InnerText = ""
        tblScope.Visible = True
        txtScope.Value = ""
    End Sub
    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        BOcommon.result(objbo.uspDelete("CODScope", "Scope_ID", Request.QueryString("id")), True, "frmCODScope.aspx", " ", Constants._DELETE)
    End Sub
    Protected Sub grdScope_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdScope.PageIndexChanging
        grdScope.PageIndex = e.NewPageIndex
        binddata()
    End Sub

    Protected Sub grdScope_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdScope.RowDataBound
        Select Case e.Row.RowType
            Case DataControlRowType.DataRow
                Dim lbl As Label = CType(e.Row.FindControl("lblNo"), Label)
                lbl.Text = (grdScope.PageIndex * grdScope.PageSize) + (e.Row.RowIndex + 1) & ".&nbsp;"
        End Select
    End Sub
    Protected Sub grdScope_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdScope.Sorting
        If hdnSort.Value = "" Or hdnSort.Value = e.SortExpression & " Desc" Then
            hdnSort.Value = e.SortExpression & " Asc"
        Else
            hdnSort.Value = e.SortExpression & " Desc"
        End If
        binddata()
    End Sub
    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        savedata()
        If Request.QueryString("id") Is Nothing Then
            BOcommon.result(objbo.uspCODScopeIU(objET), True, "frmCODScope.aspx", txtScope.Value, Constants._INSERT)
        Else
            objET.Scope_ID = Request.QueryString("id")
            BOcommon.result(objbo.uspCODScopeIU(objET), True, "frmCODScope.aspx", txtScope.Value, Constants._UPDATE)
        End If
    End Sub
End Class
