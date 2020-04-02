Imports BusinessLogic
Imports System.Data
Partial Class PO_frmPoListMain
    Inherits System.Web.UI.Page
    Dim dt As DataTable
    Dim objbo As New BOPODetails
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            binddata()
        End If
    End Sub
    Sub binddata()
        dt = objbo.uspGetPOSiteDetailsmain
        grdPOrawdata.DataSource = dt
        grdPOrawdata.DataBind()
    End Sub
End Class
