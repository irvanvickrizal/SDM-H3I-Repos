
Partial Class COD_frmDashboardMaster
    Inherits System.Web.UI.Page

    Private controller As New DashboardController
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        If Not Page.IsPostBack Then
            BindData()
        End If
    End Sub

    Protected Sub GvMasterDashboard_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs) Handles GvMasterDashboard.RowCommand
        If e.CommandName.Equals("EditDashboard") Then

        ElseIf e.CommandName.Equals("DeleteDashboard") Then
            DeleteDashboard(Integer.Parse(e.CommandArgument.ToString()))
        End If
    End Sub

    Protected Sub BtnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSave.Click
        Dim info As New DashboardInfo
        info.MDashboardId = 0
        info.FormName = TxtURLForm.Text
        info.DashboardName = TxtDashboardName.Text
        info.DashboardDesc = TxtDashboardDesc.Text
        info.IsDefault = IIf(ChkSetDefaultDashboard.Checked = True, True, False)
        info.LMBY = CommonSite.UserName
        CreateNewDashboard(info)
    End Sub

    Protected Sub BtnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnClear.Click
        ClearForm()
    End Sub

#Region "Custom Methods"
    Private Sub BindData()
        MvDashboardConfiguration.SetActiveView(VwDashboardMaster)
        GvMasterDashboard.DataSource = controller.GetDashboardMaster()
        GvMasterDashboard.DataBind()
    End Sub

    Private Sub CreateNewDashboard(ByVal info As DashboardInfo)
        controller.DashboardMaster_IU(info)
        ClearForm()
        BindData()
    End Sub

    Private Sub DeleteDashboard(ByVal dashboardid As Integer)
        controller.DashboardMaster_Delete(dashboardid)
        BindData()
    End Sub

    Private Sub EditDashboard(ByVal dashboardid As Integer)
        MvDashboardConfiguration.SetActiveView(VwEditDashboard)

    End Sub

    Private Sub ClearForm()
        TxtDashboardName.Text = ""
        TxtDashboardDesc.Text = ""
        TxtURLForm.Text = ""
        ChkSetDefaultDashboard.Checked = False
    End Sub

    Private Sub GvMasterDashboard_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles GvMasterDashboard.PageIndexChanging
        GvMasterDashboard.PageIndex = e.NewPageIndex
        BindData()
    End Sub

#End Region

End Class
