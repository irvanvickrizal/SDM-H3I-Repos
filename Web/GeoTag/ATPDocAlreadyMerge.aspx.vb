
Partial Class GeoTag_ATPDocAlreadyMerge
    Inherits System.Web.UI.Page

    Dim controller As New ATPGeoTagController
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        If Not Page.IsPostBack Then
            BindData()
        End If
    End Sub

    Protected Sub GvHistoricalMergeDoc_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles GvHistoricalMergeDoc.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim viewdoclink As HtmlAnchor = CType(e.Row.FindControl("viewdoclink"), HtmlAnchor)
            Dim LblATPPhotoDocId As Label = CType(e.Row.FindControl("LblATPPhotoDocId"), Label)

            If Not LblATPPhotoDocId Is Nothing And Not viewdoclink Is Nothing Then
                viewdoclink.HRef = "../fancybox_form/fb_viewMergeDocument_ATP.aspx?id=" & LblATPPhotoDocId.Text
            End If
        End If
    End Sub

#Region "Custom Methods"
    Private Sub BindData()
        GvHistoricalMergeDoc.DataSource = controller.GetHistoricalATPMergeDocBySubcon(PartnerController.GetSubconIdByUser(CommonSite.UserId))
        GvHistoricalMergeDoc.DataBind()
    End Sub
#End Region

End Class
