Imports BusinessLogic
Imports Entities
Imports Common
Imports System.Data
Partial Class COD_frmCODJava
    Inherits System.Web.UI.Page
    Dim objdo As New ETJava
    Dim objbo As New BOCODJava
    Dim dt As New DataTable
    Protected Sub btnNewGroup_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNewGroup.ServerClick
        tdTitle.InnerText = ""
        tblJava.Visible = True
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        btnSave.Attributes.Add("onclick", "javascript:return checkIsEmpty();")
        btnDelete.Attributes.Add("onclick", "javascript:return confirm('Are you sure you want to delete this record?');")

        If Page.IsPostBack = False Then
            binddata()
            If Not Request.QueryString("id") Is Nothing Then
                tdTitle.InnerText = ""
                addrow.InnerText = "Edit"
                btnSave.Text = "Update"
                btnNewGroup.Disabled = True
                Binddetails()
            End If
        End If
    End Sub
    Sub binddata()
        dt = objbo.uspCODJavaList(, ddlSelect.SelectedValue, txtSearch.Text, hdnSort.Value)
        grdJava.DataSource = dt
        'grdArea.PageSize = 3
        'grdJava.PageSize = Session("Page_size")
        grdJava.DataBind()
    End Sub

    Sub Binddetails()
        dt = objbo.uspCODJavaList(Request.QueryString("id"))
        If dt.Rows.Count > 0 Then
            txtJavaName.Value = dt.Rows(0).Item("JV_Name").ToString.Replace("'", "''")
            txtJavaDes.InnerText = dt.Rows(0).Item("JV_Desc").ToString.Replace("'", "''")
            tblJava.Visible = True
            btnDelete.Visible = True
        End If
    End Sub

    Protected Sub grdJava_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdJava.PageIndexChanging
        grdJava.PageIndex = e.NewPageIndex
        binddata()
    End Sub

    Protected Sub grdJava_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdJava.RowDataBound
        Select Case e.Row.RowType
            Case DataControlRowType.DataRow
                Dim lbl As Label = CType(e.Row.FindControl("lblNo"), Label)
                lbl.Text = (grdJava.PageIndex * grdJava.PageSize) + (e.Row.RowIndex + 1) & ".&nbsp;"
        End Select
    End Sub

    Protected Sub grdJava_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdJava.Sorting
        If hdnSort.Value = "" Or hdnSort.Value = e.SortExpression & " Desc" Then
            hdnSort.Value = e.SortExpression & " Asc"
        Else
            hdnSort.Value = e.SortExpression & " Desc"
        End If
        binddata()
    End Sub

    Sub filldetails()
        objdo.AT.RStatus = Constants.STATUS_ACTIVE
        objdo.AT.LMBY = Session("User_Name")
        objdo.JV_Name = txtJavaName.Value
        objdo.JV_Desc = txtJavaDes.Value
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        filldetails()
        If Request.QueryString("id") Is Nothing Then
            BOcommon.result(objbo.uspCODJavaIU(objdo), True, "frmCODjava.aspx", "JV_Name", Constants._INSERT)
        Else
            objdo.JV_ID = Request.QueryString("id")
            BOcommon.result(objbo.uspCODJavaIU(objdo), True, "frmCODjava.aspx", "JV_Name", Constants._UPDATE)
        End If
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        binddata()
    End Sub

    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        BOcommon.result(objbo.uspDelete("CODJava", "JV_ID", Request.QueryString("id")), True, "frmCODJava.aspx", " ", Constants._DELETE)
    End Sub

    Protected Sub btnCanel_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCanel.ServerClick
        Response.Redirect("frmCodJava.aspx")
    End Sub
End Class

