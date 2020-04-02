Imports BusinessLogic
Imports Common
Imports Entities
Imports System.Data
Partial Class NSN_frmRole
    Inherits System.Web.UI.Page
    Dim objBO As New BORole
    Dim objET As New ETTRole
    Dim objED As New ETDelete
    Dim objBD As New BODelete
    Dim objBOD As New BODDLs
    Dim dt As New DataTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Page_size") Is Nothing Then Response.Redirect("~\SessionTimeout.aspx")
        btnInsert.Attributes.Add("onClick", "javascript:return checkIsEmpty();")
        tblSetup.Visible = False
        If Page.IsPostBack = False Then
            objBOD.fillDDL(ddlGroup, "TUserType", True, Constants._DDL_Default_Select)
            GetList()
            If Not Request.QueryString("ID") Is Nothing Then
                tblSetup.Visible = True
                'objBOD.fillDDL(ddlGroup, "TUserType", True, Constants._DDL_Default_Select)
                BindData()
                btnInsert.Text = "Update"
                btnNew.Enabled = False
            End If
        End If
    End Sub
    Sub GetList()
        dt = objBO.uspTRoleLD(, ddlSelect.SelectedValue, txtSearch.Value, hdnSort.Value)
        grdUsrRole.DataSource = dt
        grdUsrRole.PageSize = Session("Page_size")
        grdUsrRole.DataBind()
    End Sub
    Sub BindData()
        dt = objBO.uspTRoleLD(Request.QueryString("ID"))
        ddlGroup.SelectedValue = dt.Rows(0).Item("GrpId").ToString
        txtCode.Value = dt.Rows(0).Item("Rolecode").ToString
        txtDesc.Value = dt.Rows(0).Item("RoleDesc").ToString
        ddlLevel.SelectedValue = dt.Rows(0).Item("LVLCOde").ToString
        If dt.Rows(0).Item("RoleType").ToString = True Then
            RBL.SelectedValue = 1
        Else
            RBL.SelectedValue = 0
        End If
    End Sub
    Sub SaveData()
        If Not Request.QueryString("ID") Is Nothing Then objET.RoleID = Request.QueryString("ID")
        objET.GrpID = ddlGroup.SelectedValue
        objET.Rolecode = txtCode.Value.Replace("'", "''")
        objET.RoleDesc = txtDesc.Value
        objET.LVLCode = ddlLevel.SelectedValue
        objET.RoleType = RBL.SelectedValue
        objET.AT.Rstatus = Constants.STATUS_ACTIVE
        objET.AT.LMBY = "subhash"
        'Session("User_Name")
    End Sub

    Protected Sub btnInsert_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnInsert.Click
        SaveData()
        If Not Request.QueryString("ID") Is Nothing Then
            BOcommon.result(objBO.uspTRoleIU(objET), True, "frmRole.aspx", "", Constants._UPDATE)
        Else
            BOcommon.result(objBO.uspTRoleIU(objET), True, "frmRole.aspx", "", Constants._INSERT)
        End If
        tblSetup.Visible = False
    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Response.Redirect("frmRole.aspx")
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        GetList()
    End Sub

    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        tblSetup.Visible = True
        txtCode.Value = ""
        txtDesc.Value = ""
        txtSearch.Value = ""
        btnInsert.Text = "Save"
    End Sub
    
    Protected Sub grdUsrRole_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles grdUsrRole.RowDeleting
        Dim RoleID As Integer = grdUsrRole.DataKeys(e.RowIndex).Value
        objED.TableName = "TRole"
        objED.FieldName = "RoleID"
        objED.FieldValue = RoleID
        BOcommon.result(objBD.uspDelete(objED), False, "", "", Constants._DELETE)
        GetList()
    End Sub

    Protected Sub grdUsrRole_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdUsrRole.PageIndexChanging
        grdUsrRole.PageIndex = e.NewPageIndex
        GetList()
    End Sub

    Protected Sub grdUsrRole_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdUsrRole.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lbl As Label = CType(e.Row.FindControl("lblNo"), Label)
            lbl.Text = (grdUsrRole.PageIndex * grdUsrRole.PageSize) + (e.Row.RowIndex + 1) & ".&nbsp;"
        End If
    End Sub

    Protected Sub grdUsrRole_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdUsrRole.Sorting
        If hdnSort.Value = "" Or hdnSort.Value = e.SortExpression & " Desc" Then
            hdnSort.Value = e.SortExpression & " Asc"
        Else
            hdnSort.Value = e.SortExpression & " Desc"
        End If
        GetList()
    End Sub
End Class
