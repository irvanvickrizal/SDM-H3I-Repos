Imports System.Data
Imports BusinessLogic
Imports Common

Partial Class MDB_TI_Milestone
    Inherits System.Web.UI.Page

    Dim objdl As New BODDLs
    Dim objUtil As New DBUtil
    Dim dt As DataTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        If Page.IsPostBack = False Then
            Process.InnerHtml = "Process : TI"
            objdl.fillDDL(ddlPO, "PODetails", False, Constants._DDL_Default_Select)
            bindData()
            BindChart()
            BindStackedChart()
        End If
    End Sub

    Sub bindData()
        dt = objUtil.ExeQueryDT("exec usp_MgmtEPM '" & ddlPO.SelectedValue & "'", "ManagementDashboardEPM")
        GVEPM.DataSource = dt
        'GVEPM.PageSize = Session("Page_size")
        GVEPM.DataBind()
    End Sub

    Protected Sub ddlPO_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPO.SelectedIndexChanged
        bindData()
        BindChart()
        BindStackedChart()
    End Sub

    Sub BindChart()
        Dim dtChart As New DataTable()
        dtChart = objUtil.ExeQueryDT("exec usp_MgmtEPM '" & ddlPO.SelectedValue & "'", "ChartTI")
        chart.ColumnChart.ColumnSpacing = 1

        dtChart.Columns(0).ColumnName = ddlPO.SelectedValue & "          "
        dtChart.Columns(1).ColumnName = "On AIR     "
        dtChart.Columns(2).ColumnName = "KPI Met    "
        dtChart.Columns(3).ColumnName = "Folder to NSN    "
        dtChart.Columns(4).ColumnName = "BAUT Submitted    "
        dtChart.Columns(5).ColumnName = "BAUT Approved     "
        dtChart.Columns(6).ColumnName = "BAST Submitted    "
        dtChart.Columns(7).ColumnName = "BAST Approved     "

        chart.DataSource = dtChart
        chart.DataBind()

    End Sub

    Protected Sub GVEPM_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GVEPM.RowDataBound
        If e.Row.RowType = DataControlRowType.Header Then
            e.Row.Cells(0).Text = ddlPO.SelectedValue
        End If
    End Sub

    Sub BindStackedChart()
        Dim dtChart As New DataTable()
        dtChart = objUtil.ExeQueryDT("exec uspManagementDashboardProcessChart 'TI'", "StackedChartTI")
        stackedchart.ColumnChart.ColumnSpacing = 1
        stackedchart.DataSource = dtChart
        stackedchart.DataBind()
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        'Response.Redirect("managementdashboard.aspx")
        'WebAsyncRefreshPanel2.Controls.Add(New LiteralControl("<script type='text/javascript'>window.location='managementdashboard.aspx';</script>"))
        Infragistics.WebUI.Shared.CallBackManager.AddScriptBlock(Me, Nothing, "window.location='managementdashboard.aspx';")
    End Sub
End Class
