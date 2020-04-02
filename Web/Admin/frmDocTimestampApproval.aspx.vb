Partial Class Admin_frmDocTimestampApproval
    Inherits System.Web.UI.Page


    Dim controller As New WFSiteBaseGRPController
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        If Not Page.IsPostBack Then
            BindData()
            BindWorkflow()
        End If
    End Sub

    Protected Sub GvTimestampDocument_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs) Handles GvTimestampDocument.RowCommand
        If e.CommandName.Equals("deletewf") Then
            DeleteWorkflowGroup(Integer.Parse(e.CommandArgument.ToString()))
        End If
    End Sub

    Protected Sub GvTimestamp_PageIndex(ByVal sender As Object, ByVal e As GridViewPageEventArgs) Handles GvTimestamp.PageIndexChanging
        GvTimestamp.PageIndex = e.NewPageIndex
        BindWorkflow()
    End Sub

    Protected Sub GvTimestamp_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs) Handles GvTimestamp.RowCommand
        If e.CommandName.Equals("addwf") Then
            AddWorkflowGroup(Integer.Parse(e.CommandArgument.ToString()), CommonSite.UserId)
        End If
    End Sub

#Region "Custom Methods"
    Private Sub BindData()
        GvTimestampDocument.DataSource = controller.GetTimestampDoc_view()
        GvTimestampDocument.DataBind()
    End Sub

    Private Sub BindWorkflow()
        GvTimestamp.DataSource = controller.GetTimestampDoc()
        GvTimestamp.DataBind()
    End Sub

    Private Sub AddWorkflowGroup(ByVal Docid As Integer, ByVal userid As Integer)
        Dim isSucceed As Boolean = controller.Timestamp_I(Docid, userid)
        If isSucceed = True Then
            BindData()
            BindWorkflow()
        Else
            If (Not ClientScript.IsStartupScriptRegistered("alert")) Then
                Page.ClientScript.RegisterStartupScript _
                (Me.GetType(), "alert", "errorInsert();", True)
            End If
        End If
    End Sub

    Private Sub DeleteWorkflowGroup(ByVal TimestampDoc_ID As Integer)
        Dim isSucceed As Boolean = controller.TimestampDoc_D(TimestampDoc_ID)
        If isSucceed = True Then
            BindData()
            BindWorkflow()
        Else
            If (Not ClientScript.IsStartupScriptRegistered("alert")) Then
                Page.ClientScript.RegisterStartupScript _
                (Me.GetType(), "alert", "errorDelete();", True)
            End If
        End If

    End Sub

    Private Sub GvTimestampDocument_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles GvTimestampDocument.PageIndexChanging
        GvTimestampDocument.PageIndex = e.NewPageIndex
        BindData()
    End Sub

#End Region

End Class
