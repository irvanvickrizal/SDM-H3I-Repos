Imports Common
Imports BusinessLogic
Imports Entities
Imports System.Data
Partial Class COD_frmCustomer
    Inherits System.Web.UI.Page
    Dim objdl As New BODDLs
    Dim objdo As New ETCODSite
    Dim objbo As New BOCODSite
    Dim dt As New DataTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            btnSave.Attributes.Add("onclick", "javascript:return checkIsEmpty();")
            objdl.fillDDL(DDLZone, "CODZone", True, Constants._DDL_Default_Select)
            BindData()
            If Not Request.QueryString("id") Is Nothing Then
                rowadd.InnerText = "Edit"
                binddetails()
            End If
        End If
        If hdncsname.Value <> "" Then
            txtSSName.Value = hdncsname.Value
        End If
    End Sub
    Sub BindData()
        dt = objbo.uspCodCustomerSupList(, DDLZone.SelectedValue, ddlSelect.SelectedValue, txtSearch.Text, hdnSort.Value)
        grdCustomer.PageSize = Session("Page_size")
        grdCustomer.DataSource = dt
        grdCustomer.DataBind()
    End Sub
    Sub binddetails()
        dt = objbo.uspCodCustomerList(Request.QueryString("id"))
        If dt.Rows.Count > 0 Then
            txtNo.Value = dt.Rows(0).Item("Site_No").ToString
            txtName.Value = dt.Rows(0).Item("Site_Name").ToString
            txtSTDesc.Value = dt.Rows(0).Item("Site_Desc").ToString
            txtSSName.Value = dt.Rows(0).Item("Name").ToString
            tblSite.Visible = True
        End If
    End Sub
    Protected Sub btnCancel_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.ServerClick
        Response.Redirect("frmCODCustomerList.aspx")
    End Sub
    Sub fillDetails()
        With objdo
            'dt = objbo.uspCodCustomerList(Request.QueryString("id"))
            'If dt.Rows.Count > 0 Then
            .AT.RStatus = Constants.STATUS_ACTIVE
            .AT.LMBY = "SysAdmin" 'Session("User_Name")
            'txtSSName.Value = dt.Rows(0).Item("Name").ToString
            'End If
        End With
    End Sub
    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        fillDetails()
        Dim chk As HtmlInputCheckBox
        Dim strid As String = ""

        For i As Integer = 0 To grdCustomer.Rows.Count - 1
            chk = CType(grdCustomer.Rows(i).Cells(1).FindControl("chkBxSelect"), HtmlInputCheckBox)
            If chk.Checked = True Then
                strid = strid & IIf(strid = "", "", ",") & "'" & chk.Value & "'"
            End If
        Next
        If strid <> "" Then
            strid = strid.Replace("'", "''")
            BOcommon.result(objbo.uspCODCustomerIU(hdnSupId.Value, strid, objdo.AT.RStatus, objdo.AT.LMBY), True, "frmCODCustomerList.aspx", "Site_Name", Constants._INSERT)
            'Response.Redirect("frmCODCustomerList.aspx")
        Else
            Response.Write("<script language='javascript'>alert('please select site')</script>")
        End If
    End Sub
    Protected Function SelectCheckBoxEnabled(ByVal siteId As Object) As Boolean
        Dim returnvalue As Boolean = True
        Try
            If Not (siteId Is DBNull.Value) Then
                Dim reccount As Integer = 0
                reccount = objbo.uspCODCustomerCheckedList(siteId)
                If reccount > 0 Then
                    returnvalue = False
                End If
            End If
        Catch ex As Exception

        End Try
        Return returnvalue
    End Function
    Protected Sub grdCustomer_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdCustomer.Sorting
        If hdnSort.Value = "" Or hdnSort.Value = e.SortExpression & " Desc" Then
            hdnSort.Value = e.SortExpression & " Asc"
        Else
            hdnSort.Value = e.SortExpression & " Desc"
        End If
        BindData()
    End Sub

    Protected Sub grdCustomer_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdCustomer.RowDataBound
        Select Case e.Row.RowType
            Case DataControlRowType.DataRow
                Dim lbl As Label = CType(e.Row.Cells(0).FindControl("lblNo"), Label)
                lbl.Text = grdCustomer.PageIndex * grdCustomer.PageSize + (e.Row.RowIndex + 1) & ".&nbsp;"
        End Select
    End Sub

    Protected Sub grdCustomer_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdCustomer.PageIndexChanging
        grdCustomer.PageIndex = e.NewPageIndex
        '        txtSSName.Value = hdncsname.Value
        BindData()
    End Sub

    Protected Sub grdCustomer_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdCustomer.RowCreated
        If e.Row.RowType = DataControlRowType.DataRow AndAlso e.Row.RowState = DataControlRowState.Normal OrElse e.Row.RowState = DataControlRowState.Alternate Then

            Dim chkBxSelect As HtmlInputCheckBox = DirectCast(e.Row.Cells(1).FindControl("chkBxSelect"), HtmlInputCheckBox)
            Dim chkBxHeader As CheckBox = DirectCast(Me.grdCustomer.HeaderRow.FindControl("chkBxHeader"), CheckBox)

            chkBxSelect.Attributes("onclick") = String.Format("javascript:ChildClick(this,'{0}')", chkBxHeader.ClientID)
        End If
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        BindData()
    End Sub

    Protected Sub DDLZone_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DDLZone.SelectedIndexChanged
        BindData()
    End Sub
End Class
