Imports Common
Partial Class frmLogout
    Inherits System.Web.UI.Page
    Dim objutil As New DBUtil
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If Not Session("User_Id") Is Nothing Then objutil.ExeQueryScalar("Update EBastUsers_1 Set USRPassword = '',IsLogin=0 Where usr_id=" & Session("User_Id"))
        Session.Clear()
        Session.Abandon()
        'Response.Redirect("http://117.102.80.43/NSNTIPilot/Default.aspx")
        Response.Redirect(ConfigurationManager.AppSettings("TIURL"))
        'Response.Redirect("http://localhost/TI/Default.aspx")
    End Sub
End Class