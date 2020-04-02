
Partial Class DashBoard_frmSSVL0RC
    Inherits System.Web.UI.Page
    Dim generalcontrol As New GeneralController

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        If Not Page.IsPostBack Then
            'Modified by Fauzan, 30 Nov 2018.
            If CommonSite.DocId(ConfigurationManager.AppSettings("SSVDOCID")) = CInt(ConfigurationManager.AppSettings("SSVDOCID")) Then
                lblTitle.Text = "SSV L0 Report Ready to Submit"
            Else
                lblTitle.Text = "SSV L2 Report Ready to Submit"
            End If
            BindData(CommonSite.DocId(ConfigurationManager.AppSettings("SSVDOCID")), CommonSite.UserId)
        End If
    End Sub

    Protected Sub gvSSVL0RC_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs) Handles gvSSVL0RC.RowCommand
        If e.CommandName.Equals("submitform") Then
            Dim row As GridViewRow = DirectCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
            Dim lblWPID As Label = DirectCast(row.FindControl("lblWPID"), Label)
            Dim lblSiteID As Label = DirectCast(row.FindControl("lblSiteID"), Label)
            Dim lblFldtype As Label = DirectCast(row.FindControl("lblFldtype"), Label)
            Dim lblpoid As Label = DirectCast(row.FindControl("lblpoid"), Label)
            Dim lblSiteno As Label = DirectCast(row.FindControl("lblSiteno"), Label)
            If lblWPID IsNot Nothing Then
                'Modified By Fauzan, 30 Nov 2018.
                'Response.Redirect("frmTreeDocUploadNPO.aspx?docid=" & ConfigurationManager.AppSettings("SSVDOCID") & "&wpid=" & lblWPID.Text & "&siteno=" & lblSiteno.Text & "&from=ssvrc")
                Response.Redirect("frmTreeDocUploadNPO.aspx?docid=" & CommonSite.DocId(ConfigurationManager.AppSettings("SSVDOCID")) & "&wpid=" & lblWPID.Text & "&siteno=" & lblSiteno.Text & "&from=ssvrc")
            End If

        End If
    End Sub

    Protected Sub BtnGoToDashboard_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnGoToDashboard.Click
        Response.Redirect("~/welcome.aspx")
    End Sub

#Region "Custom Methods"
    Private Sub BindData(ByVal docid As Integer, ByVal userid As Integer)
        gvSSVL0RC.DataSource = generalcontrol.Document_ReadyToUploadDetail(docid, userid)
        gvSSVL0RC.DataBind()
    End Sub
#End Region
End Class
