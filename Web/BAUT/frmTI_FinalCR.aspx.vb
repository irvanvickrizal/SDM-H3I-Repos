Imports CRFramework
Imports System.Data
Imports System.IO
Imports Common
Imports BusinessLogic
Imports Entities
Imports System.Collections.Generic

Partial Class BAUT_frmTI_FinalCR
    Inherits System.Web.UI.Page
    Dim controller As New CRController
    Dim objdb As New DBUtil
    Dim objET1 As New ETSiteDoc
    Dim objET As New ETAuditTrail
    Dim objBo As New BOSiteDocs
    Dim objBOAT As New BOAuditTrail
    Dim objdo As New ETWFTransaction
    Dim dt As New DataTable
    Dim crid As New Int32

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim packageid As String = Request.QueryString("wpid")
            HdnCRID.Value = GetCRId(packageid)
            'crid = GetCRId(packageid)
            BindCRDetail(Convert.ToInt32(HdnCRID.Value))
            GetApprovalList(Convert.ToInt32(HdnCRID.Value))
        End If
    End Sub

    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        Return
    End Sub

    Protected Sub LbtReviewCRFinalClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles LbtReviewCRFinal.Click
        If Request.Browser.Browser = "IE" Then
            GeneratePDFForm()
        Else
            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('Please use Internet Explorer');", True)
        End If
    End Sub

    Protected Sub LbtCancelCRFinalClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles LbtCancelCRFinal.Click
        Response.Redirect("../CR/frmCRReadyCreation.aspx")
    End Sub


