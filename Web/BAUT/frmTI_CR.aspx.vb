Imports Common
Imports BusinessLogic
Imports System.Data
Imports CRFramework
Imports System.IO
Imports System.Collections.Generic

Partial Class BAUT_frmTI_CR
    Inherits System.Web.UI.Page

    Dim objdb As New DBUtil
    Dim objbl As New BODDLs
    Dim controller As New CRController
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        If Not Page.IsPostBack Then
            If Not String.IsNullOrEmpty(Request.QueryString("id")) Then
                UpdateCRNO(Convert.ToInt32(Request.QueryString("id")), Request.QueryString("wpid"))
                BindOldData(Convert.ToInt32(Request.QueryString("id")))
                BindSiteAtt()
                BindInitiator()
            End If
            'ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScriptdd", "getControlPosition();", True)
        End If
    End Sub

    Private Sub UpdateCRNO(ByVal crid As Int32, ByVal wpid As String)
        Dim crNo As String = controller.GetCRNo(wpid, crid)
        If Not String.IsNullOrEmpty(crNo) Then
            controller.UpdateCRNo(crid, crNo)
        End If
    End Sub


    Protected Sub LbtEditCRClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles LbtEditCR.Click
        Response.Redirect("~/CR/frmNewChangeRequest.aspx?id=" & Request.QueryString("id") & "&wpid=" & Request.QueryString("wpid"))
    End Sub

    Protected Sub LbtGeneratePDFFormClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbtGeneratePDFForm.Click
        If Request.Browser.Browser = "IE" Then
            dvGeneratePanel.Visible = False
            GeneratePDFForm()
        Else
            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('Please use Internet Explorer');", True)
        End If
        
    End Sub


