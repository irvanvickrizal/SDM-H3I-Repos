
Partial Class Admin_frmManagePermission
    Inherits System.Web.UI.Page

    Dim controller As New PermissionController
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        If Not Page.IsPostBack Then
            BindRoles(DdlRoles)
            BindData()
        End If
    End Sub

    Protected Sub BtnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnAdd.Click
        If ValidationChecking() = True Then
            Dim info As New PermissionInfo
            info.PermissionId = 0
            info.PermissionType = DdlPermissionType.SelectedValue
            info.PermissionCategory = DdlPermissionCategory.SelectedValue
            info.RoleId = Integer.Parse(DdlRoles.SelectedValue)
            info.LMBY = CommonSite.UserId
            If IsPermissionAvailable(info.PermissionCategory, info.PermissionType, info.RoleId) = False Then
                AddPermission(info)
            Else
                LblWarningMessage.Text = "This Permission already exist"
                LblWarningMessage.Visible = True
            End If
        End If
    End Sub

    Protected Sub GvPermission_PageIndexing(ByVal sender As Object, ByVal e As GridViewPageEventArgs) Handles GvPermissions.PageIndexChanging
        GvPermissions.PageIndex = e.NewPageIndex
        BindData()
    End Sub

    Protected Sub GvPermissions_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs) Handles GvPermissions.RowCommand
        If e.CommandName.Equals("deletepermission") Then
            DeletePermission(Integer.Parse(e.CommandArgument.ToString()))
        End If
    End Sub

#Region "Custom Methods"
    Private Sub BindData()
        GvPermissions.DataSource = controller.GetODPermissions()
        GvPermissions.DataBind()
    End Sub

    Private Sub BindRoles(ByVal ddl As DropDownList)
        ddl.DataSource = controller.GetRoles()
        ddl.DataTextField = "RoleName"
        ddl.DataValueField = "RoleId"
        ddl.DataBind()

        ddl.Items.Insert(0, "-- select role --")
    End Sub

    Private Sub AddPermission(ByVal info As PermissionInfo)
        Dim isSucceed As Boolean = controller.ODPermission_IU(info)
        If isSucceed = True Then
            LblWarningMessage.Text = "Permission successfully added"
            LblWarningMessage.ForeColor = Drawing.Color.Green
            ClearForm()
            BindData()
        Else
            LblWarningMessage.Text = "Permission failed added"
            LblWarningMessage.ForeColor = Drawing.Color.Red
        End If
        LblWarningMessage.Visible = True
        LblGvDocumentWarningMessage.Visible = False
    End Sub

    Private Sub DeletePermission(ByVal permissionid As Integer)
        Dim isSucceed As Boolean = controller.ODPermission_D(permissionid)
        If isSucceed = True Then
            LblGvDocumentWarningMessage.Text = "Permission successfully deleted"
            LblGvDocumentWarningMessage.ForeColor = Drawing.Color.Green
            BindData()
        Else
            LblGvDocumentWarningMessage.Text = "Failed to delete the Permission"
            LblGvDocumentWarningMessage.ForeColor = Drawing.Color.Red
        End If
        LblWarningMessage.Visible = False
        LblGvDocumentWarningMessage.Visible = True
    End Sub

    Private Function ValidationChecking() As Boolean
        Dim isValid As Boolean = True
        Dim strWarningMessage As String = String.Empty
        If DdlRoles.SelectedIndex < 1 Then
            isValid = False
            strWarningMessage = "Role is Required"
        End If

        If DdlPermissionType.SelectedIndex < 1 And isValid = True Then
            isValid = False
            strWarningMessage = "Permission Type is Required"
        End If

        If DdlPermissionCategory.SelectedIndex < 1 And isValid = True Then
            isValid = False
            strWarningMessage = "Permission Category is Required"
        End If

        If isValid = False Then
            LblWarningMessage.Text = strWarningMessage
            LblWarningMessage.Visible = True
            LblWarningMessage.ForeColor = Drawing.Color.Red
        Else
            LblWarningMessage.Visible = False
        End If

        Return isValid
    End Function

    Private Sub ClearForm()
        BindRoles(DdlRoles)
        DdlPermissionCategory.SelectedValue = "0"
        DdlPermissionType.SelectedValue = "0"
    End Sub

    Private Function IsPermissionAvailable(ByVal permissioncategory As String, ByVal permissiontype As String, ByVal roleid As Integer) As Boolean
        Return controller.IsAvailablePermission(roleid, permissioncategory, permissiontype)
    End Function
#End Region

End Class