#Region "Custom Methods"

    Private Sub BindCRDetail(ByVal crid As Int32)
        Dim info As CRInfo = controller.GetCRDetail(crid)
        'LblDescriptionofChange.Text = info.DescriptionofChange
        'ChkRegulatoryRequirement.Checked = info.IsRegulatoryRequirement
        'ChkSiteCondition.Checked = info.IsSiteCondition
        'ChkDesignChange.Checked = info.IsDesignChange
        'ChkTechnicalError.Checked = info.IsTechnicalError
        'ChkOther.Checked = info.IsOther
        'LblJustificationComments.Text = info.JustificationComments
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
        HdnWPId.Value = info.PackageId
        BindSiteAtt(info.PackageId)
        GetAllCRApproved(info.PackageId)
        BindDocCRApproved(info.PackageId)
    End Sub

    Public Sub GetAllCRApproved(ByVal packageid As String)
        GvFinalDescription.DataSource = controller.GetListCRApproved(packageid)
        GvFinalDescription.DataBind()
    End Sub

    Public Sub BindDocCRApproved(ByVal packageid As String)
        GvCRDocApproved.DataSource = controller.GetListCRApproved(packageid)
        GvCRDocApproved.DataBind()
    End Sub

    Public Function GetApprovalList(ByVal crid As Int32) As List(Of String)
        Dim approvals As List(Of ReviewerInfo) = controller.GetCRFinalApproval(crid)
        Dim bindApprovalList As New List(Of String)
        Dim approval As New ReviewerInfo
        If approvals.Count > 0 Then
            Dim count As Integer = 0
            For Each approval In approvals
                Dim strApproval As String = String.Empty
                count += 1
                If String.IsNullOrEmpty(approval.SignTitle) Then
                    strApproval = Convert.ToString(count) & ". " & approval.TaskEvent & " by " & approval.UserName & ", " & String.Format("{0:dd-MMMM-yyyy}", approval.ExecuteDate)
                Else
                    strApproval = Convert.ToString(count) & ". " & approval.TaskEvent & " by " & approval.UserName & " as " & approval.SignTitle & ", " & String.Format("{0:dd-MMMM-yyyy}", approval.ExecuteDate)
                End If
                bindApprovalList.Add(strApproval)
            Next
            'GvApprovals.DataSource = bindApprovalList
            'GvApprovals.DataBind()
        End If
        Return bindApprovalList
    End Function

    Public Function GetApprovalListString(ByVal crid As Int32) As String
        Dim approvals As List(Of ReviewerInfo) = controller.GetCRFinalApproval(crid)
        Dim bindApprovalList As String = String.Empty
        Dim approval As New ReviewerInfo
        If approvals.Count > 0 Then
            Dim count As Integer = 0
            For Each approval In approvals
                Dim strApproval As String = String.Empty
                count += 1
                If String.IsNullOrEmpty(approval.SignTitle) Then
                    bindApprovalList += Convert.ToString(count) & ". " & approval.TaskEvent & " by " & approval.UserName & ", " & String.Format("{0:dd-MMMM-yyyy}", approval.ExecuteDate) & " <br/>"
                Else
                    bindApprovalList += Convert.ToString(count) & ". " & approval.TaskEvent & " by " & approval.UserName & " as " & approval.SignTitle & ", " & String.Format("{0:dd-MMMM-yyyy}", approval.ExecuteDate) & " <br/>"
                End If
            Next
            'bindApprovalList = strApproval
        End If
        Return bindApprovalList
    End Function

    Private Function GetCRId(ByVal packageid As String) As Int32
        Return controller.GetCRIDLastApproved(packageid)
    End Function

    

    Private Sub BindSiteAtt(ByVal wpid As String)
        If Not String.IsNullOrEmpty(wpid) Then
            Dim strQuery As String = "select top 1 siteNo, sitename,PoNo,poname,RGNName,TselProjectID,SiteId,SiteVersion,Scope from poepmsitenew where workpackageid='" & wpid & "'"
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
                hdnSiteId.Value = dtSiteAtt.Rows(0).Item(6).ToString()
                HdnVersion.Value = dtSiteAtt.Rows(0).Item(7).ToString()
                HdnScope.Value = dtSiteAtt.Rows(0).Item(8).ToString()
            End If
        End If
    End Sub

    Private Sub GeneratePDFForm()
        ButtonPanelControl.Visible = False
        ListCRApprovedDocPanel.Visible = False
        If (uploaddocument()) = 1 Then
            Dim isCOEnabled As Integer = objdb.ExeQueryScalar("select count(*) from sitedoc where siteid=" & HdnSiteId.Value & " and version=" & HdnVersion.Value & " and docid=" & ConfigurationManager.AppSettings("CODocId"))
            If isCOEnabled = 0 Then
                EnablingCOChecklist()
            End If
            Response.Redirect("../CR/frmCRReadyCreation.aspx")
        End If
    End Sub

    Private Function uploaddocument() As Integer
        Dim isSucceed As Integer = 0
        Try
            dt = objBo.getbautdocdetailsNEW(ConfigurationManager.AppSettings("CRDocId"))
            Dim sec As String = dt.Rows(0).Item("sec_name").ToString
            Dim subsec As String = dt.Rows(0).Item("subsec_name").ToString
            Dim secpath As String = ""
            Dim ft As String = ""
            Dim path As String = ""
            Dim filenameorg As String
            Dim fileNameOnly As String
            Dim ReFileName As String
            fileNameOnly = "-CRFinal-"
            filenameorg = LblSiteID.Text & fileNameOnly & Format(CDate(DateTime.Now), "ddMMyyyyHHss")
            ReFileName = filenameorg & ".htm"
            ft = ConfigurationManager.AppSettings("Type") & LblPONo.Text & "-" & HdnScope.Value & "\"
            path = ConfigurationManager.AppSettings("Fpath") & LblSiteID.Text & ft
            Dim orgDocPath As String = LblSiteID.Text & ft & secpath & ReFileName
            'Dim strResult As String = DOInsertTrans(HdnSiteId.Value, ConfigurationManager.AppSettings("CRDocId"), HdnVersion.Value, path, orgDocpath)
            Dim DocPath As String = ""
            DocPath = LblSiteID.Text & ft & secpath & CreatePDFFile(Replace(path, "  \", "\"))
            ''offline mode
            'DocPath = LblSiteID.Text & ft & secpath

            Dim info As New CRFinalInfo
            info.CRFinalID = GetCRFinalID()
            info.DocPath = DocPath
            info.OrgDocPath = orgDocPath
            info.CRID = Convert.ToInt32(HdnCRID.Value)
            info.PackageId = Request.QueryString("wpid")
            info.LMBY = CommonSite.UserId
            info.IsUploaded = True
            controller.InsertUpdateCRFinal(info)
            With objET1
                .SiteID = HdnSiteId.Value
                .DocId = ConfigurationManager.AppSettings("CRDocId")
                .IsUploded = 1
                .Version = HdnVersion.Value
                .keyval = 2
                .DocPath = DocPath
                .AT.RStatus = Constants.STATUS_ACTIVE
                .AT.LMBY = Session("User_Name")
                .orgDocPath = orgDocPath
                .PONo = LblPONo.Text
            End With
            objBo.updatedocupload(objET1)
            'Fill Transaction table
            'AuditTrail()
            CRAuditTrail(Convert.ToInt32(HdnCRID.Value), DateTime.Now)
            isSucceed = 1
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('Error during Transaction," & ex.Message & "');", True)
        End Try
        Return isSucceed
    End Function

    Private Function CreatePDFFile(ByVal StrPath As String) As String
        StrPath = StrPath.Replace(" \", "\").ToString 'bugfix100816 causing error when the path containing space before \
        Dim dtba As New DataTable
        Dim filenameorg As String
        Dim ReFileName As String
        filenameorg = LblSiteID.Text & "-CRFinal-" & Format(CDate(DateTime.Now), "ddMMyyyyHHss")
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
        sw.WriteLine(".lblBTextDisclaimer{font-family: verdana;font-size: 6.5pt;color: #000000;text-align: left;font-weight: bold;}")
        sw.WriteLine(".lblBold{font-family: verdana;font-size: 12pt;color: #000000;font-weight: bold;}")
        sw.WriteLine(".subpanelheader{text-align: left;}")
        sw.WriteLine(".lblSubHeader{font-family:Verdana;font-size:8pt;font-weight:bolder;color:white;}")
        sw.WriteLine(".lblSubHeader2{font-family:Verdana;font-size:9pt;font-weight:bolder;}")
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
        sw.WriteLine("</style></head>")
        sw.WriteLine("<body class=""MainCSS"">")
        dvPrint.RenderControl(sw)
        sw.WriteLine("</body>")
        sw.WriteLine("</html>")
        sw.Close()
        sw.Dispose()
        Return Common.TakeFileUpload.ConvertAnyFormatToPDFHtmlNew(StrPath & ReFileName, StrPath, filenameorg)
    End Function

    Private Sub CRAuditTrail(ByVal crid As Int32, ByVal startdatetime As DateTime)
        Dim info As New CRLogInfo
        info.CR_ID = crid
        info.EventStartTime = startdatetime
        info.EventEndTime = startdatetime
        info.Task = 5
        info.Userid = CommonSite.UserId
        info.RoleId = CommonSite.RollId
        info.Remarks = "This doc is Reviewed As CR Final"
        info.Categories = String.Empty
        info.SubDocId = 0
        controller.InsertNewCRLog(info)
    End Sub
    
    Sub fillDetails()
        objdo.DocId = Integer.Parse(ConfigurationManager.AppSettings("CRDocId"))
        objdo.Site_Id = hdnsiteid.Value
        objdo.SiteVersion = hdnversion.Value
        objdo.AT.RStatus = Constants.STATUS_ACTIVE
        objdo.AT.LMBY = Session("User_Name")
    End Sub
    Sub AuditTrail()
        objET.PoNo = lblPONO.Text
        objET.SiteId = hdnsiteid.Value
        objET.DocId = ConfigurationManager.AppSettings("CRDocId")
        objET.Task = "1"
        objET.Status = "1"
        objET.Userid = Session("User_Id")
        objET.Roleid = Session("Role_Id")
        objET.fldtype = hdnScope.Value
        objBOAT.uspAuditTrailI(objET)
    End Sub
    Function DOInsertTrans(ByVal siteid As String, ByVal docName As String, ByVal version As Integer, ByVal path As String, ByVal orgpath As String) As String
        Try
            Dim dtNew As DataTable = objBo.doinserttrans(0, ConfigurationManager.AppSettings("CRDocId"))
            Dim status As Integer = 99
            objBo.uspwftransactionNOTWFI(Integer.Parse(ConfigurationManager.AppSettings("CRDocId")), 0, HdnSiteId.Value, HdnVersion.Value, 1, Session("User_Id"), status, 2, Session("User_Name"))
            Return 1
        Catch ex As Exception
            Return "0"
        End Try
    End Function
    Private Function GetCRFinalID() As Int32
        If Not String.IsNullOrEmpty("crfinalid") Then
            Return Convert.ToInt32(Request.QueryString("crfinalid"))
        Else
            Return 0
        End If
    End Function

    Private Sub EnablingCOChecklist()
        Dim perc As Double = 0
        Dim sCount As Integer = objdb.ExeQueryScalar("select count(*) from sitedoc where siteid=" & HdnSiteId.Value & " and version=" & HdnVersion.Value)
        perc = Format(100 / sCount + 1, "0.00")
        Dim i As Integer = 0
        i = objBo.uspSiteDocIU(HdnSiteId.Value, Constants.STATUS_ACTIVE, Session("User_Name"), ConfigurationManager.AppSettings("CODocid"), perc, 1, HdnVersion.Value, , LblPONo.Text)
        If i = 0 Then
            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('Error while doing the transaction.');", True)
        End If
    End Sub

    
#End Region

End Class
