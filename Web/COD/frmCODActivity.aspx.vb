
Partial Class COD_frmCODActivity
    Inherits System.Web.UI.Page

    Private controller As New CODActivityController

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        If Not Page.IsPostBack Then
            BindData(False)
        End If
    End Sub

    Protected Sub BtnAdd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnAdd.Click
        ClearForm()
        MvCorePanel.SetActiveView(VwModifiedActivity)
        BtnAdd.Visible = False
        BtnUpdate.Visible = False
        BtnSave.Visible = True
    End Sub

    Protected Sub BtnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSave.Click
        Dim info As New CODActivityInfo
        info.ActivityName = TxtActivityName.Text
        info.ActivityDesc = TxtDescription.Text
        info.IsDeleted = False
        info.ActivityId = 0
        info.LMBY = CommonSite.UserId
        AddUpdateData(info)
        BtnAdd.Visible = True
        
    End Sub

    Protected Sub BtnUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnUpdate.Click
        Dim info As New CODActivityInfo
        info.ActivityName = TxtActivityName.Text
        info.ActivityDesc = TxtDescription.Text
        info.IsDeleted = False
        info.ActivityId = Integer.Parse(HdnActivityId.Value)
        info.LMBY = CommonSite.UserId
        AddUpdateData(info)
        BtnAdd.Visible = True
    End Sub

    Protected Sub BtnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnCancel.Click
        ClearForm()
        MvCorePanel.SetActiveView(VwListActivities)
        BtnAdd.Visible = True
    End Sub

    Protected Sub GvActivities_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs) Handles GvActivities.RowCommand
        If e.CommandName.Equals("EditActivity") Then
            Dim info As CODActivityInfo = GetActivity(Integer.Parse(e.CommandArgument.ToString()))
            TxtActivityName.Text = info.ActivityName
            TxtDescription.Text = info.ActivityDesc
            HdnActivityId.Value = Convert.ToString(info.ActivityId)
            BtnAdd.Visible = False
            BtnUpdate.Visible = True
            BtnSave.Visible = False
            MvCorePanel.SetActiveView(VwModifiedActivity)
        ElseIf e.CommandName.Equals("DeleteActivity") Then
            controller.CODActivity_TempDeleted(Integer.Parse(e.CommandArgument.ToString()))
            BindData(False)
        End If
    End Sub

#Region "Custom Methods"
    Private Sub BindData(ByVal isDeleted As Boolean)
        GvActivities.DataSource = controller.GetCODActivities(False)
        GvActivities.DataBind()
        MvCorePanel.SetActiveView(VwListActivities)
    End Sub

    Private Sub AddUpdateData(ByVal info As CODActivityInfo)
        If (controller.CODActivity_IU(info) = True) Then
            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "SucceedSave();", True)
            BindData(False)
        Else
            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "ErrorSave();", True)
        End If

    End Sub

    Private Function GetActivity(ByVal activityid As Integer) As CODActivityInfo
        Return controller.GetCODActivity(activityid)
    End Function

    Private Sub ClearForm()
        TxtActivityName.Text = ""
        TxtDescription.Text = ""
    End Sub

#End Region

End Class