#Region "custom methods"
    Private Sub BindSiteAtt()
        If Not String.IsNullOrEmpty(Request.QueryString("wpid")) Then
            Dim strQuery As String = "select top 1 siteNo, sitename,PoNo,poname,RGNName,TselProjectID from poepmsitenew where workpackageid='" & Request.QueryString("wpid") & "'"
            Dim dtSiteAtt As DataTable = objdb.ExeQueryDT(strQuery, "siteAtt")
            If dtSiteAtt.Rows.Count > 0 Then
                LblSiteID.Text = dtSiteAtt.Rows(0).Item(0).ToString()
                LblSiteName.Text = dtSiteAtt.Rows(0).Item(1).ToString()
                LblArea.Text = dtSiteAtt.Rows(0).Item(4).ToString()
                LblPONo.Text = dtSiteAtt.Rows(0).Item(2).ToString()
                LblEOName.Text = dtSiteAtt.Rows(0).Item(3).ToString()
                LblProjectType.Text = "2G"
                LblProjectID.Text = dtSiteAtt.Rows(0).Item(5).ToString()
                LblDateSubmitted.Text = String.Format("{0:dd-MMM-yyy}", DateTime.Now)
                LblProjectCategory.Text = "TI"
            End If
        End If
    End Sub

    Private Sub BindInitiator()
        Dim strQuery As String = "select top 1 name, usrType,usrRole from ebastusers_1 where usr_id=" & CommonSite.UserId
        Dim dtInitiator As DataTable = objdb.ExeQueryDT(strQuery, "init")
        If dtInitiator.Rows.Count > 0 Then
            LblInitiatorName.Text = dtInitiator.Rows(0).Item(0)
            LblInitiatorArea.Text = LblArea.Text
            If dtInitiator.Rows(0).Item(1).ToString.ToLower().Equals("n") Then
                LblInitiatorDepartment.Text = "Nokia Siemens Networks"
            Else
                LblInitiatorDepartment.Text = "Telkomsel"
            End If
        End If
    End Sub

    Private Sub BindOldData(ByVal crid As Int32)
        Dim info As CRInfo = controller.GetCRDetail(crid)
        LblDescriptionofChange.Text = info.DescriptionofChange
        ChkRegulatoryRequirement.Checked = info.IsRegulatoryRequirement
        ChkSiteCondition.Checked = info.IsSiteCondition
        ChkDesignChange.Checked = info.IsDesignChange
        ChkTechnicalError.Checked = info.IsTechnicalError
        ChkOther.Checked = info.IsOther
        LblJustificationComments.Text = info.JustificationComments
        ChkDesignImpact.Checked = info.IsDesignImpact
        ChkBudgetImpact.Checked = info.IsBudgetImpact
        chkNoImpact.Checked = info.IsNoImpact
        LblContractUSD.Text = String.Format("{0:###,##.#0}", info.ContractUSD)
        LblContractIDR.Text = String.Format("{0:###,##.#0}", info.ContractIDR)
        LblImplementationUSD.Text = String.Format("{0:###,##.#0}", info.ImplementationUSD)
        LblImplementationIDR.Text = String.Format("{0:###,##.#0}", info.ImplementationIDR)
        LblIndicativePriceCostUSD.Text = String.Format("{0:###,##.#0}", info.IndicativePriceCostUSD)
        LblIndicativePriceCostIDR.Text = String.Format("{0:###,##.#0}", info.IndicativePriceCostIDR)
        LblPercentagePriceChangeUSD.Text = Convert.ToDecimal(info.PercentagePriceUSD)
        LblPercentagePriceChangeIDR.Text = Convert.ToDecimal(info.PercentagePriceIDR)
        LblScheduleImpacts.Text = info.ScheduleImpact
        LblOtherImpacts.Text = info.OtherImpact
        BindTselSignature(4, Request.QueryString("wpid"), info.WFID)
        BindNSNSignature(1, Request.QueryString("wpid"), info.WFID)
        hdnWFID.Value = info.WFID
    End Sub

    Private Sub BindTselSignature(ByVal grpid As Integer, ByVal packageid As String, ByVal wfid As Integer)
        RptDigitalSignTelkomsel.DataSource = controller.GetUserApproverByGrouping(packageid, grpid, wfid)
        RptDigitalSignTelkomsel.DataBind()
    End Sub

    Private Sub BindNSNSignature(ByVal grpid As Integer, ByVal packageid As String, ByVal wfid As Integer)
        RptDigitalSignNSN.DataSource = controller.GetUserApproverByGrouping(packageid, grpid, wfid)
        RptDigitalSignNSN.DataBind()
    End Sub

    Private Sub GeneratePDFForm()
        If Not (String.IsNullOrEmpty(hdnWFID.Value)) Then

            If Integer.Parse(hdnWFID.Value) > 0 Then
                LblErrorMessage.Visible = False
                Try
                    Dim dtWFdef As DataTable = controller.GetWorkflowDetail(Integer.Parse(hdnWFID.Value))
                    Dim FileNameOnly As String
                    Dim Filenameorg As String
                    Dim ReFilename As String
                    Dim secpath As String = ""
                    Dim ft As String
                    Dim path As String
                    FileNameOnly = "-CR-"
                    Filenameorg = LblSiteID.Text & FileNameOnly & Format(CDate(DateTime.Now), "ddMMyyyyHHss")
                    ReFilename = Filenameorg & ".htm"
                    ft = ConfigurationManager.AppSettings("Type") & LblPONo.Text & "-" & Request.QueryString("wpid") & "\" & "CR\"
                    path = ConfigurationManager.AppSettings("Fpath") & LblSiteID.Text & ft
                    'Dim strResult As String = DOInsertTrans(hdnsiteid.Value, hdndocId.Value, vers, path)

                    Dim DocPath As String = ""
                    Dim orgDocPath As String = ""
                    DocPath = LblSiteID.Text & ft & secpath & CreatePDFFile(Replace(path, "  \", "\"))
                    ''For offline testing
                    'DocPath = LblSiteID.Text & ft & secpath
                    orgDocPath = LblSiteID.Text & ft & secpath & ReFilename
                    If (controller.UpdateCRDocPath(Convert.ToInt32(Request.QueryString("id")), DocPath, orgDocPath) = True) Then
                        If dtWFdef.Rows.Count > 0 Then
                            Dim aa As Integer = 0
                            Dim sorder As Integer = 0
                            Dim getDate As DateTime = DateTime.Now
                            controller.DeleteCRWorkflow(Convert.ToInt32(Request.QueryString("id")))
                            For aa = 0 To dtWFdef.Rows.Count - 1
                                sorder = dtWFdef.Rows(aa).Item("sorder")
                                Dim info As New CRWFTransactionInfo
                                info.CRID = Convert.ToInt32(Request.QueryString("id"))
                                info.TSKID = dtWFdef.Rows(aa).Item("Tsk_id")
                                info.UGPID = dtWFdef.Rows(aa).Item("GrpId")
                                info.RoleId = dtWFdef.Rows(aa).Item("RoleId")
                                info.PageNo = 1
                                info.LMBY = CommonSite.UserId
                                info.LMDT = DateTime.Now()
                                info.Status = 1

                                If sorder = 1 Then
                                    info.StartTime = getDate
                                    info.EndTime = getDate
                                    info.Status = 0
                                Else
                                    If sorder = 2 Then
                                        info.StartTime = getDate
                                    End If
                                End If
                                info.XVal = 0
                                info.YVal = 0
                                info.WFID = Integer.Parse(hdnWFID.Value)
                                info.RStatus = 2
                                controller.InsertCRTransaction(info)
                            Next
                            CRAuditTrail(Convert.ToInt32(Request.QueryString("id")), getDate)
                            ConfigureXYCordinate(Convert.ToInt32(Request.QueryString("id")), Integer.Parse(hdnWFID.Value))
                            HistoricalRejectionLogUpdate(Convert.ToInt32(Request.QueryString("id")), Request.QueryString("wpid"))
                        End If
                    End If
                    ScriptManager.RegisterStartupScript(Page, GetType(Panel), "MyScriptWhh", "alert('CR Form Successfully Generating to PDF Form');", True)
                    Response.Redirect("../CR/frmListCR.aspx?wpid=" & Request.QueryString("wpid"))
                Catch ex As Exception
                    LblErrorMessage.Visible = True
                    LblErrorMessage.Text = ex.Message.ToString()
                    ScriptManager.RegisterStartupScript(Page, GetType(Panel), "MyScript", "alert('Error :" & ex.Message.ToString() & "');", True)
                End Try
            End If
        End If
    End Sub

    Private Sub ConfigureXYCordinate(ByVal crid As Int32, ByVal wfid As Integer)
        Dim listtwfNSNApprover As List(Of TWFDefinitionInfo) = controller.GetRoleTaskInTwfDefinition(wfid, "Approver", 1)
        Dim listtwfTselApprover As List(Of TWFDefinitionInfo) = controller.GetRoleTaskInTwfDefinition(wfid, "Approver", 4)
        Dim xVal As Integer = 475
        Dim yValTsel As Integer = 204
        Dim yValNSN As Integer = yValTsel - 30 - (listtwfTselApprover.Count * 40)
        Dim twfNSNApprover As New TWFDefinitionInfo
        Dim count As Integer = 0
        If listtwfNSNApprover.Count > 0 Then
            For Each twfNSNApprover In listtwfNSNApprover
                If count > 0 Then
                    yValNSN -= 38
                End If
                Dim sno As Int32 = controller.GetSNOByRoleTaskCRID(twfNSNApprover.RoleId, twfNSNApprover.TskId, crid)
                If sno > 0 Then
                    controller.UpdateCRCoordinate(sno, xVal, yValNSN)
                End If
                count += 1
            Next
        End If

        Dim twfTselApprover As New TWFDefinitionInfo
        count = 0
        If listtwfTselApprover.Count > 0 Then
            For Each twfTselApprover In listtwfTselApprover
                If count > 0 Then
                    yValTsel -= 38
                End If
                Dim sno As Int32 = controller.GetSNOByRoleTaskCRID(twfTselApprover.RoleId, twfTselApprover.TskId, crid)
                If sno > 0 Then
                    controller.UpdateCRCoordinate(sno, xVal, yValTsel)
                End If
                count += 1
            Next
        End If
    End Sub

    Private Sub CRAuditTrail(ByVal crid As Int32, ByVal startdatetime As DateTime)
        Dim info As New CRLogInfo
        info.CR_ID = crid
        info.EventStartTime = startdatetime
        info.EventEndTime = startdatetime
        info.Task = 1
        info.Userid = CommonSite.UserId
        info.RoleId = CommonSite.RollId
        info.Remarks = String.Empty
        info.Categories = String.Empty
        info.SubDocId = 0
        controller.InsertNewCRLog(info)
    End Sub

    Private Sub HistoricalRejectionLogUpdate(ByVal crid As Int32, ByVal packageid As String)
        controller.HistoricalRejectionUpdate(CommonSite.UserId, crid, "CR", packageid)
    End Sub

    Function CreatePDFFile(ByVal StrPath As String) As String
        StrPath = StrPath.Replace(" \", "\").ToString 'bugfix100816 causing error when the path containing space before \
        Dim dtba As New DataTable
        Dim filenameorg As String
        Dim ReFileName As String
        'filenameorg = hdnSiteno.Value & "-BAST-" & Format(CDate(DateTime.Now), "ddMMyyyyHHss")
        filenameorg = LblSiteID.Text & "-CR-" & Format(CDate(DateTime.Now), "ddMMyyyyHHss")
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
        sw.WriteLine(".lblBText{font-family: verdana;font-size: 6.5pt;color: #000000;text-align: left;font-weight: bold;}")
        sw.WriteLine(".lblBTextPrice{font-family: verdana;font-size: 6.5pt;color: #000000;text-align: left;font-weight: bold;}")
        sw.WriteLine(".lblBold{font-family: verdana;font-size: 12pt;color: #000000;font-weight: bold;}")
        sw.WriteLine(".subpanelheader{text-align: left;}")
        sw.WriteLine(".lblSubHeader{font-family:Arial Unicode MS;font-size:8pt;font-weight:bolder;color:white;}")
        sw.WriteLine(".clearSpace{height:40px;}")
        sw.WriteLine(".lblTextC{font-family: verdana;font-size: 6.5pt;color: #000000;text-align: left;}")
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
        sw.WriteLine(".signPanel{height:300px;}")
        sw.WriteLine("</style></head>")
        sw.WriteLine("<body class=""MainCSS"">")
        dvPrint.RenderControl(sw)
        sw.WriteLine("</body>")
        sw.WriteLine("</html>")
        sw.Close()
        sw.Dispose()
        Return Common.TakeFileUpload.ConvertAnyFormatToPDFHtmlNew(StrPath & ReFileName, StrPath, filenameorg)
    End Function

#End Region


End Class
