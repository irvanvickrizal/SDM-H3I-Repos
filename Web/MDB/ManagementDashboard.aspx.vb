Imports System.Data

Imports BusinessLogic
Imports Common
Imports Entities
Partial Class DashBoard_ManagementDashboard
    Inherits System.Web.UI.Page
    Dim objBO As New BODashBoard
    Dim dtdg As New DataTable
    Dim objUtil As New DBUtil

    Sub BindDataEPM()

        Dim dtRegion As New DataTable()
        dtRegion = objUtil.ExeQueryDT("exec uspManagementDashboard", "ManagementDashboard")
        createTable()
        For Each drow As DataRow In dtRegion.Rows
            Dim myrow As DataRow
            myrow = dtdg.NewRow
            myrow("potype") = drow.Item("potype")
            myrow("ContractDT") = drow.Item("ContractDT")
            myrow("Cancel") = drow.Item("Cancel")
            myrow("Active") = drow.Item("Active")
            myrow("ReadyForBast") = Convert.ToInt32(drow.Item("ReadyForBast"))
            myrow("BASTRemaining") = drow.Item("BASTRemaining").ToString
            myrow("BASTPrecentage") = Format(drow.Item("BASTPrecentage"), "#0").ToString + " %"
            myrow("Remark") = drow.Item("Remark")
            dtdg.Rows.Add(myrow)
        Next
        GVEPM.PageSize = CommonSite.PageSize()
        GVEPM.DataSource = dtRegion
        Try
            GVEPM.DataBind()
        Catch ex As Exception
            GVEPM.PageIndex = 0
        End Try
    End Sub
    Sub createTable()
        dtdg.Columns.Add("potype", Type.GetType("System.String"))
        dtdg.Columns.Add("ContractDT", Type.GetType("System.Int32"))
        dtdg.Columns.Add("Cancel", Type.GetType("System.Int32"))
        dtdg.Columns.Add("Active", Type.GetType("System.Int32"))
        dtdg.Columns.Add("ReadyForBast", Type.GetType("System.Int32"))

        dtdg.Columns.Add("BASTRemaining", Type.GetType("System.String"))
        dtdg.Columns.Add("BASTPrecentage", Type.GetType("System.String"))
        dtdg.Columns.Add("Remark", Type.GetType("System.String"))
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            BindDataEPM()
            BindChartTI()
            BindChartCME()
            BindChartSITAC()
            BindChartSIS()
        End If

    End Sub
    Protected Sub GVEPM_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GVEPM.RowDataBound
        Dim drv As DataRowView = CType(e.Row.DataItem, DataRowView)
        If drv Is Nothing Then
            Return
        End If



        If drv("potype").ToString() = "Total" Then
            Dim ilblDRMRemaining As Label = CType(e.Row.FindControl("lblDRMRemaining"), Label)
            Dim ilblBASTRemaining As Label = CType(e.Row.FindControl("lblBASTRemaining"), Label)
            e.Row.Font.Bold = True
            e.Row.Cells(9).Text = "- ( " & ilblDRMRemaining.Text & ")"
            e.Row.Cells(10).Text = "- ( " & ilblBASTRemaining.Text & " )"
        Else
       
            Dim ilblPoType As Label = CType(e.Row.FindControl("lblPoType"), Label)

            Dim ilblBASTRemaining As Label = CType(e.Row.FindControl("lblBASTRemaining"), Label)

            If ilblPoType.Text = "TI" Then
                e.Row.Cells(0).Text = "<a href=""TI_drilldown.aspx"">" & ilblPoType.Text & "</a>"
            Else
                e.Row.Cells(0).Text = "<a href=""ManagementDashobardProcess.aspx?Process=" & ilblPoType.Text.Trim & """>" & ilblPoType.Text & "</a>"
            End If
            'e.Row.Cells(0).Text = "<a href=""ManagementDashobardProcess.aspx?Process=" & ilblPoType.Text.Trim & """>" & ilblPoType.Text & "</a>"


            If (ilblBASTRemaining.Text <> "0") Then
                e.Row.Cells(5).Text = "- (  " & ilblBASTRemaining.Text & " )"
            End If

        End If



    End Sub

    Sub BindChartTI()
        Dim dtChart As New DataTable()
        dtChart = objUtil.ExeQueryDT("exec uspManagementDashboardChart 'TI'", "ChartTI")
        chartTI.ColumnChart.ColumnSpacing = 1
        chartTI.DataSource = dtChart
        chartTI.DataBind()
    End Sub

    Sub BindChartCME()
        Dim dtChart As New DataTable()
        dtChart = objUtil.ExeQueryDT("exec uspManagementDashboardChart 'CME'", "ChartCME")
        chartCME.ColumnChart.ColumnSpacing = 1
        If dtChart.Rows.Count >= 1 Then
            chartCME.DataSource = dtChart
            chartCME.DataBind()
            chartCME.Visible = True
        Else
            chartCME.Visible = False
        End If
       
    End Sub

    Sub BindChartSITAC()
        Dim dtChart As New DataTable()
        dtChart = objUtil.ExeQueryDT("exec uspManagementDashboardChart 'SITAC'", "ChartSITAC")
        chartSITAC.ColumnChart.ColumnSpacing = 1
        If dtChart.Rows.Count >= 1 Then
            chartSITAC.DataSource = dtChart
            chartSITAC.DataBind()
            chartSITAC.Visible = True
        Else
            chartSITAC.Visible = False
        End If
    End Sub

    Sub BindChartSIS()
        Dim dtChart As New DataTable()
        dtChart = objUtil.ExeQueryDT("exec uspManagementDashboardChart 'SIS'", "ChartSIS")
        chartSIS.ColumnChart.ColumnSpacing = 1
        If dtChart.Rows.Count >= 1 Then
            chartSIS.DataSource = dtChart
            chartSIS.DataBind()
            chartSIS.Visible = True
        Else
            chartSIS.Visible = False
        End If

    End Sub

End Class
