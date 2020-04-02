Imports Common
Partial Class Delegation_frmDelegation
    Inherits System.Web.UI.Page

    Dim controller As New DelegationController
    Dim mailbase As New TakeMail
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        If (Not Page.IsPostBack) Then
            BindData(CommonSite.UserId)
            FunctionChecking(CommonSite.UserId)
            BindSupervisorUser(CommonSite.UserId)
        End If
    End Sub

    Protected Sub BtnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnAdd.Click
        If DdlDelegateUsers.SelectedIndex > 0 Then
            Dim info As New DelegationInfo
            info.UserId = CommonSite.UserId
            info.UserDelegationId = Integer.Parse(DdlDelegateUsers.SelectedValue)
            info.Status = "Active"
            info.LMBY = CommonSite.UserId
            Dim isSucceed As Boolean = controller.Delegation_IU(info)
            If isSucceed = True Then
                SendMailDelegation(info.UserDelegationId, CommonSite.UserId)
                ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "SucceedSave();", True)
                BindClearForm()
            Else
                ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "ErrorSave();", True)
            End If
        End If
    End Sub

    Protected Sub GvDelegations_PageIndexing(ByVal sender As Object, ByVal e As GridViewPageEventArgs) Handles GvDelegations.PageIndexChanging
        GvDelegations.PageIndex = e.NewPageIndex
        FunctionChecking(CommonSite.UserId)
        BindData(CommonSite.UserId)
    End Sub

#Region "custom methods"
    Private Sub BindData(ByVal userid As Integer)
        GvDelegations.DataSource = controller.GetDelegationUser(userid)
        GvDelegations.DataBind()
    End Sub

    Private Sub BindClearForm()
        FunctionChecking(CommonSite.UserId)
        BindData(CommonSite.UserId)
        BindSupervisorUser(CommonSite.UserId)
    End Sub

    Private Sub FunctionChecking(ByVal userid As Integer)
        If (controller.IsInDelegation(userid) = True) Then
            BtnAdd.Visible = False
            DdlDelegateUsers.Enabled = False
            LblWarningMessage.Text = "You are in Delegating User"
            LblWarningMessage.Visible = True
        Else
            LblWarningMessage.Visible = False
            DdlDelegateUsers.Enabled = True
            BtnAdd.Visible = True
        End If
    End Sub

    Private Sub BindSupervisorUser(ByVal userid As Integer)
        DdlDelegateUsers.DataSource = controller.GetSupervisorUser(userid)
        DdlDelegateUsers.DataTextField = "username"
        DdlDelegateUsers.DataValueField = "UserId"
        DdlDelegateUsers.DataBind()
        DdlDelegateUsers.Items.Insert(0, "--select supervisor--")
    End Sub

    Private Sub SendMailDelegation(ByVal userdelegationid As Integer, ByVal userid As Integer)
        Dim info As UserProfile = controller.GetUserDelegation(userdelegationid)
        Dim currentuser As UserProfile = controller.GetUserDelegation(userid)
        Dim sbBody As New StringBuilder
        sbBody.Append("Dear " & info.Fullname & ",<br/> You are in delegation as requested by " & currentuser.Fullname & "<br/><br/>thanks<br/>Powered By EBAST SYSTEM")
        mailbase.SendMailUser(info.Email, sbBody.ToString(), "EBAST Delegation Mail from " & currentuser.Fullname)
    End Sub

#End Region

End Class
