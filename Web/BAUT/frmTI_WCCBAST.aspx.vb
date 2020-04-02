Imports System.Data
Imports System.IO
Imports Common
Imports BusinessLogic
Imports Entities
Imports DAO
Partial Class frmTI_WCCBAST
    Inherits System.Web.UI.Page
    Dim dt As New DataTable
    Dim objUtil As New DBUtil
    Dim str As String
    Dim objETAT As New ETWCCAuditTrail
    Dim objETSD As New ETWCCSiteDoc
    Dim objETWcc As New ETODWcc
    Dim objETWFT As New ETWCCWFTransaction
    Dim objBOAT As New BOWCCAuditTrail
    Dim objBOMR As New BOWCCMailReport
    Dim objBO As New BOODWcc
    Dim objBOSD As New BOWCCSiteDocs
    Dim dt2 As New DataTable
    Dim objdb As New DBUtil
    Dim cst As New Constants
    Dim docId As Integer = 0
    Dim i, j, k, l As Integer
    Dim strsql As String
    Dim dt1 As New DataTable
    Dim roleid As Integer
    Dim grp As String
    Dim FileNamePath As String
    Dim FileNameOnly As String
    Dim ReFileName As String
    Dim url As String
    Dim objmail As New TakeMail
    Dim oddt As New DataTable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        imgdateissue.Attributes.Add("onclick", "javascript:popUpCalendar(this,txtDateIssued,'dd-mmm-yyyy');return false;")
        imgstdate.Attributes.Add("onclick", "javascript:popUpCalendar(this,txtStartDate,'dd-mmm-yyyy');return false;")
        imgcmpdate.Attributes.Add("onclick", "javascript:popUpCalendar(this,txtCompletionDate,'dd-mmm-yyyy');return false;")
        imgdelaydate.Attributes.Add("onclick", "javascript:popUpCalendar(this,txtDelayDateFromPo,'dd-mmm-yyyy');return false;")
        If Not IsPostBack Then
            Binddata()
        End If
    End Sub
    Sub Binddata()
        str = "Exec uspWCCTIBautOnLine " & Request.QueryString("id")
        dt = objUtil.ExeQueryDT(str, "SiteDoc1")
        If dt.Rows.Count > 0 Then
            lblPO.Text = dt.Rows(0).Item("pono").ToString
            hdnSiteno.Value = dt.Rows(0).Item("site_no").ToString
            hdnsiteid.Value = dt.Rows(0).Item("SiteId").ToString
            hdnversion.Value = dt.Rows(0).Item("version").ToString
            hdnWfId.Value = dt.Rows(0).Item("WF_Id").ToString
            hdndocid.Value = dt.Rows(0).Item("docid").ToString
            hdnScope.Value = dt.Rows(0).Item("Scope").ToString
            hdnDGBox.Value = dt.Rows(0).Item("DGBox").ToString
            hdnPono.Value = dt.Rows(0).Item("pono").ToString
            str = "Exec [uspWCCGetOnLineFormBind] " & dt.Rows(0).Item("WF_Id").ToString & "," & dt.Rows(0).Item("SiteId").ToString & ""
            dt = objUtil.ExeQueryDT(str, "SiteDoc1")
            Dim dtv As DataView = dt.DefaultView
            dtv.Sort = "tsk_id desc"
            DLDigitalSign.DataSource = dtv
            DLDigitalSign.DataBind()
            HDDgSignTotal.Value = dt.Rows.Count
            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScriptxdd", "getControlPosition();", True)
        Else
            hdnsiteid.Value = 0
            hdnversion.Value = 0
            hdnWfId.Value = 0
            hdndocid.Value = 0
        End If
    End Sub
    Sub Dochecking()
        roleid = Session("Role_Id")
        grp = Session("User_Type")
        If objBOSD.uspWCCApprRequired(hdnsiteid.Value, hdndocid.Value, hdnversion.Value) <> 0 Then
            If objBOSD.wccVerifypermission(hdndocid.Value, roleid, grp) <> 0 Then
                Select Case objBOSD.wccDocUploadverify(hdnsiteid.Value, hdndocid.Value, hdnversion.Value).ToString
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
                        'ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "Showmain('2sta');", True)
                        'makevisible()
                        Exit Sub
                End Select
            Else
                If Session("Role_Id") = 1 Then ''if role is sysadmin then no need to verify permission everthing permitted.                    
                    Select Case objBOSD.wccDocUploadverify(hdnsiteid.Value, hdndocid.Value, hdnversion.Value).ToString
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
                            'ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "Showmain('2sta');", True)
                            Exit Sub
                    End Select
                Else
                    hdnKeyVal.Value = 0
                    'ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "Showmain('nop');", True)
                End If
            End If
        Else 'Seeta 20081230 Appr Not Required
            Select Case objBOSD.wccDocUploadverify(hdnsiteid.Value, hdndocid.Value, hdnversion.Value).ToString
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
                    'ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "Showmain('2sta');", True)
                    Exit Sub
                    'Response.Redirect("frmSiteDocUploadTree.aspx")
            End Select
        End If
    End Sub
    Sub uploaddocument(ByVal vers As Integer, ByVal keyval As Integer)
        dt = objBOSD.getWCCbautdocdetailsNEW(hdndocid.Value)
        Dim sec As String = dt.Rows(0).Item("sec_name").ToString
        Dim subsec As String = dt.Rows(0).Item("subsec_name").ToString
        Dim secpath As String = ""
        Dim ft As String = ""
        Dim path As String = ""
        Dim filenameorg As String
        FileNameOnly = "-" & Constants._Doc_BAST & "-"
        filenameorg = hdnSiteno.Value & FileNameOnly & Format(CDate(DateTime.Now), "ddMMyyyyHHss")
        ReFileName = filenameorg & ".htm"
        ft = ConfigurationManager.AppSettings("Type") & hdnPono.Value & "-" & hdnScope.Value & "\"
        path = ConfigurationManager.AppSettings("Fpath") & hdnSiteno.Value & ft
        Dim strResult As String = DOInsertTrans(hdnsiteid.Value, hdndocId.Value, vers, path)
        Dim DocPath As String = ""
        If strResult = "0" Then
            DocPath = hdnSiteno.Value & ft & secpath & CreatePDFFile(Replace(path, "  \", "\"))
        ElseIf strResult = "1" Then
            DocPath = hdnSiteno.Value & ft & secpath & CreatePDFFile(Replace(path, "  \", "\"))
        Else
            DocPath = hdnSiteno.Value & ft & secpath & CreatePDFFile(Replace(path, "  \", "\"))
        End If
        With objETSD
            .SiteID = hdnsiteid.Value
            .DocId = hdndocid.Value
            .IsUploded = 1
            .Version = vers
            .keyval = keyval
            .DocPath = DocPath
            .AT.RStatus = Constants.STATUS_ACTIVE
            .AT.LMBY = Session("User_Name")
            .orgDocPath = hdnSiteno.Value & ft & secpath & ReFileName
            .PONo = hdnPono.Value
        End With
        objBOSD.wccUpdatedocupload(objETSD)
        'Fill Transaction table
        AuditTrail()
    End Sub
    Sub AuditTrail()
        objETAT.PoNo = hdnPono.Value
        objETAT.SiteId = hdnsiteid.Value
        objETAT.DocId = hdndocid.Value
        objETAT.Task = "1"
        objETAT.Status = "1"
        objETAT.Userid = Session("User_Id")
        objETAT.Roleid = Session("Role_Id")
        objETAT.fldtype = hdnScope.Value
        objBOAT.uspWCCAuditTrailI(objETAT)
    End Sub
    Function DOInsertTrans(ByVal siteid As String, ByVal docName As String, ByVal version As Integer, ByVal path As String) As String
        docId = hdndocId.Value
        Dim dtNew As DataTable
        If hdnWfId.Value <> 0 Then
            dtNew = objBOSD.wccDoinserttrans(hdnWfId.Value, docId)
            If hdnDGBox.Value = True Then
                If dtNew.Rows.Count > 0 Then
                    objBOSD.wccDelWFTransaction(hdndocid.Value, dtNew.Rows(0).Item(1).ToString, hdnsiteid.Value, hdnversion.Value)
                    Dim aa As Integer = 0
                    Dim status As Integer
                    For aa = 0 To dtNew.Rows.Count - 1
                        fillDetails()
                        If (dtNew.Rows(aa).Item(5).ToString <> "1") Then status = 1
                        objETWFT.Status = status
                        objETWFT.DocId = docId
                        objETWFT.UserType = dtNew.Rows(aa).Item(3).ToString
                        objETWFT.UsrRole = dtNew.Rows(aa).Item(4).ToString
                        objETWFT.WFId = dtNew.Rows(aa).Item(1).ToString
                        objETWFT.UGP_Id = dtNew.Rows(aa).Item("grpId").ToString
                        objETWFT.TSK_Id = dtNew.Rows(aa).Item(5).ToString
                        objETWFT.XVal = dtNew.Rows(aa).Item("X_Coordinate").ToString
                        objETWFT.YVal = dtNew.Rows(aa).Item("Y_Coordinate").ToString
                        objETWFT.PageNo = dtNew.Rows(aa).Item("PageNo").ToString
                        objBOSD.uspWCCwftransactionIU(objETWFT)
                    Next
                End If
                Return "1"
            End If
            CreateXY()
        Else
            Dim status As Integer = 99
            objBOSD.uspWCCwftransactionNOTWFI(docId, 0, hdnsiteid.Value, hdnversion.Value, 1, Session("User_Id"), status, 2, Session("User_Name"))
            Return "0"
        End If
    End Function
    Sub fillDetails()
        objETWFT.DocId = hdndocid.Value
        objETWFT.Site_Id = hdnsiteid.Value
        objETWFT.SiteVersion = hdnversion.Value
        objETWFT.AT.RStatus = Constants.STATUS_ACTIVE
        objETWFT.AT.LMBY = Session("User_Name")
    End Sub
    Protected Sub btnGenerate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGenerate.Click
        lblSubcontractorName.Text = txtSubconractorName.Value
        If lblSubcontractorName.Text = "" Then lblSubcontractorName.Text = "N/A"
        lblDateIssued.Text = txtDateIssued.Value
        If lblDateIssued.Text = "" Then lblDateIssued.Text = "N/A"
        lblCertificateNumber.Text = txtCertificateNumber.Value
        If lblCertificateNumber.Text = "" Then lblCertificateNumber.Text = "N/A"
        lblSiteID.Text = txtSiteId.Value
        If lblSiteID.Text = "" Then lblSiteID.Text = "N/A"
        lblSiteName.Text = txtSiteName.Value
        If lblSiteName.Text = "" Then lblSiteName.Text = "N/A"
        lblWorkPackageId.Text = txtWorkPackageID.Value
        If lblWorkPackageId.Text = "" Then lblWorkPackageId.Text = "N/A"
        lblWorkDescription.Text = txtWorkDescription.Value
        If lblWorkDescription.Text = "" Then lblWorkDescription.Text = "N/A"
        lblStartDate.Text = txtStartDate.Value
        If lblStartDate.Text = "" Then lblStartDate.Text = "N/A"
        lblCompletionDate.Text = txtCompletionDate.Value
        If lblCompletionDate.Text = "" Then lblCompletionDate.Text = "N/A"
        lblDelayDateFromPo.Text = txtDelayDateFromPo.Value
        If lblDelayDateFromPo.Text = "" Then lblDelayDateFromPo.Text = "N/A"
        lblPOPartner.Text = txtPoPartner.Value
        If lblPOPartner.Text = "" Then lblPOPartner.Text = "N/A"
        lblPoTelkomsel.Text = txtPoTelkomsel.Value
        If lblPoTelkomsel.Text = "" Then lblPoTelkomsel.Text = "N/A"
        lblTelkomselBaut_BastDate.Text = txtTelcomeSelBAUTDate.Value
        If lblTelkomselBaut_BastDate.Text = "" Then lblTelkomselBaut_BastDate.Text = "N/A"
        txtSubconractorName.Visible = False
        txtDateIssued.Visible = False
        txtCertificateNumber.Visible = False
        txtSiteId.Visible = False
        txtSiteName.Visible = False
        txtWorkPackageID.Visible = False
        txtWorkDescription.Visible = False
        txtStartDate.Visible = False
        txtCompletionDate.Visible = False
        txtDelayDateFromPo.Visible = False
        txtPoPartner.Visible = False
        txtPoTelkomsel.Visible = False
        txtTelcomeSelBAUTDate.Visible = False
        imgdateissue.Visible = False
        imgstdate.Visible = False
        imgdelaydate.Visible = False
        imgcmpdate.Visible = False
        i = objdb.ExeQueryScalar("exec uspCheckIntegration  '" & hdndocid.Value & "' ,'" & hdnSiteno.Value & "'," & hdnversion.Value & "")
        Select Case i
            Case 1
                Dochecking()
            Case 2
                hdnKeyVal.Value = 0
                'ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "Showmain('Int');", True)
            Case 3
                Dochecking()
            Case 4
                hdnKeyVal.Value = 0
                'ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "Showmain('IntD');", True)
        End Select
        GenerateDocument(hdnKeyVal.Value)
    End Sub
    Sub GenerateDocument(ByVal vers As Integer)
        objETWcc.swid = Request.QueryString("id")
        objETWcc.PoNo = lblPO.Text
        objETWcc.Podate = Date.Now
        objETWcc.SiteNo = hdnsiteid.Value
        objETWcc.Version = vers
        objETWcc.SubcontractorName = txtSubconractorName.Value
        objETWcc.DateIssued = txtDateIssued.Value
        objETWcc.CertificateNumber = txtCertificateNumber.Value
        objETWcc.SiteID = txtSiteId.Value
        objETWcc.SiteName = txtSiteName.Value
        objETWcc.WorkPackageID = txtWorkPackageID.Value
        objETWcc.WorkDescription = txtWorkDescription.Value
        objETWcc.StartDate = txtStartDate.Value
        objETWcc.CompletionDate = txtCompletionDate.Value
        objETWcc.DelayDatefromPO = txtDelayDateFromPo.Value
        objETWcc.POPartner = txtPoPartner.Value
        objETWcc.POTelkomsel = txtPoTelkomsel.Value
        objETWcc.TelkomselBAUTBASTDate = txtTelcomeSelBAUTDate.Value
        objETWcc.AT.LMBY = Session("User_Name")
        uploaddocument(0, hdnKeyVal.Value)
    End Sub
    Function CreatePDFFile(ByVal StrPath As String) As String
        StrPath = StrPath.Replace(" \", "\").ToString 'bugfix100816 causing error when the path containing space before \
        Dim dtba As New DataTable
        Dim filenameorg As String
        Dim ReFileName As String
        filenameorg = hdnSiteno.Value & "-WCC-" & Format(CDate(DateTime.Now), "ddMMyyyyHHss")
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
        DivWCC.RenderControl(sw)
        sw.WriteLine("</body>")
        sw.WriteLine("</html>")
        sw.Close()
        sw.Dispose()
        Return Common.TakeFileUpload.ConvertAnyFormatToPDFHtmlNew(StrPath & ReFileName, StrPath, filenameorg)
    End Function
    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        Return
    End Sub
    Sub CreateXY()
        Dim dtNew As DataTable
        Dim strSql1 As String = "select wfdid,wfid,sorder,grpcode,roleid,a.tsk_id,a.grpId,tt.tskname,0 as   X_Coordinate,0 as  Y_Coordinate," & _
        "0 as  PageNo,IsNull(a.Sorder,0)Sno from wccTWFDefinition A inner join tusertype B on a.grpid=b.grpid inner join ttask as tt on tt.tsk_id=a.tsk_id " & _
        " where tt.TSK_Id=1 and wfid=" & hdnWfId.Value & "   order by wfdid"
        dtNew = objUtil.ExeQueryDT(strSql1, "dd")
        If dtNew.Rows.Count > 0 Then
            'objBOSD.wccDelWFTransaction(hdndocid.Value, dtNew.Rows(0).Item(1).ToString, hdnsiteid.Value, hdnversion.Value)
            objUtil.ExeNonQuery("exec uspWCCwftransactionD " & hdndocid.Value & "," & dtNew.Rows(0).Item(1).ToString & "," & hdnsiteid.Value & "," & hdnversion.Value & " ")
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
            objETWFT.Status = status
            objETWFT.UserType = dvIn(TopK).Item(3).ToString
            objETWFT.UsrRole = dvIn(TopK).Item(4).ToString
            objETWFT.WFId = dvIn(TopK).Item(1).ToString
            objETWFT.TSK_Id = dvIn(TopK).Item(5).ToString
            objETWFT.UGP_Id = dvIn(TopK).Item("grpId").ToString
            objETWFT.XVal = 0
            objETWFT.YVal = 0
            objETWFT.PageNo = 0
            objETWFT.Site_Id = hdnsiteid.Value
            objETWFT.Status = status
            objBOSD.uspWCCwftransactionIU(objETWFT)
        Next
        strSql1 = "select wfdid,wfid,sorder,grpcode,roleid,a.tsk_id,a.grpId,tt.tskname,0 as   X_Coordinate,0 as  Y_Coordinate," & _
        "0 as  PageNo,IsNull(a.Sorder,0)Sno from wccTWFDefinition A inner join tusertype B on a.grpid=b.grpid inner join ttask as tt on tt.tsk_id=a.tsk_id " & _
        " where tt.TSK_Id <> 1 and wfid=" & hdnWfId.Value & "   order by sorder"
        DtNewOne = objUtil.ExeQueryDT(strSql1, "dd")
        dvNotIn = DtNewOne.DefaultView
        dvNotIn.RowFilter = "TSK_Id <>1"
        status = 1
        Dim intCount As Integer = 0, IncrMentY As Integer = 0
        For IncrMentX As Integer = 0 To dvNotIn.Count - 1
            fillDetails()
            objETWFT.Status = status
            objETWFT.UserType = dvNotIn(IncrMentX).Item(3).ToString
            objETWFT.UsrRole = dvNotIn(IncrMentX).Item(4).ToString
            objETWFT.WFId = dvNotIn(IncrMentX).Item(1).ToString
            objETWFT.TSK_Id = dvNotIn(IncrMentX).Item(5).ToString
            objETWFT.UGP_Id = dvNotIn(IncrMentX).Item("grpId").ToString
            objETWFT.XVal = 0
            objETWFT.YVal = 0
            objETWFT.PageNo = 0
            objETWFT.Site_Id = hdnsiteid.Value
            objBOSD.uspWCCwftransactionIU(objETWFT)
        Next
        Dim strsql2 As String
        Dim dtnew2 As DataTable
        Dim dvnotin2 As DataView
        Dim tcount As Integer = 0, IncrY As Integer = 0
        strsql2 = "select wfdid,wfid,sorder,grpcode,roleid,a.tsk_id,a.grpId,tt.tskname,0 as   X_Coordinate,0 as  Y_Coordinate," & _
        "0 as  PageNo,IsNull(a.Sorder,0)Sno from wccTWFDefinition A inner join tusertype B on a.grpid=b.grpid inner join ttask as tt on tt.tsk_id=a.tsk_id " & _
        " where tt.TSK_Id not in (1,5) and wfid=" & hdnWfId.Value & "   order by wfdid"
        dtnew2 = objUtil.ExeQueryDT(strsql2, "dd2")
        dvnotin2 = dtnew2.DefaultView
        dvnotin2.RowFilter = "TSK_Id <>1"
        status = 1
        For IncrMentX As Integer = 0 To dvnotin2.Count - 1
            Dim iHDX As HiddenField = CType(DLDigitalSign.Items(IncrMentX).FindControl("hdXCoordinate"), HiddenField)
            Dim iHDY As HiddenField = CType(DLDigitalSign.Items(IncrMentX).FindControl("hdYCoordinate"), HiddenField)
            objETWFT.DocId = hdndocid.Value
            objETWFT.Site_Id = hdnsiteid.Value
            objETWFT.SiteVersion = hdnversion.Value
            objETWFT.Status = status
            objETWFT.UserType = dvnotin2(IncrMentX).Item(3).ToString
            objETWFT.UsrRole = dvnotin2(IncrMentX).Item(4).ToString
            objETWFT.WFId = dvnotin2(IncrMentX).Item(1).ToString
            objETWFT.TSK_Id = dvnotin2(IncrMentX).Item(5).ToString
            objETWFT.UGP_Id = dvnotin2(IncrMentX).Item("grpId").ToString
            Dim introw As Integer = 0
            If Request.QueryString("rw") IsNot Nothing Then
                introw = Request.QueryString("rw")
            End If
            Dim strversion As String = Request.Browser.Type
            If InStr(strversion, "IE6") > 0 Then
                If dvnotin2.Count = 2 Then
                    If IncrMentX = 0 Then
                        objETWFT.XVal = iHDX.Value + 30
                    Else
                        objETWFT.XVal = (iHDX.Value / 2) + 43 + (intCount * 30)
                    End If
                Else
                    objETWFT.XVal = (iHDX.Value / 2) + (intCount * 30)
                End If
                objETWFT.YVal = 185 + (iHDY.Value - 791) + (IncrY * 52) - (introw * 40)
            ElseIf InStr(strversion, "IE7") > 0 Then
                If dvnotin2.Count = 2 Then
                    If IncrMentX = 0 Then
                        objETWFT.XVal = iHDX.Value + 30
                    Else
                        objETWFT.XVal = (iHDX.Value / 2) + 43 + (intCount * 30)
                    End If
                Else
                    objETWFT.XVal = (iHDX.Value / 2) + (intCount * 30)
                End If
                objETWFT.YVal = 185 + (iHDY.Value - 791) + (IncrY * 52) - (introw * 40)
            ElseIf InStr(strversion, "IE8") > 0 Then
                If dvnotin2.Count = 2 Then
                    If IncrMentX = 0 Then
                        objETWFT.XVal = iHDX.Value + 33
                    Else
                        objETWFT.XVal = (iHDX.Value / 2) + 44 + (intCount * 30)
                    End If
                Else
                    objETWFT.XVal = (iHDX.Value / 2) + (intCount * 30)
                End If
                objETWFT.YVal = 195 + (791 - iHDY.Value) + (IncrY * 52) - (introw * 14)
            ElseIf InStr(strversion, "Fire") > 0 Then
                If dvnotin2.Count = 2 Then
                    If IncrMentX = 0 Then
                        objETWFT.XVal = iHDX.Value + 30
                    Else
                        objETWFT.XVal = (iHDX.Value / 2) + 56 + (intCount * 30)
                    End If
                Else
                    objETWFT.XVal = (iHDX.Value / 2) + (intCount * 30)
                End If
                objETWFT.YVal = 200 + (791 - iHDY.Value) + (IncrY * 52) + (introw * 12)
            Else
                If dvnotin2.Count = 2 Then
                    If IncrMentX = 0 Then
                        objETWFT.XVal = iHDX.Value + 30
                    Else
                        objETWFT.XVal = (iHDX.Value / 2) + 43 + (intCount * 30)
                    End If
                Else
                    objETWFT.XVal = (iHDX.Value / 2) + (intCount * 30)
                End If
                objETWFT.YVal = 185 + (iHDY.Value - 791) + (IncrY * 52) - (introw * 40)
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
            objETWFT.PageNo = Y
            objETWFT.Site_Id = hdnsiteid.Value
            objdb.ExeNonQuery("update wccWfTransaction set xval=" & objETWFT.XVal & ",yval=" & objETWFT.YVal & ", pageno=" & objETWFT.PageNo & " where site_id= " & objETWFT.Site_Id & " and siteversion= " & objETWFT.SiteVersion & " and docid= " & objETWFT.DocId & " and tsk_id=" & objETWFT.TSK_Id & "")
        Next
    End Sub
End Class
