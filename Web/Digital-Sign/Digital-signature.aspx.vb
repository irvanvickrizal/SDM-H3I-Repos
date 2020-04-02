Imports BusinessLogic
Imports Common
Imports System.Data
Imports System.Collections.Generic
Imports AXmsCtrl
Imports System.IO
Imports NSNCustomizeConfiguration
Imports Common_NSNFramework


Partial Class Digital_signature
    Inherits System.Web.UI.Page
    Shared strPath As String = ""
    Dim objBo As New BODashBoard
    Dim objboSiteDoc As New BOSiteDocs
    Dim objBOM As New BOMailReport
    Dim intPage, intX, intY, intHeight, intWidth As New Integer
    Dim objdb As New DBUtil
    Dim dtwf As New DataTable
    'Dim objdbutil As New DBUtil
    Dim objutil As New DBUtil
    Dim i, j, k, l As Integer
    Dim objsms As New SMSNew
    Dim objmail As New TakeMail
    Dim bautok As Integer
    Dim bastok As Integer
    Dim nextidi, nextidj As Integer
    Dim finalbast As Integer
    Dim objsmsnew As New SMSNew
    Dim dt, dtn, userprep, userdt, userdt1, userdt2 As New DataTable
    Dim siteid As Int32
    Dim siteversion As Integer
    Dim strsql As String = ""
    Dim divRev As String = ""
#Region "NSNFramework"
    Dim dbutils_nsn As New DBUtils_NSN
