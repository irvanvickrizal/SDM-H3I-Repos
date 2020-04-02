Imports System
Imports System.Data
Imports Common
Imports Common_NSNFramework
Imports BusinessLogic

Partial Class DashBoard_frmSiteDocCount_QC
    Inherits System.Web.UI.Page
    Dim dt As New DataTable
    Dim objBo As New BODashBoard
    Dim objdb As New DBUtil

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            BindTaskPending(False)
        End If
    End Sub

    Protected Sub BtnViewAll_click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnViewAll.Click
        BindTaskPending(True)
    End Sub

    Protected Sub BtnGoToDashboard_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnGoToDashboard.Click
        Select Case Session("User_Type")
            Case "N"
                Response.Redirect("../frmDashboard_Temp.aspx?from=qcpending")
            Case "C"
                Response.Redirect("frmSiteAllTaskPending.aspx")
            Case "S"
                Response.Redirect("../frmDashboard_Temp.aspx?from=qcpending")
        End Select
    End Sub

#Region "custom methods"
    Private Sub BindTaskPending(ByVal isViewAll As Boolean)
        If isViewAll = True Then
            dt = objdb.ExeQueryDT("exec uspDashBoardAgendaSiteDocCount_QCApproverReviewer_All " & CommonSite.UserId, "dt")
        Else
            dt = objdb.ExeQueryDT("exec uspDashBoardAgendaSiteDocCount_QCApproverReviewer " & CommonSite.UserId, "dt")
        End If

        If dt.Rows.Count > 0 Then
            GrdDocCount.DataSource = dt
            GrdDocCount.DataBind()
        Else
            Select Case Session("User_Type")
                Case "N"
                    Response.Redirect("../frmDashboard_Temp.aspx?from=qcpending")
                Case "C"
                    Response.Redirect("frmSiteAllTaskPending.aspx")
                Case "S"
                    Response.Redirect("../frmDashboard_Temp.aspx?from=qcpending")
            End Select
        End If
    End Sub
#End Region

End Class
