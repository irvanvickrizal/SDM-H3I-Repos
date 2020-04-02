Imports BusinessLogic
Imports Common
Imports System.Data
Imports AXmsCtrl
Partial Class WCC_frmWccDigital_Signature
    Inherits System.Web.UI.Page
    Dim objBo As New BODashBoard
    Dim objBosd As New BOSiteDocs
    Dim objBOsite As New BOSiteDocs
    Shared strPath As String = ""
    Dim intPage, intX, intY, intHeight, intWidth As New Integer
    Dim dt, dtn As New DataTable
    Dim objbositedoc As New BOSiteDocs
    Dim objBOM As New BOMailReport
    Dim objdb As New DBUtil
    Dim dtwf As New DataTable
    Dim objutil As New DBUtil
    Dim i, j, k, l As Integer
    Dim objsms As New mCore.SMS
    Dim objmail As New TakeMail
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Cache.SetNoStore()
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        objsms.License.Company = "TAKE UNITED SDN BHD"
        objsms.License.LicenseType = "PRO-DISTRIBUTION"
        objsms.License.Key = "TRQT-NF2L-Y7V9-HF4E"
        If Page.IsPostBack = False Then
            txtUserName.Text = Session("User_Login")
            intHeight = 50
            intWidth = 150
            GetPDFDocument()
            binddoc()
        End If
        intHeight = 50
        intWidth = 150
        'check for review task
        Dim tskid As Integer
        tskid = objutil.ExeQueryScalar("select tsk_id from WccWfTransaction  where sno= " & Convert.ToInt32(Request.QueryString("id")) & " ")
        If tskid = 5 Then 'means reviewer
            dgrow.Visible = False
            rerow.Visible = True
        Else
            dgrow.Visible = True
            rerow.Visible = False
        End If
        ' check to show reviewer
        Dim userdt As New DataTable
        Dim strsql As String = ""
        strsql = "select 'User : ' + name  + ' ( ' + case usrtype when 'C' then 'Telkomsel'when 'N' then 'NSN'else 'NSN'end + ' )' userid ,convert(varchar(20),enddatetime,113) enddate  from WccWfTransaction   W" & _
                 " inner join ebastusers_1 E on W.userid=e.usr_id " & _
                 " where tsk_id=5 and site_id=(select site_id from WccWfTransaction  where sno= " & Convert.ToInt32(Request.QueryString("id")) & ") " & _
                 " and siteversion=(select siteversion from WccWfTransaction  where sno= " & Convert.ToInt32(Request.QueryString("id")) & ")" & _
                 " and docid=(select docid from WccWfTransaction  where sno= " & Convert.ToInt32(Request.QueryString("id")) & ") and enddatetime is not null"
        userdt = objutil.ExeQueryDT(strsql, "ddd")
        If userdt.Rows.Count <> 0 Then
            reviewerid.Visible = True
            lblreviewer.Text = "This document was reviewed by  " & userdt.Rows(0).Item(0).ToString & " On " & userdt.Rows(0).Item(1).ToString & " "
        Else
            reviewerid.Visible = False
        End If

        lnkrequest.Attributes.Add("onclick", "waitPreloadPage();")
    End Sub
    Sub binddoc()
        'hardcoded to filter only for BAUT and BAST
        Dim dt As New DataTable
        'dt = objBOsite.uspWccSiteBAUTDocList(Request.QueryString("siteno"), Request.QueryString("version"), Request.QueryString("docname"))
        Dim strsql As String = "Exec uspWccSiteBAUTDocList " & Request.QueryString("swid")
        dt = objutil.ExeQueryDT(strsql, "digilist")
        grdDocuments.DataSource = dt
        grdDocuments.DataBind()
        grdDocuments.Columns(1).Visible = False
        grdDocuments.Columns(2).Visible = False
        grdDocuments.Columns(3).Visible = True
        grdDocuments.Columns(4).Visible = False
        grdDocuments.Columns(5).Visible = False
        grdDocuments.Columns(6).Visible = False
        grdDocuments.Columns(7).Visible = True
        'get the parent document
        strsql = "Exec uspWccSiteBAUTDocList " & Request.QueryString("swid") & ",1"
        dt = objutil.ExeQueryDT(strsql, "digilist")
        grdDocuments2.DataSource = dt
        grdDocuments2.DataBind()
        grdDocuments2.Columns(1).Visible = False
        grdDocuments2.Columns(2).Visible = False
        grdDocuments2.Columns(3).Visible = True
        grdDocuments2.Columns(4).Visible = False
        grdDocuments2.Columns(5).Visible = False
        grdDocuments2.Columns(6).Visible = False
        grdDocuments2.Columns(7).Visible = True
        grdDocuments2.Visible = False
        btnReject.Visible = True
        btnReject.Enabled = False
    End Sub
    Sub GetPDFDocument()
        Dim dt As DataTable
        'dt = objBo.uspWccDigitalSign())

        dt = objdb.ExeQueryDT("exec wccgetdocpath " & Convert.ToInt32(Request.QueryString("id")) & "", "555")


        If (dt.Rows.Count > 0) Then
            strPath = dt.Rows(0)("docpath").ToString()
            HDPath.Value = dt.Rows(0)("docpath").ToString()
            HDPono.Value = dt.Rows(0)("pono").ToString()
            HDDocid.Value = dt.Rows(0)("docid").ToString()
            hdnx.Value = dt.Rows(0)("xval")
            hdny.Value = dt.Rows(0)("yval")
            hdpageNo.Value = dt.Rows(0)("pageno")
            PDFViwer.Attributes.Add("src", ConfigurationManager.AppSettings("WCCVpath") + strPath)
            'Response.ContentType = "application/pdf"
            'Response.WriteFile(ConfigurationManager.AppSettings("Vpath") + strPath.Replace("/", "\"))
        End If
    End Sub
    Sub GetBautSign(ByVal newpassword As String)
        Try
            Dim strsql As String = "Exec uspWccGetBautXY " & Request.QueryString("id") & ",'" & HDPono.Value & "'"
            dt = objutil.ExeQueryDT(strsql, "digilist")
            If (dt.Rows.Count > 0) Then
                Dim Flags As Integer = 0
                Dim IntCount As Integer = 0
                If 1 = 1 Then Flags = Flags Or SAPIWrapper.SAPI_ENUM_DRAWING_ELEMENT_GRAPHICAL_IMAGE
                If 1 = 1 Then Flags = Flags Or SAPIWrapper.SAPI_ENUM_DRAWING_ELEMENT_SIGNED_BY
                If 1 = 0 Then Flags = Flags Or SAPIWrapper.SAPI_ENUM_DRAWING_ELEMENT_REASON
                If 1 = 1 Then Flags = Flags Or SAPIWrapper.SAPI_ENUM_DRAWING_ELEMENT_TIME
                Dim strResult As String = SAPIWrapper.SAPI_sign_file(ConfigurationManager.AppSettings("WccPath") + HDPath.Value, Nothing, txtUserName.Text, _
                                          newpassword, dt.Rows(0)("pageno"), dt.Rows(0)("xposition"), dt.Rows(0)("yposition"), intHeight, intWidth, False, "baut sign" & Request.QueryString("id").ToString(), Flags, "baut sign" & Request.QueryString("id").ToString())
                'objutil.ExeNonQuery("exec uspErrLog '', 'xyposs','" & strResult & "','bautxypo'")
            End If
        Catch ex As Exception
            'Response.Write(ex.Message.ToString)
            objutil.ExeNonQuery("exec uspErrLog '', 'xyposs','" & ex.Message.ToString.Replace("'", "''") & "','bautxypo'")
        End Try
    End Sub
    Protected Sub BtnSign_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSign.Click
        Dim npwd1 As String = ""
        npwd1 = "12345678"
        Dim j As Integer
        Dim objdb As New DBUtil
        j = objdb.ExeQueryScalar("select count(*) from dgpassword where username='" & txtUserName.Text & "' and password='" & txtPassword.Text & "'")
        If j > 0 Then
            Try
                Dim Flags As Integer = 0
                Dim IntCount As Integer = 0
                If 1 = 1 Then Flags = Flags Or SAPIWrapper.SAPI_ENUM_DRAWING_ELEMENT_GRAPHICAL_IMAGE
                If 1 = 1 Then Flags = Flags Or SAPIWrapper.SAPI_ENUM_DRAWING_ELEMENT_SIGNED_BY
                If 1 = 0 Then Flags = Flags Or SAPIWrapper.SAPI_ENUM_DRAWING_ELEMENT_REASON
                If 1 = 1 Then Flags = Flags Or SAPIWrapper.SAPI_ENUM_DRAWING_ELEMENT_TIME
                Dim strResult As String = SAPIWrapper.SAPI_sign_file(ConfigurationManager.AppSettings("WccPath") + HDPath.Value, Nothing, txtUserName.Text, _
                                          npwd1, hdpageNo.Value, hdnx.Value, hdny.Value, intHeight, intWidth, False, "test", Flags, "")
                If (strResult = Constants._DigitalSign_Result) Then
                    If (HDDocid.Value = CommonSite.BAUTID) Then
                        GetBautSign(npwd1)
                    End If
                    Dim i As Integer = -1
                    'i = objBo.uspDocApproved(Convert.ToInt32(Request.QueryString("id")), CommonSite.UserId(), CommonSite.UserName(), CommonSite.RollId())
                    i = objdb.ExeNonQuery("Exec uspWCCDocApproved " & Convert.ToInt32(Request.QueryString("id")) & " , " & CommonSite.UserId() & " , '" & CommonSite.UserName() & "', " & CommonSite.RollId() & " ")
                    If i = 0 Then
                        ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('Error while doing the transaction.');", True)
                    ElseIf i = 1 Then
                        'added by satish on 11thApril
                        '*************************** for mail functionality.***********************
                        'to find next user
                        Dim nextid As Integer
                        Dim strs As String = "SELECT ISNULL(USERID,1)USERID FROM WccWfTransaction  WHERE" & _
                        " SITE_ID=(SELECT SITE_ID FROM WccWfTransaction  WHERE SNO = " & Convert.ToInt32(Request.QueryString("id")) & " ) " & _
                        " AND SITEVERSION = (SELECT SITEVERSION FROM WccWfTransaction  WHERE SNO = " & Convert.ToInt32(Request.QueryString("id")) & ")" & _
                        " AND STARTDATETIME IS NOT NULL AND ENDDATETIME IS NULL"
                        nextid = objdb.ExeQueryScalar(strs)
                        objdb.ExeNonQuery("exec uspErrLog '', '" & Request.QueryString("id") & "','" & nextid & "' ,'sendmailtransnormal1'")
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
                        'update WccSiteDoc
                        'do insert trans 
                        '*********************
                        ' this if is only for wctrbast
                        If InStr(Request.QueryString("docname"), "(BAST)") > 0 Then
                            Dim t As Integer = 0
                            t = objdb.ExeQueryScalar("select count(*) from WccWfTransaction  where STARTDATETIME IS NOT NULL AND ENDDATETIME IS NULL and site_id=(select site_id from WccWfTransaction  where sno=" & Request.QueryString("id") & ") and siteversion=" & Request.QueryString("version") & "  and docid=" & ConfigurationManager.AppSettings("BASTID")) '1032
                            If t <> 0 Then
                                Dim swid As Integer = 0
                                swid = objdb.ExeQueryScalar("select top 1 sw_id from WccSiteDoc where siteid=( select site_id from WccWfTransaction  where sno=" & Request.QueryString("id") & ") and version=" & Request.QueryString("version") & "  and docid=" & ConfigurationManager.AppSettings("WCTRBASTID")) '1034
                                objdb.ExeNonQuery("update odwctrbast set bastsubdate=getdate() where swid= " & swid)
                                'toupdate to be approved record status as 4 inorder to avoid approval from dashboard. 
                                objdb.ExeNonQuery("update WccWfTransaction  set rstatus=4 where tsk_id=4  and site_id = (select site_id from WccWfTransaction  where sno=" & Request.QueryString("id") & ") and siteversion= " & Request.QueryString("version") & " and enddatetime is null  and docid=" & ConfigurationManager.AppSettings("BASTID")) '1032
                                objdb.ExeNonQuery("update WccSiteDoc set rstatus=2 where siteid = (select site_id from WccWfTransaction  where sno=" & Request.QueryString("id") & ") and version= " & Request.QueryString("version") & " and docid=" & ConfigurationManager.AppSettings("WCTRBASTID")) '1034
                                ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript123", "gopage(" & swid & ");", True)
                            End If
                        End If
                        'this is to update back the rstatus =2
                        If InStr(Request.QueryString("docname"), "WCTR BAST") > 0 Then
                            Dim y As Integer = 0
                            y = objdb.ExeQueryScalar("select count(*) from WccWfTransaction  where STARTDATETIME IS NOT NULL AND ENDDATETIME IS NULL and site_id=(select site_id from WccWfTransaction  where sno=" & Request.QueryString("id") & ") and siteversion=" & Request.QueryString("version") & " and docid=" & ConfigurationManager.AppSettings("WCTRBASTID")) '1034
                            If y = 0 Then objdb.ExeNonQuery("update WccWfTransaction  set rstatus=2 where tsk_id=4  and site_id = (select site_id from WccWfTransaction  where sno=" & Request.QueryString("id") & ") and siteversion= " & Request.QueryString("version") & " and enddatetime is null  and docid=" & ConfigurationManager.AppSettings("BASTID")) '1032
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
            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('invalid  userid or password');", True)
        End If
        dgdiv.Style("display") = "none"
    End Sub
    Protected Sub btnreject_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReject.Click
        Dim rdbr As New RadioButtonList
        Dim str As String = ""
        Dim y As Integer = 0
        Dim count As Integer = 0
        Dim sw_id As Integer
        Dim remarks As String = ""
        Dim usrid As Integer = Session("User_Id")
        Dim roleid As Integer = Session("Role_Id")
        Dim usrname As String = Session("User_Name")
        Dim pono As String = ""
        Dim siteid As String = ""
        Dim docid As Integer
        For k = 0 To grdDocuments.Rows.Count - 1
            rdbr = grdDocuments.Rows(k).Cells(5).FindControl("rdbstatus")
            If rdbr.SelectedValue = 1 Then
                str = IIf(str <> "", str & "," & grdDocuments.Rows(k).Cells(4).Text.ToString, grdDocuments.Rows(k).Cells(4).Text.ToString)
                sw_id = grdDocuments.Rows(k).Cells(4).Text.ToString
                Dim txt1 As TextBox = grdDocuments.Rows(k).Cells(6).FindControl("txtremarks")
                remarks = txt1.Text
                docid = grdDocuments.Rows(k).Cells(1).Text.ToString
                siteid = grdDocuments.Rows(k).Cells(5).Text.ToString
                pono = grdDocuments.Rows(k).Cells(6).Text.ToString
                objutil.ExeNonQuery("Exec uspWccBAUTDocReject " & sw_id & ",'" & remarks & "'," & usrid & "," & roleid & ",'" & usrname & "','" & pono & "','" & siteid & "'," & docid & "")
                count = count + 1
            End If
        Next
        'reject the parent document as well
        If count > 0 Then
            For k = 0 To grdDocuments2.Rows.Count - 1
                rdbr = grdDocuments2.Rows(k).Cells(5).FindControl("rdbstatus")
                str = IIf(str <> "", str & "," & grdDocuments2.Rows(k).Cells(4).Text.ToString, grdDocuments2.Rows(k).Cells(4).Text.ToString)
                sw_id = grdDocuments2.Rows(k).Cells(4).Text.ToString
                Dim txt1 As TextBox = grdDocuments2.Rows(k).Cells(6).FindControl("txtremarks")
                remarks = txt1.Text
                docid = grdDocuments2.Rows(k).Cells(1).Text.ToString
                siteid = grdDocuments2.Rows(k).Cells(5).Text.ToString
                pono = grdDocuments2.Rows(k).Cells(6).Text.ToString
                objutil.ExeNonQuery("Exec uspWccBAUTDocReject " & sw_id & ",'" & remarks & "'," & usrid & "," & roleid & ",'" & usrname & "','" & pono & "','" & siteid & "'," & docid & "")
                count = count + 1
            Next
        End If
        Dim scripts As String = ""
        If count = 0 Then
            scripts = "alert('No document Selected for Reject');"
            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "rejected", scripts, True)
        Else
            scripts = "alert('Document rejected successfully');WindowsClose2();"
            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "rejected", scripts, True)
        End If
    End Sub
    Protected Sub grdDocuments_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdDocuments.RowDataBound
        Select Case e.Row.RowType
            Case DataControlRowType.DataRow
                Dim lbl As Label = CType(e.Row.FindControl("lblNo"), Label)
                lbl.Text = (grdDocuments.PageIndex * grdDocuments.PageSize) + (e.Row.RowIndex + 1) & ".&nbsp;"
        End Select
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim url As String
            url = "../PO/frmViewDocument.aspx?id=" & e.Row.Cells(4).Text
            If e.Row.Cells(2).Text <> "" And e.Row.Cells(2).Text <> "&nbsp;" Then
                e.Row.Cells(3).Text = "<a href='#' onclick=""window.open('" & url & "','mywindow','status=yes,menubar=no,scrollbars=yes,resizable=yes,width=700px,height=600px')"">" & e.Row.Cells(3).Text & "</a>"
            Else
                If InStr(e.Row.Cells(3).Text, "WCTR BAST") > 0 Then
                    url = "../baut/frmti_wctrbast.aspx?id=" & e.Row.Cells(4).Text & "&Open=0"
                    e.Row.Cells(3).Text = "<a href='#' onclick=""window.open('" & url & "','mywindow','status=yes,menubar=no,scrollbars=yes,resizable=yes,width=700px,height=600px')"">" & e.Row.Cells(3).Text & "</a>"
                Else
                    e.Row.Cells(3).Text = e.Row.Cells(3).Text
                End If
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
    '                myEmail.Body = myEmail.Body & sb
    '                myEmail.Body = myEmail.Body & "<br/>" & dt.Rows(0).Item(5).ToString   ''closing
    '                myEmail.IsBodyHtml = True
    '                myEmail.From = New System.Net.Mail.MailAddress(senderAdd, "NSN")
    '                mySMTPClient.Host = ConfigurationManager.AppSettings("smtp")
    '                Try
    '                    mySMTPClient.Send(myEmail)
    '                Catch ex As Exception
    '                    objdb.ExeNonQuery("exec uspErrLog '', 'mailsending','" & ex.Message.ToString.Replace("'", "''") & "','sendmailtransnormal'")
    '                End Try
    '            Else
    '                objdb.ExeNonQuery("exec uspErrLog '', 'mailsending','no mail id found','sendmailtransnormal'")
    '            End If
    '        End If
    '    Catch ex As Exception
    '        objdb.ExeNonQuery("exec uspErrLog '', 'mailsending','" & ex.Message.ToString.Replace("'", "''") & "','sendmailtransnormal2'")
    '    End Try
    'End Sub
    Protected Sub lnkrequest_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkrequest.Click
        Dim npwd As String = ""
        Dim msgdata As New StringBuilder
        If txtUserName.Text <> "" Then
            objsms.Port = "COM3"
            objsms.BaudRate = 115200
            objsms.DataBits = 8
            Dim phone As String = ""
            phone = "+" & objdb.ExeQueryScalarString("select phoneno from ebastusers_1 where usrlogin='" & Session("User_Login") & "' and rstatus=2")
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
            loadingdiv.Style("display") = "none"
            'ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('Please check for your password in your phone');", True)
            Response.write("<script>alert('Please check for your password in your phone');</script>")
        Else
            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('Please keyin the user name and click request password');", True)
        End If
    End Sub
    Sub DOThis(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rad As RadioButtonList = CType(sender, RadioButtonList)
        Dim grdr As GridViewRow = CType(rad.NamingContainer, GridViewRow)
        Dim x As Integer = grdr.RowIndex
        Dim rdl As RadioButtonList = grdDocuments.Rows(x).Cells(5).FindControl("rdbstatus")
        Dim txt1 As TextBox = grdDocuments.Rows(x).Cells(6).FindControl("txtremarks")
        txt1.Text = "Please enter remarks here......"
        If rdl.SelectedValue = 0 Then 'when approve
            txt1.Visible = False
            txtUserName.Enabled = True
            txtPassword.Enabled = True
            lnkrequest.Enabled = True
            BtnSign.Enabled = True
            btnReject.Enabled = False
        Else 'when reject
            txt1.Visible = True
            txtUserName.Enabled = False
            txtPassword.Enabled = False
            lnkrequest.Enabled = False
            BtnSign.Enabled = False
            btnReject.Enabled = True
        End If
    End Sub
    Protected Sub grdDocuments2_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        Select Case e.Row.RowType
            Case DataControlRowType.DataRow
                Dim lbl As Label = CType(e.Row.FindControl("lblnotwo"), Label)
                lbl.Text = (grdDocuments2.PageIndex * grdDocuments2.PageSize) + (e.Row.RowIndex + 1) & ".&nbsp;"
        End Select
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim url As String
            url = "../PO/frmViewDocument.aspx?id=" & e.Row.Cells(4).Text
            If e.Row.Cells(2).Text <> "" And e.Row.Cells(2).Text <> "&nbsp;" Then
                e.Row.Cells(3).Text = "<a href='#' onclick=""window.open('" & url & "','mywindowopen','status=yes,menubar=no,scrollbars=yes,resizable=yes,width=700px,height=600px')"">" & e.Row.Cells(3).Text & "</a>"
            Else
                If InStr(e.Row.Cells(3).Text, "WCTR BAST") > 0 Then
                    url = "../baut/frmti_wctrbast.aspx?id=" & e.Row.Cells(4).Text & "&Open=0"
                    e.Row.Cells(3).Text = "<a href='#' onclick=""window.open('" & url & "','mywindowopen','status=yes,menubar=no,scrollbars=yes,resizable=yes,width=700px,height=600px')"">" & e.Row.Cells(3).Text & "</a>"
                Else
                    e.Row.Cells(3).Text = e.Row.Cells(3).Text
                End If
            End If
        End If
    End Sub
    Protected Sub btnreview_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim i As Integer
        'i = objBo.uspDocApproved(Convert.ToInt32(Request.QueryString("id")), CommonSite.UserId(), CommonSite.UserName(), CommonSite.RollId())
        i = objutil.ExeQueryScalar("Exec [uspDocApproved] " & Convert.ToInt32(Request.QueryString("id")) & "," & CommonSite.UserId() & ",'" & CommonSite.UserName() & "'," & CommonSite.RollId() & "," & ConfigurationManager.AppSettings("BAUTID") & "," & ConfigurationManager.AppSettings("BASTID") & "")
        If i = 0 Then
            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('Error while doing the transaction.');", True)
        ElseIf i = 1 Then
            'to find next user
            Dim nextid As Integer
            Dim strs As String = "SELECT ISNULL(USERID,1)USERID FROM WccWfTransaction  WHERE" & _
            " SITE_ID=(SELECT SITE_ID FROM WccWfTransaction  WHERE SNO = " & Convert.ToInt32(Request.QueryString("id")) & " ) " & _
            " AND SITEVERSION = (SELECT SITEVERSION FROM WccWfTransaction  WHERE SNO = " & Convert.ToInt32(Request.QueryString("id")) & ")" & _
            " AND STARTDATETIME IS NOT NULL AND ENDDATETIME IS NULL"
            nextid = objdb.ExeQueryScalar(strs)
            objdb.ExeNonQuery("exec uspErrLog '', '" & Request.QueryString("id") & "','" & nextid & "' ,'sendmailtransnormal1'")
            If nextid = 0 Then nextid = 1
            Try
                objmail.sendmailTrans(nextid, 0, "0", 0, ConfigurationManager.AppSettings("uploadmailconst"))
            Catch ex As Exception
                objdb.ExeNonQuery("exec uspErrLog '', 'mailsending','" & ex.Message.ToString.Replace("'", "''") & "','sendmailtransupload'")
            End Try
            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "WindowsClosenew();", True)
        End If
    End Sub
End Class
