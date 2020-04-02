Imports BusinessLogic
Imports System.Data
Imports Common
Partial Class PO_frmPODuplicatewp
    Inherits System.Web.UI.Page
    Dim dt As DataTable
    Dim objbo As New BOPODetails
    Dim objdl As New BODDLs
    Dim objdb As New DBUtil

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Session("Page_size") = 20
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        If Page.IsPostBack = False Then
            bindData()
        End If
    End Sub
    Sub bindData()
        dt = objdb.ExeQueryDT("select po_id,pono,siteno,sitename,workpkgid,workpackagename from podetails P where 1 < ( select count(workpkgid) from podetails PN where P.workpkgid=PN.workpkgid)", "ee")
        grdPOrawdata.DataSource = dt
        grdPOrawdata.PageSize = Session("Page_size")
        grdPOrawdata.DataBind()
    End Sub
    Protected Sub grdPOrawdata_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdPOrawdata.PageIndexChanging
        grdPOrawdata.PageIndex = e.NewPageIndex
        bindData()
    End Sub
End Class
