Imports CRFramework

Partial Class PO_frmViewCRDocument
    Inherits System.Web.UI.Page
    Dim controller As New CRController
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
        BindDocumentView(controller.GetCRDocPath(GetCRID(), GetSubDocId()))
    End Sub

    Private Sub BindDocumentView(ByVal strPath As String)
        docView.Attributes.Add("height", "575px")
        docView.Attributes.Add("width", "100%")
        docView.Attributes.Add("src", ConfigurationManager.AppSettings("Vpath") + strPath.Replace("\", "/"))
    End Sub

    Private Function GetCRID() As Int32
        Dim CRID As Int32 = IIf(Not String.IsNullOrEmpty(Request.QueryString("crid")), Convert.ToInt32(Request.QueryString("crid")), 0)
        Return CRID
    End Function

    Private Function GetSubDocId() As Int32
        Dim Subdocid As Int32 = IIf(Not String.IsNullOrEmpty(Request.QueryString("subdocid")), Convert.ToInt32(Request.QueryString("subdocid")), 0)
        Return Subdocid
    End Function
#End Region
End Class
