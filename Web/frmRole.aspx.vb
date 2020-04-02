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
        btnDelete.Attributes.Add("onclick", "javascript:return confirm('Are you sure you want to delete this record?');")   'Added by Fauzan 17 Dec 2018
        'tblSetup.Visible = False
        If Page.IsPostBack = False Then
            objBOD.fillDDL(ddlGroup, "TUserType", True, Constants._DDL_Default_Select)
            GetList()
            'Commented by Fauzan, 17 Dec 2018
            'If Not Request.QueryString("ID") Is Nothing Then
            '    tblSetup.Visible = True
            '    BindData()
            '    btnInsert.Text = "Update"
            '    btnNew.Enabled = False
            'End If
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
        If dt.Rows(0).Item("RoleType").ToString = "False" Then
            RBL.SelectedValue = 0
        Else
            RBL.SelectedValue = 1
        End If
    End Sub
    Sub SaveData()
        'Modified by Fauzan, 17 Dec 2018.
        'If Not Request.QueryString("ID") Is Nothing Then objET.RoleID = Request.QueryString("ID")
        If Not String.IsNullOrEmpty(usrRoleId.Value) Then objET.RoleID = usrRoleId.Value
        objET.GrpID = ddlGroup.SelectedValue
        objET.Rolecode = txtCode.Value.Replace("'", "''")
        objET.RoleDesc = txtDesc.Value
        objET.LVLCode = ddlLevel.SelectedValue
        objET.RoleType = RBL.SelectedValue
        objET.AT.RStatus = Constants.STATUS_ACTIVE
        objET.AT.LMBY = "subhash"
    End Sub
    Protected Sub btnInsert_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnInsert.Click
        'Modified by Fauzan, 17 Dec 2018.
        SaveData()
        'If Not Request.QueryString("ID") Is Nothing Then
        '    BOcommon.result(objBO.uspTRoleIU(objET), True, "frmRole.aspx", "", Constants._UPDATE)
        'Else
        '    BOcommon.result(objBO.uspTRoleIU(objET), True, "frmRole.aspx", "", Constants._INSERT)
        'End If
        'tblSetup.Visible = False
        If Not String.IsNullOrEmpty(usrRoleId.Value) Then
            BOcommon.result(objBO.uspTRoleIU(objET), True, "frmRole.aspx", "", Constants._UPDATE)
        Else
            BOcommon.result(objBO.uspTRoleIU(objET), True, "frmRole.aspx", "", Constants._INSERT)
        End If
    End Sub
    'Commented by Fauzan, 17 Dec 2018
    'Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
    '    Response.Redirect("frmRole.aspx")
    'End Sub
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        GetList()
    End Sub
    'Commented by Fauzan, 17 Dec 2018
    'Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
    '    tblSetup.Visible = True
    '    txtCode.Value = ""
    '    txtDesc.Value = ""
    '    txtSearch.Value = ""
    '    btnInsert.Text = "Save"
    'End Sub
    Protected Sub grdUsrRole_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles grdUsrRole.RowDeleting
        'Dim RoleID As Integer = grdUsrRole.DataKeys(e.RowIndex).Value
        'objED.TableName = "TRole"
        'objED.FieldName = "RoleID"
        'objED.FieldValue = RoleID
        'BOcommon.result(objBD.uspDelete(objED), False, "", "", Constants._DELETE)
        'GetList()
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

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        'Added by Fauzan, 17 Dec 2018
        objED.TableName = "TRole"
        objED.FieldName = "RoleID"
        objED.FieldValue = usrRoleId.Value
        BOcommon.result(objBD.uspDelete(objED), True, "frmRole.aspx", " ", Constants._DELETE)
    End Sub

    Private Sub grdUsrRole_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles grdUsrRole.RowCommand
        If e.CommandName = "GetDetails" Then
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            If index > -1 Then
                Dim code As LinkButton = DirectCast(grdUsrRole.Rows(index).Cells(1).Controls.Item(1), LinkButton)
                Dim Desc As String = grdUsrRole.Rows(index).Cells(2).Text
                Dim id As HiddenField = DirectCast(grdUsrRole.Rows(index).FindControl("hiddenId"), HiddenField)
                dt = objBO.uspTRoleLD(id.Value)
                usrRoleId.Value = id.Value
                If dt.Rows(0).Item("RoleType").ToString = "False" Then
                    RBL.SelectedValue = 0
                Else
                    RBL.SelectedValue = 1
                End If
                btnDelete.Visible = True
                ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "modalEdit('" & code.Text & "', '" & Desc & "', " _
                    & "'" & dt.Rows(0).Item("GrpId").ToString & "', '" & dt.Rows(0).Item("LVLCOde").ToString & "');", True)
            End If
        End If
    End Sub
End Class
