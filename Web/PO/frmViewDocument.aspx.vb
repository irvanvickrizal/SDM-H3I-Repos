Imports System
Imports BusinessLogic
Imports System.Data
Imports System.IO
Partial Class PO_frmViewDocument
    Inherits System.Web.UI.Page
    Dim strpath As String
    Dim dt As DataTable
    Dim objbo As New BODashBoard
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        If Page.IsPostBack = False Then
            dt = objbo.DigitalSign(Convert.ToInt32(Request.QueryString("id")), "1")
            strpath = dt.Rows(0)("docpath").ToString()
            docView.Attributes.Add("src", ConfigurationManager.AppSettings("Vpath") + strpath.Replace("\", "/"))
        End If
        'bugfix100806 fixing missing signature after document sign
        If Request.QueryString("reloaded") <> "true" Then
            'Response.AddHeader("Refresh", "0;URL=" + Request.Url.ToString.Replace(Request.Url.ToString()(2).ToString, "www.telkomsel.nsnebast.com") + "&reloaded=true")
        End If
    End Sub
    Protected Sub btnRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        'bugfix100806 fixing missing signature after document sign
        Response.AddHeader("Refresh", "0;URL=" + Request.Url.ToString.Replace(Request.Url.ToString()(2).ToString, "www.telkomsel.nsnebast.com") + "&reloaded=true")
    End Sub
End Class
