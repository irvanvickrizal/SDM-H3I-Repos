Imports CRFramework
Imports System.Data
Imports Common
Imports BusinessLogic
Imports System.Collections.Generic
Imports System.IO

Partial Class BAUT_frmTI_CO
    Inherits System.Web.UI.Page

    Dim controller As New CRController
    Dim co_controller As New COController
    Dim objdb As New DBUtil
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        If Not Page.IsPostBack Then
            BindData()
        End If
    End Sub

    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        Return
    End Sub

    Protected Sub gvDetails_RowCreated(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles gvDetails.RowCreated
        If e.Row.RowType = DataControlRowType.Header Then
            'Creating a gridview object            
            Dim objGridView As GridView = CType(sender, GridView)

            'Creating a gridview row object
            Dim objgridviewrow As GridViewRow = New GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert)

            'Creating a table cell object
            Dim objtablecell As New TableCell

            AddMergedCells(objgridviewrow, objtablecell, 1, "", System.Drawing.Color.White.Name)
            AddMergedCells(objgridviewrow, objtablecell, 2, "Contract", System.Drawing.Color.White.Name)
            AddMergedCells(objgridviewrow, objtablecell, 2, "Change Request", System.Drawing.Color.White.Name)
            'Lastly add the gridrow object to the gridview object at the 0th position
            'Because, the header row position is 0.
            objGridView.Controls(0).Controls.AddAt(0, objgridviewrow)
        End If
    End Sub

    Protected Sub GvBudgetImpact_RowCreated(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles GvBudgetImpact.RowCreated
        If e.Row.RowType = DataControlRowType.Header Then
            'Creating a gridview object            
            Dim objGridView As GridView = CType(sender, GridView)

            'Creating a gridview row object
            Dim objgridviewrow As GridViewRow = New GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert)
            'Creating a table cell object
            Dim objtablecell As New TableCell
            AddMergedCells(objgridviewrow, objtablecell, 2, "Description", System.Drawing.Color.White.Name)
            AddMergedCells(objgridviewrow, objtablecell, 2, "Contract", System.Drawing.Color.White.Name)
            AddMergedCells(objgridviewrow, objtablecell, 2, "Change Request", System.Drawing.Color.White.Name)
            AddMergedCells(objgridviewrow, objtablecell, 2, "Delta", System.Drawing.Color.White.Name)
            'Lastly add the gridrow object to the gridview object at the 0th position
            'Because, the header row position is 0.
            objGridView.Controls(0).Controls.AddAt(0, objgridviewrow)
        End If
    End Sub

    Protected Sub GvBudgetImpact_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.Footer Then
            Dim i As Integer
            Dim totalContractUSD As Double = 0
            Dim totalContractIDR As Double = 0
            Dim totalCRUSD As Double = 0
            Dim totalCRIDR As Double = 0
            For i = 0 To GvBudgetImpact.Rows.Count - 1
                Dim LblContractUSD As New Label
                Dim LblContractIDR As New Label
                Dim LblCRUSD As New Label
                Dim LblCRIDR As New Label
                LblContractUSD = CType(GvBudgetImpact.Rows(i).Cells(0).FindControl("LblContractUSDHIdden"), Label)
                LblContractIDR = CType(GvBudgetImpact.Rows(i).Cells(0).FindControl("LblContractIDRHidden"), Label)
                LblCRUSD = CType(GvBudgetImpact.Rows(i).Cells(0).FindControl("LblCRUSDHidden"), Label)
                LblCRIDR = CType(GvBudgetImpact.Rows(i).Cells(0).FindControl("LblCRIDRHidden"), Label)
                totalContractUSD += Convert.ToDouble(LblContractUSD.Text)
                totalContractIDR += Convert.ToDouble(LblContractIDR.Text)
                totalCRUSD += Convert.ToDouble(LblCRUSD.Text)
                totalCRIDR += Convert.ToDouble(LblCRIDR.Text)
            Next
            e.Row.Cells.Clear()
            Dim oCell As New TableCell
            oCell.ColumnSpan = 2
            oCell.Style.Add("text-align", "center")
            oCell.BorderColor = Drawing.Color.Black
            oCell.BorderStyle = BorderStyle.Solid
            oCell.BorderWidth = 1
            oCell.Text = "<span class='lblBText'>Total</span></td><td align=right class='borderConfig'><span class='lblBText'>" & String.Format("{0:###,##.#0}", totalContractUSD) & "</span></td><td align=right class='borderConfig'><span class='lblBText'>" & String.Format("{0:###,##.#0}", totalContractIDR) & "</span></td><td align=right class='borderConfig'><span class='lblBText'>" & _
                         String.Format("{0:###,##.#0}", totalCRUSD) & "</span></td><td align=right class='borderConfig'><span class='lblBText'>" & String.Format("{0:###,##.#0}", totalCRIDR) & "</span></td><td align=right class='borderConfig'><span class='lblBText'>" & String.Format("{0:###,##.#0}", (totalCRUSD - totalContractUSD)) & _
                         "</span></td><td align=right class='borderConfig'><span class='lblBText'>" & String.Format("{0:###,##.#0}", (totalCRIDR - totalContractIDR)) & "</span></td>"
            e.Row.Cells.Add(oCell)
        End If
    End Sub

    Protected Sub LbtGeneratePDF_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbtGeneratePDFForm.Click
        If Request.Browser.Browser = "IE" Then
            dvGeneratePanel.Visible = False
            GeneratePDFForm()
        Else
            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('Please use Internet Explorer');", True)
        End If
    End Sub

    Protected Sub LbtEditCR_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LbtEditCR.Click
        Response.Redirect("../CO/frmCO.aspx?wpid=" & HdnPackageId.Value)
    End Sub

