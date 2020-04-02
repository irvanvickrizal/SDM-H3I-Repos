Imports System.Data
Imports System.IO
Imports Common
Imports Entities
Imports BusinessLogic
Partial Class BAUT_frmTI_BAUTNEW
    Inherits System.Web.UI.Page
    Dim dt As New DataTable
    Dim dt2 As New DataTable
    Dim objUtil As New DBUtil
    Dim objdb As New DBUtil
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
    Dim url As String
    Dim objmail As New TakeMail
    Dim oddt As New DataTable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        imgPODate.Attributes.Add("onclick", "javascript:popUpCalendar(this,txtPODated,'dd-mmm-yyyy');return false;")
        imgATPDate.Attributes.Add("onclick", "javascript:popUpCalendar(this,txtATPDate,'dd-mmm-yyyy');return false;")
        imgDate.Attributes.Add("onclick", "javascript:popUpCalendar(this,txtDate,'dd-mmm-yyyy');return false;")
        imgOADated.Attributes.Add("onclick", "javascript:popUpCalendar(this,txtOADated,'dd-mmm-yyyy');return false;")
        imgd4.Attributes.Add("onclick", "javascript:popUpCalendar(this,txtd4,'dd-mmm-yyyy');return false;")
        imgkpidate.Attributes.Add("onclick", "javascript:popUpCalendar(this,txtkpidate,'dd-mmm-yyyy');return false;")
        imgWrkStarted.Attributes.Add("onclick", "javascript:popUpCalendar(this,txtWorkStarted,'dd-mmm-yyyy');return false;")
        imgWorkShouldFinished.Attributes.Add("onclick", "javascript:popUpCalendar(this,txtWorkShouldFinished,'dd-mmm-yyyy');return false;")
        imgWorkhasbeenFinished.Attributes.Add("onclick", "javascript:popUpCalendar(this,txtWorkHasBeenFinished,'dd-mmm-yyyy');return false;")
        imgWorkShouldFinishedbaut.Attributes.Add("onclick", "javascript:popUpCalendar(this,txtWorkShouldFinishedbaut,'dd-mmm-yyyy');return false;")
        imgKPIAccepted.Attributes.Add("onclick", "javascript:popUpCalendar(this,txtKPIAccepted,'dd-mmm-yyyy');return false;")
        If Not IsPostBack Then
            If Not Request.QueryString("id") Is Nothing Then
                'generate baut refno
                txtBautNo.Value = objdb.ExeQueryScalarString("select dbo.GenerateBAUTRefNo(" & Request.QueryString("id") & ",'BAUT')")
                Binddata()
                filloddata()
                FillData()
                Dim strsql As String = "Exec uspSiteBAUTDocListOnlineForm " & Request.QueryString("id")
                dt = objUtil.ExeQueryDT(strsql, "sisbaut")
                grddocuments.DataSource = dt
                grddocuments.DataBind()
                grddocuments.Columns(1).Visible = False
                grddocuments.Columns(2).Visible = False
                grddocuments.Columns(4).Visible = False
            End If
        End If
        ScriptManager.RegisterStartupScript(Page, GetType(updatePanel), "MyScriptdd", "getControlPosition();", True)
    End Sub
    Sub FillData()
        Dim str1 As String = "EXEC uspGetODWCTRBAUT " & Request.QueryString("id")
        dt2 = objUtil.ExeQueryDT(str1, "ODWCTRBAST")
        If dt2.Rows.Count > 0 Then
            'txtDay.Value = dt2.Rows(0).Item("wday").ToString
            'txtDateWCTR.Value = dt2.Rows(0).Item("wDate").ToString
            'txtMonth.Value = dt2.Rows(0).Item("wmonth").ToString
            'txtYear.Value = dt2.Rows(0).Item("wyear").ToString
            txtDay.Value = objUtil.ExeQueryScalarString("select dbo.GetWeekDayNameOfDate(GETDATE()) as WeekDayName")
            txtDateWCTR.Value = objUtil.ExeQueryScalar("select day(getdate())").ToString()
            txtMonth.Value = objUtil.ExeQueryScalar("select month(getdate())").ToString()
            txtYear.Value = objUtil.ExeQueryScalar("select year(getdate())").ToString()
            txtWorkShouldFinishedbaut.Value = dt2.Rows(0).Item("wsfinishbaut").ToString
            txtKPIAccepted.Value = dt2.Rows(0).Item("wskpiapproval").ToString
            txtReasonBaut1.Value = dt2.Rows(0).Item("DotherR1baut").ToString
            txtReasonDaysBaut1.Value = dt2.Rows(0).Item("DotherR1daysbaut").ToString
            txtReasonBaut2.Value = dt2.Rows(0).Item("DotherR2baut").ToString
            txtReasonDaysBaut2.Value = dt2.Rows(0).Item("DotherR2daysbaut").ToString
            txtReasonBaut3.Value = dt2.Rows(0).Item("DotherR3baut").ToString
            txtReasonDaysBaut3.Value = dt2.Rows(0).Item("DotherR3daysbaut").ToString
            txtTotalC.Value = dt2.Rows(0).Item("Totalc").ToString
            txtsiteidpo.Text = dt2.Rows(0).Item("siteidpo").ToString
            txtsitenamepo.Text = dt2.Rows(0).Item("sitenamepo").ToString
        Else
            txtDay.Value = objUtil.ExeQueryScalarString("select  datename(dw,getdate())")
            txtDateWCTR.Value = DateTime.Now.Day
            txtMonth.Value = DateTime.Now.Month
            txtYear.Value = DateTime.Now.Year
        End If
    End Sub
    Sub filloddata()
        Dim strsql As String = "Exec odbautDetail " & Request.QueryString("id")
        oddt = objUtil.ExeQueryDT(strsql, "odbaut")
        If oddt.Rows.Count > 0 Then
            txtDate.Value = IIf(oddt.Rows(0).Item("BautDate").ToString.IndexOf("1900") = -1, oddt.Rows(0).Item("BautDate").ToString, "")
            txtBautNo.Value = oddt.Rows(0).Item("Bautrefno").ToString
            txtATPDated.Value = oddt.Rows(0).Item("ATPdate").ToString
            txtATPDate.Value = IIf(oddt.Rows(0).Item("ATPdated").ToString.IndexOf("1900") = -1, oddt.Rows(0).Item("ATPdated").ToString, "")
            txtOADate.Value = oddt.Rows(0).Item("OnAirdate").ToString
            txtOADated.Value = IIf(oddt.Rows(0).Item("OnAirdated").ToString.IndexOf("1900") = -1, oddt.Rows(0).Item("OnAirdated").ToString, "")
            txtkpidate.Value = IIf(oddt.Rows(0).Item("kpidate").ToString.IndexOf("1900") = -1, oddt.Rows(0).Item("kpidate").ToString, "")
            txtd4.Value = IIf(oddt.Rows(0).Item("Item4").ToString.IndexOf("1900") = -1, oddt.Rows(0).Item("Item4").ToString, "")
            txtd1.Value = oddt.Rows(0).Item("Item1").ToString
            txtd2.Value = oddt.Rows(0).Item("Item2").ToString
            txtd3.Value = oddt.Rows(0).Item("Item3").ToString
            txtConfig.Value = oddt.Rows(0).Item("Item3").ToString
            txtPODated.Value = IIf(oddt.Rows(0).Item("CustPORecDt").ToString.IndexOf("1900") = -1, oddt.Rows(0).Item("CustPORecDt").ToString, "")
        End If
    End Sub
    Sub Binddata()
        str = "Exec uspTIBautOnLine " & Request.QueryString("id")
        dt = objUtil.ExeQueryDT(str, "SiteDoc1")
        If dt.Rows.Count > 0 Then
            If Not String.IsNullOrEmpty(dt.Rows(0).Item("projectid")) Then
                lblSite.Text = dt.Rows(0).Item("site_no").ToString & "/" & dt.Rows(0).Item("site_name").ToString
                'txtPO.Text = dt.Rows(0).Item("pono").ToString
                'txtPONo.Text = dt.Rows(0).Item("pono").ToString
                txtPO.Text = IIf(String.IsNullOrEmpty(dt.Rows(0).Item("HOTAsPerPO").ToString), dt.Rows(0).Item("pono").ToString, dt.Rows(0).Item("HOTAsPerPO").ToString)
                txtPONo.Text = IIf(String.IsNullOrEmpty(dt.Rows(0).Item("HOTAsPerPO").ToString), dt.Rows(0).Item("pono").ToString, dt.Rows(0).Item("HOTAsPerPO").ToString)

                txtATPDate.Value = dt.Rows(0).Item("atp").ToString
                lblSiteWCTR.Text = dt.Rows(0).Item("site_no").ToString & "/" & dt.Rows(0).Item("site_name").ToString
                'txtPoWCTR.Text = dt.Rows(0).Item("pono").ToString
                txtPoWCTR.Text = IIf(String.IsNullOrEmpty(dt.Rows(0).Item("HOTAsPerPO").ToString), dt.Rows(0).Item("pono").ToString, dt.Rows(0).Item("HOTAsPerPO").ToString)

                lblScope.Text = dt.Rows(0).Item("Scope").ToString
                lblScope2.Text = dt.Rows(0).Item("Scope").ToString
                hdnSiteno.Value = dt.Rows(0).Item("site_no").ToString
                hdnsiteid.Value = dt.Rows(0).Item("SiteId").ToString
                hdnversion.Value = dt.Rows(0).Item("version").ToString
                hdnWfId.Value = dt.Rows(0).Item("WF_Id").ToString
                hdndocid.Value = dt.Rows(0).Item("docid").ToString
                hdnScope.Value = dt.Rows(0).Item("Scope").ToString
                hdnDGBox.Value = dt.Rows(0).Item("DGBox").ToString
                hdnPono.Value = dt.Rows(0).Item("pono").ToString
                lblProjectidwctr.Text = dt.Rows(0).Item("projectid").ToString
                lblProjectId.Text = dt.Rows(0).Item("projectid").ToString
                txtsiteidpo.Text = dt.Rows(0).Item("siteidpo").ToString
                txtsitenamepo.Text = dt.Rows(0).Item("sitenamepo").ToString
                lblBsiteidpo.Text = dt.Rows(0).Item("siteidpo").ToString
                lblBsitenamepo.Text = dt.Rows(0).Item("sitenamepo").ToString
                If (hdnWfId.Value = ConfigurationManager.AppSettings("WFBAUTBID")) Then
                    LblHeaderBAUT.Text = "BERITA ACARA UJI TERIMA BARANG (BAUTB)"
                    LblBautRefNumber.Text = "BAUTB Ref Number"
                End If
                If (dt.Rows(0).Item("CustomerPOReceiveDate") IsNot Nothing) Then
                    'txtWorkStarted.Attributes.Add("readonly", "readonly")
                    'txtPODated.Attributes.Add("readonly", "readonly")
                    txtWorkStarted.Value = dt.Rows(0).Item("CustomerPOReceiveDate").ToString
                    txtPODated.Value = dt.Rows(0).Item("CustomerPOReceiveDate").ToString
                    imgWrkStarted.Visible = True
                    imgPODate.Visible = True
                End If

                '-- New Code Irvan Vickrizal 2312012
                hdnMasterScope.Value = Convert.ToString(dt.Rows(0).Item("MasterScope"))
                hdnTypeofWork.Value = Convert.ToString(dt.Rows(0).Item("typeofWork"))

                DoCheckingMasterScope(hdnMasterScope.Value, hdnTypeofWork.Value)

                i = objUtil.ExeQueryScalar("exec uspCheckIntegration  '" & hdndocid.Value & "' ,'" & hdnSiteno.Value & "'," & hdnversion.Value & "")
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

                Dim podate As System.Nullable(Of DateTime) = New POExtendedController().GetPODate(hdnPono.Value)
                If podate.HasValue = False Then
                    hdnKeyVal.Value = 0
                    ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "Showmain('napodate');", True)
                Else
                    imgPODate.Visible = False
                    txtPODated.Visible = False
                    txtPODated.Value = String.Format("{0:dd-MMMM-yyyy}", podate.Value)
                    lblPODate.Text = String.Format("{0:dd-MMMM-yyyy}", podate.Value)
                End If

                str = "Exec [uspGetOnLineFormBind] " & dt.Rows(0).Item("WF_Id").ToString & "," & dt.Rows(0).Item("SiteId").ToString
                dt = objUtil.ExeQueryDT(str, "SiteDoc1")
                'DLDigitalSign.DataSource = dt
                'DLDigitalSign.DataBind()
                '-------------New Code 05 Januari 2012 --------
                Dim dtSignPosition As DataView = dt.DefaultView
                dtSignPosition.Sort = "tsk_id desc"
                DLDigitalSign.DataSource = dtSignPosition
                DLDigitalSign.DataBind()
                'dlWCTR.DataSource = dt
                dlWCTR.DataSource = dtSignPosition
                dlWCTR.DataBind()
                '----------------------------------------------

                Dim dtv As DataView = dt.DefaultView
                dtv.Sort = "tsk_id desc"
                dtList.DataSource = dtv
                dtList.DataBind()
                HDDgSignTotal.Value = dt.Rows.Count
            Else
                hdnKeyVal.Value = 0
                ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "Showmain('naprojectid');", True)
            End If
            
        Else
            hdnsiteid.Value = 0
            hdnversion.Value = 0
            hdnWfId.Value = 0
            hdndocid.Value = 0
        End If
        ScriptManager.RegisterStartupScript(Page, GetType(updatePanel), "MyScriptdd", "getControlPosition();", True)
    End Sub
    Sub DoCheckingMasterScope(ByVal masterscope As String, ByVal typeofwork As String)
        If String.IsNullOrEmpty(masterscope) Then
            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScripta", "Showmain('nscope');", True)
        End If
        If String.IsNullOrEmpty(typeofwork) Then
            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScripta", "Showmain('nwork');", True)
        End If
    End Sub

    Sub Dochecking()
        roleid = Session("Role_Id")
        grp = Session("User_Type")
        If objBO.uspApprRequired(hdnsiteid.Value, hdndocid.Value, hdnversion.Value) <> 0 Then
            If objBO.verifypermission(hdndocid.Value, roleid, grp) <> 0 Then
                Select Case objBO.DocUploadverify(hdnsiteid.Value, hdndocid.Value, hdnversion.Value).ToString
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
                        ScriptManager.RegisterStartupScript(Page, GetType(updatePanel), "MyScripta", "Showmain('2sta');", True)
                        Exit Sub
                End Select
            Else
                If Session("Role_Id") = 1 Then ''if role is sysadmin then no need to verify permission everthing permitted.
                    Select Case objBO.DocUploadverify(hdnsiteid.Value, hdndocid.Value, hdnversion.Value).ToString
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
                            ScriptManager.RegisterStartupScript(Page, GetType(updatePanel), "MyScriptb", "Showmain('2sta');", True)
                            Exit Sub
                    End Select
                Else
                    hdnKeyVal.Value = 0
                    ScriptManager.RegisterStartupScript(Page, GetType(updatePanel), "MyScriptc", "Showmain('nop');", True)
                End If
            End If
        Else 'Seeta 20081230 Appr Not Required
            Select Case objBO.DocUploadverify(hdnsiteid.Value, hdndocid.Value, hdnversion.Value).ToString
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
                    ScriptManager.RegisterStartupScript(Page, GetType(updatePanel), "MyScriptd", "Showmain('2sta');", True)
                    Exit Sub
            End Select
        End If
    End Sub

    Function CreatePDFFile(ByVal StrPath As String) As String
        StrPath = StrPath.Replace(" \", "\").ToString 'bugfix100816 causing error when the path containing space before \
        Dim dtba As New DataTable
        Dim filenameorg As String
        Dim ReFileName As String
        filenameorg = hdnSiteno.Value & "-BAUT-" & Format(CDate(DateTime.Now), "ddMMyyyyHHss")
        ReFileName = filenameorg & ".htm"
        StrPath = StrPath.Replace(" \", "\").ToString 'bugfix100816 causing error when the path containing space before \
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
        DivPrintWCTR.RenderControl(sw)
        sw.WriteLine("</td></tr><tr><td class=""PageBreak"">")
        sw.WriteLine("</td></tr><tr><td>")
        dvPrint.RenderControl(sw)
        sw.WriteLine("</td></tr></table>")
        sw.WriteLine("</body>")
        sw.WriteLine("</html>")
        sw.Close()
        sw.Dispose()
        Return Common.TakeFileUpload.ConvertAnyFormatToPDFHtmlNew(StrPath & ReFileName, StrPath, filenameorg)
    End Function

    Sub uploaddocument(ByVal vers As Integer, ByVal keyval As Integer)
        dt = objBO.getbautdocdetailsNEW(hdndocid.Value)
        Dim sec As String = dt.Rows(0).Item("sec_name").ToString
        Dim subsec As String = dt.Rows(0).Item("subsec_name").ToString
        Dim secpath As String = ""
        Dim ft As String = ""
        Dim path As String = ""
        Dim filenameorg As String
        FileNameOnly = "-" & Constants._Doc_BAUT & "-"
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
        With objET1
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
        objBO.updatedocupload(objET1)
        'bugfix101109
        objdb.ExeNonQuery("exec bautcheckinsert '" & objET1.PONo & "'," & objET1.SiteID & ", " & objET1.Version & ", '" & Session("User_Id") & "'," & ConfigurationManager.AppSettings("BAUTID"))
        Dim strsql As String = "update bautmaster set Pstatus=1, ReferenceNO='" & lblBautNo.Text & "'where Site_ID = " & hdnsiteid.Value & " and SiteVersion =" & hdnversion.Value & " and PONO = '" & hdnPono.Value & "'"
        objUtil.ExeQuery(strsql)
        chek4alldoc() 'for messaage to previous screen and saving final docupload date in reporttable
        'for BAUT
        'objBo.check4BAUTBAST(hdnsiteid.Value, hdnversion.Value, 1, lblPONO.Text, Session("User_Name"))
        objdb.ExeQueryScalar("exec uspcheck4BAUTBAST " & hdnsiteid.Value & " ," & hdnversion.Value & "," & 1 & ",'" & hdnPono.Value & "','" & Session("User_Name") & "'," & ConfigurationManager.AppSettings("BAUTID") & "," & ConfigurationManager.AppSettings("BASTID") & " ")
        'for BAST1
        'objBo.check4BAUTBAST(hdnsiteid.Value, hdnversion.Value, 2, lblPONO.Text, Session("User_Name"))
        objdb.ExeQueryScalar("exec uspcheck4BAUTBAST " & hdnsiteid.Value & " ," & hdnversion.Value & "," & 2 & ",'" & hdnPono.Value & "','" & Session("User_Name") & "'," & ConfigurationManager.AppSettings("BAUTID") & "," & ConfigurationManager.AppSettings("BASTID") & " ")
        'Fill Transaction table
        AuditTrail()
        If Request.QueryString("pono") IsNot Nothing Then
            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScripts", "WindowsClose();", True)
        Else
            If hdnready4baut.Value = 1 Then
                Response.Redirect("../PO/frmSiteDocUploadTree.aspx?ready=yes")
            Else
                Response.Redirect("../PO/frmSiteDocUploadTree.aspx")
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
                        objdo.Status = status
                        objdo.DocId = docId
                        objdo.UserType = dtNew.Rows(aa).Item(3).ToString
                        objdo.UsrRole = dtNew.Rows(aa).Item(4).ToString
                        objdo.WFId = dtNew.Rows(aa).Item(1).ToString
                        objdo.UGP_Id = dtNew.Rows(aa).Item("grpId").ToString
                        objdo.TSK_Id = dtNew.Rows(aa).Item(5).ToString
                        objdo.XVal = dtNew.Rows(aa).Item("X_Coordinate").ToString
                        objdo.YVal = dtNew.Rows(aa).Item("Y_Coordinate").ToString
                        objdo.PageNo = dtNew.Rows(aa).Item("PageNo").ToString
                        objBO.uspwftransactionIU(objdo)
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
        " where tt.TSK_Id=1 and wfid=" & hdnWfId.Value & "   order by wfdid"
        dtNew = objUtil.ExeQueryDT(strSql1, "dd")
        If dtNew.Rows.Count > 0 Then
            objBO.DelWFTransaction(hdndocid.Value, dtNew.Rows(0).Item(1).ToString, hdnsiteid.Value, hdnversion.Value)
        End If
        Dim status As Integer = 0
        Dim DtNewOne As DataTable
        Dim dvIn As New DataView
        Dim dvNotIn As New DataView
        Dim dvNotInNew As New DataView
        Dim J As Integer = 1
        Dim Y As Integer = 0, Ybaut As Integer = 0, Jbaut As Integer = 0, TopK As Integer = 0
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
        strSql1 = "select distinct wfdid,wfid,sorder,grpcode,roleid,a.tsk_id,a.grpId,tt.tskname,0 as   X_Coordinate,0 as  Y_Coordinate," & _
        "0 as  PageNo,IsNull(a.Sorder,0)Sno from twfdefinition A inner join tusertype B on a.grpid=b.grpid inner join ttask as tt on tt.tsk_id=a.tsk_id " & _
        " where tt.TSK_Id <> 1  and wfid=" & hdnWfId.Value & "   order by wfdid"
        DtNewOne = objUtil.ExeQueryDT(strSql1, "dd")
        dvNotIn = DtNewOne.DefaultView
        dvNotIn.RowFilter = "TSK_Id <>1"
        dvNotInNew.RowFilter = "TSK_Id <>5"

        status = 1
        Dim bb As Boolean, intCount As Integer = 0, IncrMentY As Integer = 0
        Dim kkb As Boolean
        Dim kki As Integer = 0
        For IncrMentX As Integer = 0 To dvNotIn.Count - 1
            If dvNotIn(IncrMentX).Item(5) = 5 Then
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
                kkb = True
                kki = IncrMentX - 1
            Else
                Dim iHDX As HiddenField
                Dim iHDY As HiddenField
                Dim iHDXBaut As HiddenField
                Dim iHDYBaut As HiddenField
                If kkb = True Then
                    iHDX = CType(dlWCTR.Items(IncrMentX - 1).FindControl("hdXCoordinate"), HiddenField)
                    iHDY = CType(dlWCTR.Items(IncrMentX - 1).FindControl("hdYCoordinate"), HiddenField)
                    iHDXBaut = CType(DLDigitalSign.Items(IncrMentX - 1).FindControl("hdXCoordinate"), HiddenField)
                    iHDYBaut = CType(DLDigitalSign.Items(IncrMentX - 1).FindControl("hdYCoordinate"), HiddenField)
                    kkb = False
                Else
                    iHDX = CType(dlWCTR.Items(IncrMentX).FindControl("hdXCoordinate"), HiddenField)
                    iHDY = CType(dlWCTR.Items(IncrMentX).FindControl("hdYCoordinate"), HiddenField)
                    iHDXBaut = CType(DLDigitalSign.Items(IncrMentX).FindControl("hdXCoordinate"), HiddenField)
                    iHDYBaut = CType(DLDigitalSign.Items(IncrMentX).FindControl("hdYCoordinate"), HiddenField)
                End If
                fillDetails()
                objdo.Status = status
                objdo.UserType = dvNotIn(IncrMentX).Item(3).ToString
                objdo.UsrRole = dvNotIn(IncrMentX).Item(4).ToString
                objdo.WFId = dvNotIn(IncrMentX).Item(1).ToString
                objdo.TSK_Id = dvNotIn(IncrMentX).Item(5).ToString
                objdo.UGP_Id = dvNotIn(IncrMentX).Item("grpId").ToString
                Dim strversion As String = Request.Browser.Type
                'bugfix101004: override y value to disable the auto calculation
                If InStr(strversion, "IE6") > 0 Then
                    ' update sign position 09012012 -- irvan v
                    If IncrMentX > 0 Then
                        objdo.XVal = 28
                        objdo.YVal = 150
                    Else
                        objdo.XVal = 314
                        objdo.YVal = 150
                    End If
                ElseIf InStr(strversion, "IE7") > 0 Then
                    ' update sign position 09012012 -- irvan v
                    If IncrMentX > 0 Then
                        objdo.XVal = 28
                        objdo.YVal = 150
                    Else
                        objdo.XVal = 314
                        objdo.YVal = 150
                    End If
                ElseIf InStr(strversion, "IE8") > 0 Then
                    ' update sign position 09012012 -- irvan v
                    If IncrMentX > 0 Then
                        objdo.XVal = 28
                        objdo.YVal = 150
                    Else
                        objdo.XVal = 314
                        objdo.YVal = 150
                    End If
                ElseIf InStr(strversion, "Fire") > 0 Then
                    ' update sign position 09012012 -- irvan v
                    If IncrMentX > 0 Then
                        objdo.XVal = 28
                        objdo.YVal = 150
                    Else
                        objdo.XVal = 314
                        objdo.YVal = 150
                    End If
                Else
                    ' update sign position 09012012 -- irvan v incrMentX re-changeposition 
                    If IncrMentX > 0 Then
                        objdo.XVal = 28
                        objdo.YVal = 150
                    Else
                        objdo.XVal = 314
                        objdo.YVal = 150
                    End If
                End If
                '--
                Y = (Math.Ceiling(iHDY.Value / 791))
                If Y = 0 Then Y = 1
                Y = 1
                objdo.PageNo = Y
                objdo.Site_Id = hdnsiteid.Value
                objdo.Status = status
                objBO.uspwftransactionIU(objdo)
                Dim XBauts As Integer = 1
                Dim YBauts As Integer = 0
                'bugfix101004: override y value to disable the auto calculation
                If InStr(strversion, "IE6") > 0 Then
                    ' update sign position 09012012 -- irvan v incrmentX re-change position
                    If IncrMentX > 0 Then
                        objdo.XVal = 39
                        objdo.YVal = 150
                    Else
                        objdo.XVal = 320
                        objdo.YVal = 150
                    End If
                ElseIf InStr(strversion, "IE7") > 0 Then
                    ' update sign position 09012012 -- irvan v incrmentX re-change position
                    If IncrMentX > 0 Then
                        objdo.XVal = 39
                        objdo.YVal = 150
                    Else
                        objdo.XVal = 320
                        objdo.YVal = 150
                    End If
                ElseIf InStr(strversion, "IE8") > 0 Then
                    ' update sign position 09012012 -- irvan v incrmentX re-change position
                    If IncrMentX > 0 Then
                        objdo.XVal = 39
                        objdo.YVal = 150
                    Else
                        objdo.XVal = 320
                        objdo.YVal = 150
                    End If
                ElseIf InStr(strversion, "Fire") > 0 Then
                    ' update sign position 09012012 -- irvan v incrmentX re-change position
                    If IncrMentX > 0 Then
                        objdo.XVal = 39
                        objdo.YVal = 150
                    Else
                        objdo.XVal = 320
                        objdo.YVal = 150
                    End If
                Else
                    ' update sign position 09012012 -- irvan v incrmentX re-change position
                    If IncrMentX > 0 Then
                        objdo.XVal = 39
                        objdo.YVal = 150
                    Else
                        objdo.XVal = 320
                        objdo.YVal = 150
                    End If
                End If
                XBauts = objdo.XVal
                YBauts = objdo.YVal
                '--
                Ybaut = 2
                Try
                    objdb.ExeNonQuery("exec [uspBAUTXYI] " & hdnsiteid.Value & "," & hdnversion.Value & "," & hdndocid.Value & "," & XBauts & "," & YBauts & ", '" & hdnPono.Value & "'," & Ybaut & "," & dvNotIn(IncrMentX).Item(1).ToString & "," & dvNotIn(IncrMentX).Item(5).ToString)
                Catch ex As Exception
                    objdb.ExeNonQuery("exec uspErrLog '', 'BAUTXYI','" & ex.Message.ToString.Replace("'", "''") & "','uspBAUTXYI'")
                End Try
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
    Protected Sub grddocuments_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grddocuments.RowDataBound
        Select Case e.Row.RowType
            Case DataControlRowType.DataRow
                Dim lbl As Label = CType(e.Row.FindControl("lblNo"), Label)
                lbl.Text = (grddocuments.PageIndex * grddocuments.PageSize) + (e.Row.RowIndex + 1) & ".&nbsp;"
        End Select
        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim LblDocId As Label = CType(e.Row.FindControl("LblDocId"), Label)

            If LblDocId.Text = ConfigurationManager.AppSettings("ATP") Then
                url = "../PO/frmViewDocumentATP.aspx?id=" & e.Row.Cells(4).Text
            Else
                url = "../PO/frmViewDocument.aspx?id=" & e.Row.Cells(4).Text
            End If

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
            objBO.uspRPTupdate(lblPO.Text, hdnSiteno.Value)
        Else
            hdnready4baut.Value = 0
        End If
    End Sub
    Sub AuditTrail()
        objET.PoNo = hdnPono.Value
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
        If Request.Browser.Browser = "IE" Then
            ScriptManager.RegisterStartupScript(Page, GetType(updatePanel), "MyScriptdd", "getControlPosition();", True)
            'added below sub for saving the online form data
            Dim strsql As String = "Exec odbautinsert " & Request.QueryString("id") & " ,'" & _
            objdb.removeUnwantedQueryChar(hdnPono.Value) & "','" & _
            objdb.removeUnwantedQueryChar(hdnSiteno.Value) & "'," & _
            objdb.removeUnwantedQueryChar(hdnversion.Value) & "," & _
            IIf(txtDate.Value.ToString <> "", "'" & objdb.removeUnwantedQueryChar(txtDate.Value) & "'", "null") & ",'" & _
            objdb.removeUnwantedQueryChar(txtBautNo.Value) & "'," & _
            IIf(txtATPDated.Value.ToString <> "", "'" & objdb.removeUnwantedQueryChar(txtATPDated.Value) & "'", "null") & ",'" & _
            objdb.removeUnwantedQueryChar(Session("User_Name")) & "'," & _
            IIf(txtATPDate.Value.ToString <> "", "'" & objdb.removeUnwantedQueryChar(txtATPDate.Value) & "'", "null") & "," & _
            IIf(txtOADate.Value.ToString <> "", "'" & objdb.removeUnwantedQueryChar(txtOADate.Value) & "'", "null") & "," & _
            IIf(txtOADated.Value.ToString <> "", "'" & objdb.removeUnwantedQueryChar(txtOADated.Value) & "'", "null") & "," & _
            IIf(txtkpidate.Value.ToString <> "", "'" & objdb.removeUnwantedQueryChar(txtkpidate.Value) & "'", "null") & "," & _
            IIf(txtd4.Value.ToString <> "", "'" & objdb.removeUnwantedQueryChar(txtd4.Value) & "'", "null") & ",'" & _
            txtd1.Value.Replace("'", "''") & "','" & objdb.removeUnwantedQueryChar(txtd2.Value) & "','" & _
            txtd3.Value.Replace("'", "''") & "','" & objdb.removeUnwantedQueryChar(txtConfig.Value) & "'"
            Dim result As Integer = 0
            result = objUtil.ExeQueryScalar(strsql)
            strsql = "EXEC uspODWctrBAUTI " & Request.QueryString("id") & ",'" & _
            objdb.removeUnwantedQueryChar(hdnPono.Value) & "','" & _
            objdb.removeUnwantedQueryChar(hdnsiteid.Value) & "'," & _
            objdb.removeUnwantedQueryChar(hdnversion.Value) & ",'" & _
            objdb.removeUnwantedQueryChar(txtDay.Value) & "'," & _
            IIf(txtDateWCTR.Value <> "", objdb.removeUnwantedQueryChar(txtDateWCTR.Value), 0) & "," & _
            IIf(txtMonth.Value <> "", objdb.removeUnwantedQueryChar(txtMonth.Value), 0) & "," & _
            IIf(txtYear.Value <> "", objdb.removeUnwantedQueryChar(txtYear.Value), 0) & ",'" & _
            objdb.removeUnwantedQueryChar(lblProjectidwctr.Text) & "'," & _
            IIf(txtWorkShouldFinishedbaut.Value.ToString <> "", "'" & objdb.removeUnwantedQueryChar(txtWorkShouldFinishedbaut.Value) & "'", "null") & "," & _
            IIf(txtKPIAccepted.Value.ToString <> "", "'" & objdb.removeUnwantedQueryChar(txtKPIAccepted.Value) & "'", "null") & ",'" & _
            objdb.removeUnwantedQueryChar(txtReasonBaut1.Value) & "'," & _
            IIf(txtReasonDaysBaut1.Value <> "", objdb.removeUnwantedQueryChar(txtReasonDaysBaut1.Value), 0) & ",'" & _
            objdb.removeUnwantedQueryChar(txtReasonBaut2.Value) & "'," & _
            IIf(txtReasonDaysBaut2.Value <> "", objdb.removeUnwantedQueryChar(txtReasonDaysBaut2.Value), 0) & ",'" & _
            objdb.removeUnwantedQueryChar(txtReasonBaut3.Value) & "'," & _
            IIf(txtReasonDaysBaut3.Value <> "", objdb.removeUnwantedQueryChar(txtReasonDaysBaut3.Value), 0) & "," & _
            IIf(txtTotalC.Value <> "", objdb.removeUnwantedQueryChar(txtTotalC.Value), 0) & ",'" & _
            objdb.removeUnwantedQueryChar(txtsiteidpo.Text) & "','" & _
            objdb.removeUnwantedQueryChar(txtsitenamepo.Text) & "','" & _
            objdb.removeUnwantedQueryChar(Session("User_Name")) & "','" & _
            objdb.removeUnwantedQueryChar(DateTime.Now) & "'"
            result = objUtil.ExeQueryScalar(strsql)
            If result = 1 Then
                Dim ss As Boolean = True
                For intCount As Integer = 0 To grddocuments.Rows.Count - 1
                    Dim rdl1 As RadioButtonList = CType(grddocuments.Rows(intCount).FindControl("rdbstatus"), RadioButtonList)
                    If rdl1.SelectedValue = 1 Then
                        ss = False
                    End If
                Next
                If (ss) Then
                    'Response.Write("<script>alert('No permission')</script>")
                Else
                    Response.Write("<script>alert('No permissions to generate')</script>")
                    Exit Sub
                End If
                If (hdnKeyVal.Value = "") Then
                    hdnKeyVal.Value = 0
                End If
                lblDate.Text = IIf(txtDate.Value = "", "", txtDate.Value)
                imgDate.Visible = False
                txtDate.Visible = False
                lblATPDated.Text = IIf(txtATPDated.Value = "", "", txtATPDated.Value)
                txtATPDated.Visible = False
                lblATPDate.Text = IIf(txtATPDate.Value = "", "", txtATPDate.Value)
                ImgATPDate.Visible = False
                txtATPDate.Visible = False
                lblOADate.Text = IIf(txtOADate.Value = "", "", txtOADate.Value)
                txtOADate.Visible = False
                lblOADated.Text = IIf(txtOADated.Value = "", "", txtOADated.Value)
                ImgOADated.Visible = False
                txtOADated.Visible = False
                lblkpidate.Text = IIf(txtkpidate.Value = "", "", txtkpidate.Value)
                imgkpidate.Visible = False
                txtkpidate.Visible = False
                lbld4.Text = IIf(txtd4.Value = "", "", txtd4.Value)
                lbld1.Text = txtd1.Value
                txtd1.Visible = False
                lbld2.Text = txtd2.Value
                txtd2.Visible = False
                lbld3.Text = txtd3.Value
                txtd3.Visible = False
                Imgd4.Visible = False
                txtd4.Visible = False
                lblConfig.Text = txtConfig.Value
                txtConfig.Visible = False
                lblBautNo.Text = IIf(txtBautNo.Value <> "", txtBautNo.Value, "")
                txtBautNo.Visible = False
                lblsiteidpo.Text = txtsiteidpo.Text
                lblBsiteidpo.Text = txtsiteidpo.Text
                txtsiteidpo.Visible = False
                lblPoWCTR.Text = txtPoWCTR.Text
                txtPoWCTR.Visible = False
                lblPO.Text = txtPO.Text
                txtPO.Visible = False
                lblPONo.Text = txtPONo.Text
                txtPONo.Visible = False
                lblsitenamepo.Text = txtsitenamepo.Text
                lblBsitenamepo.Text = txtsitenamepo.Text
                txtsitenamepo.Visible = False
                lblKPIAccepted.Text = txtKPIAccepted.Value
                If lblKPIAccepted.Text = "" Then lblKPIAccepted.Text = ""
                imgKPIAccepted.Visible = False
                If lblProjectidwctr.Text = "" Then lblProjectidwctr.Text = ""
                lblDay.Text = txtDay.Value
                If lblDay.Text = "" Then lblDay.Text = ""
                txtDay.Visible = False
                lblDate.Text = txtDate.Value
                If lblDate.Text = "" Then lblDate.Text = ""
                txtDate.Visible = False
                lblDateWCTR.Text = txtDateWCTR.Value
                If txtDateWCTR.Value = "" Then lblDateWCTR.Text = ""
                txtDateWCTR.Visible = False
                lblMonth.Text = txtMonth.Value
                If lblMonth.Text = "" Then lblMonth.Text = ""
                txtMonth.Visible = False
                lblYear.Text = txtYear.Value
                If lblYear.Text = "" Then lblYear.Text = ""
                txtYear.Visible = False
                lblDurationExec.Text = txtDurationExec.Value
                If lblDurationExec.Text = "" Then lblDurationExec.Text = ""
                txtDurationExec.Visible = False
                lblWrkStarted.Text = txtWorkStarted.Value
                If lblWrkStarted.Text = "" Then lblWrkStarted.Text = ""
                imgWrkStarted.Visible = False
                txtWorkStarted.Visible = False
                lblWorkShouldFinished.Text = txtWorkShouldFinished.Value
                If lblWorkShouldFinished.Text = "" Then lblWorkShouldFinished.Text = ""
                imgWorkShouldFinished.Visible = False
                txtWorkShouldFinished.Visible = False
                lblWorkhasbeenFinished.Text = txtWorkHasBeenFinished.Value
                If lblWorkhasbeenFinished.Text = "" Then lblWorkhasbeenFinished.Text = ""
                imgWorkhasbeenFinished.Visible = False
                txtWorkHasBeenFinished.Visible = False
                lblActualExec.Text = txtActualExec.Value
                If lblActualExec.Text = "" Then lblActualExec.Text = ""
                txtActualExec.Visible = False
                lblTotalA.Text = txtTotalA.Value
                If lblTotalA.Text = "" Then lblTotalA.Text = ""
                txtTotalA.Visible = False
                txtTotalB.Visible = False
                lblReason1.Text = txtReason1.Value
                If lblReason1.Text = "" Then lblReason1.Text = ""
                txtReason1.Visible = False
                lblReason2.Text = txtReason2.Value
                If lblReason2.Text = "" Then lblReason2.Text = ""
                txtReason2.Visible = False
                lblReason3.Text = txtReason3.Value
                If lblReason3.Text = "" Then lblReason3.Text = ""
                txtReason3.Visible = False
                lblReasonDays1.Text = txtReasonDays1.Value
                If lblReasonDays1.Text = "" Then lblReasonDays2.Text = ""
                txtReasonDays1.Visible = False
                lblReasonDays2.Text = txtReasonDays2.Value
                If lblReasonDays2.Text = "" Then lblReasonDays2.Text = ""
                txtReasonDays2.Visible = False
                lblReasonDays3.Text = txtReasonDays3.Value
                If lblReasonDays3.Text = "" Then lblReasonDays3.Text = ""
                txtReasonDays3.Visible = False
                lblWorkShouldFinishedbaut.Text = txtWorkShouldFinishedbaut.Value
                If lblWorkShouldFinishedbaut.Text = "" Then lblWorkShouldFinishedbaut.Text = ""
                imgWorkShouldFinishedbaut.Visible = False
                txtWorkShouldFinishedbaut.Visible = False
                If lblKPIAccepted.Text = "" Then lblKPIAccepted.Text = ""
                txtKPIAccepted.Visible = False
                lblReasonBaut1.Text = txtReasonBaut1.Value
                txtReasonBaut1.Visible = False
                If lblReasonBaut1.Text = "" Then lblReasonBaut1.Text = ""
                lblReasonBaut2.Text = txtReasonBaut2.Value
                If lblReasonBaut2.Text = "" Then lblReasonBaut2.Text = ""
                txtReasonBaut2.Visible = False
                lblReasonBaut3.Text = txtReasonBaut3.Value
                If lblReasonBaut3.Text = "" Then lblReasonBaut3.Text = ""
                txtReasonBaut3.Visible = False
                lblReasonDaysBaut1.Text = txtReasonDaysBaut1.Value
                If lblReasonDaysBaut1.Text = "" Then lblReasonDaysBaut2.Text = ""
                txtReasonDaysBaut1.Visible = False
                lblReasonDaysBaut2.Text = txtReasonDaysBaut2.Value
                If lblReasonDaysBaut2.Text = "" Then lblReasonDaysBaut2.Text = ""
                txtReasonDaysBaut2.Visible = False
                lblReasonDaysBaut3.Text = txtReasonDaysBaut3.Value
                If lblReasonDaysBaut3.Text = "" Then lblReasonDaysBaut3.Text = ""
                txtReasonDaysBaut3.Visible = False
                lblTotalC.Text = txtTotalC.Value
                If lblTotalC.Text = "" Then lblTotalC.Text = ""
                txtTotalC.Visible = False
                lblJobDelay.Text = txtJobDelay.Value
                If lblJobDelay.Text = "" Then lblJobDelay.Text = ""
                txtJobDelay.Visible = False
                btnGenerate.Visible = False
                grddocuments.Visible = False



                If hdnDGBox.Value = True Then
                    strsql = "select count(*) from docSignPositon where doc_id=" & hdndocid.Value
                    If objUtil.ExeQueryScalar(strsql) > 0 Then
                        uploaddocument(Convert.ToInt32(hdnversion.Value), Convert.ToInt32(hdnKeyVal.Value))
                    Else
                        Response.Write("<script>alert('please Set X,Y coordinates for Digital sign in document Template..')</script>")
                    End If
                Else
                    uploaddocument(Convert.ToInt32(hdnversion.Value), Convert.ToInt32(hdnKeyVal.Value))
                End If
            Else
                ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('Error while saving the Data');", True)
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
    Sub DOThis(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rad As RadioButtonList = CType(sender, RadioButtonList)
        Dim grdr As GridViewRow = CType(rad.NamingContainer, GridViewRow)
        Dim x As Integer = grdr.RowIndex
        Dim rdl As RadioButtonList = grddocuments.Rows(x).Cells(5).FindControl("rdbstatus")
        Dim txt1 As TextBox = grddocuments.Rows(x).Cells(6).FindControl("txtremarks")
        txt1.Text = "Please enter remarks here......"
        If rdl.SelectedValue = 0 Then 'approve
            txt1.Visible = False
        Else 'reject
            txt1.Visible = True
        End If
    End Sub
End Class
