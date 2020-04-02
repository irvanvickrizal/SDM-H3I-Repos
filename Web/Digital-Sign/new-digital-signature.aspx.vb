Imports BusinessLogic
Imports Common
Imports System.Data
Imports System.Collections.Generic
Imports Common_NSNFramework
Imports System.IO

Partial Class Digital_Sign_new_digital_signature
    Inherits System.Web.UI.Page

    Dim objutil As New DBUtil
    Dim objBo As New BODashBoard
    Dim intHeight As Integer = 0
    Dim intWidth As Integer = 150
    Dim strPath As String = String.Empty
    Dim objBOS As New BOSiteDocs
    Dim controller As New HCPTController
    Dim objmail As New TakeMail



#Region "NSNFramework"
    Dim dbutils_nsn As New DBUtils_NSN
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If GetDOCID() = ConfigurationManager.AppSettings("ssvdocid") Then
                lblacceptRemarks.Visible = True
                txtRemarksAccept.Visible = True
            Else
                txtRemarksAccept.Visible = False
                lblacceptRemarks.Visible = False
            End If
            HdnGuid.Value = Guid.NewGuid.ToString()
            'HdnUsername.Value = objutil.ExeQueryScalarString("select usrlogin from ebastusers_1 where usr_id=" & CommonSite.UserId) no need to call since get from session once login
            'Change by Irvan Vickrizal - 23 Jan 2019
            HdnUsername.Value = CommonSite.UserLogin
            intHeight = 50
            intWidth = 150
            GetPDFDocument()
            binddoc()
            BindData()
            If DocIsPartofApprovalSheet(GetDOCID()) = True Then
                ViewPanelATPApprovalSheet(True)
                BindReviewerATP(GetDOCID(), GetWPID())
                pnlapproval.Visible = False
                pnlreviewer.Visible = True
                MvApprovalButtonPanel.SetActiveView(vwDefaultPanel)
                btnreview.Text = "Accept"
            Else
                ViewPanelATPApprovalSheet(False)
                BindPanelApproval(GetTaskID())
                BindReviewer(GetSNO(), GetWPID())
                btnreview.Text = "Review"
            End If

        End If
    End Sub

    Protected Sub grddocuments_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grddocuments.RowDataBound
        Select Case e.Row.RowType
            Case DataControlRowType.DataRow
                Dim lbl As Label = CType(e.Row.FindControl("lblNo"), Label)
                lbl.Text = (grddocuments.PageIndex * grddocuments.PageSize) + (e.Row.RowIndex + 1) & ".&nbsp;"
        End Select
        If e.Row.RowType = DataControlRowType.DataRow Then
            'Dim url As String
            'url = "../PO/frmViewDocument.aspx?id=" & e.Row.Cells(4).Text
            Dim LblDocId As Label = CType(e.Row.FindControl("LblDocId"), Label)
            Dim url As String = String.Empty
            If DocIsPartofApprovalSheet(LblDocId.Text) = True Then
                url = "../PO/frmViewDocumentATP.aspx?id=" & e.Row.Cells(4).Text
            Else
                url = "../PO/frmViewDocument.aspx?id=" & e.Row.Cells(4).Text
            End If
            If e.Row.Cells(2).Text <> "" And e.Row.Cells(2).Text <> "&nbsp;" Then
                'e.Row.Cells(3).Text = "<a href='#' onclick=""window.open('" & url & "','mywindow','status=yes,menubar=no,scrollbars=yes,resizable=yes,width=700px,height=600px')"">" & e.Row.Cells(3).Text & "</a>"
                e.Row.Cells(3).Text = "<a href='" & url & "' TARGET='_blank'>" & e.Row.Cells(3).Text & "</a>"
            Else
                If InStr(e.Row.Cells(3).Text, "WCTR BAST") > 0 Then
                    url = "../baut/frmti_wctrbast.aspx?id=" & e.Row.Cells(4).Text & "&Open=0"
                    e.Row.Cells(3).Text = "<a href='#' onclick=""window.open('" & url & "','mywindow','status=yes,menubar=no,scrollbars=yes,resizable=yes,width=700px,height=600px')"">" & e.Row.Cells(3).Text & "</a>"
                Else
                    e.Row.Cells(3).Text = e.Row.Cells(3).Text
                End If
            End If
        End If
    End Sub

    Protected Sub LbtRequestPassword_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkrequest.Click
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        Dim strPassword As String = objutil.ExeQueryScalarString("Exec random_password 8,'simple'")
        Dim dtUsers As DataTable = objutil.ExeQueryDT("select top 1 name, phoneno from ebastusers_1 where phoneno is not null and usr_id =" & CommonSite.UserId.ToString(), "useratt")
        If dtUsers.Rows.Count() > 0 Then
            Dim isSucceedSendSMS As Boolean = New SMSNew().SendDGPassSMS(dtUsers.Rows(0).Item("name").ToString(), dtUsers.Rows(0).Item("phoneno").ToString(), strPassword)
            'Dim isSucceedSendSMS As Boolean = True
            If isSucceedSendSMS = True Then
                Dim info As New DGPassInfo
                info.UserInfo.UserId = CommonSite.UserId
                info.NewPassword = strPassword
                If controller.DGPassword_I(info) = True Then
                    Response.Write("<script>alert('Please check for your password in your phone');</script>")
                End If
            End If
        End If
    End Sub

#Region "Header ATP Approval Sheet"
    Public Sub ViewPanelATPApprovalSheet(ByVal isvisible As Boolean)
        If isvisible = True Then
            dvPrint.Visible = True
            divReviewer2.Visible = False
            BindSiteInfo(GetWPID())
        Else
            dvPrint.Visible = False
            divReviewer2.Visible = True
        End If
    End Sub

    Public Sub BindSiteInfo(ByVal wpid As String)
        Dim info As SiteInfo = controller.GetSiteInfoDetail(wpid)
        If info IsNot Nothing Then
            divBTSType.InnerText = info.ProjectID
            divPONO.InnerText = info.PONO
            divScope.InnerText = info.Scope
            divSiteID.InnerText = info.SiteNo
            divSiteName.InnerText = info.SiteName
        End If
    End Sub
#End Region

