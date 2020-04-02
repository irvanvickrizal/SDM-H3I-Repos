Imports BusinessLogic
Imports System.Data
Imports Common
Partial Class PO_frmSiteReMapList
    Inherits System.Web.UI.Page
    Dim dt As DataTable
    Dim objbo As New BOPODetails
    Dim objdl As New BODDLs
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        If Page.IsPostBack = False Then
            objdl.fillDDL(ddlPO, "PODetails", True, Constants._DDL_Default_All)
            bindData()
        End If
    End Sub
    Sub bindData()
        If ddlPO.SelectedItem.Value = "0" Then
            dt = objbo.uspSiteRemapList(ddlPO.SelectedValue, ddlSelect.SelectedValue, txtSearch.Text.Replace("'", "''"), 1)
        Else
            dt = objbo.uspSiteRemapList(ddlPO.SelectedValue, ddlSelect.SelectedValue, txtSearch.Text.Replace("'", "''"), 0)
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
End Class