#Region "custom methods"
    Private Sub BindData()
        Dim info As COInfo = GetCODetail(GetCOId())
        HdnPackageId.Value = info.PackageId
        HdnWFID.Value = info.WFID
        ChkRegulatoryRequirement.Checked = info.IsRegulatoryRequirement
        ChkSiteCondition.Checked = info.IsSiteCondition
        ChkDesignChange.Checked = info.IsDesignChange
        ChkTechnicalError.Checked = info.IsTechnicalErrorOmission
        ChkOther.Checked = info.IsOther
        Description_JustificationComments.Text = info.DescriptionComment
        TechnicalImpact.Text = info.technicalImpact
        TxtTechnicalImpact.Text = info.technicalImpact
        ScheduleImpacts.Text = info.ScheduleImpact
        TxtScheduleImpacts.Text = info.ScheduleImpact
        OtherImpact.Text = info.OtherImpact
        TxtOtherImpact.Text = info.OtherImpact
        BindSiteAtt(HdnPackageId.Value)
        BindInitiator()
        BindDescriptionofChange(HdnPackageId.Value)
        BindCOBudget(HdnPackageId.Value)
        BindTselSignature(4, HdnPackageId.Value, HdnWFID.Value)
        BindNSNSignature(1, HdnPackageId.Value, HdnWFID.Value)
    End Sub

    Private Sub BindSiteAtt(ByVal packageid As String)
        If Not String.IsNullOrEmpty(packageid) Then
            Dim strQuery As String = "select top 1 siteNo, sitename,PoNo,poname,RGNName,TselProjectID,SiteId,SiteVersion from poepmsitenew where workpackageid='" & packageid & "'"
            Dim dtSiteAtt As DataTable = objdb.ExeQueryDT(strQuery, "siteAtt")
            If dtSiteAtt.Rows.Count > 0 Then
                LblSiteID.Text = dtSiteAtt.Rows(0).Item(0).ToString()
                LblSiteName.Text = dtSiteAtt.Rows(0).Item(1).ToString()
                LblArea.Text = dtSiteAtt.Rows(0).Item(4).ToString()
                LblPONo.Text = dtSiteAtt.Rows(0).Item(2).ToString()
                LblEOName.Text = dtSiteAtt.Rows(0).Item(3).ToString()
                LblProjectType.Text = "2G"
                LblProjectID.Text = dtSiteAtt.Rows(0).Item(5).ToString()
                LblDateSubmitted.Text = String.Format("{0:dd-MMM-yyyy}", DateTime.Now)
                LblProjectCategory.Text = "TI"
                HdnSiteId.Value = dtSiteAtt.Rows(0).Item(6).ToString()
                HdnVersion.Value = dtSiteAtt.Rows(0).Item(7).ToString()
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

    Private Sub BindDescriptionofChange(ByVal packageid As String)
        Dim ds As DataSet = co_controller.GetCOListDesc_DS(packageid)
        If (ds.Tables(0).Rows.Count > 0) Then
            gvDetails.DataSource = ds
            gvDetails.DataBind()
        End If
    End Sub

    Private Sub BindCOBudget(ByVal packageid As String)
        'Dim ds As DataSet = co_controller.GetCOBudget_DS(packageid)
        'If (ds.Tables(0).Rows.Count > 0) Then
        '    GvBudgetImpact.DataSource = ds
        '    GvBudgetImpact.DataBind()
        'End If
        GvBudgetImpact.DataSource = co_controller.GetCOBudgetList(packageid)
        GvBudgetImpact.DataBind()

    End Sub

    Public Function GetDetailTotal(ByVal getTotal As String) As String
        Dim i As Integer
        Dim totalContractUSD As Double = 0
        Dim totalContractIDR As Double = 0
        Dim totalCRUSD As Double = 0
        Dim totalCRIDR As Double = 0
        For i = 0 To GvBudgetImpact.Rows.Count - 1
            Dim LblContractUSD As New Label
            Dim LblContractIDR As New Label
            Dim LblCRUSD As New Label
            Dim LblCRIDR As New Label
            LblContractUSD = CType(GvBudgetImpact.Rows(i).Cells(0).FindControl("LblContractUSD"), Label)
            LblContractIDR = CType(GvBudgetImpact.Rows(i).Cells(0).FindControl("LblContractIDR"), Label)
            LblCRUSD = CType(GvBudgetImpact.Rows(i).Cells(0).FindControl("LblCRUSD"), Label)
            LblCRIDR = CType(GvBudgetImpact.Rows(i).Cells(0).FindControl("LblCRIDR"), Label)
            totalContractUSD += Convert.ToDouble(LblContractUSD.Text)
            totalContractIDR += Convert.ToDouble(LblContractIDR.Text)
            totalCRUSD += Convert.ToDouble(LblCRUSD.Text)
            totalCRIDR += Convert.ToDouble(LblCRIDR.Text)
        Next
        If getTotal.Equals("totalContractUSD") Then
            Return String.Format("{0:###,##.#0}", totalContractUSD)
        ElseIf getTotal.Equals("totalContractIDR") Then
            Return String.Format("{0:###,##.#0}", totalContractIDR)
        ElseIf getTotal.Equals("totalCRUSD") Then
            Return String.Format("{0:###,##.#0}", totalCRUSD)
        ElseIf getTotal.Equals("totalCRIDR") Then
            Return String.Format("{0:###,##.#0}", totalCRIDR)
        ElseIf getTotal.EndsWith("totalDeltaUSD") Then
            Return String.Format("{0:###,##.#0}", totalCRUSD - totalContractUSD)
        ElseIf getTotal.EndsWith("totalDeltaIDR") Then
            Return String.Format("{0:###,##.#0}", totalCRIDR - totalContractIDR)
        Else
            Return String.Format("{0:###,##.#0}", 0)
        End If
        Return "0.00"
    End Function

    Public Function GetDetailPercentage() As COBudgetPercentageInfo
        Dim info As COBudgetPercentageInfo = co_controller.GetPercentageChangeCO(HdnPackageId.Value)
        Return info
    End Function

    Private Sub BindTselSignature(ByVal grpid As Integer, ByVal packageid As String, ByVal wfid As Integer)
        RptDigitalSignTelkomsel.DataSource = controller.GetUserApproverByGrouping(packageid, grpid, wfid)
        RptDigitalSignTelkomsel.DataBind()
    End Sub

    Private Sub BindNSNSignature(ByVal grpid As Integer, ByVal packageid As String, ByVal wfid As Integer)
        RptDigitalSignNSN.DataSource = controller.GetUserApproverByGrouping(packageid, grpid, wfid)
        RptDigitalSignNSN.DataBind()
    End Sub

    Private Sub GeneratePDFForm()
        If Not (String.IsNullOrEmpty(HdnWFID.Value)) Then

            If Integer.Parse(HdnWFID.Value) > 0 Then
                LblErrorMessage.Visible = False
                Try
                    Dim dtWFdef As DataTable = controller.GetWorkflowDetail(Integer.Parse(HdnWFID.Value))
                    Dim FileNameOnly As String
                    Dim Filenameorg As String
                    Dim ReFilename As String
                    Dim secpath As String = ""
                    Dim ft As String
                    Dim path As String
                    FileNameOnly = "-CO-"
                    Filenameorg = LblSiteID.Text & FileNameOnly & Format(CDate(DateTime.Now), "ddMMyyyyHHss")
                    ReFilename = Filenameorg & ".htm"
                    ft = ConfigurationManager.AppSettings("Type") & LblPONo.Text & "-" & Request.QueryString("wpid") & "\" & "CO\"
                    path = ConfigurationManager.AppSettings("Fpath") & LblSiteID.Text & ft
                    'Dim strResult As String = DOInsertTrans(hdnsiteid.Value, hdndocId.Value, vers, path)

                    Dim DocPath As String = ""
                    Dim orgDocPath As String = ""
                    DocPath = LblSiteID.Text & ft & secpath & CreatePDFFile(Replace(path, "  \", "\"))
                    ''Testing Offline Mode
                    'DocPath = LblSiteID.Text & ft & secpath
                    orgDocPath = LblSiteID.Text & ft & secpath & ReFilename
                    'Rstatus is 5 mean CO is approval processing
                    Dim intSucceed As Integer = co_controller.COUpdateDocUpload(Convert.ToInt32(Request.QueryString("id")), DocPath, orgDocPath, Integer.Parse(ConfigurationManager.AppSettings("codocid")), 5, CommonSite.UserId, Integer.Parse(HdnWFID.Value))

                    If (intSucceed = 1) Then
                        If dtWFdef.Rows.Count > 0 Then
                            Dim aa As Integer = 0
                            Dim sorder As Integer = 0
                            Dim getDate As DateTime = DateTime.Now
                            co_controller.DeleteCOTransaction(Integer.Parse(ConfigurationManager.AppSettings("codocid")), Convert.ToInt32(Request.QueryString("id")))
                            For aa = 0 To dtWFdef.Rows.Count - 1
                                sorder = dtWFdef.Rows(aa).Item("sorder")
                                Dim info As New COWFTransactionInfo
                                info.COID = Convert.ToInt32(Request.QueryString("id"))
                                info.DocId = Integer.Parse(ConfigurationManager.AppSettings("codocid"))
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
                                info.WFID = Integer.Parse(HdnWFID.Value)
                                info.RStatus = 2
                                co_controller.InsertCOTransaction(info)
                            Next
                            ConfigureXYCordinate(Convert.ToInt32(Request.QueryString("id")), Integer.Parse(HdnWFID.Value))
                            HistoricalRejectionLogUpdate(Convert.ToInt32(Request.QueryString("id")), HdnPackageId.Value)
                        End If
                        ScriptManager.RegisterStartupScript(Page, GetType(Panel), "MyScriptWhh", "alert('CR Form Successfully Generating to PDF Form');", True)

                        If Not String.IsNullOrEmpty(Request.QueryString("listtype")) Then
                            Response.Redirect("../CR/CRCORejection.aspx?type=co")
                        Else
                            Response.Redirect("../CO/frmCOReadyCreation.aspx")
                        End If

                    Else
                        ScriptManager.RegisterStartupScript(Page, GetType(Panel), "MyScriptWhh", "alert('Error While During Transaction');", True)
                    End If
                    
                Catch ex As Exception
                    LblErrorMessage.Visible = True
                    LblErrorMessage.Text = ex.Message.ToString()
                    ScriptManager.RegisterStartupScript(Page, GetType(Panel), "MyScript", "alert('Error :" & ex.Message.ToString() & "');", True)
                End Try
            End If
        End If
    End Sub

    Function CreatePDFFile(ByVal StrPath As String) As String
        StrPath = StrPath.Replace(" \", "\").ToString 'bugfix100816 causing error when the path containing space before \
        Dim dtba As New DataTable
        Dim filenameorg As String
        Dim ReFileName As String
        'filenameorg = hdnSiteno.Value & "-BAST-" & Format(CDate(DateTime.Now), "ddMMyyyyHHss")
        filenameorg = LblSiteID.Text & "-CO-" & Format(CDate(DateTime.Now), "ddMMyyyyHHss")
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
        sw.WriteLine(".lblSubHeader{font-family:verdana;font-size:8pt;font-weight:bolder;color:white;}")
        sw.WriteLine(".clearSpace{height:40px;}")
        sw.WriteLine(".lblTextC{font-family: verdana;font-size: 6.5pt;color: #000000;text-align: left;}")
        sw.WriteLine(".lblTextC1{font-family: verdana;font-size: 6.5pt;color: #000000;text-align: right;}")
        sw.WriteLine(".textFieldStyle{background-color: white;border: 1px solid;color: black;font-family: verdana;font-size: 6.5pt;text-align:left;}")
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
        sw.WriteLine("#AttributePanel{margin-top:5px;height:135px;}")
        sw.WriteLine(".lblBHText{font-family: Verdana;font-size: 6.5pt;color: #000000;text-align: center;font-weight: bold;}")
        sw.WriteLine(".scheduleImpactField,.otherImpactField{height:20px;text-align:left;margin-top:5px;}")
        sw.WriteLine(".technicalImpactField{height:45px;text-align:left;margin-top:5px;}")
        sw.WriteLine(".descdetailfield{margin-top: 3px; width: 100%; margin-left: 3px;}")
        sw.WriteLine(".budgetImpactField{margin-top:3px;height:150px;}")
        sw.WriteLine(".borderConfig{border-width:1px;border-style:solid;border-color:black;}")
        sw.WriteLine(".seenextpage{margin-left:3px;margin-top:5px;margin-bottom:5px;}")
        sw.WriteLine(".signPanel{height:420px;text-align:left;margin-top:10px;}")
        sw.WriteLine(".clearDiv{height:10px;}")
        sw.WriteLine(".PageBreak {page-break-before: always;}")
        sw.WriteLine("</style></head>")
        sw.WriteLine("<body class=""MainCSS"">")
        dvPrint.RenderControl(sw)
        sw.WriteLine("<div class=""PageBreak""></div>")
        dvPrintDescriptionofChange.RenderControl(sw)
        sw.WriteLine("</body>")
        sw.WriteLine("</html>")
        sw.Close()
        sw.Dispose()
        Return Common.TakeFileUpload.ConvertAnyFormatToPDFHtmlNew(StrPath & ReFileName, StrPath, filenameorg)
    End Function

    Private Sub ConfigureXYCordinate(ByVal crid As Int32, ByVal wfid As Integer)
        Dim listtwfNSNApprover As List(Of TWFDefinitionInfo) = controller.GetRoleTaskInTwfDefinition(wfid, "Approver", 1)
        Dim listtwfTselApprover As List(Of TWFDefinitionInfo) = controller.GetRoleTaskInTwfDefinition(wfid, "Approver", 4)
        Dim xVal As Integer = 480
        Dim yValTsel As Integer = 180
        Dim yValNSN As Integer = yValTsel - 30 - (listtwfTselApprover.Count * 40)
        Dim twfNSNApprover As New TWFDefinitionInfo
        Dim count As Integer = 0
        If listtwfNSNApprover.Count > 0 Then
            For Each twfNSNApprover In listtwfNSNApprover
                If count > 0 Then
                    yValNSN -= 40
                End If
                Dim sno As Int32 = co_controller.GetSNOByRoleTaskCOID(twfNSNApprover.RoleId, twfNSNApprover.TskId, crid, Integer.Parse(ConfigurationManager.AppSettings("codocid")))
                If sno > 0 Then
                    co_controller.UpdateCOCoordinate(sno, xVal, yValNSN)
                End If
                count += 1
            Next
        End If

        Dim twfTselApprover As New TWFDefinitionInfo
        count = 0
        If listtwfTselApprover.Count > 0 Then
            For Each twfTselApprover In listtwfTselApprover
                If count > 0 Then
                    yValTsel -= 40
                End If
                Dim sno As Int32 = co_controller.GetSNOByRoleTaskCOID(twfTselApprover.RoleId, twfTselApprover.TskId, crid, Integer.Parse(ConfigurationManager.AppSettings("codocid")))
                If sno > 0 Then
                    co_controller.UpdateCOCoordinate(sno, xVal, yValTsel)
                End If
                count += 1
            Next
        End If
    End Sub

    Private Sub HistoricalRejectionLogUpdate(ByVal coid As Int32, ByVal packageid As String)
        controller.HistoricalRejectionUpdate(CommonSite.UserId, Convert.ToInt32(ConfigurationManager.AppSettings("codocid")), "co", packageid)
    End Sub

    Private Function GetCOId() As Int32
        If Not String.IsNullOrEmpty(Request.QueryString("id")) Then
            Return Convert.ToInt32(Request.QueryString("id"))
        Else
            Return 0
        End If
    End Function

    Private Function GetCODetail(ByVal coid As Int32) As COInfo
        Return co_controller.GetODCO(coid)
    End Function

    Private Sub AddMergedCells(ByVal objgridviewrow As GridViewRow, ByVal objtablecell As TableCell, ByVal colspan As Integer, ByVal celltext As String, ByVal backcolor As String)
        objtablecell = New TableCell
        objtablecell.Text = celltext
        objtablecell.ColumnSpan = colspan
        objtablecell.Style.Add("background-color", backcolor)
        objtablecell.Style.Add("font-family", "Verdana")
        objtablecell.Style.Add("font-size", "6.5pt")
        objtablecell.Style.Add("font-weight", "bold")
        objtablecell.HorizontalAlign = HorizontalAlign.Center
        objtablecell.BorderColor = Drawing.Color.Black
        objtablecell.BorderWidth = 1
        objtablecell.BorderStyle = BorderStyle.Solid
        objgridviewrow.Cells.Add(objtablecell)
    End Sub

#End Region

End Class
