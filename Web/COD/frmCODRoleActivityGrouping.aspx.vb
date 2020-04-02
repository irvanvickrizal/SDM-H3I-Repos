
Partial Class COD_frmCODRoleActivityGrouping
    Inherits System.Web.UI.Page
    Dim rcontroller As New RoleController
    Dim controller As New CODActivityController
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        If Not Page.IsPostBack Then
            BindData()
            BindUserTypes()
        End If
    End Sub

    Protected Sub DdlUserType_IndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DdlUserType.SelectedIndexChanged
        If DdlUserType.SelectedIndex > 0 Then
            BindRoles(Integer.Parse(DdlUserType.SelectedValue))
        Else
            DdlRoles.Items.Clear()
        End If
    End Sub

    Protected Sub DdlRoles_IndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DdlRoles.SelectedIndexChanged
        If DdlRoles.SelectedIndex > 0 Then
            BindActivities()
        Else
            DdlActivities.Items.Clear()
        End If
    End Sub

    Protected Sub BtnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnAdd.Click
        Dim isValid As Boolean = True
        Dim strRemarks As String = String.Empty
        If (DdlUserType.SelectedIndex < 1) Then
            isValid = False
            strRemarks = "Please choose User Type first"
        End If

        If ((DdlRoles.SelectedIndex < 1 Or DdlRoles.Items.Count = 0) And isValid = True) Then
            isValid = False
            strRemarks = "Please choose User Role first!"
        End If

        If ((DdlActivities.SelectedIndex Or DdlActivities.Items.Count = 0) < 1 And isValid = True) Then
            isValid = False
            strRemarks = "Please choose User Activity First!"
        End If

        If isValid = True Then
            pnlWarningMessage(False, strRemarks)
            AddData(Integer.Parse(DdlRoles.SelectedValue), Integer.Parse(DdlActivities.SelectedValue), CommonSite.UserId)
        Else
            pnlWarningMessage(True, strRemarks)
        End If
    End Sub

    Protected Sub BtnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnClear.Click
        ClearForm()
    End Sub

    Protected Sub GvActiviyRolesGroup_ItemCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs) Handles GvActiviyRolesGroup.RowCommand
        If e.CommandName.Equals("DeleteRole") Then
            DeleteData(Integer.Parse(e.CommandArgument.ToString()))
        End If
    End Sub


#Region "Custom Methods"

    Private Sub AddData(ByVal roleid As Integer, ByVal activityid As Integer, ByVal userid As Integer)
        If (controller.GroupingRoleActivity_I(roleid, activityid, userid) = True) Then
            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "SucceedSave();", True)
            ClearForm()
            BindData()
        Else
            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "ErrorSave();", True)
        End If

    End Sub

    Private Sub DeleteData(ByVal roleactivityid As Integer)
        controller.GroupinRoleActivity_D(roleactivityid)
        BindData()
    End Sub

    Private Sub BindData()
        GvActiviyRolesGroup.DataSource = controller.GetGroupingRoleActivities()
        GvActiviyRolesGroup.DataBind()
    End Sub

    Private Sub BindUserTypes()
        DdlUserType.DataSource = rcontroller.GetUserTypes()
        DdlUserType.DataTextField = "UserType"
        DdlUserType.DataValueField = "UserTypeId"
        DdlUserType.DataBind()
        DdlUserType.Items.Insert(0, "-- User Type--")
    End Sub

    Private Sub BindRoles(ByVal grpid As Integer)
        DdlRoles.DataSource = rcontroller.GetRolesByUserType(grpid)
        DdlRoles.DataTextField = "Rolename"
        DdlRoles.DataValueField = "RoleId"
        DdlRoles.DataBind()
        DdlRoles.Items.Insert(0, "-- Roles --")
    End Sub

    Private Sub BindActivities()
        DdlActivities.DataSource = controller.GetCODActivities(False)
        DdlActivities.DataTextField = "ActivityName"
        DdlActivities.DataValueField = "ActivityId"
        DdlActivities.DataBind()
        DdlActivities.Items.Insert(0, "-- Activity --")
    End Sub

    Private Sub pnlWarningMessage(ByVal isVisible As Boolean, ByVal remarks As String)
        'panelwarningmessage.Visible = isVisible
        LblWarningMessage.Text = remarks
        LblWarningMessage.Visible = isVisible
    End Sub

    Private Sub ClearForm()
        DdlUserType.ClearSelection()
        DdlRoles.Items.Clear()
        DdlActivities.Items.Clear()
    End Sub

    Private Sub GvActiviyRolesGroup_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles GvActiviyRolesGroup.PageIndexChanging
        GvActiviyRolesGroup.PageIndex = e.NewPageIndex
        BindData()
    End Sub

#End Region

End Class
