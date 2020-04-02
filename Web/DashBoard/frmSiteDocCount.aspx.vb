Imports BusinessLogic
Imports Common
Imports Entities
Imports System.Data
Imports NSNCustomizeConfiguration
Imports Common_NSNFramework
Partial Class DashBoard_frmSiteDocCount
    Inherits System.Web.UI.Page
    Dim dt As New DataTable
    Dim objBo As New BODashBoard
    Dim objdb As New DBUtil
    Dim objutils_nsn As New DBUtils_NSN
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        If Page.IsPostBack = False Then
            BindData()
        End If
    End Sub
    
    Sub BindData()
        dt = objdb.ExeQueryDT("exec HCPT_uspTrans_GetTaskPending " & CommonSite.UserId() & ",-1", "ds")
        If dt.Rows.Count > 0 Then
            GrdDocCount.DataSource = dt
            GrdDocCount.DataBind()
        Else
            Select Case Session("User_Type")
                Case "N"
                    Response.Redirect("../frmDashboard_Temp.aspx")
                Case "C"
                    Response.Redirect("frmSiteAllTaskPending.aspx")
                Case "H"
                    Response.Redirect("frmSiteAllTaskPending.aspx")
                Case "S"
                    Response.Redirect("../frmDashboard_Temp.aspx")
            End Select
            'Response.Redirect("../Welcome.aspx")
        End If
    End Sub

    Protected Sub btnViewAll_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnViewAll.ServerClick
        Dim isApproverReviewer As Boolean = False
        If (CommonSite.RollId = 28 And CommonSite.UserId = 166) Then 'Agus Handayana Only
            isApproverReviewer = True
        End If
        If isApproverReviewer = True Then
            ' BindTaskApprover_Reviewer(True) 'Agus Handayana Only'
        Else
            'If (CommonSite.RollId = 28 Or CommonSite.RollId = 73 Or CommonSite.RollId = 105) Then
            '    BindTaskApproverReviewer(True)
            'ElseIf (CommonSite.RollId = 11 Or CommonSite.RollId = 41 Or CommonSite.RollId = 23 Or CommonSite.RollId = 29 Or CommonSite.RollId = 106 Or CommonSite.RollId = 112 Or CommonSite.RollId = 113) Then ' Approver User Type
            '    BindTaskApprover(True)
            'ElseIf (CommonSite.RollId = 43 Or CommonSite.RollId = 48 Or CommonSite.RollId = 49 Or CommonSite.RollId = 50 Or _
            '                CommonSite.RollId = 60 Or CommonSite.RollId = 63 Or CommonSite.RollId = 74 Or CommonSite.RollId = 75) Then
            '    BindTaskReviewer(True)
            'ElseIf (CommonSite.RollId = ConfigurationManager.AppSettings("ATPROLEAPP")) Then
            '    BindTaskReviewerATP(False)
            'Else
            '    dt = objdb.ExeQueryDT("exec uspDashBoardAgendaSiteDocCount2 " & CommonSite.UserId() & "", "ds")
            'End If
            GrdDocCount.DataSource = dt
            GrdDocCount.DataBind()
        End If
    End Sub

    Protected Sub BtnClose_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnClose.ServerClick
        Dim dhcontroller As New DashboardController
        Response.Redirect("../Welcome.aspx")   'Added by Fauzan, 13 Nov 2018
        'Commented by Fauzan, 13 Nov 2018.
        'Select Case Session("User_Type")
        '    Case "N"
        '        Response.Redirect("../frmDashboard_Temp.aspx")
        '    Case "C"
        '        Response.Redirect("frmSiteAllTaskPending.aspx")
        '    Case "H"
        '        Response.Redirect("frmSiteAllTaskPending.aspx")
        '    Case "S"
        '        Response.Redirect("../frmDashboard_Temp.aspx")
        'End Select
    End Sub

    Protected Sub GrdDocCount_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GrdDocCount.PageIndexChanging
        GrdDocCount.PageIndex = e.NewPageIndex
        BindData()
    End Sub
   
End Class
