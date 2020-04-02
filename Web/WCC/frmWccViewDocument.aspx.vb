Imports BusinessLogic
Imports System.Data
Imports common
Partial Class WCC_frmWccViewDocument
    Inherits System.Web.UI.Page
    Dim strpath As String
    Dim dt As DataTable
    Dim objbo As New BODashBoard
    Dim objdb As New dbutil

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Cache.SetNoStore()
        If Page.IsPostBack = False Then
            'dt = objbo.DigitalSign(Convert.ToInt32(Request.QueryString("id")), "1")
            dt = objdb.ExeQueryDT("exec wccGetDocPath " & Convert.ToInt32(Request.QueryString("id")) & ",1 ", "wccd")
            strpath = dt.Rows(0)("docpath").ToString()
            docView.Attributes.Add("src", ConfigurationManager.AppSettings("WCCVpath") + strpath)
        End If
    End Sub
End Class
