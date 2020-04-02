Imports BusinessLogic
Imports Common
Imports System.Data
Imports System.Collections.Generic
Imports AXmsCtrl
Imports System.IO
Imports NSNCustomizeConfiguration
Imports Common_NSNFramework

Partial Class Digital_Sign_QC_Digital_signature
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
    Dim i, j, k, l, docid, docidparent As Integer
    Dim objsms As New SMSNew
    Dim objmail As New TakeMail
    Dim finalbast As Integer
    Dim objsmsnew As New SMSNew
    Dim dt, dtn, userprep, userdt, userdt1, userdt2 As New DataTable
    Dim siteid, siteversion As Integer
    Dim strQuery As String = ""
    Dim divRev As String = ""
#Region "NSNFramework"
    Dim dbutils_nsn As New DBUtils_NSN
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        If Page.IsPostBack = False Then
            lnkrequest.Attributes.Add("onclick", "waitPreloadPage();")
            strQuery = String.Empty
            If (Not String.IsNullOrEmpty(Request.QueryString("id")) And GetAttributeMandatories() = True) Then
                txtUserName.Text = objutil.ExeQueryScalarString("select usrlogin from ebastusers_1 where usrlogin='" & Session("User_Login") & "'")
                txtUserName.Enabled = False
                intHeight = 50
                intWidth = 150
                GetPDFDocument()
                binddoc()

                'set up button approval rejection in the same place
                MvApprovalButtonPanel.SetActiveView(vwDefaultPanel)
                MvPanelSign.SetActiveView(defaultSign)
                '---------------------------------------------------------------------

                If Integer.Parse(hdnTaskid.Value) = 5 Then
                    dgrow.Visible = False
                    rerow.Visible = True
                Else
                    dgrow.Visible = True
                    rerow.Visible = False
                End If

                strQuery = String.Empty
                strQuery = "exec uspGetReviewerByTransactionId " & hdnSiteid.Value & ", " & hdnSiteVersion.Value & ", " & hdndocid.Value

                userdt = objutil.ExeQueryDT(strQuery, "ddd")

                If userdt.Rows.Count <> 0 Then
                    divRev = "<strong>This document was reviewed by  " & userdt.Rows(0).Item(0).ToString & " On " & userdt.Rows(0).Item(1).ToString & "</strong>"
                Else
                    If Integer.Parse(hdnTaskid.Value) = 4 Then
                        strQuery = "select count(*) from wftransaction  W" & _
                        " where tsk_id=5 and site_id=(select site_id from wftransaction where sno= " & Convert.ToInt32(Request.QueryString("id")) & ") " & _
                        " and siteversion=(select siteversion from wftransaction where sno= " & Convert.ToInt32(Request.QueryString("id")) & ")" & _
                        " and docid=(select docid from wftransaction where sno= " & Convert.ToInt32(Request.QueryString("id")) & ")"
                        Dim rcount As Integer
                        rcount = objutil.ExeQueryScalar(strQuery)
                        If rcount > 0 Then
                            divRev = "Reviewer task missing, please contact Administrator"
                        End If
                    End If
                End If
                divReviewer2.InnerHtml = divRev
            Else
                Response.Redirect("~/SessionTimeOut.aspx")
            End If

        End If
    End Sub

    Protected Sub BtnSubmitSignRejectClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSubmitSignReject.Click
        Dim categories As String = String.Empty
        Dim MyItem As ListItem
        Dim countThick As Integer = 0
        For Each MyItem In CbReasonLists.Items
            If MyItem.Selected = True Then
                If countThick = 1 Then
                    categories = categories & ", "
                End If
                categories = categories & MyItem.Text
                countThick = 1
            End If
        Next
        SubmitSignReject(TxtRemarks_SignRejectPanel.Text, categories)
    End Sub

    Protected Sub BtnSignRejectClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSignReject.Click
        BindSignRejectReasons()
    End Sub

    Protected Sub BtnCancelSignRejectClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnCancelSignReject.Click
        MvPanelSign.SetActiveView(defaultSign)
    End Sub

    Protected Sub BtnRejectReviewNewClick(ByVal Sender As Object, ByVal e As System.EventArgs) Handles BtnRejectReviewNew.Click
        BindReasons()
    End Sub

    Protected Sub BtnCancelSubmitClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnCancelSubmit.Click
        MvApprovalButtonPanel.SetActiveView(vwDefaultPanel)
        TxtRemarksReject.Text = ""
    End Sub

    Protected Sub BtnSubmitRejectClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSubmitReject.Click
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
        SubmitSignReject(TxtRemarksReject.Text, categories)
    End Sub

    Protected Sub BtnSign_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSign.Click
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        'bugfix100805 to check and fix the xy coordinate
        'objutil.ExeQuery("Exec [checkXYCoordinate]")
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
                    If (HDDocid.Value = ConfigurationManager.AppSettings("QCID")) Then
                        GetAcceptanceDateSign(npwd1)
                    End If
                    Dim i As Integer = -1
                    'i = objBo.uspDocApproved(Convert.ToInt32(Request.QueryString("id")), CommonSite.UserId(), CommonSite.UserName(), CommonSite.RollId(), ConfigurationManager.AppSettings("BAUTID"), ConfigurationManager.AppSettings("BASTID"))
                    strQuery = "Exec [uspDocApproved] " & Convert.ToInt32(Request.QueryString("id")) & "," & CommonSite.UserId() & ",'" & CommonSite.UserName() & "'," & CommonSite.RollId() & "," & ConfigurationManager.AppSettings("BAUTID") & "," & ConfigurationManager.AppSettings("BASTID") & "," & ConfigurationManager.AppSettings("BAST2ID") & " "
                    i = objutil.ExeQueryScalar(strQuery)
                    If i = 0 Then
                        ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('Error while doing the transaction.');", True)
                    ElseIf i = 1 Then
                        Dim nextid As Integer
                        Dim k As Integer
                        Dim siteid As Integer
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
                        'new code irvan vickrizal 24112011
                        Dim userType As String = CommonSite.UserType.ToLower()
                        If (CommonSite.UserType.ToLower() = "n") Then
                            If (docid = ConfigurationManager.AppSettings("BASTID") Or docid = ConfigurationManager.AppSettings("BOQDOCID") Or docid = ConfigurationManager.AppSettings("WCTRBASTID")) Then
                                If (Integer.Parse(hdnTaskid.Value) = 3) Then
                                    dbutils_nsn.CheckGroupingDocWillBeReviewed(siteid, siteversion, ConfigurationManager.AppSettings("BASTID"), ConfigurationManager.AppSettings("WCTRBASTID"), ConfigurationManager.AppSettings("BOQDOCID"))
                                End If
                            End If
                        End If

                        If (hdndocid.Value = ConfigurationManager.AppSettings("QCID")) Then

                            If Integer.Parse(hdnTaskid.Value) > 0 Then
                                Dim strGetNextUserQuery As String = "select usr.usr_id, email,Name,phoneNo from ebastusers_1 usr " & _
                                                                    "inner join ebastuserrole rol on usr.usr_id = rol.usr_id " & _
                                                                    "where usrRole in(select roleid from twfdefinition where wfid =(select wfid from wftransaction where sno = " & Convert.ToInt32(Request.QueryString("id")) & ")" & _
                                                                    "and sorder = (select sorder + 1 from twfdefinition where wfid = (select wfid from wftransaction where sno = " & Convert.ToInt32(Request.QueryString("id")) & ")" & _
                                                                    "and tsk_id = " & hdnTaskid.Value & ")) and rol.rgn_id in( select rgn_id from codsite where site_id= " & Convert.ToInt32(siteid) & ")"
                                Dim dtusrQCatts As DataTable = objutil.ExeQueryDT(strGetNextUserQuery, "usrnext")
                                Dim getSiteAttributeQuery As String = "select  TselProjectID,SiteNo, PoNo, Workpackageid,Scope from poepmsitenew where workpackageid = '" & Request.QueryString("wpid") & "'"
                                Dim dtSiteAtt As DataTable = objutil.ExeQueryDT(getSiteAttributeQuery, "siteAtt")
                                Dim siteAttEmail As String = String.Empty
                                If dtSiteAtt.Rows.Count > 0 Then
                                    siteAttEmail = Convert.ToString(dtSiteAtt.Rows(0).Item(1)) & "-" & Convert.ToString(dtSiteAtt.Rows(0).Item(2)) & "-" & Convert.ToString(dtSiteAtt.Rows(0).Item(3)) & "-" & Convert.ToString(dtSiteAtt.Rows(0).Item(4))
                                End If
                                If dtusrQCatts.Rows.Count = 1 Then
                                    
                                    Dim sbBody As New StringBuilder
                                    sbBody.Append("Dear " & dtusrQCatts.Rows(0).Item(2) & ", <br/><br/>")
                                    sbBody.Append("There is QC document of " & siteAttEmail & " Waiting on your approval " & "<br/><br />")
                                    sbBody.Append("<a href='http://www.telkomsel.nsnebast.com'>Click here</a>" & " to go to e-BAST <br/>")
                                    sbBody.Append("Powered By EBAST" & "<br/>")
                                    sbBody.Append("<img src='http://www.telkomsel.nsnebast.com/images/nsn-logo.gif' alt='companylogo' />")
                                    objmail.SendMailQC(dtusrQCatts.Rows(0).Item(1).ToString(), sbBody.ToString(), "QC Document Waiting")
                                ElseIf dtusrQCatts.Rows.Count > 1 Then
                                    Dim sbBody As New StringBuilder
                                    sbBody.Append("Dear Sir/Madam , <br/><br/>")
                                    sbBody.Append("There is QC document of " & siteAttEmail & " Waiting on your approval " & "<br/><br />")
                                    sbBody.Append("Powered By EBAST" & "<br/>")
                                    sbBody.Append("<img src='http://www.telkomsel.nsnebast.com/images/nsn-logo.gif' alt='companylogo' />")
                                    objmail.SendMailQCUserGroup(dtusrQCatts, sbBody.ToString(), "QC Document Waiting")
                                Else
                                    'QC online has been approved
                                    strGetNextUserQuery = String.Empty
                                    strGetNextUserQuery = "select usr.usr_id, email,Name,phoneNo from ebastusers_1 usr " & _
                                                                    "inner join ebastuserrole rol on usr.usr_id = rol.usr_id " & _
                                                                    "where usrRole in(select roleid from twfdefinition where wfid in(select wfid from wftransaction where sno = " & Convert.ToInt32(Request.QueryString("id")) & ")" & _
                                                                    "and sorder in(1,2))" & _
                                                                    "and rol.rgn_id in( select rgn_id from codsite where site_id= " & Convert.ToInt32(siteid) & ")"
                                    Dim dtusers1 As DataTable = objutil.ExeQueryDT(strGetNextUserQuery, "userapp1")
                                    strGetNextUserQuery = String.Empty
                                    strGetNextUserQuery = "select usr.usr_id, email,Name,phoneNo from ebastusers_1 usr " & _
                                                                                                    "inner join ebastuserrole rol on usr.usr_id = rol.usr_id " & _
                                                                                                    "where usrRole in(" & ConfigurationManager.AppSettings("bautteamrole") & ")" & _
                                                                                                    "and rol.rgn_id in( select rgn_id from codsite where site_id= " & Convert.ToInt32(siteid) & ")"

                                    'Dim dtusers2 As DataTable = objutil.ExeQueryDT(strGetNextUserQuery, "userapp2")
                                    Dim sbBody As New StringBuilder
                                    Dim strCompany As String
                                    If CommonSite.UserType = "N" Then
                                        strCompany = "NSN"
                                    ElseIf CommonSite.UserType = "C" Then
                                        strCompany = "Telkomsel"
                                    Else
                                        strCompany = "Partner"
                                    End If
                                    sbBody.Append("Dear Sir/Madam , <br/><br/>")
                                    sbBody.Append("There is QC document of " & siteAttEmail & " has been approved by " & CommonSite.UserName & " " & strCompany & "<br/><br />")
                                    sbBody.Append("Powered By EBAST" & "<br/>")
                                    sbBody.Append("<img src='http://www.telkomsel.nsnebast.com/images/nsn-logo.gif' alt='companylogo' />")

                                    objmail.SendMailQCUserGroup(dtusers1, sbBody.ToString(), "QC Document Approved")
                                    'objmail.SendMailQCUserGroup(dtusers2, sbBody.ToString(), "QC Document Approved")
                                End If
                            End If
                        End If
                        ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "WindowsCloseQCApprover();", True)
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
                Dim LblDocId As Label = CType(e.Row.FindControl("LblDocid"), Label)
                Dim rdb As RadioButtonList = CType(e.Row.FindControl("rdbstatus"), RadioButtonList)
                If LblDocId.Text = ConfigurationManager.AppSettings("ATP") Then
                    rdb.Enabled = False
                End If
        End Select
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim url As String
            url = "../PO/frmViewDocument.aspx?id=" & e.Row.Cells(4).Text
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
        Dim strquery As String = String.Empty
        Dim i As Integer
        i = objutil.ExeQueryScalar("Exec [uspDocApproved] " & Convert.ToInt32(Request.QueryString("id")) & "," & CommonSite.UserId() & ",'" & CommonSite.UserName() & "'," & CommonSite.RollId() & "," & ConfigurationManager.AppSettings("BAUTID") & "," & ConfigurationManager.AppSettings("BASTID") & "," & ConfigurationManager.AppSettings("BAST2ID") & "")
        If i = 0 Then
            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('Error while doing the transaction.');", True)
        ElseIf i = 1 Then
            ButtonDisabled()
            If (hdndocid.Value = ConfigurationManager.AppSettings("QCID")) Then
                If Integer.Parse(hdnTaskid.Value) > 0 Then
                    Dim strGetNextUserQuery As String = "select usr.usr_id, email,Name,phoneNo from ebastusers_1 usr " & _
                                                        "inner join ebastuserrole rol on usr.usr_id = rol.usr_id " & _
                                                        "where usrRole in(select roleid from twfdefinition where wfid =(select wfid from wftransaction where sno = " & Convert.ToInt32(Request.QueryString("id")) & ")" & _
                                                        "and sorder = (select sorder + 1 from twfdefinition where wfid = (select wfid from wftransaction where sno = " & Convert.ToInt32(Request.QueryString("id")) & ")" & _
                                                        "and tsk_id = " & hdnTaskid.Value & "))"
                    Dim dtusrQCatts As DataTable = objutil.ExeQueryDT(strGetNextUserQuery, "usrnext")

                    Dim getSiteAttributeQuery As String = "select  TselProjectID,SiteNo, PoNo, Workpackageid,Scope from poepmsitenew where workpackageid = '" & Request.QueryString("wpid") & "'"
                    Dim dtSiteAtt As DataTable = objutil.ExeQueryDT(getSiteAttributeQuery, "siteAtt")
                    Dim siteAttEmail As String = String.Empty
                    If dtSiteAtt.Rows.Count > 0 Then
                        siteAttEmail = Convert.ToString(dtSiteAtt.Rows(0).Item(1)) & "-" & Convert.ToString(dtSiteAtt.Rows(0).Item(2)) & "-" & Convert.ToString(dtSiteAtt.Rows(0).Item(3)) & "-" & Convert.ToString(dtSiteAtt.Rows(0).Item(4))
                    End If
                    If dtusrQCatts.Rows.Count > 0 Then
                        Dim sbBody As New StringBuilder
                        sbBody.Append("Dear " & dtusrQCatts.Rows(0).Item(2) & ", <br/><br/>")
                        sbBody.Append("There is QC document of " & siteAttEmail & " Waiting on your approval " & "<br/><br />")
                        sbBody.Append("<a href='http://www.telkomsel.nsnebast.com'>Click here</a>" & " to go to e-BAST<br/>")
                        sbBody.Append("Powered By EBAST" & "<br/>")
                        sbBody.Append("<img src='http://www.telkomsel.nsnebast.com/images/nsn-logo.gif' alt='companylogo' />")
                        objmail.SendMailQC(dtusrQCatts.Rows(0).Item(1).ToString(), sbBody.ToString(), "QC Document Waiting")
                    End If
                End If
            End If
            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "WindowsCloseQCReviewer();", True)
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
            scripts = "alert('Document rejected successfully');WindowRejectCloseQC();"
            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "rejected", scripts, True)
        End If
    End Sub