#Region "Panel approval control"
    Protected Sub BtnSignReject_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSignReject.Click
        lnkrequest.Visible = False
        BindAppReasons()
    End Sub

    Protected Sub BtnSubmitSignReject_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSubmitSignReject.Click
        Dim strCategories As String = String.Empty
        Dim MyItem As ListItem
        Dim countThick As Integer = 0
        For Each MyItem In CbReasonLists.Items
            If MyItem.Selected = True Then
                If countThick = 1 Then
                    strCategories = strCategories & ", "
                End If
                strCategories = strCategories & MyItem.Text
                countThick = 1
            End If
        Next

        Dim j As Integer
        Dim isSucceed As Boolean = True
        For j = 0 To grddocuments.Rows.Count - 1
            Dim rdbstatus As RadioButtonList = grddocuments.Rows(j).Cells(7).FindControl("rdbstatus")
            Dim txtremarks As TextBox = grddocuments.Rows(j).Cells(8).FindControl("txtremarks")
            Dim LblDocID As Label = grddocuments.Rows(j).Cells(0).FindControl("LblDocID")
            If rdbstatus IsNot Nothing And txtremarks IsNot Nothing And LblDocID IsNot Nothing Then
                If rdbstatus.SelectedValue = "1" Then
                    Dim info As New DOCTransactionInfo
                    info.DocInf.DocId = Integer.Parse(LblDocID.Text)
                    info.SiteInf.PackageId = GetWPID()
                    info.RoleInf.RoleId = CommonSite.RollId
                    info.UserInf.UserId = CommonSite.UserId
                    info.WFID = 99
                    If Not String.IsNullOrEmpty(txtremarks.Text) Then
                        If DocAttachmentRejection(info, txtremarks.Text, strCategories) = False Then
                            isSucceed = False
                        End If
                    Else
                        If DocAttachmentRejection(info, TxtRemarks_SignRejectPanel.Text, strCategories) = False Then
                            isSucceed = False
                        End If
                    End If
                End If
            End If
        Next

        If isSucceed = True Then
            Dim info As New DOCTransactionInfo
            info.SNO = GetSNO()
            info.SiteInf.PackageId = GetWPID()
            info.RoleInf.RoleId = CommonSite.RollId
            info.UserInf.UserId = CommonSite.UserId
            DocOnlineRejection(info, TxtRemarks_SignRejectPanel.Text, strCategories)
            Dim docdetail As DocInfo = controller.GetDocDetail(Integer.Parse(HDDocid.Value))
            ActivityLog_I(CommonSite.UserId, BaseConfiguration.Activity_Rejected_Doc & " " & docdetail.DocName)
            SendMailNotificationRejection(GetWPID(), GetDOCID(), strCategories, TxtRemarks_SignRejectPanel.Text)
        End If
    End Sub

    Protected Sub BtnSign_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSign.Click
        If Not String.IsNullOrEmpty(txtPassword.Text) Then
            Dim getuserlogin As String = controller.DGPassword_Validation(CommonSite.UserId, txtPassword.Text)
			If Not String.IsNullOrEmpty(getuserlogin) Then
                Dim npwd1 As String = "12345678"
                Dim Flags As Integer = 0
                Dim IntCount As Integer = 0
                Dim transinfo As DOCTransactionInfo = controller.WFTransaction_LD(GetSNO())
                Dim strResult As String = String.Empty
                If Not transinfo Is Nothing Then
                    Try
                        If 1 = 1 Then Flags = Flags Or SAPIWrapper.SAPI_ENUM_DRAWING_ELEMENT_GRAPHICAL_IMAGE
                        If 1 = 1 Then Flags = Flags Or SAPIWrapper.SAPI_ENUM_DRAWING_ELEMENT_SIGNED_BY
                        If 1 = 0 Then Flags = Flags Or SAPIWrapper.SAPI_ENUM_DRAWING_ELEMENT_REASON
                        If 1 = 1 Then Flags = Flags Or SAPIWrapper.SAPI_ENUM_DRAWING_ELEMENT_TIME
                        strResult = SAPIWrapper.SAPI_sign_file(ConfigurationManager.AppSettings("FPath") + HDPath.Value, Nothing, getuserlogin, _
                        npwd1, transinfo.PageNo, transinfo.Xval, transinfo.Yval, 50, 150, False, "test", Flags, "")
                    Catch ex As Exception
                        strResult = ex.Message.ToString()
						controller.ErrorLogInsert(101, "btnSign_Click", ex.Message.ToString(), "NON-SP")
                    End Try					
                End If
                If (strResult = Constants._DigitalSign_Result) Then
                    Dim info As New DOCTransactionInfo
                    info.SNO = GetSNO()
                    info.SiteInf.PackageId = GetWPID()
                    info.WFID = GetWFID()
                    info.TaskId = GetTaskID()
                    info.RoleInf.RoleId = CommonSite.RollId
                    info.UserInf.UserId = CommonSite.UserId
                    info.Media = "Web"      'Added by Fauzan, 28 Nov 2018. To differentiate between email approval Or web approval
                    If DocOnlineAccept(info) = True Then
                        MilestoneUpdate()
                        objmail.SendApprovalMailNotification(GetWPID(), CInt(HDDocid.Value))        'Added by Fauzan 24 Nov 2018. Email Approver.
                        'SendMailNotification(GetWPID())
                        SendMailNotificationDocApproved(GetWPID(), CommonSite.UserId)
                        LblWarningSignMessage.Text = "Sign Successfully!"
                        LblWarningSignMessage.Visible = True
                        LblWarningSignMessage.ForeColor = Drawing.Color.Green
                        LblWarningSignMessage.Font.Italic = True
                        ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "WindowsCloseApprover();", True)
                        Dim docdetail As DocInfo = controller.GetDocDetail(Integer.Parse(HDDocid.Value))
                        ActivityLog_I(CommonSite.UserId, BaseConfiguration.Activity_Approved_Doc & " " & docdetail.DocName)
                    Else
                        LblWarningSignMessage.Text = "Sign Failed!"
                        LblWarningSignMessage.Visible = True
                        LblWarningSignMessage.ForeColor = Drawing.Color.Red
                        LblWarningSignMessage.Font.Italic = True '
                        ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "WindowsCloseApproverFail();", True)
                    End If
                Else
                    ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "DGFailedProcess('" & strResult & "');", True)
                End If
            Else
                LblWarningSignMessage.Text = "Password is incorrect!"
                LblWarningSignMessage.Visible = True
                LblWarningSignMessage.ForeColor = Drawing.Color.Red
                LblWarningSignMessage.Font.Italic = True
                Dim docdetail As DocInfo = controller.GetDocDetail(Integer.Parse(HDDocid.Value))
                ActivityLog_I(CommonSite.UserId, BaseConfiguration.Activity_Password_DG_Failed & " " & docdetail.DocName)
            End If
        Else
            LblWarningSignMessage.Text = "Please enter your Digital Signature Password First!"
            LblWarningSignMessage.Visible = True
            LblWarningSignMessage.ForeColor = Drawing.Color.Red
            LblWarningSignMessage.Font.Italic = True
            Dim docdetail As DocInfo = controller.GetDocDetail(Integer.Parse(HDDocid.Value))
            ActivityLog_I(CommonSite.UserId, BaseConfiguration.Activity_Password_DG_Failed & " " & docdetail.DocName)
        End If

    End Sub

    Protected Sub BtnCancelSignReject_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnCancelSignReject.Click
        MvPanelSign.SetActiveView(defaultSign)
        lnkrequest.Visible = True
        binddoc()
    End Sub
