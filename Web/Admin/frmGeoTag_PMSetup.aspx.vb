
Partial Class Admin_frmGeoTag_PMSetup
    Inherits System.Web.UI.Page

    Dim controller As New RegistrationController
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If Not String.IsNullOrEmpty(Request.QueryString("id")) Then

            End If
        End If
    End Sub


    Protected Sub BtnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnUpdate.Click
        If FormChecking() = True Then
            Dim info As New UserProfileInfo
            info.UserId = Integer.Parse(Request.QueryString("id"))
            info.SubconId = Integer.Parse(DdlCompany.SelectedValue)
            info.Fullname = TxtPMName.Text
            info.Email = TxtEmail.Text
            info.PhoneNo = TxtPhoneNo.Text
            info.LMBY = CommonSite.UserName
            If controller.PMAccount_U(info) = True Then
                LblErrorMessage.Text = "Data Successfully updated"
                LblErrorMessage.ForeColor = Drawing.Color.Green
                LblErrorMessage.Font.Italic = True
            Else
                LblErrorMessage.Text = "Data Fail to updated"
                LblErrorMessage.ForeColor = Drawing.Color.Red
                LblErrorMessage.Font.Italic = True
            End If
        End If
    End Sub

    Protected Sub BtnChangePassword_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnResetPass.Click
        Dim randomkey As RandomKeyGenerator = New RandomKeyGenerator()
        Dim NumKeys As Integer = 20
        randomkey.KeyLetters = "abcdefghijklmnopqrstuvwxyz"
        randomkey.KeyNumbers = "0123456789"
        randomkey.KeyChars = 6
        Dim strRandomKey As String = randomkey.Generate()
        If ChangePassword(strRandomKey, Integer.Parse(Request.QueryString("id"))) = True Then
            LblErrorMessage.Text = "PM Password has been reset, Please check his/her email"
            LblErrorMessage.ForeColor = Drawing.Color.Green
            LblErrorMessage.Font.Italic = True
        Else
            LblErrorMessage.Text = "PM Password failed to reset, Please try again"
            LblErrorMessage.ForeColor = Drawing.Color.Red
            LblErrorMessage.Font.Italic = True
        End If
    End Sub

#Region "Custom Methods"
    Private Sub BindData(ByVal userid As Integer)
        Dim info As UserProfileInfo = controller.GetPMAccountDetail(userid)
        If info IsNot Nothing Then
            TxtEmail.Text = info.Email
            TxtPhoneNo.Text = info.PhoneNo
            TxtPMName.Text = info.Fullname
            TxtUserLogin.Text = info.UserLogin
            TxtUserLogin.ReadOnly = True
        Else
            BtnResetPass.Visible = False
            BtnUpdate.Visible = False
        End If
    End Sub

    Private Sub BindSubcon(ByVal sconid As Integer)
        DdlCompany.DataSource = controller.GetSubcons()
        DdlCompany.DataTextField = "SubconName"
        DdlCompany.DataValueField = "subconId"
        DdlCompany.DataBind()

        DdlCompany.Items.Insert(0, "--select company--")

        If sconid > 0 Then
            DdlCompany.SelectedValue = sconid.ToString()
        End If
    End Sub

    Private Function ChangePassword(ByVal newpassword As String, ByVal userid As Integer) As Boolean
        Return controller.PMChangePassword(userid, newpassword)
    End Function

    Private Function FormChecking() As Boolean
        Dim isvalid As Boolean = True
        Dim strErrMessage As String = String.Empty
        If String.IsNullOrEmpty(TxtPMName.Text) And isvalid = True Then
            isvalid = False
            strErrMessage = "Please complete PM Name field"
        End If

        If String.IsNullOrEmpty(TxtEmail.Text) And isvalid = True Then
            isvalid = False
            strErrMessage = "Please complete Email of PM field"
        End If

        If String.IsNullOrEmpty(TxtPhoneNo.Text) And isvalid = True Then
            isvalid = False
            strErrMessage = "Please complete Phone No of PM field"
        End If

        If DdlCompany.SelectedIndex = 0 And isvalid = True Then
            isvalid = False
            strErrMessage = "Please Choose Subcon Company"
        End If

        If isvalid = False Then
            LblErrorMessage.Text = strErrMessage
            LblErrorMessage.ForeColor = Drawing.Color.Red
            LblErrorMessage.Font.Italic = True
        End If

        Return isvalid
    End Function

    Private Sub SendMail(ByVal userid As Integer, ByVal newpassword As String)
        Dim info As UserProfileInfo = controller.GetPMAccountDetail(userid)
        If info IsNot Nothing Then
            Dim sbBody As New StringBuilder
            sbBody.Append("Dear " & info.Fullname & ", <br/><br/>")
            sbBody.Append("Your account Password has been reset.<br/> Username : " & info.UserLogin.TrimEnd() & "<br/>Password :" & info.UsrPassword.TrimEnd() & "<br/><br />")
            sbBody.Append("Please directly Login to Geo-Tag Online Training with <a href='http://geotag.licenseregistration.nsnebast.com/'>Click here</a>" & "<br/>")
            sbBody.Append("Powered By EBAST" & "<br/>")
            sbBody.Append("<img src='https://www.telkomsel.nsnebast.com/images/nsn-logo.gif' alt='companylogo' />")
        End If
    End Sub
#End Region
End Class
