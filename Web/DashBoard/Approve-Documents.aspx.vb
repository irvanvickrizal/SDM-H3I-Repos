Imports BusinessLogic
Imports Common
Imports Entities
Imports System.Data
Imports DGSignPWD
Imports AXmsCtrl
Partial Class DashBoard_Approve_Documents
    Inherits System.Web.UI.Page
    Dim objDL As New BODDLs
    Dim dt As New DataTable
    Dim objBo As New BODashBoard
    Dim intPage, intX, intY, intHeight, intWidth As New Integer
    Dim objdb As New DBUtil
    Dim dtn As New DataTable
    Dim objBOM As New BOMailReport
    Dim objdg As New UserInfo
    Dim objUtil As New DBUtil
    Dim strSql As String
    Dim objsms As New mcore.sms
    Dim objmail As New takemail
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        objsms.License.Company = "TAKE UNITED SDN BHD"
        objsms.License.LicenseType = "PRO-DISTRIBUTION"
        objsms.License.Key = "TRQT-NF2L-Y7V9-HF4E"
        If Not Page.IsPostBack Then
            BindDataFirst()
            txtUserName.Text = Session("User_Login")
            'btnDigitalSign.Attributes.Add("onclick", "return CheckMultiValueDelete();")
        End If
        ' BindValue.Style.Add("display", "none")
    End Sub
    Sub BindDataFirst()
        'dt = objBo.uspSiteDocListForUser(CommonSite.UserId())
        dt = objBo.uspPendingDocumentTotal(CommonSite.UserId())
        DLFirst.DataSource = dt
        DLFirst.DataBind()
        BindValue.Style.Add("display", "none")
    End Sub
    Sub binddata(ByVal SiteId As Integer, ByVal Version As Integer)
        BindValue.Style.Add("display", " ")
        'dt = objBo.uspSiteDocListForUser(CommonSite.UserId())
        dt = objBo.uspSiteDocListForUser(CommonSite.UserId(), SiteId, Version)
        grdDocuments.DataSource = dt
        grdDocuments.DataBind()
    End Sub
    Protected Sub DLFirst_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles DLFirst.RowDataBound
        Select Case e.Row.RowType
            Case DataControlRowType.DataRow
                Dim lbl As Label = CType(e.Row.FindControl("lblNo"), Label)
                lbl.Text = (DLFirst.PageIndex * DLFirst.PageSize) + (e.Row.RowIndex + 1) & ".&nbsp;"
        End Select
    End Sub
    Protected Sub BtnSign_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSign.Click
        Dim npwd1 As String = ""
        npwd1 = "12345678"
        Dim j As Integer
        Dim objdb As New DBUtil
        j = objdb.ExeQueryScalar("select count(*) from dgpassword where username='" & txtUserName.Text & "' and password='" & txtPassword.Text & "'")
        If j > 0 Then
            BindValue.Style.Add("display", " ")
            Dim strResult As String, strError As String = ""
            Dim IntCount As Integer = 0
            intHeight = 50
            intWidth = 150
            For iLoop As Integer = 0 To grdDocuments.Rows.Count - 1
                Dim CheckBoxSno As HtmlInputCheckBox = CType(grdDocuments.Rows(iLoop).FindControl("EmpId"), HtmlInputCheckBox)
                Dim iHdTaskId As HiddenField = CType(grdDocuments.Rows(iLoop).FindControl("HdTaskid"), HiddenField)
                Dim iHdDocPath As HiddenField = CType(grdDocuments.Rows(iLoop).FindControl("HDDocPath"), HiddenField)
                Dim iHdDocName As HiddenField = CType(grdDocuments.Rows(iLoop).FindControl("HdDocName"), HiddenField)
                Dim iHdXval As HiddenField = CType(grdDocuments.Rows(iLoop).FindControl("HDXVal"), HiddenField)
                Dim iHdYval As HiddenField = CType(grdDocuments.Rows(iLoop).FindControl("HDYVal"), HiddenField)
                Dim iHdPageNo As HiddenField = CType(grdDocuments.Rows(iLoop).FindControl("HDPage"), HiddenField)
                Dim iHdSno As HiddenField = CType(grdDocuments.Rows(iLoop).FindControl("HDSno"), HiddenField)
                Dim iHdPono As HiddenField = CType(grdDocuments.Rows(iLoop).FindControl("hdpono"), HiddenField)
                Dim iHdsiteid As HiddenField = CType(grdDocuments.Rows(iLoop).FindControl("HdSiteid"), HiddenField)
                Dim iHDVersion As HiddenField = CType(grdDocuments.Rows(iLoop).FindControl("HDVersion"), HiddenField)
                Dim iHDSiteno As HiddenField = CType(grdDocuments.Rows(iLoop).FindControl("HDSiteno"), HiddenField)
                Dim iHddocid As HiddenField = CType(grdDocuments.Rows(iLoop).FindControl("hddocid"), HiddenField)
                If CheckBoxSno.Checked Then
                    Dim Flags As Integer = 0
                    If 1 = 1 Then Flags = Flags Or SAPIWrapper.SAPI_ENUM_DRAWING_ELEMENT_GRAPHICAL_IMAGE
                    If 1 = 1 Then Flags = Flags Or SAPIWrapper.SAPI_ENUM_DRAWING_ELEMENT_SIGNED_BY
                    If 1 = 0 Then Flags = Flags Or SAPIWrapper.SAPI_ENUM_DRAWING_ELEMENT_REASON
                    If 1 = 1 Then Flags = Flags Or SAPIWrapper.SAPI_ENUM_DRAWING_ELEMENT_TIME
                    Dim i As Integer = -1
                    'strResult = SAPIWrapper.SAPI_sign_file(ConfigurationManager.AppSettings("FPath") + iHdDocPath.Value, Nothing, txtUserName.Text, _
                    'npwd1, iHdPageNo.Value, iHdXval.Value, iHdYval.Value, intHeight, intWidth, False, "test", Flags, "")
                    strResult = "success"
                    If (strResult = Constants._DigitalSign_Result) Then
                        If (iHddocid.Value = CommonSite.BAUTID) Then
                            GetBautSign(npwd1, iHdPono.Value, iHdDocPath.Value, iHdSno.Value)
                        End If
                        'bugfix100922 to add dummy bast2id only for cme (ti, sis and sitac are the same)
                        'i = objBo.uspDocApproved(Convert.ToInt32(CheckBoxSno.Value), CommonSite.UserId(), CommonSite.UserName(), CommonSite.RollId())
                        i = objUtil.ExeQueryScalar("Exec [uspDocApproved] " & Convert.ToInt32(Request.QueryString("id")) & "," & CommonSite.UserId() & ",'" & CommonSite.UserName() & "'," & CommonSite.RollId() & "," & ConfigurationManager.AppSettings("BAUTID") & "," & ConfigurationManager.AppSettings("BASTID") & "," & ConfigurationManager.AppSettings("BAST2ID"))
                        If i = 0 Then
                            strError += iHdDocName.Value + " - has been error while doing the transaction.  \n\n "
                        Else
                            'added by satish on 11thApril
                            '*************************** for mail functionality.***********************
                            'to find next user
                            Dim nextid As Integer = 0
                            Dim strs As String = "SELECT ISNULL(USERID,1) FROM WFTRANSACTION WHERE" & _
                            " SITE_ID=(SELECT SITE_ID FROM WFTRANSACTION WHERE SNO = " & Convert.ToInt32(iHdSno.Value) & " ) " & _
                            " AND SITEVERSION = (SELECT SITEVERSION FROM WFTRANSACTION WHERE SNO = " & iHdSno.Value & ")" & _
                            " AND STARTDATETIME IS NOT NULL AND ENDDATETIME IS NULL"
                            nextid = objdb.ExeQueryScalar(strs)
                            If nextid = 0 Then nextid = 1
                            Try
                                objmail.sendmailTrans(nextid, 0, "0", 0, ConfigurationManager.AppSettings("uploadmailconst"))
                            Catch ex As Exception
                                objdb.ExeNonQuery("exec uspErrLog '', 'mailsending','" & ex.Message.ToString.Replace("'", "''") & "','sendmailtransupload'")
                            End Try
                            Dim bautok As Integer, bastok As Integer
                            '*****************************
                            'Added by satish for mails to initiators on 16th march 2010
                            'start 16march2010
                            'for BAUT
                            ' bautok = objbositedoc.check4BAUTBAST(siteid, siteversion, 1, Request.QueryString("pono"), Session("User_Name"))
                            bautok = objdb.ExeQueryScalar("exec uspcheck4BAUTBAST" & iHdsiteid.Value & " ," & iHDVersion.Value & "," & 1 & ",'" & iHdPono.Value & "','" & Session("User_Name") & "'," & ConfigurationManager.AppSettings("BAUTID") & "," & ConfigurationManager.AppSettings("BASTID") & " ")
                            'for BAST1
                            'bastok = objbositedoc.check4BAUTBAST(siteid, siteversion, 2, Request.QueryString("pono"), Session("User_Name"))
                            bastok = objdb.ExeQueryScalar("exec uspcheck4BAUTBAST " & iHdsiteid.Value & " ," & iHDVersion.Value & "," & 2 & ",'" & iHdPono.Value & "','" & Session("User_Name") & "'," & ConfigurationManager.AppSettings("BAUTID") & "," & ConfigurationManager.AppSettings("BASTID") & " ")

                            If bautok = 888 Then
                                'send mail to BAUT intiator
                                nextid = objdb.ExeQueryScalar("exec uspgetBAUTBASTInitiator " & iHdsiteid.Value & "," & ConfigurationManager.AppSettings("BAUTID") & "," & 0 & " ")
                                Try
                                    'objmail.sendmailTrans(nextidi, 0, "0", 0, ConfigurationManager.AppSettings("BAUTmailconst"))
                                    objmail.sendmailIniBAUTBAST(nextid, iHDSiteno.Value, ConfigurationManager.AppSettings("BAUTmailconst"), ConfigurationManager.AppSettings("BAUTID"), iHdPono.Value)
                                Catch ex As Exception
                                    objdb.ExeNonQuery("exec uspErrLog '', 'mailsending','" & ex.Message.ToString.Replace("'", "''") & "','sendmailtransBAUTInitiator'")
                                End Try
                            End If
                            If bastok = 999 Then
                                'send mail to BAST initiator
                                nextid = objdb.ExeQueryScalar("exec uspgetBAUTBASTInitiator " & iHdsiteid.Value & "," & 0 & "," & ConfigurationManager.AppSettings("BASTID") & " ")
                                Try
                                    'objmail.sendmailTrans(nextidj, 0, "0", 0, ConfigurationManager.AppSettings("BASTmailconst"))
                                    objmail.sendmailIniBAUTBAST(nextid, iHDSiteno.Value, ConfigurationManager.AppSettings("BASTmailconst"), ConfigurationManager.AppSettings("BASTID"), iHdPono.Value)
                                Catch ex As Exception
                                    objdb.ExeNonQuery("exec uspErrLog '', 'mailsending','" & ex.Message.ToString.Replace("'", "''") & "','sendmailtransBASTInitiator'")
                                End Try
                            End If
                            strError += iHdDocName.Value + " - has been Signed Sucessfully. \n\n "
                        End If
                    Else
                        strError += iHdDocName.Value + " - has been error on signning PDF: \n " + strResult + " \n\n "
                    End If
                End If
            Next
            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('" + strError + "');", True)
            txtPassword.Text = ""
            BindDataFirst()
        Else
            Response.Write("<script> alert('invalid  userid or password');</script>")
        End If
    End Sub
    Sub GetBautSign(ByVal newpassword As String, ByVal pono As String, ByVal DocPath As String, ByVal sno As String)
        Try
            Dim strsql As String = "Exec uspGetBautXY " & sno & ",'" & pono & "'"
            dt = objUtil.ExeQueryDT(strsql, "digilist")
            If (dt.Rows.Count > 0) Then
                Dim Flags As Integer = 0
                Dim IntCount As Integer = 0
                If 1 = 1 Then Flags = Flags Or SAPIWrapper.SAPI_ENUM_DRAWING_ELEMENT_GRAPHICAL_IMAGE
                If 1 = 1 Then Flags = Flags Or SAPIWrapper.SAPI_ENUM_DRAWING_ELEMENT_SIGNED_BY
                If 1 = 0 Then Flags = Flags Or SAPIWrapper.SAPI_ENUM_DRAWING_ELEMENT_REASON
                If 1 = 1 Then Flags = Flags Or SAPIWrapper.SAPI_ENUM_DRAWING_ELEMENT_TIME
                Dim strResult As String = SAPIWrapper.SAPI_sign_file(ConfigurationManager.AppSettings("FPath") + DocPath, Nothing, txtUserName.Text, _
                                         newpassword, dt.Rows(0)("pageno"), dt.Rows(0)("xposition"), dt.Rows(0)("yposition"), intHeight, intWidth, False, "baut test", Flags, "Baut Sign")
                'objUtil.ExeNonQuery("exec uspErrLog '', 'xyposs','" & strResult & "','bautxypo'")
            End If
        Catch ex As Exception
            'Response.Write(ex.Message.ToString)
            objUtil.ExeNonQuery("exec uspErrLog '', 'xyposs','" & ex.Message.ToString.Replace("'", "''") & "','bautxypo'")
            'ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('Error on signning PDF:\n\n'" + ex.Message + ");", True)
        End Try
    End Sub
    Protected Sub DLFirst_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles DLFirst.RowDeleting
        Dim hdVesion As HiddenField = CType(DLFirst.Rows(e.RowIndex).FindControl("HDVersion"), HiddenField)
        Dim HdSiteid As HiddenField = CType(DLFirst.Rows(e.RowIndex).FindControl("HdSiteid"), HiddenField)
        BindValue.Style.Add("display", " ")
        HDVersionSelect.Value = hdVesion.Value
        HDSiteSelect.Value = HdSiteid.Value
        binddata(Convert.ToInt32(HdSiteid.Value), Convert.ToInt32(hdVesion.Value))
    End Sub
    Protected Sub grdDocuments_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdDocuments.RowDataBound
        Select Case e.Row.RowType
            Case DataControlRowType.DataRow
                Dim lbl As Label = CType(e.Row.FindControl("lblnoSec"), Label)
                lbl.Text = (grdDocuments.PageIndex * grdDocuments.PageSize) + (e.Row.RowIndex + 1) & ".&nbsp;"
        End Select
    End Sub
    Protected Sub DLFirst_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles DLFirst.PageIndexChanging
        DLFirst.PageIndex = e.NewPageIndex
        DLFirst.DataBind()
    End Sub
    Protected Sub grdDocuments_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdDocuments.PageIndexChanging
        grdDocuments.PageIndex = e.NewPageIndex
        binddata(Convert.ToInt32(HDSiteSelect.Value), Convert.ToInt32(HDVersionSelect.Value))
    End Sub
    'Sub sendmailTransNew(ByVal userid As Integer)
    '    Try
    '        dtn = objdb.ExeQueryDT("exec uspgetemailNEW " & userid, "maildt")
    '        If dtn.Rows.Count > 0 And dtn.Rows(0).Item(3) <> "X" Then
    '            dt = objBOM.uspMailReportLD(9, )  ''this is for document upload time sending mail
    '            Dim k As Integer
    '            Dim Remail As String = "'"
    '            Dim name As String = ""
    '            Dim doc As New StringBuilder
    '            Remail = dtn.Rows(0).Item(3).ToString
    '            name = dtn.Rows(0).Item(2).ToString
    '            Dim senderAdd As String = ConfigurationManager.AppSettings("Smailid")
    '            Dim receiverAdd As String = Remail
    '            Dim mySMTPClient As New System.Net.Mail.SmtpClient
    '            Dim myEmail As New System.Net.Mail.MailMessage
    '            myEmail.BodyEncoding() = System.Text.Encoding.UTF8
    '            myEmail.SubjectEncoding = System.Text.Encoding.UTF8
    '            myEmail.To.Add(receiverAdd)
    '            myEmail.Subject = dt.Rows(0).Item(3).ToString  ''subject from table
    '            myEmail.Body = dt.Rows(0).Item(2).ToString & name & ",<br> <br>" ''salutatation from table
    '            myEmail.Body = myEmail.Body & dt.Rows(0).Item(4).ToString & " <br> <br> "
    '            myEmail.Body = myEmail.Body & " Process - " & ConfigurationManager.AppSettings("Type").Replace("\", "") & " <br> <br> "
    '            Dim sb As String = ""
    '            sb = "<table  border=1>"
    '            sb = sb & "<tr><td>Contract #</td><td>Site</td><td>Scope</td><td>Document</td><td>Date Requested</td></tr>"
    '            For k = 0 To dtn.Rows.Count - 1
    '                sb = sb & "<tr><td> " & dtn.Rows(k).Item(6).ToString & "  </td><td>" & dtn.Rows(k).Item(4).ToString & "</td><td>" & dtn.Rows(k).Item(5).ToString & " </td><td>" & dtn.Rows(k).Item(0).ToString & "</td><td>" & dtn.Rows(k).Item(7).ToString & "</td></tr>"
    '            Next
    '            sb = sb & "</table>"
    '            myEmail.Body = myEmail.Body & sb & "<br/>"
    '            Dim ab As String
    '            ab = "<a href='http://www.telkomsel.nsnebast.com'>Click here</a>" & " to go to e-BAST"
    '            myEmail.Body = myEmail.Body & ab & "<br/>" & dt.Rows(0).Item(5).ToString   ''closing
    '            myEmail.IsBodyHtml = True
    '            myEmail.From = New System.Net.Mail.MailAddress(senderAdd, "NSN")
    '            mySMTPClient.Host = ConfigurationManager.AppSettings("smtp")
    '            Try
    '                mySMTPClient.Send(myEmail)
    '            Catch ex As Exception
    '                objdb.ExeNonQuery("exec uspErrLog '', 'mailsending','" & ex.Message.ToString.Replace("'", "''") & "','sendmailtrans'")
    '            End Try
    '        End If
    '    Catch ex As Exception
    '        objdb.ExeNonQuery("exec uspErrLog '', 'mailsending','" & ex.Message.ToString.Replace("'", "''") & "','sendmailtrans'")
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
    Protected Sub BtnApprovel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnApprovel.Click
        strSql = "Exec uspSiteDocListForUser " & CommonSite.UserId() & ",0,0,1"
        BindValue.Style.Add("display", "")
        dt = objUtil.ExeQueryDT(strSql, "SiteDoc")
        grdDocuments.DataSource = dt
        grdDocuments.DataBind()
    End Sub
End Class
