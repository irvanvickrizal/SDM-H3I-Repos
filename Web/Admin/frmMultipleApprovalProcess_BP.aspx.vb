Imports Common
Imports System.Data
Imports Common_NSNFramework

Partial Class Admin_frmMultipleApprovalProcess_BP
    Inherits System.Web.UI.Page
#Region "NSNFramework"
    Dim dbutils_nsn As New DBUtils_NSN
#End Region
    Private objdb As New DBUtil
    Private objmail As New TakeMail

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            BindData()
        End If
    End Sub

    Protected Sub GvDocReview_ItemCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs) Handles GvDocReview.RowCommand
        If e.CommandName.Equals("approve") Then
            Dim row As GridViewRow = DirectCast(DirectCast(e.CommandSource, LinkButton).NamingContainer, GridViewRow)
            Dim userLogin As Label = CType(row.Cells(0).FindControl("LblUserLogin"), Label)
            Dim docid As Label = CType(row.Cells(0).FindControl("LblDocId"), Label)
            Dim docpath As Label = CType(row.Cells(0).FindControl("LblDocPath"), Label)
            Dim docname As Label = CType(row.Cells(0).FindControl("LblDocName"), Label)
            Dim pageno As Label = CType(row.Cells(0).FindControl("LblPageNo"), Label)
            Dim xval As Label = CType(row.Cells(0).FindControl("LblXVAL"), Label)
            Dim yval As Label = CType(row.Cells(0).FindControl("LblYVAL"), Label)
            Dim siteno As Label = CType(row.Cells(0).FindControl("LblSiteNo"), Label)
            Dim siteid As Label = CType(row.Cells(0).FindControl("LblSiteID"), Label)
            Dim siteversion As Label = CType(row.Cells(0).FindControl("LblSiteVersion"), Label)
            Dim username As Label = CType(row.Cells(0).FindControl("LblUsername"), Label)
            Dim pono As Label = CType(row.Cells(0).FindControl("LblPoNo"), Label)
            Dim userid As Label = CType(row.Cells(0).FindControl("LblUserID"), Label)
            Dim roleid As Label = CType(row.Cells(0).FindControl("LblRoleId"), Label)
            Dim packageid As Label = CType(row.Cells(0).FindControl("LblPackageId"), Label)
            'Dim strGet As String = "userLogin= " & userLogin.Text & "- docpath= " & docpath.Text & "- pageno= " & pageno.Text & "- xval=" & xval.Text & "- yval=" & yval.Text & "- pageno=" & pageno.Text & _
            '    "Site No= " & siteno.Text & "- siteid= " & siteid.Text & "- siteversion= " & siteversion.Text & "- username= " & username.Text & "- pono= " & pono.Text & "- userid= " & userid.Text
            'LblDocTest.Text = strGet
            DocApproved(Convert.ToInt32(e.CommandArgument.ToString()), Integer.Parse(pageno.Text), Integer.Parse(xval.Text), Integer.Parse(yval.Text), 50, 150, docpath.Text, userLogin.Text, username.Text, Integer.Parse(userid.Text), Integer.Parse(roleid.Text), Integer.Parse(docid.Text), docname.Text, pono.Text, siteno.Text, Convert.ToInt32(siteid.Text), Integer.Parse(siteversion.Text), packageid.Text)
            BindData()
        End If
    End Sub

    Protected Sub GvDocReviewPageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GvDocReview.PageIndexChanging
        GvDocReview.PageIndex = e.NewPageIndex
        BindData()
    End Sub