#End Region

#Region "Panel review controller"
    Protected Sub BtnReview_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnreview.Click
        Dim info As New DOCTransactionInfo
        info.SNO = GetSNO()
        info.SiteInf.PackageId = GetWPID()
        info.WFID = GetWFID()
        info.TaskId = GetTaskID()
        info.RoleInf.RoleId = CommonSite.RollId
        info.UserInf.UserId = CommonSite.UserId
        info.Media = "Web"      'Added by Fauzan, 28 Nov 2018. To differentiate between email approval Or web approval
        If controller.DocApproved(info) = True Then
            objmail.SendApprovalMailNotification(GetWPID(), CInt(HDDocid.Value))        'Added by Fauzan 24 Nov 2018. Email Approver.
            'SendMailNotification(GetWPID())
            LblWarningSignMessage.Text = "Review Successfully!"
            LblWarningSignMessage.Visible = True
            LblWarningSignMessage.ForeColor = Drawing.Color.Green
            LblWarningSignMessage.Font.Italic = True
            MilestoneUpdate()
            Dim docdetail As DocInfo = controller.GetDocDetail(Integer.Parse(HDDocid.Value))
            ActivityLog_I(CommonSite.UserId, BaseConfiguration.Activity_Approved_Doc & " " & docdetail.DocName)
            SendMailNotificationDocApproved(GetWPID(), CommonSite.UserId)
            If ConfigurationManager.AppSettings("ssvdocid") Then
                controller.DocRemarks(GetWPID(), HdnGuid.Value, txtRemarksAccept.Text, CommonSite.UserId, GetDOCID())
            End If
            If DocIsPartofApprovalSheet(Integer.Parse(HDDocid.Value)) = True Then
                GenerateApprovalSheetATPChecking()
            Else
                ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "WindowsCloseReviewer();", True)
            End If
        Else
            LblWarningSignMessage.Text = "Sign Failed!"
            LblWarningSignMessage.Visible = True
            LblWarningSignMessage.ForeColor = Drawing.Color.Red
            LblWarningSignMessage.Font.Italic = True
            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "WindowsCloseReviewerFail();", True)
        End If
    End Sub

    Protected Sub BtnRejectReviewNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnRejectReviewNew.Click
        lblacceptRemarks.Visible = False
        txtRemarksAccept.Visible = False
        BindReasons()
    End Sub

    Protected Sub BtnSubmitReject_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSubmitReject.Click
        Dim strCategories As String = String.Empty
        Dim MyItem As ListItem
        Dim countThick As Integer = 0
        For Each MyItem In CbList.Items
            If MyItem.Selected = True Then
                If countThick = 1 Then
                    strCategories = strCategories & ", "
                End If
                strCategories = strCategories & MyItem.Text
                countThick = 1
            End If
        Next
        Dim j As Integer
        Dim isSucceed As Boolean = True
        For j = 0 To grddocuments.Rows.Count - 1
            Dim rdbstatus As RadioButtonList = grddocuments.Rows(j).Cells(7).FindControl("rdbstatus")
            Dim txtremarks As TextBox = grddocuments.Rows(j).Cells(8).FindControl("txtremarks")
            Dim LblDocID As Label = grddocuments.Rows(j).Cells(0).FindControl("LblDocID")
            If rdbstatus IsNot Nothing And txtremarks IsNot Nothing And LblDocID IsNot Nothing Then
                If rdbstatus.SelectedValue = "1" Then
                    Dim info As New DOCTransactionInfo
                    info.DocInf.DocId = Integer.Parse(LblDocID.Text)
                    info.SiteInf.PackageId = GetWPID()
                    info.RoleInf.RoleId = CommonSite.RollId
                    info.UserInf.UserId = CommonSite.UserId
                    info.WFID = 99
                    If Not String.IsNullOrEmpty(txtremarks.Text) Then
                        If DocAttachmentRejection(info, txtremarks.Text, strCategories) = False Then
                            isSucceed = False
                        End If
                    Else
                        If DocAttachmentRejection(info, TxtRemarksReject.Text, strCategories) = False Then
                            isSucceed = False
                        End If
                    End If
                End If
            End If
        Next

        If isSucceed = True Then
            Dim info As New DOCTransactionInfo
            info.SNO = GetSNO()
            info.SiteInf.PackageId = GetWPID()
            info.RoleInf.RoleId = CommonSite.RollId
            info.UserInf.UserId = CommonSite.UserId
            DocOnlineRejection(info, TxtRemarksReject.Text, strCategories)
            Dim docdetail As DocInfo = controller.GetDocDetail(Integer.Parse(HDDocid.Value))
            ActivityLog_I(CommonSite.UserId, BaseConfiguration.Activity_Rejected_Doc & " " & docdetail.DocName)
            If ConfigurationManager.AppSettings("ssvdocid") Then
                controller.DocRemarksReject(GetWPID(), HdnGuid.Value, TxtRemarksReject.Text, CommonSite.UserId, GetDOCID())
            End If
            SendMailNotificationRejection(GetWPID(), GetDOCID(), strCategories, TxtRemarksReject.Text)
        End If
    End Sub

    Protected Sub BtnCancelSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnCancelSubmit.Click
        lblacceptRemarks.Visible = True
        txtRemarksAccept.Visible = True
        MvApprovalButtonPanel.SetActiveView(vwDefaultPanel)
        binddoc()
    End Sub

#End Region

