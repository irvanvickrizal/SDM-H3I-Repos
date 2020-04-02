Imports BusinessLogic
Imports System.Data
Partial Class CR_frmViewDocument
    Inherits System.Web.UI.Page
    Dim strpath As String
    Dim dt As DataTable
    Dim objbo As New BODashBoard
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            dt = objbo.MOMView(Convert.ToInt32(Request.QueryString("id")))
            docView.Attributes.Add("src", ConfigurationManager.AppSettings("Vpath") + dt.Rows(0)("pdfpath").ToString())
        End If
    End Sub
End Class
