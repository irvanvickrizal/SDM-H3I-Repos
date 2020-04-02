'Created By : Radha
'Date : 09-10-2008
'Updated By : Dedy
'Date : 07-11-2008
Imports BusinessLogic
Imports Entities
Imports Common
Imports System.Data

Partial Class frmSubCon
    Inherits System.Web.UI.Page
    Dim objdo As New ETSubCon
    Dim objbo As New BOSubCon
    Dim dt As New DataTable

    'Protected Sub btnNewGroup_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNewGroup.ServerClick
    '    tblSubCon.Visible = True
    'End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Page_size") Is Nothing Then Response.Redirect("~\SessionTimeout.aspx")
        btnSave.Attributes.Add("onclick", "javascript:return checkIsEmpty();")
        btnDelete.Attributes.Add("onclick", "javascript:return confirm('Are you sure you want to delete this record?');")
        If Page.IsPostBack = False Then
            binddata()
            txtSubCon_Name.Disabled = False
            If Not Request.QueryString("id") Is Nothing Then
                'addrow.InnerText = "Sub Contractor Edit"
                'btnSave.Text = "Update"
                'btnNewGroup.Disabled = True
                BindDescription()
            End If
        End If
    End Sub

    Sub binddata()
        dt = objbo.uspSubConList(, ddlSelect.SelectedValue, txtSearch.Text, hdnSort.Value)
        grdSubCon.DataSource = dt
        'grdSubCon.PageSize = Session("Page_size")
        grdSubCon.DataBind()
    End Sub

    Sub BindDescription()
        dt = objbo.uspSubConList(Request.QueryString("id"))
        If dt.Rows.Count > 0 Then
            txtSubCon_Name.Value = dt.Rows(0).Item("SCON_Name").ToString.Replace("'", "''")
            txtSubCon_Addr.Value = dt.Rows(0).Item("SCON_Addr").ToString.Replace("'", "''")
            'tblSubCon.Visible = True
            btnDelete.Visible = True
        End If
    End Sub

    Protected Sub grdSubCon_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdSubCon.PageIndexChanging
        grdSubCon.PageIndex = e.NewPageIndex
        binddata()
    End Sub

    Protected Sub grdSubCon_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdSubCon.RowDataBound
        Select Case e.Row.RowType
            Case DataControlRowType.DataRow
                Dim lbl As Label = CType(e.Row.FindControl("lblNo"), Label)
                lbl.Text = (grdSubCon.PageIndex * grdSubCon.PageSize) + (e.Row.RowIndex + 1) & ".&nbsp;"
        End Select
    End Sub

    Protected Sub grdSubCon_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdSubCon.Sorting
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
        objdo.SCON_NAME = txtSubCon_Name.Value
        objdo.SCON_ADDR = txtSubCon_Addr.Value
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        fillDescription()
        'Commented by Fauzan, 13 Dec 2018
        'If Request.QueryString("id") Is Nothing Then
        '    BOcommon.result(objbo.uspSubConIU(objdo), True, "frmSubCon.aspx", "SubCon Name", Constants._INSERT)
        'Else
        '    objdo.SCON_ID = Request.QueryString("id")
        '    BOcommon.result(objbo.uspSubConIU(objdo), True, "frmSubCon.aspx", "SubCon Name", Constants._UPDATE)
        'End If
        'Added by Fauzan, 13 Dec 2018
        If String.IsNullOrEmpty(sConId.Value) Then
            BOcommon.result(objbo.uspSubConIU(objdo), True, "frmSubCon.aspx", "SubCon Name", Constants._INSERT)
        Else
            objdo.SCON_ID = sConId.Value
            BOcommon.result(objbo.uspSubConIU(objdo), True, "frmSubCon.aspx", "SubCon Name", Constants._UPDATE)
        End If
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        binddata()
    End Sub

    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        'BOcommon.result(objbo.uspDelete("SubCon", "SCON_ID", Request.QueryString("id")), True, "frmSubCon.aspx", " ", Constants._DELETE)
        'Added by Fauzan, 12 Dec 2018
        BOcommon.result(objbo.uspDelete("SubCon", "SCON_ID", sConId.Value), True, "frmSubCon.aspx", " ", Constants._DELETE)
    End Sub

    Private Sub grdSubCon_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles grdSubCon.RowCommand
        If e.CommandName = "GetDetails" Then
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            If index > -1 Then
                Dim name As LinkButton = DirectCast(grdSubCon.Rows(index).Cells(1).Controls.Item(1), LinkButton)
                Dim address As String = grdSubCon.Rows(index).Cells(2).Text
                Dim id As HiddenField = DirectCast(grdSubCon.Rows(index).FindControl("hiddenId"), HiddenField) 'DirectCast(grdCustomer.Rows(index).Cells(3).Controls.Item(1), HiddenField)
                Dim obj As Object = New System.Dynamic.ExpandoObject
                obj.Name = name.Text
                obj.Address = address
                sConId.Value = id.Value
                btnDelete.Visible = True
                ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "modalEdit('" & obj.Name & "', '" & obj.Address & "');", True)
            End If
        End If
    End Sub

    'Protected Sub btnCanel_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCanel.ServerClick
    '    Response.Redirect("frmSubCon.aspx")
    'End Sub
End Class
