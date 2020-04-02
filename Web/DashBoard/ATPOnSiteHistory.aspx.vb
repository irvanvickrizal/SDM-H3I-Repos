
Partial Class DashBoard_ATPOnSiteHistory
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        If Page.IsPostBack = False Then
            LoadATPOnSiteAcceptance()
        End If
    End Sub

    Protected Sub BtnViewAcceptanceClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnViewAcceptance.Click
        LoadATPOnSiteAcceptance()
    End Sub

    Protected Sub BtnViewRejectionClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnViewRejection.Click
        LoadATPOnSiteRejection()
    End Sub

#Region "Custom Methods"
    Private Sub LoadATPOnSiteAcceptance()
        MvATPHistorical.SetActiveView(vwATPOnSiteAcceptance)
    End Sub

    Private Sub LoadATPOnSiteRejection()
        MvATPHistorical.SetActiveView(vwATPOnSiteRejection)
    End Sub
#End Region
End Class
