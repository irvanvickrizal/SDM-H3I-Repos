Imports BusinessLogic
Imports Common
Imports System.Data
Partial Class SiteDetails
    Inherits System.Web.UI.Page

    Dim objBO As New BODashBoard
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SiteDetails()
    End Sub
    Sub SiteDetails()
        Dim iMainTable As New HtmlTable

        Dim dtStatus As New DataTable
        dtStatus = objBO.SiteDetails(CommonSite.UserId())

        DlBast.DataSource = dtStatus
        DlBast.DataBind()

    End Sub
End Class
