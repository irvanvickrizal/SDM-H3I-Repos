Imports BusinessLogic
Imports Common
Imports Entities
Imports System.Data
Partial Class USR_frmCustomerList
    Inherits System.Web.UI.Page
    Dim objBODP As New BODDLs
    Dim objBOD As New BODDLs
    Dim objBO As New BOUserLD
    Dim dt As New DataTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Page_size") Is Nothing Then Response.Redirect("~\SessionTimeout.aspx")
        If Page.IsPostBack = False Then
            objBODP.fillDDL(ddlUsertype, "TUserType1", True, Constants._DDL_Default_Select)
            objBOD.fillDDL(ddlRole, "TRole", True, Constants._DDL_Default_Select)

            If Session("User_Type") = Constants.User_Type_Customer Then
                ddlUsertype.SelectedValue = Session("User_Type")
                ddlUsertype.Enabled = False
            End If

            If UCase(Request.QueryString("SelMode")) = "TRUE" Then
                ddlUsertype.SelectedValue = Constants.User_Type_Customer
                ddlRole.SelectedValue = Constants.Customer_SS_RoleID
                ddlRole.Enabled = False
                ddlUsertype.Enabled = False
            End If
            BindData()
        End If
        If Request.QueryString("id") > 0 And Request.QueryString("SelMode") = "True" Then
            Dim retVal As String = Request.QueryString("id") & "####" & Request.QueryString("SS")
            Response.Write("<script>window.returnValue = '" + retVal + "';window.close();</script>")
        End If
    End Sub
    Sub BindData()
        Dim src As Integer
        If ddlUsertype.SelectedValue = Constants.User_Type_Customer Then
            src = 0
        End If
        dt = objBO.uspEBASTUsersLD1(0, ddlUsertype.SelectedValue, ddlSelect.SelectedValue, txtSearch.Value, hdnSort.Value, Session("User_Id"), src, ddlRole.SelectedValue)
        grdUserlist.PageSize = 3   'Session("Page_size")
        grdUserlist.DataSource = dt
        grdUserlist.DataBind()
        If Request.QueryString("SelMode") = "true" Then
            grdUserlist.Columns(1).Visible = False
            grdUserlist.Columns(2).Visible = True
        Else
            grdUserlist.Columns(1).Visible = True
            grdUserlist.Columns(2).Visible = False
        End If
    End Sub

    Protected Sub btnGo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGo.Click
        BindData()
    End Sub

    Protected Sub btnCreate_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCreate.ServerClick
        Response.Redirect("frmUserSetup.aspx")
    End Sub

    Protected Sub grdUserlist_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdUserlist.Sorting
        If hdnSort.Value = "" Or hdnSort.Value = e.SortExpression & " Desc" Then
            hdnSort.Value = e.SortExpression & " Asc"
        Else
            hdnSort.Value = e.SortExpression & " Desc"
        End If
        BindData()
    End Sub

    Protected Sub grdUserlist_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdUserlist.RowDataBound
        Select Case e.Row.RowType
            Case DataControlRowType.DataRow
                Dim lbl As Label = CType(e.Row.Cells(0).FindControl("lblNo"), Label)
                lbl.Text = grdUserlist.PageIndex * grdUserlist.PageSize + (e.Row.RowIndex + 1) & ".&nbsp;"
        End Select
    End Sub
End Class
