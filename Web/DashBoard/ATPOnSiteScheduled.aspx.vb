Imports System
Imports System.Data

Imports BusinessLogic
Imports Common
Imports Common_NSNFramework

Partial Class DashBoard_ATPOnSiteScheduled
    Inherits System.Web.UI.Page
    Dim objBo As New BODashBoard
    Dim objdb As New DBUtil
    Dim objutils_nsn As New DBUtils_NSN
    Dim dt As DataTable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        If Page.IsPostBack = False Then
            BtnViewAll.Visible = False
            If CommonSite.RollId = ConfigurationManager.AppSettings("ATPROLEAPP") Then
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
        BindTaskReviewerATPOnSite(True)
    End Sub
#Region "custom methods"
    Private Sub BindTaskReviewerATPOnSite(ByVal viewAll As Boolean)
        Dim dtPendingATPOnSite As New DataTable
        Dim strSP As String = "exec uspDashBoardAgendaSiteDocCount_ReviewerATPOnSite"
        If (viewAll = True) Then
            strSP += "_All"
        End If
        dtPendingATPOnSite = objdb.ExeQueryDT(strSP & " " & CommonSite.UserId() & "", "ds")
        gvDocCountATPOnSite.DataSource = dtPendingATPOnSite
        gvDocCountATPOnSite.DataBind()

        If (gvDocCountATPOnSite.Rows.Count > 20) Then
            BtnViewAll.Visible = True
        End If
    End Sub
#End Region

End Class
