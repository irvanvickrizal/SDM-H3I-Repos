Imports Common_NSNFramework
Imports Common
Imports System.Collections.Generic

Partial Class DashBoard_AtpOnSiteInvitationAccept
    Inherits System.Web.UI.Page
    Dim objUtil As New DBUtil
    Dim dbutils As New DBUtils_NSN
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        If Page.IsPostBack = False Then
            BtnSubmit.Visible = False
            BindDataPendingInvitation()
        End If
    End Sub

    Protected Sub gvInvitationPendingRowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvInvitationPending.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            Dim lblPendingdate As Label = CType(e.Row.FindControl("LblPendingDate"), Label)
            Dim txtCalendar As TextBox = CType(e.Row.FindControl("TxtCalendar"), TextBox)
            If (String.IsNullOrEmpty(lblPendingdate.Text) = False) Then
                Dim pendingDate As DateTime = Convert.ToDateTime(lblPendingdate.Text)
                Dim dateInvitdef As DateTime = pendingDate.AddDays(5)
                txtCalendar.Text = String.Format("{0:dd-MMMM-yyyy}", dateInvitdef)
            End If
        End If
    End Sub

    Protected Sub BtnSubmitClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSubmit.Click
        SubmitAccept()
    End Sub

#Region "Custom Methods"
    Private Sub BindDataPendingInvitation()
        gvInvitationPending.DataSource = dbutils.GetATPOnSiteInvitationPending(CommonSite.UserId, ConfigurationManager.AppSettings("ATP"), Constants_NSN._Doc_Status_Accept)
        gvInvitationPending.DataBind()

        If (gvInvitationPending.Rows.Count > 0) Then
            BtnSubmit.Visible = True
        End If
    End Sub

    Private Sub SubmitAccept()
        Dim listInvitation As New List(Of ATPOnSiteInvitationInfo)
        Dim gvrow As GridViewRow
        Dim chk As CheckBox
        Dim LblSiteId As Label
        Dim txtCalendar As TextBox
        For Each gvrow In gvInvitationPending.Rows
            chk = gvrow.FindControl("ChkInvitation")
            txtCalendar = gvrow.FindControl("TxtCalendar")
            LblSiteId = gvrow.FindControl("LblSiteId")
            If chk.Checked = True And txtCalendar.Text <> "" Then
                Dim invitinfo As New ATPOnSiteInvitationInfo
                invitinfo.ExeInvitUser = CommonSite.UserId
                invitinfo.PackageId = gvrow.Cells(5).Text
                invitinfo.InvitationDate = DateTime.ParseExact(txtCalendar.Text, "dd-MMMM-yyyy", Nothing)
                invitinfo.ExecuteDate = DateTime.Now
                invitinfo.Sitename = gvrow.Cells(4).Text
                invitinfo.SiteNo = gvrow.Cells(2).Text
                invitinfo.PONO = gvrow.Cells(3).Text
                listInvitation.Add(invitinfo)
                dbutils.UpdateATPOnSiteInvitationDate(invitinfo)
                Dim auditinfo As New AuditInfo
                auditinfo.DocId = ConfigurationManager.AppSettings("ATP")
                auditinfo.PackageId = gvrow.Cells(5).Text
                auditinfo.SiteId = Integer.Parse(LblSiteId.Text)
                auditinfo.Status = 1
                auditinfo.pono = gvrow.Cells(3).Text
                auditinfo.Remarks = ""
                auditinfo.Category = ""
                auditinfo.Task = ConfigurationManager.AppSettings("ATPOnSiteInvitDate")
                auditinfo.InvitationDate = DateTime.ParseExact(txtCalendar.Text, "dd-MMMM-yyyy", Nothing)
                auditinfo.UserId = CommonSite.UserId
                auditinfo.RoleId = CommonSite.RollId
                dbutils.InsertAuditTrailATPOnSiteInvitationDate(auditinfo)
            End If
        Next
        If listInvitation.Count > 0 Then
            SendMailInvitation(listInvitation)
            BindDataPendingInvitation()
        End If
    End Sub

    Private Sub SendMailInvitation(ByVal listinvitation As List(Of ATPOnSiteInvitationInfo))
        Dim sbBody As New StringBuilder
        Dim rowCount As Integer = 1
        Dim packageid As String = String.Empty
        Dim phoneNo As String = objUtil.ExeQueryScalarString("select phoneNo from ebastusers_1 where usr_id=" & CommonSite.UserId & "")
        sbBody.Append("Dear Customer,<br/>Referring your request to perform on-site ATP, We would like to invite you to attend ATP at the below sites and schedules:<br/>")
        sbBody.Append("<table border='1'>")
        sbBody.Append("<tr style='background-color:Orange;Color:#ffffff;font-family:verdana;font-size:11pt;'><td>No.</td><td>Site</td><td>Sitename</td><td>PONO</td><td>Package Id</td><td>InvitationDate</td></tr>")
        For Each info As ATPOnSiteInvitationInfo In listinvitation
            Dim strInvitation As String = String.Format("{0:dd-MMMM-yyyy}", info.InvitationDate)
            sbBody.Append("<tr><td>" & rowCount & "</td><td>" & info.SiteNo & "</td><td>" & info.Sitename & "</td><td>" & info.PONO & "</td><td>" & info.PackageId & "</td><td>" & strInvitation & "</td></tr>")
            packageid = info.PackageId
            rowCount += 1
        Next
        sbBody.Append("</table> <br/><br/>")
        sbBody.Append("Thank you for your attention.<br/><br/>")
        sbBody.Append("Warm Regards <br/><br/>" & CommonSite.UserName & " - Regional Team " & dbutils.GetUserRegion(CommonSite.UserId) & "<br/>" & phoneNo & "<br/>")
        sbBody.Append("<img src='http://www.telkomsel.nsnebast.com/images/nsn-logo.gif' alt='companylogo' />")
        Dim notif As New NotificationBase
        'notif.SendMailATPInvitation(dbutils.GetUserEmailInvitation(packageid, ConfigurationManager.AppSettings("ATP")), sbBody.ToString)
        Dim listusers As List(Of UserInfo) = dbutils.GetATPOnSiteLastCustomerReview(CommonSite.UserId, ConfigurationManager.AppSettings("ATPROLEAPP"))
        notif.SendMailATP(listusers, sbBody.ToString(), "ATP On-Site Invitation")
        notif.SendMessageATPOnSiteInvitation(listusers)
    End Sub

#End Region
End Class
