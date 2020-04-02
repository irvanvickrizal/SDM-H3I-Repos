
Partial Class frmUserActivityLog
    Inherits System.Web.UI.Page

    Dim controller As New HCPTController

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        If Not Page.IsPostBack Then
            BindData()
        End If
    End Sub

    Protected Sub grdActivityLog_RowDataBound(ByVal sender As Object, ByVal e As GridViewPageEventArgs) Handles grdActivityLog.PageIndexChanging
        grdActivityLog.PageIndex = e.NewPageIndex
        BindData()
    End Sub


#Region "Custom Methods"
    Private Sub BindData()
        grdActivityLog.DataSource = controller.UserLogActivity_LD(CommonSite.UserId)
        grdActivityLog.DataBind()
    End Sub
#End Region

End Class
