Imports BusinessLogic
Imports Common
Imports System.Data
Imports AXmsCtrl
Imports System.IO
Partial Class Digital_signature
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
    Dim bautok As Integer
    Dim bastok As Integer
    Dim nextidi, nextidj As Integer
    Dim finalbast As Integer
    Dim objsmsnew As New SMSNew
    Dim userdt1 As New DataTable
    Dim userdt2 As New DataTable
    Dim userdt As New DataTable
    Dim strsql As String = ""
    Dim divRev As String = ""
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        objsms.License.Company = "TAKE UNITED SDN BHD"
        objsms.License.LicenseType = "PRO-DISTRIBUTION"
        objsms.License.Key = "TRQT-NF2L-Y7V9-HF4E"
        If Page.IsPostBack = False Then
            'bugfix100902 take the user login name from db, because taken from session will make the case censitive not the same
            txtUserName.Text = objutil.ExeQueryScalarString("select usrlogin from ebastusers_1 where usrlogin='" & Session("User_Login") & "'")
            txtUserName.Enabled = False
            intHeight = 50
            intWidth = 150
            GetPDFDocument()
            binddoc()
        End If
        intHeight = 50
        intWidth = 150
        'check for review task
        Dim tskid As Integer
        tskid = objutil.ExeQueryScalar("select tsk_id from wftransaction where sno= " & Convert.ToInt32(Request.QueryString("id")) & " ")
        If tskid = 5 Then 'means reviewer
            dgrow.Visible = False
            rerow.Visible = True
        Else
            dgrow.Visible = True
            rerow.Visible = False
        End If
        'check to show reviewer
        dvPrint.Visible = False
        If HDDocid.Value = ConfigurationManager.AppSettings("ATP") Then
            'ATP Approval
            strsql = "select distinct 'User : ' + name + ' ( ' + case usrtype when 'C' then 'Telkomsel'when 'N' then 'NSN'else 'NSN'end + ' )' userid ,convert(varchar(20),enddatetime,113) enddate, tsk_id from wftransaction  W" & _
            " inner join ebastusers_1 E on W.lmby=e.name " & _
            " where site_id=(select site_id from wftransaction where sno= " & Convert.ToInt32(Request.QueryString("id")) & ") " & _
            " and siteversion=(select siteversion from wftransaction where sno= " & Convert.ToInt32(Request.QueryString("id")) & ")" & _
            " and docid=(select docid from wftransaction where sno= " & Convert.ToInt32(Request.QueryString("id")) & ") and enddatetime is not null"
            userdt = objutil.ExeQueryDT(strsql, "ddd")
            Dim kt As Integer
            If userdt.Rows.Count <> 0 Then
                dvPrint.Visible = True
                For kt = 0 To userdt.Rows.Count - 1
                    If userdt.Rows(kt).Item(2).ToString = "1" Then
                        divRev += "Prepared by " & userdt.Rows(kt).Item(0).ToString & " On " & userdt.Rows(kt).Item(1).ToString & " " & "<br>"
                    Else
                        divRev += "Approved by " & userdt.Rows(kt).Item(0).ToString & " On " & userdt.Rows(kt).Item(1).ToString & " " & "<br>"
                    End If
                Next
                divReviewer.InnerHtml = divRev
                divPONO.InnerHtml = Request.QueryString("pono")
                strsql = "select site_name from codsite where site_no = '" & Request.QueryString("siteno") & "'"
                userdt2 = objutil.ExeQueryDT(strsql, "ddd")
                If userdt.Rows.Count <> 0 Then
                    divSiteName.InnerHtml = userdt2.Rows(0).Item(0).ToString()
                End If
                divSiteID.InnerHtml = Request.QueryString("siteno")
            End If
        Else
            'bugfix-100617: when review wrong person will be displayed
            strsql = "select 'User : ' + name  + ' ( ' + case usrtype when 'C' then 'Telkomsel'when 'N' then 'NSN'else 'NSN'end + ' )' userid ,convert(varchar(20),enddatetime,113) enddate  from wftransaction  W" & _
            " inner join ebastusers_1 E on W.lmby=e.name " & _
            " where tsk_id=5 and site_id=(select site_id from wftransaction where sno= " & Convert.ToInt32(Request.QueryString("id")) & ") " & _
            " and siteversion=(select siteversion from wftransaction where sno= " & Convert.ToInt32(Request.QueryString("id")) & ")" & _
            " and docid=(select docid from wftransaction where sno= " & Convert.ToInt32(Request.QueryString("id")) & ") and enddatetime is not null"
            userdt = objutil.ExeQueryDT(strsql, "ddd")
            If userdt.Rows.Count <> 0 Then
                divRev = "<strong>This document was reviewed by  " & userdt.Rows(0).Item(0).ToString & " On " & userdt.Rows(0).Item(1).ToString & "</strong>"
            Else
                If tskid = 4 Then
                    strsql = "select count(*) from wftransaction  W" & _
                    " where tsk_id=5 and site_id=(select site_id from wftransaction where sno= " & Convert.ToInt32(Request.QueryString("id")) & ") " & _
                    " and siteversion=(select siteversion from wftransaction where sno= " & Convert.ToInt32(Request.QueryString("id")) & ")" & _
                    " and docid=(select docid from wftransaction where sno= " & Convert.ToInt32(Request.QueryString("id")) & ")"
                    Dim rcount As Integer
                    rcount = objutil.ExeQueryScalar(strsql)
                    If rcount > 0 Then
                        divRev = "Reviewer task missing ,please contact Administrator"
                    End If
                End If
            End If
            divReviewer2.InnerHtml = divRev
        End If
        lnkrequest.Attributes.Add("onclick", "waitPreloadPage();")
    End Sub
    Sub binddoc()
        'hardcoded to filter only for BAUT and BAST
        Dim dt As New DataTable
        'dt = objBOsite.uspSiteBAUTDocList(Request.QueryString("siteno"), Request.QueryString("version"), Request.QueryString("docname"))
        Dim strsql As String = "Exec uspSiteBAUTDocList " & Request.QueryString("swid")
        dt = objutil.ExeQueryDT(strsql, "digilist")
        grddocuments.DataSource = dt
        grddocuments.DataBind()
        grddocuments.Columns(1).Visible = False
        grddocuments.Columns(2).Visible = False
        grddocuments.Columns(3).Visible = True
        grddocuments.Columns(4).Visible = False
        grddocuments.Columns(5).Visible = False
        grddocuments.Columns(6).Visible = False
        grddocuments.Columns(7).Visible = True
        'get the parent document
        strsql = "Exec uspSiteBAUTDocList " & Request.QueryString("swid") & ",1"
        dt = objutil.ExeQueryDT(strsql, "digilist")
        grddocuments2.DataSource = dt
        grddocuments2.DataBind()
        grddocuments2.Columns(1).Visible = False
        grddocuments2.Columns(2).Visible = False
        grddocuments2.Columns(3).Visible = True
        grddocuments2.Columns(4).Visible = False
        grddocuments2.Columns(5).Visible = False
        grddocuments2.Columns(6).Visible = False
        grddocuments2.Columns(7).Visible = True
        grddocuments2.Visible = False
        btnReject.Visible = False
        btnReject.Enabled = False
    End Sub
    Sub GetPDFDocument()
        Dim dt As DataTable
        dt = objBo.DigitalSign(Convert.ToInt32(Request.QueryString("id")))
        If (dt.Rows.Count > 0) Then
            strPath = dt.Rows(0)("docpath").ToString()
            HDPath.Value = dt.Rows(0)("docpath").ToString()
            HDPono.Value = dt.Rows(0)("pono").ToString()
            HDDocid.Value = dt.Rows(0)("docid").ToString()
            hdnx.Value = dt.Rows(0)("xval")
            hdny.Value = dt.Rows(0)("yval")
            hdpageNo.Value = dt.Rows(0)("pageno")
            PDFViwer.Attributes.Add("src", ConfigurationManager.AppSettings("Vpath") + strPath)
        End If
    End Sub
    Sub GetBautSign(ByVal newpassword As String)
        Try
            Dim strsql As String = "Exec uspGetBautXY " & Request.QueryString("id") & ",'" & HDPono.Value & "'"
            dt = objutil.ExeQueryDT(strsql, "digilist")
            If (dt.Rows.Count > 0) Then
                Dim Flags As Integer = 0
                Dim IntCount As Integer = 0
                If 1 = 1 Then Flags = Flags Or SAPIWrapper.SAPI_ENUM_DRAWING_ELEMENT_GRAPHICAL_IMAGE
                If 1 = 1 Then Flags = Flags Or SAPIWrapper.SAPI_ENUM_DRAWING_ELEMENT_SIGNED_BY
                If 1 = 0 Then Flags = Flags Or SAPIWrapper.SAPI_ENUM_DRAWING_ELEMENT_REASON
                If 1 = 1 Then Flags = Flags Or SAPIWrapper.SAPI_ENUM_DRAWING_ELEMENT_TIME
                Dim strResult As String = SAPIWrapper.SAPI_sign_file(ConfigurationManager.AppSettings("FPath") + HDPath.Value, Nothing, txtUserName.Text, _
                                          newpassword, dt.Rows(0)("pageno"), dt.Rows(0)("xposition"), dt.Rows(0)("yposition"), intHeight, intWidth, False, "baut sign" & Request.QueryString("id").ToString(), Flags, "baut sign" & Request.QueryString("id").ToString())
                'objutil.ExeNonQuery("exec uspErrLog '', 'xyposs','" & strResult & "','bautxypo'")
            End If
        Catch ex As Exception
            'Response.Write(ex.Message.ToString)
            objutil.ExeNonQuery("exec uspErrLog '', 'xyposs','" & ex.Message.ToString.Replace("'", "''") & "','bautxypo'")
        End Try
    End Sub
    Protected Sub BtnSign_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSign.Click
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        'bugfix100805 to check and fix the xy coordinate
        objutil.ExeQuery("Exec [checkXYCoordinate]")
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
                Dim strResult As String = SAPIWrapper.SAPI_sign_file(ConfigurationManager.AppSettings("FPath") + HDPath.Value, Nothing, txtUserName.Text, _
                                          npwd1, hdpageNo.Value, hdnx.Value, hdny.Value, intHeight, intWidth, False, "test", Flags, "")
                If (strResult = Constants._DigitalSign_Result) Then
                    If (HDDocid.Value = CommonSite.BAUTID) Then
                        GetBautSign(npwd1)
                    End If

                    Dim i As Integer = -1
                    'i = objBo.uspDocApproved(Convert.ToInt32(Request.QueryString("id")), CommonSite.UserId(), CommonSite.UserName(), CommonSite.RollId(), ConfigurationManager.AppSettings("BAUTID"), ConfigurationManager.AppSettings("BASTID"))
                    i = objutil.ExeQueryScalar("Exec [uspDocApproved] " & Convert.ToInt32(Request.QueryString("id")) & "," & CommonSite.UserId() & ",'" & CommonSite.UserName() & "'," & CommonSite.RollId() & "," & ConfigurationManager.AppSettings("BAUTID") & "," & ConfigurationManager.AppSettings("BASTID") & "," & ConfigurationManager.AppSettings("BAST2ID") & " ")
                    If i = 0 Then
                        ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('Error while doing the transaction.');", True)
                    ElseIf i = 1 Then
                        'added by satish on 11thApril
                        '*************************** for mail functionality.***********************
                        'to find next user
                        Dim nextid As Integer
                        Dim k As Integer
                        Dim siteid As Integer
                        Dim siteversion As Integer
                        Dim dtr As New DataTable
                        Dim strs As String = "SELECT ISNULL(USERID,1)USERID,tsk_id,site_id,siteversion FROM WFTRANSACTION WHERE" & _
                        " SITE_ID=(SELECT SITE_ID FROM WFTRANSACTION WHERE SNO = " & Convert.ToInt32(Request.QueryString("id")) & " ) " & _
                        " AND SITEVERSION = (SELECT SITEVERSION FROM WFTRANSACTION WHERE SNO = " & Convert.ToInt32(Request.QueryString("id")) & ")" & _
                        " AND STARTDATETIME IS NOT NULL AND ENDDATETIME IS NULL"
                        ' nextid = objdb.ExeQueryScalar(strs)
                        dtr = objdb.ExeQueryDT(strs, "ddd")
                        If dtr.Rows.Count > 0 Then
                            nextid = dtr.Rows(0).Item(0).ToString
                            k = dtr.Rows(0).Item(1).ToString
                            siteid = dtr.Rows(0).Item(2).ToString
                            siteversion = dtr.Rows(0).Item(3).ToString
                            objdb.ExeNonQuery("exec uspErrLog '', '" & Request.QueryString("id") & "','" & nextid & "' ,'sendmailtransnormal1'")
                            If nextid = 0 Then nextid = 1
                            Try
                                'added by satish on 19thNov2009
                                'for baut reviewers sendding mail function
                                If k = 5 Then
                                    If InStr(Request.QueryString("docname"), "BAUT") > 0 Then
                                        objmail.sendmailTransReview(nextid, siteid, "0", 0, ConfigurationManager.AppSettings("uploadmailconst"), siteversion)
                                    Else
                                        objmail.sendmailTrans(nextid, 0, "0", 0, ConfigurationManager.AppSettings("uploadmailconst"))
                                    End If
                                Else
                                    objmail.sendmailTrans(nextid, 0, "0", 0, ConfigurationManager.AppSettings("uploadmailconst"))
                                End If

                            Catch ex As Exception
                                objdb.ExeNonQuery("exec uspErrLog '', 'mailsending','" & ex.Message.ToString.Replace("'", "''") & "','sendmailtransupload'")
                            End Try
                        Else
                            Dim dtr1 As New DataTable
                            dtr1 = objutil.ExeQueryDT("select site_id,siteversion from wftransaction WHERE SNO = " & Convert.ToInt32(Request.QueryString("id")) & " ", "ddddd")
                            siteid = dtr1.Rows(0).Item(0).ToString
                            siteversion = dtr1.Rows(0).Item(1).ToString
                        End If
                        '*****************************
                        'Added by satish for mails to initiators on 16th march 2010
                        'start 16march2010
                        'for BAUT
                        'bautok = objbositedoc.check4BAUTBAST(siteid, siteversion, 1, Request.QueryString("pono"), Session("User_Name"))
                        bautok = objdb.ExeQueryScalar("exec uspcheck4BAUTBASTNew " & siteid & " ," & siteversion & "," & 1 & ",'" & Request.QueryString("pono") & "','" & Session("User_Name") & "'," & ConfigurationManager.AppSettings("BAUTID") & "," & ConfigurationManager.AppSettings("BASTID") & " ")
                        'for BAST1
                        'bastok = objbositedoc.check4BAUTBAST(siteid, siteversion, 2, Request.QueryString("pono"), Session("User_Name"))
                        bastok = objdb.ExeQueryScalar("exec uspcheck4BAUTBASTNew " & siteid & " ," & siteversion & "," & 2 & ",'" & Request.QueryString("pono") & "','" & Session("User_Name") & "'," & ConfigurationManager.AppSettings("BAUTID") & "," & ConfigurationManager.AppSettings("BASTID") & " ")
                        If bautok = 888 Then
                            'send mail to BAUT intiator
                            nextidi = objdb.ExeQueryScalar("exec uspgetBAUTBASTInitiator " & siteid & "," & ConfigurationManager.AppSettings("BAUTID") & "," & 0 & " ")
                            Try
                                objmail.sendMailIniBAUTBAST(nextidi, Request.QueryString("siteno"), ConfigurationManager.AppSettings("BAUTmailconst"), ConfigurationManager.AppSettings("BAUTID"), Request.QueryString("pono"))
                            Catch ex As Exception
                                objdb.ExeNonQuery("exec uspErrLog '', 'mailsending','" & ex.Message.ToString.Replace("'", "''") & "','sendmailtransBAUTInitiator'")
                            End Try
                        End If
                        If bastok = 999 Then
                            Try
                                objmail.sendMailIniBAUTBAST(0, Request.QueryString("siteno"), ConfigurationManager.AppSettings("BASTmailconst"), ConfigurationManager.AppSettings("BASTID"), Request.QueryString("pono"))
                            Catch ex As Exception
                                objdb.ExeNonQuery("exec uspErrLog '', 'mailsending','" & ex.Message.ToString.Replace("'", "''") & "','sendmailtransBASTInitiator'")
                            End Try
                        End If
                        'end 16march2010
                        'start 6thmay2010
                        'mail to be send bast team upon bast final approve
                        'for sending sms to baut team and bast team
                        Try
                            objsmsnew.sendsmsnew(ConfigurationManager.AppSettings("bastteamrole"), ConfigurationManager.AppSettings("bautteamrole"), Request.QueryString("siteno"), HDDocid.Value, Request.QueryString("pono"), "Approved")
                        Catch ex As Exception
                            objdb.ExeNonQuery("exec uspErrLog '', 'smssending','" & ex.Message.ToString.Replace("'", "''") & "','objsmsnew.sendsmsnew'")
                        End Try
                        Try
                            finalbast = objutil.ExeQueryScalar("select pstatus from bastmaster  where site_Id=" & siteid & " and siteversion =" & siteversion & "")
                        Catch ex As Exception
                            finalbast = 0
                            objdb.ExeNonQuery("exec uspErrLog '', 'executing query','" & ex.Message.ToString.Replace("'", "''") & "','ExeQueryScalar'")
                        End Try
                        If finalbast = 1 Then 'means bast approved finally.'send mail to bast team
                            Try
                                objmail.FinalBASTApprove(ConfigurationManager.AppSettings("bastteamrole"), Request.QueryString("siteno"), ConfigurationManager.AppSettings("FinalBASTmailconst"), ConfigurationManager.AppSettings("BASTID"), Request.QueryString("pono"))
                            Catch ex As Exception
                                objdb.ExeNonQuery("exec uspErrLog '', 'mailsending','" & ex.Message.ToString.Replace("'", "''") & "','objmail.FinalBASTApprove'")
                            End Try
                        End If
                        'end 6thmay2010
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
                                objdb.ExeNonQuery("update wftransaction set rstatus=4 where tsk_id in (4,5)  and site_id = (select site_id from wftransaction where sno=" & Request.QueryString("id") & ") and siteversion= " & Request.QueryString("version") & " and enddatetime is null  and docid=" & ConfigurationManager.AppSettings("BASTID")) '1032
                                objdb.ExeNonQuery("update sitedoc set rstatus=2 where siteid = (select site_id from wftransaction where sno=" & Request.QueryString("id") & ") and version= " & Request.QueryString("version") & " and docid=" & ConfigurationManager.AppSettings("WCTRBASTID")) '1034
                                ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript123", "gopage(" & swid & ");", True)
                            End If
                        End If
                        'this is to update back the rstatus =2
                        If InStr(Request.QueryString("docname"), "WCTR BAST") > 0 Then
                            Dim y As Integer = 0
                            y = objdb.ExeQueryScalar("select count(*) from wftransaction where STARTDATETIME IS NOT NULL AND ENDDATETIME IS NULL and site_id=(select site_id from wftransaction where sno=" & Request.QueryString("id") & ") and siteversion=" & Request.QueryString("version") & " and docid=" & ConfigurationManager.AppSettings("WCTRBASTID")) '1034
                            If y = 0 Then objdb.ExeNonQuery("update wftransaction set rstatus=2 where tsk_id in (4,5)  and site_id = (select site_id from wftransaction where sno=" & Request.QueryString("id") & ") and siteversion= " & Request.QueryString("version") & " and enddatetime is null  and docid=" & ConfigurationManager.AppSettings("BASTID")) '1032
                        End If
                        ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "WindowsCloseApprover();", True)
                    End If
                Else
                    ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('Error on signning PDF:\n" + strResult + "');", True)
                End If
            Catch ex As Exception
                objdb.ExeNonQuery("exec uspErrLog '', 'BtnSign_Click','" & ex.Message.ToString.Replace("'", "''") & "','Error on end of Catch'")
            End Try
        Else
            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('invalid  userid or password');", True)
        End If
        dgdiv.Style("display") = "none"
    End Sub
    Protected Sub btnreject_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReject.Click
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
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
        For k = 0 To grddocuments.Rows.Count - 1
            rdbr = grddocuments.Rows(k).Cells(5).FindControl("rdbstatus")
            If rdbr.SelectedValue = 1 Then
                str = IIf(str <> "", str & "," & grddocuments.Rows(k).Cells(4).Text.ToString, grddocuments.Rows(k).Cells(4).Text.ToString)
                sw_id = grddocuments.Rows(k).Cells(4).Text.ToString
                Dim txt1 As TextBox = grddocuments.Rows(k).Cells(6).FindControl("txtremarks")
                remarks = txt1.Text
                docid = grddocuments.Rows(k).Cells(1).Text.ToString
                siteid = grddocuments.Rows(k).Cells(5).Text.ToString
                pono = grddocuments.Rows(k).Cells(6).Text.ToString
                'dedy 091106
                objutil.ExeNonQuery("Exec uspBAUTDocReject " & sw_id & ",'" & remarks & "'," & usrid & "," & roleid & ",'" & usrname & "','" & pono & "','" & siteid & "'," & docid & "," & ConfigurationManager.AppSettings("WCTRBASTID"))
                'for sending sms to baut team and bast team
                objsmsnew.sendsmsnew(ConfigurationManager.AppSettings("bastteamrole"), ConfigurationManager.AppSettings("bautteamrole"), Request.QueryString("siteno"), docid, Request.QueryString("pono"), "Rejected")
                count = count + 1
            End If
        Next
        'reject the parent document as well
        If count > 0 Then
            For k = 0 To grddocuments2.Rows.Count - 1
                rdbr = grddocuments2.Rows(k).Cells(5).FindControl("rdbstatus")
                str = IIf(str <> "", str & "," & grddocuments2.Rows(k).Cells(4).Text.ToString, grddocuments2.Rows(k).Cells(4).Text.ToString)
                sw_id = grddocuments2.Rows(k).Cells(4).Text.ToString
                Dim txt1 As TextBox = grddocuments2.Rows(k).Cells(6).FindControl("txtremarks")
                remarks = txt1.Text
                docid = grddocuments2.Rows(k).Cells(1).Text.ToString
                siteid = grddocuments2.Rows(k).Cells(5).Text.ToString
                pono = grddocuments2.Rows(k).Cells(6).Text.ToString
                'dedy 091106
                objutil.ExeNonQuery("Exec uspBAUTDocReject " & sw_id & ",'" & remarks & "'," & usrid & "," & roleid & ",'" & usrname & "','" & pono & "','" & siteid & "'," & docid & "," & ConfigurationManager.AppSettings("WCTRBASTID"))
                'for sending sms to baut team and bast team
                objsmsnew.sendsmsnew(ConfigurationManager.AppSettings("bastteamrole"), ConfigurationManager.AppSettings("bautteamrole"), Request.QueryString("siteno"), docid, Request.QueryString("pono"), "Rejected")
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
    Protected Sub grddocuments_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grddocuments.RowDataBound
        Select Case e.Row.RowType
            Case DataControlRowType.DataRow
                Dim lbl As Label = CType(e.Row.FindControl("lblNo"), Label)
                lbl.Text = (grddocuments.PageIndex * grddocuments.PageSize) + (e.Row.RowIndex + 1) & ".&nbsp;"
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
    Protected Sub lnkrequest_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkrequest.Click
        Dim npwd As String = ""
        Dim msgdata As New StringBuilder
        If txtUserName.Text <> "" Then
            objsms.Port = "COM4"
            objsms.BaudRate = 115200
            objsms.DataBits = 8
            objsms.PIN = "1234" 'bugfix100806 error when requesting sms
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
            objutil.ExeNonQuery("exec smsaudit'" & txtUserName.Text & "','" & phone & "','" & Request.QueryString("siteno") & "','" & Request.QueryString("pono") & "','" & Request.QueryString("docname") & "'")
            loadingdiv.Style("display") = "none"
            'ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('Please check for your password in your phone');", True)
            Response.Write("<script>alert('Please check for your password in your phone');</script>")
        Else
            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('Please keyin the user name and click request password');", True)
        End If
    End Sub
    Sub DOThis(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rad As RadioButtonList = CType(sender, RadioButtonList)
        Dim grdr As GridViewRow = CType(rad.NamingContainer, GridViewRow)
        Dim x As Integer = grdr.RowIndex
        Dim rdl As RadioButtonList = grddocuments.Rows(x).Cells(5).FindControl("rdbstatus")
        Dim txt1 As TextBox = grddocuments.Rows(x).Cells(6).FindControl("txtremarks")
        txt1.Text = "Please enter remarks here......"
        If rdl.SelectedValue = 0 Then 'when approve
            txt1.Visible = False
            txtUserName.Enabled = True
            txtPassword.Enabled = True
            lnkrequest.Enabled = True
            BtnSign.Enabled = True
            btnReject.Enabled = False
            btnrejectreview.Visible = False
            btnrejectreview.Enabled = False
        Else 'when reject
            txt1.Visible = True
            txtUserName.Enabled = False
            txtPassword.Enabled = False
            lnkrequest.Enabled = False
            BtnSign.Enabled = False
            btnReject.Enabled = True
            btnrejectreview.Visible = True
            btnrejectreview.Enabled = True
            btnreview.Visible = False
            btnreview.Enabled = False
        End If
    End Sub
    Protected Sub grddocuments2_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        Select Case e.Row.RowType
            Case DataControlRowType.DataRow
                Dim lbl As Label = CType(e.Row.FindControl("lblnotwo"), Label)
                lbl.Text = (grddocuments2.PageIndex * grddocuments2.PageSize) + (e.Row.RowIndex + 1) & ".&nbsp;"
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
    Protected Sub btnreview_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnreview.Click
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        'bugfix100805 to check and fix the xy coordinate
        objutil.ExeQuery("Exec [checkXYCoordinate]")
        Dim i As Integer
        'i = objBo.uspDocApproved(Convert.ToInt32(Request.QueryString("id")), CommonSite.UserId(), CommonSite.UserName(), CommonSite.RollId())
        i = objutil.ExeQueryScalar("Exec [uspDocApproved] " & Convert.ToInt32(Request.QueryString("id")) & "," & CommonSite.UserId() & ",'" & CommonSite.UserName() & "'," & CommonSite.RollId() & "," & ConfigurationManager.AppSettings("BAUTID") & "," & ConfigurationManager.AppSettings("BASTID") & "," & ConfigurationManager.AppSettings("BAST2ID") & "")
        If i = 0 Then
            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('Error while doing the transaction.');", True)
        ElseIf i = 1 Then
            Dim nextid As Integer
            Dim k As Integer
            Dim siteid As Integer
            Dim siteversion As Integer
            Dim dtr As New DataTable
            Dim strs As String = "SELECT ISNULL(USERID,1)USERID,tsk_id,site_id,siteversion FROM WFTRANSACTION WHERE" & _
            " SITE_ID=(SELECT SITE_ID FROM WFTRANSACTION WHERE SNO = " & Convert.ToInt32(Request.QueryString("id")) & " ) " & _
            " AND SITEVERSION = (SELECT SITEVERSION FROM WFTRANSACTION WHERE SNO = " & Convert.ToInt32(Request.QueryString("id")) & ")" & _
            " AND STARTDATETIME IS NOT NULL AND ENDDATETIME IS NULL"
            'nextid = objdb.ExeQueryScalar(strs)
            dtr = objdb.ExeQueryDT(strs, "ddd")
            If dtr.Rows.Count > 0 Then
                nextid = dtr.Rows(0).Item(0).ToString
                k = dtr.Rows(0).Item(1).ToString
                siteid = dtr.Rows(0).Item(2).ToString
                siteversion = dtr.Rows(0).Item(3).ToString
                objdb.ExeNonQuery("exec uspErrLog '', '" & Request.QueryString("id") & "','" & nextid & "' ,'sendmailtransnormal1'")
                'update 
                'for sending sms to baut team and bast team   
                'objsmsnew.sendsmsnew(ConfigurationManager.AppSettings("bastteamrole"), ConfigurationManager.AppSettings("bautteamrole"), Request.QueryString("siteno"), HDDocid.Value, Request.QueryString("pono"), "Reviewed")
                If nextid = 0 Then nextid = 1
                Try
                    If InStr(Request.QueryString("docname"), "(BAUT)") > 0 Then
                        objdb.ExeNonQuery("update wftransactionreview set status=0 where site_id=" & siteid & " and siteversion=" & siteversion & " and docid= " & ConfigurationManager.AppSettings("BAUTID") & "")
                        objdb.ExeNonQuery("update wftransaction set userid=" & Session("User_Id") & " where site_id=" & siteid & " and siteversion=" & siteversion & " and docid= " & ConfigurationManager.AppSettings("BAUTID") & " and tsk_id=5")
                    End If
                    If InStr(Request.QueryString("docname"), "(BAST)") > 0 > 0 Then
                        objdb.ExeNonQuery("update wftransactionreview set status=0 where site_id=" & siteid & " and siteversion=" & siteversion & " and docid= " & ConfigurationManager.AppSettings("BASTID") & "")
                        objdb.ExeNonQuery("update wftransaction set userid=" & Session("User_Id") & " where site_id=" & siteid & " and siteversion=" & siteversion & " and docid= " & ConfigurationManager.AppSettings("BASTID") & " and tsk_id=5")
                    End If
                    'objmail.sendmailTrans(nextid, 0, "0", 0, ConfigurationManager.AppSettings("uploadmailconst"))
                Catch ex As Exception
                    objdb.ExeNonQuery("exec uspErrLog '', 'mailsending','" & ex.Message.ToString.Replace("'", "''") & "','sendmailtransupload'")
                End Try
                ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "WindowsCloseReviewer();", True)
            Else
                If HDDocid.Value = ConfigurationManager.AppSettings("ATP") Then
                    'ATP Approval
                    strsql = "select distinct 'User : ' + name + ' ( ' + case usrtype when 'C' then 'Telkomsel'when 'N' then 'NSN'else 'NSN'end + ' )' userid ,convert(varchar(20),enddatetime,113) enddate, tsk_id from wftransaction  W" & _
                    " inner join ebastusers_1 E on W.lmby=e.name " & _
                    " where site_id=(select site_id from wftransaction where sno= " & Convert.ToInt32(Request.QueryString("id")) & ") " & _
                    " and siteversion=(select siteversion from wftransaction where sno= " & Convert.ToInt32(Request.QueryString("id")) & ")" & _
                    " and docid=(select docid from wftransaction where sno= " & Convert.ToInt32(Request.QueryString("id")) & ") and enddatetime is not null"
                    userdt = objutil.ExeQueryDT(strsql, "ddd")
                    Dim kt As Integer
                    If userdt.Rows.Count <> 0 Then
                        dvPrint.Visible = True
                        divRev = "" 'reset
                        For kt = 0 To userdt.Rows.Count - 1
                            If userdt.Rows(kt).Item(2).ToString = "1" Then
                                divRev += "Prepared by " & userdt.Rows(kt).Item(0).ToString & " On " & userdt.Rows(kt).Item(1).ToString & " " & "<br>"
                            Else
                                divRev += "Approved by " & userdt.Rows(kt).Item(0).ToString & " On " & userdt.Rows(kt).Item(1).ToString & " " & "<br>"
                            End If
                        Next
                        divReviewer.InnerHtml = divRev
                        divPONO.InnerHtml = Request.QueryString("pono")
                        strsql = "select site_name from codsite where site_no = '" & Request.QueryString("siteno") & "'"
                        userdt2 = objutil.ExeQueryDT(strsql, "ddd")
                        If userdt.Rows.Count <> 0 Then
                            divSiteName.InnerHtml = userdt2.Rows(0).Item(0).ToString()
                        End If
                        divSiteID.InnerHtml = Request.QueryString("siteno")
                    End If
                    'then atp merge pdf
                    Dim filenameorg As String
                    Dim ReFileName As String
                    filenameorg = "ATPFINAL-" & Format(CDate(DateTime.Now), "ddMMyyyyHHss")
                    ReFileName = filenameorg & ".htm"
                    Dim final As String = ""
                    Dim yy() As String = HDPath.Value.Split("\")
                    Dim ii As Integer
                    For ii = 0 To yy.Length - 2
                        final = IIf(final = "", yy(ii), final & "\" & yy(ii))
                    Next
                    final = final & "\"
                    strPath = ConfigurationManager.AppSettings("Fpath") + final
                    Dim sw As HtmlTextWriter = New HtmlTextWriter(New StreamWriter(strPath & ReFileName, False, System.Text.UnicodeEncoding.UTF8))
                    sw.WriteLine("<html><head><style type=""text/css"">")
                    sw.WriteLine("tr{padding: 3px;}")
                    sw.WriteLine(".MainCSS{margin-bottom: 0px;margin-left: 20px;margin-right: 20px;margin-top: 0px;width: 800px;height: 700px;text-align: center;}")
                    sw.WriteLine(".lblText{font-family: verdana;font-size: 8pt;color: #000000;text-align: left;}")
                    sw.WriteLine(".PageBreak{page-break-before:always;}")
                    sw.WriteLine(".lblBText{font-family: verdana;font-size: 8pt;color: #000000;text-align: left;font-weight: bold;}")
                    sw.WriteLine(".lblBold{font-family: verdana;font-size: 12pt;color: #000000;font-weight: bold;}")
                    sw.WriteLine(".textFieldStyle{background-color: white;border: 1px solid;color: black;font-family: verdana;font-size: 9pt;}")
                    sw.WriteLine(".GridHeader{color: #0e1b42;background-color: Orange;font-size: 9pt;font-family: Verdana;text-align: Left;vertical-align: bottom;font-weight: bold;}")
                    sw.WriteLine(".GridEvenRows{background-color: #e3e3e3;vertical-align: top;font-family: verdana;font-size: 8pt;color: #000000;}")
                    sw.WriteLine(".GridOddRows{background-color: white;vertical-align: top;font-family: verdana;font-size: 8pt;color: #000000;}")
                    sw.WriteLine(".PagerTitle{font-size: 8pt;background-color: #cddbbf;text-align: Right;vertical-align: middle;color: #25375b;font-weight: bold;}")
                    sw.WriteLine(".Hcap{height: 5px;}")
                    sw.WriteLine(".VCap{width: 10px;}")
                    sw.WriteLine("</style></head>")
                    sw.WriteLine("<body class=""MainCSS"">")
                    sw.WriteLine("<table cellpadding=""0"" cellspacing=""0"" border=""0""><tr><td>")
                    dvPrint.RenderControl(sw)
                    sw.WriteLine("</td></tr>")
                    sw.WriteLine("</td></tr><tr><td>")
                    sw.WriteLine("</td></tr></table>")
                    sw.WriteLine("</body>")
                    sw.WriteLine("</html>")
                    sw.Close()
                    sw.Dispose()
                    Dim newdocpath As String
                    newdocpath = Common.TakeFileUpload.ConvertAnyFormatToPDFHtmlNew1(strPath & ReFileName, strPath, filenameorg, ConfigurationManager.AppSettings("Fpath") & HDPath.Value)
                    newdocpath = final & newdocpath
                    objutil.ExeNonQuery("update sitedoc set docpath='" & newdocpath & "' where docid=" & ConfigurationManager.AppSettings("ATP") & " and SITEID=(SELECT SITE_ID FROM WFTRANSACTION WHERE SNO = " & Convert.ToInt32(Request.QueryString("id")) & " ) AND VERSION = (SELECT SITEVERSION FROM WFTRANSACTION WHERE SNO = " & Convert.ToInt32(Request.QueryString("id")) & ")")
                    ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "WindowsCloseReviewer();", True)
                End If
            End If
        End If
    End Sub
    Protected Sub btnrejectreview_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnrejectreview.Click
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
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
        For k = 0 To grddocuments.Rows.Count - 1
            rdbr = grddocuments.Rows(k).Cells(5).FindControl("rdbstatus")
            If rdbr.SelectedValue = 1 Then
                str = IIf(str <> "", str & "," & grddocuments.Rows(k).Cells(4).Text.ToString, grddocuments.Rows(k).Cells(4).Text.ToString)
                sw_id = grddocuments.Rows(k).Cells(4).Text.ToString
                Dim txt1 As TextBox = grddocuments.Rows(k).Cells(6).FindControl("txtremarks")
                remarks = txt1.Text
                docid = grddocuments.Rows(k).Cells(1).Text.ToString
                siteid = grddocuments.Rows(k).Cells(5).Text.ToString
                pono = grddocuments.Rows(k).Cells(6).Text.ToString
                'dedy 091106
                objutil.ExeNonQuery("Exec uspBAUTDocReject " & sw_id & ",'" & remarks & "'," & usrid & "," & roleid & ",'" & usrname & "','" & pono & "','" & siteid & "'," & docid & "," & ConfigurationManager.AppSettings("WCTRBASTID"))
                count = count + 1
            End If
        Next
        'reject the parent document as well
        If count > 0 Then
            For k = 0 To grddocuments2.Rows.Count - 1
                rdbr = grddocuments2.Rows(k).Cells(5).FindControl("rdbstatus")
                str = IIf(str <> "", str & "," & grddocuments2.Rows(k).Cells(4).Text.ToString, grddocuments2.Rows(k).Cells(4).Text.ToString)
                sw_id = grddocuments2.Rows(k).Cells(4).Text.ToString
                Dim txt1 As TextBox = grddocuments2.Rows(k).Cells(6).FindControl("txtremarks")
                remarks = txt1.Text
                docid = grddocuments2.Rows(k).Cells(1).Text.ToString
                siteid = grddocuments2.Rows(k).Cells(5).Text.ToString
                pono = grddocuments2.Rows(k).Cells(6).Text.ToString
                'dedy 091106
                objutil.ExeNonQuery("Exec uspBAUTDocReject " & sw_id & ",'" & remarks & "'," & usrid & "," & roleid & ",'" & usrname & "','" & pono & "','" & siteid & "'," & docid & "," & ConfigurationManager.AppSettings("WCTRBASTID"))
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
End Class
