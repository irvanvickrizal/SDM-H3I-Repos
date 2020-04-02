
Partial Class fancybox_Form_fb_viewMergeDocument_ATP
    Inherits System.Web.UI.Page

    Dim controller As New ATPGeoTagController
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If Not String.IsNullOrEmpty(Request.QueryString("id")) Then
                BindDocument(Request.QueryString("id"))
            End If
        End If
    End Sub

#Region "Custom Methods"
    Private Sub BindDocument(ByVal atpphotodocid As String)
        Dim info As ATPDocWithGeoTagMergeInfo = controller.GetATPGeoTagMergeDoc(atpphotodocid)
        BindDocumentView(info.MergeDocPath)
    End Sub
    Private Sub BindDocumentView(ByVal strPath As String)
        docView.Attributes.Add("height", "575px")
        docView.Attributes.Add("width", "100%")
        docView.Attributes.Add("src", ConfigurationManager.AppSettings("Vpath") + strPath.Replace("\", "/"))
    End Sub
#End Region

End Class
