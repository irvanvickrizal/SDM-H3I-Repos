Imports System.Data
Imports BusinessLogic
Imports Common
Partial Class PO_frmPORawList
    Inherits System.Web.UI.Page
    Dim objbo As New BOPoUpload
    Dim dt As New DataTable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            bindData()
        End If
    End Sub
    Sub BindData()
        grdPOrawdata.DataSource = objbo.uspPORawList()
        grdPOrawdata.DataBind()
    End Sub
End Class
