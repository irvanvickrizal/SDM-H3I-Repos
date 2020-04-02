Partial Class Main
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Response.Redirect("frmUnderMaintenance.aspx")'enable this during maintenance only
        If Session("Page_size") Is Nothing Then Response.Redirect("SessionTimeout.aspx")
    End Sub
End Class
