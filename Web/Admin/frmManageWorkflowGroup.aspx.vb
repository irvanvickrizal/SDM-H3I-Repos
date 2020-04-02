
Partial Class Admin_frmManageWorkflowGroup
    Inherits System.Web.UI.Page

    Dim controller As New WFGroupController
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        If Not Page.IsPostBack Then
            BindWorkflow(DdlWorkflow, DdlFormType.SelectedValue())
            BindData(GetFormType(DdlFormType))
        End If
    End Sub

    Protected Sub BtnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnAdd.Click
        If FormChecking() = True Then
            Dim info As New WorkflowGroupInfo
            info.WFGRPID = 0
            info.WFID = Integer.Parse(DdlWorkflow.SelectedValue)
            info.FormType = Convert.ToString(DdlFormType.SelectedValue)
            info.ModifiedUser = CommonSite.UserName
            AddData(info, info.FormType)
        End If
    End Sub


    Protected Sub DdlFormType_IndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DdlFormType.SelectedIndexChanged
        BindWorkflow(DdlWorkflow, DdlFormType.SelectedValue())
    End Sub

    Protected Sub GvWorkflowGroups_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs) Handles GvWorkflowGroups.RowCommand
        If e.CommandName.Equals("deletewfgroup") Then
            DeleteData(Integer.Parse(e.CommandArgument.ToString()))
        End If
    End Sub

#Region "Custom Methods"
    Private Sub BindData(ByVal formtype As String)
        GvWorkflowGroups.DataSource = controller.GetWFGroupsBaseOnFormType(formtype)
        GvWorkflowGroups.DataBind()
    End Sub

    Private Sub BindWorkflow(ByVal ddl As DropDownList, ByVal formtype As String)
        If formtype.Equals("0") Then
            ddl.DataSource = Nothing
            ddl.DataBind()
        Else
            ddl.DataSource = controller.GetWorkflowNotInWorkflowGrouping(formtype)
            ddl.DataTextField = "flowname"
            ddl.DataValueField = "WFID"
            ddl.DataBind()
        End If
        
        ddl.Items.Insert(0, "--select workflow--")
    End Sub

    Private Sub AddData(ByVal info As WorkflowGroupInfo, ByVal formtype As String)
        Dim isSucceed As String = controller.WFGrouping_IU(info)
        If String.IsNullOrEmpty(isSucceed) Then
            BindData(GetFormType(DdlFormType))
            LblWarningMessage.Text = "Successfully to add new workflow as " & formtype & " Form Group"
            LblWarningMessage.ForeColor = Drawing.Color.Green
            LblWarningMessage.Font.Italic = True
            ClearForm()
        Else
            LblWarningMessage.Text = "failed to add new workflow as " & formtype & "group"
            LblWarningMessage.ForeColor = Drawing.Color.Red
            LblWarningMessage.Font.Italic = True
        End If
    End Sub
    Private Sub DeleteData(ByVal wfgrpid As Integer)
        Dim isSucceed As String = controller.WorkflowGroup_D(wfgrpid)
        If String.IsNullOrEmpty(isSucceed) Then
            LblGvWarningMessage.Text = "Workflow deleted successfully"
            LblGvWarningMessage.Visible = True
            LblGvWarningMessage.Font.Italic = True
            LblGvWarningMessage.ForeColor = Drawing.Color.Green
        Else
            LblGvWarningMessage.Text = "Deletion of Workflow failed"
            LblGvWarningMessage.Visible = True
            LblGvWarningMessage.Font.Italic = True
            LblGvWarningMessage.ForeColor = Drawing.Color.Red
        End If
        BindData(GetFormType(DdlFormType))
        LblWarningMessage.Visible = False
    End Sub

    Private Function FormChecking() As Boolean
        Dim isSucceed As Boolean = True
        Dim strWarningMessage As String = String.Empty
        If DdlFormType.SelectedValue = "0" And isSucceed = True Then
            strWarningMessage = "Form Type is Required!"
            isSucceed = False
        End If

        If DdlWorkflow.SelectedIndex = 0 Then
            strWarningMessage = "Workflow is Required!"
            isSucceed = False
        End If

        If isSucceed = False Then
            LblWarningMessage.Text = strWarningMessage
            LblWarningMessage.ForeColor = Drawing.Color.Red
            LblWarningMessage.Font.Italic = True
        End If

        Return isSucceed
    End Function

    Private Sub ClearForm()
        'DdlFormType.SelectedValue = "0"
        BindWorkflow(DdlWorkflow, DdlFormType.SelectedValue())
    End Sub

    Private Function GetFormType(ByVal ddl As DropDownList) As String
        If ddl.SelectedIndex > 0 Then
            Return ddl.SelectedValue
        Else
            Return Nothing
        End If
    End Function

    Private Sub GvWorkflowGroups_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles GvWorkflowGroups.PageIndexChanging
        GvWorkflowGroups.PageIndex = e.NewPageIndex
        BindData(Nothing)
    End Sub
#End Region
End Class
