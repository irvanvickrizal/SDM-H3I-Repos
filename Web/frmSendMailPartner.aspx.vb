Imports Common_NSNFramework
Imports Common
Imports BusinessLogic
Imports System.Data
Imports System.Net.Mail

Partial Class frmSendMailPartner
    Inherits System.Web.UI.Page
    Dim strQuery As String
    Dim objutil As New DBUtil
    Dim dtEmailPartner As New DataTable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            LblSuccess.Visible = False
        End If
    End Sub

    Protected Sub BtnSendMailClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSendMail.Click
        'LoadAllEmailPartners()
        'If dtEmailPartner.Rows.Count > 0 Then
        '    LblTotalSend.Text = dtEmailPartner.Rows.Count.ToString
        '    Dim kt As Integer
        '    For kt = 0 To dtEmailPartner.Rows.Count - 1
        '        SendMailInvitationPartner(dtEmailPartner.Rows(kt).Item(1), "ebast123", dtEmailPartner.Rows(kt).Item(0), dtEmailPartner.Rows(kt).Item(3))
        '        LblSendCursor.Text = kt.ToString
        '    Next
        '    LblSuccess.Visible = True
        'End If
        SendMailTrial()
    End Sub

    Private Sub LoadAllEmailPartners()
        strQuery = "select name, usrLogin, UsrPassword, Email, PhoneNo from ebastusers_1 where usrRole = 7 and acc_status='A'"
        dtEmailPartner = objutil.ExeQueryDT(strQuery, "ddd")
    End Sub

    Private Sub SendMailInvitationPartner(ByVal userlogin As String, ByVal userPass As String, ByVal username As String, ByVal email As String)
        Dim senderAdd As String = ConfigurationManager.AppSettings("Smailid")
        Dim receiverAdd As String = email
        Dim mySMTPClient As New System.Net.Mail.SmtpClient
        Dim myEmail As New System.Net.Mail.MailMessage
        myEmail.BodyEncoding() = System.Text.Encoding.UTF8
        myEmail.SubjectEncoding = System.Text.Encoding.UTF8
        myEmail.To.Clear() 'bugfix101026 clearing the addresses
        myEmail.To.Add(receiverAdd)
        myEmail.Bcc.Add("irvan.vickrizal@hotmail.com")
        myEmail.Subject = "Your Account in eBast has been Actived"
        Dim sb As New StringBuilder
        sb.Append("Dear " & username & ", <br/><br/>")
        sb.Append("Your account in eBAST has been active, Please see the below as your account :<br/><br/>")
        sb.Append("UserLogin : " & userlogin & "<br/>")
        sb.Append("Password  : " & userPass & "<br/>")
        sb.Append("Please contact eBAST helpdesk  support@nsnebast.com  for further information <br/><br/>")
        sb.Append("<b>Powered By EBAST</b><br/>")
        sb.Append("<img src='http://www.telkomsel.nsnebast.com/images/nsn-logo.gif' alt='companylogo' />")
        myEmail.Body = sb.ToString() 'closing

        myEmail.IsBodyHtml = True
        myEmail.From = New System.Net.Mail.MailAddress(senderAdd, "NSN-eBast Admin")
        mySMTPClient.Host = ConfigurationManager.AppSettings("smtp")
        Try
            mySMTPClient.Send(myEmail)
            LblSuccess.Text = "Email :" & email & " success"
        Catch ex As Exception
            objutil.ExeNonQuery("exec uspErrLog 0, 'sendMailNotifications','" & ex.Message.ToString() & "','sendMailNotifications'")
        End Try
    End Sub

    Private Sub SendMailTrial()
        Dim senderAdd As String = ConfigurationManager.AppSettings("Smailid")
        Dim mySMTPClient As New System.Net.Mail.SmtpClient
        Dim myEmail As New System.Net.Mail.MailMessage
        myEmail.BodyEncoding() = System.Text.Encoding.UTF8
        myEmail.SubjectEncoding = System.Text.Encoding.UTF8
        myEmail.To.Clear() 'bugfix101026 clearing the addresses
        myEmail.Bcc.Add("irvan.vickrizal.ext@nsn.com")
        myEmail.Bcc.Add("irvan.vickrizal@gmail.com")
        myEmail.Bcc.Add("pramono.pramono.ext@services.nsn.com")
        myEmail.Bcc.Add("pramono_204@yahoo.co.id")
        myEmail.Bcc.Add("monika.ningrum.ext@nsn.com")
        myEmail.Bcc.Add("gandhesen@gmai.com")
        myEmail.Subject = "Testing Email to NSN Office email"
        Dim sb As New StringBuilder
        sb.Append("Testing email from server")
        myEmail.Body = sb.ToString() 'closing

        myEmail.IsBodyHtml = True
        myEmail.From = New System.Net.Mail.MailAddress("irvan.vickrizal@gmail.com", "NSN-eBast Admin")
        mySMTPClient.Host = ConfigurationManager.AppSettings("smtp")
        'Dim SMTPServer As New SmtpClient("smtp.gmail.com")

        'mySMTPClient.Host = "smtp.gmail.com"
        'mySMTPClient.Port = 587
        'mySMTPClient.Credentials = New System.Net.NetworkCredential("nsnebast.email@gmail.com", "D14m0nd!@#$")
        'mySMTPClient.EnableSsl = True
        Try
            mySMTPClient.Send(myEmail)
        Catch ex As Exception
            objutil.ExeNonQuery("exec uspErrLog 0, 'sendMailNotifications','" & ex.Message.ToString() & "','sendMailNotifications'")
        End Try
    End Sub

End Class
