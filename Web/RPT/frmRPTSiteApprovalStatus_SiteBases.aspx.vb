Imports System.Collections.Generic
Imports NSNCustomizeConfiguration
Imports System.IO
Imports System.Data
Imports BusinessLogic
Imports Common
Partial Class RPT_frmRPTSiteApprovalStatus_SiteBases
    Inherits System.Web.UI.Page
    Dim dt As New DataTable
    Dim objBo As New BODashBoard
    Dim objdb As New DBUtil
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        If Page.IsPostBack = False Then
            LoadUserApproverReviewer(Integer.Parse(Session("Area_Id")))
            BtnExportExcel.Enabled = False
        End If
    End Sub
    Protected Sub BtnLoadDataClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnLoadData.Click
        If (DdlApproverReviewer.SelectedIndex <> 0) Then
            LoadTaskPending(Integer.Parse(DdlApproverReviewer.SelectedValue))
        Else
            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('Please choose user that you want to view as task pending!');", True)
        End If
    End Sub

    Protected Sub BtnExporttoExcelClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnExportExcel.Click
        Dim tw As New StringWriter()
        Dim strFilename As String = "SiteApprovalStatus_" + DdlApproverReviewer.SelectedItem.Text.Replace(" ", "") + ".xls"
        Dim hw As New System.Web.UI.HtmlTextWriter(tw)
        Dim frm As HtmlForm = New HtmlForm()
        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("content-disposition", "attachment;filename=" + strFilename)
        Response.Charset = ""
        EnableViewState = False
        Controls.Add(frm)
        frm.Controls.Add(GrdDocCount)
        frm.RenderControl(hw)
        Response.Write(tw.ToString())
        Response.End()
    End Sub
#Region "Custom Methods"
    Private Sub LoadUserApproverReviewer(ByVal ara_id As Integer)
        If (Session("Role_Id") Is Nothing = False) Then
            Dim roleId As Integer = Integer.Parse(Session("Role_Id"))
            If (roleId = 1) Then
                DdlApproverReviewer.DataSource = NSNCustomizeConfiguration.GetRolesByArea_AdminOnly()
            Else
                If (ara_id <> 0) Then
                    DdlApproverReviewer.DataSource = NSNCustomizeConfiguration.GetRolesByArea(ara_id)
                Else
                    Response.Write("<script>alert('You don't have any privilages, Please contact Administrator')</script>")
                    Response.Redirect("../frmDashboard_Temp.aspx")
                End If
            End If
            DdlApproverReviewer.DataTextField = "Username"
            DdlApproverReviewer.DataValueField = "UserId"
            DdlApproverReviewer.DataBind()
        End If
        DdlApproverReviewer.Items.Insert(0, "-- Select user --")
        
    End Sub
    Private Sub LoadTaskPending(ByVal userId As Integer)
        Dim strSP As String = "exec uspDashBoardAgendaSiteDocCount_ApproverReviewer_All"
        dt = objdb.ExeQueryDT(strSP & " " & userId & "", "ds")
        GrdDocCount.DataSource = dt
        GrdDocCount.DataBind()
        If dt.Rows.Count = 0 Then
            BtnExportExcel.Enabled = False
        Else
            BtnExportExcel.Enabled = True
        End If
    End Sub
#End Region
End Class
