Imports System.Data
Imports System.IO
Imports Common
Imports BusinessLogic
Imports Entities
Imports Common_NSNFramework

Partial Class frmTI_WCTRBAST
    Inherits System.Web.UI.Page
    Dim dt As New DataTable
    Dim objUtil As New DBUtil
    Dim str, str1 As String
    Dim cst As New Constants
    Dim objBO As New BOSiteDocs
    Dim docId As Integer = 0
    Dim i, j, k, l As Integer
    Dim strsql As String
    Dim objdo As New ETWFTransaction
    Dim dt1, dt2 As New DataTable
    Dim roleid As Integer
    Dim grp As String
    Dim FileNamePath As String
    Dim FileNameOnly As String
    Dim ReFileName As String
    Dim objBOM As New BOMailReport
    Dim objET As New ETAuditTrail
    Dim objBOAT As New BoAuditTrail
    Dim objET1 As New ETSiteDoc
    Dim objdb As New DBUtil
    Dim objmail As New TakeMail
    Dim dbutils_nsn As New DBUtils_NSN

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        btnWS.Attributes.Add("onclick", "javascript:popUpCalendar(this,txtWorkStarted,'dd-mmm-yyyy');return false;")
        btnWF.Attributes.Add("onclick", "javascript:popUpCalendar(this,txtWorkShouldFinished,'dd-mmm-yyyy');return false;")
        btnWHF.Attributes.Add("onclick", "javascript:popUpCalendar(this,txtWorkHasBeenFinished,'dd-mmm-yyyy');return false;")
        btnWSBaut.Attributes.Add("onclick", "javascript:popUpCalendar(this,txtWorkShouldFinishedbaut,'dd-mmm-yyyy');return false;")
        btnWFBaut.Attributes.Add("onclick", "javascript:popUpCalendar(this,txtKPIAccepted,'dd-mmm-yyyy');return false;")
        If Not IsPostBack Then
            If Not Request.QueryString("id") Is Nothing Then
                binddata()
                FillData()
                GetBautWCTR()
                FillDataBAUTWCTR()
            End If
        End If
        txtWorkHasBeenFinished.Attributes.Add("readonly", "false")
        txtWorkHasBeenFinished.Attributes.Add("onblur", "javascript:actualDurationBaut();")
        txtWorkHasBeenFinished.Attributes.Add("readonly", "true")
        btnGenerate.Attributes.Add("onclick", "javascript:actualDurationBaut();")
        If Request.QueryString("from") = "bast" Then
            btnGenerate_Click(Nothing, Nothing)
        ElseIf Request.QueryString("from") = "wctr" Then
            txtWorkHasBeenFinished.Attributes.Add("readonly", "true")
            btnWHF.Visible = False
        End If
    End Sub
    Sub FillDataBAUTWCTR()
        Dim str1 As String = "EXEC [uspGetODWCTRBautBAST] " & Request.QueryString("id")
        dt2 = objUtil.ExeQueryDT(str1, "ODWCTRBAST")
        If dt2.Rows.Count > 0 Then
            txtKPIAccepted.Value = dt2.Rows(0).Item("wskpiapproval").ToString
            txtReasonBaut1.Value = dt2.Rows(0).Item("DotherR1baut").ToString
            txtReasonDaysBaut1.Value = dt2.Rows(0).Item("DotherR1daysbaut").ToString
            txtReasonBaut2.Value = dt2.Rows(0).Item("DotherR2baut").ToString
            txtReasonDaysBaut2.Value = dt2.Rows(0).Item("DotherR2daysbaut").ToString
            txtReasonBaut3.Value = dt2.Rows(0).Item("DotherR3baut").ToString
            txtReasonDaysBaut3.Value = dt2.Rows(0).Item("DotherR3daysbaut").ToString
            txtTotalC.Value = dt2.Rows(0).Item("Totalc").ToString
        End If
    End Sub
    Sub GetBautWCTR()
        Dim str As String
        str = "Exec [uspWCTROnLine] " & Request.QueryString("id")
        dt = objdb.ExeQueryDT(str, "SiteDoc1")
        If (dt.Rows.Count > 0) Then
            Dim url As String = "../PO/frmViewDocument.aspx?id=" & dt.Rows(0)("sw_id").ToString
            lblLinks.Text = "<a href='#' onclick=""window.open('" & url & "','mywindow','status=yes,menubar=no,scrollbars=yes,resizable=yes,width=700px,height=600px')"">View BAUT WCTR</a>"
            lblLinks.Visible = True
            'dedy 091106
            str = "Exec [uspGetOnLineFormBindNew] " & dt.Rows(0).Item("SW_Id").ToString & "," & dt.Rows(0).Item("WF_Id").ToString & "," & dt.Rows(0).Item("SiteId").ToString & "," & ConfigurationManager.AppSettings("BAUTID")
            dt2 = objUtil.ExeQueryDT(str, "SiteDoc1")
            '-------------New Code 05 Januari 2012 --------
            Dim dtSignPosition As DataView = dt2.DefaultView
            dtSignPosition.Sort = "tsk_id desc"
            'DDBindSign.DataSource = dt2
            DDBindSign.DataSource = dtSignPosition
            DDBindSign.DataBind()
            '----------------------------------------------
            
            'bugfix100818 new request to link for the date under telkomsel signatured
            For n As Integer = 0 To dt2.Rows.Count - 1
                If dt2.Rows(n).Item("tsk_id").ToString() = "4" Then
                    txtWorkShouldFinishedbaut.Value = dt2.Rows(n).Item("apptime").ToString
                End If
            Next
        Else
            lblLinks.Visible = False
        End If
    End Sub
    Sub FillData()
        str1 = "EXEC uspGetODWCTRBAST " & Request.QueryString("id")
        dt2 = objUtil.ExeQueryDT(str1, "ODWCTRBAST")
        If dt2.Rows.Count > 0 Then
            'txtDay.Value = dt2.Rows(0).Item("wday").ToString
            'txtDate.Value = dt2.Rows(0).Item("wDate").ToString
            'txtMonth.Value = dt2.Rows(0).Item("wmonth").ToString
            'txtYear.Value = dt2.Rows(0).Item("wyear").ToString
            txtDay.Value = objUtil.ExeQueryScalarString("select dbo.GetWeekDayNameOfDate(GETDATE()) as WeekDayName")
            txtDate.Value = objUtil.ExeQueryScalar("select day(getdate())").ToString()
            txtMonth.Value = objUtil.ExeQueryScalar("select month(getdate())").ToString()
            txtYear.Value = objUtil.ExeQueryScalar("select year(getdate())").ToString()
            txtDurationExec.Value = dt2.Rows(0).Item("durationexec").ToString
            txtWorkStarted.Value = dt2.Rows(0).Item("wstart").ToString
            txtWorkShouldFinished.Value = dt2.Rows(0).Item("wsfinish").ToString
            txtWorkHasBeenFinished.Value = dt2.Rows(0).Item("BASTSubDate").ToString
            txtActualExec.Value = dt2.Rows(0).Item("actualexec").ToString
            Dim strone As String = dt2.Rows(0).Item("wstart").ToString
            Dim strtwo As String = dt2.Rows(0).Item("wsfinish").ToString
            Dim strthree As String = dt2.Rows(0).Item("BASTSubDate").ToString
            Dim dt As New DateTime
            If DateTime.TryParse(strthree, dt) Then
                Dim days As Integer = dt.Subtract(strone).TotalDays
                txtActualExec.Value = days.ToString
                Dim dt1 As New DateTime
                If DateTime.TryParse(strthree, dt1) Then
                    txtTotalA.Value = (Convert.ToInt32(IIf(txtDurationExec.Value = "", 0, txtDurationExec.Value)) - Convert.ToInt32(IIf(txtActualExec.Value = "", 0, txtActualExec.Value))).ToString
                End If
                If (Convert.ToInt32(txtTotalA.Value) < 0) Then
                    lblDelay.Text = "Delay (A)"
                Else
                    lblDelay.Text = "Acceleration (A)"
                End If
            End If
            txtReason1.Value = dt2.Rows(0).Item("DotherR1").ToString
            txtReasonDays1.Value = dt2.Rows(0).Item("DotherR1days").ToString
            txtReason2.Value = dt2.Rows(0).Item("DotherR2").ToString
            txtReasonDays2.Value = dt2.Rows(0).Item("DotherR2days").ToString
            txtReason3.Value = dt2.Rows(0).Item("DotherR3").ToString
            txtReasonDays3.Value = dt2.Rows(0).Item("DotherR3days").ToString
            txtTotalB.Value = (Convert.ToInt32(txtReasonDays1.Value) + Convert.ToInt32(IIf(txtReasonDays2.Value = "", 0, txtReasonDays2.Value)) + Convert.ToInt32(IIf(txtReasonDays3.Value = "", 0, txtReasonDays3.Value))).ToString
            txtWorkShouldFinishedbaut.Value = dt2.Rows(0).Item("wsfinishbaut").ToString
            txtKPIAccepted.Value = dt2.Rows(0).Item("wskpiapproval").ToString
            txtReasonBaut1.Value = dt2.Rows(0).Item("DotherR1baut").ToString
            txtReasonDaysBaut1.Value = dt2.Rows(0).Item("DotherR1daysbaut").ToString
            txtReasonBaut2.Value = dt2.Rows(0).Item("DotherR2baut").ToString
            txtReasonDaysBaut2.Value = dt2.Rows(0).Item("DotherR2daysbaut").ToString
            txtReasonBaut3.Value = dt2.Rows(0).Item("DotherR3baut").ToString
            txtReasonDaysBaut3.Value = dt2.Rows(0).Item("DotherR3daysbaut").ToString
            txtTotalC.Value = dt2.Rows(0).Item("totalc").ToString
            txtJobDelay.Value = dt2.Rows(0).Item("totald").ToString
            If Convert.ToInt32(IIf(txtJobDelay.Value = "", 0, txtJobDelay.Value)) >= 0 Then
                tdaccelerate.InnerHtml = "Total period of work completion has been <b>accelerated</b> :"
            Else
                tdaccelerate.InnerHtml = "Total period of work completion has been <b>delayed</b> :"
            End If
            bastsublabel.Text = dt2.Rows(0).Item("BASTSubDate").ToString
        Else
            txtDay.Value = objUtil.ExeQueryScalarString("select datename(dw,getdate())")
            txtDate.Value = DateTime.Now.Day
            txtMonth.Value = DateTime.Now.Month
            txtYear.Value = DateTime.Now.Year
        End If
    End Sub
    Sub binddata()
        str = "uspBautOnLine " & Request.QueryString("id")
        dt = objUtil.ExeQueryDT(str, "SiteDoc")
        If dt.Rows.Count > 0 Then
            lblSite.Text = dt.Rows(0).Item("site_no").ToString & "/" & dt.Rows(0).Item("site_name").ToString
            hdnSiteno.Value = dt.Rows(0).Item("site_no").ToString
            lblPO.Text = dt.Rows(0).Item("pono").ToString
            lblScope.Text = dt.Rows(0).Item("Scope").ToString
            hdnsiteid.Value = dt.Rows(0).Item("SiteId").ToString
            hdnversion.Value = dt.Rows(0).Item("version").ToString
            hdnWfId.Value = dt.Rows(0).Item("WF_Id").ToString
            hdndocId.Value = dt.Rows(0).Item("docId").ToString
            hdnScope.Value = dt.Rows(0).Item("Scope").ToString
            hdnDGBox.Value = dt.Rows(0).Item("DGBox").ToString
            lblProjectidwctr.Text = dt.Rows(0).Item("projectid").ToString
            If (dt.Rows(0).Item("CustomerPoReceiveDate") IsNot Nothing) Then
                'txtWorkStarted.Attributes.Add("readonly", "readonly")
                txtWorkStarted.Value = dt.Rows(0).Item("CustomerPoReceiveDate").ToString
                btnWS.Visible = True
            End If

            If Request.QueryString("from") <> "bast" Then
                'i = objBO.uspCheckIntegration(hdndocId.Value, hdnSiteno.Value)
                i = objdb.ExeQueryScalar("exec uspCheckIntegration  '" & hdndocId.Value & "' ,'" & hdnSiteno.Value & "'," & hdnversion.Value & "")
                Select Case i
                    Case 1 Or 3
                        Dochecking()
                    Case 2
                        hdnKeyVal.Value = 0
                        'ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "Showmain('Int');", True)
                    Case 4
                        hdnKeyVal.Value = 0
                        'ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "Showmain('IntD');", True)
                End Select
            ElseIf Request.QueryString("from") <> "wctr" Then
                'i = objBO.uspCheckIntegration(hdndocId.Value, hdnSiteno.Value)
                i = objdb.ExeQueryScalar("exec uspCheckIntegration  '" & hdndocId.Value & "' ,'" & hdnSiteno.Value & "'," & hdnversion.Value & "")
                Select Case i
                    Case 1 Or 3
                        Dochecking()
                    Case 2
                        hdnKeyVal.Value = 0
                        'ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "Showmain('Int');", True)
                    Case 4
                        hdnKeyVal.Value = 0
                        'ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "Showmain('IntD');", True)
                End Select
            Else
                hdnKeyVal.Value = 2
            End If
            str = "Exec [uspGetOnLineFormBind] " & dt.Rows(0).Item("WF_Id").ToString & "," & dt.Rows(0).Item("SiteId").ToString
            dt = objUtil.ExeQueryDT(str, "SiteDoc1")
            DLDigitalSign.DataSource = dt
            DLDigitalSign.DataBind()
            HDDgSignTotal.Value = dt.Rows.Count
            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScriptdd", "getControlPostion();", True)
        Else
            hdnsiteid.Value = 0
            hdnversion.Value = 0
            hdnWfId.Value = 0
            hdndocId.Value = 0
        End If
    End Sub
    Function CreatePDFFile(ByVal StrPath As String) As String
        'bugfix101118 refill the textbox before converting into pdf
        FillData()
        StrPath = StrPath.Replace(" \", "\").ToString 'bugfix100816 causing error when the path containing space before \
        Dim dtba As New DataTable
        Dim filenameorg As String
        Dim ReFileName As String
        filenameorg = hdnSiteno.Value & "-WCTR-" & Format(CDate(DateTime.Now), "ddMMyyyyHHss")
        ReFileName = filenameorg & ".htm"
        If (System.IO.File.Exists(StrPath & ReFileName)) Then
            System.IO.File.Delete(StrPath & ReFileName)
        End If
        If Not System.IO.Directory.Exists(StrPath) Then
            System.IO.Directory.CreateDirectory(StrPath)
        End If
        Dim sw As HtmlTextWriter = New HtmlTextWriter(New StreamWriter(StrPath & ReFileName, False, System.Text.UnicodeEncoding.UTF8))
        sw.WriteLine("<html><head><style type=""text/css"">")
        sw.WriteLine("tr{padding: 3px;}")
        sw.WriteLine(".MainCSS{margin-bottom: 0px;margin-left: 20px;margin-right: 20px;margin-top: 0px;width: 800px;height: 700px;text-align: center;}")
        sw.WriteLine(".lblText{font-family: verdana;font-size: 8pt;color: #000000;text-align: left;}")
        sw.WriteLine(".lblBText{font-family: verdana;font-size: 8pt;color: #000000;text-align: left;font-weight: bold;}")
        sw.WriteLine(".lblBold{font-family: verdana;font-size: 12pt;color: #000000;font-weight: bold;}")
        sw.WriteLine(".textFieldStyle{background-color: white;border: 1px solid;color: black;font-family: verdana;font-size: 9pt;}")
        sw.WriteLine(".GridHeader{color: #0e1b42;background-color: Orange;font-size: 9pt;font-family: verdana;text-align: Left;vertical-align: bottom;font-weight: bold;}")
        sw.WriteLine(".GridEvenRows{background-color: #e3e3e3;vertical-align: top;font-family: verdana;font-size: 8pt;color: #000000;}")
        sw.WriteLine(".GridOddRows{background-color: white;vertical-align: top;font-family: verdana;font-size: 8pt;color: #000000;}")
        sw.WriteLine(".PagerTitle{font-size: 8pt;background-color: #cddbbf;text-align: Right;vertical-align: middle;color: #25375b;font-weight: bold;}")
        sw.WriteLine(".Hcap{height: 5px;}")
        sw.WriteLine(".VCap{width: 10px;}")
        sw.WriteLine(".lblBBTextL{border-bottom: 1px #000 solid;border-left: 1px #000 solid;border-top: 1px #000 solid;font-family: verdana;font-size: 8pt;color: #000000;text-align: left;font-weight: bold;text-decoration: none;}")
        sw.WriteLine(".lblBBTextM{border-bottom: 1px #000 solid;border-top: 1px #000 solid;font-family: verdana;font-size: 8pt;color: #000000;text-align: left;font-weight: normal;text-decoration: none;width: 1%;}")
        sw.WriteLine(".lblBBTextR{border-right: 1px #000 solid;border-bottom: 1px #000 solid;border-top: 1px #000 solid;font-family: verdana;font-size: 8pt;color: #000000;text-align: left;font-weight: normal;text-decoration: none;}")
        sw.WriteLine("#lblTotalA{font-weight: bold;}")
        sw.WriteLine("#lblJobDelay{font-weight: bold;}")
        sw.WriteLine("</style></head>")
        sw.WriteLine("<body class=""MainCSS"">")
        dvPrint.RenderControl(sw)
        sw.WriteLine("</body>")
        sw.WriteLine("</html>")
        sw.Close()
        sw.Dispose()
        Return Common.TakeFileUpload.ConvertAnyFormatToPDFHtmlNew(StrPath & ReFileName, StrPath, filenameorg)
    End Function
    Function DOInsertTrans(ByVal siteid As String, ByVal docName As String, ByVal version As Integer, ByVal path As String) As String
        docId = hdndocId.Value
        Dim dtNew As DataTable
        If hdnWfId.Value <> 0 Then
            dtNew = objBO.doinserttrans(hdnWfId.Value, docId)
            If hdnDGBox.Value = True Then
                If dtNew.Rows.Count > 0 Then
                    If Request.QueryString("from") = "bast" Then
                        'objBO.DelWFTransaction(hdndocId.Value, dtNew.Rows(0).Item(1).ToString, hdnsiteid.Value, hdnversion.Value)
                    ElseIf Request.QueryString("from") = "wctr" Then
                        objBO.DelWFTransaction(hdndocId.Value, dtNew.Rows(0).Item(1).ToString, hdnsiteid.Value, hdnversion.Value)
                    Else
                        objBO.DelWFTransaction(hdndocId.Value, dtNew.Rows(0).Item(1).ToString, hdnsiteid.Value, hdnversion.Value)
                    End If
                    Dim aa As Integer = 0
                    Dim status As Integer
                    For aa = 0 To dtNew.Rows.Count - 1
                        fillDetails()
                        If (dtNew.Rows(aa).Item(5).ToString <> "1") Then status = 1
                        objdo.Status = status
                        objdo.UserType = dtNew.Rows(aa).Item(3).ToString
                        objdo.UsrRole = dtNew.Rows(aa).Item(4).ToString
                        objdo.WFId = dtNew.Rows(aa).Item(1).ToString
                        objdo.UGP_Id = dtNew.Rows(aa).Item("grpId").ToString
                        objdo.TSK_Id = dtNew.Rows(aa).Item(5).ToString
                        objdo.XVal = dtNew.Rows(aa).Item("X_Coordinate").ToString
                        objdo.YVal = dtNew.Rows(aa).Item("Y_Coordinate").ToString
                        objdo.PageNo = dtNew.Rows(aa).Item("PageNo").ToString
                        If Request.QueryString("from") = "bast" Then
                            objBO.uspwftransactionIU(objdo)
                        Else
                            'just update only rstatus
                            objUtil.ExeNonQuery("update wftransaction set rstatus=2 where docid=" & hdndocId.Value & " and site_id=" & hdnsiteid.Value & " and siteversion =" & hdnversion.Value & " ")
                        End If
                    Next
                End If
                Return "1"
            Else
                CreateXY()
                Return "1"
            End If
        Else
            Dim status As Integer = 99
            'means there no approval process for this document 'no workflow
            'objboSite.uspwftransactionNOTWFI(ddldocument.SelectedItem.Value, 0, ddlsite.SelectedItem.Value, hdnversion.Value, 1, Session("User_Id"), status, 2, Session("User_Name"))
            objBO.uspwftransactionNOTWFI(docId, 0, hdnsiteid.Value, hdnversion.Value, 1, Session("User_Id"), status, 2, Session("User_Name"))
            Return "0"
        End If
    End Function
    Sub CreateXY()
        Dim dtNew As DataTable
        Dim strSql1 As String = "select wfdid,wfid,sorder,grpcode,roleid,a.tsk_id,a.grpId,tt.tskname,0 as X_Coordinate,0 as Y_Coordinate," & _
        "0 as PageNo,IsNull(a.Sorder,0)Sno from twfdefinition A inner join tusertype B on a.grpid=b.grpid inner join ttask as tt on tt.tsk_id=a.tsk_id " & _
        "where tt.TSK_Id=1 and wfid=" & hdnWfId.Value & " order by wfdid"
        dtNew = objUtil.ExeQueryDT(strSql1, "dd")
        If dtNew.Rows.Count > 0 Then
            objBO.DelWFTransaction(hdndocId.Value, dtNew.Rows(0).Item(1).ToString, hdnsiteid.Value, hdnversion.Value)
        End If
        Dim status As Integer = 0
        Dim DtNewOne As DataTable
        Dim dvIn As New DataView
        Dim dvNotIn As New DataView
        Dim J As Integer = 1
        Dim Y As Integer = 0, TopK As Integer = 0
        dvIn = dtNew.DefaultView
        dvIn.RowFilter = "TSK_Id=1"
        For TopK = 0 To dvIn.Count - 1
            fillDetails()
            objdo.Status = status
            objdo.UserType = dvIn(TopK).Item(3).ToString
            objdo.UsrRole = dvIn(TopK).Item(4).ToString
            objdo.WFId = dvIn(TopK).Item(1).ToString
            objdo.TSK_Id = dvIn(TopK).Item(5).ToString
            objdo.UGP_Id = dvIn(TopK).Item("grpId").ToString
            objdo.XVal = 0
            objdo.YVal = 0
            objdo.PageNo = 0
            objdo.Site_Id = hdnsiteid.Value
            objdo.Status = status
            objBO.uspwftransactionIU(objdo)
        Next
        strSql1 = "select wfdid,wfid,sorder,grpcode,roleid,a.tsk_id,a.grpId,tt.tskname,0 as X_Coordinate,0 as Y_Coordinate," & _
        "0 as PageNo,IsNull(a.Sorder,0)Sno from twfdefinition A inner join tusertype B on a.grpid=b.grpid inner join ttask as tt on tt.tsk_id=a.tsk_id " & _
        " where tt.TSK_Id <> 1 and wfid=" & hdnWfId.Value & " order by sorder"
        DtNewOne = objUtil.ExeQueryDT(strSql1, "dd")
        dvNotIn = DtNewOne.DefaultView
        dvNotIn.RowFilter = "TSK_Id <>1"
        status = 1
        Dim intCount As Integer = 0, IncrMentY As Integer = 0
        For IncrMentX As Integer = 0 To dvNotIn.Count - 1
            fillDetails()
            objdo.Status = status
            objdo.UserType = dvNotIn(IncrMentX).Item(3).ToString
            objdo.UsrRole = dvNotIn(IncrMentX).Item(4).ToString
            objdo.WFId = dvNotIn(IncrMentX).Item(1).ToString
            objdo.TSK_Id = dvNotIn(IncrMentX).Item(5).ToString
            objdo.UGP_Id = dvNotIn(IncrMentX).Item("grpId").ToString
            objdo.XVal = 0
            objdo.YVal = 0
            objdo.PageNo = 0
            objdo.Site_Id = hdnsiteid.Value
            objBO.uspwftransactionIU(objdo)
        Next
        Dim strsql2 As String
        Dim dtnew2 As DataTable
        Dim dvnotin2 As DataView
        Dim tcount As Integer = 0, IncrY As Integer = 0
        strsql2 = "select wfdid,wfid,sorder,grpcode,roleid,a.tsk_id,a.grpId,tt.tskname,0 as X_Coordinate,0 as Y_Coordinate," & _
        "0 as PageNo,IsNull(a.Sorder,0)Sno from twfdefinition A inner join tusertype B on a.grpid=b.grpid inner join ttask as tt on tt.tsk_id=a.tsk_id " & _
        " where tt.TSK_Id not in (1,5) and wfid=" & hdnWfId.Value & " order by wfdid"
        dtnew2 = objUtil.ExeQueryDT(strsql2, "dd2")
        dvnotin2 = dtnew2.DefaultView
        dvnotin2.RowFilter = "TSK_Id <>1"
        status = 1
        For IncrMentX As Integer = 0 To dvnotin2.Count - 1
            Dim iHDX As HiddenField = CType(DLDigitalSign.Items(IncrMentX).FindControl("hdXCoordinate"), HiddenField)
            Dim iHDY As HiddenField = CType(DLDigitalSign.Items(IncrMentX).FindControl("hdYCoordinate"), HiddenField)
            objdo.DocId = hdndocId.Value
            objdo.Site_Id = hdnsiteid.Value
            objdo.SiteVersion = hdnversion.Value
            objdo.Status = status
            objdo.UserType = dvnotin2(IncrMentX).Item(3).ToString
            objdo.UsrRole = dvnotin2(IncrMentX).Item(4).ToString
            objdo.WFId = dvnotin2(IncrMentX).Item(1).ToString
            objdo.TSK_Id = dvnotin2(IncrMentX).Item(5).ToString
            objdo.UGP_Id = dvnotin2(IncrMentX).Item("grpId").ToString
            Dim introw As Integer = 0
            If Request.QueryString("rw") IsNot Nothing Then
                introw = Request.QueryString("rw")
            End If
            Dim strversion = Request.Browser.Type
            'bugfix101027 manual overrride of xy values
            If InStr(strversion, "IE6") > 0 Then
                objdo.XVal = 42
                objdo.YVal = 182
            ElseIf InStr(strversion, "IE7") > 0 Then
                objdo.XVal = 42
                objdo.YVal = 182
            ElseIf InStr(strversion, "IE8") > 0 Then
                objdo.XVal = 42
                objdo.YVal = 182
            ElseIf InStr(strversion, "Fire") > 0 Then
                objdo.XVal = 42
                objdo.YVal = 182
            Else
                objdo.XVal = 42
                objdo.YVal = 182
            End If
            Y = (Math.Ceiling(iHDY.Value / 791))
            If (IncrMentX > 0) Then
                If (IncrMentX Mod 2) = 0 Then
                    tcount = 0
                    IncrY = IncrY + 1
                Else
                    tcount = tcount + 1
                End If
            Else
                tcount = tcount + 1
            End If
            If Y = 0 Then Y = 1
            Y = 1
            objdo.PageNo = Y
            objdo.Site_Id = hdnsiteid.Value
            objdb.ExeNonQuery("update wftransaction set xval=" & objdo.XVal & ",yval=" & objdo.YVal & ", pageno=" & objdo.PageNo & " where site_id= " & objdo.Site_Id & " and siteversion= " & objdo.SiteVersion & " and docid= " & objdo.DocId & " and tsk_id=" & objdo.TSK_Id)
        Next
    End Sub
    Sub fillDetails()
        objdo.DocId = hdndocId.Value
        objdo.Site_Id = hdnsiteid.Value
        'objdo.SiteVersion = Request.QueryString("version")
        objdo.SiteVersion = hdnversion.Value
        If Request.QueryString("from") = "bast" Then
            objdo.AT.RStatus = Constants.STATUS_ACTIVE
        ElseIf Request.QueryString("from") = "wctr" Then
            objdo.AT.RStatus = Constants.STATUS_ACTIVE
        Else
            objdo.AT.RStatus = 4
        End If
        objdo.AT.LMBY = Session("User_Name")
    End Sub
    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        Return
    End Sub
    Public Sub chek4alldoc()
        Dim i As Integer
        i = objBO.chek4alldoc(hdnsiteid.Value, hdnversion.Value)
        If i = 0 Then
            hdnready4baut.Value = 1
            'insert to new table for report as final document uploaded
            objBO.uspRPTUpdate(lblPO.Text, hdnSiteno.Value)
        Else
            hdnready4baut.Value = 0
        End If
    End Sub
    Sub AuditTrail()
        objET.PoNo = lblPO.Text
        objET.SiteId = hdnsiteid.Value
        objET.DocId = hdndocId.Value
        objET.Task = "1"
        objET.Status = "1"
        objET.Userid = Session("User_Id")
        objET.Roleid = Session("Role_Id")
        objET.fldtype = hdnScope.Value
        objBOAT.uspAuditTrailI(objET)
    End Sub
    Sub uploaddocument(ByVal vers As Integer, ByVal keyval As Integer)
        dt = objBO.getbautdocdetailsNEW(hdndocId.Value)
        Dim sec As String = dt.Rows(0).Item("sec_name").ToString
        Dim subsec As String = dt.Rows(0).Item("subsec_name").ToString
        Dim secpath As String = ""
        Dim ft As String = ""
        Dim path As String = ""
        Dim filenameorg As String
        FileNameOnly = "-" & Constants._Doc_WCTR & "-"
        filenameorg = hdnSiteno.Value & FileNameOnly & Format(CDate(DateTime.Now), "ddMMyyyyHHss")
        ReFileName = filenameorg & ".htm"
        secpath = IIf(subsec = "" Or subsec = "0", sec & "\", sec & "\" & subsec & "\")
        ft = ConfigurationManager.AppSettings("Type") & lblPO.Text & "-" & hdnScope.Value & "\"
        path = ConfigurationManager.AppSettings("Fpath") & hdnSiteno.Value & ft & secpath
        Dim strResult As String = ""
        strResult = DOInsertTrans(hdnsiteid.Value, hdndocId.Value, vers, path)
        Dim DocPath As String = ""
        If strResult = "0" Then
            DocPath = hdnSiteno.Value & ft & secpath & CreatePDFFile(Replace(path, "  \", "\"))
        ElseIf strResult = "1" Then
            DocPath = hdnSiteno.Value & ft & secpath & CreatePDFFile(Replace(path, "  \", "\"))
        Else
            DocPath = hdnSiteno.Value & ft & secpath & CreatePDFFile(Replace(path, "  \", "\"))
        End If
        With objET1
            .SiteID = hdnsiteid.Value
            .DocId = hdndocId.Value
            .IsUploded = 1
            .Version = vers
            .keyval = keyval
            If Request.QueryString("from") = "bast" Then
                .DocPath = DocPath
                .keyval = 2
                .orgDocPath = hdnSiteno.Value & ft & secpath & ReFileName
            ElseIf Request.QueryString("from") = "wctr" Then
                .DocPath = DocPath
                .orgDocPath = hdnSiteno.Value & ft & secpath & ReFileName
                objdb.ExeNonQuery("update sitedoc set rstatus=2 where siteid = " & hdnsiteid.Value & " and version= " & vers & " and docid=" & hdndocId.Value.ToString)  '1034
            Else
                .DocPath = DocPath
                .orgDocPath = hdnSiteno.Value & ft & secpath & ReFileName
            End If
            .AT.RStatus = Constants.STATUS_ACTIVE
            .AT.LMBY = Session("User_Name")
            .PONo = lblPO.Text
        End With
        objBO.updatedocupload(objET1)
        'Fill Transaction table 
        chek4alldoc() ' for messaage to previous screen and saving final docupload date in reporttable
        'for BAUT
        objBO.check4BAUTBAST(hdnsiteid.Value, hdnversion.Value, 1, lblPO.Text, Session("User_Name"), ConfigurationManager.AppSettings("BAUTID"), ConfigurationManager.AppSettings("BASTID"))
        'for BAST1
        objBO.check4BAUTBAST(hdnsiteid.Value, hdnversion.Value, 2, lblPO.Text, Session("User_Name"), ConfigurationManager.AppSettings("BAUTID"), ConfigurationManager.AppSettings("BASTID"))
        AuditTrail()
    End Sub
    Protected Sub btnGenerate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGenerate.Click
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        If Request.Browser.Browser = "IE" Then
            Dim result As Integer = 0
            Dim strsql As String = "EXEC uspODWctrBASTI " & Request.QueryString("id") & ",'" & lblPO.Text & "','" & hdnsiteid.Value & "'," & hdnversion.Value & "," & _
            "'" & txtDay.Value & "'," & IIf(txtDate.Value <> "", txtDate.Value, 0) & "," & IIf(txtMonth.Value <> "", txtMonth.Value, 0) & "," & IIf(txtYear.Value <> "", txtYear.Value, 0) & "," & IIf(lblProjectidwctr.Text <> "", "'" & lblProjectidwctr.Text & "'", "''") & "," & _
            IIf(txtDurationExec.Value <> "", txtDurationExec.Value, 0) & "," & IIf(txtWorkStarted.Value.ToString <> "", "'" & txtWorkStarted.Value & "'", "NULL") & "," & _
            IIf(txtWorkShouldFinished.Value.ToString <> "", "'" & txtWorkShouldFinished.Value & "'", "NULL") & "," & IIf(txtWorkHasBeenFinished.Value.ToString <> "", "'" & _
            txtWorkHasBeenFinished.Value & "'", "NULL") & "," & IIf(txtActualExec.Value <> "", txtActualExec.Value, 0) & "," & _
            IIf(txtTotalA.Value <> "", txtTotalA.Value, 0) & ",'" & txtReason1.Value & "'," & IIf(txtReasonDays1.Value <> "", txtReasonDays1.Value, 0) & ",'" & _
            txtReason2.Value & "'," & IIf(txtReasonDays2.Value <> "", txtReasonDays2.Value, 0) & ",'" & txtReason3.Value & "'," & _
            IIf(txtReasonDays3.Value <> "", txtReasonDays3.Value, 0) & "," & IIf(txtTotalB.Value <> "", txtTotalB.Value, 0) & "," & _
            IIf(txtWorkShouldFinishedbaut.Value.ToString <> "", "'" & txtWorkShouldFinishedbaut.Value & "'", "null") & "," & IIf(txtKPIAccepted.Value.ToString <> "", " '" & txtKPIAccepted.Value & "'", "null") & ",'" & _
            txtReasonBaut1.Value & "'," & IIf(txtReasonDaysBaut1.Value <> "", txtReasonDaysBaut1.Value, 0) & ",'" & txtReasonBaut2.Value & "'," & IIf(txtReasonDaysBaut2.Value <> "", txtReasonDaysBaut2.Value, 0) & ",'" & txtReasonBaut3.Value & "'," & IIf(txtReasonDaysBaut3.Value <> "", txtReasonDaysBaut3.Value, 0) & "," & _
            IIf(txtTotalC.Value <> "", txtTotalC.Value, 0) & "," & IIf(txtJobDelay.Value <> "", txtJobDelay.Value, 0) & ",'" & Session("User_Name") & "','" & DateTime.Now & "'," & IIf(bastsublabel.Text <> "", "'" & bastsublabel.Text & "'", "null")
            result = objUtil.ExeQueryScalar(strsql)
            If result = 1 Then
                If hdnDGBox.Value = True Then
                    strsql = "select count(*) from docSignPositon where doc_id=" & hdndocId.Value
                    If objUtil.ExeQueryScalar(strsql) > 0 Then
                        If lblProjectidwctr.Text = "" Then lblProjectidwctr.Text = ""
                        lblDay.Text = txtDay.Value
                        If lblDay.Text = "" Then lblDay.Text = ""
                        lblDate.Text = txtDate.Value
                        If lblDate.Text = "" Then lblDate.Text = ""
                        lblMonth.Text = txtMonth.Value
                        If lblMonth.Text = "" Then lblMonth.Text = ""
                        lblYear.Text = txtYear.Value
                        If lblYear.Text = "" Then lblYear.Text = ""
                        lblDurationExec.Text = txtDurationExec.Value
                        If lblDurationExec.Text = "" Then lblDurationExec.Text = ""
                        lblWrkStarted.Text = txtWorkStarted.Value
                        If lblWrkStarted.Text = "" Then lblWrkStarted.Text = ""
                        lblWorkShouldFinished.Text = txtWorkShouldFinished.Value
                        If lblWorkShouldFinished.Text = "" Then lblWorkShouldFinished.Text = ""
                        lblWorkhasbeenFinished.Text = txtWorkHasBeenFinished.Value
                        If lblWorkhasbeenFinished.Text = "" Then lblWorkhasbeenFinished.Text = ""
                        lblActualExec.Text = txtActualExec.Value
                        If lblActualExec.Text = "" Then lblActualExec.Text = ""
                        lblTotalA.Text = txtTotalA.Value
                        If lblTotalA.Text = "" Then lblTotalA.Text = ""
                        lblReason1.Text = txtReason1.Value
                        If lblReason1.Text = "" Then lblReason1.Text = ""
                        lblReason2.Text = txtReason2.Value
                        If lblReason2.Text = "" Then lblReason2.Text = ""
                        lblReason3.Text = txtReason3.Value
                        If lblReason3.Text = "" Then lblReason3.Text = ""
                        lblReasonDays1.Text = txtReasonDays1.Value
                        If lblReasonDays1.Text = "" Then lblReasonDays2.Text = ""
                        lblReasonDays2.Text = txtReasonDays2.Value
                        If lblReasonDays2.Text = "" Then lblReasonDays2.Text = ""
                        lblReasonDays3.Text = txtReasonDays3.Value
                        If lblReasonDays3.Text = "" Then lblReasonDays3.Text = ""
                        lblTotalB.Text = txtTotalB.Value
                        If lblTotalB.Text = "" Then lblTotalB.Text = ""
                        lblWorkShouldFinishedbaut.Text = txtWorkShouldFinishedbaut.Value
                        If lblWorkShouldFinishedbaut.Text = "" Then lblWorkShouldFinishedbaut.Text = ""
                        lblKPIAccepted.Text = txtKPIAccepted.Value
                        If lblKPIAccepted.Text = "" Then lblKPIAccepted.Text = ""
                        lblReasonBaut1.Text = txtReasonBaut1.Value
                        If lblReasonBaut1.Text = "" Then lblReasonBaut1.Text = ""
                        lblReasonBaut2.Text = txtReasonBaut2.Value
                        If lblReasonBaut2.Text = "" Then lblReasonBaut2.Text = ""
                        lblReasonBaut3.Text = txtReasonBaut3.Value
                        If lblReasonBaut3.Text = "" Then lblReasonBaut3.Text = ""
                        lblReasonDaysBaut1.Text = txtReasonDaysBaut1.Value
                        If lblReasonDaysBaut1.Text = "" Then lblReasonDaysBaut2.Text = ""
                        lblReasonDaysBaut2.Text = txtReasonDaysBaut2.Value
                        If lblReasonDaysBaut2.Text = "" Then lblReasonDaysBaut2.Text = ""
                        lblReasonDaysBaut3.Text = txtReasonDaysBaut3.Value
                        If lblReasonDaysBaut3.Text = "" Then lblReasonDaysBaut3.Text = ""
                        lblTotalC.Text = txtTotalC.Value
                        If lblTotalC.Text = "" Then lblTotalC.Text = ""
                        lblJobDelay.Text = txtJobDelay.Value
                        If lblJobDelay.Text = "" Then lblJobDelay.Text = ""
                        txtDay.Visible = False
                        txtDate.Visible = False
                        txtMonth.Visible = False
                        txtYear.Visible = False
                        txtDurationExec.Visible = False
                        txtWorkStarted.Visible = False
                        txtWorkShouldFinished.Visible = False
                        txtWorkHasBeenFinished.Visible = False
                        txtActualExec.Visible = False
                        txtTotalA.Visible = False
                        txtReason1.Visible = False
                        txtReason2.Visible = False
                        txtReason3.Visible = False
                        txtReasonDays1.Visible = False
                        txtReasonDays2.Visible = False
                        txtReasonDays3.Visible = False
                        txtTotalB.Visible = False
                        txtWorkShouldFinishedbaut.Visible = False
                        txtKPIAccepted.Visible = False
                        txtReasonBaut1.Visible = False
                        txtReasonBaut2.Visible = False
                        txtReasonBaut3.Visible = False
                        txtReasonDaysBaut1.Visible = False
                        txtReasonDaysBaut2.Visible = False
                        txtReasonDaysBaut3.Visible = False
                        txtTotalC.Visible = False
                        txtJobDelay.Visible = False
                        btnWS.Visible = False
                        btnWF.Visible = False
                        btnWHF.Visible = False
                        btnWSBaut.Visible = False
                        btnWFBaut.Visible = False
                        btnGenerate.Visible = False
                        uploaddocument(hdnversion.Value, hdnKeyVal.Value)
                        If Request.QueryString("from") = "bast" Then
                            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript111", "window.close();", True)
                        ElseIf Request.QueryString("from") = "wctr" Then
                            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript11", "WindowsClosenew();", True)
                        Else
                            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript1", "Wwindow.close();", True)
                        End If
                    Else
                        Response.Write("<script>alert('please Set X,Y coordinates for Digital sign in document Template..')</script>")
                    End If
                Else
                    If lblProjectidwctr.Text = "" Then lblProjectidwctr.Text = ""
                    lblDay.Text = txtDay.Value
                    If lblDay.Text = "" Then lblDay.Text = ""
                    lblDate.Text = txtDate.Value
                    If lblDate.Text = "" Then lblDate.Text = ""
                    lblMonth.Text = txtMonth.Value
                    If lblMonth.Text = "" Then lblMonth.Text = ""
                    lblYear.Text = txtYear.Value
                    If lblYear.Text = "" Then lblYear.Text = ""
                    lblDurationExec.Text = txtDurationExec.Value
                    If lblDurationExec.Text = "" Then lblDurationExec.Text = ""
                    lblWrkStarted.Text = txtWorkStarted.Value
                    If lblWrkStarted.Text = "" Then lblWrkStarted.Text = ""
                    lblWorkShouldFinished.Text = txtWorkShouldFinished.Value
                    If lblWorkShouldFinished.Text = "" Then lblWorkShouldFinished.Text = ""
                    lblWorkhasbeenFinished.Text = txtWorkHasBeenFinished.Value
                    If lblWorkhasbeenFinished.Text = "" Then lblWorkhasbeenFinished.Text = ""
                    lblActualExec.Text = txtActualExec.Value
                    If lblActualExec.Text = "" Then lblActualExec.Text = ""
                    lblTotalA.Text = txtTotalA.Value
                    If lblTotalA.Text = "" Then lblTotalA.Text = ""
                    lblReason1.Text = txtReason1.Value
                    If lblReason1.Text = "" Then lblReason1.Text = ""
                    lblReason2.Text = txtReason2.Value
                    If lblReason2.Text = "" Then lblReason2.Text = ""
                    lblReason3.Text = txtReason3.Value
                    If lblReason3.Text = "" Then lblReason3.Text = ""
                    lblReasonDays1.Text = txtReasonDays1.Value
                    If lblReasonDays1.Text = "" Then lblReasonDays2.Text = ""
                    lblReasonDays2.Text = txtReasonDays2.Value
                    If lblReasonDays2.Text = "" Then lblReasonDays2.Text = ""
                    lblReasonDays3.Text = txtReasonDays3.Value
                    If lblReasonDays3.Text = "" Then lblReasonDays3.Text = ""
                    lblTotalB.Text = txtTotalB.Value
                    If lblTotalB.Text = "" Then lblTotalB.Text = ""
                    lblWorkShouldFinishedbaut.Text = txtWorkShouldFinishedbaut.Value
                    If lblWorkShouldFinishedbaut.Text = "" Then lblWorkShouldFinishedbaut.Text = ""
                    lblKPIAccepted.Text = txtKPIAccepted.Value
                    If lblKPIAccepted.Text = "" Then lblKPIAccepted.Text = ""
                    lblReasonBaut1.Text = txtReasonBaut1.Value
                    If lblReasonBaut1.Text = "" Then lblReasonBaut1.Text = ""
                    lblReasonBaut2.Text = txtReasonBaut2.Value
                    If lblReasonBaut2.Text = "" Then lblReasonBaut2.Text = ""
                    lblReasonBaut3.Text = txtReasonBaut3.Value
                    If lblReasonBaut3.Text = "" Then lblReasonBaut3.Text = ""
                    lblReasonDaysBaut1.Text = txtReasonDaysBaut1.Value
                    If lblReasonDaysBaut1.Text = "" Then lblReasonDaysBaut2.Text = ""
                    lblReasonDaysBaut2.Text = txtReasonDaysBaut2.Value
                    If lblReasonDaysBaut2.Text = "" Then lblReasonDaysBaut2.Text = ""
                    lblReasonDaysBaut3.Text = txtReasonDaysBaut3.Value
                    If lblReasonDaysBaut3.Text = "" Then lblReasonDaysBaut3.Text = ""
                    lblTotalC.Text = txtTotalC.Value
                    If lblTotalC.Text = "" Then lblTotalC.Text = ""
                    lblJobDelay.Text = txtJobDelay.Value
                    If lblJobDelay.Text = "" Then lblJobDelay.Text = ""
                    txtDay.Visible = False
                    txtDate.Visible = False
                    txtMonth.Visible = False
                    txtYear.Visible = False
                    txtDurationExec.Visible = False
                    txtWorkStarted.Visible = False
                    txtWorkShouldFinished.Visible = False
                    txtWorkHasBeenFinished.Visible = False
                    txtActualExec.Visible = False
                    txtTotalA.Visible = False
                    txtTotalB.Visible = False
                    txtReason1.Visible = False
                    txtReason2.Visible = False
                    txtReason3.Visible = False
                    txtReasonDays1.Visible = False
                    txtReasonDays2.Visible = False
                    txtReasonDays3.Visible = False
                    txtWorkShouldFinishedbaut.Visible = False
                    txtKPIAccepted.Visible = False
                    txtReasonBaut1.Visible = False
                    txtReasonBaut2.Visible = False
                    txtReasonBaut3.Visible = False
                    txtReasonDaysBaut1.Visible = False
                    txtReasonDaysBaut2.Visible = False
                    txtReasonDaysBaut3.Visible = False
                    txtTotalC.Visible = False
                    txtJobDelay.Visible = False
                    btnWS.Visible = False
                    btnWF.Visible = False
                    btnWHF.Visible = False
                    btnWSBaut.Visible = False
                    btnWFBaut.Visible = False
                    btnGenerate.Visible = False
                    uploaddocument(hdnversion.Value, hdnKeyVal.Value)
                    dbutils_nsn.CheckGroupingDocBAST(hdnsiteid.Value, hdnversion.Value, ConfigurationManager.AppSettings("BASTID"), ConfigurationManager.AppSettings("WCTRBASTID"), ConfigurationManager.AppSettings("BOQDOCID"))
                    If Request.QueryString("from") = "bast" Then
                        ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript222", "window.close();", True)
                    ElseIf Request.QueryString("from") = "wctr" Then
                        ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript22", "WindowsClosenew();", True)
                    Else
                        ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript2", "window.close();", True)
                    End If
                End If
            Else
                Response.Write("<script>alert('Error while Saving the Data')</script>")
            End If
        Else
            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('Please use Internet Explorer');", True)
        End If
    End Sub
    Sub Dochecking()
        roleid = Session("Role_Id")
        grp = Session("User_Type")
        If objBO.uspApprRequired(hdnsiteid.Value, hdndocId.Value, hdnversion.Value) <> 0 Then
            If objBO.verifypermission(hdndocId.Value, roleid, grp) <> 0 Then
                Select Case objBO.DocUploadverify(hdnsiteid.Value, hdndocId.Value, hdnversion.Value).ToString
                    Case 1 'This document not attached to this site
                        hdnKeyVal.Value = 1
                        btnGenerate.Attributes.Clear()
                    Case 2 'means document was already uploaded for this version of site,do u want to overwrite
                        hdnKeyVal.Value = 2
                        btnGenerate.Attributes.Clear()
                    Case 3 'means document was not yet uploaded for thissite
                        hdnKeyVal.Value = 3
                        btnGenerate.Attributes.Clear()
                    Case 4 'means document already processed for sencod stage cannot upload
                        hdnKeyVal.Value = 0
                        Exit Sub
                End Select
            Else
                If Session("Role_Id") = 1 Then 'if role is sysadmin then no need to verify permission everthing permitted.
                    Select Case objBO.DocUploadverify(hdnsiteid.Value, hdndocId.Value, hdnversion.Value).ToString
                        Case 1 'This document not attached to this site
                            hdnKeyVal.Value = 1
                            btnGenerate.Attributes.Clear()
                            'belowcase not going to happen we need to test the scenariao.
                            btnGenerate.Attributes.Add("onclick", "javascript:return " + "confirm('This Document Not attached to this site,Do you want to  upload?')")
                        Case 2 'means document was already uploaded for this version of site,do u want to overwrite
                            hdnKeyVal.Value = 2
                            btnGenerate.Attributes.Clear()
                        Case 3 'means document was not yet uploaded for thissite
                            hdnKeyVal.Value = 3
                            btnGenerate.Attributes.Clear()
                        Case 4 'means document already processed for sencod stage cannot upload
                            hdnKeyVal.Value = 0
                            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "Showmain('2sta');", True)
                            Exit Sub
                    End Select
                Else
                    hdnKeyVal.Value = 0
                    ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "Showmain('nop');", True)
                End If
            End If
        Else
            Select Case objBO.DocUploadverify(hdnsiteid.Value, hdndocId.Value, hdnversion.Value).ToString
                Case 1 'This document not attached to this site
                    hdnKeyVal.Value = 1
                    btnGenerate.Attributes.Clear()
                    'belowcase not going to happen we need to test the scenariao.
                    btnGenerate.Attributes.Add("onclick", "javascript:return " + "confirm('This Document Not attached to this site,Do you want to upload?')")
                Case 2 'means document was already uploaded for this version of site,do u want to overwrite
                    hdnKeyVal.Value = 2
                    btnGenerate.Attributes.Clear()
                Case 3 'means document was not yet uploaded for thissite
                    hdnKeyVal.Value = 3
                    btnGenerate.Attributes.Clear()
                Case 4 'means document already processed for sencod stage cannot upload
                    hdnKeyVal.Value = 0
                    ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "Showmain('2sta');", True)
                    Exit Sub
            End Select
        End If
    End Sub
End Class
