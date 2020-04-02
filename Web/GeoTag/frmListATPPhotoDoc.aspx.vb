Imports System.Collections.Generic
Imports System.IO
Imports Common
Imports BusinessLogic
Imports System.Data
Imports Entities
Imports Common_NSNFramework
Imports CRFramework


Partial Class GeoTag_frmListATPPhotoDoc
    Inherits System.Web.UI.Page

    Dim controller As New ATPGeoTagController
    Dim objdbutil As New DBUtil
#Region "NSNFramework"
    Dim dbutils_nsn As New DBUtils_NSN
#End Region
    Dim objbositedoc As New BOSiteDocs
    Dim objDo As New ETWFTransaction
    Dim objET As New ETAuditTrail
    Dim objmail As New TakeMail
    Dim objetsitedoc As New ETSiteDoc
    Dim crcontrol As New CRController
    Dim hcptcontrol As New HCPTController
    
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        If Not Page.IsPostBack Then
            BindData("TI2G", PartnerController.GetSubconIdByUser(CommonSite.UserId), ConfigurationManager.AppSettings("ATP"))
        End If
    End Sub

    Protected Sub GbPhotoListDoc_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles GvPhotoListDoc.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim viewdoclink As HtmlAnchor = CType(e.Row.FindControl("viewdoclink"), HtmlAnchor)
            Dim LblChecklistStatus As Label = CType(e.Row.FindControl("LblChecklistStatus"), Label)
            Dim LblSWID As Label = CType(e.Row.FindControl("LblSWID"), Label)
            Dim imgPDFUpload As ImageButton = CType(e.Row.FindControl("imgPDFUpload"), ImageButton)
            Dim LblATPDocPhotoId As Label = CType(e.Row.FindControl("LblATPDocPhotoId"), Label)
            If Not viewdoclink Is Nothing And Not LblATPDocPhotoId Is Nothing Then
                'viewdoclink.HRef = "../fancybox_form/fb_viewPhotoDocument_ATP.aspx?id=" & LblATPDocPhotoId.Text
                viewdoclink.Attributes.Add("onclick", "window.open('../fancybox_form/fb_viewPhotoDocument_ATP.aspx?id=" & LblATPDocPhotoId.Text & "','mywindow','fullscreen=yes')")
            End If

            If Not LblSWID Is Nothing And Not imgPDFUpload Is Nothing Then
                If Convert.ToInt32(LblSWID.Text) = 0 Then
                    imgPDFUpload.Visible = False
                    LblChecklistStatus.Visible = True
                    LblChecklistStatus.Text = "NY Checklist Created"
                Else
                    imgPDFUpload.Visible = True
                    LblChecklistStatus.Visible = False
                End If
            End If

        End If
    End Sub

    Protected Sub GbPhotoListDoc_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs) Handles GvPhotoListDoc.RowCommand
        If e.CommandName.Equals("uploaddoc") Then
            Dim row As GridViewRow = DirectCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
            Dim siteno As String = row.Cells(1).Text
            Dim sitename As String = row.Cells(2).Text
            Dim pono As String = row.Cells(3).Text
            Dim packageid As String = row.Cells(4).Text
            Dim scope As String = row.Cells(5).Text
            If Not String.IsNullOrEmpty(packageid) Then
                BindUploadATPDocumentChecklist(e.CommandArgument.ToString(), packageid, siteno, sitename, pono, scope)
            End If
        End If
    End Sub

    Protected Sub BtnBackToList_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnBackToList.Click
        BindData(ConfigurationManager.AppSettings("ScopeType"), PartnerController.GetSubconIdByUser(CommonSite.UserId), ConfigurationManager.AppSettings("ATP"))
    End Sub

    Protected Sub BtnUploadChecklist_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnUploadChecklist.Click
        FileDocChecklistUpload(FUDocChecklist, hdnATPPhotoDocId.Value)
    End Sub

    Protected Sub ImgRemove_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs) Handles ImgRemove.Click
        DeleteDocChecklist(hdnATPPhotoDocId.Value)
    End Sub

    Protected Sub BtnMergeDoc_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnMergeDoc.Click
        MergeDocumentProcess(hdnATPPhotoDocId.Value, LblWPID.Text)
    End Sub

    Protected Sub LbtViewHistoricalMergeDoc_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LbtViewHistoricalMergeDoc.Click
        Response.Redirect("ATPDocAlreadyMerge.aspx")
    End Sub

