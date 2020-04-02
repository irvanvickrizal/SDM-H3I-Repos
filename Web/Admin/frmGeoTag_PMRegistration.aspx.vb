Imports System.Text.RegularExpressions
Imports System.Text

Partial Class Admin_frmGeoTag_PMRegistration
    Inherits System.Web.UI.Page

    Dim controller As New RegistrationController
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        If Not Page.IsPostBack Then
            BindRegion()
            BindSubcon()
            BindData()
        End If
    End Sub

    Protected Sub GvRegistrationUser_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs) Handles GvRegistrationUser.RowCommand
        If e.CommandName.Equals("deleteuser") Then
            DeleteUser(e.CommandArgument.ToString())
            BindData()
        ElseIf e.CommandName.Equals("imgEdit") Then
            Response.Redirect("")
        End If
    End Sub

    Protected Sub BtnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSave.Click
        If FormChecking() = True Then
            Dim info As New UserProfileInfo
            info.Fullname = TxtFullname.Text
            info.Email = TxtEmail.Text
            info.PhoneNo = txtPhoneNo.Text
            info.SubconId = Integer.Parse(DdlCompany.SelectedValue)
            info.UserLogin = TxtUserLogin.Text
            info.UsrPassword = TxtConfirmPassword.Text
            info.CMAInfo.ModifiedUser = CommonSite.UserName
            info.UserId = 0
            AddUser(info)
        End If
    End Sub

    Sub checkall(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim mainchk As New CheckBox
        mainchk = GvRegions.HeaderRow.Cells(0).FindControl("chkall")
        Dim i As Integer
        For i = 0 To GvRegions.Rows.Count - 1
            Dim chk As New HtmlInputCheckBox
            chk = GvRegions.Rows(i).Cells(0).FindControl("ChkReview")
            If mainchk.Checked = True Then
                If chk.Disabled = False Then
                    chk.Checked = True
                Else
                    chk.Checked = False
                End If
            Else
                chk.Checked = False
            End If
        Next
    End Sub

#Region "Custom Methods"
    Private Sub BindData()
        GvRegistrationUser.DataSource = controller.GetAccountUsers()
        GvRegistrationUser.DataBind()
    End Sub

    Private Sub BindSubcon()
        DdlCompany.DataSource = controller.GetSubcons()
        DdlCompany.DataTextField = "SubconName"
        DdlCompany.DataValueField = "subconId"
        DdlCompany.DataBind()

        DdlCompany.Items.Insert(0, "--select company--")
    End Sub

    Private Sub BindRegion()
        GvRegions.DataSource = controller.GetRegions()
        GvRegions.DataBind()
    End Sub

    Private Sub AddUser(ByVal info As UserProfileInfo)
        Dim getuserid As Integer = controller.CreateUserLogin(info)
        If getuserid > 0 Then
            Dim j As Integer
            Dim intSuccess As Integer = 1
            For j = 0 To GvRegions.Rows.Count - 1
                Dim chk As New HtmlInputCheckBox
                chk = GvRegions.Rows(j).Cells(0).FindControl("ChkReview")
                If chk.Checked = True Then
                    Dim LblRgnId As Label = GvRegions.Rows(j).Cells(0).FindControl("LblRgnId")
                    If Not LblRgnId Is Nothing AndAlso Not String.IsNullOrEmpty(LblRgnId.Text) Then
                        Dim isSucceed As Boolean = True
                        Dim reginfo As New RegionalUserGroupInfo
                        reginfo.GrpId = 0
                        reginfo.RgnInfo.RgnId = Integer.Parse(LblRgnId.Text)
                        reginfo.ModifiedUser = CommonSite.UserName
                        reginfo.UsrInfo.UserId = getuserid
                        If controller.UserRegionalGroup_IU(reginfo) = True Then
                            LblWarningMessage.Text = TxtFullname.Text & " successfully added"
                            LblWarningMessage.Font.Italic = True
                            LblWarningMessage.ForeColor = Drawing.Color.Green
                            SendMail(info)
                            ClearForm()
                            BindData()
                        Else
                            LblWarningMessage.Text = TxtFullname.Text & " fail added"
                            LblWarningMessage.Font.Italic = True
                            LblWarningMessage.ForeColor = Drawing.Color.Red
                            DeleteUser(info.UserId)
                        End If
                    End If
                End If
            Next            
        Else
            LblWarningMessage.Text = TxtFullname.Text & " fail added"
            LblWarningMessage.Font.Italic = True
            LblWarningMessage.ForeColor = Drawing.Color.Red
        End If
    End Sub

    Private Function AddRegionalGroup(ByVal info As RegionalUserGroupInfo) As Boolean
        Return controller.UserRegionalGroup_IU(info)
    End Function

    Private Sub DeleteUser(ByVal userid As String)
        controller.DeleteUserLogin(userid)
        controller.DeleteRegionalGroup(userid)
    End Sub

    Private Function FormChecking() As Boolean
        Dim isValid As Boolean = True
        Dim strErrMessage As String = String.Empty
        If String.IsNullOrEmpty(TxtFullname.Text) Then
            isValid = False
            strErrMessage = "Fullname is required"
        End If
        If String.IsNullOrEmpty(TxtEmail.Text) And isValid = True Then
            isValid = False
            strErrMessage = "Email is required"
        Else
            If EmailChecking(TxtEmail.Text) = False And isValid = True Then
                isValid = False
                strErrMessage = "Incorrect Email Format"
            End If
        End If

        If String.IsNullOrEmpty(txtPhoneNo.Text) And isValid = True Then
            isValid = False
            strErrMessage = "Phone No is Required!"
        End If

        If DdlCompany.SelectedIndex = 0 And isValid = True Then
            isValid = False
            strErrMessage = "Company is required!"
        End If

        If String.IsNullOrEmpty(TxtUserLogin.Text) And isValid = True Then
            isValid = False
            strErrMessage = "User Login is required"
        Else
            If TxtUserLogin.Text.Length < 6 And isValid = True Then
                isValid = False
                strErrMessage = "Min Character of User Login is 6"
            Else
                If controller.Checking_UserLoginIsExist(TxtUserLogin.Text) And isValid = True Then
                    isValid = False
                    strErrMessage = "This User Login already exist, please try another!"
                End If
            End If
        End If
        If String.IsNullOrEmpty(TxtUserPassword.Text) And isValid = True Then
            isValid = False
            strErrMessage = "Password is required"
        End If
        If Not TxtConfirmPassword.Text.Equals(TxtUserPassword.Text) And isValid = True Then
            isValid = False
            strErrMessage = "Your confirm password doesn't match, please re-type again!"
        End If

        If CheckingRegionSelected() = 0 And isValid = True Then
            isValid = False
            strErrMessage = "Please thick at least one Region Group!"
        End If

        If isValid = False Then
            LblWarningMessage.Text = strErrMessage
            LblWarningMessage.Font.Italic = True
            LblWarningMessage.ForeColor = Drawing.Color.Red
            LblWarningMessage.Visible = True
        Else
            LblWarningMessage.Text = ""
        End If

        Return isValid
    End Function

    Private Function EmailChecking(ByVal email As String) As Boolean
        Dim pattern As String = "^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"
        Dim emailAddressMatch As Match = Regex.Match(email, pattern)
        If emailAddressMatch.Success Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Function CheckingRegionSelected() As Integer
        Dim j As Integer
        Dim intSuccess As Integer = 0
        For j = 0 To GvRegions.Rows.Count - 1
            Dim chk As New HtmlInputCheckBox
            chk = GvRegions.Rows(j).Cells(0).FindControl("ChkReview")
            If chk.Checked = True Then
                intSuccess += 1
            End If
        Next
        Return intSuccess
    End Function

    Private Sub ClearForm()
        TxtFullname.Text = ""
        TxtEmail.Text = ""
        TxtUserLogin.Text = ""
        TxtConfirmPassword.Text = ""
        TxtUserPassword.Text = ""
        txtPhoneNo.Text = ""
        BindSubcon()
        BindRegion()
    End Sub

    Private Sub SendMail(ByVal info As UserProfileInfo)
        Dim sbBody As New StringBuilder
        sbBody.Append("Dear " & info.Fullname & ", <br/><br/>")
        sbBody.Append("Your account Login to register your engineer geo-tag training is " & info.UserLogin & "<br/>Your password is" & info.UsrPassword & "<br/><br />")
        sbBody.Append("Please directly Login to Geo-Tag Online Training with <a href='http://www.telkomsel.nsnebast.com'>Click here</a>" & "<br/>")
        sbBody.Append("Powered By EBAST" & "<br/>")
        sbBody.Append("<img src='https://www.telkomsel.nsnebast.com/images/nsn-logo.gif' alt='companylogo' />")
    End Sub
#End Region

End Class
