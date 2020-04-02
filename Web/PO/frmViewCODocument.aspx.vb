Imports System.Data
Imports CRFramework
Imports Common

Partial Class PO_frmViewCODocument
    Inherits System.Web.UI.Page

    Dim objdb As New DBUtil
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        If Page.IsPostBack = False Then
            GetPathDocument()
        End If
    End Sub

    Protected Sub BtnRefreshClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        GetPathDocument()
    End Sub

#Region "Custom Methods"
    Private Sub GetPathDocument()
        docView.Attributes.Clear()
        Dim docpath As String = objdb.ExeQueryScalarString("select docpath from sitedoc where sw_id=" & GetSWID())
        BindDocumentView(docpath)
    End Sub

    Private Sub BindDocumentView(ByVal strPath As String)
        docView.Attributes.Add("height", "575px")
        docView.Attributes.Add("width", "100%")
        docView.Attributes.Add("src", ConfigurationManager.AppSettings("Vpath") + strPath.Replace("\", "/"))
    End Sub
    
    Private Function GetSWID() As Int32
        Dim swid As Int32 = IIf(Not String.IsNullOrEmpty(Request.QueryString("swid")), Convert.ToInt32(Request.QueryString("swid")), 0)
        Return swid
    End Function
#End Region
End Class
