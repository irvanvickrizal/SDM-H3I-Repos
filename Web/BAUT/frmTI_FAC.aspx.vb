Imports System.Data
Imports Common
Imports Entities
Imports BusinessLogic
Imports System.Collections.Generic
Imports NSNCustomizeConfiguration
Imports CRFramework
Imports Common_NSNFramework
Imports System.IO

Partial Class BAUT_frmTI_FAC
    Inherits System.Web.UI.Page

    Dim dt As New DataTable
    Dim objUtil As New DBUtil
    Dim objBO As New BOSiteDocs
    Dim objET As New ETAuditTrail
    Dim objET1 As New ETSiteDoc
    Dim FileNamePath As String
    Dim FileNameOnly As String
    Dim ReFileName As String
    Dim objmail As New TakeMail
    Dim controller As New HCPTController
    Dim kpicontrol As New KPIController
    Dim faccontrol As New FACController
#Region "NSNFramework"
    Dim dbutils_nsn As New DBUtils_NSN
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        If Not Page.IsPostBack Then
            btnSubmitReject.Visible = False
            BindData(GetWPID())
            'Dochecking()

            dt = objBO.uspSiteTIDocList(Request.QueryString("id"))
            grdDocuments.DataSource = dt
            grdDocuments.DataBind()
            Dim dtGetATPDocList As DataTable = objUtil.ExeQueryDT("exec uspSiteGetATPDocList " & Request.QueryString("id"), "dt")
            grdDocuments.Columns(1).Visible = False
            grdDocuments.Columns(2).Visible = False
            grdDocuments.Columns(4).Visible = False
        End If
    End Sub
    Protected Sub grddocuments_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdDocuments.RowDataBound
        Select Case e.Row.RowType
            Case DataControlRowType.DataRow
                Dim lbl As Label = CType(e.Row.FindControl("lblNo"), Label)
                lbl.Text = (grdDocuments.PageIndex * grdDocuments.PageSize) + (e.Row.RowIndex + 1) & ".&nbsp;"
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

    Protected Sub btnGenerate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGenerate.Click
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        If Request.Browser.Browser = "IE" Then
            Dim info As New FACInfo
            info.SiteInf.PackageId = GetWPID()
            If ChkPassed.Checked = True Then
                info.IsPassed = True
            Else
                info.IsPassed = False
            End If
            If ChkNoPassed.Checked = True Then
                info.IsNotPassed = True
            Else
                info.IsNotPassed = False
            End If
            info.CMAInfo.LMBY = CommonSite.UserId
            info.POType = DdlPOType.SelectedValue
            If faccontrol.ODFAC_IU(info) = True Then
                Dim strQuerCountNotYetUploaded As String = "select count(*) from sitedoc where isuploaded = 0 and siteid =" & hdnsiteid.Value & " and version=" & hdnVersion.Value & _
                " and docid in(select doc_id from coddoc where parent_id = (select docid from sitedoc where sw_id=" & Request.QueryString("id") & "))"
                Dim countNotYetUploaded As Integer = objUtil.ExeQueryScalar(strQuerCountNotYetUploaded)
                If countNotYetUploaded = 0 Then
                    btnGenerate.Visible = False
                    grdDocuments.Visible = False
                    pnlPOType.Visible = False
                    uploaddocument(Integer.Parse(hdnVersion.Value), 3)                    
                    ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('Document Successfully Generated');", True)
                Else
                    ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('Document NY Completed');", True)
                End If
                '      If countNotYetUploaded = 0 Then
                '          If (hdnDGBox.Value = "") Then hdnDGBox.Value = 0
                '          If (hdnKeyVal.Value = "") Then hdnKeyVal.Value = 0
                '          If (hdnVersion.Value = "") Then hdnVersion.Value = 0
                '          If hdnDGBox.Value = True Then
                '              Dim strsql As String = "select count(*) from docSignPositon where doc_id=" & hdndocid.Value
                '              If objUtil.ExeQueryScalar(strsql) > 0 Then
                '                  btnGenerate.Visible = False
                '                  grdDocuments.Visible = False
                '                  uploaddocument(Convert.ToInt32(hdnVersion.Value), Convert.ToInt32(hdnKeyVal.Value))
                '              Else
                '                  Response.Write("<script>alert('please Set X,Y coordinates for Digital sign in document Template..')</script>")
                '              End If
                '          Else
                '              
                '          End If

                '      Else
                '          ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('Document not yet Completed');", True)
                '      End If
                '  Else
                '      ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('Error while saving transaction');", True)
                '  End If

            End If
        Else
            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('Please use Internet Explorer');", True)
        End If
    End Sub

    Protected Sub DdlPOType_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles DdlPOType.SelectedIndexChanged
        GetSiteAttribute(GetWPID())
    End Sub

