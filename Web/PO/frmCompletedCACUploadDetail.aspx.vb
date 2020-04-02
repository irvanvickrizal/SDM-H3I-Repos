
Partial Class PO_frmCompletedCACUploadDetail
    Inherits System.Web.UI.Page

    Dim controller As New HCPTController
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            BindSiteInfo(GetWPID())
        End If
    End Sub

    Protected Sub btnConfirm_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnConfirm.Click

    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancel.Click
        Response.Redirect("frmCompletedCACUpload.aspx")
    End Sub

#Region "custom methods"
    Private Sub BindSiteInfo(ByVal wpid As String)
        Dim info As SiteInfo = controller.GetSiteInfoDetail(wpid)
        If info IsNot Nothing Then
            lblProjectName.Text = info.PONO
            lblServiceType.Text = info.Scope
            lblSiteID.Text = info.SiteNo
            hdnsiteno.Value = info.SiteNo
            lblSiteName.Text = info.SiteName
            hdnpono.Value = info.PONO
            hdnScope.Value = info.Scope
        End If
    End Sub

    Private Function GetWPID() As String
        If Not String.IsNullOrEmpty(Request.QueryString("wpid")) Then
            Return Request.QueryString("wpid")
        Else
            Return "0"
        End If
    End Function

    Private Function GetSWID() As String
        If Not String.IsNullOrEmpty(Request.QueryString("swid")) Then
            Return Request.QueryString("swid")
        Else
            Return "0"
        End If
    End Function
#End Region

End Class
