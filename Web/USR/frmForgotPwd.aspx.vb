Imports BusinessLogic
Imports Common
Imports Entities
Imports System.Data
Imports System.Net.Mail
Imports System.Web.Security
Partial Class frmForgotPwd
    Inherits System.Web.UI.Page
    Dim dt As New DataTable
    Dim objbo As New BOUserSetup
    Dim etu As New ETEBASTUsers
    Dim objUtil As New DBUtil
    Dim i As Integer
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        btnSubmit.Attributes.Add("onclick", "javascript:return checkIsEmpty();")
        If Not Page.IsPostBack Then
            tblSecurity.Visible = False
            tblPWD.Visible = True
        End If
    End Sub
    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        If txtAnswer.Value = hdnanswer.Value Then
            'change passord to default
            '.USR_ID & ",'" & oPwd & "','" & .USRPassword & "','" & .USRSQ & "','" & .USRSA & "'," & .AT.RStatus & ",'" & .AT.LMBY & "'"
            etu.USR_ID = hdnusrid.Value
            etu.USRPassword = FormsAuthentication.HashPasswordForStoringInConfigFile("ebast123" & "TAKE", "MD5")
            etu.USRSQ = ddlQA.SelectedItem.Text.Replace("'", "''")
            etu.USRSA = txtAnswer.Value
            etu.AT.RStatus = 2
            etu.AT.LMBY = "s"
            objbo.uspCPwd(etu, "FPWD")
            'sending mail
            Dim senderAdd As String = ConfigurationManager.AppSettings("Smailid")
            Dim receiverAdd As String = hdnmail.Value
            Dim mySMTPClient As New System.Net.Mail.SmtpClient
            Dim myEmail As New System.Net.Mail.MailMessage
            myEmail.BodyEncoding() = System.Text.Encoding.UTF8
            myEmail.SubjectEncoding = System.Text.Encoding.UTF8
            myEmail.To.Add(receiverAdd)
            myEmail.Subject = "Forgot Password Request"
            myEmail.Body = "Dear " & hdnname.Value & "<br>"
            myEmail.Body = myEmail.Body & "Your Password is changed to default password i.e ebast123 " & " < br > """
            myEmail.Body = myEmail.Body & "Please Login using this password and change your password after login & " & "< br > """
            myEmail.Body = myEmail.Body & " Thank you"
            myEmail.IsBodyHtml = True
            myEmail.From = New System.Net.Mail.MailAddress(senderAdd, "NSN")
            mySMTPClient.Host = ConfigurationManager.AppSettings("smtp")
            Try
                mySMTPClient.Send(myEmail)
                Response.Write("<script>alert('please check your email for password')</script>")
                Server.Transfer("~\Default.aspx")
            Catch ex As Exception
                objUtil.ExeNonQuery("exec uspErrLog '', 'mailsending','" & ex.Message.ToString.Replace("'", "''") & "','forgotpwd'")

            End Try
        Else
            Response.Write("<script>alert('Wrong Answer')</script>")
        End If
    End Sub
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        dt = objbo.uspChkUserid(txtUserID.Value)
        If dt.Rows.Count <> 0 Then
            tblSecurity.Visible = True
            hdnname.Value = dt.Rows(0).Item(0).ToString
            ddlQA.SelectedItem.Text = dt.Rows(0).Item(1).ToString
            ddlQA.Enabled = False
            hdnanswer.Value = dt.Rows(0).Item(2).ToString
            hdnmail.Value = dt.Rows(0).Item(3).ToString
            hdnusrid.Value = dt.Rows(0).Item(4).ToString
        Else
            Response.Write("<script>alert('Invalid UserID')</script>")
        End If
    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        txtUserID.Value = ""
        txtAnswer.Value = ""
        tblSecurity.Visible = False
        tblPWD.Visible = True
    End Sub
    'Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    '    btnSubmit.Attributes.Add("onclick", "javascript:return checkIsEmpty();")
    '    If Not Page.IsPostBack Then
    '        tblPWD.Visible = True
    '    End If
    'End Sub
    'Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
    '    'If txtAnswer.Value = hdnanswer.Value Then
    '    ''change passord to default
    '    ''.USR_ID & ",'" & oPwd & "','" & .USRPassword & "','" & .USRSQ & "','" & .USRSA & "'," & .AT.RStatus & ",'" & .AT.LMBY & "'"
    '    'etu.USR_ID = hdnusrid.Value
    '    'etu.USRPassword = FormsAuthentication.HashPasswordForStoringInConfigFile("ebast123" & "TAKE", "MD5")
    '    'etu.USRSQ = ddlQA.SelectedItem.Text.Replace("'", "''")
    '    'etu.USRSA = txtAnswer.Value
    '    'etu.AT.RStatus = 2
    '    'etu.AT.LMBY = "s"
    '    'objbo.uspCPwd(etu, FormsAuthentication.HashPasswordForStoringInConfigFile("ebast123" & "TAKE", "MD5"))
    '    ''sending mail
    '    'Dim senderAdd As String = ConfigurationManager.AppSettings("Smailid")
    '    'Dim receiverAdd As String = hdnmail.Value
    '    'Dim mm As New MailMessage(senderAdd, receiverAdd)
    '    'mm.Subject = "Forgot Password Request"
    '    'mm.Body = "Dear" & hdnname.Value & "<br>"
    '    'mm.Body = mm.Body & "Your Password is changed to default password i.e ebast123 " & " < br > """
    '    'mm.Body = mm.Body & "Please Login using this password and change your password after login & " & "< br > """
    '    'mm.Body = mm.Body & " Thank you"
    '    'mm.IsBodyHtml = True
    '    'Dim client As New SmtpClient
    '    'client.Host = ConfigurationManager.AppSettings("smtp")
    '    'client.Port = ConfigurationManager.AppSettings("Portno")
    '    'client.DeliveryMethod = SmtpDeliveryMethod.PickupDirectoryFromIis
    '    'client.Send(mm)
    '    'Response.Write("<script>alert('please check your email for password')</script>")
    '    'Server.Transfer("~\Default.aspx")
    '    'PasswordGenerator()
    '    'Else
    '    'Response.Write("<script>alert('Wrong Answer')</script>")
    '    'End If

    '    Response.Redirect("~/Digital-Sign/Digital-signature.aspx")
    'End Sub
    'Sub PasswordGenerator()
    '    Dim allowedChars As String = ""
    '    allowedChars = "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z,A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,1,2,3,4,5,6,7,8,9,0"
    '    Dim sep As Char = ","
    '    Dim arr() As String = allowedChars.Split(",")
    '    Dim strPassword As String = ""
    '    Dim temp As String = ""
    '    Dim rand As Random = New Random()
    '    For i As Integer = 0 To 7
    '        temp = arr(rand.Next(0, arr.Length))
    '        strPassword = strPassword & temp
    '    Next
    '    strpwd.Text = strPassword.ToString.ToLower
    'End Sub
    'Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
    '    dt = objbo.uspChkUserid(txtUserID.Value)
    '    If dt.Rows.Count <> 0 Then
    '        btnSubmit.Visible = True
    '        newpwd.Visible = True
    '        hdnname.Value = dt.Rows(0).Item(0).ToString
    '        hdnusrid.Value = dt.Rows(0).Item(4).ToString
    '        strpwd.Text = objUtil.ExeQueryScalarString("Exec random_password")
    '        Dim aa As DGSignPWD.UserInfo
    '        aa.LoginName = txtUserID.Value
    '        aa.Password = strpwd.Text
    '        DGSignPWD.SetPassword("cosignadmin", "12345678", "117.102.80.44", aa)
    '        'Dim strpPassword As String = ""
    '        'strpPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(strPwd.Text & "TAKE", "MD5")
    '        'objUtil.ExeQueryScalar("Update EBastUsers_1 Set USRPassword = '" & strpPassword & "',IsLogin=0 Where usr_id=" & hdnusrid.Value)
    '    Else
    '        Response.Write("<script>alert('Invalid UserID')</script>")
    '    End If
    'End Sub
End Class
