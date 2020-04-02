Imports BusinessLogic
Imports Common
Imports System.Data
Imports AXmsCtrl
Imports DGSignPWD
Partial Class Digital_signature_cus
    Inherits System.Web.UI.Page
    Dim objBo As New BODashBoard
    Dim objBo1 As New BOOnlineForm
    Shared strPath As String = ""
    Dim intPage, intX, intY, intHeight, intWidth As New Integer
    Dim objdb As New DBUtil
    Dim dtn As New DataTable
    Dim objBOM As New BOMailReport
    Dim dt As New DataTable
    Dim objdg As New UserInfo
    Dim objsms As New mCore.SMS
    Dim objmail As New TakeMail
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        objsms.License.Company = "TAKE UNITED SDN BHD"
        objsms.License.LicenseType = "PRO-DISTRIBUTION"
        objsms.License.Key = "TRQT-NF2L-Y7V9-HF4E"
        'Response.Cache.SetNoStore()
        If Page.IsPostBack = False Then
            GetPDFDocument()
            GetBautWCTR()

        End If
        txtUserName.Text = Session("User_Login")
        intX = 365

        intPage = -1
        intY = 300
        intHeight = 55
        intWidth = 115


    End Sub
    Sub GetBautWCTR()
        Dim str As String
        str = "Exec [uspWCTROnLine] " & Request.QueryString("swid")
        dt = objdb.ExeQueryDT(str, "SiteDoc1")
        If (dt.Rows.Count > 0) Then
            Dim url As String = "../PO/frmViewDocument.aspx?id=" & dt.Rows(0)("sw_id").ToString
            lblLinks.Text = "<a href='#' onclick=""window.open('" & url & "','mywindow','status=yes,menubar=no,scrollbars=yes,resizable=yes,width=700px,height=600px')"">View BAUT WCTR</a>"
            lblLinks.Visible = True
        Else
            lblLinks.Visible = False

        End If
    End Sub
    Sub GetPDFDocument()
        Dim dt As DataTable
        dt = objBo.CustomerDigitalSign(Request.QueryString("siteno"), Request.QueryString("id"))

        ' dt = objBo.DigitalSign(Convert.ToInt32(7))
        If (dt.Rows.Count > 0) Then
            strPath = dt.Rows(0)("docpath").ToString()
            PDFViwer.Attributes.Add("src", ConfigurationManager.AppSettings("Vpath") + strPath)
        End If


    End Sub
    Protected Sub BtnSign_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSign.Click
        Dim npwd1 As String = ""
        npwd1 = "12345678"

        Dim j As Integer
        Dim objdb As New DBUtil
        j = objdb.ExeQueryScalar("select count(*) from dgpassword where username='" & txtUserName.Text & "' and password='" & txtPassword.Text & "'")
        If j > 0 Then

            Try

                Dim i As Integer = -1
                i = objBo1.updateSiteBautuploadRStatus(Request.QueryString("siteno"), Request.QueryString("id"))

                Dim Flags As Integer = 0
                Dim IntCount As Integer = 0
                If 1 = 1 Then Flags = Flags Or SAPIWrapper.SAPI_ENUM_DRAWING_ELEMENT_GRAPHICAL_IMAGE
                If 1 = 1 Then Flags = Flags Or SAPIWrapper.SAPI_ENUM_DRAWING_ELEMENT_SIGNED_BY
                If 1 = 0 Then Flags = Flags Or SAPIWrapper.SAPI_ENUM_DRAWING_ELEMENT_REASON
                If 1 = 1 Then Flags = Flags Or SAPIWrapper.SAPI_ENUM_DRAWING_ELEMENT_TIME

                Dim strResult As String = SAPIWrapper.SAPI_sign_file(ConfigurationManager.AppSettings("FPath") + strPath, Nothing, txtUserName.Text, _
                                          npwd1, intPage, intX, intY, intHeight, intWidth, False, "test", Flags, "")
                If i = 0 Then
                    ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('Error while doing the transaction.');", True)
                ElseIf i = 1 Then

                    'added by satish on 11thApril
                    '*************************** for mail functionality.***********************
                    'to find next user
                    Dim nextid As Integer
                    Dim strs As String = "SELECT ISNULL(USERID,1)USERID  FROM WFTRANSACTION WHERE" & _
                    " SITE_ID=(SELECT SITE_ID FROM WFTRANSACTION WHERE SNO = " & Convert.ToInt32(Request.QueryString("id")) & " ) " & _
                    " AND SITEVERSION = (SELECT SITEVERSION FROM WFTRANSACTION WHERE SNO = " & Convert.ToInt32(Request.QueryString("id")) & ")" & _
                    " AND STARTDATETIME IS NOT NULL AND ENDDATETIME IS NULL"

                    nextid = objdb.ExeQueryScalar(strs)
                    objdb.ExeNonQuery("exec uspErrLog '', '" & Request.QueryString("id") & "','" & nextid & "' ,'sendmailtranscust1'")
                    If nextid = 0 Then nextid = 1
                    Try
                        objmail.sendmailTrans(nextid, 0, "0", 0, ConfigurationManager.AppSettings("uploadmailconst"))
                    Catch ex As Exception
                        objdb.ExeNonQuery("exec uspErrLog '', 'mailsending','" & ex.Message.ToString.Replace("'", "''") & "','sendmailtransupload'")
                    End Try

                    '**************************************************************************
                    ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('Signed Sucessfully.');", True)
                End If
            Catch ex As Exception
                ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('Error on signning PDF:\n\n'" + ex.Message + ");", True)
            End Try

        Else
            Response.Write("<script> alert('invalid  userid or password');</script>")
        End If


    End Sub
    'Sub sendmailTransNew(ByVal userid As Integer)
    '    Try
    '        dtn = objdb.ExeQueryDT("exec uspgetemailNEW " & userid, "maildt")
    '        If dtn.Rows.Count > 0 Then
    '            If dtn.Rows(0).Item(3) <> "X" Then
    '                dt = objBOM.uspMailReportLD(9, )  ''this is for document upload time sending mail
    '                Dim k As Integer
    '                Dim Remail As String = "'"
    '                Dim name As String = ""
    '                Dim doc As New StringBuilder
    '                Remail = dtn.Rows(0).Item(3).ToString
    '                name = dtn.Rows(0).Item(2).ToString
    '                Dim senderAdd As String = ConfigurationManager.AppSettings("Smailid")
    '                Dim receiverAdd As String = Remail
    '                Dim mySMTPClient As New System.Net.Mail.SmtpClient
    '                Dim myEmail As New System.Net.Mail.MailMessage
    '                myEmail.BodyEncoding() = System.Text.Encoding.UTF8
    '                myEmail.SubjectEncoding = System.Text.Encoding.UTF8
    '                myEmail.To.Add(receiverAdd)
    '                myEmail.Subject = dt.Rows(0).Item(3).ToString  ''subject from table
    '                myEmail.Body = dt.Rows(0).Item(2).ToString & name & ",<br> <br>" ''salutatation from table
    '                myEmail.Body = myEmail.Body & dt.Rows(0).Item(4).ToString & " <br> <br> "
    '                myEmail.Body = myEmail.Body & " Process - " & ConfigurationManager.AppSettings("Type").Replace("\", "") & " <br> <br> "
    '                Dim sb As String = ""
    '                sb = "<table  border=1>"
    '                sb = sb & "<tr><td>Contract #</td><td>Site</td><td>Scope</td><td>Document</td><td>Date Requested</td></tr>"
    '                For k = 0 To dtn.Rows.Count - 1
    '                    sb = sb & "<tr><td> " & dtn.Rows(k).Item(6).ToString & "  </td><td>" & dtn.Rows(k).Item(4).ToString & "</td><td>" & dtn.Rows(k).Item(5).ToString & " </td><td>" & dtn.Rows(k).Item(0).ToString & "</td><td>" & dtn.Rows(k).Item(7).ToString & "</td></tr>"
    '                Next
    '                sb = sb & "</table>"
    '                myEmail.Body = myEmail.Body & sb & "<br/>"
    '                Dim ab As String
    '                ab = "<a href='http://www.telkomsel.nsnebast.com'>Click here</a>" & " to go to e-BAST"
    '                myEmail.Body = myEmail.Body & ab & "<br/>" & dt.Rows(0).Item(5).ToString   ''closing
    '                myEmail.IsBodyHtml = True
    '                myEmail.From = New System.Net.Mail.MailAddress(senderAdd, "NSN")
    '                mySMTPClient.Host = ConfigurationManager.AppSettings("smtp")
    '                Try
    '                    mySMTPClient.Send(myEmail)
    '                Catch ex As Exception
    '                    objdb.ExeNonQuery("exec uspErrLog '', 'mailsending','" & ex.Message.ToString.Replace("'", "''") & "','sendmailtrans'")
    '                End Try
    '            Else
    '                objdb.ExeNonQuery("exec uspErrLog '', 'mailsending', 'no email id found','sendmailtrans'")
    '            End If


    '        End If
    '    Catch ex As Exception
    '        objdb.ExeNonQuery("exec uspErrLog '', 'mailsending','" & ex.Message.ToString.Replace("'", "''") & "','sendmailtranscust2'")
    '    End Try
    'End Sub
    Protected Sub lnkrequest_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkrequest.Click
        Dim npwd As String = ""
        Dim msgdata As New StringBuilder

        If txtUserName.Text <> "" Then
            objsms.Port = "COM4"
            objsms.BaudRate = 115200
            objsms.DataBits = 8
            Dim phone As String = ""
            phone = "+" & objdb.ExeQueryScalarString("select phoneno from ebastusers_1 where usrlogin='" & Session("User_Login") & "'and rstatus=2")
            npwd = objdb.ExeQueryScalarString("Exec random_password 8,'simple'")
            If Session("User_Name") Is Nothing Then Session("User_Name") = ""
            msgdata.Append("Dear " & Session("User_Name") & " your requested password is : " & npwd)
            msgdata.Append(Environment.NewLine)
            msgdata.Append("Powered by e-BAST")
            Try
                objsms.SendSMS(phone, msgdata.ToString(), False)
            Catch ex As Exception
                Response.Write(ex.Message.ToString)
            End Try
            objsms.Disconnect()
            objdb.ExeNonQuery("Exec uspdgpasswordinsert '" & txtUserName.Text & "','" & npwd & "'")
            Response.Write("<script> alert('Please check for your password in your phone')</script>")
        Else
            Response.Write("<script> alert('Please keyin the user name and click request password')</script>")
        End If
    End Sub
    'Protected Sub lnkrequest_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkrequest.Click
    '    Dim npwd As String = ""
    '    Dim msgdata As String = ""
    '    If txtUserName.Text <> "" Then
    '        objsms.Port = "COM4"
    '        objsms.BaudRate = 115200
    '        objsms.DataBits = 8
    '        Dim phone As String = ""
    '        phone = "+" & objdb.ExeQueryScalarString("select phoneno from ebastusers_1 where usrlogin='" & Session("User_Login") & "'")
    '        npwd = objdb.ExeQueryScalarString("Exec random_password 8,'simple'")
    '        If Session("User_Name") Is Nothing Then Session("User_Name") = ""
    '        msgdata = "Dear " & Session("User_Name") & " your requested password is : " & npwd
    '        Try
    '            objsms.SendSMS(phone, msgdata, False)
    '        Catch ex As Exception
    '            Response.Write(ex.Message.ToString)
    '        End Try
    '        objsms.Disconnect()
    '        objdb.ExeNonQuery("Exec uspdgpasswordinsert '" & txtUserName.Text & "','" & npwd & "'")
    '        Response.Write("<script> alert('Please check for your password in your phone')</script>")
    '    Else
    '        Response.Write("<script> alert('Please keyin the user name and click request password')</script>")
    '    End If

    '    'Dim dd As New sms.Service
    '    'Dim npwd As String = ""
    '    'Dim msgdata As String = ""
    '    'If txtUserName.Text <> "" Then
    '    '    Dim phone As String = ""
    '    '    phone = "+" & objdb.ExeQueryScalarString("select phoneno from ebastusers_1 where usrlogin='" & Session("User_Login") & "'")
    '    '    npwd = objdb.ExeQueryScalarString("Exec random_password 8,'simple'")
    '    '    If Session("User_Name") Is Nothing Then Session("User_Name") = ""
    '    '    msgdata = "Dear " & Session("User_Name") & " your requested password is : " & npwd
    '    '    dd.sendsms(Session("User_Name"), phone, npwd)
    '    '    objdb.ExeNonQuery("Exec uspdgpasswordinsert '" & txtUserName.Text & "','" & npwd & "'")
    '    '    Response.Write("<script> alert('Please check for your password in your phone')</script>")
    '    'Else
    '    '    Response.Write("<script> alert('Please keyin the user name and click request password')</script>")
    '    'End If
    'End Sub
    'Protected Sub lnkrequest_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkrequest.Click
    '    Dim npwd As String = ""
    '    If txtUserName.Text <> "" Then
    '        If txtUserName.Text.ToUpper = "NSN" Then
    '            npwd = "eeieyou1"
    '        ElseIf txtUserName.Text.ToUpper = "TELKOMSEL" Then
    '            npwd = "yipeenon"
    '        Else
    '            npwd = "abcd1234"
    '        End If
    '        'npwd = objdb.ExeQueryScalarString("Exec random_password " & 8 & ",'" & "simple" & "'")
    '        objdg.LoginName = txtUserName.Text
    '        objdg.Password = npwd
    '        objdg.CommonName = ""
    '        objdg.Email = ""
    '        Try
    '            ' SetPassword("cosignadmin", "12345678", "117.102.80.44", objdg)
    '            objGsmProtocol.Device = ConfigurationManager.AppSettings("Modemport")
    '            objSmsMessage.Recipient = "+" & Session("phone")
    '            objSmsMessage.Data = "Requested password is " & npwd
    '            objGsmProtocol.LogFile = "C:\SMSLog.txt"
    '            Try
    '                objGsmProtocol.Send(objSmsMessage)
    '            Catch ex As Exception
    '                objdb.ExeNonQuery("exec uspErrLog '', 'smssending','" & ex.Message.ToString.Replace("'", "''") & "','sendsms'")
    '            End Try
    '            objdb.ExeNonQuery("Exec uspdgpasswordinsert '" & txtUserName.Text & "','" & npwd & "'")
    '            Response.Write("<script> alert('Please check for your password in your phone'')</script>")
    '        Catch ex As Exception
    '            Response.Write("<script> alert('User Not Found')</script>")
    '        End Try
    '    Else
    '        Response.Write("<script> alert('Please keyin the user name and click request password')</script>")
    '    End If
    'End Sub
End Class