#End Region
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        If Page.IsPostBack = False Then
            'bugfix100902 take the user login name from db, because taken from session will make the case censitive not the same
            txtUserName.Text = objutil.ExeQueryScalarString("select usrlogin from ebastusers_1 where usrlogin='" & Session("User_Login") & "'")
            txtUserName.Enabled = False
            intHeight = 50
            intWidth = 150
            GetPDFDocument()
            binddoc()
            'new code irvan for set up button approval rejection in the same place
            MvApprovalButtonPanel.SetActiveView(vwDefaultPanel)
            BtnRejectReviewNew.Visible = False
            btnApproveWithRemarks.Visible = False
            '---------------------------------------------------------------------
        End If
        intHeight = 50
        intWidth = 150
        'check for review task
        Dim tskid As Integer
        tskid = objutil.ExeQueryScalar("select tsk_id from wftransaction where sno= " & Convert.ToInt32(Request.QueryString("id")) & " ")
        If tskid = 5 Or tskid = 6 Then 'means reviewer
            dgrow.Visible = False
            rerow.Visible = True
            If tskid = 6 Then
                btnreview.Text = "Approve"
                listdocuments.Visible = False
            End If
        Else
            dgrow.Visible = True
            rerow.Visible = False
        End If
        'new code 25 oct 2011 -- Irvan Vickrizal
        Dim docid, docidparent As Integer
        Dim strQuery As String = String.Empty
        strQuery = "select top 1 docid,site_id,siteversion from wftransaction where sno=" & Convert.ToInt32(Request.QueryString("id"))
        Dim dtDocAtt As DataTable = objutil.ExeQueryDT(strQuery, "ddd")
        If (dtDocAtt IsNot Nothing) Then
            docid = dtDocAtt.Rows(0).Item(0)
            siteid = dtDocAtt.Rows(0).Item(1)
            siteversion = dtDocAtt.Rows(0).Item(2)
        End If
        docidparent = objutil.ExeQueryScalar("select distinct parent_id from coddoc where doc_id in (" & docid & ")")
        '----------------------------------------
        'check to show reviewer
        dvPrint.Visible = False
        'Dim docid, docidparent As Integer
        'docid = objutil.ExeQueryScalar("select docid from wftransaction where sno=" & Convert.ToInt32(Request.QueryString("id")))
        'docidparent = objutil.ExeQueryScalar("select distinct parent_id from coddoc where doc_id in (select docid from wftransaction where sno=" & Convert.ToInt32(Request.QueryString("id")) & ")")
        'ATP Approval Full Approval
        BtnRejectReviewNew.Visible = False
        BtnATPOnSite.Visible = False
        btnApproveWithRemarks.Visible = False
        If ConfigurationManager.AppSettings("ATP") = docid.ToString Or ConfigurationManager.AppSettings("ATP") = docidparent.ToString Then
            BtnRejectReviewNew.Visible = True
            If tskid = 6 Then
                btnApproveWithRemarks.Visible = True
            End If

            If (Request.QueryString("wpid") IsNot Nothing) Then
                Dim strWPId As String = Request.QueryString("wpid")
                If (strWPId <> "") Then
                    SetButtonATPOnSite(strWPId, siteid, siteversion, ConfigurationManager.AppSettings("ATP"))
                End If
            End If
            'siteid = objutil.ExeQueryScalar("select site_id from wftransaction where sno=" & Convert.ToInt32(Request.QueryString("id")))
            'siteversion = objutil.ExeQueryScalar("select siteversion from wftransaction where sno=" & Convert.ToInt32(Request.QueryString("id")))
            'new code irvan vickrizal
            btnreview.Text = "Approve"

            strsql = "select distinct case usrtype when 'C' then 'Telkomsel'when 'N' then 'NSN'else 'Partner'end usertype,name,SignTitle,enddatetime, tsk_id, w.sno from wftransaction  W" & _
                 " inner join ebastusers_1 E on e.usr_id=w.LMBY and w.docid = 2001 inner join trole tr on e.usrRole = tr.roleid " & _
                 " where site_id=(select site_id from wftransaction where sno= " & Convert.ToInt32(Request.QueryString("id")) & ") " & _
                 " and siteversion=(select siteversion from wftransaction where sno= " & Convert.ToInt32(Request.QueryString("id")) & ")" & _
                 " and docid=(select docid from wftransaction where sno= " & Convert.ToInt32(Request.QueryString("id")) & ") and enddatetime is not null and tsk_id in(1,5,6)" & _
                 " and w.status = 0 and w.rstatus = 2 order by w.sno asc"
            
            
            '-------------------------------------------------------------------------

            'strsql = "select distinct 'User : ' + name + ' ( ' + case usrtype when 'C' then 'Telkomsel'when 'N' then 'NSN'else 'NSN'end + ' )' userid ,convert(varchar(max),enddatetime,113) enddate, tsk_id, w.sno from wftransaction  W" & _
            '" inner join ebastusers_1 E on e.usr_id=w.lmby " & _
            '" where site_id=(select site_id from wftransaction where sno= " & Convert.ToInt32(Request.QueryString("id")) & ") " & _
            '" and siteversion=(select siteversion from wftransaction where sno= " & Convert.ToInt32(Request.QueryString("id")) & ")" & _
            '" and docid=(select docid from wftransaction where sno= " & Convert.ToInt32(Request.QueryString("id")) & ") " & _
            '" and enddatetime is not null and tsk_id=5" & _
            '" order by w.sno asc"

            userdt = objutil.ExeQueryDT(strsql, "ddd")
            Dim kt As Integer
            If userdt.Rows.Count <> 0 Then
                dvPrint.Visible = True
                Dim dates As String = ""
                For kt = 0 To userdt.Rows.Count - 1
                    'If userdt.Rows(kt).Item(2).ToString = "1" Then
                    '    divRev += "Prepared by " & userdt.Rows(kt).Item(0).ToString & " On " & userdt.Rows(kt).Item(1).ToString.Split(" ")(0) & " " & userdt.Rows(kt).Item(1).ToString.Split(" ")(1) & " " & userdt.Rows(kt).Item(1).ToString.Split(" ")(2) & " " & "<br>"
                    'Else
                    '    divRev += "Approved by " & userdt.Rows(kt).Item(0).ToString & " On " & userdt.Rows(kt).Item(1).ToString.Split(" ")(0) & " " & userdt.Rows(kt).Item(1).ToString.Split(" ")(1) & " " & userdt.Rows(kt).Item(1).ToString.Split(" ")(2) & " " & "<br>"
                    'End If

                    'new code irvan vickrizal
                    Dim lvldesc As String = String.Empty
                    If (userdt.Rows(kt).Item(4).ToString = "1") Then
                        lvldesc = userdt.Rows(kt).Item(0).ToString & " prepared by "
                    Else
                        lvldesc = userdt.Rows(kt).Item(0).ToString & " approved by "
                    End If
                    divRev += lvldesc & userdt.Rows(kt).Item(1).ToString & ", " & userdt.Rows(kt).Item(2).ToString & " On " & String.Format("{0:dd MMM yyyy}", userdt.Rows(kt).Item(3)) & " " & "<br />"
                Next
                divReviewer.InnerHtml = divRev
                divPONO.InnerHtml = Request.QueryString("pono")

                If Not Request.QueryString("wpid") Is Nothing Then
                    Dim strQueryHOTAsPerPo As String = "select HOTAsPerPO from podetails where workpkgid='" & Request.QueryString("wpid") & "'"
                    Dim dtHOTasPerPO As DataTable = objutil.ExeQueryDT(strQueryHOTAsPerPo, "HotasPerPo")
                    If dtHOTasPerPO.Rows.Count > 0 Then
                        If Not String.IsNullOrEmpty(dtHOTasPerPO.Rows(0).Item(0).ToString) Then
                            divPONO.InnerHtml = dtHOTasPerPO.Rows(0).Item(0).ToString
                        End If
                    End If
                End If

                divSiteID.InnerHtml = Request.QueryString("siteno")
                'strsql = "select site_name from codsite where site_no = '" & Request.QueryString("siteno") & "'"
                strsql = "select top 1(SiteName) site_name from epmdata where workpackageid='" & Request.QueryString("wpid") & "' order by lmdt desc"
                userdt2 = objutil.ExeQueryDT(strsql, "ddd")
                divSiteName.InnerHtml = userdt2.Rows(0).Item(0).ToString()
                strsql = "select top 1 pd.fldtype from wftransaction W" & _
                " inner join podetails pd on pd.siteno=(select site_no from codsite where site_id=(select site_id from wftransaction where sno= " & Convert.ToInt32(Request.QueryString("id")) & ")) " & _
                " and pd.siteversion=(select siteversion from wftransaction where sno= " & Convert.ToInt32(Request.QueryString("id")) & ") " & _
                " where w.site_id=(select site_id from wftransaction where sno= " & Convert.ToInt32(Request.QueryString("id")) & ") " & _
                " and w.siteversion=(select siteversion from wftransaction where sno= " & Convert.ToInt32(Request.QueryString("id")) & ")" & _
                " and w.docid=(select docid from wftransaction where sno= " & Convert.ToInt32(Request.QueryString("id")) & ") " & _
                " and enddatetime is not null and tsk_id=1"
                divScope.InnerHtml = objutil.ExeQueryScalarString(strsql)
            End If
            divReviewer.InnerHtml = divRev
        Else
            'Normal review approval
            'bugfix-100617: when review wrong person will be displayed
            strsql = "select distinct 'User : ' + name + ' ( ' + case usrtype when 'C' then 'Telkomsel'when 'N' then 'NSN'else 'NSN'end + ' )' userid ,convert(varchar(max),enddatetime,113) enddate, tsk_id, w.sno from wftransaction  W" & _
            " inner join ebastusers_1 E on e.usr_id=w.lmby " & _
            " where site_id=(select site_id from wftransaction where sno= " & Convert.ToInt32(Request.QueryString("id")) & ") " & _
            " and siteversion=(select siteversion from wftransaction where sno= " & Convert.ToInt32(Request.QueryString("id")) & ")" & _
            " and docid=(select docid from wftransaction where sno= " & Convert.ToInt32(Request.QueryString("id")) & ") " & _
            " and enddatetime is not null and tsk_id=5 " & _
            " order by w.sno asc"
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
                        divRev = "Reviewer task missing, please contact Administrator"
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
        'dt = objboSiteDoc.uspSiteBAUTDocList(Request.QueryString("siteno"), Request.QueryString("version"), Request.QueryString("docname"))
        'Dim strsql As String = "Exec uspSiteBAUTDocList " & Request.QueryString("swid")
        Dim strsql As String = "Exec uspSiteBAUTDocList " & Request.QueryString("swid") & ",0 , " & CommonSite.UserId
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
        'strsql = "Exec uspSiteBAUTDocList " & Request.QueryString("swid") & ",1"
        strsql = "Exec uspSiteBAUTDocList " & Request.QueryString("swid") & ",1 ," & CommonSite.UserId
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
            PDFViwer.Attributes.Add("src", ConfigurationManager.AppSettings("Vpath") + strPath.Replace("\", "/"))
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
                Dim strResult As String = SAPIWrapper.SAPI_sign_file(ConfigurationManager.AppSettings("FPath") + HDPath.Value, _
                    Nothing, txtUserName.Text, newpassword, dt.Rows(0)("pageno"), dt.Rows(0)("xposition"), dt.Rows(0)("yposition"), _
                    intHeight, intWidth, False, "baut sign" & Request.QueryString("id").ToString(), Flags, _
                    "baut sign" & Request.QueryString("id").ToString())
            End If
        Catch ex As Exception
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
                LblTestSign.Text = strResult
                If (strResult = Constants._DigitalSign_Result) Then
                    If (HDDocid.Value = CommonSite.BAUTID) Then
                        GetBautSign(npwd1)
                    End If
                    Dim i As Integer = -1
                    'i = objBo.uspDocApproved(Convert.ToInt32(Request.QueryString("id")), CommonSite.UserId(), CommonSite.UserName(), CommonSite.RollId(), ConfigurationManager.AppSettings("BAUTID"), ConfigurationManager.AppSettings("BASTID"))
                    strsql = "Exec [uspDocApproved] " & Convert.ToInt32(Request.QueryString("id")) & "," & CommonSite.UserId() & ",'" & CommonSite.UserName() & "'," & CommonSite.RollId() & "," & ConfigurationManager.AppSettings("BAUTID") & "," & ConfigurationManager.AppSettings("BASTID") & "," & ConfigurationManager.AppSettings("BAST2ID") & " "
                    i = objutil.ExeQueryScalar(strsql)
                    If i = 0 Then
                        ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('Error while doing the transaction.');", True)
                    ElseIf i = 1 Then
                        Dim nextid As Integer
                        Dim k As Integer
                        Dim siteversion As Integer
                        Dim docid As Integer
                        Dim dtr As New DataTable
                        Dim strs As String = "SELECT ISNULL(USERID,1)USERID,tsk_id,site_id,siteversion,docid FROM WFTRANSACTION WHERE" & _
                        " SITE_ID=(SELECT SITE_ID FROM WFTRANSACTION WHERE SNO = " & Convert.ToInt32(Request.QueryString("id")) & " ) " & _
                        " AND SITEVERSION = (SELECT SITEVERSION FROM WFTRANSACTION WHERE SNO = " & Convert.ToInt32(Request.QueryString("id")) & ")" & _
                        " AND STARTDATETIME IS NOT NULL AND ENDDATETIME IS NULL"
                        dtr = objdb.ExeQueryDT(strs, "ddd")
                        If dtr.Rows.Count > 0 Then
                            nextid = dtr.Rows(0).Item(0).ToString
                            k = dtr.Rows(0).Item(1).ToString
                            siteid = dtr.Rows(0).Item(2).ToString
                            siteversion = dtr.Rows(0).Item(3).ToString
                            docid = dtr.Rows(0).Item(4).ToString
                            'objdb.ExeNonQuery("exec uspErrLog '', '" & Request.QueryString("id") & "','" & nextid & "' ,'sendmailtransnormal1'")
                            'If nextid = 0 Then nextid = 1
                            ''bugfix101116 sent mail notification with CC
                            'Dim dtnew As New DataTable
                            'Dim n As Integer
                            'dtnew = objutil.ExeQueryDT("exec uspSiteDocUserId " & siteid.ToString() & "," & siteversion.ToString() & "," & docid.ToString(), "dd")
                            'If dtnew.Rows.Count > 0 Then
                            '    For n = 0 To dtnew.Rows.Count - 1
                            '        Try
                            '            objmail.sendMailNotifications(dtnew.Rows(0).Item(3).ToString(), ConfigurationManager.AppSettings("uploadmailconst"))
                            '        Catch ex As Exception
                            '            objdb.ExeNonQuery("exec uspErrLog '', 'sendMailNotifications','" & ex.Message.ToString.Replace("'", "''") & "','sendMailNotifications'")
                            '        End Try
                            '    Next
                            'End If
                        Else
                            Dim dtr1 As New DataTable
                            dtr1 = objutil.ExeQueryDT("select site_id,siteversion from wftransaction WHERE SNO = " & Convert.ToInt32(Request.QueryString("id")) & " ", "ddddd")
                            siteid = dtr1.Rows(0).Item(0).ToString
                            siteversion = dtr1.Rows(0).Item(1).ToString
                        End If
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
                        '***********************************************************************
                        'IF it is BAST  second statge then we need to generate coresponding WCTR
                        '***********************************************************************
                        'Data updated by taking bast revie data
                        'Generate PDF
                        'update sitedoc
                        'do insert trans 
                        '*********************
                        ' this if is only for wctr bast
                        If InStr(Request.QueryString("docname"), "(BAST)") > 0 Then
                            Dim t As Integer = 0
                            t = objdb.ExeQueryScalar("select count(*) from wftransaction where STARTDATETIME IS NOT NULL AND ENDDATETIME IS NULL and site_id=(select site_id from wftransaction where sno=" & Request.QueryString("id") & ") and siteversion=" & Request.QueryString("version") & "  and docid=" & ConfigurationManager.AppSettings("BASTID")) '1032
                            If t <> 0 Then
                                Dim swid As Integer = 0
                                swid = objdb.ExeQueryScalar("select top 1 sw_id from sitedoc where siteid=( select site_id from wftransaction where sno=" & Request.QueryString("id") & ") and version=" & Request.QueryString("version") & "  and docid=" & ConfigurationManager.AppSettings("WCTRBASTID")) '1034
                                objdb.ExeNonQuery("update odwctrbast set bastsubdate=getdate() where swid= " & swid)
                                'to update to be approved record status as 4 inorder to avoid approval from dashboard. 
                                objdb.ExeNonQuery("update wftransaction set rstatus=4 where tsk_id in (4,5)  and site_id = (select site_id from wftransaction where sno=" & Request.QueryString("id") & ") and siteversion= " & Request.QueryString("version") & " and enddatetime is null  and docid=" & ConfigurationManager.AppSettings("BASTID")) '1032
                                objdb.ExeNonQuery("update sitedoc set rstatus=2 where siteid = (select site_id from wftransaction where sno=" & Request.QueryString("id") & ") and version= " & Request.QueryString("version") & " and docid=" & ConfigurationManager.AppSettings("WCTRBASTID")) '1034
                                ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript123", "gopage(" & swid & ");", True)
                            End If
                        End If
                        'this is to update back the rstatus=2
                        If InStr(Request.QueryString("docname"), "WCTR BAST") > 0 Then
                            Dim y As Integer = 0
                            y = objdb.ExeQueryScalar("select count(*) from wftransaction where STARTDATETIME IS NOT NULL AND ENDDATETIME IS NULL and site_id=(select site_id from wftransaction where sno=" & Request.QueryString("id") & ") and siteversion=" & Request.QueryString("version") & " and docid=" & ConfigurationManager.AppSettings("WCTRBASTID")) '1034
                            If y = 0 Then objdb.ExeNonQuery("update wftransaction set rstatus=2 where tsk_id in (4,5)  and site_id = (select site_id from wftransaction where sno=" & Request.QueryString("id") & ") and siteversion= " & Request.QueryString("version") & " and enddatetime is null  and docid=" & ConfigurationManager.AppSettings("BASTID")) '1032
                        End If
                        'new code irvan vickrizal 24112011
                        Dim userType As String = CommonSite.UserType.ToLower()
                        Dim dtrAtt As New DataTable
                        dtrAtt = objutil.ExeQueryDT("select site_id,siteversion, docid, tsk_id from wftransaction WHERE SNO = " & Convert.ToInt32(Request.QueryString("id")) & " ", "ddddd")
                        siteid = dtrAtt.Rows(0).Item(0).ToString
                        siteversion = dtrAtt.Rows(0).Item(1).ToString
                        docid = Integer.Parse(dtrAtt.Rows(0).Item(2))

                        Dim tskid As Integer = Integer.Parse(dtrAtt.Rows(0).Item(3))
                        If (CommonSite.UserType.ToLower() = "n") Then
                            If (docid = ConfigurationManager.AppSettings("BASTID") Or docid = ConfigurationManager.AppSettings("BOQDOCID") Or docid = ConfigurationManager.AppSettings("WCTRBASTID")) Then
                                If (tskid = 3) Then
                                    dbutils_nsn.CheckGroupingDocWillBeReviewed(siteid, siteversion, ConfigurationManager.AppSettings("BASTID"), ConfigurationManager.AppSettings("WCTRBASTID"), ConfigurationManager.AppSettings("BOQDOCID"))
                                End If
                            End If
                        End If

                        'irvan new code Februari 21022012--------------------Sending Baut Approved

                        If (CommonSite.UserType.ToLower() = "c" And docid = ConfigurationManager.AppSettings("BAUTID") And tskid = 4) Then
                            If Not String.IsNullOrEmpty(Request.QueryString("wpid")) Then
                                Try
                                    dbutils_nsn.AddBastMaster(siteid, siteversion, Integer.Parse(ConfigurationManager.AppSettings("bastteamrole")))
                                    'SendMailBAUTApproved(Request.QueryString("wpid"), siteid, "TI2G")
                                Catch ex As Exception
                                    ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "WindowsCloseApprover();", True)
                                    Exit Sub
                                End Try
                            End If
                        End If
                        '-----------------------------------------------------
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
                objutil.ExeNonQuery("Exec uspBAUTDocReject " & sw_id & ",'" & remarks & "'," & usrid & "," & roleid & ",'" & usrname & "','" & pono & "','" & siteid & "'," & docid & "," & ConfigurationManager.AppSettings("WCTRBASTID") & "," & ConfigurationManager.AppSettings("BAUTID") & "," & ConfigurationManager.AppSettings("BASTID"))
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
                objutil.ExeNonQuery("Exec uspBAUTDocReject " & sw_id & ",'" & remarks & "'," & usrid & "," & roleid & ",'" & usrname & "','" & pono & "','" & siteid & "'," & docid & "," & ConfigurationManager.AppSettings("WCTRBASTID") & "," & ConfigurationManager.AppSettings("BAUTID") & "," & ConfigurationManager.AppSettings("BASTID"))
                count = count + 1
            Next
        End If
        Dim scripts As String = ""
        If count = 0 Then
            scripts = "alert('No document Selected for Reject');"
            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "rejected", scripts, True)
        Else
            scripts = "alert('Document rejected successfully');WindowRejectClose();"
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
            'Dim url As String
            'url = "../PO/frmViewDocument.aspx?id=" & e.Row.Cells(4).Text
            Dim LblDocId As Label = CType(e.Row.FindControl("LblDocId"), Label)
            Dim url As String = String.Empty
            If LblDocId.Text = ConfigurationManager.AppSettings("ATP") Then
                url = "../PO/frmViewDocumentATP.aspx?id=" & e.Row.Cells(4).Text
            Else
                url = "../PO/frmViewDocument.aspx?id=" & e.Row.Cells(4).Text
            End If
            If e.Row.Cells(2).Text <> "" And e.Row.Cells(2).Text <> "&nbsp;" Then
                'e.Row.Cells(3).Text = "<a href='#' onclick=""window.open('" & url & "','mywindow','status=yes,menubar=no,scrollbars=yes,resizable=yes,width=700px,height=600px')"">" & e.Row.Cells(3).Text & "</a>"
                e.Row.Cells(3).Text = "<a href='" & url & "' TARGET='_blank'>" & e.Row.Cells(3).Text & "</a>"
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
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        Dim npwd As String = ""
        Dim msgdata As New StringBuilder
        If txtUserName.Text <> "" Then
            objsms.requestSMS(Session("User_Name"), Session("User_Login"), Request.QueryString("siteno"), Request.QueryString("pono"), Request.QueryString("docname"))
            loadingdiv.Style("display") = "none"
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
            txtUserName.Visible = True
            txtPassword.Enabled = True
            txtPassword.Visible = True
            lnkrequest.Enabled = True
            lnkrequest.Visible = True
            BtnSign.Enabled = True
            BtnSign.Visible = True
            btnReject.Enabled = False
            btnReject.Visible = False
            btnreview.Enabled = True
            btnreview.Visible = True
            btnrejectreview.Enabled = False
            btnrejectreview.Visible = False
        Else 'when reject
            txt1.Visible = True
            txtUserName.Enabled = False
            txtUserName.Visible = False
            txtPassword.Enabled = False
            txtPassword.Visible = False
            lnkrequest.Enabled = False
            lnkrequest.Visible = False
            BtnSign.Enabled = False
            BtnSign.Visible = False
            btnReject.Enabled = True
            btnReject.Visible = True
            btnreview.Enabled = False
            btnreview.Visible = False
            btnrejectreview.Enabled = True
            btnrejectreview.Visible = True
        End If
    End Sub
    Protected Sub grddocuments2_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        Select Case e.Row.RowType
            Case DataControlRowType.DataRow
                Dim lbl As Label = CType(e.Row.FindControl("lblnotwo"), Label)
                lbl.Text = (grddocuments2.PageIndex * grddocuments2.PageSize) + (e.Row.RowIndex + 1) & ".&nbsp;"
        End Select
        If e.Row.RowType = DataControlRowType.DataRow Then
            'Dim url As String
            'url = "../PO/frmViewDocument.aspx?id=" & e.Row.Cells(4).Text
            Dim LblDocId As Label = CType(e.Row.FindControl("LblDocId"), Label)
            Dim url As String = String.Empty
            If LblDocId.Text = ConfigurationManager.AppSettings("ATP") Then
                url = "../PO/frmViewDocumentATP.aspx?id=" & e.Row.Cells(4).Text
            Else
                url = "../PO/frmViewDocument.aspx?id=" & e.Row.Cells(4).Text
            End If
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

        Dim strquery As String = String.Empty
        Dim docid, docidparent, transCnt As Integer
        strquery = "select docid, site_id, siteversion, tsk_id from wftransaction where sno=" & Convert.ToInt32(Request.QueryString("id"))
        Dim siteatt As DataTable = objutil.ExeQueryDT(strquery, "ddd")
        docid = siteatt.Rows(0).Item(0)
        docidparent = objutil.ExeQueryScalar("select distinct parent_id from coddoc where doc_id in (" & docid & ")")
        'bugfix100805 to check and fix the xy coordinate

        Dim i As Integer
        'i = objBo.uspDocApproved(Convert.ToInt32(Request.QueryString("id")), CommonSite.UserId(), CommonSite.UserName(), CommonSite.RollId())
        If docid = ConfigurationManager.AppSettings("ATP") Then
            i = objutil.ExeQueryScalar("Exec [uspDocApprovedATP] " & Convert.ToInt32(Request.QueryString("id")) & "," & CommonSite.UserId() & ",'" & CommonSite.UserName() & "'," & CommonSite.RollId() & "," & ConfigurationManager.AppSettings("BAUTID") & "," & ConfigurationManager.AppSettings("BASTID") & "," & ConfigurationManager.AppSettings("BAST2ID") & "," & Request.QueryString("wpid"))
        Else
            objutil.ExeQuery("Exec [checkXYCoordinate]")
            i = objutil.ExeQueryScalar("Exec [uspDocApproved] " & Convert.ToInt32(Request.QueryString("id")) & "," & CommonSite.UserId() & ",'" & CommonSite.UserName() & "'," & CommonSite.RollId() & "," & ConfigurationManager.AppSettings("BAUTID") & "," & ConfigurationManager.AppSettings("BASTID") & "," & ConfigurationManager.AppSettings("BAST2ID") & "")
        End If

        If i = 0 Then
            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('Error while doing the transaction.');", True)
        ElseIf i = 1 Then
            ButtonDisabled()
            If Not ConfigurationManager.AppSettings("ATP") = docid.ToString Or ConfigurationManager.AppSettings("ATP") = docidparent.ToString Then
                dbutils_nsn.UpdateDocBAST(siteatt.Rows(0).Item(1), siteatt.Rows(0).Item(2), ConfigurationManager.AppSettings("BASTID"), ConfigurationManager.AppSettings("WCTRBASTID"), ConfigurationManager.AppSettings("BOQDOCID"))
            End If

            'ATP Approval Full Approval
            If ConfigurationManager.AppSettings("ATP") = docid.ToString Or ConfigurationManager.AppSettings("ATP") = docidparent.ToString Then
                InsertAuditApproveWithRemarks(Convert.ToInt32(Request.QueryString("id")), docid, Integer.Parse(siteatt.Rows(0).Item(3).ToString()), 2, CommonSite.UserId, CommonSite.RollId, Request.QueryString("wpid"), "")
                'strsql = "select distinct 'User : ' + name + ' ( ' + case usrtype when 'C' then 'Telkomsel'when 'N' then 'NSN'else 'NSN'end + ' )' userid ,convert(varchar(max),enddatetime,113) enddate, tsk_id, w.sno from wftransaction  W" & _
                '" inner join ebastusers_1 E on e.usr_id=w.LMBY " & _
                '" where site_id=(select site_id from wftransaction where sno= " & Convert.ToInt32(Request.QueryString("id")) & ") " & _
                '" and siteversion=(select siteversion from wftransaction where sno= " & Convert.ToInt32(Request.QueryString("id")) & ")" & _
                '" and docid=(select docid from wftransaction where sno= " & Convert.ToInt32(Request.QueryString("id")) & ") and enddatetime is not null and tsk_id in(5,6)" & _
                '" order by w.sno asc"

                '---- New Code irvan V
                'strsql = "select distinct case usrtype when 'C' then 'Telkomsel'when 'N' then 'NSN'else 'NSN'end + ' 1st Approval by '+ name +', '+ SignTitle userid,enddatetime, tsk_id, w.sno from wftransaction  W" & _
                '                  " inner join ebastusers_1 E on e.usr_id=(select top 1 userid from audittrail where siteid = " & siteid & "and task=1 and version=" & siteversion & " order by eventendtime desc) inner join trole tr on e.usrRole = tr.roleid " & _
                '                  " where site_id=(select site_id from wftransaction where sno= " & Convert.ToInt32(Request.QueryString("id")) & ") " & _
                '                  " and siteversion=(select siteversion from wftransaction where sno= " & Convert.ToInt32(Request.QueryString("id")) & ")" & _
                '                  " and docid=(select docid from wftransaction where sno= " & Convert.ToInt32(Request.QueryString("id")) & ") " & _
                '                  " and enddatetime is not null and tsk_id = 1 " & _
                '                  " order by w.sno asc"
                ''--------------------------------------------------
                'Dim userapprovalfirst As DataTable = objutil.ExeQueryDT(strsql, "ddd")
                'strsql = String.Empty

                strsql = "select distinct case usrtype when 'C' then 'Telkomsel'when 'N' then 'NSN'else 'Partner'end usertype,name,SignTitle,enddatetime, tsk_id, w.sno from wftransaction  W" & _
                " inner join ebastusers_1 E on e.usr_id=w.LMBY and w.docid = 2001 inner join trole tr on e.usrRole = tr.roleid " & _
                " where site_id=(select site_id from wftransaction where sno= " & Convert.ToInt32(Request.QueryString("id")) & ") " & _
                " and siteversion=(select siteversion from wftransaction where sno= " & Convert.ToInt32(Request.QueryString("id")) & ")" & _
                " and docid=(select docid from wftransaction where sno= " & Convert.ToInt32(Request.QueryString("id")) & ")" & _
                " and w.status = 0 and w.rstatus = 2 order by w.sno asc"
                userdt = objutil.ExeQueryDT(strsql, "ddd")
                Dim kt As Integer
                If userdt.Rows.Count <> 0 Then
                    dvPrint.Visible = True
                    divRev = "" 'reset
                    Dim dates As String = ""
                    'divRev += userprep.Rows(0).Item(0).ToString & " On " & String.Format("{0:dd MMM yyyy hh:mm}", userprep.Rows(0).Item(1)) & "<br/>"
                    For kt = 0 To userdt.Rows.Count - 1
                        'If userdt.Rows(kt).Item(2).ToString = "1" Then
                        '    divRev += "Prepared by " & userdt.Rows(kt).Item(0).ToString & " On " & userdt.Rows(kt).Item(1).ToString.Split(" ")(0) & " " & userdt.Rows(kt).Item(1).ToString.Split(" ")(1) & " " & userdt.Rows(kt).Item(1).ToString.Split(" ")(2) & " " & "<br>"
                        'Else
                        '    divRev += "Approved by " & userdt.Rows(kt).Item(0).ToString & " On " & userdt.Rows(kt).Item(1).ToString.Split(" ")(0) & " " & userdt.Rows(kt).Item(1).ToString.Split(" ")(1) & " " & userdt.Rows(kt).Item(1).ToString.Split(" ")(2) & " " & "<br>"
                        'End If

                        'new code Irvan V
                        Dim lvlApproval As String = String.Empty
                        If (userdt.Rows(kt).Item(4).ToString = "1") Then
                            lvlApproval = userdt.Rows(kt).Item(0).ToString & " Prepared by "
                        Else
                            lvlApproval = userdt.Rows(kt).Item(0).ToString & " Approved by "
                        End If
                        divRev += lvlApproval & userdt.Rows(kt).Item(1).ToString & ", " & userdt.Rows(kt).Item(2).ToString & " On " & String.Format("{0:dd MMM yyyy}", userdt.Rows(kt).Item(3)) & " " & "<br />"
                    Next

                    divReviewer.InnerHtml = divRev
                    divPONO.InnerHtml = Request.QueryString("pono")
                    'strsql = "select site_name from codsite where site_no = '" & Request.QueryString("siteno") & "'"
                    strsql = "select top 1(SiteName) site_name from epmdata where workpackageid='" & Request.QueryString("wpid") & "' order by lmdt desc"
                    userdt2 = objutil.ExeQueryDT(strsql, "ddd")
                    If userdt.Rows.Count <> 0 Then
                        divSiteName.InnerHtml = userdt2.Rows(0).Item(0).ToString()
                    End If
                    divSiteID.InnerHtml = Request.QueryString("siteno")
                    strsql = "select top 1 pd.fldtype from wftransaction W" & _
                    " inner join podetails pd on pd.siteno=(select site_no from codsite where site_id=(select site_id from wftransaction where sno= " & Convert.ToInt32(Request.QueryString("id")) & ")) " & _
                    "   and pd.siteversion=(select siteversion from wftransaction where sno= " & Convert.ToInt32(Request.QueryString("id")) & ") " & _
                    " where w.site_id=(select site_id from wftransaction where sno= " & Convert.ToInt32(Request.QueryString("id")) & ") " & _
                    " and w.siteversion=(select siteversion from wftransaction where sno= " & Convert.ToInt32(Request.QueryString("id")) & ")" & _
                    " and w.docid=(select docid from wftransaction where sno= " & Convert.ToInt32(Request.QueryString("id")) & ") and enddatetime is not null"
                    divScope.InnerHtml = objutil.ExeQueryScalarString(strsql)
                End If
                'strsql = "select count(*) from wftransaction W" & _
                '" where site_id=(select site_id from wftransaction where sno= " & Convert.ToInt32(Request.QueryString("id")) & ") " & _
                '" and siteversion=(select siteversion from wftransaction where sno= " & Convert.ToInt32(Request.QueryString("id")) & ")" & _
                '" and docid=(select docid from wftransaction where sno= " & Convert.ToInt32(Request.QueryString("id")) & ") and enddatetime is null"

                Dim transCntATPOnsite As Integer = 0
                strsql = "select count(*) from wftransaction " & _
                " where site_id=" & siteid & " and siteversion=" & siteversion & " and docid=" & ConfigurationManager.AppSettings("ATP") & " and enddatetime is null"
                transCnt = objutil.ExeQueryScalar(strsql)

                If transCnt = 0 Then
                    strsql = "select count(*) from wftransaction where status=1 and rstatus=5 and docid=" & ConfigurationManager.AppSettings("ATP") & _
                    " and site_id =" & siteid & " and siteversion=" & siteversion
                    transCntATPOnsite = objutil.ExeQueryScalar(strsql)
                End If

                ' --------- custom coding for sendmail -----------
                Dim strQuery2 As String = String.Empty
                strQuery2 = "select site_id, siteversion from wftransaction where sno= " & Convert.ToInt32(Request.QueryString("id"))
                Dim userdt5 As DataTable = objutil.ExeQueryDT(strQuery2, "ddd")
                strQuery2 = String.Empty
                Dim sendmail_userid As Integer = GetUserByATPFlow(userdt5.Rows(0).Item(0), userdt5.Rows(0).Item(1), ConfigurationManager.AppSettings("ATP"))
                If (sendmail_userid > 0) Then
                    sendmailATP(Convert.ToInt32(userdt5.Rows(0).Item(0)), userdt5.Rows(0).Item(1), sendmail_userid, False, "", "")
                End If
                '-------------------------------------------------
                If transCnt = 0 And transCntATPOnsite = 0 Then
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
                    'if 
                    'newdocpath = Common.TakeFileUpload.ConvertAnyFormatToPDFHtmlNew1(strPath & ReFileName, strPath, filenameorg, ConfigurationManager.AppSettings("Fpath") & HDPath.Value)
                    'newdocpath = final & newdocpath
                    'objutil.ExeNonQuery("update sitedoc set docpath='" & newdocpath & _
                    '"' where docid=(SELECT DOCID FROM WFTRANSACTION WHERE SNO=" & Convert.ToInt32(Request.QueryString("id")) & _
                    '") AND SITEID=(SELECT SITE_ID FROM WFTRANSACTION WHERE SNO=" & Convert.ToInt32(Request.QueryString("id")) & _
                    '") AND VERSION=(SELECT SITEVERSION FROM WFTRANSACTION WHERE SNO=" & Convert.ToInt32(Request.QueryString("id")) & ")")
                    '------ Change for Improvement ------'
                    'newdocpath = Common.TakeFileUpload.ConvertAnyFormatToPDFHtmlNew(strPath & ReFileName, strPath, filenameorg)
                    'newdocpath = final & newdocpath
                    newdocpath = String.Empty

                    'new code irvan vickrizal -- Buffering Doc Generate Pending
                    Dim info As New ATPPipelineInfo
                    info.Status = "Pending"
                    info.PathFolder = strPath
                    info.PackageId = Request.QueryString("wpid")
                    info.OriginalFilename = filenameorg
                    info.OriginalPath = strPath & ReFileName
                    info.TaskPendingId = 0
                    Dim controller As New ATPPipelineController
                    controller.ATPPipelineInsert(info)
                    '---------------------------------

                    objutil.ExeNonQuery("update sitedoc set docpath='" & newdocpath & _
                    "' where docid=" & siteatt.Rows(0).Item(0) & _
                    "  AND SITEID=" & Convert.ToInt32(siteatt.Rows(0).Item(1)) & _
                    " AND VERSION=" & siteatt.Rows(0).Item(2))
                    objutil.ExeNonQuery("exec uspInsertATPDocument " & Convert.ToInt32(siteatt.Rows(0).Item(1)) & ", " & ConfigurationManager.AppSettings("ATPDoc") & _
                    ", '" & HDPath.Value & "', " & siteatt.Rows(0).Item(2) & ", " & ConfigurationManager.AppSettings("ATP"))

                    SubmitAllApproved(False, String.Empty)
                End If
            End If
            'ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "WindowsCloseReviewer();", True)
            If (HDDocid.Value = ConfigurationManager.AppSettings("ATP")) Then
                If CommonSite.UserType = "C" Then
                    ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "windowsCloseATPReviewer();", True)
                Else
                    ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "WindowsCloseReviewer();", True)
                End If

            Else
                ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "WindowsCloseReviewer();", True)
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
                objutil.ExeNonQuery("Exec uspBAUTDocReject " & sw_id & ",'" & remarks & "'," & usrid & "," & roleid & ",'" & usrname & "','" & pono & "','" & siteid & "'," & docid & "," & ConfigurationManager.AppSettings("WCTRBASTID") & "," & ConfigurationManager.AppSettings("BAUTID") & "," & ConfigurationManager.AppSettings("BASTID"))
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
                objutil.ExeNonQuery("Exec uspBAUTDocReject " & sw_id & ",'" & remarks & "'," & usrid & "," & roleid & ",'" & usrname & "','" & pono & "','" & siteid & "'," & docid & "," & ConfigurationManager.AppSettings("WCTRBASTID") & "," & ConfigurationManager.AppSettings("BAUTID") & "," & ConfigurationManager.AppSettings("BASTID"))
                count = count + 1
            Next
        End If
        Dim scripts As String = ""
        If count = 0 Then
            scripts = "alert('No document Selected for Reject');"
            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "rejected", scripts, True)
        Else
            scripts = "alert('Document rejected successfully');WindowRejectClose();"
            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "rejected", scripts, True)
        End If
    End Sub
    Protected Sub BtnATPOnSiteClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnATPOnSite.Click
        If (Request.QueryString("wpid") IsNot Nothing) Then
            SubmitOnSite(Request.QueryString("wpid"))
        End If
    End Sub

    Protected Sub BtnApproveWithRemarksClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnApproveWithRemarks.Click
        MvApprovalButtonPanel.SetActiveView(vwApproveWithRemarks)
    End Sub

    Protected Sub BtnSubmitApproveWithRemarksClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSubmitApproveWithRemarks.Click
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")

        Dim strquery As String = String.Empty
        Dim docid, docidparent, transCnt As Integer
        strquery = "select docid, site_id, siteversion, tsk_id from wftransaction where sno=" & Convert.ToInt32(Request.QueryString("id"))
        Dim siteatt As DataTable = objutil.ExeQueryDT(strquery, "ddd")
        docid = siteatt.Rows(0).Item(0)
        docidparent = objutil.ExeQueryScalar("select distinct parent_id from coddoc where doc_id in (" & docid & ")")
        'bugfix100805 to check and fix the xy coordinate

        Dim i As Integer
        'i = objBo.uspDocApproved(Convert.ToInt32(Request.QueryString("id")), CommonSite.UserId(), CommonSite.UserName(), CommonSite.RollId())
        If docid = ConfigurationManager.AppSettings("ATP") Then
            i = objutil.ExeQueryScalar("Exec [uspDocApprovedWithRemarksATP] " & Convert.ToInt32(Request.QueryString("id")) & "," & CommonSite.UserId() & ",'" & CommonSite.UserName() & "'," & CommonSite.RollId() & "," & ConfigurationManager.AppSettings("BAUTID") & "," & ConfigurationManager.AppSettings("BASTID") & "," & ConfigurationManager.AppSettings("BAST2ID") & "," & Request.QueryString("wpid"))
        Else
            objutil.ExeQuery("Exec [checkXYCoordinate]")
            i = objutil.ExeQueryScalar("Exec [uspDocApproved] " & Convert.ToInt32(Request.QueryString("id")) & "," & CommonSite.UserId() & ",'" & CommonSite.UserName() & "'," & CommonSite.RollId() & "," & ConfigurationManager.AppSettings("BAUTID") & "," & ConfigurationManager.AppSettings("BASTID") & "," & ConfigurationManager.AppSettings("BAST2ID") & "")
        End If

        If i = 0 Then
            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('Error while doing the transaction.');", True)
        ElseIf i = 1 Then
            ButtonDisabled()
            'ATP Approval Full Approval
            InsertAuditApproveWithRemarks(Convert.ToInt32(Request.QueryString("id")), docid, Integer.Parse(siteatt.Rows(0).Item(3).ToString()), 2, CommonSite.UserId, CommonSite.RollId, Request.QueryString("wpid"), TxtApproveWithRemarks.Text)
            If ConfigurationManager.AppSettings("ATP") = docid.ToString Or ConfigurationManager.AppSettings("ATP") = docidparent.ToString Then
                strsql = "select distinct case usrtype when 'C' then 'Telkomsel'when 'N' then 'NSN'else 'Partner'end usertype,name,SignTitle,enddatetime, tsk_id, w.sno from wftransaction  W" & _
                " inner join ebastusers_1 E on e.usr_id=w.LMBY and w.docid = 2001 inner join trole tr on e.usrRole = tr.roleid " & _
                " where site_id=(select site_id from wftransaction where sno= " & Convert.ToInt32(Request.QueryString("id")) & ") " & _
                " and siteversion=(select siteversion from wftransaction where sno= " & Convert.ToInt32(Request.QueryString("id")) & ")" & _
                " and docid=(select docid from wftransaction where sno= " & Convert.ToInt32(Request.QueryString("id")) & ") and enddatetime is not null and tsk_id in(1,5,6)" & _
                " and w.status = 0 and w.rstatus = 2 order by w.sno asc"
                userdt = objutil.ExeQueryDT(strsql, "ddd")

                Dim kt As Integer
                If userdt.Rows.Count <> 0 Then
                    dvPrint.Visible = True
                    divRev = "" 'reset
                    Dim dates As String = ""
                    For kt = 0 To userdt.Rows.Count - 1
                        'new code Irvan V
                        Dim lvlApproval As String = String.Empty
                        If (userdt.Rows(kt).Item(4).ToString = "1") Then
                            lvlApproval = userdt.Rows(kt).Item(0).ToString & " Prepared by "
                        Else
                            lvlApproval = userdt.Rows(kt).Item(0).ToString & " Approved by "
                        End If
                        divRev += lvlApproval & userdt.Rows(kt).Item(1).ToString & ", " & userdt.Rows(kt).Item(2).ToString & " On " & String.Format("{0:dd MMM yyyy}", userdt.Rows(kt).Item(3)) & " " & "<br />"
                    Next

                    divReviewer.InnerHtml = divRev
                    divPONO.InnerHtml = Request.QueryString("pono")
                    strsql = "select sitename from epmdata where workpackageid = '" & Request.QueryString("wpid") & "'"
                    userdt2 = objutil.ExeQueryDT(strsql, "ddd")
                    If userdt.Rows.Count <> 0 Then
                        divSiteName.InnerHtml = userdt2.Rows(0).Item(0).ToString()
                    End If
                    divSiteID.InnerHtml = Request.QueryString("siteno")
                    strsql = "select top 1 pd.fldtype from wftransaction W" & _
                    " inner join podetails pd on pd.siteno=(select site_no from codsite where site_id=(select site_id from wftransaction where sno= " & Convert.ToInt32(Request.QueryString("id")) & ")) " & _
                    "   and pd.siteversion=(select siteversion from wftransaction where sno= " & Convert.ToInt32(Request.QueryString("id")) & ") " & _
                    " where w.site_id=(select site_id from wftransaction where sno= " & Convert.ToInt32(Request.QueryString("id")) & ") " & _
                    " and w.siteversion=(select siteversion from wftransaction where sno= " & Convert.ToInt32(Request.QueryString("id")) & ")" & _
                    " and w.docid=(select docid from wftransaction where sno= " & Convert.ToInt32(Request.QueryString("id")) & ") and enddatetime is not null"
                    divScope.InnerHtml = objutil.ExeQueryScalarString(strsql)
                End If

                Dim transCntATPOnsite As Integer = 0
                strsql = "select count(*) from wftransaction " & _
                " where site_id=" & siteid & " and siteversion=" & siteversion & " and docid=" & ConfigurationManager.AppSettings("ATP") & " and enddatetime is null"
                transCnt = objutil.ExeQueryScalar(strsql)

                If transCnt = 0 Then
                    strsql = "select count(*) from wftransaction where status=1 and rstatus=5 and docid=" & ConfigurationManager.AppSettings("ATP") & _
                    " and site_id =" & siteid & " and siteversion=" & siteversion
                    transCntATPOnsite = objutil.ExeQueryScalar(strsql)
                End If

                ' --------- custom coding for sendmail -----------
                Dim strQuery2 As String = String.Empty
                strQuery2 = "select site_id, siteversion from wftransaction where sno= " & Convert.ToInt32(Request.QueryString("id"))
                Dim userdt5 As DataTable = objutil.ExeQueryDT(strQuery2, "ddd")
                strQuery2 = String.Empty
                Dim sendmail_userid As Integer = GetUserByATPFlow(userdt5.Rows(0).Item(0), userdt5.Rows(0).Item(1), ConfigurationManager.AppSettings("ATP"))
                If (sendmail_userid > 0) Then
                    sendmailATP(Convert.ToInt32(userdt5.Rows(0).Item(0)), userdt5.Rows(0).Item(1), sendmail_userid, False, "", "")
                End If
                '-------------------------------------------------
                If transCnt = 0 And transCntATPOnsite = 0 Then
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
                    'if 
                    'newdocpath = Common.TakeFileUpload.ConvertAnyFormatToPDFHtmlNew1(strPath & ReFileName, strPath, filenameorg, ConfigurationManager.AppSettings("Fpath") & HDPath.Value)
                    'newdocpath = final & newdocpath
                    'objutil.ExeNonQuery("update sitedoc set docpath='" & newdocpath & _
                    '"' where docid=(SELECT DOCID FROM WFTRANSACTION WHERE SNO=" & Convert.ToInt32(Request.QueryString("id")) & _
                    '") AND SITEID=(SELECT SITE_ID FROM WFTRANSACTION WHERE SNO=" & Convert.ToInt32(Request.QueryString("id")) & _
                    '") AND VERSION=(SELECT SITEVERSION FROM WFTRANSACTION WHERE SNO=" & Convert.ToInt32(Request.QueryString("id")) & ")")
                    newdocpath = Common.TakeFileUpload.ConvertAnyFormatToPDFHtmlNew(strPath & ReFileName, strPath, filenameorg)
                    newdocpath = final & newdocpath
                    objutil.ExeNonQuery("update sitedoc set docpath='" & newdocpath & _
                    "' where docid=" & siteatt.Rows(0).Item(0) & _
                    "  AND SITEID=" & Convert.ToInt32(siteatt.Rows(0).Item(1)) & _
                    " AND VERSION=" & siteatt.Rows(0).Item(2))
                    objutil.ExeNonQuery("exec uspInsertATPDocument " & Convert.ToInt32(siteatt.Rows(0).Item(1)) & ", " & ConfigurationManager.AppSettings("ATPDoc") & _
                    ", '" & HDPath.Value & "', " & siteatt.Rows(0).Item(2) & ", " & ConfigurationManager.AppSettings("ATP"))
                    SubmitAllApproved(True, TxtApproveWithRemarks.Text)
                End If
            End If
            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "windowsCloseATPReviewer();", True)
        End If
    End Sub

    Protected Sub BtnCancelApproveWithRemarksClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnCancelApproveWithRemarks.Click
        TxtApproveWithRemarks.Text = ""
        MvApprovalButtonPanel.SetActiveView(vwDefaultPanel)
    End Sub