#Region "Custom Methods"
    Private Sub BindData()
        GvDocReview.DataSource = New MAController().GetAllMultipleApprovalProcess()
        GvDocReview.DataBind()
    End Sub

    Private Sub DocApproved(ByVal sno As Int32, ByVal pageno As Integer, ByVal xval As Integer, ByVal yval As Integer, ByVal intHeight As Integer, ByVal intWidth As Integer, ByVal docpath As String, ByVal userlogin As String, ByVal username As String, ByVal userid As Integer, ByVal roleid As Integer, ByVal docid As Integer, ByVal docname As String, ByVal pono As String, ByVal siteno As String, ByVal siteid As Int32, ByVal siteversion As Integer, ByVal packageid As String)
        Dim passinjection As String = "12345678"
        Dim finalbast As Integer = 0
        Try

            Dim Flags As Integer = 0
            Dim IntCount As Integer = 0
            If 1 = 1 Then Flags = Flags Or SAPIWrapper.SAPI_ENUM_DRAWING_ELEMENT_GRAPHICAL_IMAGE
            If 1 = 1 Then Flags = Flags Or SAPIWrapper.SAPI_ENUM_DRAWING_ELEMENT_SIGNED_BY
            If 1 = 0 Then Flags = Flags Or SAPIWrapper.SAPI_ENUM_DRAWING_ELEMENT_REASON
            If 1 = 1 Then Flags = Flags Or SAPIWrapper.SAPI_ENUM_DRAWING_ELEMENT_TIME
            Dim strResult As String = SAPIWrapper.SAPI_sign_file(ConfigurationManager.AppSettings("FPath") + docpath, Nothing, userlogin, _
            passinjection, pageno, xval, yval, intHeight, intWidth, False, "test", Flags, "")
            If (strResult = Constants._DigitalSign_Result) Then
                If (docid = CommonSite.BAUTID) Then
                    GetBautSign(passinjection, pono, sno, docpath, username, intHeight, intWidth, siteno)
                End If
                Dim i As Integer = -1
                Dim strsql As String = "Exec [uspDocApproved_Multiple_BG_Process] " & sno & "," & userid & ",'" & username & "'," & roleid & "," & ConfigurationManager.AppSettings("BAUTID") & "," & ConfigurationManager.AppSettings("BASTID")
                i = objdb.ExeQueryScalar(strsql)
                If i = 0 Then
                    ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('Error while doing the transaction.');", True)
                ElseIf i = 1 Then
                    Dim nextid As Integer
                    Dim k As Integer
                    'Dim docid As Integer
                    Dim dtr As New DataTable
                    Dim strs As String = "SELECT ISNULL(USERID,1)USERID,tsk_id,site_id,siteversion,docid FROM WFTRANSACTION WHERE" & _
                    " SITE_ID=(SELECT SITE_ID FROM WFTRANSACTION WHERE SNO = " & sno & " ) " & _
                    " AND SITEVERSION = (SELECT SITEVERSION FROM WFTRANSACTION WHERE SNO = " & sno & ")" & _
                    " AND STARTDATETIME IS NOT NULL AND ENDDATETIME IS NULL"
                    dtr = objdb.ExeQueryDT(strs, "ddd")
                    If dtr.Rows.Count > 0 Then
                        nextid = dtr.Rows(0).Item(0).ToString
                        k = dtr.Rows(0).Item(1).ToString
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
                    End If
                    Try
                        finalbast = objdb.ExeQueryScalar("select pstatus from bastmaster  where site_Id=" & siteid & " and siteversion =" & siteversion & "")
                    Catch ex As Exception
                        finalbast = 0
                        objdb.ExeNonQuery("exec uspErrLog '', 'executing query','" & ex.Message.ToString.Replace("'", "''") & "','ExeQueryScalar'")
                    End Try
                    If finalbast = 1 Then 'means bast approved finally.'send mail to bast team
                        Try
                            objmail.FinalBASTApprove(ConfigurationManager.AppSettings("bastteamrole"), siteno, ConfigurationManager.AppSettings("FinalBASTmailconst"), ConfigurationManager.AppSettings("BASTID"), pono)
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
                    If InStr(docname, "(BAST)") > 0 Then
                        Dim t As Integer = 0
                        t = objdb.ExeQueryScalar("select count(*) from wftransaction where STARTDATETIME IS NOT NULL AND ENDDATETIME IS NULL and site_id=(select site_id from wftransaction where sno=" & sno & ") and siteversion=" & siteversion & "  and docid=" & ConfigurationManager.AppSettings("BASTID")) '1032
                        If t <> 0 Then
                            Dim swid As Integer = 0
                            swid = objdb.ExeQueryScalar("select top 1 sw_id from sitedoc where siteid=( select site_id from wftransaction where sno=" & sno & ") and version=" & siteversion & "  and docid=" & ConfigurationManager.AppSettings("WCTRBASTID")) '1034
                            objdb.ExeNonQuery("update odwctrbast set bastsubdate=getdate() where swid= " & swid)
                            'to update to be approved record status as 4 inorder to avoid approval from dashboard. 
                            objdb.ExeNonQuery("update wftransaction set rstatus=4 where tsk_id in (4,5)  and site_id = (select site_id from wftransaction where sno=" & sno & ") and siteversion= " & siteversion & " and enddatetime is null  and docid=" & ConfigurationManager.AppSettings("BASTID")) '1032
                            objdb.ExeNonQuery("update sitedoc set rstatus=2 where siteid = (select site_id from wftransaction where sno=" & sno & ") and version= " & siteversion & " and docid=" & ConfigurationManager.AppSettings("WCTRBASTID")) '1034
                            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript123", "gopage(" & swid & ");", True)
                        End If
                    End If
                    'this is to update back the rstatus=2
                    If InStr(docname, "WCTR BAST") > 0 Then
                        Dim y As Integer = 0
                        y = objdb.ExeQueryScalar("select count(*) from wftransaction where STARTDATETIME IS NOT NULL AND ENDDATETIME IS NULL and site_id=(select site_id from wftransaction where sno=" & sno & ") and siteversion=" & siteversion & " and docid=" & ConfigurationManager.AppSettings("WCTRBASTID")) '1034
                        If y = 0 Then objdb.ExeNonQuery("update wftransaction set rstatus=2 where tsk_id in (4,5)  and site_id = (select site_id from wftransaction where sno=" & sno & ") and siteversion= " & siteversion & " and enddatetime is null  and docid=" & ConfigurationManager.AppSettings("BASTID")) '1032
                    End If
                    'new code irvan vickrizal 24112011
                    Dim userType As String = CommonSite.UserType.ToLower()
                    Dim dtrAtt As New DataTable
                    dtrAtt = objdb.ExeQueryDT("select site_id,siteversion, docid, tsk_id from wftransaction WHERE SNO = " & sno & " ", "ddddd")
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
                        If Not String.IsNullOrEmpty(packageid) Then
                            Try
                                dbutils_nsn.AddBastMaster(siteid, siteversion, Integer.Parse(ConfigurationManager.AppSettings("bastteamrole")))
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
            objdb.ExeNonQuery("exec uspErrLog '', 'MAAdminProcess','" & ex.Message.ToString.Replace("'", "''") & "','Error on end of Catch'")
        End Try
    End Sub

    Sub GetBautSign(ByVal newpassword As String, ByVal pono As String, ByVal sno As Int32, ByVal docpath As String, ByVal username As String, ByVal intHeight As Integer, ByVal intWidth As Integer, ByVal siteno As String)
        Try
            Dim strsql As String = "Exec uspGetBautXY " & sno & ",'" & pono & "'"
            Dim dt As DataTable = objdb.ExeQueryDT(strsql, "digilist")
            If (dt.Rows.Count > 0) Then
                Dim Flags As Integer = 0
                Dim IntCount As Integer = 0
                If 1 = 1 Then Flags = Flags Or SAPIWrapper.SAPI_ENUM_DRAWING_ELEMENT_GRAPHICAL_IMAGE
                If 1 = 1 Then Flags = Flags Or SAPIWrapper.SAPI_ENUM_DRAWING_ELEMENT_SIGNED_BY
                If 1 = 0 Then Flags = Flags Or SAPIWrapper.SAPI_ENUM_DRAWING_ELEMENT_REASON
                If 1 = 1 Then Flags = Flags Or SAPIWrapper.SAPI_ENUM_DRAWING_ELEMENT_TIME
                Dim strResult As String = SAPIWrapper.SAPI_sign_file(ConfigurationManager.AppSettings("FPath") + docpath, _
                    Nothing, username, newpassword, dt.Rows(0)("pageno"), dt.Rows(0)("xposition"), dt.Rows(0)("yposition"), _
                    intHeight, intWidth, False, "baut sign" & sno, Flags, _
                    "baut sign" & siteno)
            End If
        Catch ex As Exception
            objdb.ExeNonQuery("exec uspErrLog '', 'xyposs','" & ex.Message.ToString.Replace("'", "''") & "','bautxypo'")
        End Try
    End Sub
#End Region

End Class
