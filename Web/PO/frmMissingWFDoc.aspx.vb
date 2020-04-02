Imports System.Data
Partial Class PO_frmMissingWFDoc
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack = True Then
            Response.Cache.SetNoStore()
            grdwfmisdoc.DataSource = Session("jkdt")
            grdwfmisdoc.DataBind()
        End If
    End Sub

    Protected Sub btnOK_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOK.Click
        Session.Remove("jkdt")
        Response.Write("<script> window.close()</script>")
    End Sub
End Class
