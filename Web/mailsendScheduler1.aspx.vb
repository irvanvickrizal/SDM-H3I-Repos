Imports System.Data
Imports System.Data.SqlClient
Imports Common
Imports Microsoft.VisualBasic
Imports Entities
Imports BusinessLogic
Partial Class mailsendScheduler1
    Inherits System.Web.UI.Page
    Dim objetsitedoc As New ETSiteDoc
    Dim objbositedoc As New BOSiteDocs
    Dim objBOM As New BOMailReport
    Dim objET As New ETAuditTrail
    Dim objdb As New DBUtil
    Dim dtn, dt, dt1, dt2, dtr As New DataTable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    End Sub
    Protected Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
        Dim mbody, userid, url As String
        Dim j, k, l As Integer
        Try
            If Request.QueryString("rdx") = "-1" Then
                Response.Write("Signature Verification Completed")
                ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "CloseWindow();", True)
            Else
                Response.Write(Request.QueryString("rdx") & "<br><br>")
            End If
            If Request.QueryString("rdx") = "0" Then
                objdb.ExeQuery("delete from ReminderUsers") 'clearing
                dt1 = objdb.ExeQueryDT("exec uspGetMailReminderUsers", "maildt")
                If dt1.Rows.Count > 0 Then
                    For j = 0 To dt1.Rows.Count - 1
                        userid = dt1.Rows(j).Item("userid").ToString
                        objdb.ExeQuery("insert into ReminderUsers(userid) values(" & userid & ")")
                    Next
                End If
                dt1 = objdb.ExeQueryDT("select top 1 * from ReminderUsers", "maildt")
                If dt1.Rows.Count() > 0 Then
                    userid = dt1.Rows(0).Item("userid").ToString
                    url = "window.location='mailsendScheduler2.aspx?rdx=" & userid
                    Response.Write("<script language='JavaScript'>" & url & "';</script>")
                Else
                    Response.Write("No Records")
                End If
            End If
            If Request.QueryString("rdx") <> "" And Request.QueryString("rdx") <> "-1" Then
                '--process the mail
                dtn = objdb.ExeQueryDT("exec uspGetMailReminderDocs " & Request.QueryString("rdx"), "maildt")
                If dtn.Rows.Count > 0 Then
                    dt = objBOM.uspMailReportLD(ConfigurationManager.AppSettings("uploadmailconst"), ) 'this is for document upload time sending mail
                    Dim Remail As String = "'"
                    Dim name As String = ""
                    Dim doc As New StringBuilder
                    Remail = dtn.Rows(0).Item(3).ToString
                    name = dtn.Rows(0).Item(2).ToString
                    Dim senderAdd As String = ConfigurationManager.AppSettings("Smailid")
                    Dim receiverAdd As String = Remail
                    Dim mySMTPClient As New System.Net.Mail.SmtpClient
                    Dim myEmail As New System.Net.Mail.MailMessage
                    myEmail.BodyEncoding() = System.Text.Encoding.UTF8
                    myEmail.SubjectEncoding = System.Text.Encoding.UTF8
                    myEmail.To.Clear() 'bugfix101026 clearing the addresses
                    myEmail.To.Add(receiverAdd)
                    myEmail.Subject = dt.Rows(0).Item(3).ToString & "(" & name & ")" 'subject from table
                    mbody = dt.Rows(0).Item(2).ToString & name & ",<br> <br>" 'salutatation from table
                    mbody = mbody & dt.Rows(0).Item(4).ToString & " <br> <br> "
                    mbody = mbody & " Process - " & ConfigurationManager.AppSettings("Type").Replace("\", "") & " <br> <br> "
                    mbody = mbody & "<table border='1'>"
                    mbody = mbody & "<tr><td>Contract #</td><td>Site</td><td>Scope</td><td>Document</td><td>Date Submitted</td><td>KPI Date</td></tr>"
                    For k = 0 To dtn.Rows.Count - 1
                        mbody = mbody & "<tr><td> " & dtn.Rows(k).Item(6).ToString & "  </td><td>" & dtn.Rows(k).Item(4).ToString & "</td><td>" & dtn.Rows(k).Item(5).ToString & " </td><td>" & dtn.Rows(k).Item(0).ToString & "</td><td>" & dtn.Rows(k).Item(7).ToString & "</td><td>" & dtn.Rows(k).Item(8).ToString & "</td></tr>"
                    Next
                    'CC Mail
                    dt2 = objdb.ExeQueryDT("exec uspDocUsers " & dtn.Rows(0).Item(9).ToString & "," & dtn.Rows(0).Item(10).ToString, "maildt")
                    For l = 0 To dt2.Rows.Count - 1
                        If Len(dt2.Rows(l).Item(0).ToString) > 0 Then
                            myEmail.Bcc.Add(dt2.Rows(l).Item(0).ToString)
                        End If
                    Next
                    'myEmail.Bcc.Add("irvan.vickrizal@gmail.com") 'put any email for debuging
                    'end CC Mail
                    myEmail.Bcc.Add("pramono.pramono.ext@nsn.com")
                    myEmail.Bcc.Add("monika.ningrum.ext@nsn.com")
                    myEmail.Bcc.Add("irvan.vickrizal.ext@nsn.com")
                    mbody = mbody & "</table>"
                    mbody = mbody & "<br> <br> <br>"
                    mbody = mbody & "<a href='https://www.telkomsel.nsnebast.com'>Click here</a> to go to e-BAST"
                    mbody = mbody & "<br> <br> <br>" & dt.Rows(0).Item(5).ToString & " <br> <br>  e-BAST Team "
                    myEmail.Body = mbody 'closing
                    myEmail.IsBodyHtml = True
                    myEmail.From = New System.Net.Mail.MailAddress(senderAdd, "NSN")
                    mySMTPClient.Host = ConfigurationManager.AppSettings("smtp")
                    'mySMTPClient.Host = "smtp.gmail.com"
                    'mySMTPClient.Port = 587
                    'mySMTPClient.Credentials = New System.Net.NetworkCredential("nsnebast.email@gmail.com", "D14m0nd!@#$")
                    'mySMTPClient.EnableSsl = True
                    Try
                        mySMTPClient.Send(myEmail)
                    Catch ex As Exception
                        objdb.ExeNonQuery("exec uspErrLog 0, 'sendMailNotifications','" & ex.Message.ToString() & "','sendMailNotifications'")
                    End Try
                Else
                    objdb.ExeNonQuery("exec uspErrLog 0, 'sendMailNotifications', 'no email id found','sendMailNotifications'")
                End If
                '--
                objdb.ExeQuery("delete from ReminderUsers where userid=" & Request.QueryString("rdx"))
                dt1 = objdb.ExeQueryDT("select top 1 * from ReminderUsers", "maildt")
                If dt1.Rows.Count > 0 Then
                    userid = dt1.Rows(0).Item("userid").ToString
                    url = "window.location='mailsendScheduler2.aspx?rdx=" & userid
                    Response.Write("<script language='JavaScript'>" & url & "';</script>")
                Else
                    url = "window.location='mailsendScheduler2.aspx?rdx=-1"
                    Response.Write("<script language='JavaScript'>" & url & "';</script>")
                End If
            End If
        Catch ex As Exception
            'if error during processing then exit
            url = "window.location='mailsendScheduler2.aspx?rdx=-1"
            Response.Write("<script language='JavaScript'>" & url & "';</script>")
        End Try
    End Sub
End Class
