Imports BusinessLogic
Imports Entities
Imports Common
Imports System.Data
Partial Class COD_frmCODCurrency
    Inherits System.Web.UI.Page
    Dim objdo As New ETCODCurrency
    Dim objbo As New BOCODCurrency
    Dim dt As New DataTable

    'Commented by Fauzan, 26 Dec 2018
    'Protected Sub btnNewGroup_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNewGroup.ServerClick
    '    tdTitle.InnerText = ""
    '    tblCurrency.Visible = True
    'End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        btnSave.Attributes.Add("onclick", "javascript:return checkIsEmpty();")
        btnDelete.Attributes.Add("onclick", "javascript:return confirm('Are you sure you want to delete this record?');")
        If Page.IsPostBack = False Then
            binddata()
            'Commented by Fauzan, 26 Dec 2018
            'If Not Request.QueryString("id") Is Nothing Then
            '    tdTitle.InnerText = ""
            '    addrow.InnerText = "Edit"
            '    btnSave.Text = "Update"
            '    btnNewGroup.Disabled = True
            '    Binddetails()
            'End If
        End If
    End Sub
    Sub binddata()
        dt = objbo.uspCODCurrencyList(, ddlSelect.SelectedValue, txtSearch.Value, hdnSort.Value)
        grdCurrency.DataSource = dt
        'grdCurrency.PageSize = 5
        'grdCurrency.PageSize = Session("Page_size")
        grdCurrency.DataBind()
    End Sub
    Sub Binddetails()
        dt = objbo.uspCODCurrencyList(Request.QueryString("id"))
        If dt.Rows.Count > 0 Then
            txtCurrcode.Value = dt.Rows(0).Item("Code").ToString.Replace("'", "''")
            txtCurrDes.Value = dt.Rows(0).Item("Description").ToString.Replace("'", "''")
            txtExCurr.Value = dt.Rows(0).Item("Exchange_Curr").ToString.Replace("'", "''")
            'tblCurrency.Visible = True
            btnDelete.Visible = True
        End If
    End Sub
    Sub Savedetails()
        objdo.AT.RStatus = Constants.STATUS_ACTIVE
        objdo.AT.LMBY = Session("User_Name")
        objdo.Code = txtCurrcode.Value
        objdo.Description = txtCurrDes.Value
        objdo.Exchange_Curr = txtExCurr.Value
    End Sub
    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Savedetails()
        'If Request.QueryString("id") Is Nothing Then
        '    BOcommon.result(objbo.uspCODCurrencyIU(objdo), True, "frmCODCurrency.aspx", "Code", Constants._INSERT)
        'Else
        '    objdo.Curr_ID = Request.QueryString("id")
        '    BOcommon.result(objbo.uspCODCurrencyIU(objdo), True, "frmCODCurrency.aspx", "Code", Constants._UPDATE)
        'End If
        'Added by Fauzan, 26 Dec 2018
        If String.IsNullOrEmpty(currId.Value) Then
            BOcommon.result(objbo.uspCODCurrencyIU(objdo), True, "frmCODCurrency.aspx", "Code", Constants._INSERT)
        Else
            objdo.Curr_ID = currId.Value
            BOcommon.result(objbo.uspCODCurrencyIU(objdo), True, "frmCODCurrency.aspx", "Code", Constants._UPDATE)
        End If
    End Sub
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        binddata()
    End Sub
    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        'BOcommon.result(objbo.uspDelete("CODCurrency", "Curr_ID", Request.QueryString("id")), True, "frmCODCurrency.aspx", " ", Constants._DELETE)
        'Added by Fauzan, 26 Dec 2018
        BOcommon.result(objbo.uspDelete("CODCurrency", "Curr_ID", currId.Value), True, "frmCODCurrency.aspx", " ", Constants._DELETE)
    End Sub

    'Commented by Fauzan, 26 Dec 2018
    'Protected Sub btnCanel_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCanel.ServerClick
    '    Response.Redirect("frmCodCurrency.aspx")
    'End Sub
    Protected Sub grdCurrency_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdCurrency.PageIndexChanging
        grdCurrency.PageIndex = e.NewPageIndex
        binddata()
    End Sub
    Protected Sub grdCurrency_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdCurrency.RowDataBound
        Select Case e.Row.RowType
            Case DataControlRowType.DataRow
                Dim lbl As Label = CType(e.Row.FindControl("lblNo"), Label)
                lbl.Text = (grdCurrency.PageIndex * grdCurrency.PageSize) + (e.Row.RowIndex + 1) & ".&nbsp;"
        End Select
    End Sub
    Protected Sub grdCurrency_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdCurrency.Sorting
        If hdnSort.Value = "" Or hdnSort.Value = e.SortExpression & " Desc" Then
            hdnSort.Value = e.SortExpression & " Asc"
        Else
            hdnSort.Value = e.SortExpression & " Desc"
        End If
        binddata()
    End Sub

    Private Sub grdCurrency_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles grdCurrency.RowCommand
        If e.CommandName = "GetDetails" Then
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            If index > -1 Then
                Dim code As LinkButton = DirectCast(grdCurrency.Rows(index).Cells(1).Controls.Item(1), LinkButton)
                Dim Desc As String = grdCurrency.Rows(index).Cells(2).Text
                Dim exchangeCurr As String = grdCurrency.Rows(index).Cells(3).Text
                Dim id As HiddenField = DirectCast(grdCurrency.Rows(index).FindControl("hdnId"), HiddenField)
                currId.Value = id.Value
                btnDelete.Visible = True
                ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "modalEdit('" & code.Text & "', '" & Desc & "', '" & exchangeCurr & "');", True)
            End If
        End If
    End Sub
End Class



