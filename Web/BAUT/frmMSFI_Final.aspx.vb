Imports System.Data
Imports System.IO
Imports Common
Imports Entities
Imports BusinessLogic
Imports CRFramework
Imports Common_NSNFramework

Partial Class BAUT_frmMSFI_Final
    Inherits System.Web.UI.Page

    Dim msficontrol As New MSFIController
    Dim controller As New HCPTController
    Dim objBO As New BOSiteDocs
    Dim objET As New ETAuditTrail
    Dim objET1 As New ETSiteDoc
    Dim objUtil As New DBUtil

#Region "NSNFramework"
    Dim dbutils_nsn As New DBUtils_NSN
#End Region

    

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            dvPrint2.Visible = False
            BindData(GetMSFIID())
            BindDocDetail()
        End If
    End Sub

    Protected Sub btnGenerate_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGenerate.Click
        If Request.Browser.Browser = "IE" Then
            uploaddocument(GetVersionID(), 3)
        Else
            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('Please use Internet Explorer');", True)
        End If
    End Sub

#Region "custom methods"
    Private Sub BindData(ByVal msfid As Int32)
        BindMSFI(msficontrol.MSFIDetail_GetList(msfid))
        GetSiteAttribute(GetWPID())
    End Sub

    Private Sub uploaddocument(ByVal vers As Integer, ByVal keyval As Integer)
        Dim dt As DataTable = objBO.getbautdocdetailsNEW(hdndocid.Value) '(Constants._Doc_SSR)
        Dim sec As String = dt.Rows(0).Item("sec_name").ToString
        Dim subsec As String = dt.Rows(0).Item("subsec_name").ToString
        Dim secpath As String = ""
        Dim ft As String = ""
        Dim path As String = ""
        Dim filenameorg As String
        Dim FileNameOnly As String = "-" & "MSFI" & "-"
        filenameorg = lblSiteID.Text & FileNameOnly & Format(CDate(DateTime.Now), "ddMMyyyyHHss")
        Dim ReFileName As String = filenameorg & ".htm"
        secpath = IIf(subsec = "" Or subsec = "0", sec & "\", sec & "\" & subsec & "\")
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
            .PONo = hdnpono.Value
        End With
        objBO.updatedocupload(objET1)       
        AuditTrail()

        If String.IsNullOrEmpty(Request.QueryString("from")) Then
            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScripta", "Showmain('success');", True)
        Else
            'Response.Redirect("../HCPT_Dashboard/frmCACReadyCreation.aspx")
            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScripta", "Showmain('success');", True)
        End If

    End Sub

    Sub AuditTrail()
        objET.PoNo = hdnpono.Value
        objET.SiteId = hdnsiteid.Value
        objET.DocId = hdndocid.Value
        objET.Version = hdnversion.Value
        objET.Task = "1"
        objET.Status = "1"
        objET.Userid = Session("User_Id")
        objET.Roleid = Session("Role_Id")
        objET.fldtype = hdnScope.Value
        dbutils_nsn.InsertAuditTrailNew(objET, GetWPID())
    End Sub

    Private Function DOInsertTrans(ByVal siteid As String, ByVal docName As String, ByVal version As Integer, ByVal path As String) As String
        Dim docId As Integer = Integer.Parse(hdndocid.Value)
        Dim wfcontrol As New CRController
        Dim isSucceed As Boolean = True
        Dim dtNew As DataTable
        If hdnWfId.Value <> 0 Then
            dtNew = wfcontrol.GetWorkflowDetail(Integer.Parse(hdnWfId.Value))
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
                                transinfo.Xval = 0
                                transinfo.Yval = 0
                            ElseIf transinfo.UGPID = 4 Then
                                transinfo.Xval = 0
                                transinfo.Yval = 0
                            ElseIf transinfo.UGPID = 11 Then
                                transinfo.Xval = 0
                                transinfo.Yval = 0
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
        Else
            Dim status As Integer = 99
            objBO.uspwftransactionNOTWFI(docId, 0, hdnsiteid.Value, hdnversion.Value, 1, Session("User_Id"), status, 2, Session("User_Name"))
            Return "0"
        End If
        Return "-1"
    End Function

    Private Function CreatePDFFile(ByVal StrPath As String) As String
        StrPath = StrPath.Replace(" \", "\").ToString 'bugfix100816 causing error when the path containing space before \        
        Dim dtba As New DataTable
        Dim filenameorg As String
        Dim ReFileName As String
        filenameorg = hdnsiteno.Value & "-" & "MSFI" & "-" & Format(CDate(DateTime.Now), "ddMMyyyyHHss")
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
        sw.WriteLine("#dvPrint{width:800px;height:700px;}")
        sw.WriteLine("#dvPrint2{width:800px;height:700px;}")
        sw.WriteLine(".lblText{font-family: verdana;font-size: 8pt;color: #000000;text-align: left;}")
        sw.WriteLine(".lblFieldHeader{font-family: verdana;font-size: 10px;color: #000000;text-align: center;font-weight:bold;}")
        sw.WriteLine(".lblFieldText{font-family: verdana;font-size: 9px;color: #000000;}")
        sw.WriteLine(".lblFieldBoldText{font-family: verdana;font-size: 9px;color: #000000;font-weight:bolder;}")
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
        sw.WriteLine(".siteATTPanel{margin-top:10px;  height:70px; text-align:left;}")
        sw.WriteLine(".SiteDetailInfoPanel{margin-top: 10px;width: 100%;text-align: left;height: 150px;}")
        sw.WriteLine(".sitedescription{margin-top: 10px;width: 800px;text-align: left;height:60px;}")
        sw.WriteLine(".pnlremarks{height:60px;}")
        sw.WriteLine(".whitespace{height:5px;}")
        sw.WriteLine(".pnlNote{height:200px;}")
        sw.WriteLine(".footerPanel{height:350px;margin-top:10px;}")
        sw.WriteLine(".PageBreak{page-break-before: always;}")
        sw.WriteLine("</style></head>")
        sw.WriteLine("<body class=""MainCSS"">")
        dvPrint.RenderControl(sw)
        If hdnPageNo.Value = "2" Then
            sw.WriteLine("<div class=""PageBreak""></div>")
            dvPrint2.RenderControl(sw)
        End If
        sw.WriteLine("</body>")
        sw.WriteLine("</html>")
        sw.Close()
        sw.Dispose()
        Return EBASTFileUpload.ConvertAnyFormatToPDFHtmlNew(StrPath & ReFileName, StrPath, filenameorg)
    End Function

    Private Sub BindMSFI(ByVal dtresult As DataTable)
        If dtresult.Rows.Count > 0 Then
            Dim strTableItemList As New StringBuilder
            Dim strTableItemListContinue1 As New StringBuilder
            Dim strTableItemListContinue2 As New StringBuilder
            strTableItemList.Append("<table cellspacing=""0"" rules=""all"" border=""1"" id=""GvItemList"" style=""border-color:Black;width:790px;border-collapse:collapse;"">")
            strTableItemList.Append("<tr class=""HeaderGrid2"" style=""font-weight:bold;"">")
            strTableItemList.Append("<td align=""center"" style=""background-color:White;border-color:black;border-style:solid;border-width:1px;""><span class=""lblFieldHeader"">SN</span></td>")
            strTableItemList.Append("<td align=""center"" style=""background-color:White;border-color:black;border-style:solid;border-width:1px;""><span class=""lblFieldHeader"">Site Document Package</span></td>")
            strTableItemList.Append("<td align=""center"" style=""background-color:White;border-color:black;border-style:solid;border-width:1px;""><span class=""lblFieldHeader"">Category</span></td>")
            strTableItemList.Append("<td align=""center"" style=""background-color:White;border-color:black;border-style:solid;border-width:1px;""><span class=""lblFieldHeader"">Remarks</span></td>")
            strTableItemList.Append("</tr>")
            Dim countrow As Integer = 0
            For Each drw As DataRow In dtresult.Rows
                If (countrow > 35 And countrow <= 60) Then
                    hdnPageNo.Value = "2"
                    strTableItemListContinue1.Append("<tr class=""oddGrid2"">")
                    If Integer.Parse(drw.Item("parent_id")) = 0 Then
                        strTableItemListContinue1.Append("<td style=""border-color:Black;border-width:1px;border-style:solid;width:20px;""><span class=""lblFieldBoldText"">" & drw.Item("sn") & "</span></td>")
                        strTableItemListContinue1.Append("<td style=""border-color:Black;border-width:1px;border-style:solid;width:255px;""><span class=""lblFieldBoldText"">" & drw.Item("docname") & "</span></td>")
                        strTableItemListContinue1.Append("<td style=""border-color:Black;border-width:1px;border-style:solid;width:145px;""></td>")
                    Else
                        strTableItemListContinue1.Append("<td style=""border-color:Black;border-width:1px;border-style:solid;width:20px;"">&nbsp;&nbsp;<span class=""lblFieldText"">" & drw.Item("sn") & "</span></td>")
                        strTableItemListContinue1.Append("<td style=""border-color:Black;border-width:1px;border-style:solid;width:255px;"">&nbsp;&nbsp;<span class=""lblFieldText"">" & drw.Item("docname") & "</span></td>")
                        strTableItemListContinue1.Append("<td style=""border-color:Black;border-width:1px;border-style:solid;width:145px;"">")
                        If Convert.ToBoolean(drw.Item("iscopy")) = True Then
                            strTableItemListContinue1.Append("<span class=""lblFieldText""><input type=""checkbox"" value=""IsCopy"" onClick=""return readOnlyCheckBox()"" checked >Copy</span>")
                        Else
                            strTableItemListContinue1.Append("<span class=""lblFieldText""><input type=""checkbox"" value=""IsCopy"" readonly>Copy</span>")
                        End If

                        If Convert.ToBoolean(drw.Item("isoriginal")) = True Then
                            strTableItemListContinue1.Append("<span class=""lblFieldText""><input type=""checkbox"" value=""isOriginal"" checked>Original</span>")
                        Else
                            strTableItemListContinue1.Append("<span class=""lblFieldText""><input type=""checkbox"" value=""IsOriginal"">Original</span>")
                        End If

                        If Convert.ToBoolean(drw.Item("isna")) = True Then
                            strTableItemListContinue1.Append("<span class=""lblFieldText""><input type=""checkbox"" value=""isna"" checked>NA</span>")
                        Else
                            strTableItemListContinue1.Append("<span class=""lblFieldText""><input type=""checkbox"" value=""isna"">NA</span>")
                        End If
                        strTableItemListContinue1.Append("</td>")
                    End If
                    strTableItemListContinue1.Append("<td style=""border-color:Black;border-width:1px;border-style:solid;width:250px;""><span class=""lblFieldText"">" & drw.Item("remarks") & "</span></td>")
                    strTableItemListContinue1.Append("</tr>")
                ElseIf (countrow > 60) Then
                    hdnPageNo.Value = "3"
                    strTableItemListContinue2.Append("<tr class=""oddGrid2"">")
                    If Integer.Parse(drw.Item("parent_id")) = 0 Then
                        strTableItemListContinue2.Append("<td style=""border-color:Black;border-width:1px;border-style:solid;width:20px;""><span class=""lblFieldBoldText"">" & drw.Item("sn") & "</span></td>")
                        strTableItemListContinue2.Append("<td style=""border-color:Black;border-width:1px;border-style:solid;width:255px;""><span class=""lblFieldBoldText"">" & drw.Item("docname") & "</span></td>")
                        strTableItemListContinue2.Append("<td style=""border-color:Black;border-width:1px;border-style:solid;width:145px;""></td>")
                    Else
                        strTableItemListContinue2.Append("<td style=""border-color:Black;border-width:1px;border-style:solid;width:20px;"">&nbsp;&nbsp;<span class=""lblFieldText"">" & drw.Item("sn") & "</span></td>")
                        strTableItemListContinue2.Append("<td style=""border-color:Black;border-width:1px;border-style:solid;width:255px;"">&nbsp;&nbsp;<span class=""lblFieldText"">" & drw.Item("docname") & "</span></td>")
                        strTableItemListContinue2.Append("<td style=""border-color:Black;border-width:1px;border-style:solid;width:145px;"">")
                        If Convert.ToBoolean(drw.Item("iscopy")) = True Then
                            strTableItemListContinue2.Append("<span class=""lblFieldText""><input type=""checkbox"" value=""IsCopy"" onClick=""return readOnlyCheckBox()"" checked >Copy</span>")
                        Else
                            strTableItemListContinue2.Append("<span class=""lblFieldText""><input type=""checkbox"" value=""IsCopy"" readonly>Copy</span>")
                        End If

                        If Convert.ToBoolean(drw.Item("isoriginal")) = True Then
                            strTableItemListContinue2.Append("<span class=""lblFieldText""><input type=""checkbox"" value=""isOriginal"" checked>Original</span>")
                        Else
                            strTableItemListContinue2.Append("<span class=""lblFieldText""><input type=""checkbox"" value=""IsOriginal"">Original</span>")
                        End If

                        If Convert.ToBoolean(drw.Item("isna")) = True Then
                            strTableItemListContinue2.Append("<span class=""lblFieldText""><input type=""checkbox"" value=""isna"" checked>NA</span>")
                        Else
                            strTableItemListContinue2.Append("<span class=""lblFieldText""><input type=""checkbox"" value=""isna"">NA</span>")
                        End If
                        strTableItemListContinue2.Append("</td>")
                    End If
                    strTableItemListContinue2.Append("<td style=""border-color:Black;border-width:1px;border-style:solid;width:250px;""><span class=""lblFieldText"">" & drw.Item("remarks") & "</span></td>")
                    strTableItemListContinue2.Append("</tr>")
                Else
                    hdnPageNo.Value = "1"
                    strTableItemList.Append("<tr class=""oddGrid2"">")
                    If Integer.Parse(drw.Item("parent_id")) = 0 Then
                        strTableItemList.Append("<td style=""border-color:Black;border-width:1px;border-style:solid;width:20px;""><span class=""lblFieldBoldText"">" & drw.Item("sn") & "</span></td>")
                        strTableItemList.Append("<td style=""border-color:Black;border-width:1px;border-style:solid;width:255px;""><span class=""lblFieldBoldText"">" & drw.Item("docname") & "</span></td>")
                        strTableItemList.Append("<td style=""border-color:Black;border-width:1px;border-style:solid;width:145px;""></td>")
                    Else
                        strTableItemList.Append("<td style=""border-color:Black;border-width:1px;border-style:solid;width:20px;"">&nbsp;&nbsp;<span class=""lblFieldText"">" & drw.Item("sn") & "</span></td>")
                        strTableItemList.Append("<td style=""border-color:Black;border-width:1px;border-style:solid;width:255px;"">&nbsp;&nbsp;<span class=""lblFieldText"">" & drw.Item("docname") & "</span></td>")
                        strTableItemList.Append("<td style=""border-color:Black;border-width:1px;border-style:solid;width:145px;"">")
                        If Convert.ToBoolean(drw.Item("iscopy")) = True Then
                            strTableItemList.Append("<span class=""lblFieldText""><input type=""checkbox"" value=""IsCopy"" onClick=""return readOnlyCheckBox()"" checked >Copy</span>")
                        Else
                            strTableItemList.Append("<span class=""lblFieldText""><input type=""checkbox"" value=""IsCopy"" readonly>Copy</span>")
                        End If

                        If Convert.ToBoolean(drw.Item("isoriginal")) = True Then
                            strTableItemList.Append("<span class=""lblFieldText""><input type=""checkbox"" value=""isOriginal"" checked>Original</span>")
                        Else
                            strTableItemList.Append("<span class=""lblFieldText""><input type=""checkbox"" value=""IsOriginal"">Original</span>")
                        End If

                        If Convert.ToBoolean(drw.Item("isna")) = True Then
                            strTableItemList.Append("<span class=""lblFieldText""><input type=""checkbox"" value=""isna"" checked>NA</span>")
                        Else
                            strTableItemList.Append("<span class=""lblFieldText""><input type=""checkbox"" value=""isna"">NA</span>")
                        End If
                        strTableItemList.Append("</td>")
                    End If
                    strTableItemList.Append("<td style=""border-color:Black;border-width:1px;border-style:solid;width:250px;""><span class=""lblFieldText"">" & drw.Item("remarks") & "</span></td>")
                    strTableItemList.Append("</tr>")
                End If
                countrow += 1
            Next
            strTableItemList.Append("</table>")
            ltrMSFIList.Text = strTableItemList.ToString()
            If (hdnPageNo.Value = "2") Then
                dvPrint2.Visible = True
                BindMSFICont1(strTableItemListContinue1.ToString())
            End If
        End If
    End Sub

    Private Sub BindMSFICont1(ByVal getList As String)
        Dim strTableItemList As New StringBuilder
        strTableItemList.Append("<table cellspacing=""0"" rules=""all"" border=""1"" id=""GvItemList2"" style=""border-color:Black;width:790px;border-collapse:collapse;"">")
        strTableItemList.Append("<tr class=""HeaderGrid2"" style=""font-weight:bold;"">")
        strTableItemList.Append("<td align=""center"" style=""background-color:White;border-color:black;border-style:solid;border-width:1px;""><span class=""lblFieldHeader"">SN</span></td>")
        strTableItemList.Append("<td align=""center"" style=""background-color:White;border-color:black;border-style:solid;border-width:1px;""><span class=""lblFieldHeader"">Site Document Package</span></td>")
        strTableItemList.Append("<td align=""center"" style=""background-color:White;border-color:black;border-style:solid;border-width:1px;""><span class=""lblFieldHeader"">Category</span></td>")
        strTableItemList.Append("<td align=""center"" style=""background-color:White;border-color:black;border-style:solid;border-width:1px;""><span class=""lblFieldHeader"">Remarks</span></td>")
        strTableItemList.Append("</tr>")
        strTableItemList.Append(getList)
        strTableItemList.Append("</table>")
        ltrMSFIList2.Text = strTableItemList.ToString()
    End Sub

    Private Sub BindMSFICont2(ByVal getList As String)

    End Sub

    Private Sub GetSiteAttribute(ByVal wpid As String)
        Dim info As SiteInfo = controller.GetSiteInfoDetail(wpid)
        If info IsNot Nothing Then
            lblProjectName.Text = info.PONO
            lblProjectName2.Text = info.PONO
            lblServiceType.Text = info.Scope
            lblSvcType2.Text = info.Scope
            lblSiteID.Text = info.SiteNo
            lblSiteID2.Text = info.SiteNo
            hdnsiteno.Value = info.SiteNo
            lblSiteName.Text = info.SiteName
            lblSitename2.Text = info.SiteName
            hdnpono.Value = info.PONO
            hdnScope.Value = info.Scope
        End If
    End Sub

    Private Function GetMSFIID() As Int32
        If Not String.IsNullOrEmpty(Request.QueryString("mid")) Then
            Return Convert.ToInt32(Request.QueryString("mid"))
        Else
            Return 0
        End If
    End Function

    Private Function GetWPID() As String
        If Not String.IsNullOrEmpty(Request.QueryString("wpid")) Then
            Return Convert.ToString(Request.QueryString("wpid"))
        Else
            Return "0"
        End If
    End Function

    Private Function GetSWID() As Int32
        If String.IsNullOrEmpty(Request.QueryString("swid")) Then
            Return 0
        Else
            Return Convert.ToInt32(Request.QueryString("swid"))
        End If
    End Function

    Private Function GetVersionID() As Integer
        If (Not String.IsNullOrEmpty(Request.QueryString("vers"))) Then
            Return Integer.Parse(Request.QueryString("vers"))
        Else
            Return -1
        End If
    End Function

    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        Return
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

#End Region

End Class
