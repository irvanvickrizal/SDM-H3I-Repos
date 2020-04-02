Imports BusinessLogic
Imports Common
Imports Entities
Imports System.Data

Partial Class DashBoard_frmDocApprovedAtpOnSite
    Inherits System.Web.UI.Page
    Dim objDL As New BODDLs
    Dim dt As New DataTable
    Dim objBo As New BODashBoard
    Dim objutil As New DBUtil

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        If Not Page.IsPostBack Then
            dt = objutil.ExeQueryDT("exec uspSiteDocListReviewerATPOnSiteTask " & CommonSite.UserId() & ",'" & ConfigurationManager.AppSettings("APPTASKID") & "','" & Request.QueryString("Id") & "','" & Request.QueryString("wpid") & "'", "ser")
            gvDocumentATPReviewed.DataSource = dt
            gvDocumentATPReviewed.DataBind()
        End If
    End Sub

    Protected Sub gvDocumentATPReviewed_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvDocumentATPReviewed.RowDataBound
        'Select Case e.Row.RowType
        '    Case DataControlRowType.DataRow
        '        Dim lbl As Label = CType(e.Row.FindControl("lblNo"), Label)
        '        lbl.Text = (gvDocumentATPReviewed.PageIndex * gvDocumentATPReviewed.PageSize) + (e.Row.RowIndex + 1) & ".&nbsp;"
        'End Select
        If e.Row.RowType = DataControlRowType.DataRow Then
            'Dim lblSwid As Label = e.Row.FindControl("LblSW_Id")
            'If (String.IsNullOrEmpty(lblSwid.Text) = False) Then
            '    Dim url As String = "../PO/frmViewDocument.aspx?id=" & lblSwid.Text
            '    If url.Length > 14 Then
            '        e.Row.Cells(4).Text = "<a href='#' onclick=""window.open('" & url & "','mywindow','status=yes,menubar=no,scrollbars=yes,resizable=yes,width=700px,height=600px')"">" & e.Row.Cells(3).Text & "</a>"
            '    Else
            '        e.Row.Cells(4).Text = e.Row.Cells(3).Text
            '    End If
            'End If
            End If
    End Sub

    Protected Sub BtnBackToATPPendingClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnBackToATPPending.Click
        Response.Redirect("frmSiteDocCount_ATP.aspx?id=" & CommonSite.UserId())
    End Sub
End Class
