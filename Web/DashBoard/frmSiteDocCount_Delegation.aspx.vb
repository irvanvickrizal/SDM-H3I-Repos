Imports BusinessLogic
Imports Common
Imports Entities
Imports System.Data
Imports NSNCustomizeConfiguration
Imports Common_NSNFramework


Partial Class DashBoard_frmSiteDocCount_Delegation
    Inherits System.Web.UI.Page

    Dim dt As New DataTable
    Dim objBo As New BODashBoard
    Dim objdb As New DBUtil
    Dim objutils_nsn As New DBUtils_NSN

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If (Not String.IsNullOrEmpty(Request.QueryString("uid"))) Then
                BindData(Integer.Parse(Request.QueryString("uid")))
            End If
        End If
    End Sub

    Protected Sub btnViewAll_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnViewAll.ServerClick
        Dim isApproverReviewer As Boolean = False
        Dim roleid As Integer = objdb.ExeQueryScalar("select usrRole from ebastusers_1 where usr_id=" & Request.QueryString("uid"))
        If (roleid = 28 And CommonSite.UserId = 166) Then 'Agus Handayana Only
            isApproverReviewer = True
        End If
        If isApproverReviewer = True Then
            BindTaskApprover_Reviewer(True, Integer.Parse(Request.QueryString("uid"))) 'Agus Handayana Only'
        Else
            If (roleid = 28 Or roleid = 73 Or roleid = 105) Then
                BindTaskApproverReviewer(True, Integer.Parse(Request.QueryString("uid")))
            ElseIf (roleid = 11 Or roleid = 41 Or roleid = 23 Or roleid = 29 Or roleid = 106 Or roleid = 112 Or roleid = 113) Then ' Approver User Type
                BindTaskApprover(True, Integer.Parse(Request.QueryString("uid")))
            ElseIf (roleid = 43 Or roleid = 48 Or roleid = 49 Or roleid = 50 Or _
                            roleid = 60 Or roleid = 63 Or roleid = 74 Or roleid = 75) Then
                BindTaskReviewer(True, Integer.Parse(Request.QueryString("uid")))
            ElseIf (roleid = ConfigurationManager.AppSettings("ATPROLEAPP")) Then
                BindTaskReviewerATP(False, Integer.Parse(Request.QueryString("uid")))
            Else
                dt = objdb.ExeQueryDT("exec uspDashBoardAgendaSiteDocCount2 " & Request.QueryString("uid") & "", "ds")
            End If
            GrdDocCount.DataSource = dt
            GrdDocCount.DataBind()
        End If
    End Sub

    Protected Sub BtnClose_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnClose.ServerClick
        'Response.Redirect("../frmDashboard_Temp.aspx")
        Select Case Session("User_Type")
            Case "N"
                Response.Redirect("../frmDashboard_Temp.aspx")
            Case "C"
                Response.Redirect("frmSiteAllTaskPending.aspx")
            Case "S"
                Response.Redirect("../frmDashboard_Temp.aspx")
        End Select
    End Sub

    Protected Sub DdlSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles DdlTaskUser.SelectedIndexChanged
        BindTaskApprover_Reviewer(False, Integer.Parse(Request.QueryString("uid")))
    End Sub

    Protected Sub GrdDocCount_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs) Handles GrdDocCount.RowCommand
        If e.CommandName.Equals("opendetail") Then
            Dim row As GridViewRow = DirectCast(DirectCast(e.CommandSource, LinkButton).NamingContainer, GridViewRow)
            Dim siteno As Label = DirectCast(row.Cells(6).FindControl("LblSiteNo"), Label)
            Dim tskid As Label = DirectCast(row.Cells(6).FindControl("LblTskId"), Label)
            Dim packageid As Label = DirectCast(row.Cells(6).FindControl("LblPackageId"), Label)

            If Not siteno Is Nothing And Not tskid Is Nothing And Not packageid Is Nothing Then
                'LblResult.Text = siteno.Text & "--" & tskid.Text & "--" & packageid.Text
                Response.Redirect("frmDocApproved_Delegation.aspx?id=" & siteno.Text & "&TId=" & tskid.Text & "&wpid=" & packageid.Text & "&doctype=common&uid=" & Request.QueryString("uid"))
            End If
        End If
    End Sub

