
Partial Class WCC_frmWorkflowDocSetup_WCC
    Inherits System.Web.UI.Page
    Dim wcccontroller As New WCCController
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            BindData()
            BindWorkflow()
        End If
    End Sub

    Protected Sub GvWorkflows_PageIndexChanging(ByVal sender As Object, ByVal e As GridViewPageEventArgs) Handles GvWorkflows.PageIndexChanging
        GvWorkflows.PageIndex = e.NewPageIndex
        BindWorkflow()
    End Sub

    Protected Sub GvWorkflows_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs) Handles GvWorkflows.RowCommand
        If e.CommandName.Equals("addflow") Then
            AddFlow(Integer.Parse(e.CommandArgument.ToString()))
            BindData()
        End If
    End Sub

#Region "Custom Methods"
    Private Sub BindData()
        GvWCCWorkflow.DataSource = wcccontroller.GetWCCWorkflowGrouping()
        GvWCCWorkflow.DataBind()
    End Sub
    Private Sub BindWorkflow()
        GvWorkflows.DataSource = wcccontroller.GetWorkflows()
        GvWorkflows.DataBind()
    End Sub

    Private Sub AddFlow(ByVal wfid As Integer)
        Dim info As New WCCFlowGroupingInfo
        info.LMBY = CommonSite.UserName
        info.WFID = wfid
        wcccontroller.WorkflowGrouping_I(info)
    End Sub

#End Region

End Class
