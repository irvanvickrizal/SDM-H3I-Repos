
Partial Class Admin_frmWorkflowSiteBaseGRP
    Inherits System.Web.UI.Page


    Dim controller As New WFSiteBaseGRPController
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        If Not Page.IsPostBack Then
            BindData()
            BindWorkflow()
        End If
    End Sub

    Protected Sub GvWFSiteBaseGroup_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs) Handles GvWFSiteBaseGroup.RowCommand
        If e.CommandName.Equals("deletewf") Then
            DeleteWorkflowGroup(Integer.Parse(e.CommandArgument.ToString()))
        End If
    End Sub

    Protected Sub GvWorkflows_PageIndex(ByVal sender As Object, ByVal e As GridViewPageEventArgs) Handles GvWorkflows.PageIndexChanging
        GvWorkflows.PageIndex = e.NewPageIndex
        BindWorkflow()
    End Sub

    Protected Sub GvWorkflow_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs) Handles GvWorkflows.RowCommand
        If e.CommandName.Equals("addwf") Then
            AddWorkflowGroup(Integer.Parse(e.CommandArgument.ToString()), CommonSite.UserId)
        End If
    End Sub

#Region "Custom Methods"
    Private Sub BindData()
        GvWFSiteBaseGroup.DataSource = controller.GetWFSiteBaseGrouping()
        GvWFSiteBaseGroup.DataBind()
    End Sub

    Private Sub BindWorkflow()
        GvWorkflows.DataSource = controller.GetWorkflowNotInSiteBaseGrouping()
        GvWorkflows.DataBind()
    End Sub

    Private Sub AddWorkflowGroup(ByVal wfid As Integer, ByVal userid As Integer)
        Dim isSucceed As Boolean = controller.WorkflowSiteBaseGrouping_I(wfid, userid)
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

    Private Sub DeleteWorkflowGroup(ByVal sno As Integer)
        Dim isSucceed As Boolean = controller.WFSiteBase_D(sno)
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

    Private Sub GvWFSiteBaseGroup_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles GvWFSiteBaseGroup.PageIndexChanging
        GvWFSiteBaseGroup.PageIndex = e.NewPageIndex
        BindData()
    End Sub
#End Region

End Class
