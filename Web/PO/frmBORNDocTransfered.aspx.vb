Imports System.Data
Imports BusinessLogic
Imports Entities
Imports Common
Imports System.IO
Imports Common_NSNFramework
Imports Newtonsoft.Json



Partial Class PO_frmBORNDocTransfered
    Inherits System.Web.UI.Page
    Dim controller As New HCPTController
    Dim bornintegration As New BORNController
    Dim objBO As New BOSiteDocs
    Dim objET As New ETAuditTrail
    Dim objET1 As New ETSiteDoc
    Dim objUtil As New DBUtil

#Region "NSNFramework"
    Dim dbutils_nsn As New DBUtils_NSN
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            BindSiteInfo(GetWPID())
            BindApproval(GetSubmissionSNO())
            BindDocDetail(GetDocID())
            BindBOQSVC(GetSubmissionSNO())
            BindBOQEQP(GetSubmissionSNO())
            BindSiteDocDetail(GetSWID())
        End If
    End Sub

    Protected Sub btnConfirm_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnConfirm.Click
        'If Request.Browser.Browser = "IE" Then

        'Else
        '    ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('Please use Internet Explorer');", True)
        'End If
        uploaddocument(hdnVersion.Value, 3)
    End Sub

#Region "Custom Methods"

    Private Sub BindSiteDocDetail(ByVal swid As Int32)
        Dim strQuery As String = "Exec HCPT_uspGeneral_GetDetailDoc " & Convert.ToString(GetSWID())
        Dim dtSiteDOCDetail As DataTable = objUtil.ExeQueryDT(strQuery, "SiteDoc1")
        If dtSiteDOCDetail.Rows.Count > 0 Then
            hdnSiteid.Value = dtSiteDOCDetail.Rows(0).Item("SiteId").ToString
            hdnVersion.Value = dtSiteDOCDetail.Rows(0).Item("version").ToString
        End If
    End Sub

    Private Sub BindSiteInfo(ByVal wpid As String)
        Dim info As SiteInfo = controller.GetSiteInfoDetail(wpid)
        If info IsNot Nothing Then
            divPONO.InnerText = info.HOTAsPO
            divScope.InnerText = info.Scope
            dvScopeSvc.InnerText = info.Scope
            dvScopeEqp.InnerText = info.Scope
            'divSiteID.InnerText = info.SiteNo '130518_130214
            'dvSiteIDEqp.InnerText = info.SiteNo
            'dvSiteIDSvc.InnerText = info.SiteNo

            divSiteID.InnerText = "130518_130214"
            dvSiteIDEqp.InnerText = "130518_130214"
            dvSiteIDSvc.InnerText = "130518_130214"

            divSiteName.InnerText = IIf(Not String.IsNullOrEmpty(info.SiteName), info.SiteName, info.SiteNo)
            dvSitenameEqp.InnerText = IIf(Not String.IsNullOrEmpty(info.SiteName), info.SiteName, info.SiteNo)
            dvSitenameSvc.InnerText = IIf(Not String.IsNullOrEmpty(info.SiteName), info.SiteName, info.SiteNo)

            hdnSiteNo.Value = info.SiteNo
            hdnScope.Value = info.Scope
            hdnPONO.Value = info.PONO
        End If
    End Sub

    Private Sub BindDocDetail(ByVal docid As Integer)
        Dim dinfo As DocInfo = controller.GetDocDetail(docid)
        If dinfo IsNot Nothing Then
            ltrDocname.Text = dinfo.DocName
            ltrDocNameTitlePOSVC.Text = dinfo.DocName
            ltrDocTitleNameEqp.Text = dinfo.DocName
            hdnDocName.Value = dinfo.DocName
        End If
    End Sub

    Private Sub BindApproval(ByVal ssno As Int32)
        Dim count As Integer = 1
        Dim strReviewer As String = String.Empty
        'For Each info As DOCTransactionInfo In controller.GetReviewTransactionLog(sno, wpid, "reviewer")
        '    If count > 1 Then
        '        strReviewer += "<br/>"
        '    End If
        '    strReviewer += "This document was reviewed by <b>" & info.UserInf.Username & " As " & info.UserInf.SignTitle & "</b> On <b>" & String.Format("{0:dd-MMM-yyyy hh:mm:ss}", info.EndDateTime) & "</b>"
        '    count += 1
        'Next
        Dim getApprovalLog = bornintegration.BORN_GetApprovalLog(GetSubmissionSNO())
        If (getApprovalLog.Rows.Count > 0) Then
            For Each drw As DataRow In getApprovalLog.Rows
                If count > 1 Then
                    strReviewer += "<br/>"
                End If
                strReviewer += "This document was " & drw.Item("TaskDesc") & " <b>" & drw.Item("name") & " as " & drw.Item("signtitle") & "</b> On <b>" & String.Format("{0:dd-MMM-yyyy hh:mm:ss}", Convert.ToDateTime(drw.Item("complete_date"))) & "</b>"
                count += 1
            Next
        End If
        divReviewer.InnerHtml = strReviewer
    End Sub

    Private Sub BindBOQSVC(ByVal ssno As Int32)
        Dim dtResult As DataTable = bornintegration.BORN_GetSubPODetail(ssno, "SVC")
        If (dtResult.Rows.Count > 0) Then
            For Each drw As DataRow In dtResult.Rows
                lblPONOSVC.InnerText = drw.Item("cpo_no")
            Next
        End If

        Dim dtBOQList As DataTable = bornintegration.BORN_GetBOQListDetail(ssno, "SVC")
        If (dtBOQList.Rows.Count > 0) Then
            Dim strTableItemList As New StringBuilder
            Dim strTableItemListContinue1 As New StringBuilder
            Dim strTableItemListContinue2 As New StringBuilder
            strTableItemList.Append("<table cellspacing=""0"" rules=""all"" border=""1"" id=""GvItemList"" style=""border-color:Black;width:790px;border-collapse:collapse;"">")
            strTableItemList.Append("<tr class=""HeaderGrid2"" style=""font-weight:bold;"">")
            strTableItemList.Append("<td align=""center"" style=""background-color:White;border-color:black;border-style:solid;border-width:1px;""><span class=""lblFieldHeader"">Package</span></td>")
            strTableItemList.Append("<td align=""center"" style=""background-color:White;border-color:black;border-style:solid;border-width:1px;""><span class=""lblFieldHeader"">Part Number</span></td>")
            strTableItemList.Append("<td align=""center"" style=""background-color:White;border-color:black;border-style:solid;border-width:1px;""><span class=""lblFieldHeader"">Description</span></td>")
            strTableItemList.Append("<td align=""center"" style=""background-color:White;border-color:black;border-style:solid;border-width:1px;""><span class=""lblFieldHeader"">UOM</span></td>")
            strTableItemList.Append("<td align=""center"" style=""background-color:White;border-color:black;border-style:solid;border-width:1px;""><span class=""lblFieldHeader"">As Plan QTY</span></td>")
            strTableItemList.Append("<td align=""center"" style=""background-color:White;border-color:black;border-style:solid;border-width:1px;""><span class=""lblFieldHeader"">Final QTY</span></td>")
            strTableItemList.Append("<td align=""center"" style=""background-color:White;border-color:black;border-style:solid;border-width:1px;""><span class=""lblFieldHeader"">Delta QTY</span></td>")
            strTableItemList.Append("<td align=""center"" style=""background-color:White;border-color:black;border-style:solid;border-width:1px;""><span class=""lblFieldHeader"">Serial No.</span></td>")
            strTableItemList.Append("</tr>")
            For Each drw As DataRow In dtBOQList.Rows
                strTableItemList.Append("<tr class=""oddGrid2"">")
                strTableItemList.Append("<td style=""border-color:Black;border-width:1px;border-style:solid;width:100px;""><span class=""lblFieldText"">" & drw.Item("package_name") & "</span></td>")
                strTableItemList.Append("<td style=""border-color:Black;border-width:1px;border-style:solid;width:80px;""><span class=""lblFieldText"">" & drw.Item("material_code") & "</span></td>")
                strTableItemList.Append("<td style=""border-color:Black;border-width:1px;border-style:solid;width:150px;""><span class=""lblFieldText"">" & drw.Item("material_desc") & "</span></td>")
                strTableItemList.Append("<td style=""border-color:Black;border-width:1px;border-style:solid;width:60px;""><span class=""lblFieldText"">" & drw.Item("UOM") & "</span></td>")
                strTableItemList.Append("<td style=""border-color:Black;border-width:1px;border-style:solid;width:80px;text-align:center;""><span class=""lblFieldText"">" & drw.Item("asplan_qty") & "</span></td>")
                strTableItemList.Append("<td style=""border-color:Black;border-width:1px;border-style:solid;width:80px;text-align:center;""><span class=""lblFieldText"">" & drw.Item("asbuilt_qty") & "</span></td>")
                strTableItemList.Append("<td style=""border-color:Black;border-width:1px;border-style:solid;width:80px;text-align:center;""><span class=""lblFieldText"">" & Integer.Parse(drw.Item("asplan_qty")) - Integer.Parse(drw.Item("asbuilt_qty")) & "</span></td>")
                Dim strSerialNo As String = Convert.ToString(drw.Item("serial_no"))
                Dim detailserialno As String = String.Empty
                If Not String.IsNullOrEmpty(strSerialNo) Then
                    If Not strSerialNo.ToLower().Equals("na") Then
                        Dim getlistserialno As List(Of SerialNoInfo) = JsonConvert.DeserializeObject(Of List(Of SerialNoInfo))(strSerialNo)
                        If getlistserialno.Count > 0 Then
                            Dim rowcount As Integer = 0
                            For Each getdetail In getlistserialno
                                If getdetail.SerialNumber.ToLower() <> "na" Then

                                    If rowcount > 0 Then
                                        detailserialno += ", "
                                    End If
                                    detailserialno += getdetail.SerialNumber
                                    rowcount += 1
                                End If
                            Next
                        End If
                    End If
                End If
                strTableItemList.Append("<td style=""border-color:Black;border-width:1px;border-style:solid;width:80px;text-align:center;""><span class=""lblFieldText"">" & detailserialno & "</span></td>")
                strTableItemList.Append("</tr>")
            Next
            strTableItemList.Append("</table>")
            ltrBOQListSVC.Text = strTableItemList.ToString()
        End If
    End Sub

    Private Sub BindBOQEQP(ByVal ssno As Int32)
        Dim dtResult As DataTable = bornintegration.BORN_GetSubPODetail(ssno, "EQP")
        If (dtResult.Rows.Count > 0) Then
            For Each drw As DataRow In dtResult.Rows
                lblPONOEqp.InnerText = drw.Item("cpo_no")
            Next
        End If

        Dim dtBOQList As DataTable = bornintegration.BORN_GetBOQListDetail(ssno, "EQP")
        If (dtBOQList.Rows.Count > 0) Then
            Dim strTableItemList As New StringBuilder
            Dim strTableItemListContinue1 As New StringBuilder
            Dim strTableItemListContinue2 As New StringBuilder
            strTableItemList.Append("<table cellspacing=""0"" rules=""all"" border=""1"" id=""GvItemList"" style=""border-color: Black;width:790px;border-collapse:collapse;"">")
            strTableItemList.Append("<tr class=""HeaderGrid2"" style=""font-weight:bold;"">")
            strTableItemList.Append("<td align=""center"" style=""background-color:White;border-color:black;border-style:solid;border-width:1px;""><span class=""lblFieldHeader"">Package</span></td>")
            strTableItemList.Append("<td align=""center"" style=""background-color:White;border-color:black;border-style:solid;border-width:1px;""><span class=""lblFieldHeader"">Part Number</span></td>")
            strTableItemList.Append("<td align=""center"" style=""background-color:White;border-color:black;border-style:solid;border-width:1px;""><span class=""lblFieldHeader"">Description</span></td>")
            strTableItemList.Append("<td align=""center"" style=""background-color:White;border-color:black;border-style:solid;border-width:1px;""><span class=""lblFieldHeader"">UOM</span></td>")
            strTableItemList.Append("<td align=""center"" style=""background-color:White;border-color:black;border-style:solid;border-width:1px;""><span class=""lblFieldHeader"">As Plan QTY</span></td>")
            strTableItemList.Append("<td align=""center"" style=""background-color:White;border-color:black;border-style:solid;border-width:1px;""><span class=""lblFieldHeader"">Final QTY</span></td>")
            strTableItemList.Append("<td align=""center"" style=""background-color:White;border-color:black;border-style:solid;border-width:1px;""><span class=""lblFieldHeader"">Delta QTY</span></td>")
            strTableItemList.Append("<td align=""center"" style=""background-color:White;border-color:black;border-style:solid;border-width:1px;""><span class=""lblFieldHeader"">Serial No.</span></td>")
            strTableItemList.Append("</tr>")
            For Each drw As DataRow In dtBOQList.Rows
                strTableItemList.Append("<tr class=""oddGrid2"">")
                strTableItemList.Append("<td style=""border-color:Black;border-width:1px;border-style:solid;width:100px;""><span class=""lblFieldText"">" & drw.Item("package_name") & "</span></td>")
                strTableItemList.Append("<td style=""border-color:Black;border-width:1px;border-style:solid;width:80px;""><span class=""lblFieldText"">" & drw.Item("material_code") & "</span></td>")
                strTableItemList.Append("<td style=""border-color:Black;border-width:1px;border-style:solid;width:150px;""><span class=""lblFieldText"">" & drw.Item("material_desc") & "</span></td>")
                strTableItemList.Append("<td style=""border-color:Black;border-width:1px;border-style:solid;width:60px;""><span class=""lblFieldText"">" & drw.Item("UOM") & "</span></td>")
                strTableItemList.Append("<td style=""border-color:Black;border-width:1px;border-style:solid;width:80px;text-align:center;""><span class=""lblFieldText"">" & drw.Item("asplan_qty") & "</span></td>")
                strTableItemList.Append("<td style=""border-color:Black;border-width:1px;border-style:solid;width:80px;text-align:center;""><span class=""lblFieldText"">" & drw.Item("asbuilt_qty") & "</span></td>")
                strTableItemList.Append("<td style=""border-color:Black;border-width:1px;border-style:solid;width:80px;text-align:center;""><span class=""lblFieldText"">" & Integer.Parse(drw.Item("asplan_qty")) - Integer.Parse(drw.Item("asbuilt_qty")) & "</span></td>")
                Dim strSerialNo As String = Convert.ToString(drw.Item("serial_no"))
                Dim detailserialno As String = String.Empty
                If Not String.IsNullOrEmpty(strSerialNo) Then
                    If Not strSerialNo.ToLower().Equals("na") Then
                        Dim getlistserialno As List(Of SerialNoInfo) = JsonConvert.DeserializeObject(Of List(Of SerialNoInfo))(strSerialNo)
                        If getlistserialno.Count > 0 Then
                            Dim rowcount As Integer = 0
                            For Each getdetail In getlistserialno
                                If getdetail.SerialNumber.ToLower() <> "na" Then

                                    If rowcount > 0 Then
                                        detailserialno += ", "
                                    End If
                                    detailserialno += getdetail.SerialNumber
                                    rowcount += 1
                                End If
                            Next
                        End If
                    End If
                End If
                strTableItemList.Append("<td style=""border-color:Black;border-width:1px;border-style:solid;width:80px;text-align:center;""><span class=""lblFieldText"">" & detailserialno & "</span></td>")
                strTableItemList.Append("</tr>")
            Next
            strTableItemList.Append("</table>")
            ltrBOQListEqp.Text = strTableItemList.ToString()
        End If
    End Sub


    Private Function GetWPID() As String
        If Not String.IsNullOrEmpty(Request.QueryString("wpid")) Then
            Return Request.QueryString("wpid")
        Else
            Return "0"
        End If
    End Function

    Private Function GetSubmissionSNO() As Int32
        If Not String.IsNullOrEmpty(Request.QueryString("ssno")) Then
            Return Request.QueryString("ssno")
        Else
            Return 0
        End If
    End Function

    Private Function GetDocID() As Integer
        If Not String.IsNullOrEmpty(Request.QueryString("docid")) Then
            Return Request.QueryString("docid")
        Else
            Return 0
        End If
    End Function

    Private Function GetSWID() As Int32
        If String.IsNullOrEmpty(Request.QueryString("swid")) Then
            Return 0
        Else
            Return Convert.ToInt32(Request.QueryString("swid"))
        End If
    End Function

    Private Sub uploaddocument(ByVal vers As Integer, ByVal keyval As Integer)
        Dim dt As DataTable = objBO.getbautdocdetailsNEW(GetDocID()) '(Constants._Doc_SSR)
        Dim sec As String = dt.Rows(0).Item("sec_name").ToString
        Dim subsec As String = dt.Rows(0).Item("subsec_name").ToString
        Dim secpath As String = ""
        Dim ft As String = ""
        Dim path As String = ""
        Dim filenameorg As String
        Dim FileNameOnly As String = "-" & hdnDocName.Value.Replace(" ", "") & "-"
        filenameorg = divSiteID.InnerText & FileNameOnly & Format(CDate(DateTime.Now), "ddMMyyyyHHss")
        Dim ReFileName As String = filenameorg & ".htm"
        secpath = IIf(subsec = "" Or subsec = "0", sec & "\", sec & "\" & subsec & "\")
        ft = ConfigurationManager.AppSettings("Type") & hdnScope.Value & "-" & GetWPID() & "\"
        path = ConfigurationManager.AppSettings("Fpath") & hdnSiteNo.Value & ft & secpath
        'Dim strResult As String = DOInsertTrans(hdnsiteid.Value, hdndocid.Value, vers, path)
        'Dim strResult As String = "1"
        Dim DocPath As String = ""
        DocPath = hdnSiteNo.Value & ft & secpath & CreatePDFFile(Replace(path, "  \", "\"), hdnDocName.Value.Replace(" ", ""))
        'For offline testing mode
        'DocPath = lblSiteID.Text & ft & secpath

        Response.Write(DocPath)
        With objET1
            .SiteID = hdnSiteid.Value
            .DocId = GetDocID()
            .IsUploded = 1
            .Version = vers
            .keyval = keyval
            .DocPath = DocPath
            .AT.RStatus = Constants.STATUS_ACTIVE
            .AT.LMBY = Session("User_Id")
            .orgDocPath = DocPath 'hdnSiteno.Value & ft & secpath & ReFileName
            .PONo = hdnPONO.Value
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
        objET.PoNo = hdnPONO.Value
        objET.SiteId = hdnSiteid.Value
        objET.DocId = GetDocID()
        objET.Version = hdnVersion.Value
        objET.Task = "1"
        objET.Status = "1"
        objET.Userid = Session("User_Id")
        objET.Roleid = Session("Role_Id")
        objET.fldtype = hdnScope.Value
        DBUtils_NSN.InsertAuditTrailNew(objET, GetWPID())
    End Sub

    Function CreatePDFFile(ByVal StrPath As String, ByVal docname As String) As String
        StrPath = StrPath.Replace(" \", "\").ToString 'bugfix100816 causing error when the path containing space before \
        Dim dtba As New DataTable
        Dim filenameorg As String
        Dim ReFileName As String
        filenameorg = hdnSiteNo.Value & "-" & docname & "-" & Format(CDate(DateTime.Now), "ddMMyyyyHHss")
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
        sw.WriteLine(".lblBText{font-family: verdana;font-size: 8pt;color: #000000;text-align: left;font-weight: bold;}")
        sw.WriteLine(".lblBold{font-family: verdana;font-size: 12pt;color: #000000;font-weight: bold;}")
        sw.WriteLine(".lblFieldHeader{font-family: verdana;font-size: 10px;color: #000000;font-weight: bold;}")
        sw.WriteLine(".lblFieldText{font-family: verdana;font-size: 9px;color: #000000;}")
        sw.WriteLine(".textFieldStyle{background-color: white;border: 1px solid;color: black;font-family: verdana;font-size: 9pt;}")
        sw.WriteLine(".GridHeader{color: #0e1b42;background-color: Orange;font-size: 9pt;font-family: Verdana;text-align: Left;vertical-align: bottom;font-weight: bold;}")
        sw.WriteLine(".GridEvenRows{background-color: #e3e3e3;vertical-align: top;font-family: verdana;font-size: 8pt;color: #000000;}")
        sw.WriteLine(".GridOddRows{background-color: white;vertical-align: top;font-family: verdana;font-size: 8pt;color: #000000;}")
        sw.WriteLine(".PagerTitle{font-size: 8pt;background-color: #cddbbf;text-align: Right;vertical-align: middle;color: #25375b;font-weight: bold;}")
        sw.WriteLine(".Hcap{height: 5px;}")
        sw.WriteLine(".VCap{width: 10px;}")
        sw.WriteLine(".PageBreak{page-break-before: always;}")
        sw.WriteLine("#dvPrintApproval{width:800px;height:700px;margin-top:20px;}")
        sw.WriteLine("#dvPrintPOSVC{width:800px;height:700px;margin-top:20px;}")
        sw.WriteLine("#dvPrintPOEqp{width:800px;height:700px;margin-top:20px;}")
        sw.WriteLine("</style></head>")
        sw.WriteLine("<body class=""MainCSS"">")
        dvPrintApproval.RenderControl(sw)
        sw.WriteLine("<div class=""PageBreak""></div>")
        dvPrintPOSVC.RenderControl(sw)
        sw.WriteLine("<div class=""PageBreak""></div>")
        dvPrintPOEqp.RenderControl(sw)
        sw.WriteLine("</body>")
        sw.WriteLine("</html>")
        sw.Close()
        sw.Dispose()
        Return EBASTFileUpload.ConvertAnyFormatToPDFHtmlNew(StrPath & ReFileName, StrPath, filenameorg)
    End Function


#End Region

End Class
