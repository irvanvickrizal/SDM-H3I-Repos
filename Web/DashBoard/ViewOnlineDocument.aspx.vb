Imports BusinessLogic
Imports Common
Imports System.Data
Partial Class DashBoard_ViewOnlineDocument
    Inherits System.Web.UI.Page
    Dim objBo As New BODashBoard
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Response.Cache.SetNoStore()
        If Page.IsPostBack = False Then
            GetPDFDocument()

        End If
      
    End Sub
    Sub GetPDFDocument()
        Dim dt As DataTable
        dt = objBo.CustomerDigitalSign(Request.QueryString("siteno").Trim, Request.QueryString("version"))

        ' dt = objBo.DigitalSign(Convert.ToInt32(7))
        If (dt.Rows.Count > 0) Then
            PDFViwer.Attributes.Add("src", ConfigurationManager.AppSettings("Vpath") + dt.Rows(0)("docpath").ToString())
        End If


    End Sub
End Class
