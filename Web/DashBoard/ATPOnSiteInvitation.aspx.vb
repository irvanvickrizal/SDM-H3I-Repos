Imports BusinessLogic
Imports Common
Imports System.Collections.Generic
Imports Entities
Imports System.Data
Imports NSNCustomizeConfiguration
Imports Common_NSNFramework
Partial Class DashBoard_ATPOnSiteInvitation
    Inherits System.Web.UI.Page
    Dim dt As New DataTable
    Dim objBo As New BODashBoard
    Dim objdb As New DBUtil
    Dim dbutils As New DBUtils_NSN
    Dim objmail As New TakeMail
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        If Page.IsPostBack = False Then
            BtnSubmit.Visible = False
            BindDataATPOnSitePendingInvitation()
        End If
    End Sub

    Protected Sub BtnSubmitClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSubmit.Click
        SubmitPendingInvitation()
    End Sub

    Protected Sub BtnSearchClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSearch.Click
        SubmitSearch()
    End Sub

    Protected Sub BtnClearClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnClear.Click
        SubmitClear()
    End Sub

#Region "Custom Methods"
    Private Sub BindDataATPOnSitePendingInvitation()
        gvPendingATPOnSiteInvitation.DataSource = dbutils.GetATPOnSiteInvitation(CommonSite.UserId, ConfigurationManager.AppSettings("ATP"), Constants_NSN._Doc_Status_Pending)
        gvPendingATPOnSiteInvitation.DataBind()

        If gvPendingATPOnSiteInvitation.Rows.Count > 0 Then
            BtnSubmit.Visible = True
        End If
    End Sub

    Private Sub SubmitSearch()
        Dim strQuery As String = String.Empty
        If String.IsNullOrEmpty(TxtPoNo.Text) = False Then
            If String.IsNullOrEmpty(TxtSiteNo.Text) = False Then
                If String.IsNullOrEmpty(TxtPackageId.Text) = False Then
                    strQuery = "where pono like '" & TxtPoNo.Text & "%' and site_no like '" & TxtSiteNo.Text & "%' and package_id in(" & TxtPackageId.Text & ")"
                Else
                    strQuery = "where pono like '" & TxtPoNo.Text & "%' and site_no like '" & TxtSiteNo.Text & "%'"
                End If
            Else
                strQuery = "where pono like '" & TxtPoNo.Text & "%'"
            End If
        ElseIf String.IsNullOrEmpty(TxtSiteNo.Text) = False Then
            If String.IsNullOrEmpty(TxtPoNo.Text) = False Then
                If String.IsNullOrEmpty(TxtPackageId.Text) = False Then
                    strQuery = "where pono like '" & DdlPoNo.SelectedValue.ToString & " " & TxtPoNo.Text & "%' and site_no like '" & TxtSiteNo.Text & "%' and package_id in(" & TxtPackageId.Text & ")"
                Else
                    strQuery = "where pono like '" & DdlPoNo.SelectedValue.ToString & " " & TxtPoNo.Text & "%' and site_no like '" & TxtSiteNo.Text & "%'"
                End If
            Else
                strQuery = "where site_no like '" & TxtSiteNo.Text & "%'"
            End If
        ElseIf String.IsNullOrEmpty(TxtPackageId.Text) = False Then
            If String.IsNullOrEmpty(TxtPoNo.Text) = False Then
                If String.IsNullOrEmpty(TxtSiteNo.Text) = False Then
                    strQuery = "where pono like '" & DdlPoNo.SelectedValue.ToString & " " & TxtPoNo.Text & "%' and site_no like '" & TxtSiteNo.Text & "%' and package_id in(" & TxtPackageId.Text & ")"
                Else
                    strQuery = "where pono like '" & DdlPoNo.SelectedValue.ToString & " " & TxtPoNo.Text & "%' and package_id in(" & TxtPackageId.Text & ")"
                End If
            Else
                strQuery = "where package_id in(" & TxtPackageId.Text & ")"
            End If
        End If
        If String.IsNullOrEmpty(strQuery) = False Then

        End If
    End Sub

    Private Sub SubmitClear()
        TxtSiteNo.Text = ""
        TxtPackageId.Text = ""
        TxtPoNo.Text = ""
    End Sub

    Private Sub SubmitPendingInvitation()
        Dim Chk As New CheckBox
        Dim Ddl As New DropDownList
        Dim LblSiteId As New Label
        Dim gvrow As GridViewRow
        Dim EmployeeID As String = ""
        Dim intTotalAccept As Integer = 0
        Dim intTotalReject As Integer = 0
        Dim strAccept As String = String.Empty
        Dim strReject As String = String.Empty
        Dim listatprejects As New List(Of AuditInfo)
        For Each gvrow In gvPendingATPOnSiteInvitation.Rows
            Chk = gvrow.FindControl("ChkInvitation")
            Ddl = gvrow.FindControl("DdlDecision")
            LblSiteId = gvrow.FindControl("LblSiteId")
            If Chk.Checked = True And Ddl.SelectedValue <> "-1" Then
                If (Ddl.SelectedValue.ToString() = "1") Then
                    If (intTotalAccept > 0 And String.IsNullOrEmpty(strAccept) = False) Then
                        strAccept += ", "
                    End If
                    strAccept += gvrow.Cells(5).Text
                    intTotalAccept += 1
                Else
                    If (intTotalReject > 0 And String.IsNullOrEmpty(strReject) = False) Then
                        strReject += ", "
                    End If
                    strReject += gvrow.Cells(5).Text
                    intTotalReject += 1
                    
                    Dim auditinfo As New AuditInfo
                    auditinfo.DocId = ConfigurationManager.AppSettings("ATP")
                    auditinfo.PackageId = gvrow.Cells(5).Text
                    auditinfo.SiteNo = gvrow.Cells(2).Text
                    auditinfo.SiteId = Convert.ToInt32(LblSiteId.Text)
                    auditinfo.Status = 1
                    auditinfo.pono = gvrow.Cells(3).Text
                    auditinfo.Sitename = gvrow.Cells(4).Text
                    auditinfo.Remarks = ""
                    auditinfo.Category = ""
                    auditinfo.Task = ConfigurationManager.AppSettings("ATPOnSiteReject")
                    auditinfo.UserId = CommonSite.UserId
                    auditinfo.RoleId = CommonSite.RollId
                    auditinfo.InvitationDate = DateTime.Now
                    dbutils.InsertAuditTrailATPOnSiteInvitationDate(auditinfo)
                    listatprejects.Add(auditinfo)
                End If
            End If
        Next
        If (String.IsNullOrEmpty(strAccept) = False) Then
            ATPAccepted(strAccept)
        End If

        If (String.IsNullOrEmpty(strReject) = False) Then
            ATPRejected(strReject)
            SendMailATPOnSiteReject(listatprejects)
        End If

        If (String.IsNullOrEmpty(strAccept) = False) Or (String.IsNullOrEmpty(strReject) = False) Then
            BindDataATPOnSitePendingInvitation()
        End If

    End Sub

    Private Sub ATPAccepted(ByVal strCollectWPid As String)
        dbutils.UpdateATPOnSiteStatus(strCollectWPid, ConfigurationManager.AppSettings("ATP"), True)
    End Sub


    Private Sub ATPRejected(ByVal strCollectWPid As String)
        dbutils.UpdateATPOnSiteStatus(strCollectWPid, ConfigurationManager.AppSettings("ATP"), False)
        objdb.ExeQuery("exec uspUpdateATPOnSiteTransReject '" & strCollectWPid & "'," & ConfigurationManager.AppSettings("ATP"))
    End Sub

    Private Sub SendMailATPOnSiteReject(ByVal listatprejects As List(Of AuditInfo))
        Dim sbBody As New StringBuilder
        Dim info As New AuditInfo
        Dim countRow As Integer = 1
        Dim phoneNo As String = objdb.ExeQueryScalarString("select phoneNo from ebastusers_1 where usr_id=" & CommonSite.UserId & "")
        'Body Message'
        sbBody.Append("Dear Customer, <br/>Referring to your request to perform on-site ATP for the below sites, we are regret to not able to fulfill your request for it has reached the maximum percentage of sites  to be visited / on-site ATP.  :<br/><br/>")
        sbBody.Append("<table border=1><tr style='background-color:Orange;Color:#ffffff;font-family:verdana;font-size:11pt;'><td>No.</td><td>SiteNo</td><td>SiteName</td><td>PoNo</td><td>PackageId</td></tr>")
        For Each info In listatprejects
            sbBody.Append("<tr><td>" & countRow & "</td><td>" & info.SiteNo & "</td><td>" & info.Sitename & "</td><td>" & info.pono & "</td><td>" & info.PackageId & "</td></tr>")
            countRow += 1
        Next
        sbBody.Append("</table>")
        sbBody.Append("Thank you for your attention.<br/><br/>")
        sbBody.Append("Warm Regards <br/><br/>" & CommonSite.UserName & " - Regional Team " & dbutils.GetUserRegion(CommonSite.UserId) & "<br/>" & phoneNo & "<br/>")
        sbBody.Append("<img src='http://www.telkomsel.nsnebast.com/images/nsn-logo.gif' alt='companylogo' />")
        Dim notif As New NotificationBase
        objmail.SendMailATP(dbutils.GetATPOnSiteLastCustomerReview(CommonSite.UserId, ConfigurationManager.AppSettings("ATPROLEAPP")), sbBody.ToString(), "ATP On-Site Rejection")
    End Sub
#End Region
End Class