#Region "Custom Methods"

    Private Sub BindData()
        Dim info As DOCTransactionInfo = controller.WFTransaction_LD(GetSNO())
        If info IsNot Nothing Then
            HdGRPID.Value = Convert.ToString(info.UGPID)
            hdnx.Value = Convert.ToString(info.Xval)
            hdny.Value = Convert.ToString(info.Yval)
            HdWFID.Value = Convert.ToString(info.WFID)
            HdTskId.Value = Convert.ToString(info.TaskId)
            HdGRPID.Value = Convert.ToString(info.UGPID)
            HDDocid.Value = Convert.ToString(info.DocInf.DocId)
        End If
    End Sub

    Private Sub MilestoneUpdate()
        If controller.GetLastTaskBaseWorkflowGrp(Integer.Parse(HdWFID.Value), Integer.Parse(HdGRPID.Value)) = Integer.Parse(HdTskId.Value) Then
            If Integer.Parse(HdGRPID.Value) = 1 Then 'means nsn submission
                controller.Milestone_IU(GetWPID(), Integer.Parse(HDDocid.Value), Date.Now, Nothing)
            ElseIf Integer.Parse(HdGRPID.Value) = 4 Then 'means customer approved'
                controller.Milestone_IU(GetWPID(), Integer.Parse(HDDocid.Value), Nothing, Date.Now)
            End If
        End If
    End Sub

    Private Sub GenerateApprovalSheetATPChecking()
        If Integer.Parse(HdGRPID.Value) = 4 Then 'means customer approval'
            If controller.GetLastTaskBaseWorkflowGrp(Integer.Parse(HdWFID.Value), Integer.Parse(HdGRPID.Value)) = Integer.Parse(HdTskId.Value) Then
                If GenerateATPApprovalSheet(GetWPID()) = False Then
                    controller.WFTransaction_LastRollback(GetSNO(), CommonSite.RollId, CommonSite.UserId)
                    ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "windowsCloseATPReviewerFailGenerate();", True)
                Else
                    ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "WindowsCloseReviewer();", True)
                End If
            Else
                ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "WindowsCloseReviewer();", True)
            End If
        Else
            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "WindowsCloseReviewer();", True)
        End If

    End Sub

    Sub GetPDFDocument()
        Dim dt As DataTable
        dt = objBo.DigitalSign(Convert.ToInt32(Request.QueryString("id")))
        If (dt.Rows.Count > 0) Then
            ltrDocname.Text = dt.Rows(0)("docname").ToString()
            strPath = dt.Rows(0)("docpath").ToString()
            HDPath.Value = dt.Rows(0)("docpath").ToString()
            HDPono.Value = dt.Rows(0)("pono").ToString()
            HDDocid.Value = dt.Rows(0)("docid").ToString()
            hdnx.Value = dt.Rows(0)("xval")
            hdny.Value = dt.Rows(0)("yval")
            hdpageNo.Value = dt.Rows(0)("page_no")
            LblDocPath.Text = strPath
            PDFViwer.Attributes.Add("src", ConfigurationManager.AppSettings("Vpath") + strPath.Replace("\", "/"))
        Else
            LblDocPath.Text = "Nothing"
        End If
    End Sub

    Sub binddoc()
        'hardcoded to filter only for BAUT and BAST
        Dim dt As New DataTable
        'dt = objboSiteDoc.uspSiteBAUTDocList(Request.QueryString("siteno"), Request.QueryString("version"), Request.QueryString("docname"))
        'Dim strsql As String = "Exec uspSiteBAUTDocList " & Request.QueryString("swid")
        Dim strsql As String = "Exec uspSiteBAUTDocList " & Request.QueryString("swid") & ",0 , " & CommonSite.UserId
        dt = objutil.ExeQueryDT(strsql, "digilist")
        grddocuments.DataSource = dt
        grddocuments.DataBind()
        grddocuments.Columns(1).Visible = False
        grddocuments.Columns(2).Visible = False
        grddocuments.Columns(3).Visible = True
        grddocuments.Columns(4).Visible = False
        grddocuments.Columns(5).Visible = False
        grddocuments.Columns(6).Visible = False
        grddocuments.Columns(7).Visible = True
        'get the parent document
        'strsql = "Exec uspSiteBAUTDocList " & Request.QueryString("swid") & ",1"
        'strsql = "Exec uspSiteBAUTDocList " & Request.QueryString("swid") & ",1 ," & CommonSite.UserId
        'dt = objutil.ExeQueryDT(strsql, "digilist")
        'grddocuments2.DataSource = dt
        'grddocuments2.DataBind()
        'grddocuments2.Columns(1).Visible = False
        'grddocuments2.Columns(2).Visible = False
        'grddocuments2.Columns(3).Visible = True
        'grddocuments2.Columns(4).Visible = False
        'grddocuments2.Columns(5).Visible = False
        'grddocuments2.Columns(6).Visible = False
        'grddocuments2.Columns(7).Visible = True
        'grddocuments2.Visible = False
    End Sub

    Protected Sub DOThis(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rad As RadioButtonList = CType(sender, RadioButtonList)
        Dim grdr As GridViewRow = CType(rad.NamingContainer, GridViewRow)
        Dim x As Integer = grdr.RowIndex
        Dim rdl As RadioButtonList = grddocuments.Rows(x).Cells(5).FindControl("rdbstatus")
        Dim txt1 As TextBox = grddocuments.Rows(x).Cells(6).FindControl("txtremarks")
        'txt1.Text =
        If rdl.SelectedValue = 0 Then 'when approve
            txt1.Visible = False
            txtPassword.Enabled = True
            txtPassword.Visible = True
            lnkrequest.Enabled = True
            lnkrequest.Visible = True
            BtnSign.Enabled = True
            BtnSign.Visible = True
            If pnlapproval.Visible = True Then
                Dim j As Integer
                Dim intSuccess As Integer = 0
                Dim isrejectexist As Boolean = False
                For j = 0 To grddocuments.Rows.Count - 1
                    Dim rdbstatus As RadioButtonList = grddocuments.Rows(j).Cells(7).FindControl("rdbstatus")
                    If rdbstatus.SelectedValue = "1" Then
                        isrejectexist = True
                    End If
                Next
                If isrejectexist = True Then
                    lnkrequest.Visible = False
                    BindAppReasons()
                Else
                    lnkrequest.Visible = True
                    MvPanelSign.SetActiveView(defaultSign)
                    BindPanelApproval(GetTaskID())
                End If

            Else
                Dim j As Integer
                Dim intSuccess As Integer = 0
                Dim isrejectexist As Boolean = False
                For j = 0 To grddocuments.Rows.Count - 1
                    Dim rdbstatus As RadioButtonList = grddocuments.Rows(j).Cells(7).FindControl("rdbstatus")
                    If rdbstatus.SelectedValue = "1" Then
                        isrejectexist = True
                    End If
                Next
                If isrejectexist = True Then
                    BindReasons()
                Else
                    MvApprovalButtonPanel.SetActiveView(vwDefaultPanel)
                End If

            End If
            btnreview.Enabled = True
            btnreview.Visible = True
        Else 'when reject
            txt1.Visible = True
            If pnlapproval.Visible = True Then
                lnkrequest.Visible = False
                BindAppReasons()
            Else
                BindReasons()
            End If

            txtPassword.Enabled = False
            txtPassword.Visible = False
            lnkrequest.Enabled = False
            lnkrequest.Visible = False
            BtnSign.Enabled = False
            BtnSign.Visible = False
        End If
    End Sub

    Private Sub BindPanelApproval(ByVal tskid As Integer)
        Dim taskdesc As String = controller.GetTaskDesc(tskid)
        If taskdesc.ToLower().Equals("approver") Then
            pnlapproval.Visible = True
            pnlreviewer.Visible = False
            If controller.DGPassword_CheckIsExpired(CommonSite.UserId) = True Then
                pnlDGSign.Visible = True
                PnlRequestPass.Visible = True
                MvPanelSign.SetActiveView(defaultSign)
            Else
                pnlDGSign.Visible = True
                PnlRequestPass.Visible = True
                MvPanelSign.SetActiveView(defaultSign)
            End If
        ElseIf taskdesc.ToLower().Equals("reviewer") Then
            pnlapproval.Visible = False
            pnlreviewer.Visible = True
            MvApprovalButtonPanel.SetActiveView(vwDefaultPanel)
        End If
    End Sub

    Private Sub BindReasons()
        MvApprovalButtonPanel.SetActiveView(vwRejectPanel)
        CbList.DataSource = dbutils_nsn.GetReasons(2001)
        CbList.DataTextField = "ReasonName"
        CbList.DataValueField = "ReasonId"
        CbList.DataBind()
    End Sub

    Private Sub BindAppReasons()
        MvPanelSign.SetActiveView(defaultSignReject)
        CbReasonLists.DataSource = dbutils_nsn.GetReasons(2001)
        CbReasonLists.DataTextField = "ReasonName"
        CbReasonLists.DataValueField = "ReasonId"
        CbReasonLists.DataBind()
    End Sub

    Private Sub BindReviewer(ByVal sno As Int32, ByVal wpid As String)
        Dim count As Integer = 1
        Dim strReviewer As String = String.Empty
        For Each info As DOCTransactionInfo In controller.GetReviewTransactionLog(sno, wpid, "reviewer")
            If count > 1 Then
                strReviewer += "<br/>"
            End If
            strReviewer += "This document was reviewed by <b>" & info.UserInf.Username & " As " & info.UserInf.SignTitle & "</b> On <b>" & String.Format("{0:dd-MMM-yyyy hh:mm:ss}", info.EndDateTime) & "</b>"
            count += 1
        Next
        divReviewer2.InnerHtml = strReviewer
    End Sub

    Private Sub BindReviewerATP(ByVal docid As Integer, ByVal wpid As String)
        Dim count As Integer = 1
        Dim strReviewer As String = String.Empty
        For Each info As DOCTransactionInfo In controller.GetDocReviewerLog(docid, wpid)
            Dim strCompany As String = String.Empty
            Dim strTaskName As String = String.Empty
            If count > 1 Then
                strReviewer += "<br/>"
            End If
            If info.UserInf.UserType.ToLower().Equals("s") Then
                strCompany = "Partner"
            ElseIf info.UserInf.UserType.ToLower().Equals("n") Then
                strCompany = "NSN"
            ElseIf info.UserInf.UserType.ToLower().Equals("h") Then
                strCompany = "Huawei"
            ElseIf info.UserInf.UserType.ToLower().Equals("c") Then
                strCompany = "H3I"
            End If

            If info.TaskId = 1 Then
                strTaskName = "Prepared by"
            Else
                strTaskName = "Approved by"
            End If
            strReviewer += strCompany & " " & strTaskName & " <b>" & info.UserInf.Username & " as " & info.UserInf.SignTitle & "</b> on <b>" & String.Format("{0:dd-MMM-yyyy hh:mm:ss}", info.EndDateTime) & "</b>"
            count += 1
        Next
        divReviewer.InnerHtml = strReviewer
    End Sub
    Private Function DocOnlineAccept(ByVal info As DOCTransactionInfo) As Boolean
        Return controller.DocApproved(info)
    End Function

    Private Function DocAttachmentRejection(ByVal info As DOCTransactionInfo, ByVal remarks As String, ByVal categories As String) As Boolean
        Return controller.DocRejected_Attachment(info, remarks, categories)
    End Function

    Private Sub DocOnlineRejection(ByVal info As DOCTransactionInfo, ByVal remarks As String, ByVal categories As String)
        If controller.DocRejected(info, remarks, categories) = True Then
            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "WindowRejectClose();", True)
        Else
            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "WindowRejectFailClose();", True)
        End If
    End Sub

    Private Function GetTaskID() As Integer
        If String.IsNullOrEmpty(Request.QueryString("tskid")) Then
            Return 0
        Else
            Return Integer.Parse(Request.QueryString("tskid"))
        End If
    End Function

    Private Function GetSNO() As Int32
        If Not String.IsNullOrEmpty(Request.QueryString("id")) Then
            Return Convert.ToInt32(Request.QueryString("id"))
        Else
            Return 0
        End If
    End Function

    Private Function GetDOCID() As Integer
        Return controller.GetDOCIDBaseSNO(GetSNO())
    End Function

    Private Function GetWPID() As String
        If Not String.IsNullOrEmpty(Request.QueryString("wpid")) Then
            Return Request.QueryString("wpid")
        Else
            Return "0"
        End If
    End Function

    Private Function GetWFID() As Integer
        If Not String.IsNullOrEmpty(Request.QueryString("wfid")) Then
            Return Integer.Parse(Request.QueryString("wfid"))
        Else
            Return 0
        End If
    End Function


#Region "ATP Approval Sheet"
    Public Function GenerateATPApprovalSheet(ByVal wpid As String) As Boolean
        Dim isSucceed As Boolean = True
        Dim info As SiteInfo = controller.GetSiteInfoDetail(wpid)
        If info IsNot Nothing Then
            Try
                Dim atpapprovaltemplate As String = ReadApprovalTemplate()
                If Not String.IsNullOrEmpty(atpapprovaltemplate) Then
                    Dim sb As StringBuilder = New StringBuilder()
                    Dim filenameorg As String
                    Dim ReFileName As String
                    Dim ft As String = ConfigurationManager.AppSettings("Type") & info.Scope & "-" & info.PackageId & "\"
                    filenameorg = ltrDocname.Text.Replace(" ","") & "-ApprovalSheet-" & Format(CDate(DateTime.Now), "ddMMyyyyHHss")
                    ReFileName = filenameorg & ".htm"
                    Dim docpath As String = info.SiteNo & ft
                    'Dim strPath As String = ConfigurationManager.AppSettings(GetFolderPath(scontype)) + docpath
                    sb.Append(atpapprovaltemplate)
                    sb.Replace("[LOGOHCPT]", "<img src='https://sdmthree.nsnebast.com/Images/three-logo.png' height='46px' width='60px' alt='hcptlogo' />")
                    sb.Replace("[LOGONSN]", "<img src='https://sdmthree.nsnebast.com/Images/nokia.png' height='36px' width='104px' alt='nokialogo'/>")
                    sb.Replace("[DIVPONOTXT]", info.PONO)
                    sb.Replace("[DIVSITENAMETXT]", info.SiteName)
                    sb.Replace("[DIVSITEIDTXT]", info.SiteNo)
                    sb.Replace("[DIVSCOPETXT]", info.Scope)
					sb.Replace("[DOCNAME]", ltrDocname.Text)
					sb.Replace("[DOCNAMERPT]",ltrDocname.Text)
                    sb.Replace("[DIVREVIEWERTXT]", GetATPReviewer(GetDOCID(), GetWPID()))
                    Dim orgdocpath As String = GetDefaultDocPath(Integer.Parse(HDDocid.Value)) & ReFileName
                    If Not System.IO.Directory.Exists(GetDefaultDocPath(Integer.Parse(HDDocid.Value))) Then
                        System.IO.Directory.CreateDirectory(GetDefaultDocPath(Integer.Parse(HDDocid.Value)))
                    End If
                    Try
                        Dim sw As HtmlTextWriter = New HtmlTextWriter(New StreamWriter(GetDefaultDocPath(Integer.Parse(HDDocid.Value)) & ReFileName, False, UnicodeEncoding.UTF8))
                        sw.WriteLine(sb.ToString())
                        sw.Close()
                        sw.Dispose()
                    Catch ex As Exception
                        isSucceed = False
                        controller.ErrorLogInsert(150, "GenerateATPApprovalSheet", ex.Message.ToString(), "MainFunction")
                    End Try
                    If isSucceed = True Then
                        'Dim newdocpath As String = Common.TakeFileUpload.ConvertAnyFormatToPDFHtmlNew(GetDefaultDocPath(Integer.Parse(HDDocid.Value)) & ReFileName, GetDefaultDocPath(Integer.Parse(HDDocid.Value)), filenameorg)
                        Dim newdocpath As String = EBASTFileUpload.ConvertAnyFormatToPDFHtmlNew(GetDefaultDocPath(Integer.Parse(HDDocid.Value)) & ReFileName, GetDefaultDocPath(Integer.Parse(HDDocid.Value)), filenameorg)
						'controller.ATPDocCompleted_U(Integer.Parse(ConfigurationManager.AppSettings("ATP")), info.PackageId, GetDefaultDocPath2(Integer.Parse(HDDocid.Value)) & newdocpath, Integer.Parse(ConfigurationManager.AppSettings("ATPDoc")))
						'Doc Approval Sheet Merge syntax and Change code Irvan 22 Sep 2017----------------------------------------------------------------------------------------
						Dim firstDocPath As String = GetDefaultDocPath(Integer.Parse(HDDocid.Value)) & newdocpath
                        Dim SecondDocPath As String = ConfigurationManager.AppSettings("FPath") & HDPath.Value
                        Dim strResult As String = New NSNPDFController().MergePdfNew(firstDocPath, SecondDocPath, GetDefaultDocPath(Integer.Parse(HDDocid.Value)) & "Approved_" & hdDocAliasName.Value & ".pdf")
						controller.ATPDocCompleted_U(Integer.Parse(HDDocid.Value), info.PackageId, GetDefaultDocPath2(Integer.Parse(HDDocid.Value)) & "Approved_" & hdDocAliasName.Value & ".pdf", Integer.Parse(ConfigurationManager.AppSettings("ATPDoc")))
						'-----------------------------End of Line new syntax-------------------------------------------------------------------------------------------------------
                    End If
                Else
                    isSucceed = False
                End If
            Catch ex As Exception
                isSucceed = False
                controller.ErrorLogInsert(150, "GenerateATPApprovalSheet", ex.Message.ToString(), "MainFunction")
            End Try
        End If
        Return isSucceed
    End Function

    Public Function GetATPReviewer(ByVal docid As Integer, ByVal wpid As String) As String
        Dim count As Integer = 1
        Dim strReviewer As String = String.Empty
        For Each info As DOCTransactionInfo In controller.GetDocReviewerLog(docid, wpid)
            Dim strCompany As String = String.Empty
            Dim strTaskName As String = String.Empty
            If count > 1 Then
                strReviewer += "<br/>"
            End If
            If info.UserInf.UserType.ToLower().Equals("s") Then
                strCompany = "Partner"
            ElseIf info.UserInf.UserType.ToLower().Equals("n") Then
                strCompany = "NSN"
            ElseIf info.UserInf.UserType.ToLower().Equals("h") Then
                strCompany = "Huawei"
            ElseIf info.UserInf.UserType.ToLower().Equals("c") Then
                strCompany = "H3I"
            End If

            If info.TaskId = 1 Then
                strTaskName = "Prepared by"
            Else
                strTaskName = "Approved by"
            End If
            strReviewer += strCompany & " " & strTaskName & " <b>" & info.UserInf.Username & " as " & info.UserInf.SignTitle & "</b> on <b>" & String.Format("{0:dd-MMM-yyyy hh:mm:ss}", info.EndDateTime) & "</b>"
            count += 1
        Next
        Return strReviewer
    End Function

    Public Function ReadApprovalTemplate() As String
        Dim serverpath As String = Server.MapPath("ApprovalSheet_Template")
        Dim filepath As String = serverpath + "\" + "ATP_ApprovalSheet.htm"
        'filepath = HttpContext.Current.Server.MapPath("~/Template/EBAST/ABSOLUTE_TEMPLATE/ATP_ApprovalSheet.htm")
        'filepath = "E:\\EBAST\\ABSOLUTE_TEMPLATE\\ATP_ApprovalSheet.htm"
        Dim content As String = String.Empty
        Try
            Using reader As StreamReader = New StreamReader(filepath, System.Text.Encoding.UTF8)
                content = reader.ReadToEnd()
            End Using
        Catch ex As Exception
            content = String.Empty
            controller.ErrorLogInsert(150, "ReadAppprovalTemplate", ex.Message.ToString(), "MainFunction")
        End Try

        If String.IsNullOrEmpty(content) Then
            Try
                filepath = "E:\\EBAST\\ABSOLUTE_TEMPLATE\\ATP_ApprovalSheet.htm"
                Console.WriteLine("Re-attempt to read Approval sheet template")
                Using reader As StreamReader = New StreamReader(filepath, System.Text.Encoding.UTF8)
                    content = reader.ReadToEnd()
                End Using
            Catch ex As Exception
                controller.ErrorLogInsert(150, "ReadAppprovalTemplate", ex.Message.ToString(), "MainFunction")
                content = String.Empty
            End Try
        End If

        Return content
    End Function

    Public Function GetFolderPath(ByVal scontype As String) As String
        If scontype.Equals("TI2GDemo") Or scontype.Equals("TI3GDemo") Then
            Return "FPath_Demo"
        Else
            If scontype.Equals("TI2G") Or scontype.Equals("CME2G") Or scontype.Equals("SIS2G") Then
                Return "FPath"
            ElseIf scontype.Equals("TI3G") Or scontype.Equals("CME3G") Or scontype.Equals("SIS3G") Then
                Return "FPath_3G"
            End If
        End If
        Return ""
    End Function


    Public Function GetDefaultDocPath(ByVal docid As Integer) As String
        Dim dt As DataTable = objBOS.getbautdocdetailsNEW(docid) '(Constants._Doc_SSR)
        Dim sec As String = dt.Rows(0).Item("sec_name").ToString
        Dim subsec As String = dt.Rows(0).Item("subsec_name").ToString
		Dim aliasname As String = dt.Rows(0).Item("alias_docname").ToString
        hdDocAliasName.Value = aliasname
        Dim secpath As String = ""
        Dim ft As String = ""
        Dim path As String = ""
        Dim filenameorg As String
        Dim FileNameOnly As String = "-" & "ATPFINAL" & "-"
        filenameorg = divSiteID.InnerText & FileNameOnly & Format(CDate(DateTime.Now), "ddMMyyyyHHss")
        Dim ReFileName As String = filenameorg & ".htm"
        secpath = IIf(subsec = "" Or subsec = "0", sec & "\", sec & "\" & subsec & "\")
        ft = ConfigurationManager.AppSettings("Type") & divScope.InnerText.TrimEnd.TrimStart() & "-" & GetWPID() & "\"
        path = ConfigurationManager.AppSettings("Fpath") & divSiteID.InnerText.TrimEnd.TrimStart() & ft & secpath

        Return path
    End Function

    Public Function GetDefaultDocPath2(ByVal docid As Integer) As String
        Dim dt As DataTable = objBOS.getbautdocdetailsNEW(docid) '(Constants._Doc_SSR)
        Dim sec As String = dt.Rows(0).Item("sec_name").ToString
        Dim subsec As String = dt.Rows(0).Item("subsec_name").ToString
        Dim secpath As String = ""
        Dim ft As String = ""
        Dim path As String = ""
        Dim filenameorg As String
        Dim FileNameOnly As String = "-" & "ATPFINAL" & "-"
        filenameorg = divSiteID.InnerText & FileNameOnly & Format(CDate(DateTime.Now), "ddMMyyyyHHss")
        Dim ReFileName As String = filenameorg & ".htm"
        secpath = IIf(subsec = "" Or subsec = "0", sec & "\", sec & "\" & subsec & "\")
        ft = ConfigurationManager.AppSettings("Type") & divScope.InnerText.TrimEnd.TrimStart() & "-" & GetWPID() & "\"
        path = divSiteID.InnerText.TrimEnd.TrimStart() & ft & secpath

        Return path
    End Function
    Protected Sub lbtUploadRejectDocument_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lbtUploadRejectDocument.Click

        If fuOtherDocument.HasFile Then
            Dim DocPath As String = ConfigurationManager.AppSettings("Fpath") & ConfigurationManager.AppSettings("Type") & HDPono.Value & "-" & GetWPID() & "\SDM\RejectAdditionalDocument\"

            If Directory.Exists(DocPath) = False Then
                Directory.CreateDirectory(DocPath)
            End If

            fuOtherDocument.SaveAs(DocPath + fuOtherDocument.FileName)

            controller.UploadRejectOtherDocument(GetWPID(), HdnGuid.Value, DocPath + fuOtherDocument.FileName, CommonSite.UserId, GetDOCID())

            tblFileUploadOtherDocument.Visible = False
            GetPDFDocument()
            binddoc()
            BindData()
        End If
    End Sub

#End Region

#Region "Send mail Notification"
    Private Sub SendMailNotification(ByVal packageid As String)
        'Dim users As List(Of UserProfileInfo) = doccontroller.GetNextDocPIC(packageid, wfid, tskid)
        Dim users As List(Of UserProfileInfo) = controller.GetNextPIC(packageid, Integer.Parse(HDDocid.Value))
        Dim info As SiteInfo = controller.GetSiteInfoDetail(packageid)
        Dim docdetail As DocInfo = controller.GetDocDetail(Integer.Parse(HDDocid.Value))
        If users IsNot Nothing Then
            If users.Count > 0 Then
                Dim sb As New StringBuilder
                For Each user As UserProfileInfo In users
                    Dim strSubject As String = String.Empty
                    'sb.Append("Dear " & user.Fullname & "<br/>")
                    sb.Append("Dear Sir/Madam, <br/>")
                    If docdetail IsNot Nothing Then
                        sb.Append("Following detail of " & docdetail.DocName & " is waiting for your review/approval <br/>")
                        strSubject = docdetail.DocName & " Waiting"
                    Else
                        sb.Append("Following detail of document is waiting for your review/approval <br/>")
                        strSubject = "Document Waiting"
                    End If

                    sb.Append("Site ID: " & info.SiteNo & "<br/>")
                    sb.Append("SiteName: " & info.SiteName & "<br/>")
                    sb.Append("WorkpackageID: " & info.PackageId & "<br/>")
                    sb.Append("PONO: " & info.PONO & "<br/>")
                    sb.Append("<a href='http://hcptdemo.nsnebast.com'>Click here</a>" & " to Login to e-RFT<br/>")
                    sb.Append("Powered By EBAST" & "<br/><br/>")
                    sb.Append("<img src='http://hcptdemo.nsnebast.com/images/nsn-logo.gif' alt='companylogo' />")
                    If docdetail IsNot Nothing Then
                        objmail.SendMailUser(user.Email, sb.ToString(), docdetail.DocName + " Waiting")
                    Else
                        objmail.SendMailUser(user.Email, sb.ToString(), "Document Waiting")
                    End If
                Next
            End If
        End If
        
    End Sub

    Private Sub SendMailNotificationDocApproved(ByVal packageid As String, ByVal userid As Integer)
        Dim usrinfo As UserProfileInfo = New UserController().GetUserLD(userid)
        Dim info As SiteInfo = controller.GetSiteInfoDetail(packageid)
        Dim docdetail As DocInfo = controller.GetDocDetail(Integer.Parse(HDDocid.Value))

        If usrinfo IsNot Nothing And info IsNot Nothing Then
            Dim sb As New StringBuilder
            Dim strSubject As String = String.Empty
            sb.Append("Dear " & usrinfo.Fullname & "<br/>")
            If docdetail IsNot Nothing Then
                sb.Append("Following detail of " & docdetail.DocName & " is approved by you at " & String.Format("{0:dd-MMM-yyyy hh:mm:ss}", Date.Now()) & "<br/>")
                strSubject = docdetail.DocName & " Approved"
            Else
                sb.Append("Following detail of document is waiting your approval/review <br/>")
                strSubject = "Document Waiting"
            End If

            sb.Append("Site ID: " & info.SiteNo & "<br/>")
            sb.Append("SiteName:" & info.SiteName & "<br/>")
            sb.Append("WorkpackageID: " & info.PackageId & "<br/>")
            sb.Append("PONO: " & info.PONO & "<br/>")
            sb.Append("<a href='https://sdmthree.nsnebast.com'>Click here</a>" & " to Login to e-BAST<br/>")
            sb.Append("Powered By EBAST" & "<br/><br/>")
            sb.Append("<img src='http://sdmthree.nsnebast.com/images/nsn-logo.gif' alt='companylogo' />")
            If docdetail IsNot Nothing Then
                objmail.SendMailUser(usrinfo.Email, sb.ToString(), strSubject)
            End If
        End If

    End Sub

    Private Sub SendMailNotificationRejection(ByVal wpid As String, ByVal docid As Integer, ByVal category As String, ByVal remarks As String)
        'Dim users As List(Of Common_NSNFramework.UserInfo) = DocController.GetBCCUsersNotification(BaseConfiguration.FORM_TYPE_SOAC_REJECTION, packageid)
        Dim userpic As List(Of UserProfileInfo) = controller.GetPICRejectionRelated(docid, wpid)
        Dim sb As New StringBuilder
        Dim info As SiteInfo = controller.GetSiteInfoDetail(wpid)
        Dim docdetail As DocInfo = controller.GetDocDetail(Integer.Parse(HDDocid.Value))
        Dim dtuser As DataTable = objutil.ExeQueryDT("select name, signtitle from ebastusers_1 where usr_id=" & CommonSite.UserId.ToString(), "dtusers")
        Dim companyname As String = String.Empty
        If CommonSite.UserType.ToLower().Equals("n") Then
            companyname = "Nokia Solutions and Networks"
        ElseIf CommonSite.UserType.ToLower().Equals("c") Then
            companyname = "H3I"
        ElseIf CommonSite.UserType.ToLower().Equals("h") Then
            companyname = "Huawei"
        Else
            companyname = "Subcontractor"
        End If
        'Dim companyname As String = IIf(CommonSite.UserType.ToLower().Equals("n"), "Nokia Siemens Networks", "Telkomsel")
        If userpic.Count() > 0 Then
            For Each pic As UserProfileInfo In userpic
                'sb.Append("Dear " & pic.Fullname & ",<br/>")
                sb.Append("Dear Sir/Madam,<br/>")
                sb.Append("Following detail " & docdetail.DocName & " is Rejected by " & dtuser.Rows(0).Item(0).ToString() & " as " & dtuser.Rows(0).Item(1).ToString() & "," & companyname & "<br/>")
                sb.Append("PONO: " & info.PONO & "<br/>")
                sb.Append("POName: " & info.POName & "<br/>")
                sb.Append("Site ID: " & info.SiteNo & "<br/>")
                sb.Append("SiteName: " & info.SiteName & "<br/>")
                sb.Append("WorkpackageID: " & info.PackageId & "<br/>")
                sb.Append("Scope: " & info.Scope & "<br/>")
                sb.Append("Category :  <b>" & category & "</b><br>")
                sb.Append("Remarks :  <b>" & remarks & "</b><br>")
                sb.Append("<a href='http://hcptdemo.nsnebast.com'>Click here</a>" & " to Login to e-RFT<br/>")
                sb.Append("Powered By EBAST" & "<br/><br/>")
                sb.Append("<img src='http://hcptdemo.nsnebast.com/images/nsn-logo.gif' alt='companylogo' />")
                objmail.SendMailUser(pic.Email, sb.ToString(), docdetail.DocName & " Rejection")
                sb = New StringBuilder
            Next
        End If
        


        'If users.Count() > 0 Then
        '    Dim sb2 As New StringBuilder
        '    sb2.Append("Dear Sir/Madam,<br/><br/>")
        '    sb2.Append("For Your info,<br/>")
        '    sb2.Append("Following detail SOAC Document is Rejected by " & dtuser.Rows(0).Item(0).ToString() & " as " & dtuser.Rows(0).Item(1).ToString() & "," & companyname & "<br/>")
        '    sb2.Append("Site ID PO: " & info.SiteIdPO & "<br/>")
        '    sb2.Append("SiteName PO: " & info.SiteNamePO & "<br/>")
        '    sb2.Append("PONO: " & info.PONO & "<br/>")
        '    sb2.Append("PO Date: " & String.Format("{0:dd-MMMM-yyyy}", soacinfo.PORefNoDate) & "<br/>")
        '    sb2.Append("On Air Date: " & String.Format("{0:dd-MMMM-yyyy}", soacinfo.OnAirDate) & "<br/>")
        '    sb2.Append("Category :  <b>" & category & "</b><br>")
        '    sb.Append("Remarks :  <b>" & remarks & "</b><br>")
        '    sb2.Append("<a href='https://telkomsel.nsnebast.com'>Click here</a>" & " to go to e-BAST<br/>")
        '    sb2.Append("Powered By EBAST" & "<br/>")
        '    sb2.Append("<img src='https://www.telkomsel.nsnebast.com/images/CRCOSubHeader/nsn-logo.gif' alt='companylogo' />")
        '    objmail.SendMailUserGroup(users, sb2.ToString(), "SOAC Rejection")
        'End If

    End Sub
	
	Private Function DocIsPartofApprovalSheet(ByVal docid As Integer) As Boolean
        Dim isPart As Boolean = False
        Dim getdocapprovalsheet As String() = ConfigurationManager.AppSettings("docwithapprovalsheet").Split(",")
        Dim totalIndexApprovalsheet As Integer = getdocapprovalsheet.Length
        Dim incrementindex As Integer = 0
        While incrementindex < totalIndexApprovalsheet
            If (getdocapprovalsheet(incrementindex).Equals(docid.ToString())) Then
                isPart = True
            End If
            incrementindex += 1
        End While

        Return isPart
    End Function

#End Region

#End Region

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

End Class


