
Partial Class fancybox_Form_fb_ATPMergeDocViewLog
    Inherits System.Web.UI.Page
    Dim controller As New ATPGeoTagController
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If Not String.IsNullOrEmpty(Request.QueryString("id")) Then
                BindData(Request.QueryString("id"))
            End If
        End If
    End Sub

#Region "Custom Methods"
    Private Sub BindData(ByVal atpphotodocid As String)
        GvAuditLog.DataSource = controller.GetATPMergeDocLog(atpphotodocid)
        GvAuditLog.DataBind()
    End Sub
#End Region

End Class
