'Created By : Radha
'Date : 09-10-2008
'Updated By : Dedy
'Date : 07-11-2008
Imports BusinessLogic
Imports Entities
Imports Common
Imports System.Data

Partial Class frmTDstGroup
    Inherits System.Web.UI.Page
    Dim objdo As New ETTDstGroup
    Dim objbo As New BOTDstGroup
    Dim dt As New DataTable

    'Commented by Fauzan, 17 Dec 2018
    'Protected Sub btnNewGroup_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNewGroup.ServerClick
    '    tblTDstGroup.Visible = True
    'End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Page_size") Is Nothing Then Response.Redirect("~\SessionTimeout.aspx")
        btnSave.Attributes.Add("onclick", "javascript:return checkIsEmpty();")
        btnDelete.Attributes.Add("onclick", "javascript:return confirm('Are you sure you want to delete this record?');")

        If Page.IsPostBack = False Then
            binddata()

            'If Not Request.QueryString("id") Is Nothing Then
            '    addrow.InnerText = "Distribution Group Edit"
            '    btnSave.Text = "Update"
            '    btnNewGroup.Disabled = True
            '    BindDescription()
            'End If
        End If
    End Sub

    Sub binddata()
        dt = objbo.uspTDstGroupList(, ddlSelect.SelectedValue, txtSearch.Value, hdnSort.Value)
        grdTDstGroup.DataSource = dt
        'grdTDstGroup.PageSize = 3
        grdTDstGroup.PageSize = Session("Page_size")
        grdTDstGroup.DataBind()
    End Sub

    Sub BindDescription()
        dt = objbo.uspTDstGroupList(Request.QueryString("id"))

        If dt.Rows.Count > 0 Then
            txtDS_Name.Value = dt.Rows(0).Item("DS_Name").ToString.Replace("'", "''")
            txtDS_Desc.Value = dt.Rows(0).Item("DS_Desc").ToString.Replace("'", "''")
            'tblTDstGroup.Visible = True
            btnDelete.Visible = True
        End If
    End Sub

    Protected Sub grdTDstGroup_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdTDstGroup.PageIndexChanging
        grdTDstGroup.PageIndex = e.NewPageIndex
        binddata()
    End Sub

    Protected Sub grdTDstGroup_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdTDstGroup.RowDataBound
        Select Case e.Row.RowType
            Case DataControlRowType.DataRow
                Dim lbl As Label = CType(e.Row.FindControl("lblNo"), Label)
                lbl.Text = (grdTDstGroup.PageIndex * grdTDstGroup.PageSize) + (e.Row.RowIndex + 1) & ".&nbsp;"
        End Select
    End Sub

    Protected Sub grdTDstGroup_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdTDstGroup.Sorting
        If hdnSort.Value = "" Or hdnSort.Value = e.SortExpression & " Desc" Then
            hdnSort.Value = e.SortExpression & " Asc"
        Else
            hdnSort.Value = e.SortExpression & " Desc"
        End If
        binddata()
    End Sub

    Sub fillDescription()
        objdo.AT.RStatus = Constants.STATUS_ACTIVE
        objdo.AT.LMBY = Session("User_Name")
        objdo.DS_Name = txtDS_Name.Value
        objdo.DS_Desc = txtDS_Desc.Value
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        'Modified by Fauzan, 17 Dec 2018
        fillDescription()
        'If Request.QueryString("id") Is Nothing Then
        '    BOcommon.result(objbo.uspTDstGroupIU(objdo), True, "frmTDstGroup.aspx", "DS_Name", Constants._INSERT)
        'Else
        '    objdo.DS_Id = Request.QueryString("id")
        '    BOcommon.result(objbo.uspTDstGroupIU(objdo), True, "frmTDstGroup.aspx", "DS_Name", Constants._UPDATE)
        'End If
        If String.IsNullOrEmpty(DSId.Value) Then
            BOcommon.result(objbo.uspTDstGroupIU(objdo), True, "frmTDstGroup.aspx", "DS_Name", Constants._INSERT)
        Else
            objdo.DS_Id = DSId.Value
            BOcommon.result(objbo.uspTDstGroupIU(objdo), True, "frmTDstGroup.aspx", "DS_Name", Constants._UPDATE)
        End If
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        binddata()
    End Sub

    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        'Modified by Fauzan, 17 Dec 2018
        'BOcommon.result(objbo.uspDelete("TDstGroup", "DS_ID", Request.QueryString("id")), True, "frmTDstGroup.aspx", " ", Constants._DELETE)
        BOcommon.result(objbo.uspDelete("TDstGroup", "DS_ID", DSId.Value), True, "frmTDstGroup.aspx", " ", Constants._DELETE)
    End Sub

    Private Sub grdTDstGroup_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles grdTDstGroup.RowCommand
        If e.CommandName = "GetDetails" Then
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            If index > -1 Then
                Dim code As LinkButton = DirectCast(grdTDstGroup.Rows(index).Cells(1).Controls.Item(1), LinkButton)
                Dim Desc As String = grdTDstGroup.Rows(index).Cells(2).Text
                Dim id As HiddenField = DirectCast(grdTDstGroup.Rows(index).FindControl("hiddenId"), HiddenField)
                DSId.Value = id.Value
                btnDelete.Visible = True
                ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "modalEdit('" & code.Text & "', '" & Desc & "');", True)
            End If
        End If
    End Sub

    'Commented by Fauzan, 17 Dec 2018
    'Protected Sub btnCanel_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCanel.ServerClick
    '    Response.Redirect("frmTDstGroup.aspx")
    'End Sub
End Class
