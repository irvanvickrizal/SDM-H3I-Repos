Imports System.Data
Imports System.IO
Imports Common
Imports Entities
Imports BusinessLogic
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
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        btnDate.Attributes.Add("onclick", "javascript:popUpCalendar(this,txtLUpdate,'dd-mmm-yyyy');return false;")
        If Not IsPostBack Then
            If Not Request.QueryString("id") Is Nothing Then
                Binddata()
                dt = objBO.uspSiteTIDocList(Request.QueryString("id"))
                grddocuments.DataSource = dt
                grddocuments.DataBind()
                grddocuments.Columns(1).Visible = False
                grddocuments.Columns(2).Visible = False
                grddocuments.Columns(4).Visible = False
                grddocuments.Columns(5).Visible = False
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
            lblTWork.Text = dt.Rows(0)("TypeOfWork").ToString()
            lblNEType.Text = dt.Rows(0)("NEType").ToString()
            txtNEType.Text = dt.Rows(0)("NEType").ToString()
            lblBSCName.Text = dt.Rows(0)("BSCName").ToString()
            txtBSCName.Text = dt.Rows(0)("BSCName").ToString()
            lblNSiteID.Text = dt.Rows(0)("NewSiteID").ToString()
            txtNSiteID.Text = dt.Rows(0)("NewSiteID").ToString()
            lblLAC.Text = dt.Rows(0)("LAC").ToString()
            txtLAC.Text = dt.Rows(0)("LAC").ToString()
            lblCI.Text = dt.Rows(0)("CI").ToString()
            txtCI.Text = dt.Rows(0)("CI").ToString()
            lblOnAirCon.Text = dt.Rows(0)("OnAirCon").ToString()
            txtOnAirCon.Text = dt.Rows(0)("OnAirCon").ToString()
            lblLUpdate.Text = dt.Rows(0)("ludate1").ToString()
            txtLUpdate.Value = dt.Rows(0)("ludate1").ToString()
            lblVer.Text = dt.Rows(0)("luversion").ToString()
            txtVer.Text = dt.Rows(0)("luversion").ToString()
            If (dt.Rows(0)("ChkDrive").ToString().ToLower() = "true") Then
                ChkDrive.Checked = True
            End If
            If (dt.Rows(0)("ChkKPI").ToString().ToLower() = "true") Then
                ChkKPI.Checked = True
            End If
            If (dt.Rows(0)("ChkAlaram").ToString().ToLower() = "true") Then
                ChkFAlarm.Checked = True
            End If
        End If
    End Sub
    Sub Binddata()
        str = "Exec uspTIQCBautOnLine " & Request.QueryString("id")
        dt = objUtil.ExeQueryDT(str, "SiteDoc1")
        If dt.Rows.Count > 0 Then
            lblPO.Text = dt.Rows(0).Item("pono").ToString
            lblSiteID.Text = dt.Rows(0).Item("site_no").ToString
            lblSiteName.Text = dt.Rows(0).Item("site_name").ToString
            lblBand.Text = dt.Rows(0).Item("band").ToString
            lblExtConfig.Text = dt.Rows(0).Item("existconfig").ToString
            lblIntDate.Text = dt.Rows(0).Item("siteintegration").ToString
            lblOnAirDate.Text = dt.Rows(0).Item("siteacponair").ToString
            lblAcpDate.Text = dt.Rows(0).Item("siteacponbast").ToString
            If lblIntDate.Text = "" Then lblIntDate.Text = ""
            If lblOnAirDate.Text = "" Then lblOnAirDate.Text = ""
            If lblAcpDate.Text = "" Then lblAcpDate.Text = ""
            hdnSiteno.Value = dt.Rows(0).Item("site_no").ToString
            hdnsiteid.Value = dt.Rows(0).Item("SiteId").ToString
            hdnversion.Value = dt.Rows(0).Item("version").ToString
            hdnWfId.Value = dt.Rows(0).Item("WF_Id").ToString
            hdndocid.Value = dt.Rows(0).Item("docid").ToString
            hdnScope.Value = dt.Rows(0).Item("Scope").ToString
            hdnDGBox.Value = dt.Rows(0).Item("DGBox").ToString
            ' i = objBO.uspCheckIntegration(hdndocid.Value, hdnSiteno.Value)
            i = objdb.ExeQueryScalar("exec uspCheckIntegration  '" & hdndocid.Value & "' ,'" & hdnSiteno.Value & "'," & hdnversion.Value & "")
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
            str = "Exec [uspGetOnLineFormBind] " & DT.Rows(0).Item("WF_Id").ToString & "," & DT.Rows(0).Item("SiteId").ToString
            DT = objutil.ExeQueryDT(str, "SiteDoc1")
            DLDigitalSign.DataSource = DT
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
        sw.WriteLine(".lblText{font-family: verdana;font-size: 8pt;color: #000000;text-align: left;}")
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
            .AT.LMBY = Session("User_Name")
            .orgDocPath = DocPath 'hdnSiteno.Value & ft & secpath & ReFileName
            .PONo = lblPO.Text
        End With
        objBO.updatedocupload(objET1)
        Dim strsql As String = "Update bautmaster set Pstatus=1 where Site_ID = " & hdnsiteid.Value & " and SiteVersion =" & hdnversion.Value & " and PONO = '" & lblPO.Text & "'"
        objUtil.ExeUpdate(strsql)
        'sendmail2()
        chek4alldoc() ' for messaage to previous screen ' and saving final docupload date in reporttable
        'for BAUT
        'objBo.check4BAUTBAST(hdnsiteid.Value, hdnversion.Value, 1, lblPONO.Text, Session("User_Name"))
        objdb.ExeQueryScalar("exec uspcheck4BAUTBAST " & hdnsiteid.Value & " ," & hdnversion.Value & "," & 1 & ",'" & lblPO.Text & "','" & Session("User_Name") & "'," & ConfigurationManager.AppSettings("BAUTID") & "," & ConfigurationManager.AppSettings("BASTID") & " ")
        'for BAST1
        'objBo.check4BAUTBAST(hdnsiteid.Value, hdnversion.Value, 2, lblPONO.Text, Session("User_Name"))
        objdb.ExeQueryScalar("exec uspcheck4BAUTBAST " & hdnsiteid.Value & " ," & hdnversion.Value & "," & 2 & ",'" & lblPO.Text & "','" & Session("User_Name") & "'," & ConfigurationManager.AppSettings("BAUTID") & "," & ConfigurationManager.AppSettings("BASTID") & " ")
        'Fill Transaction table
        AuditTrail()
        If hdnready4baut.Value = 1 Then
            Response.Redirect("../PO/frmSiteDocUploadTree.aspx?ready=yes")
        Else
            Response.Redirect("../PO/frmSiteDocUploadTree.aspx")
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
        " where tt.TSK_Id<> 1 and wfid=" & hdnWfId.Value & "   order by wfdid"
        DtNewOne = objUtil.ExeQueryDT(strSql1, "dd")
        dvNotIn = DtNewOne.DefaultView
        dvNotIn.RowFilter = "TSK_Id <>1"
        status = 1
        Dim bb As Boolean, intCount As Integer = 0, IncrMentY As Integer = 0
        For IncrMentX As Integer = 0 To dvNotIn.Count - 1
            Dim iHDX As HiddenField = CType(DLDigitalSign.Items(IncrMentX).FindControl("hdXCoordinate"), HiddenField)
            Dim iHDY As HiddenField = CType(DLDigitalSign.Items(IncrMentX).FindControl("hdYCoordinate"), HiddenField)
            fillDetails()
            objdo.Status = status
            objdo.UserType = dvNotIn(IncrMentX).Item(3).ToString
            objdo.UsrRole = dvNotIn(IncrMentX).Item(4).ToString
            objdo.WFId = dvNotIn(IncrMentX).Item(1).ToString
            objdo.TSK_Id = dvNotIn(IncrMentX).Item(5).ToString
            objdo.UGP_Id = dvNotIn(IncrMentX).Item("grpId").ToString
            If dvNotIn.Count = 2 Then
                If IncrMentX = 0 Then
                    objdo.XVal = (iHDX.Value / 2) - 13
                Else
                    objdo.XVal = (iHDX.Value / 2) + (intCount * 27)
                End If
            Else
                objdo.XVal = (iHDX.Value / 2) + (intCount * 27)
            End If
            objdo.YVal = 110 + (791 - iHDY.Value) + (IncrMentY * 52)
            Y = 20 + (Math.Ceiling(iHDY.Value / 791))
            If (IncrMentX > 0) Then
                If (IncrMentX Mod 2) = 0 Then
                    intCount = 0
                    IncrMentY = IncrMentY + 1
                Else
                    intCount = intCount + 1
                End If
            Else
                intCount = intCount + 1
            End If

            If Y = 0 Then Y = 1
            Y = 1
            objdo.PageNo = Y
            objdo.Site_Id = hdnsiteid.Value
            objdo.Status = status
            objBO.uspwftransactionIU(objdo)
            If bb = False Then
                objUtil.ExeNonQuery("exec uspErrLog '', '" & hdnsiteid.Value & "','" & dvNotIn(IncrMentX).Item(3).ToString & "' ,'sendmaQC'")
                'sendmailTrans(hdnsiteid.Value, dvNotIn(IncrMentX).Item(3).ToString, dvNotIn(IncrMentX).Item(4).ToString)
                Try
                    objmail.sendmailTrans(0, hdnsiteid.Value, dvNotIn(IncrMentX).Item(3).ToString, dvNotIn(IncrMentX).Item(4).ToString, ConfigurationManager.AppSettings("uploadmailconst"))
                Catch ex As Exception
                    objdb.ExeNonQuery("exec uspErrLog '', 'mailsending','" & ex.Message.ToString.Replace("'", "''") & "','sendmailbaut'")
                End Try
                bb = True
            End If
        Next
    End Sub
    Sub fillDetails()
        objdo.DocId = hdndocid.Value
        objdo.Site_Id = hdnsiteid.Value
        objdo.SiteVersion = hdnversion.Value
        objdo.AT.RStatus = Constants.STATUS_ACTIVE
        objdo.AT.LMBY = Session("User_Name")
    End Sub
    Sub DOThis(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rad As RadioButtonList = CType(sender, RadioButtonList)
        Dim grdr As GridViewRow = CType(rad.NamingContainer, GridViewRow)
        Dim x As Integer = grdr.RowIndex
        Dim rdl As RadioButtonList = grddocuments.Rows(x).Cells(5).FindControl("rdbstatus")
        Dim txt1 As TextBox = grddocuments.Rows(x).Cells(6).FindControl("txtremarks")
        txt1.Text = "Please enter remarks here......"
        If rdl.SelectedValue = 0 Then  'approve
            txt1.Visible = False
        Else 'reject
            txt1.Visible = True
        End If
    End Sub
    Protected Sub grddocuments_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grddocuments.RowDataBound
        Select Case e.Row.RowType
            Case DataControlRowType.DataRow
                Dim lbl As Label = CType(e.Row.FindControl("lblNo"), Label)
                lbl.Text = (grddocuments.PageIndex * grddocuments.PageSize) + (e.Row.RowIndex + 1) & ".&nbsp;"
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
        If Request.Browser.Browser = "IE" Then
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
                    btnGenerate.Visible = False
                    grddocuments.Visible = False
                    Dim strLUpdate As String
                    If (txtLUpdate.Value.ToString() = "") Then
                        strLUpdate = "NULL"
                    Else
                        strLUpdate = txtLUpdate.Value.ToString()
                    End If
                    If (hdSno.Value = 0) Then
                        str = "Exec uspODQCIU  0," & Request.QueryString("id") & ",'" & lblPO.Text & "','" & lblSiteID.Text & "'," & hdnversion.Value & ",'" & txtTWork.Text & "','" & txtNEType.Text & "','" & txtBSCName.Text & "','" & txtNSiteID.Text & "','" & txtLAC.Text & "','" & txtCI.Text & "','" & txtOnAirCon.Text & "'," & ChkDrive.Checked & "," & ChkKPI.Checked & "," & ChkFAlarm.Checked & "," & IIf(strLUpdate = "", "null", "'" & strLUpdate & "'") & ",'" & objCommon.UserName() & "','" & lblVer.Text & "'"
                    Else
                        str = "Exec uspODQCIU  " & hdSno.Value & "," & Request.QueryString("id") & ",'" & lblPO.Text & "','" & lblSiteID.Text & "'," & hdnversion.Value & ",'" & txtTWork.Text & "','" & txtNEType.Text & "','" & txtBSCName.Text & "','" & txtNSiteID.Text & "','" & txtLAC.Text & "','" & txtCI.Text & "','" & txtOnAirCon.Text & "'," & ChkDrive.Checked & "," & ChkKPI.Checked & "," & ChkFAlarm.Checked & "," & IIf(strLUpdate = "", "null", "'" & strLUpdate & "'") & ",'" & objCommon.UserName() & "','" & lblVer.Text & "'"
                    End If
                    objUtil.ExeQueryScalar(str)
                    uploaddocument(Convert.ToInt32(hdnversion.Value), Convert.ToInt32(hdnKeyVal.Value))
                    Response.Redirect("../PO/frmSiteDocUploadTree.aspx")
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
                btnGenerate.Visible = False
                grddocuments.Visible = False
                Dim strLUpdate As String
                If (txtLUpdate.Value.ToString() = "") Then
                    strLUpdate = "NULL"
                Else
                    strLUpdate = txtLUpdate.Value.ToString()
                End If
                If (hdSno.Value = 0) Then
                    str = "Exec uspODQCIU  0," & Request.QueryString("id") & ",'" & lblPO.Text & "','" & lblSiteID.Text & "'," & hdnversion.Value & ",'" & txtTWork.Text & "','" & txtNEType.Text & "','" & txtBSCName.Text & "','" & txtNSiteID.Text & "','" & txtLAC.Text & "','" & txtCI.Text & "','" & txtOnAirCon.Text & "'," & ChkDrive.Checked & "," & ChkKPI.Checked & "," & ChkFAlarm.Checked & "," & IIf(strLUpdate = "", "null", "'" & strLUpdate & "'") & ",'" & objCommon.UserName() & "','" & lblVer.Text & "'"
                Else
                    str = "Exec uspODQCIU  " & hdSno.Value & "," & Request.QueryString("id") & ",'" & lblPO.Text & "','" & lblSiteID.Text & "'," & hdnversion.Value & ",'" & txtTWork.Text & "','" & txtNEType.Text & "','" & txtBSCName.Text & "','" & txtNSiteID.Text & "','" & txtLAC.Text & "','" & txtCI.Text & "','" & txtOnAirCon.Text & "'," & ChkDrive.Checked & "," & ChkKPI.Checked & "," & ChkFAlarm.Checked & "," & IIf(strLUpdate = "", "null", "'" & strLUpdate & "'") & ",'" & objCommon.UserName() & "','" & lblVer.Text & "'"
                End If
                objUtil.ExeQueryScalar(str)
                uploaddocument(Convert.ToInt32(hdnversion.Value), Convert.ToInt32(hdnKeyVal.Value))
                Response.Redirect("../PO/frmSiteDocUploadTree.aspx")
            End If
        Else
            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('Please use Internet Explorer');", True)
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
End Class