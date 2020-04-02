'Created By : Radha
'Date : 09-10-2008
'Updated By : Dedy
'Date : 07-11-2008
Imports BusinessLogic
Imports Entities
Imports Common
Imports System.Data

Partial Class frmCustomer
    Inherits System.Web.UI.Page
    Dim objdo As New ETCustomer
    Dim objbo As New BOCustomer
    Dim dt As New DataTable

    'Protected Sub btnNewGroup_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNewGroup.ServerClick
    '    'tblCustomer.Visible = True
    'End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Page_size") Is Nothing Then Response.Redirect("~\SessionTimeout.aspx")
        btnSave.Attributes.Add("onclick", "javascript:return checkIsEmpty();")
        btnDelete.Attributes.Add("onclick", "javascript:return confirm('Are you sure you want to delete this record?');")
        If Page.IsPostBack = False Then
            binddata()
            txtCus_Name.Disabled = False
            If Not Request.QueryString("id") Is Nothing Then
                'Commented by Fauzan, 12 Dec 2018
                'addrow.InnerText = "Customer Edit"
                'btnSave.Text = "Update"
                'btnNewGroup.Disabled = True
                BindDescription()
            End If
        End If
    End Sub

    Sub binddata()
        dt = objbo.uspCustomerLD(, ddlSelect.SelectedValue, txtSearch.Text, hdnSort.Value)
        grdCustomer.DataSource = dt
        'grdCustomer.PageSize = 3
        ' grdCustomer.PageSize = Session("Page_size")
        grdCustomer.DataBind()
    End Sub

    Sub BindDescription()
        dt = objbo.uspCustomerLD(Request.QueryString("id"))
        If dt.Rows.Count > 0 Then
            txtCus_Name.Value = dt.Rows(0).Item("CUS_Name").ToString.Replace("'", "''")
            txtCus_Addr.Value = dt.Rows(0).Item("CUS_Addr").ToString.Replace("'", "''")
            'tblCustomer.Visible = True 'Commented by Fauzan, 12 Dec 2018
            btnDelete.Visible = True
        End If
    End Sub

    Protected Sub grdCustomer_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdCustomer.PageIndexChanging
        grdCustomer.PageIndex = e.NewPageIndex
        binddata()
    End Sub

    Protected Sub grdCustomer_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdCustomer.RowDataBound
        Select Case e.Row.RowType
            Case DataControlRowType.DataRow
                Dim lbl As Label = CType(e.Row.FindControl("lblNo"), Label)
                lbl.Text = (grdCustomer.PageIndex * grdCustomer.PageSize) + (e.Row.RowIndex + 1) & ".&nbsp;"
        End Select
    End Sub

    Protected Sub grdCustomer_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdCustomer.Sorting
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
        objdo.CUS_NAME = txtCus_Name.Value
        objdo.CUS_ADDR = txtCus_Addr.Value
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        fillDescription()
        'Commented by Fauzan, 12 Dec 2018.
        'If Request.QueryString("id") Is Nothing Then
        '    BOcommon.result(objbo.uspCustomerIU(objdo), True, "frmCustomer.aspx", "Customer Name", Constants._INSERT)
        'Else
        '    objdo.CUS_ID = Request.QueryString("id")
        '    BOcommon.result(objbo.uspCustomerIU(objdo), True, "frmCustomer.aspx", "Customer Name", Constants._UPDATE)
        'End If

        'Added by Fauzan, 12 Dec 2018
        If String.IsNullOrEmpty(cusId.Value) Then
            BOcommon.result(objbo.uspCustomerIU(objdo), True, "frmCustomer.aspx", "Customer Name", Constants._INSERT)
        Else
            objdo.CUS_ID = cusId.Value
            BOcommon.result(objbo.uspCustomerIU(objdo), True, "frmCustomer.aspx", "Customer Name", Constants._UPDATE)
        End If
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        binddata()
    End Sub

    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        'BOcommon.result(objbo.uspDelete("Customer", "CUS_ID", Request.QueryString("id")), True, "frmCustomer.aspx", " ", Constants._DELETE)
        'Added by Fauzan, 12 Dec 2018
        BOcommon.result(objbo.uspDelete("Customer", "CUS_ID", cusId.Value), True, "frmCustomer.aspx", " ", Constants._DELETE)
    End Sub

    Private Sub grdCustomer_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles grdCustomer.RowCommand
        If e.CommandName = "GetDetails" Then
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            If index > -1 Then
                Dim name As LinkButton = DirectCast(grdCustomer.Rows(index).Cells(1).Controls.Item(1), LinkButton)
                Dim address As String = grdCustomer.Rows(index).Cells(2).Text
                Dim id As HiddenField = DirectCast(grdCustomer.Rows(index).FindControl("hiddenId"), HiddenField) 'DirectCast(grdCustomer.Rows(index).Cells(3).Controls.Item(1), HiddenField)
                Dim obj As Object = New System.Dynamic.ExpandoObject
                obj.Name = name.Text
                obj.Address = address
                cusId.Value = id.Value
                btnDelete.Visible = True
                ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "modalEdit('" & obj.Name & "', '" & obj.Address & "');", True)
            End If
        End If
    End Sub
    'Protected Sub btnCanel_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCanel.ServerClick
    '    Response.Redirect("frmCustomer.aspx")
    'End Sub
End Class