#Region "Improvement Code -- Irvan Vickrizal"
    Private Sub SubmitOnSite(ByVal wpid As String)
        If (wpid <> "") Then
            ButtonDisabled()
            Dim atpinfo As New ATPOnSiteInfo
            atpinfo.PackageId = wpid
            atpinfo.DocumentStatus = Constants_NSN._Doc_Status_Pending
            atpinfo.PendingDate = DateTime.Now
            atpinfo.ExePendingUser = CommonSite.UserId
            atpinfo.Remarks = ""
            dbutils_nsn.InsertNewATPOnsite(atpinfo)
            dbutils_nsn.UpdateTransactionATPOnSite(Convert.ToInt64(Request.QueryString("id")), CommonSite.UserId, wpid, CommonSite.RollId)
            Dim auditinfo As New AuditInfo
            auditinfo.DocId = ConfigurationManager.AppSettings("ATP")
            auditinfo.PackageId = wpid
            auditinfo.SiteId = siteid
            auditinfo.Status = 1
            auditinfo.pono = Request.QueryString("pono")
            auditinfo.Remarks = ""
            auditinfo.Category = ""
            auditinfo.Task = ConfigurationManager.AppSettings("ATPOnSiteRequest")
            auditinfo.UserId = CommonSite.UserId
            auditinfo.RoleId = CommonSite.RollId
            dbutils_nsn.InsertAuditTrailATPOnSite(auditinfo)
            SendMailOnSiteRequest(auditinfo)
            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "WindowATPOnSiteClose();", True)
        End If
    End Sub

    Private Sub SendMailOnSiteRequest(ByVal auditinfo As AuditInfo)
        Dim sbBody As New StringBuilder
        sbBody.Append("Dear NSN-Regional Team, <br/>Referring Customer requested by " & CommonSite.UserName & " to performing On-site ATP for " & Request.QueryString("siteno") & "-" & divSiteName.InnerText & "-" & Request.QueryString("pono") & "<br/><br/>")
        sbBody.Append("Please find in your task ATP on Site Request pending as follow up customer request <br/>")
        sbBody.Append("warm regards<br/> eBAST-Admin")
        sbBody.Append("<img src='http://www.telkomsel.nsnebast.com/images/nsn-logo.gif' alt='companylogo' />")
        Dim notif As New NotificationBase
        notif.SendMailATP(dbutils_nsn.GetATPReviewByRole(ConfigurationManager.AppSettings("ATPROLEAPPREV"), auditinfo.SiteId, siteversion, ConfigurationManager.AppSettings("ATP")), sbBody.ToString(), "ATP ON-Site Request")
    End Sub
    Sub BindReasons()
        MvApprovalButtonPanel.SetActiveView(vwRejectPanel)
        CbList.DataSource = dbutils_nsn.GetReasons(ConfigurationManager.AppSettings("ATP"))
        CbList.DataTextField = "ReasonName"
        CbList.DataValueField = "ReasonId"
        CbList.DataBind()
    End Sub

    Protected Sub BtnRejectReviewNewClick(ByVal Sender As Object, ByVal e As System.EventArgs) Handles BtnRejectReviewNew.Click
        BindReasons()
    End Sub

    Protected Sub BtnCancelSubmitClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnCancelSubmit.Click
        MvApprovalButtonPanel.SetActiveView(vwDefaultPanel)
        TxtRemarksReject.Text = ""
    End Sub

    Protected Sub BtnSubmitRejectClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSubmitReject.Click
        SubmitReject()
    End Sub

    Private Sub SubmitReject()
        Dim wpid As String = Request.QueryString("wpid")
        'strsql = "select cs.site_id, po.siteversion from podetails po inner join codsite cs on cs.site_no = po.siteno  where workpkgid='" + wpid + "'"
        'Dim sitedesc As DataTable = objutil.ExeQueryDT(strsql, "ddd")
        Dim categories As String = String.Empty
        Dim MyItem As ListItem
        Dim countThick As Integer = 0
        For Each MyItem In CbList.Items
            If MyItem.Selected = True Then
                If countThick = 1 Then
                    categories = categories & ", "
                End If
                categories = categories & MyItem.Text
                countThick = 1
            End If
        Next
        If (wpid IsNot Nothing) Then
            Dim GrpId As String = Request.QueryString("id")
            Dim i As Integer = -1
            i = dbutils_nsn.DocReject(GrpId, wpid, CommonSite.UserId, CommonSite.UserName, CommonSite.RollId, TxtRemarksReject.Text, ConfigurationManager.AppSettings("BAUTID"), ConfigurationManager.AppSettings("BASTID"), ConfigurationManager.AppSettings("WCTRBASTID"), categories)
            If i = 0 Then
                ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('Error while doing the transaction.');", True)
            ElseIf i = 1 Then
                Try
                    strsql = "select usr.usr_id,usr.email,usr.phoneNo,usr.name,wf.site_id,wf.siteversion,wf.tsk_id, usr.usrType, usr.srcid from wftransaction wf " & _
                             "inner join codsite cs on wf.site_id = cs.site_id " & _
                             "inner join podetails po on po.siteno = cs.site_no and wf.siteversion = po.siteversion " & _
                             "inner join ebastusers_1 usr on usr.usr_id = wf.lmby " & _
                             "where po.workpkgid = '" & wpid & "' and docid = " & ConfigurationManager.AppSettings("ATP") & " and wf.tsk_id in (1,5)"
                    Dim usersemail As DataTable = objutil.ExeQueryDT(strsql, "ddd")
                    Dim ki As Integer = 0
                    Dim listusers As New List(Of UserInfo)
                    Dim info As New UserInfo
                    If usersemail.Rows.Count <> 0 Then
                        For ki = 0 To userdt.Rows.Count - 1
                            If usersemail.Rows(ki).Item("usrType") = "S" Then
                                Dim listsubcon As New List(Of UserInfo)
                                listsubcon = dbutils_nsn.GetUserSubcon(Integer.Parse(usersemail.Rows(ki).Item("srcid")))
                                Dim subcon As New UserInfo
                                For Each subcon In listsubcon
                                    Dim subconinfo As New UserInfo
                                    Dim found = listusers.Find(AddressOf New emailmatching(subcon.Email).FindByEmail)
                                    If found Is Nothing Then
                                        subconinfo.UserId = subcon.UserId
                                        subconinfo.Username = subcon.Username
                                        subconinfo.UserType = subcon.UserType
                                        subconinfo.Email = subcon.Email
                                        subconinfo.Handphone = subcon.Handphone
                                        listusers.Add(subconinfo)
                                    End If
                                Next
                            Else
                                info.UserId = usersemail.Rows(ki).Item("usr_id")
                                info.Username = usersemail.Rows(ki).Item("name")
                                info.UserType = usersemail.Rows(ki).Item("usrType")
                                info.Email = usersemail.Rows(ki).Item("email")
                                info.Handphone = usersemail.Rows(ki).Item("phoneNo")
                                listusers.Add(info)
                                'sendmailATP(usersemail.Rows(ki).Item(4), usersemail.Rows(ki).Item(5), usersemail.Rows(ki).Item(0), True, categories, TxtRemarksReject.Text)
                            End If
                        Next
                    End If
                    SendEmailRejection(listusers, TxtRemarksReject.Text, categories)
                    'Dim ki As Integer = 0
                    'If usersemail.Rows.Count <> 0 Then
                    '    For ki = 0 To userdt.Rows.Count - 1
                    '        sendmailATP(usersemail.Rows(ki).Item(4), usersemail.Rows(ki).Item(5), usersemail.Rows(ki).Item(0), True, categories, TxtRemarksReject.Text)
                    '    Next
                    'End If
                    'objmail.sendMailReject(GrpId, CommonSite.UserId(), CommonSite.UserName(), txtRemarks.Text.ToString, ConfigurationManager.AppSettings("rejmailconst"), "Normal")
                Catch ex As Exception
                    'objdb.ExeNonQuery("exec uspErrLog 0, 'mailsendingreject','" & ex.Message.ToString() & "','sendmailreject'")
                End Try
                ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "WindowRejectCloseATP();", True)
            End If
        End If
    End Sub

    Private Sub SubmitAllApproved(ByVal withRemarks As Boolean, ByVal remarks As String)
        Dim wpid As String = Request.QueryString("wpid")
        If (wpid IsNot Nothing) Then
            Try
                strsql = "select usr.usr_id,usr.email,usr.phoneNo,usr.name,wf.site_id,wf.siteversion,wf.tsk_id, usr.usrtype from wftransaction wf " & _
                         "inner join codsite cs on wf.site_id = cs.site_id " & _
                         "inner join podetails po on po.siteno = cs.site_no and wf.siteversion = po.siteversion " & _
                         "inner join ebastusers_1 usr on usr.usr_id = wf.lmby " & _
                         "where po.workpkgid = '" & wpid & "' and docid = " & ConfigurationManager.AppSettings("ATP") & " and wf.tsk_id in (1,5,6)"

                Dim usersemail As DataTable = objutil.ExeQueryDT(strsql, "ddd")
                Dim ki As Integer = 0
                Dim listusers As New List(Of UserInfo)
                Dim siteidmail As Int32 = usersemail.Rows(0).Item(4)
                Dim siteversionmail As Integer = usersemail.Rows(0).Item(5)
                If usersemail.Rows.Count <> 0 Then
                    For ki = 0 To usersemail.Rows.Count - 1
                        'sendmailATP(usersemail.Rows(ki).Item(4), usersemail.Rows(ki).Item(5), usersemail.Rows(ki).Item(0), True, categories, TxtRemarksReject.Text)
                        Dim userinfo As New UserInfo
                        userinfo.Username = usersemail.Rows(ki).Item(3)
                        userinfo.UserType = usersemail.Rows(ki).Item(7)
                        userinfo.UserId = usersemail.Rows(ki).Item(0)
                        userinfo.Handphone = usersemail.Rows(ki).Item(2)
                        userinfo.Email = usersemail.Rows(ki).Item(1)
                        listusers.Add(userinfo)
                    Next
                    'Dim notif As New NotificationBase
                    SendMailATPApproved(listusers, siteidmail, siteversionmail, withRemarks, remarks)
                End If
            Catch ex As Exception
                objdb.ExeNonQuery("exec uspErrLog '', 'mailsendingATPapproved','" & ex.Message.ToString.Replace("'", "''") & "','sendmailATPapproved'")
            End Try
        End If
    End Sub

    Private Sub SendEmailRejection(ByVal listusers As List(Of UserInfo), ByVal remarks As String, ByVal categories As String)
        Dim sb As New StringBuilder
        Dim compRejected As String = String.Empty
        strsql = "select top 1 siteno, sitename, pono, scope, workpackageid from poepmsitenew where siteid=" & siteid & " and siteversion=" & siteversion
        Dim dtSiteatt As DataTable = objutil.ExeQueryDT(strsql, "ddd")
        sb.Append("Dear Sir / Madam, <br/>")
        If CommonSite.UserType = "N" Then
            compRejected = "NSN"
        ElseIf CommonSite.UserType = "C" Then
            compRejected = "Telkomsel"
        Else
            compRejected = "Partner"
        End If
        sb.Append("There is ATP document of " & dbutils_nsn.GetSiteID_PONO(siteid, siteversion) & " has been rejected by " & CommonSite.UserName & "-" & compRejected & "<br/><br />")
        sb.Append("Summary : <br/>Reason :" & categories & "<br/>remarks :" & remarks & "<br/><br/>")
        sb.Append("Powered By EBAST" & "<br/>")
        sb.Append("<img src='http://www.telkomsel.nsnebast.com/images/nsn-logo.gif' alt='companylogo' />")
        Dim notif As New NotificationBase
        'notif.SendMailATP(listusers, sb.ToString(), "Document ATP Rejected")
        objmail.SendMailATP(listusers, sb.ToString(), "Document ATP Rejected")
    End Sub

    Private Sub SendMailATPApproved(ByVal listusers As List(Of UserInfo), ByVal siteid As Int32, ByVal version As Integer, ByVal withRemarks As Boolean, ByVal remarks As String)
        Dim sb As New StringBuilder
        Dim strsubject As String = String.Empty
        Dim compRejected As String = String.Empty
        strsql = "select top 1 siteno, sitename, pono, scope, workpackageid from poepmsitenew where siteid=" & siteid & " and siteversion=" & version
        Dim dtSiteatt As DataTable = objutil.ExeQueryDT(strsql, "ddd")
        Dim strSiteAttribute As String = dtSiteatt.Rows(0).Item(0) & "-" & dtSiteatt.Rows(0).Item(1) & "-" & dtSiteatt.Rows(0).Item(2) & "-" & _
                                         dtSiteatt.Rows(0).Item(3) & "-" & dtSiteatt.Rows(0).Item(4)
        sb.Append("Dear Sir / Madam, <br/>")
        If CommonSite.UserType = "N" Then
            compRejected = "NSN"
        ElseIf CommonSite.UserType = "C" Then
            compRejected = "Telkomsel"
        Else
            compRejected = "Partner"
        End If
        sb.Append("There is ATP document with the following site details : <br/><br/> ")
        sb.Append("siteid : <b>" & dtSiteatt.Rows(0).Item(0).ToString() & "</b><br/>")
        sb.Append("sitename : <b>" & dtSiteatt.Rows(0).Item(1).ToString() & "</b><br/>")
        sb.Append("pono : <b>" & dtSiteatt.Rows(0).Item(2).ToString() & "</b><br/>")
        sb.Append("Scope : <b>" & dtSiteatt.Rows(0).Item(3).ToString() & "</b><br/>")
        sb.Append("WPackageid : <b>" & dtSiteatt.Rows(0).Item(4).ToString() & "</b><br/><br/>")
        If withRemarks = True Then
            sb.Append("has been approved (with Remarks) by " & CommonSite.UserName & "-" & compRejected & " as final approval.<br/><br />")
            sb.Append("Detail of Remarks : " & remarks & "<br/><br/>")
            strsubject = "Document ATP Final Approved (With Remarks)"
        Else
            sb.Append("has been approved by " & CommonSite.UserName & "-" & compRejected & " as final approval.<br/><br />")
            strsubject = "Document ATP Final Approved"
        End If

        sb.Append("Powered By EBAST" & "<br/>")
        sb.Append("<img src='http://www.telkomsel.nsnebast.com/images/nsn-logo.gif' alt='companylogo' />")
        objmail.SendMailATP(listusers, sb.ToString(), strsubject)
    End Sub
