Imports BusinessLogic
Imports Common
Imports System.Data
Imports Entities
Partial Class COD_frmCODCustomerSiteDetailsView
    Inherits System.Web.UI.Page
    Dim dt As DataTable
    Dim objbo As New BOCODSite

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            If Request.QueryString("id") <> Nothing Then
                SiteDetailsView()
            End If
        End If
    End Sub
    Sub SiteDetailsView()
        dt = objbo.uspCODCustomerViewList(Convert.ToInt32(Request.QueryString("id")))
        If dt.Rows.Count > 0 Then
            lblSiteNo.Text = dt.Rows(0)("Site_No").ToString()
            lblSiteName.Text = dt.Rows(0)("Site_Name").ToString()
            lblSiteDesc.Text = dt.Rows(0)("Name").ToString()
            lblEmail.Text = dt.Rows(0)("JV_Name").ToString()
            lblPhone.Text = dt.Rows(0)("ARA_Desc").ToString()
            lblAddress.Text = dt.Rows(0)("RGNName").ToString()
            lblCity.Text = dt.Rows(0)("ZNName").ToString()
        End If
    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Response.Write("<script language='javascript'>window.close();</script>")
    End Sub
End Class
