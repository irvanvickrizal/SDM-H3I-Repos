Imports BusinessLogic
Imports Entities
Imports Common
Imports System.Data

Partial Class NSN_frmUserType 
    Inherits System.Web.UI.Page
    Dim objBO As New BOUserType
    Dim objET As New ETTUserType
    Dim objED As New ETDelete
    Dim objBD As New BODelete
    Dim dt As New DataTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Page_size") Is Nothing Then Response.Redirect("~\SessionTimeout.aspx")
        btnInsert.Attributes.Add("onClick", "javascript:return checkIsEmpty();")
        btnDelete.Attributes.Add("onclick", "javascript:return confirm('Are you sure you want to delete this record?');")   'Added by Fauzan 17 Dec 2018
        'tblSetup.Visible = False
        If Page.IsPostBack = False Then
            GetList()
            'Commented by Fauzan, 17 Dec 2018
            'If Not Request.QueryString("ID") Is Nothing Then
            '    tblSetup.Visible = True
            '    BindData()
            '    btnInsert.Text = "Update"
            'End If
        End If
    End Sub
    Sub GetList()
        dt = objBO.uspTUserTypeLD(, ddlSelect.SelectedValue, txtSearch.Value, hdnSort.Value)
        grdUsrGrp.DataSource = dt
        grdUsrGrp.DataBind()
    End Sub
    Sub BindData()
        dt = objBO.uspTUserTypeLD(Request.QueryString("ID"))
        txtCode.Value = dt.Rows(0).Item("GrpCode").ToString()
        txtDesc.Value = dt.Rows(0).Item("GrpDesc").ToString()
    End Sub
    Sub SaveData()
        'Modified by Fauzan, 17 Dec 2018.
        'If Not Request.QueryString("ID") Is Nothing Then objET.GrpId = Request.QueryString("ID")
        If Not String.IsNullOrEmpty(usrTypeId.Value) Then objET.GrpId = usrTypeId.Value
        objET.GrpCode = txtCode.Value.Replace("'", "''")
        objET.GrpDesc = txtDesc.Value
        objET.AT.RStatus = Constants.STATUS_ACTIVE
        objET.AT.LMBY = "subhash" 'Session("User_Name")
    End Sub

    Protected Sub btnInsert_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnInsert.Click
        'Modified by Fauzan, 17 Dec 2018.
        SaveData()
        'If Not Request.QueryString("ID") Is Nothing Then
        '    BOcommon.result(objBO.uspTUserTypeIU(objET), True, "frmUserType.aspx", "", Constants._UPDATE)
        'Else
        '    BOcommon.result(objBO.uspTUserTypeIU(objET), True, "frmUserType.aspx", "", Constants._INSERT)
        'End If
        'tblSetup.Visible = False
        If Not String.IsNullOrEmpty(usrTypeId.Value) Then
            BOcommon.result(objBO.uspTUserTypeIU(objET), True, "frmUserType.aspx", "", Constants._UPDATE)
        Else
            BOcommon.result(objBO.uspTUserTypeIU(objET), True, "frmUserType.aspx", "", Constants._INSERT)
        End If
    End Sub

    Protected Sub grdUsrGrp_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles grdUsrGrp.RowDeleting
        'Commented by Fauzan, 17 Dec 2018
        'Dim GrpId As Integer = grdUsrGrp.DataKeys(e.RowIndex).Value
        'objED.TableName = "TUserType"
        'objED.FieldName = "GrpId"
        'objED.FieldValue = GrpId
        'BOcommon.result(objBD.uspDelete(objED), False, "", "", Constants._DELETE)
        'GetList()
    End Sub

    Protected Sub grdUsrGrp_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdUsrGrp.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lbl As Label = CType(e.Row.FindControl("lblNo"), Label)
            lbl.Text = (grdUsrGrp.PageIndex * grdUsrGrp.PageSize) + (e.Row.RowIndex + 1) & ".&nbsp;"
        End If
    End Sub

    'Commented by Fauzan, 17 Dec 2018
    'Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
    '    tblSetup.Visible = True
    '    txtCode.Value = ""
    '    txtDesc.Value = ""
    '    txtSearch.Value = ""
    '    btnInsert.Text = "Save"
    'End Sub

    'Commented by Fauzan, 17 Dec 2018
    'Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
    '    Response.Redirect("frmUserType.aspx")
    'End Sub

    Protected Sub grdUsrGrp_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdUsrGrp.PageIndexChanging
        grdUsrGrp.PageIndex = e.NewPageIndex
        GetList()
    End Sub

    Protected Sub grdUsrGrp_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdUsrGrp.Sorting
        If hdnSort.Value = "" Or hdnSort.Value = e.SortExpression & " Desc" Then
            hdnSort.Value = e.SortExpression & " Asc"
        Else
            hdnSort.Value = e.SortExpression & " Desc"
        End If
        GetList()
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        GetList()
    End Sub

    Private Sub grdUsrGrp_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles grdUsrGrp.RowCommand
        If e.CommandName = "GetDetails" Then
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            If index > -1 Then
                Dim code As LinkButton = DirectCast(grdUsrGrp.Rows(index).Cells(1).Controls.Item(1), LinkButton)
                Dim Desc As String = grdUsrGrp.Rows(index).Cells(2).Text
                Dim id As HiddenField = DirectCast(grdUsrGrp.Rows(index).FindControl("hiddenId"), HiddenField)
                usrTypeId.Value = id.Value
                btnDelete.Visible = True
                ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "modalEdit('" & code.Text & "', '" & Desc & "');", True)
            End If
        End If
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        'Added by Fauzan, 17 Dec 2018
        objED.TableName = "TUserType"
        objED.FieldName = "GrpId"
        objED.FieldValue = usrTypeId.Value
        BOcommon.result(objBD.uspDelete(objED), True, "frmUserType.aspx", " ", Constants._DELETE)
    End Sub
End Class
