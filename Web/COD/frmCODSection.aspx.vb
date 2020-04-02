'Created By : Radha
'Date : 09-10-2008
'Updated By : Dedy
'Date : 04-11-2008
Imports BusinessLogic
Imports Entities
Imports Common
Imports System.Data

Partial Class frmCODSection
    Inherits System.Web.UI.Page
    Dim objdo As New ETCODSection
    Dim objbo As New BOCODSection
    Dim dt As New DataTable

    'Commented by Fauzan, 26 Dec 2018
    'Protected Sub btnNewGroup_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNewGroup.ServerClick
    '    tdTitle.InnerText = ""
    '    tblSection.Visible = True
    'End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Page_size") Is Nothing Then Response.Redirect("~\SessionTimeout.aspx")
        btnSave.Attributes.Add("onclick", "javascript:return checkIsEmpty();")
        btnDelete.Attributes.Add("onclick", "javascript:return confirm('Are you sure you want to delete this record?');")
        If Page.IsPostBack = False Then
            binddata()
            'Commented by Fauzan, 26 Dec 2018
            'If Not Request.QueryString("id") Is Nothing Then
            '    tdTitle.InnerText = ""
            '    addrow.InnerText = "Edit "
            '    btnSave.Text = "Update"
            '    btnNewGroup.Disabled = True
            '    BindDescription()
            'End If
        End If
    End Sub

    Sub binddata()
        dt = New DataTable
        dt = objbo.uspCODSectionList(, ddlSelect.SelectedValue, txtSearch.Text, hdnSort.Value)
        grdSection.DataSource = dt
        'grdSection.PageSize = Session("Page_size")
        grdSection.DataBind()
        'grdSection.Columns(2).Visible = False
    End Sub

    Sub BindDescription()
        dt = objbo.uspCODSectionList(Request.QueryString("id"))
        If dt.Rows.Count > 0 Then
            txtSec_Name.Value = dt.Rows(0).Item("SEC_Name").ToString.Replace("'", "''")
            'txtSec_Desc.InnerText = dt.Rows(0).Item("SEC_Desc").ToString.Replace("'", "''")
            'tblSection.Visible = True
            btnDelete.Visible = True
        End If
    End Sub

    Protected Sub grdSection_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdSection.PageIndexChanging
        grdSection.PageIndex = e.NewPageIndex
        binddata()
    End Sub

    Protected Sub grdSection_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdSection.RowDataBound
        Select Case e.Row.RowType
            Case DataControlRowType.DataRow
                Dim lbl As Label = CType(e.Row.FindControl("lblNo"), Label)
                lbl.Text = (grdSection.PageIndex * grdSection.PageSize) + (e.Row.RowIndex + 1) & ".&nbsp;"
        End Select
    End Sub

    Protected Sub grdSection_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdSection.Sorting
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
        objdo.SEC_Name = txtSec_Name.Value
        objdo.SEC_Desc = txtSec_Desc.Value
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        fillDescription()
        'If Request.QueryString("id") Is Nothing Then
        '    BOcommon.result(objbo.uspCODSectionIU(objdo), True, "frmCODSection.aspx", "Section", Constants._INSERT)
        'Else
        '    objdo.SEC_ID = Request.QueryString("id")
        '    BOcommon.result(objbo.uspCODSectionIU(objdo), True, "frmCODSection.aspx", "Section", Constants._UPDATE)
        'End If
        'Added by Fauzan, 26 Dec 2018
        If String.IsNullOrEmpty(sectionId.Value) Then
            BOcommon.result(objbo.uspCODSectionIU(objdo), True, "frmCODSection.aspx", "Section", Constants._INSERT)
        Else
            objdo.SEC_ID = sectionId.Value
            BOcommon.result(objbo.uspCODSectionIU(objdo), True, "frmCODSection.aspx", "Section", Constants._UPDATE)
        End If
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        binddata()
    End Sub

    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        'BOcommon.result(objbo.uspDelete("CODSection", "SEC_ID", Request.QueryString("id")), True, "frmCODSection.aspx", " ", Constants._DELETE)
        'Added by Fauzan, 26 Dec 2018
        BOcommon.result(objbo.uspDelete("CODSection", "SEC_ID", sectionId.Value), True, "frmCODSection.aspx", " ", Constants._DELETE)
    End Sub

    Private Sub grdSection_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles grdSection.RowCommand
        If e.CommandName = "GetDetails" Then
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            If index > -1 Then
                Dim name As LinkButton = DirectCast(grdSection.Rows(index).Cells(1).Controls.Item(1), LinkButton)
                Dim id As HiddenField = DirectCast(grdSection.Rows(index).FindControl("hdnId"), HiddenField)
                Dim description As HiddenField = DirectCast(grdSection.Rows(index).FindControl("hdnDesc"), HiddenField)
                sectionId.Value = id.Value
                btnDelete.Visible = True
                ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "modalEdit('" & name.Text & "', '" & description.Value & "');", True)
            End If
        End If
    End Sub

    'Commented by Fauzan, 26 Dec 2018
    'Protected Sub btnCanel_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCanel.ServerClick
    '    Response.Redirect("frmCODSection.aspx")
    'End Sub
End Class