#Region "custom methods"
    Private Function GetAttributeMandatories() As Boolean
        strQuery = "exec uspGetSiteAttributeByTransactionId " & Request.QueryString("id")
        Dim dtDocAtt As DataTable = objutil.ExeQueryDT(strQuery, "ddd")
        If dtDocAtt.Rows.Count > 0 Then
            hdnSiteid.Value = dtDocAtt.Rows(0).Item(0)
            hdnSiteVersion.Value = dtDocAtt.Rows(0).Item(1)
            hdndocid.Value = dtDocAtt.Rows(0).Item(2)
            hdnParentDocid.Value = dtDocAtt.Rows(0).Item(3)
            hdnTaskid.Value = dtDocAtt.Rows(0).Item(4)
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub GetPDFDocument()
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

    Private Sub binddoc()
        'hardcoded to filter only for BAUT and BAST
        Dim dt As New DataTable
        strQuery = String.Empty
        If hdndocid.Value = ConfigurationManager.AppSettings("QCID") Then
            strQuery = "Exec uspSiteQCDocList " & Request.QueryString("swid") & ",0 , " & CommonSite.UserId
        Else
            strQuery = "Exec uspSiteBAUTDocList " & Request.QueryString("swid") & ",0 , " & CommonSite.UserId
        End If

        dt = objutil.ExeQueryDT(strQuery, "digilist")
        grddocuments.DataSource = dt
        grddocuments.DataBind()
        grddocuments.Columns(1).Visible = False
        grddocuments.Columns(2).Visible = False
        grddocuments.Columns(3).Visible = True
        grddocuments.Columns(4).Visible = False
        grddocuments.Columns(5).Visible = False
        grddocuments.Columns(6).Visible = False
        If hdndocid.Value = ConfigurationManager.AppSettings("QCID") Then
            grddocuments.Columns(7).Visible = True
        Else
            grddocuments.Columns(7).Visible = True
        End If

        'get the parent document
        'strsql = "Exec uspSiteBAUTDocList " & Request.QueryString("swid") & ",1 ," & CommonSite.UserId
        strQuery = String.Empty
        If hdndocid.Value = ConfigurationManager.AppSettings("QCID") Then
            strQuery = "Exec uspSiteQCDocList " & Request.QueryString("swid") & ",1 , " & CommonSite.UserId
        Else
            strQuery = "Exec uspSiteBAUTDocList " & Request.QueryString("swid") & ",1 , " & CommonSite.UserId
        End If
        dt = objutil.ExeQueryDT(strQuery, "digilist")
        grddocuments2.DataSource = dt
        grddocuments2.DataBind()
        grddocuments2.Columns(1).Visible = False
        grddocuments2.Columns(2).Visible = False
        grddocuments2.Columns(3).Visible = True
        grddocuments2.Columns(4).Visible = False
        grddocuments2.Columns(5).Visible = False
        grddocuments2.Columns(6).Visible = False

        If hdndocid.Value = ConfigurationManager.AppSettings("QCID") Then
            grddocuments2.Columns(7).Visible = False
        Else
            grddocuments2.Columns(7).Visible = True
        End If
        grddocuments2.Visible = False
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

    Private Sub GetBautSign(ByVal newpassword As String)
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

    Private Sub GetAcceptanceDateSign(ByVal newpassword As String)
        Dim isSucceed As Boolean = False
        Try
            Dim strsql As String = "select top 1 tsk_id from wftransaction where sno=" & Convert.ToInt32(Request.QueryString("id"))
            Dim taskid As Integer = objutil.ExeQueryScalar(strsql)
            If taskid = 4 Then
                Dim Flags As Integer = 0
                Dim IntCount As Integer = 0
                If 1 = 1 Then Flags = Flags Or SAPIWrapper.SAPI_ENUM_DRAWING_ELEMENT_GRAPHICAL_IMAGE
                If 1 = 1 Then Flags = Flags Or SAPIWrapper.SAPI_ENUM_DRAWING_ELEMENT_SIGNED_BY
                If 1 = 0 Then Flags = Flags Or SAPIWrapper.SAPI_ENUM_DRAWING_ELEMENT_REASON
                If 1 = 1 Then Flags = Flags Or SAPIWrapper.SAPI_ENUM_DRAWING_ELEMENT_TIME
                Dim strResult As String = SAPIWrapper.SAPI_sign_file(ConfigurationManager.AppSettings("FPath") + HDPath.Value, _
                    Nothing, "Manager_NPO", newpassword, 1, 500, 519, _
                18, 100, False, "test", Flags, "")
                isSucceed = True
            End If
        Catch ex As Exception
            objutil.ExeNonQuery("exec uspErrLog '', 'xyposs','" & ex.Message.ToString.Replace("'", "''") & "','acceptancedatesigned'")
        End Try

        If isSucceed = True Then
            Try
                objutil.ExeNonQuery("exec uspQCUpdateAcceptanceDate " & Request.QueryString("wpid"))
            Catch ex As Exception
                objutil.ExeNonQuery("exec uspErrLog '', 'acpdateupdated','" & ex.Message.ToString.Replace("'", "''") & "','acceptancedateupdated'")
            End Try
        End If

    End Sub

    Private Sub BindReasons()
        MvApprovalButtonPanel.SetActiveView(vwRejectPanel)
        CbList.DataSource = dbutils_nsn.GetReasons(ConfigurationManager.AppSettings("QCID"))
        CbList.DataTextField = "ReasonName"
        CbList.DataValueField = "ReasonId"
        CbList.DataBind()
    End Sub

    Private Sub BindSignRejectReasons()
        MvPanelSign.SetActiveView(defaultSignReject)
        CbReasonLists.DataSource = dbutils_nsn.GetReasons(ConfigurationManager.AppSettings("QCID"))
        CbReasonLists.DataTextField = "ReasonName"
        CbReasonLists.DataValueField = "ReasonId"
        CbReasonLists.DataBind()
    End Sub

    Private Sub SubmitReject(ByVal remarks As String, ByVal catReason As String)
        Dim wpid As String = Request.QueryString("wpid")
        Dim siteid As Integer = 0
        'strsql = "select cs.site_id, po.siteversion from podetails po inner join codsite cs on cs.site_no = po.siteno  where workpkgid='" + wpid + "'"
        'Dim sitedesc As DataTable = objutil.ExeQueryDT(strsql, "ddd")

        If (wpid IsNot Nothing) Then
            Dim GrpId As String = Request.QueryString("id")
            Dim i As Integer = -1
            i = dbutils_nsn.DocReject(GrpId, wpid, CommonSite.UserId, CommonSite.UserName, CommonSite.RollId, TxtRemarksReject.Text, ConfigurationManager.AppSettings("BAUTID"), ConfigurationManager.AppSettings("BASTID"), ConfigurationManager.AppSettings("WCTRBASTID"), catReason)
            If i = 0 Then
                ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('Error while doing the transaction.');", True)
            ElseIf i = 1 Then
                Try
                    strQuery = "select usr.usr_id,usr.email,usr.phoneNo,usr.name,wf.site_id,wf.siteversion,wf.tsk_id, usr.usrType, usr.srcid from wftransaction wf " & _
                                                      "inner join codsite cs on wf.site_id = cs.site_id " & _
                                                      "inner join podetails po on po.siteno = cs.site_no and po.siteversion = wf.siteversion " & _
                                                      "inner join ebastusers_1 usr on usr.usr_id = wf.lmby " & _
                                                      "where wf.sno = " & GrpId

                    Dim usersemail As DataTable = objutil.ExeQueryDT(strQuery, "ddd")
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

                    SendEmailRejection(listusers, remarks, catReason)
                Catch ex As Exception
                    objdb.ExeNonQuery("exec uspErrLog 0, 'mailsendingreject','" & ex.Message.ToString() & "','sendmailqcreject'")
                End Try
                ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "WindowRejectCloseQC();", True)
            End If
        End If
    End Sub

    Private Sub SubmitAllApproved()
        Dim wpid As String = Request.QueryString("wpid")
        Dim siteid As Integer = 0
        If (wpid IsNot Nothing) Then
            Try
                strQuery = "select usr.usr_id,usr.email,usr.phoneNo,usr.name,wf.site_id,wf.siteversion,wf.tsk_id, usr.usrtype from wftransaction wf " & _
                         "inner join codsite cs on wf.site_id = cs.site_id " & _
                         "inner join podetails po on po.siteno = cs.site_no and wf.siteversion = po.siteversion " & _
                         "inner join ebastusers_1 usr on usr.usr_id = wf.lmby " & _
                         "where po.workpkgid = '" & wpid & "' and docid = " & ConfigurationManager.AppSettings("ATP") & " and wf.tsk_id in (1,5,6)"

                Dim usersemail As DataTable = objutil.ExeQueryDT(strQuery, "ddd")
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
                    SendMailATPApproved(listusers, siteidmail, siteversionmail)
                End If
            Catch ex As Exception
                objdb.ExeNonQuery("exec uspErrLog '', 'mailsendingATPapproved','" & ex.Message.ToString.Replace("'", "''") & "','sendmailATPapproved'")
            End Try
        End If
    End Sub

    Private Sub SendEmailRejection(ByVal listusers As List(Of UserInfo), ByVal remarks As String, ByVal categories As String)
        Dim sb As New StringBuilder
        Dim compRejected As String = String.Empty
        sb.Append("Dear Sir / Madam, <br/>")
        If CommonSite.UserType = "N" Then
            compRejected = "NSN"
        ElseIf CommonSite.UserType = "C" Then
            compRejected = "Telkomsel"
        Else
            compRejected = "Partner"
        End If
        If hdndocid.Value = ConfigurationManager.AppSettings("ATP") Then
            sb.Append("There is ATP document of " & dbutils_nsn.GetSiteID_PONO(siteid, siteversion) & " has been rejected by " & CommonSite.UserName & "-" & compRejected & "<br/><br />")
        Else
            sb.Append("There is QC document of " & dbutils_nsn.GetSiteID_PONO(siteid, siteversion) & " has been rejected by " & CommonSite.UserName & "-" & compRejected & "<br/><br />")
        End If

        sb.Append("Summary : <br/>Reason :" & categories & "<br/>remarks :" & remarks & "<br/><br/>")
        sb.Append("Powered By EBAST" & "<br/>")
        sb.Append("<img src='http://www.telkomsel.nsnebast.com/images/nsn-logo.gif' alt='companylogo' />")
        Dim notif As New NotificationBase
        'notif.SendMailATP(listusers, sb.ToString(), "Document ATP Rejected")
        If hdndocid.Value = ConfigurationManager.AppSettings("ATP") Then
            objmail.SendMailATP(listusers, sb.ToString(), "Document ATP Rejected")
        Else
            objmail.SendMailATP(listusers, sb.ToString(), "Document QC Rejected")
        End If
    End Sub

    Private Sub SendMailATPApproved(ByVal listusers As List(Of UserInfo), ByVal site_Id As Integer, ByVal version As Integer)
        Dim sb As New StringBuilder
        Dim compRejected As String = String.Empty
        sb.Append("Dear Sir / Madam, <br/>")
        If CommonSite.UserType = "N" Then
            compRejected = "NSN"
        ElseIf CommonSite.UserType = "C" Then
            compRejected = "Telkomsel"
        Else
            compRejected = "Partner"
        End If
        sb.Append("There is ATP document of " & dbutils_nsn.GetSiteID_PONO(site_Id, version) & " has been approved by " & CommonSite.UserName & "-" & compRejected & " as final approval.<br/><br />")
        sb.Append("Powered By EBAST" & "<br/>")
        sb.Append("<img src='http://www.telkomsel.nsnebast.com/images/nsn-logo.gif' alt='companylogo' />")
        Dim notif As New NotificationBase
        notif.SendMailATP(listusers, sb.ToString(), "Document ATP Final Approved")
    End Sub

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

    Private Sub ButtonDisabled()
        btnreview.Visible = False
        BtnRejectReviewNew.Visible = False
    End Sub

    Private Sub SubmitSignReject(ByVal remarks As String, ByVal catReason As String)
        Dim wpid As String = Request.QueryString("wpid")
        Dim site_id As Int32 = 0

        If (wpid IsNot Nothing) Then
            Dim GrpId As String = Request.QueryString("id")
            Dim i As Integer = -1
            i = dbutils_nsn.DocReject(GrpId, wpid, CommonSite.UserId, CommonSite.UserName, CommonSite.RollId, remarks, ConfigurationManager.AppSettings("BAUTID"), ConfigurationManager.AppSettings("BASTID"), ConfigurationManager.AppSettings("WCTRBASTID"), catReason)
            If i = 0 Then
                ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('Error while doing the transaction.');", True)
            ElseIf i = 1 Then
                Try
                    'strsql = "select usr.usr_id,usr.email,usr.phoneNo,usr.name,wf.site_id,wf.siteversion,wf.tsk_id, usr.usrType, usr.srcid from wftransaction wf " & _
                    '         "inner join codsite cs on wf.site_id = cs.site_id " & _
                    '         "inner join podetails po on po.siteno = cs.site_no and po.siteversion = wf.siteversion " & _
                    '         "inner join ebastusers_1 usr on usr.usr_id = wf.lmby " & _
                    '         "where wf.sno = " & GrpId
                    'Dim usersemail As DataTable = objutil.ExeQueryDT(strsql, "ddd")
                    Dim intTSKID As Integer = objutil.ExeQueryScalar("select tsk_id from wftransaction where sno =" & Convert.ToInt32(Request.QueryString("id")))
                    site_id = objutil.ExeQueryScalar("select top 1 site_id from wftransaction where sno=" & Convert.ToInt32(Request.QueryString("id")))
                    siteversion = objutil.ExeQueryScalar("select top 1 siteversion from wftransaction where sno=" & Convert.ToInt32(Request.QueryString("id")))
                    Dim rdbr As New RadioButtonList
                    Dim str As String = ""
                    Dim count As Integer = 0
                    Dim sw_id As String
                    Dim usrname As String = Session("User_Name")
                    Dim pono As String = ""
                    Dim siteid As String = ""
                    Dim docid As Integer
                    For k = 0 To grddocuments.Rows.Count - 1
                        rdbr = grddocuments.Rows(k).Cells(5).FindControl("rdbstatus")
                        If rdbr.SelectedValue = 1 Then
                            str = IIf(str <> "", str & "," & grddocuments.Rows(k).Cells(4).Text.ToString, grddocuments.Rows(k).Cells(4).Text.ToString)
                            sw_id = Convert.ToString(grddocuments.Rows(k).Cells(4).Text)
                            'Dim txt1 As TextBox = grddocuments.Rows(k).Cells(6).FindControl("txtremarks")
                            docid = Convert.ToString(grddocuments.Rows(k).Cells(1).Text)
                            siteid = Convert.ToString(grddocuments.Rows(k).Cells(5).Text)
                            pono = Convert.ToString(grddocuments.Rows(k).Cells(6).Text)
                            If Not String.IsNullOrEmpty(sw_id) Then
                                objutil.ExeNonQuery("Exec uspBAUTDocReject " & sw_id & ",'" & remarks & "'," & CommonSite.UserId & "," & CommonSite.RollId & ",'" & usrname & "','" & pono & "','" & site_id & "'," & docid & "," & ConfigurationManager.AppSettings("WCTRBASTID") & "," & ConfigurationManager.AppSettings("BAUTID") & "," & ConfigurationManager.AppSettings("BASTID"))
                            End If
                            count = count + 1
                        End If
                    Next
                    If intTSKID > 0 Then
                        Dim sorder As String = String.Empty
                        If intTSKID = 3 Then
                            sorder = "1"
                        Else
                            sorder = "2,1"
                        End If
                        Dim strGetNextUserQuery As String = "select usr.usr_id, email,Name,phoneNo from ebastusers_1 usr " & _
                                                            "inner join ebastuserrole rol on usr.usr_id = rol.usr_id " & _
                                                            "where usrRole in(select roleid from twfdefinition where wfid =(select wfid from wftransaction where sno = " & Convert.ToInt32(Request.QueryString("id")) & ") " & _
                                                            "and sorder in (" & sorder & ")) " & _
                                                            "and rol.rgn_id in(select rgn_id from codsite where site_id= " & site_id & ")"

                        Dim strGetSubconsQuery As String = "select usr_id, email,Name,phoneNo from ebastusers_1 where usr_id in " & _
                                                            "((select distinct (lmby) from wftransaction where site_id = " & site_id & " and siteversion = " & siteversion & _
                                                            "and docid in(select doc_id from coddoc where parent_id = " & ConfigurationManager.AppSettings("QCID") & ")))"

                        Dim usersemail As DataTable = objutil.ExeQueryDT(strGetNextUserQuery, "usrnext")
                        Dim usersubconemail As DataTable = objutil.ExeQueryDT(strGetSubconsQuery, "sbcnext")

                        If usersemail.Rows.Count > 1 Then
                            Dim ki As Integer = 0
                            Dim kii As Integer = 0
                            Dim listusers As New List(Of UserInfo)

                            For ki = 0 To usersemail.Rows.Count - 1
                                Dim subcon As New UserInfo
                                Dim found = listusers.Find(AddressOf New emailmatching(usersemail.Rows(ki).Item(1).ToString()).FindByEmail)
                                If found Is Nothing Then
                                    Dim info As New UserInfo
                                    info.UserId = Integer.Parse(usersemail.Rows(ki).Item(0).ToString())
                                    info.Username = usersemail.Rows(ki).Item(2).ToString()
                                    info.Email = usersemail.Rows(ki).Item(1).ToString()
                                    info.Handphone = Convert.ToString(usersemail.Rows(ki).Item(3))
                                    listusers.Add(info)
                                End If
                            Next
                            For kii = 0 To usersubconemail.Rows.Count - 1
                                Dim found = listusers.Find(AddressOf New emailmatching(usersubconemail.Rows(kii).Item(1).ToString()).FindByEmail)
                                If found Is Nothing Then
                                    Dim info As New UserInfo
                                    info.UserId = Integer.Parse(usersubconemail.Rows(kii).Item(0).ToString())
                                    info.Username = usersubconemail.Rows(kii).Item(2).ToString()
                                    info.Email = usersubconemail.Rows(kii).Item(1).ToString()
                                    info.Handphone = Convert.ToString(usersubconemail.Rows(kii).Item(3))
                                    listusers.Add(info)
                                End If
                            Next
                            SendEmailSignRejection(listusers, remarks, catReason)
                        End If
                    End If
                    ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "WindowRejectCloseQC();", True)
                Catch ex As Exception
                    objdb.ExeNonQuery("exec uspErrLog 01, 'mailsendingreject','" & ex.Message.ToString.Replace("'", " ") & "','sendmailreject'")
                    ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "WindowRejectCloseQC();", True)
                End Try
                ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "WindowRejectCloseQC();", True)
            End If
        End If
    End Sub

    Private Sub SendEmailSignRejection(ByVal listusers As List(Of UserInfo), ByVal remarks As String, ByVal categories As String)
        Dim getSiteAttributeQuery As String = "select  TselProjectID,SiteNo, PoNo, Workpackageid,Scope from poepmsitenew where workpackageid = '" & Request.QueryString("wpid") & "'"
        Dim dtSiteAtt As DataTable = objutil.ExeQueryDT(getSiteAttributeQuery, "siteAtt")
        Dim siteAttEmail As String = String.Empty
        If dtSiteAtt.Rows.Count > 0 Then
            siteAttEmail = Convert.ToString(dtSiteAtt.Rows(0).Item(1)) & "-" & Convert.ToString(dtSiteAtt.Rows(0).Item(2)) & "-" & Convert.ToString(dtSiteAtt.Rows(0).Item(3)) & "-" & Convert.ToString(dtSiteAtt.Rows(0).Item(4))
        End If
        Dim sb As New StringBuilder
        Dim compRejected As String = String.Empty
        sb.Append("Dear Sir / Madam, <br/>")
        If CommonSite.UserType = "N" Then
            compRejected = "NSN"
        ElseIf CommonSite.UserType = "C" Then
            compRejected = "Telkomsel"
        Else
            compRejected = "Partner"
        End If
        sb.Append("There is QC document of " & siteAttEmail & " has been rejected by " & CommonSite.UserName & "-" & compRejected & "<br/><br />")
        sb.Append("Summary : <br/>Reason :" & categories & "<br/>remarks :" & remarks & "<br/><br/>")
        sb.Append("<a href='http://www.telkomsel.nsnebast.com'>Click here</a>" & " to go to e-BAST<br/>")
        sb.Append("Powered By EBAST" & "<br/>")
        sb.Append("<img src='http://www.telkomsel.nsnebast.com/images/nsn-logo.gif' alt='companylogo' />")
        Dim notif As New NotificationBase
        objmail.SendMailATP(listusers, sb.ToString(), "Document QC Rejected")
    End Sub

    Public Sub sendmailATP(ByVal siteid As Integer, ByVal siteversion As Integer, ByVal userid As Integer, ByVal isRejected As Boolean, ByVal reason As String, ByVal remarks As String)
        Dim notif As New NotificationBase
        If (isRejected = True) Then
            Dim isSucceed As Boolean = notif.SendMail(siteid, siteversion, userid, isRejected, NotificationBase.EmailDocType.ATP, ConfigurationManager.AppSettings("ATP"), reason, remarks, CommonSite.UserName, CommonSite.UserType)
        Else
            strQuery = "select usr.usr_id,usr.email,usr.name,usr.phoneNo, usr.usrTYPE from wftransactionreview wfr " & _
                             "inner join ebastusers_1 usr on usr.usr_id = wfr.userid " & _
                             "where usr.usrRole = (select usrRole from ebastusers_1 where usr_id =(select userid from wftransaction where site_id = " & siteid & " and siteversion = " & siteversion & " and startdatetime is not null and enddatetime is null)) " & _
                             "and wfr.site_id = " & siteid & "and wfr.siteversion = " & siteversion & " and wfr.docid= " & ConfigurationManager.AppSettings("ATP")

            Dim useremails As DataTable = objutil.ExeQueryDT(strQuery, "ddd")
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

    Private Sub SendMailBAUTApproved(ByVal wpid As String, ByVal siteid As Integer, ByVal scope As String)
        objmail.SendMailBautApproved(wpid, siteid, CommonSite.UserName, scope)
    End Sub

#End Region
End Class
