Imports BusinessLogic
Imports Common
Imports entities
Imports System.Net.Mail

Partial Class frmResetPassword
    Inherits System.Web.UI.Page
    Dim objET As New ETEBASTUsers
    Dim objBO As New BOUserLD
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            SetupFirstDisplay()
        End If
    End Sub

    Protected Sub LbtGoToSignInFormClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles LbtGoToSignInForm.Click
        Response.Redirect("default.aspx")
    End Sub

    Protected Sub BtnSendPasswordClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSendPassword.Click
        If (String.IsNullOrEmpty(TxtUsername.Text) = False) Then
            PasswordReset()
        End If
    End Sub
#Region "Custom Methods"
    Private Sub SetupFirstDisplay()
        PnlCorrectUsername.Visible = False
        PnlIncorrectUsername.Visible = False
    End Sub

    Private Sub PasswordReset()
        If (IsValidUsername(TxtUsername.Text) = True) Then
            PnlIncorrectUsername.Visible = False
            PnlCorrectUsername.Visible = True
            TxtUsername.Text = ""
        Else
            PnlIncorrectUsername.Visible = True
            PnlCorrectUsername.Visible = False
        End If
    End Sub

    Private Function IsValidUsername(ByVal strUsername As String) As Boolean
        Dim email As String = NSNCustomizeConfiguration.GetMailByUsername(strUsername)
        If String.IsNullOrEmpty(email) = True Then
            Return False
        Else
            Dim personal() As String = email.Split("-")
            Dim randomkey As RandomKeyGenerator = New RandomKeyGenerator()
            Dim NumKeys As Integer = 20

            randomkey.KeyLetters = "abcdefghijklmnopqrstuvwxyz"
            randomkey.KeyNumbers = "0123456789"
            randomkey.KeyChars = 6

            HdfPassword.Value = randomkey.Generate()
            objET.USR_ID = personal(2)
            objET.Approved = Convert.ToInt16(Convert.ToBoolean(personal(3)))
            objET.ACC_Status = personal(4)
            objET.USRPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(HdfPassword.Value & "TAKE", "MD5")
            objBO.uspEBASTUsersStatusU(objET, "R")
            SendMail(email, strUsername)
            Return True
        End If
    End Function

    Private Sub SendMail(ByVal strMail As String, ByVal username As String)
        Dim profile() As String = strMail.Split("-")
        Dim mbody As String = String.Empty
        Dim remail As String = profile(0)
        Dim nameofuser As String = profile(1)
        Dim senderAdd As String = ConfigurationManager.AppSettings("Smailid")
        Dim receiverAdd As String = remail
        Dim mySMTPClient As New System.Net.Mail.SmtpClient
        Dim myEmail As New System.Net.Mail.MailMessage
        myEmail.BodyEncoding() = System.Text.Encoding.UTF8
        myEmail.SubjectEncoding = System.Text.Encoding.UTF8
        myEmail.To.Clear() 'bugfix101026 clearing the addresses
        myEmail.To.Add(receiverAdd)
        myEmail.Subject = "Your new password for eBast application"
        mbody = "Dear " & profile(1) & ", <br/><br/>"
        mbody = mbody & "As you requested, your password in eBast has now been reset. Your new login details are as follows: <br/>"
        mbody = mbody & "http://www.telkomsel.nsnebast.com" & "<br/>"
        mbody = mbody & "username : <b>" & username & "</b><br/>"
        mbody = mbody & "password : <b>" & HdfPassword.Value & "</b><br/><br/><br/>"

        mbody = mbody & "regards and thanks <br/><img src='http://www.telkomsel.nsnebast.com/images/nsn-logo.gif' alt='companylogo' /><br/><b>eBast Administrator</b><br/> Think First Think Different"
        myEmail.Body = mbody 'closing

        myEmail.IsBodyHtml = True
        myEmail.From = New System.Net.Mail.MailAddress("nsnebast.email@gmail.com", "NSN-eBast Admin")
        mySMTPClient.Host = ConfigurationManager.AppSettings("smtp")
        Try
            mySMTPClient.Send(myEmail)
        Catch ex As Exception
            'objdb.ExeNonQuery("exec uspErrLog 0, 'sendMailNotifications','" & ex.Message.ToString() & "','sendMailNotifications'")
        End Try
    End Sub

#End Region
End Class
