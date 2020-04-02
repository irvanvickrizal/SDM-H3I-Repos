Imports System.Data
Imports BusinessLogic
Imports Common
Imports Entities
Partial Class DashBoard_ManagementDashobardProcess
    Inherits System.Web.UI.Page

    Dim objBO As New BODashBoard
    Dim dtdg As New DataTable
    Dim objUtil As New DBUtil
    Dim dtRegion As New DataTable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            BindDataEPM()
            BindChart()
        End If

    End Sub
    Sub BindDataEPM()
        If Request.QueryString("Process") <> Nothing Then

            Process.InnerHtml = "Process : " + Request.QueryString("Process")

            dtRegion = objUtil.ExeQueryDT("exec uspManagementDashboardProcess '" + Request.QueryString("Process") + "'", "ManagementDashboardProcess")
            createTable()
            Dim Contract As Integer = 0, Cancel As Integer = 0, Active As Integer = 0, DRM As Integer = 0, ReadyForBast As Integer = 0, DRMRemaining As Integer = 0, BastRemaining As Integer = 0, BastPrecentage As Decimal = 0
            For Each drow As DataRow In dtRegion.Rows
                Dim myrow As DataRow
                myrow = dtdg.NewRow
                myrow("PoNo") = drow.Item("PoNo")
                myrow("potype") = drow.Item("potype")
                myrow("PoDate") = drow.Item("PoDate")
                myrow("poname") = drow.Item("poname")
                myrow("ContractDT") = drow.Item("ContractDT")
                Contract += drow.Item("ContractDT")
                myrow("Cancel") = drow.Item("Cancel")
                Cancel += drow.Item("Cancel")
                myrow("Active") = drow.Item("Active")
                Active += drow.Item("Active")
                myrow("DRM") = drow.Item("DRM")
                DRM += drow.Item("DRM")
                myrow("ReadyForBast") = Convert.ToInt32(drow.Item("ReadyForBast"))
                ReadyForBast += drow.Item("ReadyForBast")
                myrow("DRMRemaining") = drow.Item("DRMRemaining").ToString
                DRMRemaining += drow.Item("DRMRemaining")
                myrow("BASTRemaining") = drow.Item("BASTRemaining").ToString
                BastRemaining += drow.Item("BASTRemaining")
                myrow("BASTPrecentage") = Format(drow.Item("BASTPrecentage"), "#0").ToString + " %"
                BastPrecentage += drow.Item("BASTPrecentage")
                myrow("Remark") = drow.Item("Remark")
                myrow("PoNew") = drow.Item("PoNew")
                dtdg.Rows.Add(myrow)
            Next
            If (dtRegion.Rows.Count >= 1) Then
                Dim myrow1 As DataRow
                myrow1 = dtdg.NewRow
                myrow1("PoNo") = " "
                myrow1("potype") = "Total"
                myrow1("PoDate") = ""
                myrow1("poname") = ""
                myrow1("ContractDT") = Contract
                myrow1("Cancel") = Cancel
                myrow1("Active") = Active
                myrow1("DRM") = DRM
                myrow1("ReadyForBast") = ReadyForBast

                myrow1("DRMRemaining") = DRMRemaining.ToString
                myrow1("BASTRemaining") = BastRemaining.ToString

                'myrow1("BASTPrecentage") = Math.Round((BastPrecentage / dtRegion.Rows.Count)).ToString + " %"
                myrow1("BASTPrecentage") = Format((ReadyForBast / Active) * 100, "#0").ToString + " %"
                myrow1("Remark") = ""
                dtdg.Rows.Add(myrow1)
            End If
            Select Case Request.QueryString("Process").ToString.ToLower
                Case "ti"
                    'GVEPM.Columns(6).HeaderText = "ON Air"
                    'renuka 12/05/2009
                    GVEPM.Columns(6).HeaderText = "ON Air Done"
                    'GVEPM.Columns(6).HeaderText = "ON Air Remaining"
                    'renuka 12/05/2009
                    GVEPM.Columns(8).HeaderText = "ON Air Remaining"

                Case "sis"
                    GVEPM.Columns(6).HeaderText = "DRM"
                    GVEPM.Columns(8).HeaderText = "DRM Remaining"
                Case "sitac"
                    GVEPM.Columns(6).HeaderText = "RFC"
                    GVEPM.Columns(8).HeaderText = "RFC Remaining"
                Case "cme"
                    GVEPM.Columns(6).HeaderText = "RFI"
                    GVEPM.Columns(8).HeaderText = "RFI Remaining"
            End Select


            GVEPM.PageSize = CommonSite.PageSize()
            GVEPM.DataSource = dtdg
            Try
                GVEPM.DataBind()
            Catch ex As Exception
                GVEPM.PageIndex = 0
            End Try
        End If
    End Sub
    Sub createTable()
        dtdg.Columns.Add("PoNo", Type.GetType("System.String"))
        dtdg.Columns.Add("potype", Type.GetType("System.String"))
        dtdg.Columns.Add("PoDate", Type.GetType("System.String"))
        dtdg.Columns.Add("Vendor", Type.GetType("System.String"))
        dtdg.Columns.Add("poname", Type.GetType("System.String"))
        dtdg.Columns.Add("ContractDT", Type.GetType("System.Int32"))
        dtdg.Columns.Add("Cancel", Type.GetType("System.Int32"))
        dtdg.Columns.Add("Active", Type.GetType("System.Int32"))
        dtdg.Columns.Add("DRM", Type.GetType("System.Int32"))
        dtdg.Columns.Add("ReadyForBast", Type.GetType("System.Int32"))
        dtdg.Columns.Add("DRMRemaining", Type.GetType("System.String"))
        dtdg.Columns.Add("BASTRemaining", Type.GetType("System.String"))
        dtdg.Columns.Add("BASTPrecentage", Type.GetType("System.String"))
        dtdg.Columns.Add("Remark", Type.GetType("System.String"))
        dtdg.Columns.Add("PoNew", Type.GetType("System.String"))
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
            e.Row.Cells(8).Text = "- ( " & ilblDRMRemaining.Text & ")"
            e.Row.Cells(9).Text = "- ( " & ilblBASTRemaining.Text & " )"
        Else
            Dim iLabelPono As Label = CType(e.Row.FindControl("lblCustomerPono"), Label)
            Dim ihdPono As HiddenField = CType(e.Row.FindControl("hdPono"), HiddenField)
            Dim ilblContract As Label = CType(e.Row.FindControl("lblContract"), Label)
            Dim ilblCancel As Label = CType(e.Row.FindControl("lblCancel"), Label)
            Dim ilblActive As Label = CType(e.Row.FindControl("lblActive"), Label)
            Dim ilblDRM As Label = CType(e.Row.FindControl("lblDRM"), Label)
            Dim ilblBastDone As Label = CType(e.Row.FindControl("lblBastDone"), Label)
            Dim ilblDRMRemaining As Label = CType(e.Row.FindControl("lblDRMRemaining"), Label)
            Dim ilblBASTRemaining As Label = CType(e.Row.FindControl("lblBASTRemaining"), Label)

            e.Row.Cells(2).Text = "<a href='#' onclick=""OverAllStatus('" & Request.QueryString("Process") & "','" & ihdPono.Value & "',1)"">" & iLabelPono.Text & "</a>"
            If (ilblContract.Text <> "0") Then
                e.Row.Cells(3).Text = "<a href='#' onclick=""OverAllStatus('" & Request.QueryString("Process") & "','" & ihdPono.Value & "',2)"">" & ilblContract.Text & "</a>"
            End If
            If (ilblCancel.Text <> "0") Then
                e.Row.Cells(4).Text = "<a href='#' onclick=""OverAllStatus('" & Request.QueryString("Process") & "','" & ihdPono.Value & "',3)"">" & ilblCancel.Text & "</a>"
            End If
            If (ilblActive.Text <> "0") Then
                e.Row.Cells(5).Text = "<a href='#' onclick=""OverAllStatus('" & Request.QueryString("Process") & "','" & ihdPono.Value & "',4)"">" & ilblActive.Text & "</a>"
            End If
            If (ilblDRM.Text <> "0") Then
                e.Row.Cells(6).Text = "<a href='#' onclick=""OverAllStatus('" & Request.QueryString("Process") & "','" & ihdPono.Value & "',5)"">" & ilblDRM.Text & "</a>"
            End If
            If (ilblBastDone.Text <> "0") Then
                e.Row.Cells(7).Text = "<a href='#' onclick=""OverAllStatus('" & Request.QueryString("Process") & "','" & ihdPono.Value & "',6)"">" & ilblBastDone.Text & "</a>"
            End If
            If (ilblDRMRemaining.Text <> "0") Then
                e.Row.Cells(8).Text = "- ( <a href='#' onclick=""OverAllStatus('" & Request.QueryString("Process") & "','" & ihdPono.Value & "',7)"" style=""color:Red;"">" & ilblDRMRemaining.Text & "</a> )"
            End If
            If (ilblBASTRemaining.Text <> "0") Then
                e.Row.Cells(9).Text = "- ( <a href='#' onclick=""OverAllStatus('" & Request.QueryString("Process") & "','" & ihdPono.Value & "',8)"" style=""color:Red;"">" & ilblBASTRemaining.Text & "</a> )"
            End If

        End If



    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Response.Redirect("managementdashboard.aspx")
    End Sub

    Sub BindChart()
        Dim dtChart As New DataTable()
        dtChart = objUtil.ExeQueryDT("exec uspManagementDashboardProcessChart '" & Request.QueryString("Process") & "'", "ChartTI")
        chart.ColumnChart.ColumnSpacing = 1
        chart.DataSource = dtChart
        chart.DataBind()
    End Sub
End Class