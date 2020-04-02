Imports Entities
Imports BusinessLogic
Imports DAO
Imports Common
Imports System.Data
Partial Class MailReport
    Inherits System.Web.UI.Page
    Dim objBO As New BOMailReport
    Dim objET As New ETMailReport
    Dim dt As New DataTable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        btnsave.Attributes.Add("onClick", "javascript:return checkIsEmpty();")
        If Page.IsPostBack = False Then
            BindData()
            If Not Request.QueryString("id") Is Nothing Then
                GetList()
                btnSave.Text = "Update"
                tblMail.Visible = True
                add.InnerText = "Edit"
                td1.InnerText = ""
                btnNewGroup.Disabled = True
            End If
        End If
    End Sub
    Sub BindData()
        dt = objBO.uspMailReportLD(, hdnSort.Value)
        grdMailReport.DataSource = dt
        grdMailReport.DataBind()
    End Sub
    Sub GetList()
        dt = objBO.uspMailReportLD(Request.QueryString("id"), hdnSort.Value)
        'ddlmailtype.SelectedValue  = dt.Rows(0).Item("Sno").ToString
        ddlmailtype.Items.FindByText(dt.Rows(0).Item("MailType").ToString).Selected = True
        ddlmailtype.Enabled = False
        txtsalutation.Text = dt.Rows(0).Item("Salutation").ToString
        txtsubject.Text = dt.Rows(0).Item("Subject").ToString
        txtbody.Text = dt.Rows(0).Item("Body").ToString
        txtclosing.Text = dt.Rows(0).Item("Closing").ToString
        'DDLFrequency.SelectedIndex = dt.Rows(0).Item("Sno").ToString
        DDLFrequency.Items.FindByText(dt.Rows(0).Item("Frequency").ToString).Selected = True
        'DDLFrequency.SelectedItem.Text = dt.Rows(0).Item("Frequency").ToString
        If DDLFrequency.SelectedItem.Text = "Every Upload" Then
            'objET.FrqHours = 0
            Hours.Visible = False
        Else
            Hours.Visible = True
            ' objET.FrqHours = txtHr.Value
            txtHr.Value = dt.Rows(0).Item("FrqHours")
        End If
    End Sub
    Sub SaveData()
        objET.MailType = ddlmailtype.SelectedItem.Text
        objET.Salutation = txtsalutation.Text.Replace("'", "'")
        objET.Subject = txtsubject.Text.Replace("'", "'")
        objET.Body = txtbody.Text.Replace("'", "'")
        objET.Closing = txtclosing.Text.Replace("'", "'")
        objET.Frequency = DDLFrequency.SelectedItem.Text
        If objET.Frequency = "Every Upload" Then
            objET.FrqHours = 0
        Else
            objET.FrqHours = txtHr.Value
        End If
        objET.AT.RStatus = Constants.STATUS_ACTIVE
        objET.AT.LMBY = Session("User_Name")
    End Sub
    'Sub Update()
    '    objET.Sno = Request.QueryString("id")
    '    objET.MailType = ddlmailtype.SelectedItem.Text
    '    objET.Salutation = txtsalutation.Text
    '    objET.Subject = txtsubject.Text
    '    objET.Body = txtbody.Text
    '    objET.Closing = txtclosing.Text
    '    objET.Frequency = DDLFrequency.SelectedItem.Text
    '    If objET.Frequency = "Every Upload" Then
    '        objET.FrqHours = 0
    '    Else
    '        objET.FrqHours = txtHr.Value
    '    End If
    '    objET.AT.RStatus = Constants.STATUS_ACTIVE
    '    objET.AT.LMBY = Session("User_Name")
    'End Sub
    Protected Sub btnsave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If Not Request.QueryString("id") Is Nothing Then
            'Update()
            SaveData()
            objET.Sno = Request.QueryString("id")
            BOcommon.result(objBO.uspMailReportIU(objET), True, "frmMailReport.aspx", ddlmailtype.SelectedItem.Text, Constants._UPDATE)
        Else
            SaveData()
            BOcommon.result(objBO.uspMailReportIU(objET), True, "frmMailReport.aspx", ddlmailtype.SelectedItem.Text, Constants._INSERT)
        End If
    End Sub
    Protected Sub grdMailREport_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdMailReport.PageIndexChanging
        grdMailReport.PageIndex = e.NewPageIndex
        BindData()
    End Sub
    Protected Sub grdMailReport_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdMailReport.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lbl As Label = CType(e.Row.FindControl("lblNo"), Label)
            lbl.Text = (grdMailReport.PageIndex * grdMailReport.PageSize) + (e.Row.RowIndex + 1) & ".&nbsp;"
        End If
    End Sub
    Protected Sub grdMailReport_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdMailReport.Sorting
        If hdnSort.Value = "" Or hdnSort.Value = e.SortExpression & " Desc" Then
            hdnSort.Value = e.SortExpression & " Asc"
        Else
            hdnSort.Value = e.SortExpression & " Desc"
        End If
        BindData()
    End Sub
    Protected Sub btncancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Response.Redirect("frmMailReport.aspx")
    End Sub
    Protected Sub DDLFrequency_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DDLFrequency.SelectedIndexChanged
        If DDLFrequency.SelectedIndex = 1 Then
            Hours.Visible = False
        Else
            Hours.Visible = True
        End If
    End Sub
    Protected Sub btnNewGroup_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNewGroup.ServerClick
        td1.InnerText = ""
        tblMail.Visible = True
    End Sub
End Class