#Region "custom methods"
    Private Sub BindData(ByVal wpid As String)
        Dim info As FACInfo = faccontrol.ODFAC_LD(wpid)
        If info IsNot Nothing Then
            ChkNoPassed.Checked = info.IsNotPassed
            ChkPassed.Checked = info.IsPassed
            If Not String.IsNullOrEmpty(info.POType) Then
                DdlPOType.SelectedValue = info.POType
            Else
                DdlPOType.SelectedValue = "eqpandsvc"
            End If
        End If
        GetSiteAttribute(wpid)
        BindDocDetail()
        BindFinalApprovers(GetWPID(), 1, 0, 0, Integer.Parse(hdnwfid.Value), "approver", DLDigitalSign_NSNOnly)
        BindFinalApprovers(GetWPID(), 11, 4, 0, Integer.Parse(hdnwfid.Value), "approver", DdlDigitalSignature_Customer)
    End Sub

    Private Sub GetSiteAttribute(ByVal wpid As String)
        Dim info As SiteInfo = controller.GetSiteInfoDetail(wpid)
        If info IsNot Nothing Then
            If DdlPOType.SelectedValue.Equals("eqpandsvc") Then
                LblPono.Text = info.PONO & " A and B"
            ElseIf DdlPOType.SelectedValue.Equals("eqp") Then
                LblPono.Text = info.PONO & " A"
            Else
                LblPono.Text = info.PONO & " B"
            End If
            lblSiteID.Text = info.SiteNo
            hdnsiteno.Value = info.SiteNo
            lblSiteName.Text = info.SiteName
            LblDateofCreation.Text = String.Format("{0:dd-MMM-yyyy}", Date.Now)
            LblContractorName.Text = "PT. Nokia Networks"
            LblTypeofWork.Text = info.POName
            hdnScope.Value = info.Scope
            LblRFTNo.Text = "No:" & info.SiteNo & "/" & info.WorkpackageName & "-" & "476/506/010/0413"
        End If
    End Sub

    Private Sub BindDocDetail()
        Dim strQuery As String = "Exec HCPT_uspGeneral_GetDetailDoc " & Convert.ToString(GetSWID())
        Dim dtSiteDOCDetail As DataTable = objUtil.ExeQueryDT(strQuery, "SiteDoc1")
        If dtSiteDOCDetail.Rows.Count > 0 Then
            hdnsiteid.Value = dtSiteDOCDetail.Rows(0).Item("SiteId").ToString
            hdnVersion.Value = dtSiteDOCDetail.Rows(0).Item("version").ToString
            hdnwfid.Value = dtSiteDOCDetail.Rows(0).Item("WF_Id").ToString
            hdndocid.Value = dtSiteDOCDetail.Rows(0).Item("docid").ToString
            hdnDGBox.Value = dtSiteDOCDetail.Rows(0).Item("DGBox").ToString
        End If
    End Sub

    Sub Dochecking()
        Dim roleid As Integer = CommonSite.RollId
        Dim grp As String = CommonSite.UserType
        If objBO.uspApprRequired(hdnsiteid.Value, hdndocid.Value, hdnVersion.Value) <> 0 Then
            If objBO.verifypermission(hdndocid.Value, roleid, grp) <> 0 Then
                Select Case objBO.DocUploadverify(hdnsiteid.Value, hdndocid.Value, hdnVersion.Value).ToString
                    Case 1 'This document not attached to this site
                        hdnKeyVal.Value = 1
                        btnGenerate.Attributes.Clear()
                        'belowcase not going to happen we need to test the scenariao.
                        btnGenerate.Attributes.Add("onclick", "javascript:return " + "confirm('This Document Not attached to this site,Do you want to upload?')")
                    Case 2 'means document was already uploaded for this version of site,do u want to overwrite
                        hdnKeyVal.Value = 2
                        btnGenerate.Attributes.Clear()
                        btnGenerate.Attributes.Add("onclick", "javascript:return " + "confirm('This Document already uploaded Do you want to replace?')")
                    Case 3 'means document was not yet uploaded for thissite
                        hdnKeyVal.Value = 3
                        btnGenerate.Attributes.Clear()
                    Case 4 'means document already processed for sencod stage cannot upload
                        hdnKeyVal.Value = 0
                        ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScripta", "Showmain('2sta');", True)
                        Exit Sub
                End Select
            Else
                If Session("Role_Id") = 1 Then ''if role is sysadmin then no need to verify permission everthing permitted.
                    Select Case objBO.DocUploadverify(hdnsiteid.Value, hdndocid.Value, hdnVersion.Value).ToString
                        Case 1 'This document not attached to this site
                            hdnKeyVal.Value = 1
                            btnGenerate.Attributes.Clear()
                            'belowcase not going to happen we need to test the scenariao.
                            btnGenerate.Attributes.Add("onclick", "javascript:return " + "confirm('This Document Not attached to this site,Do you want to  upload?')")
                        Case 2 'means document was already uploaded for this version of site,do u want to overwrite
                            hdnKeyVal.Value = 2
                            btnGenerate.Attributes.Clear()
                            btnGenerate.Attributes.Add("onclick", "javascript:return " + "confirm('This Document already uploaded Do you want to replace?')")
                        Case 3 'means document was not yet uploaded for thissite
                            hdnKeyVal.Value = 3
                            btnGenerate.Attributes.Clear()
                        Case 4 'means document already processed for sencod stage cannot upload
                            hdnKeyVal.Value = 0
                            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScriptb", "Showmain('2sta');", True)
                            Exit Sub
                    End Select
                Else
                    hdnKeyVal.Value = 0
                    ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScriptc", "Showmain('nop');", True)
                End If
            End If
        Else 'irvan 20140202 Appr Not Required
            Select Case objBO.DocUploadverify(hdnsiteid.Value, hdndocid.Value, hdnVersion.Value).ToString
                Case 1 'This document not attached to this site
                    hdnKeyVal.Value = 1
                    btnGenerate.Attributes.Clear()
                    'belowcase not going to happen we need to test the scenariao.
                    btnGenerate.Attributes.Add("onclick", "javascript:return " + "confirm('This Document Not attached to this site,Do you want to  upload?')")
                Case 2 'means document was already uploaded for this version of site,do u want to overwrite
                    hdnKeyVal.Value = 2
                    btnGenerate.Attributes.Clear()
                    btnGenerate.Attributes.Add("onclick", "javascript:return " + "confirm('This Document already uploaded Do you want to replace?')")
                Case 3 'means document was not yet uploaded for thissite
                    hdnKeyVal.Value = 3
                    btnGenerate.Attributes.Clear()
                Case 4 'means document already processed for sencod stage cannot upload
                    hdnKeyVal.Value = 0
                    ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScriptd", "Showmain('2sta');", True)
                    Exit Sub
            End Select
        End If
    End Sub

    Private Function CreatePDFFile(ByVal StrPath As String) As String
        StrPath = StrPath.Replace(" \", "\").ToString 'bugfix100816 causing error when the path containing space before \
        Dim dtba As New DataTable
        Dim filenameorg As String
        Dim ReFileName As String
        filenameorg = hdnsiteno.Value & "-" & "FAC" & "-" & Format(CDate(DateTime.Now), "ddMMyyyyHHss")
        ReFileName = filenameorg & ".htm"
        Dim strPathnew As String = StrPath & ReFileName
        'Dim strPathOrg As String = hdnsiteno.Value & "\TI\" & hdnScope.Value & "-" & GetWPID() & "\"
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
        sw.WriteLine("#dvPrint{width:800px;height:950px;}")
        sw.WriteLine(".lblText{font-family: verdana;font-size: 8pt;color: #000000;text-align: left;}")
        sw.WriteLine(".lblTextItalic{font-family: verdana;font-size: 8pt;color: #000000;text-align: left;font-style:italic;}")
        sw.WriteLine(".lblBText{font-family: verdana;font-size: 8pt;color: #000000;text-align: left;font-weight: bold;}")
        sw.WriteLine(".lblBold{font-family: verdana;font-size: 18px;color: #000000;font-weight: bold;}")
        sw.WriteLine(".lblRef{font-family: verdana;font-size: 11px;color: #000000;font-weight: bold;}")
        sw.WriteLine(".textFieldStyle{background-color: white;border: 1px solid;color: black;font-family: verdana;font-size: 9pt;}")
        sw.WriteLine(".GrdDocPanelRows{background-color: white;vertical-align: middle;font-family: verdana;font-size: 7.5pt;color: #000000;}")
        sw.WriteLine(".PagerTitle{font-size: 8pt;background-color: #cddbbf;text-align: Right;vertical-align: middle;color: #25375b;font-weight: bold;}")
        sw.WriteLine(".Hcap{height: 5px;}")
        sw.WriteLine(".VCap{width: 10px;}")
        sw.WriteLine(".headerform{margin-top: 15px;height: 60px;}")
        sw.WriteLine(".siteATTPanel{margin-top:10px;  height:120px; text-align:left;}")
        sw.WriteLine(".SiteDetailInfoPanel{margin-top: 10px;width: 100%;text-align: left;height: 150px;}")
        sw.WriteLine(".sitedescription{margin-top: 10px;width: 800px;text-align: left;height:60px;}")
        sw.WriteLine(".pnlremarks{height:60px;text-align:left;}")
        sw.WriteLine(".whitespace{height:5px;}")
        sw.WriteLine(".pnlNote{height:80px;text-align:left;}")
        sw.WriteLine(".pnlNote2{height:20px;text-align:left;}")
        sw.WriteLine(".footerPanel{height:350px;margin-top:10px;}")
        sw.WriteLine("</style></head>")
        sw.WriteLine("<body class=""MainCSS"">")
        dvPrint.RenderControl(sw)
        sw.WriteLine("</body>")
        sw.WriteLine("</html>")
        sw.Close()
        sw.Dispose()
        'Return Common.TakeFileUpload.ConvertAnyFormatToPDFHtmlNew(ConfigurationManager.AppSettings("Fpath") & strPathnew, ConfigurationManager.AppSettings("Fpath") & strPathOrg, filenameorg)
        Return EBASTFileUpload.ConvertAnyFormatToPDFHtmlNew(StrPath & ReFileName, StrPath, filenameorg)
		'lblGenerateTest.Text = StrPath & ReFileName & "||" & strPath & "," & filenameorg
        'Return filenameorg & ".pdf"
    End Function

    Private Sub uploaddocument(ByVal vers As Integer, ByVal keyval As Integer)
        dt = objBO.getbautdocdetailsNEW(hdndocid.Value) '(Constants._Doc_SSR)
        Dim sec As String = dt.Rows(0).Item("sec_name").ToString
        Dim subsec As String = dt.Rows(0).Item("subsec_name").ToString
        Dim secpath As String = ""
        Dim ft As String = ""
        Dim path As String = ""
        Dim filenameorg As String
        FileNameOnly = "-" & "FAC" & "-"
        filenameorg = lblSiteID.Text & FileNameOnly & Format(CDate(DateTime.Now), "ddMMyyyyHHss")
        ReFileName = filenameorg & ".htm"
        secpath = IIf(subsec = "" Or subsec = "0", IIf(String.IsNullOrEmpty(sec), "", sec & "\"), IIf(String.IsNullOrEmpty(sec), "", sec & "\") & subsec & "\")
        ft = ConfigurationManager.AppSettings("Type") & hdnScope.Value & "-" & GetWPID() & "\"
        path = ConfigurationManager.AppSettings("Fpath") & lblSiteID.Text & ft & secpath
        Dim strResult As String = DOInsertTrans(hdnsiteid.Value, hdndocid.Value, vers, path)
        'Dim strResult As String = "1"
        Dim DocPath As String = ""
        If strResult = "0" Then
            DocPath = lblSiteID.Text & ft & secpath & CreatePDFFile(Replace(path, "  \", "\"))
        ElseIf strResult = "1" Then
            DocPath = lblSiteID.Text & ft & secpath & CreatePDFFile(Replace(path, "  \", "\"))
        Else
            DocPath = lblSiteID.Text & ft & secpath & CreatePDFFile(Replace(path, "  \", "\"))
        End If
        'For offline testing mode
        'DocPath = lblSiteID.Text & ft & secpath

        'Response.Write(DocPath)
        With objET1
            .SiteID = hdnsiteid.Value
            .DocId = hdndocid.Value
            .IsUploded = 1
            .Version = vers
            .keyval = keyval
            .DocPath = DocPath
            .AT.RStatus = Constants.STATUS_ACTIVE
            .AT.LMBY = Session("User_Id")
            .orgDocPath = DocPath 'hdnSiteno.Value & ft & secpath & ReFileName
            .PONo = LblPono.Text
        End With
        objBO.updatedocupload(objET1)
		ActivityLog_I(CommonSite.UserId, BaseConfiguration.Activity_Generate_Doc & " FAC")
        'Dim strsql As String = "Update bautmaster set Pstatus=1 where Site_ID = " & hdnsiteid.Value & " and SiteVersion =" & hdnversion.Value & " and PONO = '" & lblPO.Text & "'"
        'objUtil.ExeUpdate(strsql)
        'sendmail2()
        'chek4alldoc() ' for messaage to previous screen ' and saving final docupload date in reporttable
        'for BAUT
        'objBo.check4BAUTBAST(hdnsiteid.Value, hdnversion.Value, 1, lblPONO.Text, Session("User_Name"))
        'objdb.ExeQueryScalar("exec uspcheck4BAUTBAST " & hdnsiteid.Value & " ," & hdnversion.Value & "," & 1 & ",'" & lblPO.Text & "','" & Session("User_Name") & "'," & ConfigurationManager.AppSettings("BAUTID") & "," & ConfigurationManager.AppSettings("BASTID") & " ")
        'for BAST1
        'objBo.check4BAUTBAST(hdnsiteid.Value, hdnversion.Value, 2, lblPONO.Text, Session("User_Name"))
        'objdb.ExeQueryScalar("exec uspcheck4BAUTBAST " & hdnsiteid.Value & " ," & hdnversion.Value & "," & 2 & ",'" & lblPO.Text & "','" & Session("User_Name") & "'," & ConfigurationManager.AppSettings("BAUTID") & "," & ConfigurationManager.AppSettings("BASTID") & " ")
        'Fill Transaction table
        AuditTrail()
        'SendGenerateMail()
        If String.IsNullOrEmpty(Request.QueryString("from")) Then
            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScripta", "Showmain('successuploaded');", True)
        Else
            'Response.Redirect("../HCPT_Dashboard/frmFACReadyCreation.aspx")
			ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScripta", "Showmain('successandbacktofacreadycreation');", True)
        End If

    End Sub

    Sub AuditTrail()
        objET.PoNo = LblPono.Text
        objET.SiteId = hdnsiteid.Value
        objET.DocId = hdndocid.Value
        objET.Version = hdnVersion.Value
        objET.Task = "1"
        objET.Status = "1"
        objET.Userid = Session("User_Id")
        objET.Roleid = Session("Role_Id")
        objET.fldtype = hdnScope.Value
        DBUtils_NSN.InsertAuditTrailNew(objET, GetWPID())
    End Sub

    Private Function DOInsertTrans(ByVal siteid As String, ByVal docName As String, ByVal version As Integer, ByVal path As String) As String
        Dim docId As Integer = Integer.Parse(hdndocid.Value)
        Dim wfcontrol As New CRController
        Dim isSucceed As Boolean = True
        Dim dtNew As DataTable
        If hdnwfid.Value <> 0 Then
            dtNew = wfcontrol.GetWorkflowDetail(Integer.Parse(hdnwfid.Value))
            If hdnDGBox.Value = False Then
                If dtNew.Rows.Count > 0 Then
                    'objBO.DelWFTransaction(hdndocid.Value, dtNew.Rows(0).Item(1).ToString, hdnsiteid.Value, hdnversion.Value)
                    If controller.WFTransaction_D(GetWPID(), Integer.Parse(hdndocid.Value)) = True Then
                        Dim aa As Integer = 0
                        Dim sorder As Integer
                        For aa = 0 To dtNew.Rows.Count - 1
                            'fillDetails()
                            sorder = dtNew.Rows(aa).Item("sorder")
                            Dim transinfo As New DOCTransactionInfo
                            transinfo.TaskId = Integer.Parse(dtNew.Rows(aa).Item("Tsk_id").ToString())
                            transinfo.SiteInf.PackageId = GetWPID()
                            transinfo.DocInf.DocId = docId
                            transinfo.WFID = Integer.Parse(hdnwfid.Value)
                            transinfo.UGPID = Integer.Parse(dtNew.Rows(aa).Item("GrpId").ToString())
                            transinfo.PageNo = 1
                            transinfo.RStatus = 2
                            transinfo.RoleInf.RoleId = Integer.Parse(dtNew.Rows(aa).Item("RoleId").ToString())
                            transinfo.CMAInfo.LMBY = CommonSite.UserId
                            transinfo.Status = 1
                            If sorder = 1 Then
                                transinfo.StartDateTime = Date.Now()
                                transinfo.EndDateTime = Date.Now()
                                transinfo.Status = 0
                            ElseIf sorder = 2 Then
                                transinfo.StartDateTime = Date.Now()
                            Else
                                transinfo.StartDateTime = Nothing
                                transinfo.EndDateTime = Nothing
                            End If

                            If New WFGroupController().IsTaskApprover(transinfo.TaskId) = True Then
                                If transinfo.UGPID = 1 Then
                                    transinfo.Xval = 60
                                    transinfo.Yval = 345
                                ElseIf transinfo.UGPID = 4 Then
                                    transinfo.Xval = 60
                                    transinfo.Yval = 195
                                ElseIf transinfo.UGPID = 11 Then
                                    transinfo.Xval = 315
                                    transinfo.Yval = 195
                                Else
                                    transinfo.Xval = 0
                                    transinfo.Yval = 0
                                End If
                            End If

                            If controller.WFTransaction_I(transinfo) = False Then
                                isSucceed = False
                                controller.WFTransaction_D(GetWPID, Integer.Parse(hdndocid.Value))
                                Exit For
                            End If
                        Next
                    End If
                End If
                Return "1"
            Else
                Return "1"
            End If
        Else
            Dim status As Integer = 99
            objBO.uspwftransactionNOTWFI(docId, 0, hdnsiteid.Value, hdnVersion.Value, 1, Session("User_Id"), status, 2, Session("User_Name"))
            Return "0"
        End If
    End Function

    Private Function GetWPID() As String
        If String.IsNullOrEmpty(Request.QueryString("wpid")) Then
            Return "0"
        Else
            Return Request.QueryString("wpid")
        End If
    End Function

    Private Function GetSWID() As Int32
        If String.IsNullOrEmpty(Request.QueryString("id")) Then
            Return 0
        Else
            Return Convert.ToInt32(Request.QueryString("id"))
        End If
    End Function

    Private Sub BindFinalApprovers(ByVal packageid As String, ByVal grpid1 As Integer, ByVal grpid2 As Integer, ByVal grpid3 As Integer, ByVal wfid As Integer, ByVal taskdesc As String, ByVal dglist As DataList)
        dglist.DataSource = controller.GetFinalApprovers(packageid, grpid1, grpid2, grpid3, wfid, taskdesc)
        dglist.DataBind()
    End Sub

    Protected Sub DOThis(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rad As RadioButtonList = CType(sender, RadioButtonList)
        Dim grdr As GridViewRow = CType(rad.NamingContainer, GridViewRow)
        Dim x As Integer = grdr.RowIndex
        Dim rdl As RadioButtonList = grdDocuments.Rows(x).Cells(5).FindControl("rdbstatus")
        Dim txt1 As TextBox = grdDocuments.Rows(x).Cells(6).FindControl("txtremarks")
        'txt1.Text = "Please enter remarks here......"
        If rdl.SelectedValue = 0 Then  'approve
            txt1.Visible = False
            btnSubmitReject.Visible = False
            btnGenerate.Visible = True
        Else 'reject
            txt1.Visible = True
            btnSubmitReject.Visible = True
            btnGenerate.Visible = False
        End If
    End Sub

    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        Return
    End Sub

#Region "Activity Log"
    Private Sub ActivityLog_I(ByVal userid As Integer, ByVal activitydesc As String)
        'Dim ipaddress As String = HttpContext.Current.Request.UserHostAddress
        Dim ipaddress As String = Me.Page.Request.ServerVariables("REMOTE_ADDR")
        Dim info As New UserActivityLogInfo
        info.UserId = userid
        info.IPAddress = ipaddress
        info.Description = activitydesc

        controller.UserLogActivity_I(info)
    End Sub

    Private Function GetUserIdByUserLogin(ByVal usrLogin As String) As Integer
        Return controller.GetUserIDBaseUserLogin(usrLogin)
    End Function
#End Region
#End Region

End Class
