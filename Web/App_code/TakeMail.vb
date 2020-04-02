Imports Common
Imports System.Collections.Generic
Imports System.Data.SqlClient
Imports System.Data
Imports Microsoft.VisualBasic
Imports Entities
Imports BusinessLogic

Public Class TakeMail
    Dim objetsitedoc As New ETSiteDoc
    Dim objbositedoc As New BOSiteDocs
    Dim objBOM As New BOMailReport
    Dim objET As New ETAuditTrail
    Dim objdb As New DBUtil
    Dim dtn, dt, dt1, dt2, dtr As New DataTable
    Dim mailcontroller As New MailConfigController

    Public Sub sendMailNotifications(ByVal userid As String, ByVal formatconstant As Integer)
        Dim mbody As String = ""
        Dim k, l As Integer
        dtn = objdb.ExeQueryDT("exec uspGetMailReminderDocs " & userid, "maildt")
        If dtn.Rows.Count > 0 And dtn.Rows(0).Item(3) <> "X" Then
            dt = objBOM.uspMailReportLD(formatconstant, ) 'this is for document upload time sending mail
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
                myEmail.To.Add(dt2.Rows(l).Item(0).ToString)
            Next
            'myEmail.To.Add("melissa.andini.ext@nsn.com") 'put any email for debuging
            'end CC Mail
            mbody = mbody & "</table>"
            mbody = mbody & "<br> <br> <br>"
            mbody = mbody & "<a href='http://www.telkomsel.nsnebast.com'>Click here</a> to go to e-BAST"
            mbody = mbody & "<br> <br> <br>" & dt.Rows(0).Item(5).ToString & " <br> <br>  e-BAST Team "
            myEmail.Body = mbody 'closing
            myEmail.IsBodyHtml = True
            myEmail.From = New System.Net.Mail.MailAddress(senderAdd, "NSN")
            mySMTPClient.Host = ConfigurationManager.AppSettings("smtp")
            Try
                'bugfix101210 has been replace by task scheduler mail sender
                'mySMTPClient.Send(myEmail)
            Catch ex As Exception
                objdb.ExeNonQuery("exec uspErrLog 0, 'sendMailNotifications','" & ex.Message.ToString() & "','sendMailNotifications'")
            End Try

        Else
            objdb.ExeNonQuery("exec uspErrLog 0, 'sendMailNotifications', 'no email id found','sendMailNotifications'")
        End If
    End Sub
    Public Sub sendMailNotificationsAllUsers(ByVal formatconstant As Integer)
        Dim mbody As String = ""
        Dim j, k, l As Integer
        dt1 = objdb.ExeQueryDT("exec uspGetMailReminderUsers", "maildt")
        If dt1.Rows.Count > 0 Then
            For j = 0 To dt1.Rows.Count - 1
                dtn = objdb.ExeQueryDT("exec uspGetMailReminderDocs " & dt1.Rows(j).Item(2), "maildt")
                If dt1.Rows(0).Item(1) <> "X" Then
                    dt = objBOM.uspMailReportLD(formatconstant, ) 'this is for document upload time sending mail
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
                        myEmail.To.Add(dt2.Rows(l).Item(0).ToString)
                    Next
                    'myEmail.To.Add("melissa.andini.ext@nsn.com") 'put any email for debuging
                    'end CC Mail
                    mbody = mbody & "</table>"
                    mbody = mbody & "<br> <br> <br>"
                    mbody = mbody & "<a href='http://www.telkomsel.nsnebast.com'>Click here</a> to go to e-BAST"
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
            Next
        End If
    End Sub
    Public Sub sendMailTrans(ByVal userid As Integer, ByVal siteid As Int32, ByVal usertype As String, ByVal usrrole As Integer, ByVal formatconstant As Integer)
        Dim mbody As String = ""
        If userid = 0 Then
            dtn = objbositedoc.uspgetemail(siteid, usertype, usrrole)
        Else
            dtn = objdb.ExeQueryDT("exec uspgetemailNEW " & userid, "maildt")
        End If
        If dtn.Rows.Count > 0 Then
            If dtn.Rows(0).Item(3) <> "X" Then
                dt = objBOM.uspMailReportLD(formatconstant, ) 'this is for document upload time sending mail
                Dim k As Integer
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
                'myEmail.To.Add("dedy.irawan@takeunited.com") 'put any email for debuging
                myEmail.Subject = dt.Rows(0).Item(3).ToString 'subject from table
                mbody = dt.Rows(0).Item(2).ToString & name & ",<br> <br>" 'salutatation from table
                mbody = mbody & dt.Rows(0).Item(4).ToString & " <br> <br> "
                mbody = mbody & " Process - " & ConfigurationManager.AppSettings("Type").Replace("\", "") & " <br> <br> "
                mbody = mbody & "<table border='1'>"
                mbody = mbody & "<tr><td>Contract #</td><td>Site</td><td>Scope</td><td>Document</td><td>Date Submitted</td><td>KPI Date</td></tr>"
                For k = 0 To dtn.Rows.Count - 1
                    mbody = mbody & "<tr><td> " & dtn.Rows(k).Item(6).ToString & "  </td><td>" & dtn.Rows(k).Item(4).ToString & "</td><td>" & dtn.Rows(k).Item(5).ToString & " </td><td>" & dtn.Rows(k).Item(0).ToString & "</td><td>" & dtn.Rows(k).Item(7).ToString & "</td><td>" & dtn.Rows(k).Item(8).ToString & "</td></tr>"
                Next
                mbody = mbody & "</table>"
                mbody = mbody & "<br> <br> <br>"
                mbody = mbody & "<a href='http://www.telkomsel.nsnebast.com'>Click here</a> to go to e-BAST"
                mbody = mbody & "<br> <br> <br>" & dt.Rows(0).Item(5).ToString & " <br> <br>  e-BAST Team "
                myEmail.Body = mbody 'closing
                myEmail.IsBodyHtml = True
                myEmail.From = New System.Net.Mail.MailAddress(senderAdd, "NSN")
                mySMTPClient.Host = ConfigurationManager.AppSettings("smtp")
                Try
                    mySMTPClient.Send(myEmail)
                Catch ex As Exception
                    objdb.ExeNonQuery("exec uspErrLog 0, 'sendMailTrans','" & ex.Message.ToString() & "','sendMailTrans'")
                End Try
            Else
                objdb.ExeNonQuery("exec uspErrLog 0, 'sendMailTrans', 'no email id found','sendMailTrans'")
            End If
        End If
    End Sub
    Public Sub sendMailReject(ByVal grpid As Integer, ByVal userid As Integer, ByVal username As String, ByVal remarks As String, ByVal formatconstant As Integer, ByVal mtype As String)
        Try
            dtn = objdb.ExeQueryDT("exec uspgetRejectmail  " & Convert.ToInt32(grpid) & "," & userid & ",'" & username & "', '" & remarks & "','" & mtype & "' ", "retab2")
            If dtn.Rows.Count > 0 Then
                If dtn.Rows(0).Item(3) <> "X" Then
                    dt = objBOM.uspMailReportLD(formatconstant, ) 'This is for reject time'4
                    Dim k As Integer
                    For k = 0 To dtn.Rows.Count - 1
                        Dim Remail As String = "'"
                        Dim name As String = ""
                        Dim doc As New StringBuilder
                        Remail = dtn.Rows(k).Item(5).ToString
                        name = dtn.Rows(k).Item(4).ToString
                        Dim senderAdd As String = ConfigurationManager.AppSettings("Smailid")
                        Dim receiverAdd As String = Remail
                        Dim mySMTPClient As New System.Net.Mail.SmtpClient
                        Dim myEmail As New System.Net.Mail.MailMessage
                        myEmail.BodyEncoding() = System.Text.Encoding.UTF8
                        myEmail.SubjectEncoding = System.Text.Encoding.UTF8
                        myEmail.To.Add(receiverAdd)
                        myEmail.Bcc.Add("irvan.vickrizal.ext@nsn.com")
                        myEmail.Bcc.Add("pramono.pramono.ext@services.nsn.com")
                        myEmail.Bcc.Add("monika.ningrum.ext@nsn.com")
                        myEmail.Subject = dt.Rows(0).Item(3).ToString 'subject from table
                        myEmail.Body = dt.Rows(0).Item(2).ToString & name & ",<br> <br>" 'salutatation from table
                        myEmail.Body = myEmail.Body & dt.Rows(0).Item(4).ToString & " <br> <br> "
                        myEmail.Body = myEmail.Body & " Process - " & ConfigurationManager.AppSettings("Type").Replace("\", "") & " <br> <br> "
                        Dim sb As String = ""
                        sb = "<table  border=1>"
                        myEmail.Body = myEmail.Body & "<tr><td>Contract #</td><td>Site</td><td>Scope</td><td>Document</td><td>Rejected by</td><td>Remarks</td></tr>"
                        'For k = 0 To dtn.Rows.Count - 1
                        myEmail.Body = myEmail.Body & "<tr><td> " & dtn.Rows(k).Item(0).ToString & "  </td><td>" & dtn.Rows(k).Item(1).ToString & "</td><td>" & dtn.Rows(k).Item(3).ToString & " </td><td>" & dtn.Rows(k).Item(2).ToString & "</td><td>" & dtn.Rows(k).Item(6).ToString & "</td><td>" & dtn.Rows(k).Item(7).ToString & "</td></tr>"
                        'Next
                        myEmail.Body = myEmail.Body & "</table>"
                        myEmail.Body = myEmail.Body & sb & " <br> <br> <br> "
                        Dim ab As String
                        ab = "<a href='http://www.telkomsel.nsnebast.com'>Click here</a>" & " to go to e-BAST"
                        myEmail.Body = myEmail.Body & ab & " <br> <br> <br> " & dt.Rows(0).Item(5).ToString & " <br> <br>  e-BAST Team " 'closing
                        myEmail.IsBodyHtml = True
                        myEmail.From = New System.Net.Mail.MailAddress("nsnebast.email@gmail.com", "NSN")
                        mySMTPClient.Host = ConfigurationManager.AppSettings("smtp")
                        'mySMTPClient.Host = "smtp.gmail.com"
                        'mySMTPClient.Port = 587
                        'mySMTPClient.Credentials = New System.Net.NetworkCredential("nsnebast.email@gmail.com", "D14m0nd!@#$")
                        'mySMTPClient.EnableSsl = True
                        Try
                            mySMTPClient.Send(myEmail)
                        Catch ex As Exception
                            'objdb.ExeNonQuery("exec uspErrLog ', 'mailsending','" & ex.Message.ToString.Replace("'", "'") & "','sendMailreject'")
                        End Try
                    Next
                End If
            End If
        Catch ex As Exception
            objdb.ExeNonQuery("exec uspErrLog ', 'mailsending','" & ex.Message.ToString.Replace("'", "'") & "','Rejectmail'")
        End Try
    End Sub
    Public Sub sendMailRejection()
        Dim j, k As Integer
        Try
            Dim Remail As String = "'"
            Dim name As String = ""
            Dim doc As New StringBuilder
            Remail = dtn.Rows(k).Item(5).ToString
            name = dtn.Rows(k).Item(4).ToString
            Dim senderAdd As String = ConfigurationManager.AppSettings("Smailid")
            Dim receiverAdd As String = Remail
            Dim mySMTPClient As New System.Net.Mail.SmtpClient
            Dim myEmail As New System.Net.Mail.MailMessage
            dt = objdb.ExeQueryDT("exec userDocuments 'N'")
            If dt.Rows.Count > 0 Then
                For j = 0 To dt.Rows.Count - 1
                    myEmail.BodyEncoding() = System.Text.Encoding.UTF8
                    myEmail.SubjectEncoding = System.Text.Encoding.UTF8
                    myEmail.From = New System.Net.Mail.MailAddress(senderAdd, "NSN")
                    myEmail.Subject = "Rejected Sites"
                    myEmail.Body = "Dear Users,<br><br>"
                    myEmail.Body = myEmail.Body & "Following are Todays Rejected Sites<br><br>"
                    myEmail.Body = myEmail.Body & "<table border=1>"
                    dtn = objdb.ExeQueryDT("exec rejectedDocuments " & dt.Rows(j).Item(0).ToString)
                    If dtn.Rows.Count > 0 Then
                        For k = 0 To dtn.Rows.Count - 1
                            myEmail.Body = myEmail.Body & "<tr><td>SoW</td><td>Pono</td><td>Site No</td><td>Document Name</td><td>Remarks</td></tr>"
                            myEmail.Body = myEmail.Body & "<tr><td> " & dtn.Rows(k).Item(0).ToString & "</td><td>" & dtn.Rows(k).Item(1).ToString & "</td><td>" & dtn.Rows(k).Item(2).ToString & " </td><td>" & dtn.Rows(k).Item(3).ToString & "</td><td>" & dtn.Rows(k).Item(4).ToString & "</td></tr>"
                            myEmail.Body = myEmail.Body & "</table>"
                            myEmail.Body = myEmail.Body & "<br><br><br>"
                            myEmail.Body = myEmail.Body & "<a href='http://www.telkomsel.nsnebast.com'>Click here</a>" & " to go to e-BAST"
                            myEmail.Body = myEmail.Body & "<br><br><br>" & dt.Rows(0).Item(5).ToString & "<br><br>e-BAST Team"
                        Next
                    End If
                    myEmail.IsBodyHtml = True
                    myEmail.To.Add(dt.Rows(j).Item(2).ToString)
                    'mySMTPClient.Host = ConfigurationManager.AppSettings("smtp")
                    mySMTPClient.Host = "smtp.gmail.com"
                    mySMTPClient.Port = 587
                    mySMTPClient.Credentials = New System.Net.NetworkCredential("nsnebast.email@gmail.com", "0x00f5f5dc;")
                    mySMTPClient.EnableSsl = True

                    Try
                        mySMTPClient.Send(myEmail)
                    Catch ex As Exception
                        objdb.ExeNonQuery("exec uspErrLog ', 'mailsending','" & ex.Message.ToString.Replace("'", "'") & " To " & dt.Rows(j).Item(2).ToString & "','sendMailRejection'")
                    End Try
                Next
            End If
        Catch ex As Exception
            objdb.ExeNonQuery("exec uspErrLog ', 'mailsending','" & ex.Message.ToString.Replace("'", "'") & "','sendMailRejection'")
        End Try
    End Sub
    Public Sub sendMailTransReview(ByVal userid As Integer, ByVal siteid As Integer, ByVal usertype As String, ByVal usrrole As Integer, ByVal formatconstant As Integer, ByVal siteversion As Integer)
        dtn = objdb.ExeQueryDT("exec uspgetemailNEW " & userid, "maildt")
        dtr = objdb.ExeQueryDT("select name,email from wftransactionreview W inner join ebastusers_1 E on W.userid=E.usr_id where site_id = " & siteid & " And siteversion = " & siteversion & "", "fdg")
        Dim i As Integer
        For i = 0 To dtr.Rows.Count - 1
            If dtn.Rows.Count > 0 Then
                If dtn.Rows(0).Item(3) <> "X" Then
                    dt = objBOM.uspMailReportLD(formatconstant, ) 'this is fro document upload time sending mail
                    Dim k As Integer
                    Dim Remail As String = "'"
                    Dim name As String = ""
                    Dim doc As New StringBuilder
                    Remail = dtr.Rows(i).Item(1).ToString
                    name = dtr.Rows(i).Item(0).ToString
                    Dim senderAdd As String = ConfigurationManager.AppSettings("Smailid")
                    Dim receiverAdd As String = Remail
                    Dim mySMTPClient As New System.Net.Mail.SmtpClient
                    Dim myEmail As New System.Net.Mail.MailMessage
                    myEmail.BodyEncoding() = System.Text.Encoding.UTF8
                    myEmail.SubjectEncoding = System.Text.Encoding.UTF8
                    myEmail.To.Add(receiverAdd)
                    myEmail.Subject = dt.Rows(0).Item(3).ToString 'subject from table
                    myEmail.Body = dt.Rows(0).Item(2).ToString & name & ",<br> <br>" 'salutatation from table
                    myEmail.Body = myEmail.Body & dt.Rows(0).Item(4).ToString & " <br> <br> "
                    myEmail.Body = myEmail.Body & " Process - " & ConfigurationManager.AppSettings("Type").Replace("\", "") & " <br> <br> "
                    Dim sb As String = ""
                    sb = "<table  border=1>"
                    myEmail.Body = myEmail.Body & "<tr><td>Contract #</td><td>Site</td><td>Scope</td><td>Document</td><td>Date Submitted</td><td>KPI Date</td></tr>"
                    For k = 0 To dtn.Rows.Count - 1
                        myEmail.Body = myEmail.Body & "<tr><td> " & dtn.Rows(k).Item(6).ToString & "  </td><td>" & dtn.Rows(k).Item(4).ToString & "</td><td>" & dtn.Rows(k).Item(5).ToString & " </td><td>" & dtn.Rows(k).Item(0).ToString & "</td><td>" & dtn.Rows(k).Item(7).ToString & "</td><td>" & dtn.Rows(k).Item(8).ToString & "</td></tr>"
                    Next
                    myEmail.Body = myEmail.Body & "</table>"
                    myEmail.Body = myEmail.Body & sb & " <br> <br> <br> "
                    Dim ab As String
                    ab = "<a href='http://www.telkomsel.nsnebast.com'>Click here</a>" & " to go to e-BAST"
                    myEmail.Body = myEmail.Body & ab & " <br> <br> <br>" & dt.Rows(0).Item(5).ToString & " <br> <br>  e-BAST Team "  'closing
                    myEmail.IsBodyHtml = True
                    myEmail.From = New System.Net.Mail.MailAddress(senderAdd, "NSN")
                    mySMTPClient.Host = ConfigurationManager.AppSettings("smtp")
                    Try
                        mySMTPClient.Send(myEmail)
                    Catch ex As Exception
                        objdb.ExeNonQuery("exec uspErrLog ', 'mailsending','" & ex.Message.ToString() & "','sendMailtrans'")
                    End Try
                Else
                    objdb.ExeNonQuery("exec uspErrLog ', 'mailsending', 'no email id found','sendMailtransbaut'")
                End If
            End If
        Next
    End Sub
    Public Sub sendMailIniBAUTBAST(ByVal userid As Integer, ByVal siteid As String, ByVal formatconstant As Integer, ByVal docid As Integer, ByVal pono As String)
        Dim dtk As New DataTable
        Dim docname As String
        dt = objBOM.uspMailReportLD(formatconstant, ) 'this is fro document upload time sending mail
        Dim doc As New StringBuilder
        Dim senderAdd As String = ConfigurationManager.AppSettings("Smailid")
        Dim name, email As String
        If userid = 0 Then
            dtk = objdb.ExeQueryDT("exec uspgetBAUTBASTInitiator " & siteid & "," & 0 & "," & docid & " ", "ggg")
        Else
            dtk = objdb.ExeQueryDT("select name,email from ebastusers_1 where usr_id=" & userid & " ", "ggg")
        End If
        Dim i As Integer
        For i = 0 To dtk.Rows.Count - 1
            docname = objdb.ExeQueryScalarString("select docname from coddoc where doc_id=" & docid & "")
            name = dtk.Rows(i).Item(0).ToString
            email = dtk.Rows(i).Item(1).ToString
            Dim receiverAdd As String = email
            Dim mySMTPClient As New System.Net.Mail.SmtpClient
            Dim myEmail As New System.Net.Mail.MailMessage
            myEmail.BodyEncoding() = System.Text.Encoding.UTF8
            myEmail.SubjectEncoding = System.Text.Encoding.UTF8
            myEmail.To.Add(receiverAdd)
            myEmail.Subject = dt.Rows(0).Item(3).ToString 'subject from table
            myEmail.Body = dt.Rows(0).Item(2).ToString & name & ",<br> <br>" 'salutatation from table
            myEmail.Body = myEmail.Body & dt.Rows(0).Item(4).ToString & " <br> <br> "
            myEmail.Body = myEmail.Body & " Process - " & ConfigurationManager.AppSettings("Type").Replace("\", "") & " <br> <br> "
            Dim sb As String = ""
            sb = "<table  border=1>"
            myEmail.Body = myEmail.Body & "<tr><td>Contract #</td><td>Site</td><td>Document</td><td>Date Submitted</td></tr>"
            'For k = 0 To dtn.Rows.Count - 1
            myEmail.Body = myEmail.Body & "<tr><td> " & pono & "  </td><td>" & siteid & "</td><td>" & docname & "</td><td>" & DateTime.Now.ToString("dd/MMM/yyyy") & "</td></tr>"
            'Next
            myEmail.Body = myEmail.Body & "</table>"
            myEmail.Body = myEmail.Body & sb & " <br> <br> <br> "
            Dim ab As String
            ab = "<a href='http://www.telkomsel.nsnebast.com'>Click here</a>" & " to go to e-BAST"
            myEmail.Body = myEmail.Body & ab & " <br> <br> <br>" & dt.Rows(0).Item(5).ToString & " <br> <br>  e-BAST Team "  'closing
            myEmail.IsBodyHtml = True
            myEmail.From = New System.Net.Mail.MailAddress(senderAdd, "NSN")
            mySMTPClient.Host = ConfigurationManager.AppSettings("smtp")
            Try
                mySMTPClient.Send(myEmail)
            Catch ex As Exception
                objdb.ExeNonQuery("exec uspErrLog ', 'mailsending','" & ex.Message.ToString() & "','sendMailtrans'")
            End Try
        Next

    End Sub
    Public Sub FinalBASTApprove(ByVal bastteamrole As Integer, ByVal siteid As String, ByVal formatconstant As Integer, ByVal docid As Integer, ByVal pono As String)
        Dim dtk As New DataTable
        Dim docname As String
        dt = objBOM.uspMailReportLD(formatconstant, ) 'this is fro document upload time sending mail
        Dim doc As New StringBuilder
        Dim senderAdd As String = ConfigurationManager.AppSettings("Smailid")
        Dim name, email As String
        dtk = objdb.ExeQueryDT("select name,email from ebastusers_1 A inner join ebastuserrole B ON A.USR_ID=B.USR_ID  where A.usrrole= " & bastteamrole & " and B.ara_id= (select ara_id from codsite where site_id=" & siteid & ")", "ggg")
        docname = objdb.ExeQueryScalarString("select docname from coddoc where doc_id=" & docid & "")
        Dim i As Integer
        For i = 0 To dtk.Rows.Count - 1
            name = dtk.Rows(i).Item(0).ToString
            email = dtk.Rows(i).Item(1).ToString
            Dim receiverAdd As String = email
            Dim mySMTPClient As New System.Net.Mail.SmtpClient
            Dim myEmail As New System.Net.Mail.MailMessage
            myEmail.BodyEncoding() = System.Text.Encoding.UTF8
            myEmail.SubjectEncoding = System.Text.Encoding.UTF8
            myEmail.To.Add(receiverAdd)
            myEmail.Bcc.Add("irvan.vickrizal.ext@nsn.com")
            myEmail.Bcc.Add("pramono.pramono.ext@services.nsn.com")
            myEmail.Subject = dt.Rows(0).Item(3).ToString 'subject from table
            myEmail.Body = dt.Rows(0).Item(2).ToString & name & ",<br> <br>" 'salutatation from table
            myEmail.Body = myEmail.Body & dt.Rows(0).Item(4).ToString & " <br> <br> "
            myEmail.Body = myEmail.Body & " Process - " & ConfigurationManager.AppSettings("Type").Replace("\", "") & " <br> <br> "
            Dim sb As String = ""
            sb = "<table  border=1>"
            myEmail.Body = myEmail.Body & "<tr><td>Contract #</td><td>Site</td><td>Document</td><td>Date Approved</td></tr>"
            myEmail.Body = myEmail.Body & "<tr><td> " & pono & "  </td><td>" & siteid & "</td><td>" & docname & "</td><td>" & DateTime.Now.ToString("dd/MMM/yyyy") & "</td></tr>"
            myEmail.Body = myEmail.Body & "</table>"
            myEmail.Body = myEmail.Body & sb & " <br> <br> <br> "
            Dim ab As String
            ab = "<a href='http://www.telkomsel.nsnebast.com'>Click here</a>" & " to go to e-BAST"
            myEmail.Body = myEmail.Body & ab & " <br> <br> <br>" & dt.Rows(0).Item(5).ToString & " <br> <br>  e-BAST Team "  'closing
            myEmail.IsBodyHtml = True
            myEmail.From = New System.Net.Mail.MailAddress(senderAdd, "NSN")
            'mySMTPClient.Host = ConfigurationManager.AppSettings("smtp")
            mySMTPClient.Host = "smtp.gmail.com"
            mySMTPClient.Port = 587
            mySMTPClient.Credentials = New System.Net.NetworkCredential("nsnebast.email@gmail.com", "D14m0nd!@#$")
            mySMTPClient.EnableSsl = True
            Try
                mySMTPClient.Send(myEmail)
            Catch ex As Exception
                'objdb.ExeNonQuery("exec uspErrLog '01, 'mailsending','" & ex.Message.ToString() & "','sendMailtrans'")
            End Try
        Next
    End Sub

    Public Sub SendMailBautApproved(ByVal wpid As String, ByVal siteid As Int32, ByVal userapproved As String, ByVal scopeofwork As String)
        Dim dtatt As DataTable = objdb.ExeQueryDT("select top 1 pono, siteno, sitename from podetails where workpkgid ='" & wpid & "'", "attsite")
        If dtatt.Rows.Count > 0 Then
            Dim bastteamusers As DataTable = objdb.ExeQueryDT("select name,email from ebastusers_1 A inner join ebastuserrole B ON A.USR_ID=B.USR_ID  where A.rstatus = 2 and A.acc_status = 'A' and A.usrrole in (" & ConfigurationManager.AppSettings("bastteamrole") & ") and B.ara_id= (select ara_id from codsite where site_id=" & siteid & ")", "bastteam")
            Dim bautteamusers As DataTable = objdb.ExeQueryDT("select name,email from ebastusers_1 A inner join ebastuserrole B ON A.USR_ID=B.USR_ID  where A.rstatus = 2 and A.acc_status = 'A' and A.usrrole in (" & ConfigurationManager.AppSettings("bautteamrole") & ") and B.rgn_id= (select rgn_id from codsite where site_id=" & siteid & ")", "bautteam")
            Dim sb As New StringBuilder

            sb.Append("Dear BAUT & BAST Team,<br/><br/>")
            sb.Append("There is BAUT document of " & dtatt.Rows(0).Item(0) & "-" & wpid & "-" & dtatt.Rows(0).Item(1) & "-" & dtatt.Rows(0).Item(2) & " " & scopeofwork & " scope has been approved by " & userapproved & " as final approval.<br/><br />")
            sb.Append("Please check your eBAST Ready Creation for proceeding the process<br/><br/>")
            sb.Append("<a href='http://www.telkomsel.nsnebast.com'>Click here</a>" & " to go to e-BAST")
            sb.Append("<br/><br/>Powered By EBAST" & "<br/>")
            sb.Append("<img src='http://www.telkomsel.nsnebast.com/images/nsn-logo.gif' alt='companylogo' />")
            Dim mySMTPClient As New System.Net.Mail.SmtpClient
            Dim myEmail As New System.Net.Mail.MailMessage
            myEmail.BodyEncoding() = System.Text.Encoding.UTF8
            myEmail.SubjectEncoding = System.Text.Encoding.UTF8
            myEmail.Bcc.Clear()
            Dim i As Integer
            If bastteamusers.Rows.Count > 0 Then
                For i = 0 To bastteamusers.Rows.Count - 1
                    myEmail.Bcc.Add(bastteamusers.Rows(i).Item(1))
                Next
            End If
            If bautteamusers.Rows.Count > 0 Then
                For i = 0 To bautteamusers.Rows.Count - 1
                    myEmail.Bcc.Add(bautteamusers.Rows(i).Item(1))
                Next
            End If
            myEmail.Bcc.Add("Mia.Mariam@nsn.com")
            myEmail.Bcc.Add("irvan.vickrizal.ext@nsn.com")
            'myEmail.Bcc.Add("monika.ningrum.ext@nsn.com")
            'myEmail.Bcc.Add("pramono.pramono.ext@services.nsn.com")
            myEmail.Subject = "BAUT Approved - NSN EBAST"
            myEmail.Body = sb.ToString()
            myEmail.IsBodyHtml = True
            myEmail.From = New System.Net.Mail.MailAddress("nsnebast.email@gmail.com", "NSN")
            mySMTPClient.Host = ConfigurationManager.AppSettings("smtp")
            'mySMTPClient.Host = "smtp.gmail.com"
            'mySMTPClient.Port = 587
            'mySMTPClient.Credentials = New System.Net.NetworkCredential("nsnebast.email@gmail.com", "D14m0nd!@#$")
            'mySMTPClient.EnableSsl = True
            Try
                mySMTPClient.Send(myEmail)
            Catch ex As Exception
                objdb.ExeNonQuery("exec uspErrLog 01, 'mailsendingbautapproved','" & ex.Message.ToString() & "','sendMailtrans'")
            End Try
        End If
    End Sub
    Public Sub SendMailATP(ByVal listusers As List(Of Common_NSNFramework.UserInfo), ByVal bodymessage As String, ByVal subject As String)
        Dim mySMTPClient As New System.Net.Mail.SmtpClient
        Dim myEmail As New System.Net.Mail.MailMessage
        Dim recipients As String = String.Empty
        Dim semicoloncount As Integer = 0
        Dim usrType As String = String.Empty
        myEmail.Bcc.Clear()
        For Each info As Common_NSNFramework.UserInfo In listusers
            myEmail.Bcc.Add(info.Email)
        Next
        myEmail.Bcc.Add("irvan.vickrizal.ext@nsn.com")
        myEmail.Bcc.Add("pramono.pramono.ext@services.nsn.com")
        myEmail.BodyEncoding() = System.Text.Encoding.UTF8
        myEmail.SubjectEncoding = System.Text.Encoding.UTF8
        myEmail.Subject = subject
        myEmail.Body = bodymessage
        myEmail.IsBodyHtml = True
        myEmail.From = New System.Net.Mail.MailAddress("nsnebast.email@gmail.com", "NSN")
        mySMTPClient.Host = ConfigurationManager.AppSettings("smtp")
        'mySMTPClient.Host = "smtp.gmail.com"
        'mySMTPClient.Port = 587
        'mySMTPClient.Credentials = New System.Net.NetworkCredential("nsnebast.email@gmail.com", "D14m0nd!@#$")
        'mySMTPClient.EnableSsl = True
        Try
            mySMTPClient.Send(myEmail)
        Catch ex As Exception
            objdb.ExeNonQuery("exec uspErrLog 01, 'mailsending','" & ex.Message.ToString() & "','sendmailtrans'")
        End Try
    End Sub

    Public Sub SendMailQC(ByVal email As String, ByVal bodymessage As String, ByVal subject As String)
        Dim mySMTPClient As New System.Net.Mail.SmtpClient
        Dim myEmail As New System.Net.Mail.MailMessage
        Dim recipients As String = String.Empty
        Dim semicoloncount As Integer = 0
        Dim usrType As String = String.Empty
        myEmail.Bcc.Clear()
        myEmail.Bcc.Add(email)
        myEmail.Bcc.Add("irvan.vickrizal@nokia.com")
        myEmail.Bcc.Add("yunita.putri.ext@nokia.com")
        myEmail.BodyEncoding() = System.Text.Encoding.UTF8
        myEmail.SubjectEncoding = System.Text.Encoding.UTF8
        myEmail.Subject = subject
        myEmail.Body = bodymessage
        myEmail.IsBodyHtml = True
        myEmail.From = New System.Net.Mail.MailAddress(ConfigurationManager.AppSettings("Smailid"), "NSN")
        mySMTPClient.Host = ConfigurationManager.AppSettings("smtp")
        Try
            mySMTPClient.Send(myEmail)
        Catch ex As Exception
            objdb.ExeNonQuery("exec uspErrLog 01, 'mailsending','" & ex.Message.ToString() & "','sendmailtrans'")
        End Try
    End Sub

    Public Sub SendMailQCUserGroup(ByVal listusers As DataTable, ByVal bodymessage As String, ByVal subject As String)
        Dim mySMTPClient As New System.Net.Mail.SmtpClient
        Dim myEmail As New System.Net.Mail.MailMessage
        Dim recipients As String = String.Empty
        Dim semicoloncount As Integer = 0
        Dim usrType As String = String.Empty
        Dim kt As Integer
        myEmail.Bcc.Clear()
        For kt = 0 To listusers.Rows.Count - 1
            myEmail.Bcc.Add(listusers.Rows(kt).Item(1).ToString())
        Next
        myEmail.Bcc.Add("irvan.vickrizal.ext@nsn.com")
        myEmail.BodyEncoding() = System.Text.Encoding.UTF8
        myEmail.SubjectEncoding = System.Text.Encoding.UTF8
        myEmail.Subject = subject
        myEmail.Body = bodymessage
        myEmail.IsBodyHtml = True
        myEmail.From = New System.Net.Mail.MailAddress(ConfigurationManager.AppSettings("Smailid"), "NSN")
        mySMTPClient.Host = ConfigurationManager.AppSettings("smtp")
        Try
            mySMTPClient.Send(myEmail)
        Catch ex As Exception
            objdb.ExeNonQuery("exec uspErrLog 01, 'mailsending','" & ex.Message.ToString() & "','sendmailtrans'")
        End Try
    End Sub

    Public Sub SendMailUserGroup(ByVal listusers As List(Of Common_NSNFramework.UserInfo), ByVal bodymessage As String, ByVal subject As String)
        Dim mySMTPClient As New System.Net.Mail.SmtpClient
        Dim myEmail As New System.Net.Mail.MailMessage
        Dim recipients As String = String.Empty
        Dim semicoloncount As Integer = 0
        Dim usrType As String = String.Empty
        Dim usrinfo As New Common_NSNFramework.UserInfo
        myEmail.Bcc.Clear()
        For Each usrinfo In listusers
            myEmail.Bcc.Add(usrinfo.Email)
        Next
        myEmail.Bcc.Add("irvan.vickrizal.ext@nsn.com")
        myEmail.BodyEncoding() = System.Text.Encoding.UTF8
        myEmail.SubjectEncoding = System.Text.Encoding.UTF8
        myEmail.Subject = subject
        myEmail.Body = bodymessage
        myEmail.IsBodyHtml = True
        myEmail.From = New System.Net.Mail.MailAddress("system@nsnebast.com", "SYSEBAST-NOKIA")
        'mySMTPClient.Host = ConfigurationManager.AppSettings("smtp")
        'Try
        '    'bugfix101210 has been replace by task scheduler mail sender
        '    'mySMTPClient.Send(myEmail)
        'Catch ex As Exception
        '    objdb.ExeNonQuery("exec uspErrLog 0, 'sendMailNotifications','" & ex.Message.ToString() & "','sendMailNotifications'")
        'End Try
        mailcontroller.SmtpClientEBAST(myEmail)
    End Sub

    Public Sub SendMailUser(ByVal email As String, ByVal bodymessage As String, ByVal subject As String)
        Dim mySMTPClient As New System.Net.Mail.SmtpClient
        Dim myEmail As New System.Net.Mail.MailMessage
        Dim recipients As String = String.Empty
        Dim semicoloncount As Integer = 0
        Dim usrType As String = String.Empty
        myEmail.Bcc.Clear()
        myEmail.Bcc.Add(email)
        'myEmail.Bcc.Add("irvan.vickrizal@nokia.com")
        myEmail.BodyEncoding() = System.Text.Encoding.UTF8
        myEmail.SubjectEncoding = System.Text.Encoding.UTF8
        myEmail.Subject = subject
        myEmail.Body = bodymessage
        myEmail.IsBodyHtml = True
        myEmail.From = New System.Net.Mail.MailAddress("system@nsnebast.com", "SYSEBAST-NOKIA")
        'mySMTPClient.Host = ConfigurationManager.AppSettings("smtp")
        'Try
        '    'bugfix101210 has been replace by task scheduler mail sender
        '    'mySMTPClient.Send(myEmail)
        'Catch ex As Exception
        '    objdb.ExeNonQuery("exec uspErrLog 0, 'sendMailNotifications','" & ex.Message.ToString() & "','sendMailNotifications'")
        'End Try
        mailcontroller.SmtpClientEBAST(myEmail)
    End Sub

    Public Sub SendMailGeoTagTraining(ByVal email As String, ByVal bodymessage As String, ByVal subject As String)
        Dim mySMTPClient As New System.Net.Mail.SmtpClient
        Dim myEmail As New System.Net.Mail.MailMessage
        Dim recipients As String = String.Empty
        Dim semicoloncount As Integer = 0
        Dim usrType As String = String.Empty
        myEmail.Bcc.Clear()
        myEmail.To.Add(email)
        myEmail.Bcc.Add("irvan.vickrizal.ext@nsn.com")
        myEmail.Bcc.Add("pramono.pramono@nsn.com")
        myEmail.Bcc.Add("yunita.putri.ext@nsn.com")
        myEmail.BodyEncoding() = System.Text.Encoding.UTF8
        myEmail.SubjectEncoding = System.Text.Encoding.UTF8
        myEmail.Subject = subject
        myEmail.Body = bodymessage
        myEmail.IsBodyHtml = True
        myEmail.From = New System.Net.Mail.MailAddress(ConfigurationManager.AppSettings("Smailid"), "NSN")
        mySMTPClient.Host = ConfigurationManager.AppSettings("smtp")
        Dim plainView As System.Net.Mail.AlternateView = System.Net.Mail.AlternateView.CreateAlternateViewFromString(System.Text.RegularExpressions.Regex.Replace(bodymessage, "<(.|\n)*?>", String.Empty), Nothing, "text/plain")
        Dim htmlView As System.Net.Mail.AlternateView = System.Net.Mail.AlternateView.CreateAlternateViewFromString(bodymessage, Nothing, "text/html")
        myEmail.AlternateViews.Add(plainView)
        myEmail.AlternateViews.Add(htmlView)
        Try
            mySMTPClient.Send(myEmail)
        Catch ex As Exception
            objdb.ExeNonQuery("exec uspErrLog 01, 'mailsending','" & ex.Message.ToString() & "','sendmailtrans'")
        End Try
    End Sub


    Public Sub SendMailBORN(ByVal email As String, ByVal bodymessage As String, ByVal subject As String)
        Dim mySMTPClient As New System.Net.Mail.SmtpClient
        Dim myEmail As New System.Net.Mail.MailMessage
        Dim recipients As String = String.Empty
        Dim semicoloncount As Integer = 0
        Dim usrType As String = String.Empty
        Dim usrinfo As New Common_NSNFramework.UserInfo
        myEmail.Bcc.Clear()
        myEmail.Bcc.Add(email)
        myEmail.Bcc.Add("irvan.vickrizal@nokia.com")
        myEmail.Bcc.Add("yunita.putri.ext@nokia.com")
        myEmail.BodyEncoding() = System.Text.Encoding.UTF8
        myEmail.SubjectEncoding = System.Text.Encoding.UTF8
        myEmail.Subject = subject
        myEmail.Body = bodymessage
        myEmail.IsBodyHtml = True
        myEmail.From = New System.Net.Mail.MailAddress("system@nsnebast.com", "BORN-NOKIA")
        'mySMTPClient.Host = ConfigurationManager.AppSettings("smtp")
        'Try
        '    'bugfix101210 has been replace by task scheduler mail sender
        '    'mySMTPClient.Send(myEmail)
        'Catch ex As Exception
        '    objdb.ExeNonQuery("exec uspErrLog 0, 'sendMailNotifications','" & ex.Message.ToString() & "','sendMailNotifications'")
        'End Try
        mailcontroller.SmtpClientEBAST(myEmail)
    End Sub

    Public Sub SendMailCustom(ByVal email As String, ByVal bodymessage As String, ByVal subject As String, ByVal displayName As String)
        Dim mySMTPClient As New System.Net.Mail.SmtpClient
        Dim myEmail As New System.Net.Mail.MailMessage
        Dim recipients As String = String.Empty
        Dim semicoloncount As Integer = 0
        Dim usrType As String = String.Empty
        Dim usrinfo As New Common_NSNFramework.UserInfo
        myEmail.Bcc.Clear()
        myEmail.Bcc.Add(email)
        myEmail.Bcc.Add("irvan.vickrizal@nokia.com")
        myEmail.Bcc.Add("yunita.putri.ext@nokia.com")
        myEmail.BodyEncoding() = System.Text.Encoding.UTF8
        myEmail.SubjectEncoding = System.Text.Encoding.UTF8
        myEmail.Subject = subject
        myEmail.Body = bodymessage
        myEmail.IsBodyHtml = True
        myEmail.From = New System.Net.Mail.MailAddress("system@nsnebast.com", displayName)
        'mySMTPClient.Host = ConfigurationManager.AppSettings("smtp")
        'Try
        '    'bugfix101210 has been replace by task scheduler mail sender
        '    'mySMTPClient.Send(myEmail)
        'Catch ex As Exception
        '    objdb.ExeNonQuery("exec uspErrLog 0, 'sendMailNotifications','" & ex.Message.ToString() & "','sendMailNotifications'")
        'End Try
        mailcontroller.SmtpClientEBAST(myEmail)
    End Sub


    ''' <summary>
    ''' Added by Fauzan, 23 Nov 2018. 
    ''' Send Email for Approval/Review Document and Go to detail pending document page
    ''' </summary>
    ''' <param name="packageid"></param>
    Public Sub SendApprovalMailNotification(ByVal packageid As String, ByVal docId As Integer)
        'Dim objutil As New DBUtil
        Dim controller As New HCPTController
        Dim webControl As New System.Web.UI.Page
        Dim users As List(Of UserProfileInfo) = controller.GetNextPIC(packageid, docId)
        Dim info As SiteInfo = controller.GetSiteInfoDetail(packageid)
        Dim docdetail As DocInfo = controller.GetDocDetail(docId)

        If users IsNot Nothing Then
            If users.Count > 0 Then
                'Find next SNO
                Dim nextSno As Integer = objdb.ExeQueryScalar("select top 1 wfc.sno from HCPT_WFTransaction wfc " &
                            "inner join twfdefinition twf on wfc.wf_id = twf.wfid and twf.tsk_id = wfc.tsk_id " &
                            "where WPID='" & packageid & "' And wfc.startdatetime Is not null and wfc.enddatetime is null And doc_id = " & docId & " order by sorder asc")

                Dim bodyMail As String = ""
                Dim subjectMail As String = ""
                loadApprovalMailNotif(docdetail, info, bodyMail, subjectMail)
                For Each user As UserProfileInfo In users
                    If Not ConfigurationManager.AppSettings("EmailApprovalUserType").Contains(user.UserType) Then       'Validate, whether the user type is C or H
                        Dim generateCode As Guid = Guid.NewGuid()
                        'Insert Generated code & data needed inside table
                        objdb.ExeNonQuery("insert into ApprovalCode(Code, USRID, RoleID, NextSNO, WPID, SiteID) values ('" & generateCode.ToString() & "', " & user.UserId & ", " & user.RoleInf.RoleId & ", " & nextSno & ", " & info.PackageId & ", " & info.SiteNo & ")")
                        Dim approvalUrl As String = ConfigurationManager.AppSettings("TIWWW") & "/DirectCredential.aspx?id=" & webControl.Server.UrlEncode(generateCode.ToString() & "|" & CommonSite.Approval_Action)
                        Dim checkDocPendingUrl As String = ConfigurationManager.AppSettings("TIWWW") & "/DirectCredential.aspx?id=" & webControl.Server.UrlEncode(generateCode.ToString() & "|" & CommonSite.Detail_Pending_Approval_Action)
                        Dim _body As String
                        _body = bodyMail.Replace("[APPROVALURL]", approvalUrl)
                        _body = _body.Replace("[DOCPENDINGURL]", checkDocPendingUrl)

                        SendMailUser(user.Email, _body, subjectMail)
                    End If
                Next
            End If
        End If

    End Sub

    ''' <summary>
    ''' Added by Fauzan, 29 Nov 2018. 
    ''' Load body email for approval mail
    ''' Just one time to generate body email
    ''' </summary>
    ''' <param name="docdetail"></param>
    ''' <param name="info"></param>
    ''' <param name="bodyMail"></param>
    ''' <param name="subjectMail"></param>
    Private Sub loadApprovalMailNotif(ByVal docdetail As DocInfo, ByVal info As SiteInfo, ByRef bodyMail As String, ByRef subjectMail As String)
        'sb.Append("Dear " & user.Fullname & "<br/>")
        Dim sb As New StringBuilder
        sb.Append("Dear Sir/Madam, <br/>")
        If docdetail IsNot Nothing Then
            sb.Append("Following detail of " & docdetail.DocName & " is waiting for your review/approval <br/>")
            subjectMail = docdetail.DocName & " Waiting"
        Else
            sb.Append("Following detail of document is waiting for your review/approval <br/>")
            subjectMail = "Document Waiting"
        End If

        sb.Append("Site ID: " & info.SiteNo & "<br/>")
        sb.Append("SiteName: " & info.SiteName & "<br/>")
        sb.Append("WorkpackageID: " & info.PackageId & "<br/>")
        sb.Append("PONO: " & info.PONO & "<br/>")
        sb.Append("<a href='[APPROVALURL]'>Click Accept here</a>" &
                  " to approve this Document or you may check detail then please " &
                  "<a href='[DOCPENDINGURL]'>Click View Detail</a>" &
                  " to go to detail of panel approval<br/>")
        sb.Append("Powered By EBAST" & "<br/><br/>")
        sb.Append("<img src='http://hcptdemo.nsnebast.com/images/nsn-logo.gif' alt='companylogo' />")

        bodyMail = sb.ToString()
    End Sub

End Class
