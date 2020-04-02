Imports BusinessLogic
Imports System.Data
Imports Common
Partial Class PO_frmSiteCanList
    Inherits System.Web.UI.Page
    Dim dt As DataTable
    Dim objbo As New BOPODetails
    Dim objdl As New BODDLs
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        If Page.IsPostBack = False Then
            objdl.fillDDL(ddlPO, "PODetails", True, Constants._DDL_Default_All)
            If Not Request.QueryString("Id") Is Nothing Then
                Session("PONo") = Request.QueryString("Id")
                ddlPO.SelectedValue = Session("PONo")
            End If
            bindData()
        End If
    End Sub
    Sub bindData()
        If ddlPO.SelectedItem.Value = "0" Then
            dt = objbo.uspSiteCanList(ddlPO.SelectedValue, ddlSelect.SelectedValue, txtSearch.Text.Replace("'", "''"), 1)
        Else
            dt = objbo.uspSiteCanList(ddlPO.SelectedValue, ddlSelect.SelectedValue, txtSearch.Text.Replace("'", "''"), 0)
        End If
        grdPOrawdata.DataSource = dt
        grdPOrawdata.PageSize = Session("Page_size")
        grdPOrawdata.DataBind()
    End Sub
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        bindData()
    End Sub
    Protected Sub ddlPO_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPO.SelectedIndexChanged
        Session("PONo") = ddlPO.SelectedValue
        bindData()
    End Sub
    Protected Sub grdPOrawdata_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdPOrawdata.PageIndexChanging
        grdPOrawdata.PageIndex = e.NewPageIndex
        bindData()
    End Sub
    Protected Sub grdPOrawdata_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdPOrawdata.RowCreated
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(2).Visible = False
        End If
        If e.Row.RowType = DataControlRowType.Header Then
            e.Row.Cells(2).Visible = False
        End If
    End Sub
    Protected Sub grdPOrawdata_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdPOrawdata.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim url As String
            url = "ViewCancelSiteDetails.aspx?id=" & e.Row.Cells(3).Text & "&Sno=" & e.Row.Cells(2).Text & " &TT=" & "P"
            e.Row.Cells(3).Text = "<a href='#' onclick=""window.open('" & url & "','mywindow','status=yes,menubar=no,center=yes,scrollbars=yes,resizable=yes,width:600px')"">" & e.Row.Cells(3).Text & "</a>"
        End If
    End Sub
    Protected Sub btnClose_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.ServerClick
        Response.Redirect("frmSiteCancellation.aspx")
    End Sub
End Class
