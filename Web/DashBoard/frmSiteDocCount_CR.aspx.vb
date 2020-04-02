Imports System.Data
Imports System.Data.DataTable
Imports CRFramework
Imports Common

Partial Class DashBoard_frmSiteDocCount_CR
    Inherits System.Web.UI.Page

    Dim controller As New CRController

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        If Not Page.IsPostBack Then
            GetTaskPending(False)
        End If
    End Sub

    Protected Sub BtnViewAllClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnViewAll.Click
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        GetTaskPending(True)
    End Sub

    Protected Sub BtnGoToDashboardClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnGoToDashboard.Click
        Select Case Session("User_Type")
            Case "N"
                Response.Redirect("../frmDashboard_Temp.aspx?from=crpending")
            Case "C"
                Response.Redirect("frmSiteAllTaskPending.aspx")
            Case "S"
                Response.Redirect("../frmDashboard_Temp.aspx?from=crpending")
        End Select
    End Sub


#Region "Custom Methods"
    Private Sub GetTaskPending(ByVal isViewAll As Boolean)
        GrdDocCount.DataSource = controller.GetTaskPendingByUserId(Convert.ToInt32(CommonSite.UserId), Integer.Parse(ConfigurationManager.AppSettings("codocid")), isViewAll)
        GrdDocCount.DataBind()
    End Sub
#End Region

End Class
