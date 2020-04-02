Imports BusinessLogic
Imports Entities
Imports Common
Imports System.Data
Partial Class COD_frmCODFldType
    Inherits System.Web.UI.Page
    Dim objET As New ETCODFldType
    Dim objBO As New BOCODFldType
    Dim objED As New ETDelete
    Dim objBD As New BODelete
    Dim dt As New DataTable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        btnSave.Attributes.Add("onclick", "javascript:return checkIsEmpty();")
        btnDelete.Attributes.Add("onclick", "javascript:return confirm('Are you sure you want to delete this record?');")
        If Page.IsPostBack = False Then
            binddata()
            If Not Request.QueryString("id") Is Nothing Then
                tdTitle.InnerText = ""
                addrow.InnerText = "Edit"
                btnSave.Text = "Update"
                Binddetails()
            End If
        End If
    End Sub
    Sub binddata()
        dt = objBO.uspCODFldTypeLD(, ddlSelect.SelectedValue, txtSearch.Text, hdnSort.Value)
        grdFldtype.DataSource = dt
        grdFldtype.PageSize = 4 'Session("Page_size")
        grdFldtype.DataBind()
    End Sub
    Sub Binddetails()
        dt = objBO.uspCODFldTypeLD(Request.QueryString("id"))
        If dt.Rows.Count > 0 Then
            txtCode.Value = dt.Rows(0).Item("FLD_CODE").ToString.Replace("'", "''")
            txtDesc.InnerText = dt.Rows(0).Item("FLD_DESC").ToString.Replace("'", "''")
            tblDetails.Visible = True
            btnDelete.Visible = True
        End If
    End Sub

    Sub savedata()
        objET.FLD_Code = txtCode.Value
        objET.FLD_Desc = txtDesc.Value
        objET.AT.RStatus = Constants.STATUS_ACTIVE
        objET.AT.LMBY = "subhash" 'Session("User_Name")
    End Sub

    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim id As Integer = Request.QueryString("id")
        objED.TableName = "CODFldType"
        objED.FieldName = "FLD_ID"
        objED.FieldValue = id
        BOcommon.result(objBD.uspDelete(objED), False, "", "", Constants._DELETE)
        binddata()
        tblDetails.Visible = False
    End Sub

    Protected Sub btnNew_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.ServerClick
        tdTitle.InnerText = ""
        tblDetails.Visible = True
        txtCode.Value = ""
        txtDesc.Value = ""
    End Sub

    Protected Sub btnCancel_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.ServerClick
        Response.Redirect("frmCODFldType.aspx")
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        binddata()
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        savedata()
        If Request.QueryString("id") Is Nothing Then
            BOcommon.result(objBO.uspCODFldTypeIU(objET), True, "frmCODFldType.aspx", "FLD_CODE", Constants._INSERT)
        Else
            objET.FLD_ID = Request.QueryString("id")
            BOcommon.result(objBO.uspCODFldTypeIU(objET), True, "frmCODFldType.aspx", "FLD_CODE", Constants._UPDATE)
        End If
    End Sub

    Protected Sub grdFldtype_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdFldtype.PageIndexChanging
        grdFldtype.PageIndex = e.NewPageIndex
        binddata()
    End Sub

    Protected Sub grdFldtype_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdFldtype.RowDataBound
        Select Case e.Row.RowType
            Case DataControlRowType.DataRow
                Dim lbl As Label = CType(e.Row.FindControl("lblNo"), Label)
                lbl.Text = (grdFldtype.PageIndex * grdFldtype.PageSize) + (e.Row.RowIndex + 1) & ".&nbsp;"
        End Select
    End Sub

    Protected Sub grdFldtype_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdFldtype.Sorting
        If hdnSort.Value = "" Or hdnSort.Value = e.SortExpression & " Desc" Then
            hdnSort.Value = e.SortExpression & " Asc"
        Else
            hdnSort.Value = e.SortExpression & " Desc"
        End If
        binddata()
    End Sub
End Class