#Region "Custom Methods"
    Private Sub BindData(ByVal userid As Integer)
        Dim roleid As Integer = objdb.ExeQueryScalar("select usrRole from ebastusers_1 where usr_id=" & userid.ToString())
        Dim isApproverReviewer As Boolean = False
        If (roleid = 28 And roleid = 166) Then
            isApproverReviewer = True
        End If
        If isApproverReviewer = True Then 'Agus handayana only
            Session("tskUser") = "0"
            BindTaskApprover_Reviewer(False, userid)
        Else
            If (roleid = ConfigurationManager.AppSettings("ATPROLEAPP")) Then 'Update ATP Expired Date
                objutils_nsn.UpdateATPOnSiteExpiredDateByUserRequest(CommonSite.UserId, ConfigurationManager.AppSettings("ATP"))
            End If

            Dim isDashboardRedirect = False
            If (roleid = 28 Or roleid = 73 Or roleid = 105) Then
                BindTaskApproverReviewer(False, userid)
            ElseIf (roleid = 11 Or roleid = 41 Or roleid = 23 Or roleid = 29 Or roleid = 79 Or roleid = 106 Or roleid = 107 Or roleid = 108 Or roleid = 112 Or roleid = 113) Then
                BindTaskApprover(False, userid)
            Else
                If (roleid = 1 Or roleid = 83 Or roleid = 40 Or roleid = 45 Or _
                            roleid = 65 Or roleid = 80 Or roleid = 18 Or roleid = 88) Then
                    isDashboardRedirect = True
                ElseIf (roleid = 43 Or roleid = 48 Or roleid = 49 Or roleid = 50 Or _
                roleid = 60 Or roleid = 63 Or roleid = 74 Or roleid = 75) Then
                    BindTaskReviewer(False, userid)
                Else
                    If roleid <> ConfigurationManager.AppSettings("ATPROLEAPP") Then
                        dt = objdb.ExeQueryDT("exec uspDashBoardAgendaSiteDocCount " & userid & "", "ds")
                    End If

                    If dt.Rows.Count = 0 Then
                        isDashboardRedirect = True
                    End If
                End If
            End If

            If (isDashboardRedirect = True) Then
                'Response.Redirect("../frmDashboard.aspx")
                'Response.Redirect("../frmDashboard_Temp.aspx")
                Select Case Session("User_Type")
                    Case "N"
                        Response.Redirect("../frmDashboard_Temp.aspx")
                    Case "C"
                        Response.Redirect("frmSiteAllTaskPending_Delegation.aspx")
                    Case "S"
                        Response.Redirect("../frmDashboard_Temp.aspx")
                End Select
            Else
                GrdDocCount.DataSource = dt
                GrdDocCount.DataBind()
            End If
        End If
    End Sub

    Sub BindTaskApprover_Reviewer(ByVal isViewedAll As Boolean, ByVal userid As Integer)
        PnlTaskUser.Visible = True
        LblTaskPendingApprover.Text = TaskPendingApproverCount(userid)
        LblTaskPendingReviewer.Text = TaskPendingReviewerCount(userid)
        DdlTaskUser.Visible = True
        Dim strSP As String = "exec uspDashBoardAgendaSiteDocCount"
        If (DdlTaskUser.SelectedValue.ToString().Equals("1")) Then
            strSP += "_Approver"
        Else
            strSP += "_Reviewer"
        End If

        If (isViewedAll = True) Then
            strSP += "_All"
        End If
        dt = objdb.ExeQueryDT(strSP & " " & userid & "", "ds")

        'Session("tskUser") = Convert.ToString(DdlTaskUser.SelectedValue)

        GrdDocCount.DataSource = dt
        GrdDocCount.DataBind()
        'If dt.Rows.Count = 0 Then
        '    Response.Redirect("../frmDashboard.aspx")
        'End If

        If (LblTaskPendingApprover.Text.Equals("0") And LblTaskPendingReviewer.Text.Equals("0")) Then
            'Response.Redirect("../frmDashboard_Temp.aspx")
            Select Case Session("User_Type")
                Case "N"
                    Response.Redirect("../frmDashboard_Temp.aspx")
                Case "C"
                    Response.Redirect("frmSiteAllTaskPending_Delegation.aspx")
                Case "S"
                    Response.Redirect("../frmDashboard_Temp.aspx")
            End Select
        End If
    End Sub

    Sub BindTaskApproverReviewer(ByVal viewAll As Boolean, ByVal userid As Integer)
        Dim strSP As String = "exec uspDashBoardAgendaSiteDocCount_ApproverReviewer"
        If (viewAll = True) Then
            strSP += "_All"
        End If
        dt = objdb.ExeQueryDT(strSP & " " & userid & "", "ds")
        GrdDocCount.DataSource = dt
        GrdDocCount.DataBind()
        If dt.Rows.Count = 0 Then
            'Response.Redirect("../frmDashboard_Temp.aspx")
            Select Case Session("User_Type")
                Case "N"
                    Response.Redirect("../frmDashboard_Temp.aspx")
                Case "C"
                    Response.Redirect("frmSiteAllTaskPending_Delegation.aspx")
                Case "S"
                    Response.Redirect("../frmDashboard_Temp.aspx")
            End Select
        End If
    End Sub

    Sub BindTaskApprover(ByVal viewAll As Boolean, ByVal userid As Integer)
        Dim strSP As String = "exec uspDashBoardAgendaSiteDocCount_Approver"

        If (viewAll = True) Then
            strSP += "_All"
        End If
        dt = objdb.ExeQueryDT(strSP & " " & userid & "", "ds")
        GrdDocCount.DataSource = dt
        GrdDocCount.DataBind()
        If dt.Rows.Count = 0 Then
            'Response.Redirect("../frmDashboard_Temp.aspx")
            Select Case Session("User_Type")
                Case "N"
                    Response.Redirect("../frmDashboard_Temp.aspx")
                Case "C"
                    Response.Redirect("frmSiteAllTaskPending_Delegation.aspx")
                Case "S"
                    Response.Redirect("../frmDashboard_Temp.aspx")
            End Select
        End If
    End Sub
    ' Irvan New Code Bind only Reviewer
    Sub BindTaskReviewer(ByVal viewAll As Boolean, ByVal userid As Integer)
        ' ATP Reviewer Role = 76
        Dim strSP As String = "exec uspDashBoardAgendaSiteDocCount_Reviewer"

        If (viewAll = True) Then
            strSP += "_All"
        End If
        dt = objdb.ExeQueryDT(strSP & " " & userid & "", "ds")
        GrdDocCount.DataSource = dt
        GrdDocCount.DataBind()
        If dt.Rows.Count = 0 Then
            'Response.Redirect("../frmDashboard_Temp.aspx")
            Select Case Session("User_Type")
                Case "N"
                    Response.Redirect("../frmDashboard_Temp.aspx")
                Case "C"
                    Response.Redirect("frmSiteAllTaskPending_Delegation.aspx")
                Case "S"
                    Response.Redirect("../frmDashboard_Temp.aspx")
            End Select
        End If
    End Sub

    Sub BindTaskReviewerATP(ByVal viewAll As Boolean, ByVal userid As Integer)
        Dim strSP As String = "exec uspDashBoardAgendaSiteDocCount_ReviewerATP"
        If (viewAll = True) Then
            strSP += "_All"
        End If
        dt = objdb.ExeQueryDT(strSP & " " & userid & "", "ds")
        GrdDocCount.DataSource = dt
        GrdDocCount.DataBind()
        If dt.Rows.Count = 0 Then
            Response.Redirect("frmSiteAllTaskPending_Delegation.aspx")
        End If
    End Sub
    
#End Region

End Class
