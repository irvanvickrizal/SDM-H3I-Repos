
Partial Class COD_WCCUserRegistration
    Inherits System.Web.UI.Page

    Private controller As New WCCReviewerController
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        If Not Page.IsPostBack Then
            BindRoles()
            BindDocRoles()
            BindData(GetRoleType(DdlRoles))
        End If
    End Sub

    Protected Sub GvUsers_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs) Handles GvUsers.RowCommand
        If e.CommandName.Equals("deletereguser") Then
            controller.WCCReviewer_D(Integer.Parse(e.CommandArgument.ToString()))
            BindData(GetRoleType(DdlRoles))
        End If
    End Sub

    Protected Sub DdlRoles_IndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DdlRoles.SelectedIndexChanged
        BindData(GetRoleType(DdlRoles))
    End Sub

    Protected Sub DdlRoleSave_IndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DdlRoleSave.SelectedIndexChanged
        If DdlRoleSave.SelectedIndex > 0 Then
            Dim scopes As String() = DdlRoleSave.SelectedItem.Text.Split("-")
            BindAreaRegion(scopes(1), LblLevelCode, DdlLevelCode)
        Else
            LblLevelCode.Text = "Nothing"
            DdlLevelCode.DataSource = Nothing
            DdlLevelCode.DataBind()
        End If


    End Sub

    Protected Sub BtnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSearch.Click
        If DdlRoleSave.SelectedIndex > 0 Then
            Dim scopes As String() = DdlRoleSave.SelectedItem.Text.Split("-")
            If DdlLevelCode.SelectedIndex > 0 Then
                If scopes(1).Equals("R") Then
                    BindUserRoleType(Integer.Parse(DdlRoleSave.SelectedValue), 0, Integer.Parse(DdlLevelCode.SelectedValue))
                ElseIf scopes(1).Equals("A") Then
                    BindUserRoleType(Integer.Parse(DdlRoleSave.SelectedValue), Integer.Parse(DdlLevelCode.SelectedValue), 0)
                End If
            Else
                BindUserRoleType(Integer.Parse(DdlRoleSave.SelectedValue), 0, 0)
            End If
        Else
            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "NYRoleSelect();", True)
        End If
    End Sub

    Protected Sub GvUserFlow_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs) Handles GvUserFlow.RowCommand
        If e.CommandName.Equals("AddUser") Then
            Dim info As New WCCReviewerInfo
            info.UserId = Integer.Parse(e.CommandArgument.ToString())
            info.LMBY = CommonSite.UserId
            AddUser(info)
        End If
    End Sub

#Region "Custom Methods"

    Private Sub BindData(ByVal roleid As Integer)
        GvUsers.DataSource = controller.GetWCCReviewer(roleid)
        GvUsers.DataBind()
    End Sub

    Private Sub BindRoles()
        DdlRoles.DataSource = controller.GetRoleInWCCGrouping()
        DdlRoles.DataTextField = "RoleName"
        DdlRoles.DataValueField = "RoleId"
        DdlRoles.DataBind()

        DdlRoles.Items.Insert(0, "-- All --")
    End Sub

    Private Sub BindDocRoles()
        DdlRoleSave.DataSource = controller.GetRoleInWCCGrouping()
        DdlRoleSave.DataTextField = "RoleName"
        DdlRoleSave.DataValueField = "RoleId"
        DdlRoleSave.DataBind()

        DdlRoleSave.Items.Insert(0, "-- Select --")
    End Sub

    Private Sub BindAreaRegion(ByVal lvlcode As String, ByVal lbl As Label, ByVal ddl As DropDownList)
        If lvlcode.Equals("R") Then
            lbl.Text = "Region Type"
        ElseIf lvlcode.Equals("A") Then
            lbl.Text = "Area Type"
        ElseIf lvlcode.Equals("Z") Then
            lbl.Text = "Zone Type"
        Else
            lbl.Text = "National Type"
        End If
        DdlLevelCode.DataSource = controller.GetAreaRegion(lvlcode)
        ddl.DataTextField = "RoleName"
        ddl.DataValueField = "RoleId"
        ddl.DataBind()
        ddl.Items.Insert(0, "-- All --")

    End Sub

    Private Sub BindUserRoleType(ByVal roleid As Integer, ByVal areaid As Integer, ByVal regionid As Integer)
        GvUserFlow.DataSource = controller.GetUserByRole(roleid, areaid, regionid)
        GvUserFlow.DataBind()
    End Sub

    Private Function GetRoleType(ByVal ddl As DropDownList) As Integer
        If ddl.SelectedIndex > 0 Then
            Return Integer.Parse(ddl.SelectedValue)
        Else
            Return 0
        End If
    End Function

    Private Sub AddUser(ByVal info As WCCReviewerInfo)
        Dim isSucceed As Boolean = controller.WCCReviewer_I(info)
        If isSucceed = True Then
            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "SucceedSave();", True)
            Dim scopes As String() = DdlRoleSave.SelectedItem.Text.Split("-")
            If DdlLevelCode.SelectedIndex > 0 Then
                If scopes(1).Equals("R") Then
                    BindUserRoleType(Integer.Parse(DdlRoleSave.SelectedValue), 0, Integer.Parse(DdlLevelCode.SelectedValue))
                ElseIf scopes(1).Equals("A") Then
                    BindUserRoleType(Integer.Parse(DdlRoleSave.SelectedValue), Integer.Parse(DdlLevelCode.SelectedValue), 0)
                End If
            Else
                BindUserRoleType(Integer.Parse(DdlRoleSave.SelectedValue), 0, 0)
            End If
            BindData(GetRoleType(DdlRoles))
        Else
            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "ErrorSave();", True)
        End If
        
    End Sub

#End Region

End Class
