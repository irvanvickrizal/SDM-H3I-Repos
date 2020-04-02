Imports System.Data
Imports CRFramework
Imports System.IO


Partial Class DashBoard_frmDocCRApproved
    Inherits System.Web.UI.Page
    Dim controller As New CRController
    Dim co_controller As New COController
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        If Not Page.IsPostBack Then
            SetDefaultPendingView()
            CheckAllTaskPending()
        End If
    End Sub

    Protected Sub BtnViewCRPendingClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnViewCRPending.Click
        GetCRTaskPending()
    End Sub

    Protected Sub BtnViewCOPendingClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnViewCOPending.Click
        GetCOTaskPending()
    End Sub

    Protected Sub grdapproverdocumentRowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles grdapproverdocuments.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lblno As Label = CType(e.Row.FindControl("lblno"), Label)
            If Not String.IsNullOrEmpty(lblno.Text) Then
                Dim seqno As Integer = Integer.Parse(lblno.Text)
                If seqno > 1 Then
                    e.Row.Cells(6).Visible = False
                    e.Row.Cells(7).Visible = True
                Else
                    e.Row.Cells(6).Visible = True
                    e.Row.Cells(7).Visible = False
                End If
            End If
        End If
    End Sub

    Protected Sub grdreviewerdocumentRowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles grdreviewerdocument.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lblno As Label = CType(e.Row.FindControl("lblno"), Label)
            If Not String.IsNullOrEmpty(lblno.Text) Then
                Dim seqno As Integer = Integer.Parse(lblno.Text)
                If seqno > 1 Then
                    e.Row.Cells(6).Visible = False
                    e.Row.Cells(7).Visible = True
                Else
                    e.Row.Cells(6).Visible = True
                    e.Row.Cells(7).Visible = False
                End If
            End If
        End If
    End Sub

#Region "CustomMethods"

    Private Sub SetDefaultPendingView()
        GetCRTaskPendingApprover(Convert.ToInt32(CommonSite.UserId), Convert.ToString(Request.QueryString("wpid")))
        GetCRTaskPendingReviewer(Convert.ToInt32(CommonSite.UserId), Convert.ToString(Request.QueryString("wpid")))
        GetCOTaskPendingApprover(Convert.ToInt32(CommonSite.UserId), Convert.ToString(Request.QueryString("wpid")))
        GetCOTaskPendingReviewer(Convert.ToInt32(CommonSite.UserId), Convert.ToString(Request.QueryString("wpid")))
        If grdapproverdocuments.Rows.Count > 0 Or grdreviewerdocument.Rows.Count > 0 Then
            GetCRTaskPending()
        ElseIf GvCoApproval.Rows.Count > 0 Or GvCOReview.Rows.Count > 0 Then
            GetCOTaskPending()
        Else ' Default Task Pending
            GetCRTaskPending()
        End If
        
    End Sub

    Private Sub GetCRTaskPending()
        BtnViewCRPending.BackColor = Drawing.Color.White
        BtnViewCOPending.BackColor = Drawing.Color.Gray
        MvCorePanel.SetActiveView(VwCRDocPending)
        GetCRTaskPendingApprover(CommonSite.UserId, GetWPID())
        GetCRTaskPendingReviewer(CommonSite.UserId, GetWPID())
        GetCRTaskPendingApproved(CommonSite.UserId, GetWPID())
    End Sub

    Private Sub GetCOTaskPending()
        BtnViewCRPending.BackColor = Drawing.Color.Gray
        BtnViewCOPending.BackColor = Drawing.Color.White
        MvCorePanel.SetActiveView(VwCODocPending)
        GetCOTaskPendingApprover(CommonSite.UserId, GetWPID())
        GetCOTaskPendingReviewer(CommonSite.UserId, GetWPID())
        GetCOTaskApproved(CommonSite.UserId, GetWPID())
    End Sub

    Private Sub CheckAllTaskPending()
        If grdapproverdocuments.Rows.Count = 0 And grdreviewerdocument.Rows.Count = 0 And GvCoApproval.Rows.Count = 0 And GvCOReview.Rows.Count = 0 Then
            Response.Redirect("frmSiteDocCount_CR.aspx?from=crpending")
        End If
    End Sub
    Private Sub GetCRTaskPendingApprover(ByVal userid As Int32, ByVal packageid As String)
        grdapproverdocuments.DataSource = controller.GetCRDocumentByTaskGroup(packageid, CommonSite.UserId, "Approver")
        grdapproverdocuments.DataBind()
    End Sub

    Private Sub GetCRTaskPendingReviewer(ByVal userid As Int32, ByVal packageid As String)
        grdreviewerdocument.DataSource = controller.GetCRDocumentByTaskGroup(packageid, CommonSite.UserId, "Reviewer")
        grdreviewerdocument.DataBind()
    End Sub

    Private Sub GetCRTaskPendingApproved(ByVal userid As Int32, ByVal packageid As String)
        grddocumentapproved.DataSource = controller.GetCRDocumentApprovedByTaskGroup(packageid, CommonSite.UserId)
        grddocumentapproved.DataBind()
    End Sub

    Private Sub GetCOTaskPendingApprover(ByVal userid As Int32, ByVal packageid As String)
        GvCoApproval.DataSource = co_controller.GetCODocumentByTaskGroup(packageid, CommonSite.UserId, "Approver")
        GvCoApproval.DataBind()
    End Sub

    Private Sub GetCOTaskPendingReviewer(ByVal userid As Int32, ByVal packageid As String)
        GvCOReview.DataSource = co_controller.GetCODocumentByTaskGroup(packageid, CommonSite.UserId, "Reviewer")
        GvCOReview.DataBind()
    End Sub

    Private Sub GetCOTaskApproved(ByVal userid As Int32, ByVal packageid As String)
        GvCODocApproved.DataSource = co_controller.GetCODocumentApprovedByTaskGroup(packageid, CommonSite.UserId)
        GvCODocApproved.DataBind()
    End Sub

    Private Function GetWPID() As String
        If Not String.IsNullOrEmpty(Request.QueryString("wpid")) Then
            Return Request.QueryString("wpid")
        Else
            Return "0"
        End If
    End Function
#End Region

End Class
