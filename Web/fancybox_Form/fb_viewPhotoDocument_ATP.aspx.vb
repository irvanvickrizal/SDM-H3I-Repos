
Partial Class fancybox_Form_fb_viewPhotoDocument_ATP
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            BindData(GetID())
        End If
    End Sub

#Region "Custom Methods"
    Private Sub BindData(ByVal id As String)
        Dim controller As New ATPGeoTagController
        Dim info As GeoTagInfo = controller.GetATPPhotoDoc(id)
        BindDocumentView(info.ATPDOCPath)
    End Sub

    Private Sub BindDocumentView(ByVal strPath As String)
        docView.Attributes.Add("height", "575px")
        docView.Attributes.Add("width", "100%")
        docView.Attributes.Add("src", ConfigurationManager.AppSettings("Vpath") + strPath.Replace("\", "/"))
    End Sub

    Private Function GetID() As String
        If Not String.IsNullOrEmpty(Request.QueryString("id")) Then
            Return Request.QueryString("id")
        Else
            Return "0"
        End If
    End Function
#End Region

End Class
