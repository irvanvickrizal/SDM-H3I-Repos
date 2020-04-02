
Imports System.Data
Imports BusinessLogic
Imports Common
Imports Entities

Partial Class DirectCredential
    Inherits System.Web.UI.Page
    Dim dt As New DataTable
    Dim objbo As New BOUserSetup
    Dim objET As New ETMessageBoard
    Dim objBOM As New BOMessageBoard
    Dim objUtil As New DBUtil
    Dim hcptcontrol As New HCPTController
    Dim objmail As New TakeMail

    Dim generateCode As String
    Dim action As String
    Dim objApprovalData As DataRow

    Private Sub DirectCredential_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim data As String = Request.QueryString("id")

        If Not String.IsNullOrEmpty(data) Then
            Try
                Dim datas As String() = data.Split("|")
                generateCode = datas(0)
                action = datas(1)
                'Get Data based on generated code
                dt = objUtil.ExeQueryDT("select * from ApprovalCode where RStatus = 1 and Code = '" & generateCode & "' ", "data")
                If dt.Rows.Count > 0 Then
                    objApprovalData = dt.Rows(0)
                    'Validate the Workflow status
                    Dim totalData As Integer = objUtil.ExeQueryScalar("select count(*) total from HCPT_WFTransaction where sno = " & objApprovalData("NextSNO") & " and StartDateTime is not null and EndDateTime is null and RStatus = 2 and Status = 1")
                    If totalData > 0 Then
                        routeToMenu()
                    Else
                        'if total data is 0, validate if the document is rejected or not
                        Dim isRejected As Integer = objUtil.ExeQueryScalar("select count(*) total from HCPT_WFTransaction where sno = " & objApprovalData("NextSNO") & " and RStatus = 0")
                        If isRejected > 0 Then
                            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "WindowsClose('Please be note that your document has been rejected.');", True)
                        Else
                            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "WindowsClose('Please be note that your document has been approved/reviewed.');", True)
                        End If
                        'Deactivate code to prevent keep access on this page if the document do approval or notification that the document is rejected/approved/reviewed
                        deactivateApprovalCode()
                    End If
                Else
                    ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "WindowsClose('Sorry, your reference code is expired.');", True)
                End If
            Catch ex As Exception
                hcptcontrol.ErrorLogInsert(150, "DirectCredential Load()", ex.Message.ToString(), "DirectCredential Class")
            End Try
        End If

    End Sub

    Private Sub routeToMenu()
        'Get pending doc
        dt = objUtil.ExeQueryDT("exec HCPT_uspTrans_GetTaskPending " & objApprovalData("USRID") & ", " & objApprovalData("NextSNO") & "", "ds")
        Select Case action
            Case CommonSite.Approval_Action
                approveDocument(dt)
            Case CommonSite.Detail_Pending_Approval_Action
                pendingDocumentApprovalPage(dt)
        End Select
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

    Private Sub MilestoneUpdate(ByVal info As DOCTransactionInfo)
        Dim info2 As DOCTransactionInfo = hcptcontrol.WFTransaction_LD(info.SNO)
        If hcptcontrol.GetLastTaskBaseWorkflowGrp(Integer.Parse(info.WFID), info2.UGPID) = info.TaskId Then
            If info2.UGPID = 1 Then 'means nsn submission
                hcptcontrol.Milestone_IU(info.SiteInf.PackageId, Integer.Parse(info.DocInf.DocId), Date.Now, Nothing)
            ElseIf info2.UGPID = 4 Then 'means customer approved'
                hcptcontrol.Milestone_IU(info.SiteInf.PackageId, Integer.Parse(info.DocInf.DocId), Nothing, Date.Now)
            End If
        End If
    End Sub

    Private Function GenerateApprovalSheetATPChecking(ByVal info As DOCTransactionInfo) As Boolean
        Dim info2 As DOCTransactionInfo = hcptcontrol.WFTransaction_LD(info.SNO)
        If info2.UGPID = 4 Then 'means customer approval'
            If hcptcontrol.GetLastTaskBaseWorkflowGrp(info.WFID, info2.UGPID) = info.TaskId Then
                Dim objAtpApproval As New ATPApprovalSheet(info)
                If objAtpApproval.generateATPApprovalSheet() = False Then
                    hcptcontrol.WFTransaction_LastRollback(info.SNO, objApprovalData("RoleID"), objApprovalData("USRID"))
                    Return False
                End If
            End If
        End If
        Return True
    End Function

    Private Sub SendMailNotification(ByVal packageid As String, ByVal docId As Integer)
        'Dim users As List(Of UserProfileInfo) = doccontroller.GetNextDocPIC(packageid, wfid, tskid)
        Dim users As List(Of UserProfileInfo) = hcptcontrol.GetNextPIC(packageid, docId)
        Dim info As SiteInfo = hcptcontrol.GetSiteInfoDetail(packageid)
        Dim docdetail As DocInfo = hcptcontrol.GetDocDetail(docId)
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

    Private Sub SendMailNotificationDocApproved(ByVal packageid As String, ByVal userid As Integer, ByVal docId As Integer)
        Dim usrinfo As UserProfileInfo = New UserController().GetUserLD(userid)
        Dim info As SiteInfo = hcptcontrol.GetSiteInfoDetail(packageid)
        Dim docdetail As DocInfo = hcptcontrol.GetDocDetail(docId)

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

    Private Function getCredential() As Boolean
        Dim pstatus As String = objUtil.ExeQueryScalar("exec uspValidatePasswordDateDirectCredential " & objApprovalData("USRID") & "")
        Select Case pstatus
            Case 7
                If CInt(objApprovalData("USRID")) > 0 Then
                    ActivityLog_I(CInt(objApprovalData("USRID")), "Login succeed through direct credential")
                End If
            Case Else
                ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "WindowsClose('Your user login is expired, please contact Administrator');", True)
        End Select

        dt = objUtil.ExeQueryDT("exec uspValidateLoginDirectCredential '" & objApprovalData("USRID") & "'", "tbl")
        If dt.Rows.Count > 0 Then
            If dt.Rows(0).Item(0).ToString = "valid" Then '@@@@@@123
                Dim dr As DataRow = dt.Rows(0)
                If CInt(objApprovalData("USRID")) > 0 Then
                    Session("User_Login") = dr.Item("usrlogin").ToString
                    Session("User_Pwd") = dr.Item("usrPassword").ToString
                    Session("User_Name") = dr.Item("Name").ToString
                    Session("User_Type") = dr.Item("USRType").ToString
                    Session("SRCId") = dr.Item("SRCID").ToString
                    Session("User_Id") = dr.Item("USR_Id").ToString
                    Session("Role_Id") = dr.Item("UsrRole").ToString
                    Session("Area_Id") = dr.Item("ARA_Id").ToString
                    Session("Region_Id") = dr.Item("RGN_Id").ToString
                    Session("Zone_Id") = dr.Item("ZN_Id").ToString
                    Session("Site_Id") = dr.Item("Site_Id").ToString
                    Session("FLogin") = dr.Item("FirstTime_Login").ToString
                    Session("lvlcode") = dr.Item("LVLCode").ToString
                    Session("PType") = "2G"
                    If UCase(Session("User_Login")) = "SYSADMIN" Then
                        Session("Page_size") = 30
                        Session("User_Type") = Constants.User_Type_NSN
                        Session("Role_Id") = Constants.Sysadmin_RoleID
                    Else
                        Session("Page_size") = 15
                    End If
                    ActivityLog_I(CommonSite.UserId, "Login succeed through direct credential")
                    If Session("FLogin") = True Then
                        Return True
                    End If
                Else
                End If
            End If
        End If
        Return False
    End Function

    Private Sub pendingDocumentApprovalPage(ByVal tblData As DataTable)
        If getCredential() = True Then
            If tblData.Rows.Count > 0 Then
                Session("RoutingUrl") = "~/Dashboard/frmDocApproved.aspx?Id=" & objApprovalData("SiteID") & "&TId=" & tblData.Rows(0)("tsk_id") & "&wpid=" & objApprovalData("WPID") & "&doctype=common"
                Response.Redirect("Main.aspx")
            Else
                ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "WindowsClose('Sorry, can not find your document');", True)
            End If
        Else
            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "WindowsClose('Credential is not valid');", True)
        End If
    End Sub

    Public Sub approveDocument(ByVal tblData As DataTable)
        Try
            If tblData.Rows.Count > 0 Then
                'Get Task Name
                Dim taskName As String = hcptcontrol.GetTaskDesc(CInt(tblData(0)("tsk_id")))
                'Get the data
                dt = objUtil.ExeQueryDT("exec HCPT_uspSiteDocListTaskNewSp " & objApprovalData("USRID") & ",'" & objApprovalData("WPID") & "','" & taskName & "', " & objApprovalData("NextSNO") & " ", "data")
                Dim info As New DOCTransactionInfo
                If dt.Rows.Count > 0 Then
                    'Put the data to DOCTransactionInfo object
                    info.SNO = dt.Rows(0)("sno").ToString()
                    info.SiteInf.PackageId = dt.Rows(0)("workpkgid").ToString()
                    info.WFID = dt.Rows(0)("WF_Id").ToString()
                    info.TaskId = dt.Rows(0)("Tsk_Id").ToString()
                    info.RoleInf.RoleId = objApprovalData("RoleID").ToString()
                    info.UserInf.UserId = objApprovalData("USRID")
                    info.DocInf.DocId = CInt(dt.Rows(0)("Doc_id"))
                    info.DocInf.DocName = dt.Rows(0)("DOCName").ToString()
                    info.SiteInf.Scope = dt.Rows(0)("scope").ToString()
                    info.SiteInf.SiteNo = dt.Rows(0)("siteno").ToString()
                    info.Media = "Email"
                    If hcptcontrol.DocApproved(info) = True Then
                        MilestoneUpdate(info)
                        'Execute generate Approval sheet ATP Checking first. If got error will break the flow. If success will send email and execute other flows
                        If DocIsPartofApprovalSheet(info.DocInf.DocId) = True Then
                            If GenerateApprovalSheetATPChecking(info) = False Then
                                ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "WindowsClose('Fail to generate ATP Approval Sheet. Please contact the Administrator and perform mail approval again.');", True)
                                Exit Sub
                            End If
                        End If
                        objmail.SendApprovalMailNotification(info.SiteInf.PackageId, info.DocInf.DocId)
                        SendMailNotification(info.SiteInf.PackageId, info.DocInf.DocId)
                        SendMailNotificationDocApproved(objApprovalData("WPID"), objApprovalData("USRID"), info.DocInf.DocId)
                        Dim docdetail As DocInfo = hcptcontrol.GetDocDetail(Integer.Parse(info.DocInf.DocId))
                        ActivityLog_I(objApprovalData("USRID"), BaseConfiguration.Activity_Approved_Doc & " " & docdetail.DocName)
                        If ConfigurationManager.AppSettings("ssvdocid") = info.DocInf.DocId Then
                            hcptcontrol.DocRemarks(objApprovalData("WPID"), Guid.NewGuid().ToString(), "Accept via email approval", objApprovalData("USRID"), info.DocInf.DocId)
                        End If
                        'Deactivate code to prevent keep access on this page if the document do approval or notification that the document is rejected/approved/reviewed
                        deactivateApprovalCode()
                        ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "WindowsClose('Document successfully approved/reviewed');", True)
                    Else
                        ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "WindowsClose('Failed to approve/review document');", True)
                    End If
                End If
            Else
                ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "WindowsClose('Sorry, can not find your document');", True)
            End If
        Catch ex As Exception
            hcptcontrol.ErrorLogInsert(150, "approveDocument", ex.Message.ToString(), "DirectCredential Class")
        End Try
    End Sub

    Private Sub deactivateApprovalCode()
        objUtil.ExeNonQuery("update ApprovalCode set Rstatus = 0 where Code = '" & generateCode & "'")
    End Sub

#Region "Activity Log"
    Private Sub ActivityLog_I(ByVal userid As Integer, ByVal activitydesc As String)
        'Dim ipaddress As String = HttpContext.Current.Request.UserHostAddress
        Dim ipaddress As String = Me.Page.Request.ServerVariables("REMOTE_ADDR")
        Dim info As New UserActivityLogInfo
        info.UserId = userid
        info.IPAddress = ipaddress
        info.Description = activitydesc

        hcptcontrol.UserLogActivity_I(info)
    End Sub

    Private Function GetUserIdByUserLogin(ByVal usrLogin As String) As Integer
        Return hcptcontrol.GetUserIDBaseUserLogin(usrLogin)
    End Function
#End Region
End Class