#Region "Custom Methods"
    Private Sub BindData(ByVal scope As String, ByVal sconid As Integer, ByVal atpdocid As Integer)
        MvCorePanel.SetActiveView(VwPhotoListDoc)
        GvPhotoListDoc.DataSource = controller.GetListATPPhotoDocs(sconid, scope, atpdocid)
        GvPhotoListDoc.DataBind()
    End Sub

    Private Sub BindUploadATPDocumentChecklist(ByVal atpdocphotoid As String, ByVal packageid As String, ByVal siteno As String, ByVal sitename As String, ByVal pono As String, ByVal scope As String)
        MvCorePanel.SetActiveView(VwDocMergeDetail)
        hdnATPPhotoDocId.Value = atpdocphotoid
        LblPoNo.Text = pono
        LblScope.Text = scope
        LblSitename.Text = sitename
        LblSiteNo.Text = siteno
        LblWPID.Text = packageid
        viewatpphotodoclink.HRef = "../fancybox_form/fb_viewPhotoDocument_ATP.aspx?id=" & atpdocphotoid
        viewmergelog.HRef = "../fancybox_form/fb_ATPMergeDocViewLog.aspx?id=" & atpdocphotoid
        PanelViewDocChecking(hdnATPPhotoDocId.Value)
    End Sub

    Private Sub FileDocChecklistUpload(ByVal fuDoc As FileUpload, ByVal atpphotodocid As String)
        If fuDoc.HasFile Then
            Dim FileNamePath As String = String.Empty
            Dim FileNameOnly As String = String.Empty
            Dim ReFileName As String = String.Empty
            Dim ft As String = String.Empty
            Dim path As String = String.Empty

            ft = LblPoNo.Text & "\" & LblWPID.Text & "\" & "GeoTagPhoto\"
            Dim fpath As String = ConfigurationManager.AppSettings("BufferPath")
            path = ConfigurationManager.AppSettings("Fpath") & LblSiteNo.Text & "\" & ft
            Dim DocPath As String = ""
            Dim err As Boolean = False
            'Dim strResult As String = DOInsertTrans(packageid, path)

            FileNamePath = fuDoc.PostedFile.FileName
            FileNameOnly = System.IO.Path.GetFileName(fuDoc.PostedFile.FileName)
            ReFileName = Format(CDate(DateTime.Now), "ddMMyyyyHHss") & FileNameOnly
            Dim orgDocpath As String = LblSiteNo.Text & ft & ReFileName
            DocPath = LblSiteNo.Text & "\" & ft & Common.TakeFileUpload.ConvertAnyFormatToPDF(fuDoc, path)
            controller.ATPDocumentChecklist_IU(atpphotodocid, DocPath, CommonSite.UserId, True)
            PanelViewDocChecking(atpphotodocid)
        End If
    End Sub

    Private Sub DeleteDocChecklist(ByVal atpphotodocid As String)
        Dim info As GeoTagMergeDocumentInfo = controller.GetATPDocumentChecklist(atpphotodocid)
        Dim svrpath As String = ConfigurationManager.AppSettings("FPath") & info.OriginalDocPath
        If info IsNot Nothing Then
            If File.Exists(svrpath) Then
                File.Delete(svrpath)
            End If
            controller.ATPDocChecklistRemove(atpphotodocid)
            PanelViewDocChecking(atpphotodocid)
            Dim loginfo As New GeoTagMergeDocLogInfo
            loginfo.PreparationId = 0
            loginfo.PreparationStatus = "Remove Doc Checklist"
            loginfo.RoleId = CommonSite.RollId
            loginfo.UserId = CommonSite.UserId
            loginfo.ATPPhotoDocId = atpphotodocid
            controller.ATPMergeDocLog_I(loginfo)
        End If
    End Sub

    Private Sub PanelViewDocChecking(ByVal atpphotodocid As String)
        If controller.ATPDocChecklistIsAvailable(atpphotodocid) = True Then
            ImgRemove.Visible = True
            viewatpdocchecklistlink.Attributes.Clear()
            viewatpdocchecklistlink.Visible = True
            viewatpdocchecklistlink.Attributes.Add("onclick", "window.open('../fancybox_form/fb_viewChecklistDocument_ATP.aspx?id=" & atpphotodocid & "','mywindow','fullscreen=yes')")
            'viewatpdocchecklistlink.Visible = True
            'viewatpdocchecklistlink.HRef = "../fancybox_form/fb_viewChecklistDocument_ATP.aspx?id=" & atpphotodocid
            BtnMergeDoc.Visible = True
            LblATPDocChecklistStatus.Text = ""
            LblATPDocChecklistStatus.Visible = False
            BtnUploadChecklist.Visible = False
            FUDocChecklist.Visible = False
        Else
            LblATPDocChecklistStatus.Text = "[NY Uploaded]"
            FUDocChecklist.Visible = True
            BtnUploadChecklist.Visible = True
            viewatpdocchecklistlink.Visible = False
            ImgRemove.Visible = False
            BtnMergeDoc.Visible = False
        End If
    End Sub

    Private Sub MergeDocumentProcess(ByVal atpphotodocid As String, ByVal packageid As String)
        Dim atpphotodocinfo As GeoTagInfo = controller.GetATPPhotoDoc(atpphotodocid)
        Dim atpdocchecklistinfo As GeoTagMergeDocumentInfo = controller.GetATPDocumentChecklist(atpphotodocid)
        Dim siteinfo As CRFramework.SiteInfo = controller.GetSiteInfo(packageid)
        Dim filename As String = "NewATPDoc_" & siteinfo.SiteNo & "_" & Format(CDate(DateTime.Now), "ddMMyyyyHHss") & ".pdf"
        Dim strPath As String = ConfigurationManager.AppSettings("Fpath") & siteinfo.SiteNo & "\" & siteinfo.PONO & "\" & siteinfo.Scope & "\"
        Dim strDocPath As String = siteinfo.SiteNo & "\" & siteinfo.PONO & "\" & siteinfo.Scope & "\"
        Dim firstDocPath As String = ConfigurationManager.AppSettings("FPath") & atpdocchecklistinfo.OriginalDocPath
        Dim SecondDocPath As String = ConfigurationManager.AppSettings("FPath") & atpphotodocinfo.ATPDOCPath
        Dim isValid As Boolean = True

        If Not File.Exists(firstDocPath) Then
            LblMergeStatus.Text = "ATP Doc Checklist Not Found"
            isValid = False
        End If

        If Not File.Exists(SecondDocPath) Then
            LblMergeStatus.Text += " & ATP Photo Doc Not Found"
            isValid = False
        End If

        If Not Directory.Exists(strPath) Then
            Directory.CreateDirectory(strPath)
        End If

        If isValid = True Then
            Dim strResult As String = New NSNPDFController().MergePdfNew(firstDocPath, SecondDocPath, strPath & filename)
            If strResult.Equals("success") Then
                LblMergeStatus.ForeColor = Drawing.Color.Green
                LblMergeStatus.Text = "Merge of ATP Doc Checklist and ATP Photo Doc success"
                BtnMergeDoc.Visible = False
                Dim logmergeinfo As New GeoTagMergeDocLogInfo
                logmergeinfo.ATPPhotoDocId = atpphotodocid
                logmergeinfo.UserId = CommonSite.UserId
                logmergeinfo.RoleId = CommonSite.RollId
                logmergeinfo.PreparationStatus = "Document Merged"
                AuditMergeLog(logmergeinfo)
                uploaddocument(strDocPath & filename, 3, siteinfo.SiteId.ToString(), siteinfo.Version, siteinfo.PONO, siteinfo.SiteNo, siteinfo.Scope)
            Else
                LblMergeStatus.Text = strResult
                BtnMergeDoc.Visible = True
            End If
        Else
            LblMergeStatus.ForeColor = Drawing.Color.Red
        End If
        '
    End Sub

    Sub uploaddocument(ByVal docpath As String, ByVal keyval As Integer, ByVal siteid As String, ByVal vers As Integer, ByVal pono As String, ByVal siteno As String, ByVal scope As String)
        Dim bautok As Integer = 0
        Dim strResult As String = DOInsertTrans(siteid, ConfigurationManager.AppSettings("ATP"), vers, docpath, pono)
        If strResult.Equals("1") Then
            With objetsitedoc
                .SiteID = siteid
                .DocId = ConfigurationManager.AppSettings("ATP")
                .IsUploded = 1
                .Version = vers
                .keyval = keyval
                .DocPath = docpath
                .AT.RStatus = Constants.STATUS_ACTIVE
                .AT.LMBY = CommonSite.UserId
                .orgDocPath = docpath
                .PONo = pono
            End With
            objbositedoc.updatedocupload(objetsitedoc)

            chek4alldoc(siteid, vers, pono, siteno) ' for messaage to previous screen ' and saving final docupload date in reporttable
            'for BAUT
            ' bautok = objbositedoc.check4BAUTBAST(hdnsiteid.Value, hdnversion.Value, 1, ddlPO.SelectedItem.Text, Session("User_Name"))
            bautok = objdbutil.ExeQueryScalar("exec uspcheck4BAUTBAST " & siteid & " ," & vers & "," & 1 & ",'" & pono & "','" & Session("User_Name") & "'," & ConfigurationManager.AppSettings("BAUTID") & "," & ConfigurationManager.AppSettings("BASTID") & " ")
            'for BAST1
            'bastok = objbositedoc.check4BAUTBAST(hdnsiteid.Value, hdnversion.Value, 2, ddlPO.SelectedItem.Text, Session("User_Name"))
            AuditTrail(siteid, scope, pono)
            Dim info As New ATPDocWithGeoTagMergeInfo
            info.MergeDocPath = docpath
            info.ATPPhotoDocId = hdnATPPhotoDocId.Value
            info.UserId = CommonSite.UserId
            info.RoleId = CommonSite.RollId
            info.SCONID = CommonSite.SCRID
            info.IsUploaded = True
            DoInsertHistoricalDocMerge(info)
        End If

    End Sub

    Function DOInsertTrans(ByVal siteid As Int32, ByVal docid As Integer, ByVal version As Integer, ByVal strPath As String, ByVal pono As String) As String
        Dim wfid As Integer
        Dim dtNew As DataTable
        wfid = objdbutil.ExeQueryScalar("select wf_id from sitedoc where siteid='" & siteid & "' and docid=" & docid & " and version =" & version & "")
        If wfid <> 0 Then
            'Dim ss As String
            'ss = "select wfdid,wfid,sorder,grpcode,roleid,a.tsk_id,a.grpId,tt.tskname from twfdefinition A inner join tusertype B on a.grpid=b.grpid " & _
            '" inner join ttask as tt on tt.tsk_id=a.tsk_id where wfid=" & wfid & " order by wfdid asc"
            dtNew = crcontrol.GetWorkflowDetail(wfid)

            Dim aa As Integer = 0
            Dim status As Integer = 0
            Dim isSucceed As Boolean = True
            If dtNew.Rows.Count > 0 Then
                If hcptcontrol.WFTransaction_D(LblWPID.Text, docid) = True Then
                    Dim sorder As Integer
                    For aa = 0 To dtNew.Rows.Count - 1
                        'fillDetails()
                        sorder = dtNew.Rows(aa).Item("sorder")
                        Dim transinfo As New DOCTransactionInfo
                        transinfo.TaskId = Integer.Parse(dtNew.Rows(aa).Item("Tsk_id").ToString())
                        transinfo.SiteInf.PackageId = LblWPID.Text
                        transinfo.DocInf.DocId = docid
                        transinfo.WFID = wfid
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

                        transinfo.Xval = 0
                        transinfo.Yval = 0

                        If hcptcontrol.WFTransaction_I(transinfo) = False Then
                            isSucceed = False
                            Exit For
                        End If
                    Next
                End If
                If (docid = ConfigurationManager.AppSettings("ATP")) Then
                    SendATPMail(siteid, version, ConfigurationManager.AppSettings("ATP"), wfid)
                    controller.DocWithGeoTag_IU(docid, LblWPID.Text, CommonSite.UserId, True)
                End If
            End If
            Return "1"
        End If
        Return "0"
    End Function

    Sub fillDetails(ByVal siteid As String, ByVal version As String, ByVal docid As String)
        objDo.Site_Id = siteid
        objDo.SiteVersion = version
        objDo.DocId = docid
        objDo.AT.RStatus = Constants.STATUS_ACTIVE
        objDo.AT.LMBY = Session("User_Id")
    End Sub

    Sub AuditTrail(ByVal siteid As String, ByVal scope As String, ByVal pono As String)
        objET.PoNo = pono
        objET.SiteId = siteid
        objET.DocId = Integer.Parse(ConfigurationManager.AppSettings("ATP"))
        objET.Task = "1"
        objET.Status = "1"
        objET.Userid = CommonSite.UserId
        objET.Roleid = CommonSite.RollId
        dbutils_nsn.InsertAuditTrailATPNew(objET, LblWPID.Text)
    End Sub

    Private Sub DoInsertHistoricalDocMerge(ByVal info As ATPDocWithGeoTagMergeInfo)
        controller.ATPMergeDoc_IU(info)
    End Sub

    Private Sub SendATPMail(ByVal siteid As Int32, ByVal siteversion As Integer, ByVal docid As Integer, ByVal wfid As Integer)
        Dim sbBody As New StringBuilder
        Dim strEmail As String = String.Empty
        Dim strPhone As String = String.Empty
        Dim list As List(Of UserProfileInfo) = New GeneralController().GetUserBaseOnWFTransactionPending(docid, LblWPID.Text, wfid)
        Dim intcount As Integer = 0
        Dim semicoloncount As Integer = 0
        

        Dim users As New List(Of UserInfo)
        Dim count As Integer = 0
        Dim strUser As String = String.Empty
        For Each info As UserProfileInfo In list
            Dim uinfo As New UserInfo
            uinfo.Username = info.Fullname
            uinfo.Handphone = info.PhoneNo
            uinfo.Email = info.Email
            users.Add(uinfo)
            strUser = info.Fullname
            strEmail = info.Email
            strPhone = info.PhoneNo
            count += 1
        Next
        If count > 1 Then
            sbBody.Append("Dear Sir / Madam, <br/><br/>")
        Else
            If count = 1 Then
                sbBody.Append("Dear " & strUser & ", <br/><br/>")
            End If
        End If


        sbBody.Append("There is ATP document of " & LblPoNo.Text & "-" & LblSiteNo.Text & "-" & LblSitename.Text & "-" & LblScope.Text & "-" & LblWPID.Text & " Waiting on your approval " & "<br/><br />")
        sbBody.Append("<a href='http://hcptdemo.nsnebast.com'>Click here</a>" & " to login to e-RFT <br/>")
        sbBody.Append("Powered By E-RFT" & "<br/>")
        sbBody.Append("<img src='http://hcptdemo.nsnebast.com/images/nsn-logo.gif' alt='companylogo' />")
        Dim mailnotif As New TakeMail
        If count > 1 Then
            mailnotif.SendMailUserGroup(users, sbBody.ToString(), "ATP Document Pending")
        Else
            If Not String.IsNullOrEmpty(strEmail) Then
                mailnotif.SendMailUser(strEmail, sbBody.ToString(), "ATP Document Pending")
            End If
        End If

        'Dim notif As New NotificationBase
        'notif.SendMailReviewerATP(list, siteid, siteversion)
    End Sub

    Public Sub chek4alldoc(ByVal siteid As String, ByVal version As String, ByVal pono As String, ByVal siteno As String)
        Dim i As Integer
        i = objbositedoc.chek4alldoc(siteid, version)
        If i = 0 Then
            hdnready4baut.Value = 1
            'insert to new table for report as final document uploaded
            objbositedoc.uspRPTUpdate(pono, siteno)
        Else
            hdnready4baut.Value = 0
        End If
    End Sub

    Public Sub AuditMergeLog(ByVal info As GeoTagMergeDocLogInfo)
        controller.ATPMergeDocLog_I(info)
    End Sub
#End Region

End Class
