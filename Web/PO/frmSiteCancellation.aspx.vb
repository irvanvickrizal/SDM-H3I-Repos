Imports BusinessLogic
Imports System.Data
Imports Common
Partial Class PO_frmSiteCancellation
    Inherits System.Web.UI.Page
    Dim dt As DataTable
    Dim objbo As New BOPODetails
    Dim objdl As New BODDLs
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        If Page.IsPostBack = False Then
            objdl.fillDDL(ddlPO, "PODetails", False, Constants._DDL_Default_Select)
            If Not Request.QueryString("Id") Is Nothing Then
                Session("PONo") = Request.QueryString("Id")
                ddlPO.SelectedValue = Session("PONo")
            End If
            bindData()
        End If
    End Sub
    Sub bindData()
        dt = objbo.uspPODetailsList4Can(ddlPO.SelectedValue, ddlSelect.SelectedValue, txtSearch.Text.Replace("'", "''"), 0)
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
End Class
