
Partial Class fancybox_Form_fb_ViewDocumentCoReplacement
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        BindDocument(GetCOID())
    End Sub


#Region "Custom Methods"
    Private Sub BindDocument(ByVal coid As Int32)
        Dim transcontroller As New COTransactionNAController
        Dim info As CODocumentReplacementInfo = transcontroller.GetCODocumentReplacement(coid)
        BindDocumentView(info.DocPath)
    End Sub

    Private Sub BindDocumentView(ByVal strPath As String)
        docView.Attributes.Add("height", "575px")
        docView.Attributes.Add("width", "100%")
        docView.Attributes.Add("src", ConfigurationManager.AppSettings("Vpath") + strPath.Replace("\", "/"))
    End Sub

    Private Function GetCOID() As Int32
        If Not String.IsNullOrEmpty(Request.QueryString("coid")) Then
            Return Convert.ToInt32(Request.QueryString("coid"))
        End If
        Return 0
    End Function
#End Region

End Class
