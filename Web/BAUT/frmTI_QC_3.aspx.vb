Imports System.Data
Imports System.IO
Imports Common
Imports Entities
Imports BusinessLogic
Imports QCFramework
Imports System.Collections.Generic
Imports NSNCustomizeConfiguration

Partial Class BAUT_frmTI_QC
    Inherits System.Web.UI.Page
    Dim dt As New DataTable
    Dim objUtil As New DBUtil
    Dim str As String
    Dim cst As New Constants
    Dim objBO As New BOSiteDocs
    Dim docId As Integer = 0
    Dim i, j, k, l As Integer
    Dim strsql As String
    Dim objdo As New ETWFTransaction
    Dim dt1 As New DataTable
    Dim objET As New ETAuditTrail
    Dim objBOAT As New BoAuditTrail
    Dim roleid As Integer
    Dim grp As String
    Dim FileNamePath As String
    Dim FileNameOnly As String
    Dim ReFileName As String
    Dim objBOM As New BOMailReport
    Dim objET1 As New ETSiteDoc
    Dim objdb As New DBUtil
    Dim objmail As New TakeMail
    Dim objCommon As New CommonSite
    Dim qccontroller As New CoreController
    Dim qcinfo As New CoreInfo

    Public Sub RegisterScriptDescriptors(ByVal extenderControl As IExtenderControl)

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        btnDate.Attributes.Add("onclick", "javascript:popUpCalendar(this,txtLUpdate,'dd-mmm-yyyy');return false;")
        BtnCalendar.Attributes.Add("onclick", "javascript:popUpCalendar(this,TxtOnAirDate,'dd-mmm-yyyy');return false;")
        ImgCalAcpDate.Attributes.Add("onclick", "javascript:popUpCalendar(this,TxtAcpDate,'dd-mmm-yyyy');return false;")
        BtnIntDate.Attributes.Add("onclick", "javascript:popUpCalendar(this,TxtIntDate,'dd-mmm-yyyy');return false;")
        If Not IsPostBack Then
            If Not Request.QueryString("id") Is Nothing Then
                btnSubmitReject.Visible = False
                TxtAcpDate.Text = String.Format("{0:dd-MMMM-yyyy}", DateTime.Now)
                TxtAcpDate.Visible = False
                lblAcpDate.Text = ""
                TxtExtConfig.Visible = False
                Binddata()
                dt = objBO.uspSiteTIDocList(Request.QueryString("id"))
                grdDocuments.DataSource = dt
                grdDocuments.DataBind()
                Dim dtGetATPDocList As DataTable = objUtil.ExeQueryDT("exec uspSiteGetATPDocList " & Request.QueryString("id"), "dt")
                'grdDocuments.DataSource = dtGetATPDocList
                'grdDocuments.DataBind()
                grdDocuments.Columns(1).Visible = False
                grdDocuments.Columns(2).Visible = False
                grdDocuments.Columns(4).Visible = False
                GrdDocPanel.DataSource = dt
                GrdDocPanel.DataBind()
                GrdDocPanelPrint.DataSource = dt
                GrdDocPanelPrint.DataBind()
                'grdDocuments.Visible = False
                GrdDocPanelPrint.Visible = False
                getBindOld()
            End If
        End If
        ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScriptdd", "getControlPosition();", True)
    End Sub
    Sub getBindOld()
        dt = objUtil.ExeQueryDT("exec [uspViewODQC] " & Request.QueryString("id"), "ODQC")
        If (dt.Rows.Count > 0) Then
            hdSno.Value = dt.Rows(0)("sno").ToString()
            txtTWork.Text = dt.Rows(0)("TypeOfWork").ToString()
            txtNEType.Text = dt.Rows(0)("NEType").ToString()
            txtBSCName.Text = dt.Rows(0)("BSCName").ToString()
            txtNSiteID.Text = dt.Rows(0)("NewSiteID").ToString()
            txtLAC.Text = dt.Rows(0)("LAC").ToString()
            txtCI.Text = dt.Rows(0)("CI").ToString()
            txtOnAirCon.Text = dt.Rows(0)("OnAirCon").ToString()
            lblLUpdate.Text = dt.Rows(0)("ludate1").ToString()
            txtLUpdate.Value = dt.Rows(0)("ludate1").ToString()
            lblVer.Text = dt.Rows(0)("luversion").ToString()
            txtVer.Text = dt.Rows(0)("luversion").ToString()
            TxtIntDate.Text = String.Format("{0:dd-MMMM-yyyy}", Convert.ToDateTime(dt.Rows(0)("IntegrationDate")))
            TxtOnAirDate.Text = String.Format("{0:dd-MMMM-yyyy}", Convert.ToDateTime(dt.Rows(0)("OnAirDate")))
            'TxtAcpDate.Text = String.Format("{0:dd-MMMM-yyyy}", Convert.ToDateTime(dt.Rows(0)("AcceptanceDate")))
            TxtBCFET.Text = dt.Rows(0)("BCFET").ToString()
            lblExtConfig.Text = dt.Rows(0)("existingCon").ToString()
            txtOnAirCon.Text = dt.Rows(0)("onaircon").ToString()
            TxtReasonOfReviewDelay.Text = dt.Rows(0)("ReasonDelay").ToString()
            TxtClutterType.Text = dt.Rows(0)("ClutterType").ToString()
            TxtBTSType.Text = dt.Rows(0)("BTSType").ToString()
            TxtOSSName.Text = dt.Rows(0)("OSSName").ToString()

            If (dt.Rows(0)("ChkDrive").ToString().ToLower() = "true") Then
                ChkDrive.Checked = True
            End If
            If (dt.Rows(0)("ChkKPI").ToString().ToLower() = "true") Then
                ChkKPI.Checked = True
            End If
            If (dt.Rows(0)("ChkAlarm").ToString().ToLower() = "true") Then
                ChkFAlarm.Checked = True
            End If

            'lblExtConfig.Text = TxtExtConfig.Text
            'LblBCFET.Text = TxtBCFET.Text
            'lblBSCName.Text = dt.Rows(0)("BSCName").ToString()
            'lblTWork.Text = dt.Rows(0)("TypeOfWork").ToString()
            'lblNEType.Text = dt.Rows(0)("NEType").ToString()
            'lblNSiteID.Text = dt.Rows(0)("NewSiteID").ToString()
            'lblLAC.Text = dt.Rows(0)("LAC").ToString()
            'lblCI.Text = dt.Rows(0)("CI").ToString()
            'lblOnAirCon.Text = txtOnAirCon.Text
        End If
    End Sub
    Sub Binddata()
        LblSubmissionDate.Text = String.Format("{0:dd-MMMM-yyyy}", DateTime.Now)
        str = "Exec uspTIQCBautOnLine " & Request.QueryString("id")
        dt = objUtil.ExeQueryDT(str, "SiteDoc1")
        If dt.Rows.Count > 0 Then
            lblPO.Text = dt.Rows(0).Item("pono").ToString
            LblPono.Text = dt.Rows(0).Item("pono").ToString
            lblSiteID.Text = dt.Rows(0).Item("site_no").ToString
            lblSiteName.Text = dt.Rows(0).Item("site_name").ToString
            lblBand.Text = dt.Rows(0).Item("band").ToString
            txtTWork.Text = dt.Rows(0).Item("Scope").ToString
            lblExtConfig.Text = dt.Rows(0).Item("existconfig").ToString
            If Not String.IsNullOrEmpty(Convert.ToString(dt.Rows(0).Item("siteintegration"))) Then
                TxtIntDate.Text = String.Format("{0:dd-MMMM-yyyy}", Convert.ToDateTime(dt.Rows(0).Item("siteintegration")))
            End If

            'If Not String.IsNullOrEmpty(Convert.ToString(dt.Rows(0).Item("siteacponbast"))) Then
            '    TxtAcpDate.Text = String.Format("{0:dd-MMMM-yyyy}", Convert.ToDateTime(dt.Rows(0).Item("siteacponbast")))
            'End If


            '-----------Manual insert value (temp session)---------------
            'lblOnAirDate.Text = dt.Rows(0).Item("siteacponair").ToString
            'lblAcpDate.Text = dt.Rows(0).Item("siteacponbast").ToString
            '------------------------------------------------------------

            If lblIntDate.Text = "" Then lblIntDate.Text = ""
            'If lblOnAirDate.Text = "" Then lblOnAirDate.Text = ""
            'If lblAcpDate.Text = "" Then lblAcpDate.Text = ""

            hdnSiteno.Value = dt.Rows(0).Item("site_no").ToString
            hdnsiteid.Value = dt.Rows(0).Item("SiteId").ToString
            hdnversion.Value = dt.Rows(0).Item("version").ToString
            hdnWfId.Value = dt.Rows(0).Item("WF_Id").ToString
            hdndocid.Value = dt.Rows(0).Item("docid").ToString
            hdnScope.Value = dt.Rows(0).Item("Scope").ToString
            hdnDGBox.Value = dt.Rows(0).Item("DGBox").ToString
            ' i = objBO.uspCheckIntegration(hdndocid.Value, hdnSiteno.Value)
            i = objdb.ExeQueryScalar("exec uspCheckIntegration  '" & hdndocid.Value & "' ,'" & hdnSiteno.Value & "'," & hdnversion.Value & "")
            i = 1
            Select Case i
                Case 1
                    Dochecking()
                Case 2
                    hdnKeyVal.Value = 0
                    ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "Showmain('Int');", True)
                Case 3
                    Dochecking()
                Case 4
                    hdnKeyVal.Value = 0
                    ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScriptI", "Showmain('IntD');", True)
                Case 3
            End Select
            str = "Exec [uspGetOnLineFormBind] " & dt.Rows(0).Item("WF_Id").ToString & "," & dt.Rows(0).Item("SiteId").ToString
            dt = objUtil.ExeQueryDT(str, "SiteDoc1")
            Dim dtSignPosition As DataView = dt.DefaultView
            dtSignPosition.Sort = "tsk_id desc"
            DLDigitalSign.DataSource = dtSignPosition
            DLDigitalSign.DataBind()
            HDDgSignTotal.Value = dt.Rows.Count
        Else
            hdnsiteid.Value = 0
            hdnversion.Value = 0
            hdnWfId.Value = 0
            hdndocid.Value = 0

        End If
        ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScriptdd", "getControlPosition();", True)
    End Sub
    Sub Dochecking()
        roleid = Session("Role_Id")
        grp = Session("User_Type")
        If objBO.uspApprRequired(hdnsiteid.Value, hdndocid.Value, hdnversion.Value) <> 0 Then
            If objBO.verifypermission(hdndocid.Value, roleid, grp) <> 0 Then
                Select Case objBO.DocUploadverify(hdnsiteid.Value, hdndocid.Value, hdnversion.Value).ToString
                    Case 1  ''This document not attached to this site
                        hdnKeyVal.Value = 1
                        btnGenerate.Attributes.Clear()
                        'belowcase not going to happen we need to test the scenariao.
                        btnGenerate.Attributes.Add("onclick", "javascript:return " + "confirm('This Document Not attached to this site,Do you want to upload?')")
                    Case 2 '' means document was already uploaded for this version of site,do u want to overwrite
                        hdnKeyVal.Value = 2
                        btnGenerate.Attributes.Clear()
                        btnGenerate.Attributes.Add("onclick", "javascript:return " + "confirm('This Document already uploaded Do you want to replace?')")
                    Case 3 '' means document was not yet uploaded for thissite
                        hdnKeyVal.Value = 3
                        btnGenerate.Attributes.Clear()
                    Case 4 'means document already processed for sencod stage cannot upload
                        hdnKeyVal.Value = 0
                        ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScripta", "Showmain('2sta');", True)
                        'makevisible()
                        Exit Sub
                        'Response.Redirect("frmSiteDocUploadTree.aspx")
                End Select
            Else
                If Session("Role_Id") = 1 Then ''if role is sysadmin then no need to verify permission everthing permitted.
                    Select Case objBO.DocUploadverify(hdnsiteid.Value, hdndocid.Value, hdnversion.Value).ToString
                        Case 1  ''This document not attached to this site
                            hdnKeyVal.Value = 1
                            btnGenerate.Attributes.Clear()
                            'belowcase not going to happen we need to test the scenariao.
                            btnGenerate.Attributes.Add("onclick", "javascript:return " + "confirm('This Document Not attached to this site,Do you want to  upload?')")
                        Case 2 '' means document was already uploaded for this version of site,do u want to overwrite
                            hdnKeyVal.Value = 2
                            btnGenerate.Attributes.Clear()
                            btnGenerate.Attributes.Add("onclick", "javascript:return " + "confirm('This Document already uploaded Do you want to replace?')")
                        Case 3 '' means document was not yet uploaded for thissite
                            hdnKeyVal.Value = 3
                            btnGenerate.Attributes.Clear()
                        Case 4 'means document already processed for sencod stage cannot upload
                            hdnKeyVal.Value = 0
                            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScriptb", "Showmain('2sta');", True)
                            Exit Sub
                            'Response.Redirect("frmSiteDocUploadTree.aspx")
                    End Select
                Else
                    'Page.FindControl("Panel1").Visible = False
                    hdnKeyVal.Value = 0
                    ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScriptc", "Showmain('nop');", True)
                End If
            End If
        Else 'Seeta 20081230 Appr Not Required
            Select Case objBO.DocUploadverify(hdnsiteid.Value, hdndocid.Value, hdnversion.Value).ToString
                Case 1  ''This document not attached to this site
                    hdnKeyVal.Value = 1
                    btnGenerate.Attributes.Clear()
                    'belowcase not going to happen we need to test the scenariao.
                    btnGenerate.Attributes.Add("onclick", "javascript:return " + "confirm('This Document Not attached to this site,Do you want to  upload?')")
                Case 2 '' means document was already uploaded for this version of site,do u want to overwrite
                    hdnKeyVal.Value = 2
                    btnGenerate.Attributes.Clear()
                    btnGenerate.Attributes.Add("onclick", "javascript:return " + "confirm('This Document already uploaded Do you want to replace?')")
                Case 3 '' means document was not yet uploaded for thissite
                    hdnKeyVal.Value = 3
                    btnGenerate.Attributes.Clear()
                Case 4 'means document already processed for sencod stage cannot upload
                    hdnKeyVal.Value = 0
                    ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScriptd", "Showmain('2sta');", True)
                    Exit Sub
            End Select
        End If
    End Sub
    Function CreatePDFFile(ByVal StrPath As String) As String
        StrPath = StrPath.Replace(" \", "\").ToString 'bugfix100816 causing error when the path containing space before \
        Dim dtba As New DataTable
        Dim filenameorg As String
        Dim ReFileName As String
        filenameorg = hdnSiteno.Value & "-" & Constants._Doc_SQAC & "-" & Format(CDate(DateTime.Now), "ddMMyyyyHHss")
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
        sw.WriteLine(".lblText{font-family: verdana;font-size: 7.5pt;color: #000000;text-align: left;}")
        sw.WriteLine(".lblBText{font-family: verdana;font-size: 7.5pt;color: #000000;text-align: left;font-weight: bold;}")
        sw.WriteLine(".lblBold{font-family: verdana;font-size: 12pt;color: #000000;font-weight: bold;}")
        sw.WriteLine(".textFieldStyle{background-color: white;border: 1px solid;color: black;font-family: verdana;font-size: 9pt;}")
        sw.WriteLine(".GridHeader{color: #0e1b42;background-color: Orange;font-size: 9pt;font-family: Verdana;text-align: Left;vertical-align: bottom;font-weight: bold;}")
        sw.WriteLine(".GridEvenRows{background-color: #e3e3e3;vertical-align: top;font-family: verdana;font-size: 7.5pt;color: #000000;}")
        sw.WriteLine(".GridOddRows{background-color: white;vertical-align: top;font-family: verdana;font-size: 7.5pt;color: #000000;}")
        sw.WriteLine(".GrdDocPanelRows{background-color: white;vertical-align: middle;font-family: verdana;font-size: 7.5pt;color: #000000;}")
        sw.WriteLine(".PagerTitle{font-size: 8pt;background-color: #cddbbf;text-align: Right;vertical-align: middle;color: #25375b;font-weight: bold;}")
        sw.WriteLine(".Hcap{height: 5px;}")
        sw.WriteLine(".VCap{width: 10px;}")
        sw.WriteLine(".siteATTPanel{margin-top:10px;  height:200px;}")
        sw.WriteLine("</style></head>")
        sw.WriteLine("<body class=""MainCSS"">")
        dvPrint.RenderControl(sw)
        sw.WriteLine("</body>")
        sw.WriteLine("</html>")
        sw.Close()
        sw.Dispose()
        Return Common.TakeFileUpload.ConvertAnyFormatToPDFHtmlNew(StrPath & ReFileName, StrPath, filenameorg)
    End Function
    Sub uploaddocument(ByVal vers As Integer, ByVal keyval As Integer)
        dt = objBO.getbautdocdetailsNEW(hdndocid.Value) '(Constants._Doc_SSR)
        Dim sec As String = dt.Rows(0).Item("sec_name").ToString
        Dim subsec As String = dt.Rows(0).Item("subsec_name").ToString
        Dim secpath As String = ""
        Dim ft As String = ""
        Dim path As String = ""
        Dim filenameorg As String
        FileNameOnly = "-" & Constants._Doc_SQAC & "-"
        filenameorg = hdnSiteno.Value & FileNameOnly & Format(CDate(DateTime.Now), "ddMMyyyyHHss")
        ReFileName = filenameorg & ".htm"
        secpath = IIf(subsec = "" Or subsec = "0", sec & "\", sec & "\" & subsec & "\")
        ft = ConfigurationManager.AppSettings("Type") & lblPO.Text & "-" & hdnScope.Value & "\"
        path = ConfigurationManager.AppSettings("Fpath") & hdnSiteno.Value & ft & secpath
        Dim strResult As String = DOInsertTrans(hdnsiteid.Value, hdndocid.Value, vers, path)
        Dim DocPath As String = ""
        If strResult = "0" Then
            DocPath = hdnSiteno.Value & ft & secpath & CreatePDFFile(Replace(path, "  \", "\"))
        ElseIf strResult = "1" Then
            DocPath = hdnSiteno.Value & ft & secpath & CreatePDFFile(Replace(path, "  \", "\"))
        Else
            DocPath = hdnSiteno.Value & ft & secpath & CreatePDFFile(Replace(path, "  \", "\"))
        End If
        Response.Write(DocPath)
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
            .PONo = lblPO.Text
        End With
        objBO.updatedocupload(objET1)
        'Dim strsql As String = "Update bautmaster set Pstatus=1 where Site_ID = " & hdnsiteid.Value & " and SiteVersion =" & hdnversion.Value & " and PONO = '" & lblPO.Text & "'"
        'objUtil.ExeUpdate(strsql)
        'sendmail2()
        chek4alldoc() ' for messaage to previous screen ' and saving final docupload date in reporttable
        'for BAUT
        'objBo.check4BAUTBAST(hdnsiteid.Value, hdnversion.Value, 1, lblPONO.Text, Session("User_Name"))
        'objdb.ExeQueryScalar("exec uspcheck4BAUTBAST " & hdnsiteid.Value & " ," & hdnversion.Value & "," & 1 & ",'" & lblPO.Text & "','" & Session("User_Name") & "'," & ConfigurationManager.AppSettings("BAUTID") & "," & ConfigurationManager.AppSettings("BASTID") & " ")
        'for BAST1
        'objBo.check4BAUTBAST(hdnsiteid.Value, hdnversion.Value, 2, lblPONO.Text, Session("User_Name"))
        'objdb.ExeQueryScalar("exec uspcheck4BAUTBAST " & hdnsiteid.Value & " ," & hdnversion.Value & "," & 2 & ",'" & lblPO.Text & "','" & Session("User_Name") & "'," & ConfigurationManager.AppSettings("BAUTID") & "," & ConfigurationManager.AppSettings("BASTID") & " ")
        'Fill Transaction table
        AuditTrail()
        SendGenerateMail()
        If hdnready4baut.Value = 1 Then
            If String.IsNullOrEmpty(Request.QueryString("from")) Then
                Response.Redirect("../PO/frmSiteDocUploadTree.aspx?ready=yes")
            Else
                Response.Redirect("../PO/frmQCReadyCreation.aspx")
            End If
        Else
            If String.IsNullOrEmpty(Request.QueryString("from")) Then
                Response.Redirect("../PO/frmSiteDocUploadTree.aspx")
            Else
                Response.Redirect("../PO/frmQCReadyCreation.aspx")
            End If
        End If
    End Sub
    Function DOInsertTrans(ByVal siteid As String, ByVal docName As String, ByVal version As Integer, ByVal path As String) As String
        docId = hdndocid.Value
        Dim dtNew As DataTable
        If hdnWfId.Value <> 0 Then
            dtNew = objBO.doinserttrans(hdnWfId.Value, docId)
            If hdnDGBox.Value = True Then
                If dtNew.Rows.Count > 0 Then
                    objBO.DelWFTransaction(hdndocid.Value, dtNew.Rows(0).Item(1).ToString, hdnsiteid.Value, hdnversion.Value)
                    Dim bb As Boolean
                    Dim aa As Integer = 0
                    Dim status As Integer
                    For aa = 0 To dtNew.Rows.Count - 1
                        fillDetails()
                        If (dtNew.Rows(aa).Item(5).ToString <> "1") Then status = 1
                        objdo.DocId = docId
                        objdo.UserType = dtNew.Rows(aa).Item(3).ToString
                        objdo.UsrRole = dtNew.Rows(aa).Item(4).ToString
                        objdo.WFId = dtNew.Rows(aa).Item(1).ToString
                        objdo.UGP_Id = dtNew.Rows(aa).Item("grpId").ToString
                        objdo.TSK_Id = dtNew.Rows(aa).Item(5).ToString
                        objdo.XVal = dtNew.Rows(aa).Item("X_Coordinate").ToString
                        objdo.YVal = dtNew.Rows(aa).Item("Y_Coordinate").ToString
                        objdo.PageNo = dtNew.Rows(aa).Item("PageNo").ToString
                        objdo.Status = status
                        objBO.uspwftransactionIU(objdo)
                        If (dtNew.Rows(aa).Item(5).ToString <> "1") Then
                            If bb = False Then
                                'sendmailTrans(hdnsiteid.Value, dtNew.Rows(aa).Item(3).ToString, dtNew.Rows(aa).Item(4).ToString)
                                Try
                                    objmail.sendMailTrans(0, hdnsiteid.Value, dtNew.Rows(aa).Item(3).ToString, dtNew.Rows(aa).Item(4).ToString, ConfigurationManager.AppSettings("uploadmailconst"))
                                Catch ex As Exception
                                    objdb.ExeNonQuery("exec uspErrLog '', 'mailsending','" & ex.Message.ToString.Replace("'", "''") & "','sendmailbaut'")
                                End Try

                                bb = True
                            End If
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
            objBO.uspwftransactionNOTWFI(docId, 0, hdnsiteid.Value, hdnversion.Value, 1, Session("User_Id"), status, 2, Session("User_Name"))
            Return "0"
        End If
    End Function
    Sub CreateXY()
        Dim dtNew As DataTable
        Dim strSql1 As String = "select wfdid,wfid,sorder,grpcode,roleid,a.tsk_id,a.grpId,tt.tskname,0 as   X_Coordinate,0 as  Y_Coordinate," & _
        "0 as  PageNo,IsNull(a.Sorder,0)Sno from twfdefinition A inner join tusertype B on a.grpid=b.grpid inner join ttask as tt on tt.tsk_id=a.tsk_id " & _
        " where tt.TSK_Id=1 and wfid=" & hdnWfId.Value & " order by wfdid"
        dtNew = objUtil.ExeQueryDT(strSql1, "dd")
        ' dtNew = objdbutil.ExeQueryScalar("select wf_id from sitedoc where siteid='" & siteid & "' and docid=" & docid & " and version =" & version & "")
        If dtNew.Rows.Count > 0 Then
            objBO.DelWFTransaction(hdndocid.Value, dtNew.Rows(0).Item(1).ToString, hdnsiteid.Value, hdnversion.Value)
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
            objBO.uspwftransactionIU(objdo)
        Next
        strSql1 = "select wfdid,wfid,sorder,grpcode,roleid,a.tsk_id,a.grpId,tt.tskname,0 as   X_Coordinate,0 as  Y_Coordinate," & _
        "0 as  PageNo,IsNull(a.Sorder,0)Sno from twfdefinition A inner join tusertype B on a.grpid=b.grpid inner join ttask as tt on tt.tsk_id=a.tsk_id " & _
        " where tt.TSK_Id <> 1 and wfid=" & hdnWfId.Value & "   order by sorder"
        DtNewOne = objUtil.ExeQueryDT(strSql1, "dd")
        dvNotIn = DtNewOne.DefaultView
        dvNotIn.RowFilter = "TSK_Id <>1"
        status = 1
        Dim bb As Boolean, intCount As Integer = 0, IncrMentY As Integer = 0
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

        Dim dtnew2 As DataTable
        Dim dvnotin2 As DataView

        strSql1 = "select wfdid,wfid,sorder,grpcode,roleid,a.tsk_id,a.grpId,tt.tskname,0 as   X_Coordinate,0 as  Y_Coordinate," & _
        "0 as  PageNo,IsNull(a.Sorder,0)Sno from twfdefinition A inner join tusertype B on a.grpid=b.grpid inner join ttask as tt on tt.tsk_id=a.tsk_id " & _
        " where tt.TSK_Id not in (1,5) and wfid=" & hdnWfId.Value & "   order by wfdid"
        dtnew2 = objUtil.ExeQueryDT(strSql1, "dd")
        dvnotin2 = dtnew2.DefaultView
        dvnotin2.RowFilter = "TSK_Id <>1"
        dvnotin2.Sort = "TSK_Id desc"
        status = 1

        For IncrMentX As Integer = 0 To dvnotin2.Count - 1
            'Dim iHDX As HiddenField = CType(DLDigitalSign.Items(IncrMentX).FindControl("hdXCoordinate"), HiddenField)
            'Dim iHDY As HiddenField = CType(DLDigitalSign.Items(IncrMentX).FindControl("hdYCoordinate"), HiddenField)
            fillDetails()
            objdo.Status = status
            objdo.UserType = dvnotin2(IncrMentX).Item(3).ToString
            objdo.UsrRole = dvnotin2(IncrMentX).Item(4).ToString
            objdo.WFId = dvnotin2(IncrMentX).Item(1).ToString
            objdo.TSK_Id = dvnotin2(IncrMentX).Item(5).ToString
            objdo.UGP_Id = dvnotin2(IncrMentX).Item("grpId").ToString
            objdo.PageNo = 1
            Dim strversion = Request.Browser.Type
            If InStr(strversion, "IE6") > 0 Or InStr(strversion, "IE7") > 0 Or InStr(strversion, "IE8") > 0 Or InStr(strversion, "IE9") > 0 Then
                If IncrMentX = 0 Then
                    objdo.XVal = 15
                Else
                    objdo.XVal = 300
                End If
            End If
            objdo.YVal = 210
            objdb.ExeNonQuery("update wftransaction set xval=" & objdo.XVal & ",yval=" & objdo.YVal & ", pageno=" & objdo.PageNo & " where site_id= " & objdo.Site_Id & " and siteversion= " & objdo.SiteVersion & " and docid= " & objdo.DocId & " and tsk_id=" & objdo.TSK_Id & "")
        Next
    End Sub
    Sub fillDetails()
        objdo.DocId = hdndocid.Value
        objdo.Site_Id = hdnsiteid.Value
        objdo.SiteVersion = hdnversion.Value
        objdo.AT.RStatus = Constants.STATUS_ACTIVE
        objdo.AT.LMBY = Session("User_Id")
    End Sub
    Sub DOThis(ByVal sender As Object, ByVal e As System.EventArgs)
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

    Protected Sub BtnSubmitReject_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmitReject.Click
        Dim docReject As Boolean = False
        Dim isCorrectLoop As Boolean = True
        Dim strWarningMessage As String = String.Empty
        Dim listdocrejects As New List(Of DocumentRejectInfo)
        For intCount As Integer = 0 To grdDocuments.Rows.Count - 1
            Dim rdl1 As RadioButtonList = CType(grdDocuments.Rows(intCount).FindControl("rdbstatus"), RadioButtonList)
            If rdl1.SelectedValue = 1 Then
                Dim swid As Label = CType(grdDocuments.Rows(intCount).FindControl("LblSWId"), Label)
                Dim docname As Label = CType(grdDocuments.Rows(intCount).FindControl("LblDocName"), Label)
                Dim docid As Label = CType(grdDocuments.Rows(intCount).FindControl("LblDocid"), Label)
                Dim txtRemarks As TextBox = CType(grdDocuments.Rows(intCount).FindControl("txtRemarks"), TextBox)
                If String.IsNullOrEmpty(txtRemarks.Text) Then
                    ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScriptd", "alert('Please put remarks of rejection for " & docname.Text & " document');", True)
                    isCorrectLoop = False
                    Exit For
                Else
                    Dim docrejectinfo As New DocumentRejectInfo
                    docrejectinfo.Docid = Integer.Parse(docid.Text)
                    docrejectinfo.Docname = docname.Text
                    docrejectinfo.SWId = Convert.ToInt32(swid.Text)
                    docrejectinfo.Remarks = txtRemarks.Text
                    docrejectinfo.LMBY = objUtil.ExeQueryScalarString("select top 1 lmby from wftransaction where site_id=" & hdnsiteid.Value & " and siteversion=" & hdnversion.Value & " and rstatus = 2 and docid=" & docid.Text & " order by enddatetime desc")
                    listdocrejects.Add(docrejectinfo)
                End If
            End If
        Next

        If isCorrectLoop = True Then
            If listdocrejects.Count > 0 Then
                DocumentReject(listdocrejects)
            End If
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

    Protected Sub GrdDocPanel_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GrdDocPanel.RowDataBound
        Select Case e.Row.RowType
            Case DataControlRowType.DataRow
                Dim LblDocPath As Label = CType(e.Row.FindControl("LblDocPath"), Label)
                If Not String.IsNullOrEmpty(LblDocPath.Text) Then
                    Dim LblSWId As Label = CType(e.Row.FindControl("LblSWId"), Label)
                    Dim ChkChecked As CheckBox = CType(e.Row.FindControl("ChkChecked"), CheckBox)
                    ChkChecked.Checked = True
                    Dim url As String = "../PO/frmViewDocument.aspx?id=" & LblSWId.Text
                    e.Row.Cells(1).Text = "<a href='#' onclick=""window.open('" & url & "','mywindow','status=yes,menubar=no,scrollbars=yes,resizable=yes,width=700px,height=600px')"">" & e.Row.Cells(1).Text & "</a>"
                    ChkChecked.Enabled = False
                End If
        End Select
    End Sub

    Protected Sub GrdDocPanelPrint_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GrdDocPanelPrint.RowDataBound
        Select Case e.Row.RowType
            Case DataControlRowType.DataRow
                Dim LblDocPath As Label = CType(e.Row.FindControl("LblDocPath"), Label)
                If Not String.IsNullOrEmpty(LblDocPath.Text) Then
                    Dim ChkChecked As CheckBox = CType(e.Row.FindControl("ChkChecked"), CheckBox)
                    ChkChecked.Checked = True
                End If
        End Select
    End Sub

    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        Return
    End Sub

    Private Sub DocumentReject(ByVal listdocrejects As List(Of DocumentRejectInfo))
        Dim data As DocumentRejectInfo
        For Each data In listdocrejects
            Try
                objUtil.ExeNonQuery("Exec uspBAUTDocReject " & data.SWId & ",'" & data.Remarks & "'," & CommonSite.UserId & "," & CommonSite.RollId & ",'" & CommonSite.UserName & "','" & lblPO.Text & "','" & hdnsiteid.Value & "'," & data.Docid & "," & ConfigurationManager.AppSettings("WCTRBASTID") & "," & ConfigurationManager.AppSettings("BAUTID") & "," & ConfigurationManager.AppSettings("BASTID"))
                SendMailDocumentReject(data.Docname, data.Remarks, data.LMBY)
            Catch ex As Exception
                objdb.ExeNonQuery("exec uspErrLog '', 'Document Reject','" & ex.Message.ToString.Replace("'", "''") & "','sendmailrejectedQCdoc'")
            End Try
        Next

        If String.IsNullOrEmpty(Request.QueryString("from")) Then
            Response.Redirect("../PO/frmSiteDocUploadTree.aspx")
        Else
            Response.Redirect("../PO/frmQCReadyCreation.aspx")
        End If

    End Sub

    Private Sub SendMailDocumentReject(ByVal docname As String, ByVal remarks As String, ByVal lmby As String)
        Dim sbBody As New StringBuilder
        Dim strQuery As String = "select name,phoneNo,email from ebastusers_1 where usr_id=" & lmby
        Dim dtUsers As DataTable = objUtil.ExeQueryDT(strQuery, "dt")
        Dim strCompanyRejected As String = String.Empty
        If CommonSite.UserType.ToLower() = "c" Then
            strCompanyRejected = "Telkomsel"
        ElseIf CommonSite.UserType.ToLower() = "n" Then
            strCompanyRejected = "NSN"
        Else
            strCompanyRejected = "Subcon"
        End If
        If dtUsers.Rows.Count > 0 Then
            sbBody.Append("Dear " & dtUsers.Rows(0).Item(0) & ", <br/><br/>")
            sbBody.Append("There is " & docname & " document of " & lblPO.Text & "-" & lblSiteID.Text & "-" & lblTWork.Text & "-" & Request.QueryString("wpid") & " is Rejected by " & CommonSite.UserName & " " & strCompanyRejected & "<br/>")
            sbBody.Append("Reason of Rejection : " & remarks & "<br/><br/>")
            sbBody.Append("<a href='http://www.telkomsel.nsnebast.com'>Click here</a>" & " to go to e-BAST <br/>")
            sbBody.Append("Powered By EBAST" & "<br/>")
            sbBody.Append("<img src='http://www.telkomsel.nsnebast.com/images/nsn-logo.gif' alt='companylogo' />")
            objmail.SendMailQC(dtUsers.Rows(0).Item(2).ToString(), sbBody.ToString(), docname & " document Rejected")
        End If
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
        objET.DocId = hdndocid.Value
        objET.Task = "1"
        objET.Status = "1"
        objET.Userid = Session("User_Id")
        objET.Roleid = Session("Role_Id")
        objET.fldtype = hdnScope.Value
        objBOAT.uspAuditTrailI(objET)
    End Sub
    Protected Sub btnGenerate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGenerate.Click
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScriptdd", "getControlPosition();", True)
        Dim strQuerCountNotYetUploaded As String = "select count(*) from sitedoc where isuploaded = 0 and siteid =" & hdnsiteid.Value & " and version=" & hdnversion.Value & _
            " and docid in(select doc_id from coddoc where parent_id = (select docid from sitedoc where sw_id=" & Request.QueryString("id") & "))"
        Dim countNotYetUploaded As Integer = objUtil.ExeQueryScalar(strQuerCountNotYetUploaded)
        Dim ATPAlreadyDone As Boolean = IIf(IsATPAlreadyDone(Convert.ToInt32(hdnsiteid.Value), Integer.Parse(hdnversion.Value)) = 0, False, True)
        ATPAlreadyDone = True
        If ATPAlreadyDone = True Then
            If Request.Browser.Browser = "IE" Then
                If countNotYetUploaded = 0 Then
                    lblPreBy.Text = Session("User_Name")
                    hdnRole.Value = Session("Role_Id")
                    str = "Exec uspGetAuthor " & hdnRole.Value
                    dt = objUtil.ExeQueryDT(str, "Trole")
                    lblAuthor.Text = dt.Rows(0).Item("roledesc").ToString
                    If (hdnDGBox.Value = "") Then hdnDGBox.Value = 0
                    If (hdnKeyVal.Value = "") Then hdnKeyVal.Value = 0
                    If (hdnversion.Value = "") Then hdnversion.Value = 0
                    If hdnDGBox.Value = True Then
                        strsql = "select count(*) from docSignPositon where doc_id=" & hdndocid.Value
                        If objUtil.ExeQueryScalar(strsql) > 0 Then
                            btnDate.Visible = False
                            txtTWork.Visible = False
                            txtNEType.Visible = False
                            txtBSCName.Visible = False
                            txtNSiteID.Visible = False
                            txtLAC.Visible = False
                            txtCI.Visible = False
                            txtOnAirCon.Visible = False
                            txtLUpdate.Visible = False
                            txtVer.Visible = False
                            TxtReasonOfReviewDelay.Visible = False
                            TxtBCFET.Visible = False
                            TxtIntDate.Visible = False
                            TxtOnAirDate.Visible = False
                            TxtAcpDate.Visible = False
                            TxtExtConfig.Visible = False
                            BtnCalendar.Visible = False
                            ImgCalAcpDate.Visible = False
                            BtnIntDate.Visible = False
                            TxtClutterType.Visible = False
                            TxtBTSType.Visible = False
                            TxtOSSName.Visible = False
                            lblOnAirDate.Text = TxtOnAirDate.Text
                            'lblAcpDate.Text = TxtAcpDate.Text
                            lblAcpDate.Text = ""
                            LblBCFET.Text = TxtBCFET.Text
                            LblReasonofReviewDelay.Text = TxtReasonOfReviewDelay.Text
                            'lblExtConfig.Text = TxtExtConfig.Text

                            lblTWork.Text = txtTWork.Text
                            If lblTWork.Text = "" Then lblTWork.Text = ""
                            lblNEType.Text = txtNEType.Text
                            If lblNEType.Text = "" Then lblNEType.Text = ""
                            lblBSCName.Text = txtBSCName.Text
                            If lblBSCName.Text = "" Then lblBSCName.Text = ""
                            lblNSiteID.Text = txtNSiteID.Text
                            If lblNSiteID.Text = "" Then lblNSiteID.Text = ""
                            lblLAC.Text = txtLAC.Text
                            If lblLAC.Text = "" Then lblLAC.Text = ""
                            lblCI.Text = txtCI.Text
                            If lblCI.Text = "" Then lblCI.Text = ""
                            lblIntDate.Text = TxtIntDate.Text
                            lblOnAirCon.Text = txtOnAirCon.Text
                            If lblOnAirCon.Text = "" Then lblOnAirCon.Text = ""
                            lblLUpdate.Text = txtLUpdate.Value
                            If lblLUpdate.Text = "" Then lblLUpdate.Text = ""
                            lblVer.Text = txtVer.Text
                            If lblVer.Text = "" Then lblVer.Text = ""
                            lblClutterType.Text = TxtClutterType.Text
                            If TxtClutterType.Text = "" Then lblClutterType.Text = ""
                            lblBTSType.Text = TxtBTSType.Text
                            If TxtBTSType.Text = "" Then lblBTSType.Text = ""
                            LblOSSName.Text = TxtOSSName.Text
                            If TxtOSSName.Text = "" Then LblOSSName.Text = ""
                            btnGenerate.Visible = False
                            grdDocuments.Visible = False
                            GrdDocPanel.Visible = False
                            GrdDocPanelPrint.Visible = True

                            Dim strLUpdate As String
                            If (txtLUpdate.Value.ToString() = "") Then
                                strLUpdate = "NULL"
                            Else
                                strLUpdate = txtLUpdate.Value.ToString()
                            End If

                            'If (hdSno.Value = 0) Then
                            '    str = "Exec uspODQCIU  0," & Request.QueryString("id") & ",'" & lblPO.Text & "','" & lblSiteID.Text & "'," & hdnversion.Value & ",'" & txtTWork.Text & "','" & txtNEType.Text & "','" & txtBSCName.Text & "','" & txtNSiteID.Text & "','" & txtLAC.Text & "','" & txtCI.Text & "','" & txtOnAirCon.Text & "'," & ChkDrive.Checked & "," & ChkKPI.Checked & "," & ChkFAlarm.Checked & "," & IIf(strLUpdate = "", "null", "'" & strLUpdate & "'") & ",'" & objCommon.UserName() & "','" & lblVer.Text & "'"
                            'Else
                            '    str = "Exec uspODQCIU  " & hdSno.Value & "," & Request.QueryString("id") & ",'" & lblPO.Text & "','" & lblSiteID.Text & "'," & hdnversion.Value & ",'" & txtTWork.Text & "','" & txtNEType.Text & "','" & txtBSCName.Text & "','" & txtNSiteID.Text & "','" & txtLAC.Text & "','" & txtCI.Text & "','" & txtOnAirCon.Text & "'," & ChkDrive.Checked & "," & ChkKPI.Checked & "," & ChkFAlarm.Checked & "," & IIf(strLUpdate = "", "null", "'" & strLUpdate & "'") & ",'" & objCommon.UserName() & "','" & lblVer.Text & "'"
                            'End If
                            'objUtil.ExeQueryScalar(str)

                            qcinfo.BCF_ET = TxtBCFET.Text
                            qcinfo.BSC_Name = txtBSCName.Text
                            qcinfo.CI = txtCI.Text
                            qcinfo.DocPathId = Request.QueryString("Id")
                            qcinfo.Existing_Config = lblExtConfig.Text
                            qcinfo.IsDTReport = IIf(ChkDrive.Checked = True, True, False)
                            qcinfo.IsFreeAlarm = IIf(ChkFAlarm.Checked = True, True, False)
                            qcinfo.IsKPI = IIf(ChkKPI.Checked = True, True, False)

                            qcinfo.LAC = txtLAC.Text
                            qcinfo.LMBY = CommonSite.UserId
                            qcinfo.NE_Type = txtNEType.Text
                            qcinfo.Newsite_Id = txtNSiteID.Text
                            qcinfo.OnAir_Config = txtOnAirCon.Text
                            qcinfo.Package_Id = Request.QueryString("wpid")
                            qcinfo.Reason_Delay = TxtReasonOfReviewDelay.Text
                            qcinfo.SubmitDate = DateTime.Now
                            qcinfo.Typeofwork = txtTWork.Text
                            qcinfo.IntegrationDate = DateTime.ParseExact(TxtIntDate.Text, "dd-MMMM-yyyy", Nothing)
                            qcinfo.AcceptanceDate = DateTime.ParseExact(TxtAcpDate.Text, "dd-MMMM-yyyy", Nothing)
                            qcinfo.OnAirDate = DateTime.ParseExact(TxtOnAirDate.Text, "dd-MMMM-yyyy", Nothing)
                            lblRefNO.Text = qccontroller.GetReferenceNo(Convert.ToInt32(hdnsiteid.Value))
                            qcinfo.ClutterType = TxtClutterType.Text
                            qcinfo.BTSType = TxtBTSType.Text
                            qcinfo.QCRefNo = lblRefNO.Text
                            qcinfo.OSSName = LblOSSName.Text
                            If (qccontroller.InsertNewQC(qcinfo)) = True Then
                                uploaddocument(Convert.ToInt32(hdnversion.Value), Convert.ToInt32(hdnKeyVal.Value))
                                SendGenerateMail()
                                If String.IsNullOrEmpty(Request.QueryString("from")) Then
                                    Response.Redirect("../PO/frmSiteDocUploadTree.aspx")
                                Else
                                    Response.Redirect("../PO/frmQCReadyCreation.aspx")
                                End If

                            End If
                            'uploaddocument(Convert.ToInt32(hdnversion.Value), Convert.ToInt32(hdnKeyVal.Value))
                            'Response.Redirect("../PO/frmSiteDocUploadTree.aspx")
                        Else
                            Response.Write("<script>alert('please Set X,Y coordinates for Digital sign in document Template..')</script>")
                        End If
                    Else
                        btnDate.Visible = False
                        txtTWork.Visible = False
                        txtNEType.Visible = False
                        txtBSCName.Visible = False
                        txtNSiteID.Visible = False
                        txtLAC.Visible = False
                        txtCI.Visible = False
                        txtOnAirCon.Visible = False
                        txtLUpdate.Visible = False
                        txtVer.Visible = False
                        TxtReasonOfReviewDelay.Visible = False
                        TxtBCFET.Visible = False
                        TxtIntDate.Visible = False
                        TxtOnAirDate.Visible = False
                        TxtAcpDate.Visible = False
                        TxtExtConfig.Visible = False
                        TxtClutterType.Visible = False
                        TxtBTSType.Visible = False
                        TxtOSSName.Visible = False
                        BtnCalendar.Visible = False
                        ImgCalAcpDate.Visible = False
                        BtnIntDate.Visible = False

                        lblIntDate.Text = TxtIntDate.Text
                        lblOnAirDate.Text = TxtOnAirDate.Text
                        'lblAcpDate.Text = TxtAcpDate.Text
                        lblAcpDate.Text = ""
                        LblBCFET.Text = TxtBCFET.Text
                        LblReasonofReviewDelay.Text = TxtReasonOfReviewDelay.Text
                        'lblExtConfig.Text = TxtExtConfig.Text

                        lblTWork.Text = txtTWork.Text
                        If lblTWork.Text = "" Then lblTWork.Text = ""
                        lblNEType.Text = txtNEType.Text
                        If lblNEType.Text = "" Then lblNEType.Text = ""
                        lblBSCName.Text = txtBSCName.Text
                        If lblBSCName.Text = "" Then lblBSCName.Text = ""
                        lblNSiteID.Text = txtNSiteID.Text
                        If lblNSiteID.Text = "" Then lblNSiteID.Text = ""
                        lblLAC.Text = txtLAC.Text
                        If lblLAC.Text = "" Then lblLAC.Text = ""
                        lblCI.Text = txtCI.Text
                        If lblCI.Text = "" Then lblCI.Text = ""
                        lblOnAirCon.Text = txtOnAirCon.Text
                        If lblOnAirCon.Text = "" Then lblOnAirCon.Text = ""
                        lblLUpdate.Text = txtLUpdate.Value
                        If lblLUpdate.Text = "" Then lblLUpdate.Text = ""
                        lblVer.Text = txtVer.Text
                        If lblVer.Text = "" Then lblVer.Text = ""
                        lblClutterType.Text = TxtClutterType.Text
                        If TxtClutterType.Text = "" Then lblClutterType.Text = ""
                        lblBTSType.Text = TxtBTSType.Text
                        If TxtBTSType.Text = "" Then lblBTSType.Text = ""
                        LblOSSName.Text = TxtOSSName.Text
                        If TxtOSSName.Text = "" Then LblOSSName.Text = ""
                        btnGenerate.Visible = False
                        grdDocuments.Visible = False
                        GrdDocPanel.Visible = False
                        GrdDocPanelPrint.Visible = True

                        Dim strLUpdate As String
                        If (txtLUpdate.Value.ToString() = "") Then
                            strLUpdate = "NULL"
                        Else
                            strLUpdate = txtLUpdate.Value.ToString()
                        End If
                        'If (hdSno.Value = 0) Then
                        '    str = "Exec uspODQCIU  0," & Request.QueryString("id") & ",'" & lblPO.Text & "','" & lblSiteID.Text & "'," & hdnversion.Value & ",'" & txtTWork.Text & "','" & txtNEType.Text & "','" & txtBSCName.Text & "','" & txtNSiteID.Text & "','" & txtLAC.Text & "','" & txtCI.Text & "','" & txtOnAirCon.Text & "'," & ChkDrive.Checked & "," & ChkKPI.Checked & "," & ChkFAlarm.Checked & "," & IIf(strLUpdate = "", "null", "'" & strLUpdate & "'") & ",'" & objCommon.UserName() & "','" & lblVer.Text & "'"
                        'Else
                        '    str = "Exec uspODQCIU  " & hdSno.Value & "," & Request.QueryString("id") & ",'" & lblPO.Text & "','" & lblSiteID.Text & "'," & hdnversion.Value & ",'" & txtTWork.Text & "','" & txtNEType.Text & "','" & txtBSCName.Text & "','" & txtNSiteID.Text & "','" & txtLAC.Text & "','" & txtCI.Text & "','" & txtOnAirCon.Text & "'," & ChkDrive.Checked & "," & ChkKPI.Checked & "," & ChkFAlarm.Checked & "," & IIf(strLUpdate = "", "null", "'" & strLUpdate & "'") & ",'" & objCommon.UserName() & "','" & lblVer.Text & "'"
                        'End If
                        'objUtil.ExeQueryScalar(str) 

                        qcinfo.BCF_ET = TxtBCFET.Text
                        qcinfo.BSC_Name = txtBSCName.Text
                        qcinfo.CI = txtCI.Text
                        qcinfo.DocPathId = Request.QueryString("Id")
                        qcinfo.Existing_Config = lblExtConfig.Text
                        qcinfo.IsDTReport = IIf(ChkDrive.Checked = True, True, False)
                        qcinfo.IsFreeAlarm = IIf(ChkFAlarm.Checked = True, True, False)
                        qcinfo.IsKPI = IIf(ChkKPI.Checked = True, True, False)

                        qcinfo.LAC = txtLAC.Text
                        qcinfo.LMBY = CommonSite.UserId
                        qcinfo.NE_Type = txtNEType.Text
                        qcinfo.Newsite_Id = txtNSiteID.Text
                        qcinfo.OnAir_Config = txtOnAirCon.Text
                        qcinfo.Package_Id = Request.QueryString("wpid")
                        qcinfo.Reason_Delay = TxtReasonOfReviewDelay.Text
                        qcinfo.SubmitDate = DateTime.Now
                        qcinfo.Typeofwork = txtTWork.Text
                        qcinfo.IntegrationDate = DateTime.ParseExact(TxtIntDate.Text, "dd-MMMM-yyyy", Nothing)
                        qcinfo.AcceptanceDate = DateTime.ParseExact(TxtAcpDate.Text, "dd-MMMM-yyyy", Nothing)
                        qcinfo.OnAirDate = DateTime.ParseExact(TxtOnAirDate.Text, "dd-MMMM-yyyy", Nothing)
                        lblRefNO.Text = qccontroller.GetReferenceNo(Convert.ToInt32(hdnsiteid.Value))
                        qcinfo.QCRefNo = lblRefNO.Text
                        qcinfo.ClutterType = TxtClutterType.Text
                        qcinfo.BTSType = TxtBTSType.Text
                        qcinfo.OSSName = LblOSSName.Text

                        If (qccontroller.InsertNewQC(qcinfo)) = True Then
                            uploaddocument(Convert.ToInt32(hdnversion.Value), Convert.ToInt32(hdnKeyVal.Value))
                            SendGenerateMail()
                            If String.IsNullOrEmpty(Request.QueryString("from")) Then
                                Response.Redirect("../PO/frmSiteDocUploadTree.aspx")
                            Else
                                Response.Redirect("../PO/frmQCReadyCreation.aspx")
                            End If
                        End If
                    End If

                Else
                    ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('Document not yet Completed');", True)
                End If
            Else
                ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('Please use Internet Explorer');", True)
            End If
        Else
            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('ATP Document Not Yet Completed');", True)
        End If
        
    End Sub
    Sub XYCo()
        For IncrMentX As Integer = 0 To DLDigitalSign.Items.Count - 1
            Dim iHDX As HiddenField = CType(DLDigitalSign.Items(IncrMentX).FindControl("hdXCoordinate"), HiddenField)
            Dim iHDY As HiddenField = CType(DLDigitalSign.Items(IncrMentX).FindControl("hdYCoordinate"), HiddenField)
            Dim Y As Integer = (Math.Ceiling(iHDY.Value / 800))
            If Y = 0 Then Y = 1
            Response.Write(iHDX.Value.ToString + "Y-" + iHDY.Value.ToString + "p-" + Y.ToString + "<br>")
        Next
    End Sub

    Private Sub SendGenerateMail()
        Dim sbBody As New StringBuilder
        Dim strQuery As String = String.Empty
        strQuery = "select usr.usr_id, email,Name,phoneNo from ebastusers_1 usr " & _
                   "inner join ebastuserrole rol on usr.usr_id = rol.usr_id where usrRole in(" & _
                   "select roleid from twfdefinition where wfid = (select wf_id from sitedoc where sw_id = " & Request.QueryString("id") & ") and sorder = 2) " & _
                   "and rgn_id in(select rgn_id from codsite where site_id = (select siteid from sitedoc where sw_id =" & Request.QueryString("id") & "))"

        Dim dtuseremail As DataTable = objUtil.ExeQueryDT(strQuery, "useratt")
        If dtuseremail.Rows.Count > 0 Then
            sbBody.Append("Dear " & dtuseremail.Rows(0).Item(2) & ", <br/><br/>")
            sbBody.Append("There is QC document of " & lblPO.Text & "-" & lblSiteID.Text & "-" & lblTWork.Text & "-" & Request.QueryString("wpid") & " Waiting on your approval " & "<br/><br />")
            sbBody.Append("<a href='http://www.telkomsel.nsnebast.com'>Click here</a>" & " to go to e-BAST <br/>")
            sbBody.Append("Powered By EBAST" & "<br/>")
            sbBody.Append("<img src='http://www.telkomsel.nsnebast.com/images/nsn-logo.gif' alt='companylogo' />")
            objmail.SendMailQC(dtuseremail.Rows(0).Item(1).ToString(), sbBody.ToString(), "QC Document Waiting")
        End If

    End Sub
End Class

Public Class DocumentRejectInfo

    Private _swid As Int32
    Public Property SWId() As Int32
        Get
            Return _swid
        End Get
        Set(ByVal value As Int32)
            _swid = value
        End Set
    End Property


    Private _docid As Integer
    Public Property Docid() As Integer
        Get
            Return _docid
        End Get
        Set(ByVal value As Integer)
            _docid = value
        End Set
    End Property


    Private _docname As String
    Public Property Docname() As String
        Get
            Return _docname
        End Get
        Set(ByVal value As String)
            _docname = value
        End Set
    End Property


    Private _remarks As String
    Public Property Remarks() As String
        Get
            Return _remarks
        End Get
        Set(ByVal value As String)
            _remarks = value
        End Set
    End Property


    Private _lmby As String
    Public Property LMBY() As String
        Get
            Return _lmby
        End Get
        Set(ByVal value As String)
            _lmby = value
        End Set
    End Property



End Class