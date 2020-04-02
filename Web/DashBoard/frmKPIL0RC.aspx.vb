
Partial Class DashBoard_frmKPIL0RC
    Inherits System.Web.UI.Page
    Dim generalcontrol As New GeneralController
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        If Not Page.IsPostBack Then
            If CommonSite.DocId(ConfigurationManager.AppSettings("KPIL0DOCID")) = CInt(ConfigurationManager.AppSettings("KPIL0DOCID")) Then
                lblTitle.Text = "KPI L0 Report Ready to Submit"
            Else
                lblTitle.Text = "KPI L2 Report Ready to Submit"
            End If
            BindData(CommonSite.DocId(ConfigurationManager.AppSettings("KPIL0DOCID")), CommonSite.UserId)
        End If
    End Sub

    Protected Sub gvKPIL0RC_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs) Handles gvKPIL0RC.RowCommand
        If e.CommandName.Equals("submitform") Then
            Dim row As GridViewRow = DirectCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
            Dim lblWPID As Label = DirectCast(row.FindControl("lblWPID"), Label)
            If lblWPID IsNot Nothing Then
                Response.Redirect("frmOnlineKPIL0.aspx?id=" & e.CommandArgument.ToString() & "&wpid=" & lblWPID.Text & "&from=rc")
            End If

        End If
    End Sub

    Protected Sub BtnGoToDashboard_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnGoToDashboard.Click
        Response.Redirect("~/welcome.aspx")
    End Sub

    Private Sub gvKPIL0RC_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvKPIL0RC.PageIndexChanging
        gvKPIL0RC.PageIndex = e.NewPageIndex
        BindData(ConfigurationManager.AppSettings("KPIL0DOCID"), CommonSite.UserId)
    End Sub

#Region "Custom Methods"
    Private Sub BindData(ByVal docid As Integer, ByVal userid As Integer)
        gvKPIL0RC.DataSource = generalcontrol.Document_ReadyToUploadDetail(docid, userid)
        gvKPIL0RC.DataBind()
    End Sub
#End Region
End Class
