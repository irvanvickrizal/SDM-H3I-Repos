Imports AXmsCtrl
Imports DGSignPWD
Imports BusinessLogic
Imports Common
Imports System.Data
Partial Class Digital_signature_multi
    Inherits System.Web.UI.Page
    Dim objBo As New BODashBoard
    Dim objBOsite As New BOSiteDocs
    Shared strPath As String = ""
    Dim intPage, intX, intY, intHeight, intWidth As New Integer
    Dim objdb As New DBUtil
    Dim dtn As New DataTable
    Dim objBOM As New BOMailReport
    Dim dt As New DataTable
    Dim objdg As New UserInfo
    Dim objsms As New mcore.sms
    Dim objmail As New takemail

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        objsms.License.Company = "TAKE UNITED SDN BHD"
        objsms.License.LicenseType = "PRO-DISTRIBUTION"
        objsms.License.Key = "TRQT-NF2L-Y7V9-HF4E"
        If Page.IsPostBack = False Then
            txtUserName.Text = Session("User_Login")
            intHeight = 50
            intWidth = 150
            GetPDFDocument()
            GetBautWCTR()
            If Request.QueryString("docname") = Constants._Doc_BAUT Then
                binddoc()
            End If

        End If


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
        dt = objBo.DigitalSign(Convert.ToInt32(Request.QueryString("id")))
        If (dt.Rows.Count > 0) Then
            strPath = dt.Rows(0)("docpath").ToString()
            HDPono.Value = dt.Rows(0)("pono").ToString()
            HDDocid.Value = dt.Rows(0)("docid").ToString()
            HDPath.Value = dt.Rows(0)("docpath").ToString()
            hdnx.Value = dt.Rows(0)("xval")
            hdny.Value = dt.Rows(0)("yval")
            hdpageNo.Value = dt.Rows(0)("pageno")
            PDFViwer.Attributes.Add("src", ConfigurationManager.AppSettings("Vpath") + strPath)
        End If


    End Sub
    Protected Sub BtnSign_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSign.Click
        Dim npwd1 As String = ""
        npwd1 = "12345678"

        Dim j As Integer
        ' Dim objdb As New DBUtil
        j = objdb.ExeQueryScalar("select count(*) from dgpassword where username='" & txtUserName.Text & "' and password='" & txtPassword.Text & "'")
        If j > 0 Then
            Try
                Dim Flags As Integer = 0
                Dim IntCount As Integer = 0
                If 1 = 1 Then Flags = Flags Or SAPIWrapper.SAPI_ENUM_DRAWING_ELEMENT_GRAPHICAL_IMAGE
                If 1 = 1 Then Flags = Flags Or SAPIWrapper.SAPI_ENUM_DRAWING_ELEMENT_SIGNED_BY
                If 1 = 0 Then Flags = Flags Or SAPIWrapper.SAPI_ENUM_DRAWING_ELEMENT_REASON
                If 1 = 1 Then Flags = Flags Or SAPIWrapper.SAPI_ENUM_DRAWING_ELEMENT_TIME
                Dim strResult As String = SAPIWrapper.SAPI_sign_file(ConfigurationManager.AppSettings("FPath") + HDPath.Value, Nothing, txtUserName.Text, _
                                          npwd1, hdpageNo.Value, hdnx.Value, hdny.Value, intHeight, intWidth, False, "test", Flags, "baut" & Request.QueryString("id"))
                If (strResult = Constants._DigitalSign_Result) Then
                    If (HDDocid.Value = CommonSite.BAUTID) Then
                        GetBautSign(npwd1)
                    End If
                    Dim i As Integer = -1
                    'i = objBo.uspDocApproved(Convert.ToInt32(Request.QueryString("id")), CommonSite.UserId(), CommonSite.UserName(), CommonSite.RollId(), ConfigurationManager.AppSettings("BAUTID"), ConfigurationManager.AppSettings("BASTID"))
                    i = objdb.ExeQueryScalar("Exec [uspDocApproved] " & Convert.ToInt32(Request.QueryString("id")) & "," & CommonSite.UserId() & ",'" & CommonSite.UserName() & "'," & CommonSite.RollId() & "," & ConfigurationManager.AppSettings("BAUTID") & "," & ConfigurationManager.AppSettings("BASTID") & "")
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
                        objdb.ExeNonQuery("exec uspErrLog '', '" & Request.QueryString("id") & "','" & nextid & "' ,'sendmailtransmulti1'")
                        If nextid = 0 Then nextid = 1
                        Try
                            objmail.sendmailTrans(nextid, 0, "0", 0, ConfigurationManager.AppSettings("uploadmailconst"))
                        Catch ex As Exception
                            objdb.ExeNonQuery("exec uspErrLog '', 'mailsending','" & ex.Message.ToString.Replace("'", "''") & "','sendmailtransupload'")
                        End Try
                        '**************************************************************************
                        'IF it is BAST  second statge then we need to generate coresponding WCTR
                        '**********************
                        'Data updated by taking bast revie data
                        'Generate PDF
                        'update sitedoc
                        'do insert trans 
                        '*********************
                        ' this if is only for wctrbast
                        If InStr(Request.QueryString("docname"), "(BAST)") > 0 Then
                            Dim t As Integer = 0
                            t = objdb.ExeQueryScalar("select count(*) from wftransaction where STARTDATETIME IS NOT NULL AND ENDDATETIME IS NULL and site_id=(select site_id from wftransaction where sno=" & Request.QueryString("id") & ") and siteversion=" & Request.QueryString("version") & "  and docid=" & ConfigurationManager.AppSettings("BASTID")) '1032
                            If t <> 0 Then
                                Dim swid As Integer = 0
                                swid = objdb.ExeQueryScalar("select top 1 sw_id from sitedoc where siteid=( select site_id from wftransaction where sno=" & Request.QueryString("id") & ") and version=" & Request.QueryString("version") & "  and docid=" & ConfigurationManager.AppSettings("WCTRBASTID")) '1034
                                objdb.ExeNonQuery("update odwctrbast set bastsubdate=getdate() where swid= " & swid)
                                'toupdate to be approved record status as 4 inorder to avoid approval from dashboard. 
                                objdb.ExeNonQuery("update wftransaction set rstatus=4 where tsk_id=4  and site_id = (select site_id from wftransaction where sno=" & Request.QueryString("id") & ") and siteversion= " & Request.QueryString("version") & " and enddatetime is null  and docid=" & ConfigurationManager.AppSettings("BASTID")) '1032
                                objdb.ExeNonQuery("update sitedoc set rstatus=2 where siteid = (select site_id from wftransaction where sno=" & Request.QueryString("id") & ") and version= " & Request.QueryString("version") & " and docid=" & ConfigurationManager.AppSettings("WCTRBASTID")) '1034
                                ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "gopage(" & swid & ");", True)
                            End If
                        End If
                        'this is to update back the rstatus =2
                        If InStr(Request.QueryString("docname"), "WCTR BAST") > 0 Then
                            Dim y As Integer = 0
                            y = objdb.ExeQueryScalar("select count(*) from wftransaction where STARTDATETIME IS NOT NULL AND ENDDATETIME IS NULL and site_id=(select site_id from wftransaction where sno=" & Request.QueryString("id") & ") and siteversion=" & Request.QueryString("version") & " and docid=" & ConfigurationManager.AppSettings("WCTRBASTID")) '1034
                            If y = 0 Then objdb.ExeNonQuery("update wftransaction set rstatus=2 where tsk_id=4  and site_id = (select site_id from wftransaction where sno=" & Request.QueryString("id") & ") and siteversion= " & Request.QueryString("version") & " and enddatetime is null  and docid=" & ConfigurationManager.AppSettings("BASTID")) '1032
                        End If

                        ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "WindowsClose();", True)
                    End If
                Else
                    ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('Error on signning PDF:\n" + strResult + "');", True)


                End If

            Catch ex As Exception
                'Response.Write(ex.Message.ToString)
                ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('Error on signning PDF:\n\n'" + ex.Message + ");", True)
            End Try
        Else
            Response.Write("<script> alert('invalid  userid or password');</script>")
        End If
    End Sub
    Sub GetBautSign(ByVal newpassword As String)
        Try
            Dim strsql As String = "Exec uspGetBautXY " & Request.QueryString("id") & ",'" & HDPono.Value & "'"
            dt = objdb.ExeQueryDT(strsql, "digilist")
            If (dt.Rows.Count > 0) Then
                Dim Flags As Integer = 0
                Dim IntCount As Integer = 0
                If 1 = 1 Then Flags = Flags Or SAPIWrapper.SAPI_ENUM_DRAWING_ELEMENT_GRAPHICAL_IMAGE
                If 1 = 1 Then Flags = Flags Or SAPIWrapper.SAPI_ENUM_DRAWING_ELEMENT_SIGNED_BY
                If 1 = 0 Then Flags = Flags Or SAPIWrapper.SAPI_ENUM_DRAWING_ELEMENT_REASON
                If 1 = 1 Then Flags = Flags Or SAPIWrapper.SAPI_ENUM_DRAWING_ELEMENT_TIME
                Dim strResult As String = SAPIWrapper.SAPI_sign_file(ConfigurationManager.AppSettings("FPath") + HDPath.Value, Nothing, txtUserName.Text, _
                                          newpassword, dt.Rows(0)("pageno"), dt.Rows(0)("xposition"), dt.Rows(0)("yposition"), intHeight, intWidth, False, "baut sign" & Request.QueryString("id").ToString(), Flags, "baut sign" & Request.QueryString("id").ToString())
                ' objdb.ExeNonQuery("exec uspErrLog '', 'xyposs','" & strResult & "','bautxypo'")

            End If
        Catch ex As Exception
            'Response.Write(ex.Message.ToString)
            objdb.ExeNonQuery("exec uspErrLog '', 'xyposs','" & ex.Message.ToString.Replace("'", "''") & "','bautxypo'")
        End Try
    End Sub
    Sub binddoc()
        Dim dt As New DataTable
        dt = objBOsite.uspSiteBAUTDocList(Request.QueryString("siteno"), Request.QueryString("version"))
        grddocuments.DataSource = dt
        grddocuments.DataBind()
        grddocuments.Columns(1).Visible = False
        grddocuments.Columns(2).Visible = False
        grddocuments.Columns(4).Visible = False
    End Sub
    Protected Sub grddocuments_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grddocuments.RowDataBound
        Select Case e.Row.RowType
            Case DataControlRowType.DataRow
                Dim lbl As Label = CType(e.Row.FindControl("lblNo"), Label)
                lbl.Text = (grddocuments.PageIndex * grddocuments.PageSize) + (e.Row.RowIndex + 1) & ".&nbsp;"
        End Select
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim url As String = "../PO/frmViewDocument.aspx?id=" & e.Row.Cells(4).Text
            If e.Row.Cells(2).Text <> "" And e.Row.Cells(2).Text <> "&nbsp;" Then
                e.Row.Cells(3).Text = "<a href='#' onclick=""window.open('" & url & "','mywindow','status=yes,menubar=no,scrollbars=yes,resizable=yes,width=700px,height=600px')"">" & e.Row.Cells(3).Text & "</a>"
            Else
                e.Row.Cells(3).Text = e.Row.Cells(3).Text
            End If

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
    '                objdb.ExeNonQuery("exec uspErrLog '', 'mailsending','no mail id found','sendmailtransmulti'")
    '            End If

    '        End If
    '    Catch ex As Exception
    '        objdb.ExeNonQuery("exec uspErrLog '', 'mailsending','" & ex.Message.ToString.Replace("'", "''") & "','sendmailtransmulti2'")
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
            phone = "+" & objdb.ExeQueryScalarString("select phoneno from ebastusers_1 where usrlogin='" & Session("User_Login") & "'")
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

    'End Sub

End Class