#End Region

#Region "Custom Methods"
    Private Class emailmatching
        Private ReadOnly _findEmail As String

        Sub New(ByVal strEmail As String)
            _findEmail = strEmail
        End Sub

        ' a filtering function to pass into List.Find() as a Predicate
        Public Function FindByEmail(ByVal usr As UserInfo) As Boolean
            Return usr.Email = _findEmail
        End Function
    End Class
    'Public Sub sendmailATP(ByVal siteid As Integer, ByVal siteversion As Integer, ByVal userid As Integer, ByVal isRejected As Boolean)
    'Dim strQuery As String = "select email,name,PhoneNo from ebastusers_1 where usr_id =" & userid
    'Dim userdtMail As DataTable = objutil.ExeQueryDT(strQuery, "ddd")
    '    strQuery = "select tsk_id from wftransaction where userid =" & userid
    'Dim usrTsk As DataTable = objutil.ExeQueryDT(strQuery, "ddd")
    'Dim mySMTPClient As New System.Net.Mail.SmtpClient
    'Dim myEmail As New System.Net.Mail.MailMessage
    '    myEmail.BodyEncoding() = System.Text.Encoding.UTF8
    '    myEmail.SubjectEncoding = System.Text.Encoding.UTF8
    '    myEmail.To.Add(userdtMail.Rows(0).Item(0).ToString())
    '    myEmail.Subject = "Document ATP Pending"
    '    myEmail.Body = myEmail.Body & "Dear " & userdtMail.Rows(0).Item(1) & ", <br/>"
    '    If (isRejected = True) Then
    '        myEmail.Body = myEmail.Body & "Site: " & dbutils_nsn.GetSiteID_PONO(siteid, siteversion) & " as ATP Document rejected  <br/><br />"
    '    Else
    '        myEmail.Body = myEmail.Body & "Please check For Site: " & dbutils_nsn.GetSiteID_PONO(siteid, siteversion) & " as ATP Document Pending  <br/><br />"
    '    End If

    '    If (usrTsk.Rows(0).Item(0).ToString = "6") Then
    '        If (isRejected = False) Then
    '            myEmail.Body = myEmail.Body & "NSN invite Telkomsel  to conduct on site ATP. <br/> The ATP will commence  5 working days upon this notification <br/><br/>"
    '        End If
    '    End If
    '    myEmail.Body = myEmail.Body & "Powered By EBAST" & "<br/>"
    '    myEmail.IsBodyHtml = True
    '    myEmail.From = New System.Net.Mail.MailAddress(ConfigurationManager.AppSettings("Smailid"), "NSN")
    '    mySMTPClient.Host = ConfigurationManager.AppSettings("smtp")
    '    Try
    '        mySMTPClient.Send(myEmail)
    'Dim listuser As New List(Of UserInfo)
    '        If (usrTsk.Rows(0).Item(0).ToString = "6") Then
    'Dim userInfo As New UserInfo
    '            userInfo.UserId = userid
    '            userInfo.Username = userdtMail.Rows(0).Item(1)
    '            userInfo.Handphone = userdtMail.Rows(0).Item(2)
    '            listuser.Add(userInfo)
    '            SendMessagePendingATP(siteid, siteversion, listuser)
    '        End If
    '    Catch ex As Exception
    '        objdb.ExeNonQuery("exec uspErrLog '', 'mailsending','" & ex.Message.ToString() & "','sendmailtrans'")
    '    End Try
    'End Sub
    Private Sub ButtonDisabled()
        btnreview.Visible = False
        BtnATPOnSite.Visible = False
        BtnRejectReviewNew.Visible = False
    End Sub

    Private Sub SetButtonATPOnSite(ByVal packageid As Integer, ByVal siteid As Int32, ByVal siteversion As Integer, ByVal docid As Integer)
        Dim isVisible As Boolean = False
        If (CommonSite.RollId = ConfigurationManager.AppSettings("ATPROLEAPP") And docid = ConfigurationManager.AppSettings("ATP")) Then
            isVisible = True
            CheckATPOnSiteEnabled(packageid)
        End If
    End Sub
    Private Sub CheckATPOnSiteEnabled(ByVal packageid As Integer)
        If (dbutils_nsn.IsRegisteredATPOnSite(packageid) = True) Then
            BtnATPOnSite.Visible = False
        Else
            BtnATPOnSite.Visible = True
        End If

    End Sub
    Public Sub sendmailATP(ByVal siteid As Int32, ByVal siteversion As Integer, ByVal userid As Integer, ByVal isRejected As Boolean, ByVal reason As String, ByVal remarks As String)
        Dim notif As New NotificationBase
        If (isRejected = True) Then
            Dim isSucceed As Boolean = notif.SendMail(siteid, siteversion, userid, isRejected, NotificationBase.EmailDocType.ATP, ConfigurationManager.AppSettings("ATP"), reason, remarks, CommonSite.UserName, CommonSite.UserType)
        Else
            strsql = "select usr.usr_id,usr.email,usr.name,usr.phoneNo, usr.usrTYPE from wftransactionreview wfr " & _
                             "inner join ebastusers_1 usr on usr.usr_id = wfr.userid " & _
                             "where usr.usrRole = (select usrRole from ebastusers_1 where usr_id =(select userid from wftransaction where site_id = " & siteid & " and siteversion = " & siteversion & " and startdatetime is not null and enddatetime is null)) " & _
                             "and wfr.site_id = " & siteid & "and wfr.siteversion = " & siteversion & " and wfr.docid= " & ConfigurationManager.AppSettings("ATP")

            Dim useremails As DataTable = objutil.ExeQueryDT(strsql, "ddd")
            Dim list As New List(Of UserInfo)
            Dim intcount As Integer = 0
            Dim semicoloncount As Integer = 0
            For intcount = 0 To useremails.Rows.Count - 1
                Dim userinfo As New UserInfo
                userinfo.UserId = useremails.Rows(intcount).Item(0)
                userinfo.Email = useremails.Rows(intcount).Item(1)
                userinfo.Username = useremails.Rows(intcount).Item(2)
                userinfo.Handphone = useremails.Rows(intcount).Item(3)
                userinfo.UserType = useremails.Rows(intcount).Item(4)
                list.Add(userinfo)
            Next
            notif.SendMailReviewerATP(list, siteid, siteversion)
        End If
    End Sub

    Private Sub SendMessagePendingATP(ByVal siteid As Integer, ByVal siteversion As Integer, ByVal listuser As List(Of UserInfo))
        If (listuser.Count > 0) Then
            Dim sms As New SMSNew
            sms.SendSMSATPDoc(listuser, siteid, siteversion, ConfigurationManager.AppSettings("ATP"), False)
        End If
    End Sub

    Private Sub SendMailBAUTApproved(ByVal wpid As String, ByVal siteid As Int32, ByVal scope As String)
        objmail.SendMailBautApproved(wpid, siteid, CommonSite.UserName, scope)
    End Sub

#End Region
End Class
