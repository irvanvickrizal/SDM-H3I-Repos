'Created By : Radha
'Date : 09-10-2008
'Updated By : Dedy
'Date : 05-11-2008
Imports BusinessLogic
Imports Entities
Imports Common
Imports System.Data

Partial Class frmCODSubSection
    Inherits System.Web.UI.Page
    Dim objdo As New ETCODSubSection
    Dim objbo As New BOCODSubSection
    Dim dt As New DataTable
    Dim objdl As New BODDLs

    'Commented by Fauzan, 26 Dec 2018
    'Protected Sub btnNewGroup_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNewGroup.ServerClick
    '    tdTitle.InnerText = ""
    '    tblSubSection.Visible = True
    'End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        btnSave.Attributes.Add("onclick", "javascript:return checkIsEmpty();")
        btnDelete.Attributes.Add("onclick", "javascript:return confirm('Are you sure you want to delete this record?');")

        If Page.IsPostBack = False Then
            objdl.fillDDL(ddlSectionSrc, "CodSection", True, Constants._DDL_Default_Select)
            objdl.fillDDL(ddlSection, "CodSection", True, Constants._DDL_Default_Select)
            binddata()
            'sec_binddata()

            'Commented by Fauzan, 26 Dec 2018
            'If Not Request.QueryString("id") Is Nothing Then
            '    tdTitle.InnerText = ""
            '    addrow.InnerText = "Edit"
            '    btnSave.Text = "Update"
            '    btnNewGroup.Disabled = True
            '    BindDescription()
            'End If
        End If
    End Sub

    Sub binddata()
        dt = objbo.uspCODSubSectionList(, ddlSelect.SelectedValue, txtSearch.Value, ddlSectionSrc.SelectedValue, hdnSort.Value)
        grdSubSection.DataSource = dt
        'grdSubSection.PageSize = 3
        'grdSubSection.PageSize = Session("Page_size")
        grdSubSection.DataBind()
    End Sub

    Sub sec_binddata()
        dt = objbo.uspDDLCODSection()
        ddlSection.DataSource = dt
        ddlSection.DataBind()
        ddlSectionSrc.DataSource = dt
        ddlSectionSrc.DataBind()
    End Sub

    Sub BindDescription()
        dt = objbo.uspCODSubSectionList(Request.QueryString("id"))

        If dt.Rows.Count > 0 Then
            ddlSection.SelectedValue = dt.Rows(0).Item("SEC_ID")
            txtSubSec_Name.Value = dt.Rows(0).Item("SUBSEC_Name").ToString.Replace("'", "''")
            txtSubSec_Desc.InnerText = dt.Rows(0).Item("SUBSEC_Desc").ToString.Replace("'", "''")
            'tblSubSection.Visible = True
            btnDelete.Visible = True
        End If
    End Sub

    Protected Sub grdSubSection_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdSubSection.PageIndexChanging
        grdSubSection.PageIndex = e.NewPageIndex
        binddata()
    End Sub

    Protected Sub grdSubSection_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdSubSection.RowDataBound
        Select Case e.Row.RowType
            Case DataControlRowType.DataRow
                Dim lbl As Label = CType(e.Row.FindControl("lblNo"), Label)
                lbl.Text = (grdSubSection.PageIndex * grdSubSection.PageSize) + (e.Row.RowIndex + 1) & ".&nbsp;"
        End Select
    End Sub

    Protected Sub grdSubSection_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdSubSection.Sorting
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
        objdo.SEC_ID = ddlSection.SelectedValue
        objdo.SUBSEC_Name = txtSubSec_Name.Value
        objdo.SUBSEC_Desc = txtSubSec_Desc.Value
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        fillDescription()
        'If Request.QueryString("id") Is Nothing Then
        '    BOcommon.result(objbo.uspCODSubSectionIU(objdo), True, "frmCODSubSection.aspx", "SubSec_Code", Constants._INSERT)
        'Else
        '    objdo.SUBSEC_ID = Request.QueryString("id")
        '    BOcommon.result(objbo.uspCODSubSectionIU(objdo), True, "frmCODSubSection.aspx", "SubSec_Code", Constants._UPDATE)
        'End If
        'Added by Fauzan, 26 Dec 2018
        If String.IsNullOrEmpty(subSectionId.Value) Then
            BOcommon.result(objbo.uspCODSubSectionIU(objdo), True, "frmCODSubSection.aspx", "SubSec_Code", Constants._INSERT)
        Else
            objdo.SUBSEC_ID = subSectionId.Value
            BOcommon.result(objbo.uspCODSubSectionIU(objdo), True, "frmCODSubSection.aspx", "SubSec_Code", Constants._UPDATE)
        End If
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        binddata()
    End Sub

    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        'BOcommon.result(objbo.uspDelete("CODSubSection", "SUBSEC_ID", Request.QueryString("id")), True, "frmCODSubSection.aspx", " ", Constants._DELETE)
        'Added by Fauzan, 26 Dec 2018
        BOcommon.result(objbo.uspDelete("CODSubSection", "SUBSEC_ID", subSectionId.Value), True, "frmCODSubSection.aspx", " ", Constants._DELETE)
    End Sub

    'Commented by Fauzan, 26 Dec 2018
    'Protected Sub btnCanel_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCanel.ServerClick
    '    Response.Redirect("frmCODSubSection.aspx")
    'End Sub

    Protected Sub ddlSectionSrc_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSectionSrc.SelectedIndexChanged
        binddata()
    End Sub

    Private Sub grdSubSection_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles grdSubSection.RowCommand
        If e.CommandName = "GetDetails" Then
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            If index > -1 Then
                Dim name As LinkButton = DirectCast(grdSubSection.Rows(index).Cells(1).Controls.Item(1), LinkButton)
                Dim Desc As HiddenField = DirectCast(grdSubSection.Rows(index).FindControl("hdnDesc"), HiddenField)
                Dim id As HiddenField = DirectCast(grdSubSection.Rows(index).FindControl("hdnId"), HiddenField)
                dt = objbo.uspCODSubSectionList(id.Value)
                subSectionId.Value = id.Value
                btnDelete.Visible = True
                ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "modalEdit('" & name.Text & "', '" & Desc.Value & "', " _
                    & "'" & dt.Rows(0).Item("SEC_ID").ToString & "');", True)
            End If
        End If
    End Sub
End Class
