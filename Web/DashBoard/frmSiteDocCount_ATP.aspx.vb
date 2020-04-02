Imports System
Imports System.Data
Imports Common
Imports Common_NSNFramework
Imports BusinessLogic

Partial Class DashBoard_frmSiteDocCount_ATP
    Inherits System.Web.UI.Page
    Dim objBo As New BODashBoard
    Dim objdb As New DBUtil
    Dim objutils_nsn As New DBUtils_NSN
    Dim dt As DataTable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            If CommonSite.RollId = ConfigurationManager.AppSettings("ATPROLEAPP") Then
                objutils_nsn.UpdateATPOnSiteExpiredDateByUserRequest(CommonSite.UserId, ConfigurationManager.AppSettings("ATP"))
                BindTaskReviewerATP(False)
                BindTaskReviewerATPOnSite(False)
            End If
        End If
    End Sub

    Protected Sub gvDocCountATPOnSiteRowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvDocCountATPOnSite.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            Dim lblInvitationDate As Label = CType(e.Row.FindControl("LblInvitationDate"), Label)
            If String.IsNullOrEmpty(lblInvitationDate.Text) = True Then
                Dim lbl As Label = CType(e.Row.FindControl("LblDocStatus"), Label)
                If lbl.Text <> "Reject" Then
                    lbl.Text = "Pending"
                End If
            Else
                lblInvitationDate.Text = String.Format("{0:dd-MMM-yyyy}", lblInvitationDate.Text)
            End If
        End If
    End Sub
    Protected Sub BtnViewAllClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnViewAll.Click
        BindTaskReviewerATP(True)
        BindTaskReviewerATPOnSite(False)
    End Sub

    Protected Sub BtnGoToDashboardClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnGoToDashboard.Click
        Select Case Session("User_Type")
            Case "N"
                Response.Redirect("../frmDashboard_Temp.aspx?from=atppending")
            Case "C"
                Response.Redirect("frmSiteAllTaskPending.aspx")
            Case "S"
                Response.Redirect("../frmDashboard_Temp.aspx?from=atppending")
        End Select
        'Response.Redirect("../frmDashboard_Temp.aspx?from=atppending")
    End Sub

#Region "Custom Methods"

    Private Sub BindTaskReviewerATP(ByVal viewAll As Boolean)
        Dim strSP As String = "exec uspDashBoardAgendaSiteDocCount_ReviewerATP"
        If (viewAll = True) Then
            strSP += "_All"
        End If
        dt = objdb.ExeQueryDT(strSP & " " & CommonSite.UserId() & "", "ds")
        GrdDocCount.DataSource = dt
        GrdDocCount.DataBind()
        If dt.Rows.Count = 0 Then
            Select Case Session("User_Type")
                Case "N"
                    Response.Redirect("../frmDashboard_Temp.aspx")
                Case "C"
                    Response.Redirect("frmSiteAllTaskPending.aspx")
                Case "S"
                    Response.Redirect("../frmDashboard_Temp.aspx")
            End Select
        End If
    End Sub

    Private Sub BindTaskReviewerATPOnSite(ByVal viewAll As Boolean)
        Dim dtPendingATPOnSite As New DataTable
        Dim strSP As String = "exec uspDashBoardAgendaSiteDocCount_ReviewerATPOnSite"
        If (viewAll = True) Then
            strSP += "_All"
        End If
        dtPendingATPOnSite = objdb.ExeQueryDT(strSP & " " & CommonSite.UserId() & "", "ds")
        gvDocCountATPOnSite.DataSource = dtPendingATPOnSite
        gvDocCountATPOnSite.DataBind()
        If dt.Rows.Count = 0 And dtPendingATPOnSite.Rows.Count = 0 Then
            Select Case Session("User_Type")
                Case "N"
                    Response.Redirect("../frmDashboard_Temp.aspx")
                Case "C"
                    Response.Redirect("frmSiteAllTaskPending.aspx")
                Case "S"
                    Response.Redirect("../frmDashboard_Temp.aspx")
            End Select
            'Response.Redirect("../frmDashboard_Temp.aspx")
        End If
    End Sub
#End Region
End Class
