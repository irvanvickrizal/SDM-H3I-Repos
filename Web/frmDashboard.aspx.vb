Imports BusinessLogic
Imports Common
Imports Entities
Imports System.Data
Partial Class frmDashboard
    Inherits System.Web.UI.Page
    Dim objBOD As New BODDLs
    Dim objET As New ETEBASTUsers
    Dim objBO As New BODashBoard
    Dim dt, dtdg As New DataTable
    Dim objUtil As New DBUtil
    Dim objdl As New BODDLs
    Dim strsql As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        If Page.IsPostBack = False Then
            'ddYear.Items.FindByText(Year(Now)).Selected = True
            If Session("User_Type") = "S" Then
            End If
            objdl.fillDDL(ddlPO, "PODetails", False, Constants._DDL_Default_Select)
            BindDataEPM_Infra()
            Check_Month()
            Open_Achievement()
            BindData_Infra()
            CreateAgenda()
            CreateSiteStatus()
            CreateBaut()
        End If
        BindTIChart()
        lblepmdate.Text = objUtil.ExeQueryScalarString("select convert(varchar(20),max(lmdt),106) from epmdata")
    End Sub
    Sub BindData_Infra()
        Dim dtRegion As New DataTable()
        dtRegion = objBO.usp_Dashboard(CommonSite.GetDashBoardLevel(), CommonSite.UserId())
        uwd_Site_List_doc.DataSource = dtRegion
        Try
            uwd_Site_List_doc.DataBind()
        Catch ex As Exception
        End Try
    End Sub
    Sub BindTIChart()
        Dim dtChart As New DataTable()
        dtChart = objUtil.ExeQueryDT("exec uspgraphgrouping '" & ddlPO.SelectedValue & "'", "ChartTI")
        chartTI.ColumnChart.ColumnSpacing = 1
        dtChart.Columns(0).ColumnName = ddlPO.SelectedValue & ""
        dtChart.Columns(1).ColumnName = "Task completed"
        dtChart.Columns(2).ColumnName = "Site Folder Recieved"
        dtChart.Columns(3).ColumnName = "KPI"
        dtChart.Columns(4).ColumnName = "BAUT Submitted"
        dtChart.Columns(5).ColumnName = "BAUT Done"
        dtChart.Columns(6).ColumnName = "BAST Submitted"
        dtChart.Columns(7).ColumnName = "BAST Done"
        chartTI.DataSource = dtChart
        chartTI.DataBind()
    End Sub
    Sub Change_Month()
        If ddMonth.SelectedIndex = 0 Then
            WebPanel2.Header.Text = "Monthly Achievement (Please select the month)"
        End If
        If ddMonth.SelectedIndex = 1 Then
            WebPanel2.Header.Text = "Monthly Achievement (January)"
        End If
        If ddMonth.SelectedIndex = 2 Then
            WebPanel2.Header.Text = "Monthly Achievement (February)"
        End If
        If ddMonth.SelectedIndex = 3 Then
            WebPanel2.Header.Text = "Monthly Achievement (March)"
        End If
        If ddMonth.SelectedIndex = 4 Then
            WebPanel2.Header.Text = "Monthly Achievement (April)"
        End If
        If ddMonth.SelectedIndex = 5 Then
            WebPanel2.Header.Text = "Monthly Achievement (May)"
        End If
        If ddMonth.SelectedIndex = 6 Then
            WebPanel2.Header.Text = "Monthly Achievement (June)"
        End If
        If ddMonth.SelectedIndex = 7 Then
            WebPanel2.Header.Text = "Monthly Achievement (July)"
        End If
        If ddMonth.SelectedIndex = 8 Then
            WebPanel2.Header.Text = "Monthly Achievement (August)"
        End If
        If ddMonth.SelectedIndex = 9 Then
            WebPanel2.Header.Text = "Monthly Achievement (September)"
        End If
        If ddMonth.SelectedIndex = 10 Then
            WebPanel2.Header.Text = "Monthly Achievement (October)"
        End If
        If ddMonth.SelectedIndex = 11 Then
            WebPanel2.Header.Text = "Monthly Achievement (November)"
        End If
        If ddMonth.SelectedIndex = 12 Then
            WebPanel2.Header.Text = "Monthly Achievement (December)"
        End If
    End Sub
    Sub Check_Month()
        Dim Date1 As Integer
        Date1 = Now.Month()
        If Date1 = 1 Then
            ddMonth.SelectedIndex = 1
            'WebPanel2.Header.Text = "Monthly Achievement (January)"
        End If
        If Date1 = 2 Then
            ddMonth.SelectedIndex = 2
            'WebPanel2.Header.Text = "Monthly Achievement (February)"
        End If
        If Date1 = 3 Then
            ddMonth.SelectedIndex = 3
            ' WebPanel2.Header.Text = "Monthly Achievement (March)"
        End If
        If Date1 = 4 Then
            ddMonth.SelectedIndex = 4
            'WebPanel2.Header.Text = "Monthly Achievement (April)"
        End If
        If Date1 = 5 Then
            ddMonth.SelectedIndex = 5
            ' WebPanel2.Header.Text = "Monthly Achievement (May)"
        End If
        If Date1 = 6 Then
            ddMonth.SelectedIndex = 6
            'WebPanel2.Header.Text = "Monthly Achievement (June)"
        End If
        If Date1 = 7 Then
            ddMonth.SelectedIndex = 7
            'WebPanel2.Header.Text = "Monthly Achievement (July)"
        End If
        If Date1 = 8 Then
            ddMonth.SelectedIndex = 8
            'WebPanel2.Header.Text = "Monthly Achievement (August)"
        End If
        If Date1 = 9 Then
            ddMonth.SelectedIndex = 9
            'WebPanel2.Header.Text = "Monthly Achievement (September)"
        End If
        If Date1 = 10 Then
            ddMonth.SelectedIndex = 10
            'WebPanel2.Header.Text = "Monthly Achievement (October)"
        End If
        If Date1 = 11 Then
            ddMonth.SelectedIndex = 11
            'WebPanel2.Header.Text = "Monthly Achievement (November)"
        End If
        If Date1 = 12 Then
            ddMonth.SelectedIndex = 12
            'WebPanel2.Header.Text = "Monthly Achievement (December)"
        End If
    End Sub
    Sub Open_Achievement()
        Dim dtChart As New DataTable()
        dtChart = objUtil.ExeQueryDT("exec uspgetgrouping4month " & ddMonth.SelectedValue & "," & ddYear.SelectedValue, "MilestoneTrackCount")
        UWD_Month.DataSource = dtChart
        UWD_Month.DataBind()
    End Sub
    Protected Sub ddMonth_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddMonth.SelectedIndexChanged
        Open_Achievement()
    End Sub
    Protected Sub UWD_Over_All_Status_Click(ByVal sender As Object, ByVal e As Infragistics.WebUI.UltraWebGrid.ClickEventArgs) Handles UWD_Over_All_Status.Click
        If IsActiveRow() Then
            BindDataEPM_Infra_Detail(UWD_Over_All_Status.DisplayLayout.ActiveRow.Cells.FromKey("noregion").Text)
            Me.WDM_EPM_Main.WindowState = Infragistics.Web.UI.LayoutControls.DialogWindowState.Normal
        End If
    End Sub
    Sub BindDataEPM_Infra_Detail(ByVal Region As String)
        Dim dtRegion As New DataTable()
        dtRegion = objUtil.ExeQueryDT("exec DashboardReportPONO " & Region & "," & CommonSite.GetDashBoardLevel() & "," & CommonSite.UserId(), "MilestoneTrackCount")
        createTable_EPM_Detail()
        Dim poContract As Integer = 0, Contract As Integer = 0, Cancel As Integer = 0, Active As Integer = 0, completed As Integer = 0, bautdone As Integer = 0, bastdone As Integer = 0, BastPrecentage As Decimal = 0
        For Each drow As DataRow In dtRegion.Rows
            Dim myrow As DataRow
            myrow = dtdg.NewRow
            myrow("pono") = drow.Item("pono")
            myrow("poname") = drow.Item("poname")
            myrow("ContractpoDT") = drow.Item("POCONTRACT")
            poContract += drow.Item("POCONTRACT")
            myrow("ContractDT") = drow.Item("ContractDT")
            Contract += drow.Item("ContractDT")
            myrow("Cancel") = drow.Item("Cancel")
            Cancel += drow.Item("Cancel")
            myrow("Active") = drow.Item("Active")
            Active += drow.Item("Active")
            myrow("completed") = drow.Item("Completed")
            completed += drow.Item("Completed")
            myrow("bautdone") = drow.Item("bautdone")
            bautdone += drow.Item("bautdone")
            myrow("bastdone") = drow.Item("bastdone")
            bastdone += drow.Item("bastdone")
            myrow("BASTPrecentage") = Format(drow.Item("BASTPrecentage"), "#0").ToString + " %"
            myrow("remark") = drow.Item("remark")
            dtdg.Rows.Add(myrow)
        Next
        If (dtRegion.Rows.Count >= 1) Then
            Dim myrow1 As DataRow
            myrow1 = dtdg.NewRow
            myrow1("pono") = "Total"
            myrow1("poname") = ""
            myrow1("ContractpoDT") = poContract
            myrow1("ContractDT") = Contract
            myrow1("Cancel") = Cancel
            myrow1("Active") = Active
            myrow1("completed") = completed
            myrow1("bautdone") = bautdone
            myrow1("bastdone") = bastdone
            myrow1("BASTPrecentage") = Format((bastdone / Active) * 100, "#0").ToString + " %"
            myrow1("Remark") = ""
            dtdg.Rows.Add(myrow1)
        End If
        UWG_EPM_Detail_PO.DataSource = dtdg
        Try
            UWG_EPM_Detail_PO.DataBind()
        Catch ex As Exception
            'GVEPM.PageIndex = 0
        End Try
    End Sub
    Sub createTable_EPM_Detail()
        dtdg.Columns.Add("poname", Type.GetType("System.String"))
        dtdg.Columns.Add("pono", Type.GetType("System.String"))
        dtdg.Columns.Add("ContractpoDT", Type.GetType("System.Int32"))
        dtdg.Columns.Add("ContractDT", Type.GetType("System.Int32"))
        dtdg.Columns.Add("Cancel", Type.GetType("System.Int32"))
        dtdg.Columns.Add("Active", Type.GetType("System.Int32"))
        dtdg.Columns.Add("completed", Type.GetType("System.Int32"))
        dtdg.Columns.Add("bautdone", Type.GetType("System.Int32"))
        dtdg.Columns.Add("bastdone", Type.GetType("System.String"))
        dtdg.Columns.Add("BASTPrecentage", Type.GetType("System.String"))
        dtdg.Columns.Add("Remark", Type.GetType("System.String"))
    End Sub
    Sub BindDataEPM_Infra()
        Dim dtRegion As New DataTable()
        dtRegion = objUtil.ExeQueryDT("exec dashboardreportnew " & CommonSite.GetDashBoardLevel() & "," & CommonSite.UserId(), "MilestoneTrackCount")
        createTable()
        Dim POCONTRACT As Integer = 0, Contract As Integer = 0, Cancel As Integer = 0, Active As Integer = 0, completed As Integer = 0, bautdone As Integer = 0, bastdone As Integer = 0, BastPrecentage As Decimal = 0
        For Each drow As DataRow In dtRegion.Rows
            Dim myrow As DataRow
            myrow = dtdg.NewRow
            myrow("region") = drow.Item("region")
            myrow("noregion") = drow.Item("noregion")
            myrow("ContractpoDT") = drow.Item("POCONTRACT")
            POCONTRACT += drow.Item("POCONTRACT")
            myrow("ContractDT") = drow.Item("ContractDT")
            Contract += drow.Item("ContractDT")
            myrow("Cancel") = drow.Item("Cancel")
            Cancel += drow.Item("Cancel")
            myrow("Active") = drow.Item("Active")
            Active += drow.Item("Active")
            myrow("completed") = drow.Item("Completed")
            completed += drow.Item("Completed")
            myrow("bautdone") = drow.Item("bautdone")
            bautdone += drow.Item("bautdone")
            myrow("bastdone") = drow.Item("bastdone")
            bastdone += drow.Item("bastdone")
            myrow("BASTPrecentage") = Format(drow.Item("BASTPrecentage"), "#0").ToString + " %"
            myrow("remark") = drow.Item("remark")
            dtdg.Rows.Add(myrow)
        Next
        If (dtRegion.Rows.Count >= 1) Then
            Dim myrow1 As DataRow
            myrow1 = dtdg.NewRow
            myrow1("region") = "Total"
            myrow1("noregion") = ""
            myrow1("ContractpoDT") = POCONTRACT
            myrow1("ContractDT") = Contract
            myrow1("Cancel") = Cancel
            myrow1("Active") = Active
            myrow1("completed") = completed
            myrow1("bautdone") = bautdone
            myrow1("bastdone") = bastdone
            myrow1("BASTPrecentage") = Format((bastdone / Active) * 100, "#0").ToString + " %"
            myrow1("Remark") = ""
            dtdg.Rows.Add(myrow1)
        End If
        UWD_Over_All_Status.DataSource = dtdg
        Try
            UWD_Over_All_Status.DataBind()
        Catch ex As Exception
            'GVEPM.PageIndex = 0
        End Try
    End Sub
    Sub createTable()
        dtdg.Columns.Add("region", Type.GetType("System.String"))
        dtdg.Columns.Add("noregion", Type.GetType("System.String"))
        dtdg.Columns.Add("ContractpoDT", Type.GetType("System.Int32"))
        dtdg.Columns.Add("ContractDT", Type.GetType("System.Int32"))
        dtdg.Columns.Add("Cancel", Type.GetType("System.Int32"))
        dtdg.Columns.Add("Active", Type.GetType("System.Int32"))
        dtdg.Columns.Add("completed", Type.GetType("System.Int32"))
        dtdg.Columns.Add("bautdone", Type.GetType("System.Int32"))
        dtdg.Columns.Add("bastdone", Type.GetType("System.String"))
        dtdg.Columns.Add("BASTPrecentage", Type.GetType("System.String"))
        dtdg.Columns.Add("Remark", Type.GetType("System.String"))
    End Sub
    Private Function IsActiveRow() As Boolean
        If Me.UWD_Over_All_Status.DisplayLayout.ActiveRow Is Nothing Then
            Return False
        End If
        If (UWD_Over_All_Status.DisplayLayout.ActiveRow.Cells(0).Text = "") Then
            Return False
        End If
        Return True
    End Function
    Protected Sub UWD_Month_InitializeLayout1(ByVal sender As Object, ByVal e As Infragistics.WebUI.UltraWebGrid.LayoutEventArgs) Handles UWD_Month.InitializeLayout
        e.Layout.Bands(0).Columns.FromKey("Region").Header.Caption = "Region"
        e.Layout.Bands(0).Columns.FromKey("Region").Width = 160
        e.Layout.Bands(0).Columns.FromKey("noregion").Hidden = True
        e.Layout.Bands(0).Columns.FromKey("Work Completed").Header.Caption = "Work Completed"
        e.Layout.Bands(0).Columns.FromKey("Work Completed").Width = 80
        e.Layout.Bands(0).Columns.FromKey("Work Completed").CellStyle.HorizontalAlign = HorizontalAlign.Center
        e.Layout.Bands(0).Columns.FromKey("Work Completed").Move(1)
        e.Layout.Bands(0).Columns.FromKey("BAUT Submitted").Header.Caption = "BAUT Submitted"
        e.Layout.Bands(0).Columns.FromKey("BAUT Submitted").Width = 80
        e.Layout.Bands(0).Columns.FromKey("BAUT Submitted").CellStyle.HorizontalAlign = HorizontalAlign.Center
        e.Layout.Bands(0).Columns.FromKey("BAUT Completed").Header.Caption = "BAUT Completed"
        e.Layout.Bands(0).Columns.FromKey("BAUT Completed").Width = 80
        e.Layout.Bands(0).Columns.FromKey("BAUT Completed").CellStyle.HorizontalAlign = HorizontalAlign.Center
        e.Layout.Bands(0).Columns.FromKey("BAST Submitted").Header.Caption = "BAST Submitted"
        e.Layout.Bands(0).Columns.FromKey("BAST Submitted").Width = 80
        e.Layout.Bands(0).Columns.FromKey("BAST Submitted").CellStyle.HorizontalAlign = HorizontalAlign.Center
        e.Layout.Bands(0).Columns.FromKey("BAST Completed").Header.Caption = "BAST Completed"
        e.Layout.Bands(0).Columns.FromKey("BAST Completed").Width = 80
        e.Layout.Bands(0).Columns.FromKey("BAST Completed").CellStyle.HorizontalAlign = HorizontalAlign.Center
    End Sub
    Protected Sub UWD_Over_All_Status_InitializeLayout(ByVal sender As Object, ByVal e As Infragistics.WebUI.UltraWebGrid.LayoutEventArgs) Handles UWD_Over_All_Status.InitializeLayout
        e.Layout.Bands(0).Columns.FromKey("region").Header.Caption = "Region"
        e.Layout.Bands(0).Columns.FromKey("region").Width = 160
        e.Layout.Bands(0).Columns.FromKey("noregion").Hidden = True
        e.Layout.Bands(0).Columns.FromKey("ContractpoDT").Header.Caption = "Total Po"
        e.Layout.Bands(0).Columns.FromKey("ContractpoDT").Width = 75
        e.Layout.Bands(0).Columns.FromKey("ContractpoDT").CellStyle.HorizontalAlign = HorizontalAlign.Center
        e.Layout.Bands(0).Columns.FromKey("ContractDT").Hidden = True
        e.Layout.Bands(0).Columns.FromKey("Cancel").Header.Caption = "Cancel"
        e.Layout.Bands(0).Columns.FromKey("Cancel").Width = 75
        e.Layout.Bands(0).Columns.FromKey("Cancel").CellStyle.HorizontalAlign = HorizontalAlign.Center
        e.Layout.Bands(0).Columns.FromKey("Active").Header.Caption = "Active"
        e.Layout.Bands(0).Columns.FromKey("Active").Width = 75
        e.Layout.Bands(0).Columns.FromKey("Active").CellStyle.HorizontalAlign = HorizontalAlign.Center
        e.Layout.Bands(0).Columns.FromKey("completed").Header.Caption = "Work Completed"
        e.Layout.Bands(0).Columns.FromKey("completed").Width = 75
        e.Layout.Bands(0).Columns.FromKey("completed").CellStyle.HorizontalAlign = HorizontalAlign.Center
        e.Layout.Bands(0).Columns.FromKey("bautdone").Header.Caption = "BAUT Done"
        e.Layout.Bands(0).Columns.FromKey("bautdone").Width = 75
        e.Layout.Bands(0).Columns.FromKey("bautdone").CellStyle.HorizontalAlign = HorizontalAlign.Center
        e.Layout.Bands(0).Columns.FromKey("bastdone").Header.Caption = "BAST Done"
        e.Layout.Bands(0).Columns.FromKey("bastdone").Width = 75
        e.Layout.Bands(0).Columns.FromKey("bastdone").CellStyle.HorizontalAlign = HorizontalAlign.Center
        e.Layout.Bands(0).Columns.FromKey("BASTPrecentage").Header.Caption = "%"
        e.Layout.Bands(0).Columns.FromKey("BASTPrecentage").Width = 35
        e.Layout.Bands(0).Columns.FromKey("BASTPrecentage").CellStyle.HorizontalAlign = HorizontalAlign.Center
        e.Layout.Bands(0).Columns.FromKey("Remark").Hidden = True
    End Sub
    Protected Sub uwd_Site_List_doc_InitializeLayout(ByVal sender As Object, ByVal e As Infragistics.WebUI.UltraWebGrid.LayoutEventArgs) Handles uwd_Site_List_doc.InitializeLayout
        e.Layout.Bands(0).Columns.FromKey("PoNo").Header.Caption = "PO #"
        e.Layout.Bands(0).Columns.FromKey("TotalSite").Header.Caption = "Total Sites"
        e.Layout.Bands(0).Columns.FromKey("TotalSite").Width = 80
        e.Layout.Bands(0).Columns.FromKey("TotalSite").CellStyle.HorizontalAlign = HorizontalAlign.Center
        e.Layout.Bands(0).Columns.FromKey("TotalDocument").Header.Caption = "Total Req Docs"
        e.Layout.Bands(0).Columns.FromKey("TotalDocument").Width = 90
        e.Layout.Bands(0).Columns.FromKey("TotalDocument").CellStyle.HorizontalAlign = HorizontalAlign.Center
        e.Layout.Bands(0).Columns.FromKey("TotalUploadDocument").Header.Caption = "Uploaded"
        e.Layout.Bands(0).Columns.FromKey("TotalUploadDocument").Width = 80
        e.Layout.Bands(0).Columns.FromKey("TotalUploadDocument").CellStyle.HorizontalAlign = HorizontalAlign.Center
        e.Layout.Bands(0).Columns.FromKey("NsnApproved").Header.Caption = "NSN Approved"
        e.Layout.Bands(0).Columns.FromKey("NsnApproved").Width = 90
        e.Layout.Bands(0).Columns.FromKey("NsnApproved").CellStyle.HorizontalAlign = HorizontalAlign.Center
        e.Layout.Bands(0).Columns.FromKey("CustomerApproved").Header.Caption = "Customer Approved"
        e.Layout.Bands(0).Columns.FromKey("CustomerApproved").Width = 90
        e.Layout.Bands(0).Columns.FromKey("CustomerApproved").CellStyle.HorizontalAlign = HorizontalAlign.Center
        e.Layout.Bands(0).Columns.FromKey("CompleteDocument").Header.Caption = "Completed"
        e.Layout.Bands(0).Columns.FromKey("CompleteDocument").Width = 80
        e.Layout.Bands(0).Columns.FromKey("CompleteDocument").CellStyle.HorizontalAlign = HorizontalAlign.Center
        e.Layout.Bands(0).Columns.FromKey("RemainingDocument").Header.Caption = "Remaining"
        e.Layout.Bands(0).Columns.FromKey("RemainingDocument").Width = 80
        e.Layout.Bands(0).Columns.FromKey("RemainingDocument").CellStyle.HorizontalAlign = HorizontalAlign.Center
        e.Layout.Bands(0).Columns.FromKey("Precentage").Header.Caption = "%"
        e.Layout.Bands(0).Columns.FromKey("Precentage").Width = 40
        e.Layout.Bands(0).Columns.FromKey("Precentage").CellStyle.HorizontalAlign = HorizontalAlign.Center
    End Sub
    Protected Sub ddlPO_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPO.SelectedIndexChanged
        BindTIChart()
    End Sub
    Sub CreateAgenda()
        Dim iMainTable As New HtmlTable
        iMainTable.Width = "100%"
        Dim tsites As Integer
        Dim dtTask As New DataTable
        dtTask = objBO.uspDashBoardAgendaTask(CommonSite.UserId())
        'commneted below loop and added this loop for document grouping by satish on 22nd november2010
        If dtTask.Rows.Count > 0 Then
            tsites = dtTask.Compute("sum(Site_No)", "")
            'added below by satish on 14thoctober2010 
            Dim iMainRownew As New HtmlTableRow
            Dim iMainCellSiteNamenew As New HtmlTableCell
            iMainCellSiteNamenew.Width = "75%"
            iMainCellSiteNamenew.Attributes.Add("class", "dashboard")
            iMainCellSiteNamenew.Style.Add("align", "left")
            Dim iMainCellPrecentagenew As New HtmlTableCell
            iMainCellPrecentagenew.Width = "25%"
            iMainCellPrecentagenew.Attributes.Add("class", "dashboard")
            iMainCellPrecentagenew.Style.Add("align", "right")
            iMainCellSiteNamenew.InnerHtml = "<a href='DashBoard/frmSiteDocCount.aspx?id=" & CommonSite.UserId() & "'>Task Pending Sites ( " & tsites & " ) </a>"
            iMainRownew.Cells.Add(iMainCellSiteNamenew)
            iMainRownew.Cells.Add(iMainCellPrecentagenew)
            iMainTable.Rows.Add(iMainRownew)
        End If
        Dim dtAgenda As New DataTable
        'dedy 091106
        'dtAgenda = objBO.uspDashBoardAgenda(CommonSite.GetDashBoardLevel(), CommonSite.UserId(), ConfigurationManager.AppSettings("WCTRBASTID"))
        strsql = "exec [uspDashBoardAgenda] " & CommonSite.GetDashBoardLevel().ToString & "," & CommonSite.UserId().ToString & "," & ConfigurationManager.AppSettings("WCTRBASTID").ToString & "," & Session("User_Id").ToString
        dtAgenda = objUtil.ExeQueryDT(strsql, "uspDashBoardAgenda")
        For Each dRowsStatus As DataRow In dtAgenda.Rows
            Dim iMainRow As New HtmlTableRow
            Dim iMainCellSiteName As New HtmlTableCell
            iMainCellSiteName.Width = "75%"
            iMainCellSiteName.Attributes.Add("class", "dashboard")
            iMainCellSiteName.Style.Add("align", "left")
            Dim iMainCellPrecentage As New HtmlTableCell
            iMainCellPrecentage.Width = "25%"
            iMainCellPrecentage.Attributes.Add("class", "dashboard")
            iMainCellPrecentage.Style.Add("align", "right")
            If dRowsStatus.Item("usrtype").ToString().ToLower() = "ac" Then
                If dRowsStatus.Item("CountUsrType") > 0 Then
                    iMainCellSiteName.InnerHtml = "<a href=dashboard/mom-Approve.aspx>Pending MOM Acknowlegment ( " & dRowsStatus.Item("CountUsrType") & " ) </a>"
                End If
            ElseIf dRowsStatus.Item("usrtype").ToString().ToLower() = "c" Then
                If dRowsStatus.Item("CountUsrType") > 0 Then
                    iMainCellSiteName.InnerHtml = "<a href=USR/frmUserList.aspx>Pending for Customer ( " & dRowsStatus.Item("CountUsrType") & " ) </a>"
                End If
            ElseIf dRowsStatus.Item("usrtype").ToString().ToLower() = "w" Then
                If CommonSite.GetDashBoardLevel() = 0 Then
                    If (CommonSite.UserId().ToString = "1") Then
                        If dRowsStatus.Item("CountUsrType") > 0 Then
                            iMainCellSiteName.InnerHtml = "<a href=# onclick=popwindowDashBoard(3)>Process Flow Pending Documents ( " & dRowsStatus.Item("CountUsrType") & " ) </a>"
                        End If
                    End If
                End If
            ElseIf dRowsStatus.Item("usrtype").ToString().ToLower() = "u" Then
                If dRowsStatus.Item("CountUsrType") > 0 Then
                    iMainCellSiteName.InnerHtml = "<a href=# onclick=popwindowDashBoard(4) >Exceeded SLA Documents ( " & dRowsStatus.Item("CountUsrType") & " ) </a>"
                End If
            ElseIf dRowsStatus.Item("usrtype").ToString().ToLower() = "su" Then
                If (CommonSite.GetDashBoardLevel() = 0) Then
                    If dRowsStatus.Item("CountUsrType") > 0 Then
                        iMainCellSiteName.InnerHtml = "<a href=# onclick=popwindowDashBoard(6) >Pending upload Documents ( " & dRowsStatus.Item("CountUsrType") & " ) </a>"
                    End If
                Else
                    If (CommonSite.UserId().ToString = "1") Then
                        If dRowsStatus.Item("CountUsrType") > 0 Then
                            iMainCellSiteName.InnerHtml = "<a href=# onclick=popwindowDashBoard(6) >Pending upload Documents ( " & dRowsStatus.Item("CountUsrType") & " ) </a>"
                        End If
                    End If
                End If
            ElseIf dRowsStatus.Item("usrtype").ToString().ToLower() = "r" Then
                If dRowsStatus.Item("CountUsrType") > 0 Then
                    iMainCellSiteName.InnerHtml = "<a href=# onclick=popwindowDashBoard(7) >Rejected Documents ( " & dRowsStatus.Item("CountUsrType") & " ) </a>"
                End If
            ElseIf dRowsStatus.Item("usrtype").ToString().ToLower() = "mi" Then
                If (CommonSite.UserId().ToString = "1") Then
                    If dRowsStatus.Item("CountUsrType") > 0 Then
                        iMainCellSiteName.InnerHtml = "<a href=# onclick=popwindowDashBoard(8) >No Work Package Id ( " & dRowsStatus.Item("CountUsrType") & " ) </a>"
                    End If
                End If
            ElseIf dRowsStatus.Item("usrtype").ToString().ToLower() = "mw" Then
                If (CommonSite.UserId().ToString = "1") Then
                    If dRowsStatus.Item("CountUsrType") > 0 Then
                        iMainCellSiteName.InnerHtml = "<a href=# onclick=popwindowDashBoard(9) >Missing EPM ( " & dRowsStatus.Item("CountUsrType") & " ) </a>"
                    End If
                End If
            ElseIf dRowsStatus.Item("usrtype").ToString().ToLower() = "dp" Then
                If dRowsStatus.Item("CountUsrType") > 0 Then
                    'iMainCellSiteName.InnerHtml = "<a href=# onclick=popwindowDashBoard(10) >Duplicate Sites ( " & dRowsStatus.Item("CountUsrType") & " ) </a>"
                End If
            ElseIf dRowsStatus.Item("usrtype").ToString().ToLower() = "ld" Then
                If (CommonSite.UserId().ToString <> "1") Then
                    If dRowsStatus.Item("CountUsrType") > 0 Then
                        iMainCellSiteName.InnerHtml = "<a href=dashboard/ViewLastSign.aspx>Document Signed Last 30 Days ( " & dRowsStatus.Item("CountUsrType") & " ) </a>"
                    End If
                End If
            Else
                'If dRowsStatus.Item("CountUsrType") > 0 Then
                '    iMainCellSiteName.InnerHtml = "<a href=USR/frmUserList.aspx> Pending for SubCon ( " & dRowsStatus.Item("CountUsrType") & " )</a>"
                'End If
            End If
            iMainRow.Cells.Add(iMainCellSiteName)
            iMainRow.Cells.Add(iMainCellPrecentage)
            iMainTable.Rows.Add(iMainRow)
        Next
        Dim dtNull As New DataTable
        dtNull = objUtil.ExeQueryDT("exec uspPDNull", "PDNull")
        If dtNull.Rows.Count > 0 Then
            Dim iMainRow As New HtmlTableRow
            Dim iMainCellSiteName As New HtmlTableCell
            iMainCellSiteName.Width = "75%"
            iMainCellSiteName.Attributes.Add("class", "dashboard")
            iMainCellSiteName.Style.Add("align", "left")
            Dim iMainCellPrecentage As New HtmlTableCell
            iMainCellPrecentage.Width = "25%"
            iMainCellPrecentage.Attributes.Add("class", "dashboard")
            iMainCellPrecentage.Style.Add("align", "right")
            iMainCellSiteName.InnerHtml = "<a href=PO/frmPOList.aspx?isnull=1>PO Scope having null values ( " & dtNull.Rows.Count.ToString & " ) </a>"
            iMainRow.Cells.Add(iMainCellSiteName)
            iMainRow.Cells.Add(iMainCellPrecentage)
            iMainTable.Rows.Add(iMainRow)
        End If
        Dim iMaingap As New HtmlTableRow
        Dim iMainCellgap As New HtmlTableCell
        iMainCellgap.ColSpan = 2
        iMainCellgap.Attributes.Add("class", "hgap")
        iMaingap.Cells.Add(iMainCellgap)
        iMainTable.Rows.Add(iMaingap)
        tdAgenda.Controls.Add(iMainTable)
    End Sub
    Sub CreateBaut()
        Dim iMainTable As New HtmlTable
    End Sub
    Sub CreateSiteStatus()
        Dim iMainTable As New HtmlTable
        Dim dtStatus As New DataTable
        Dim area As Integer
        Dim region As String
        Dim dtra As New DataTable
        Dim dtr As New DataTable
        Dim dta As New DataTable
        Dim mycase As String = ""
        Dim rgn As String = ""
        Dim i As Integer
        If Session("User_Name") Is Nothing Then
            Response.Redirect("~/SessionTimeOut.aspx")
        Else
            dtra = objUtil.ExeQueryDT("select ara_id,rgn_id from ebastuserrole where usr_id=" & Session("User_Id") & " and rstatus = 2", "RA")
            If dtra.Rows.Count = 0 Then
                mycase = "ALL"
                dtStatus = objUtil.ExeQueryDT("exec uspEBastDone " & ConfigurationManager.AppSettings("BAUTID") & "," & ConfigurationManager.AppSettings("BASTID"), "sitedoc")
            Else
                area = dtra.Rows(0).Item(0).ToString
                region = dtra.Rows(0).Item(1).ToString
                If region <> 0 Then
                    'region user
                    dtStatus = objUtil.ExeQueryDT("exec uspEBastDoneNew2 " & ConfigurationManager.AppSettings("BAUTID") & "," & ConfigurationManager.AppSettings("BASTID") & "," & Session("User_Id") & "", "sitedoc")
                ElseIf area <> 0 Then
                    'area user
                    dtStatus = objUtil.ExeQueryDT("exec uspEBastDoneNew3 " & ConfigurationManager.AppSettings("BAUTID") & "," & ConfigurationManager.AppSettings("BASTID") & "," & Session("User_Id") & "", "sitedoc")
                Else
                    mycase = "ALL"
                    'national user
                    dtStatus = objUtil.ExeQueryDT("exec uspEBastDone " & ConfigurationManager.AppSettings("BAUTID") & "," & ConfigurationManager.AppSettings("BASTID"), "sitedoc")
                End If
            End If
            Dim strBind As String = ""
            Dim strBindNo As String = ""
            Dim strBinddays As String = ""
            If dtStatus.Rows.Count > 1 Then
                For intCount As Integer = 0 To dtStatus.Rows.Count - 1
                    If (dtStatus.Rows(intCount)("ECount") = 0) Then
                        strBind = "<div class=""hgap"">" & dtStatus.Rows(intCount)("Process").ToString & " </div>"
                        strBindNo = "<div class=""hgap"">( " & dtStatus.Rows(intCount)("ECount").ToString & " )</div>"
                        strBinddays = "<div class=""hgap"">" & dtStatus.Rows(intCount)("nodays").ToString & "</div>"
                    Else
                        If dtStatus.Rows(intCount)("nodays") Is DBNull.Value Then
                            strBind = "<div class=""hgap""><a href=# onclick=popSitesDetails(" + intCount.ToString + ")> " & dtStatus.Rows(intCount)("Process").ToString & "  </a></div>"
                            strBindNo = "<div class=""hgap"">( " & dtStatus.Rows(intCount)("ECount").ToString & " )"
                            strBinddays = "<div class=""hgap"">" & dtStatus.Rows(intCount)("nodays").ToString & "</div>"
                        Else
                            strBind = "<div class=""hgap""><a href=# onclick=popSitesDetails(" + intCount.ToString + ")> " & dtStatus.Rows(intCount)("Process").ToString & "  </a></div>"
                            strBindNo = "<div class=""hgap"">( " & dtStatus.Rows(intCount)("ECount").ToString & " )"
                            strBinddays = dtStatus.Rows(intCount)("nodays").ToString & " Days"
                        End If
                    End If
                    Select Case intCount
                        Case 0
                            Tdbastdone.InnerHtml = strBind
                            TdbastdoneNO.InnerHtml = strBindNo
                            Tdbastdonedays.InnerHtml = strBinddays
                        Case 1
                            tdTbastsignature.InnerHtml = strBind
                            tdTbastsignatureno.InnerHtml = strBindNo
                            tdTbastsignaturedays.InnerHtml = strBinddays
                        Case 2
                            tdNbastsignature.InnerHtml = strBind
                            tdNbastsignatureNO.InnerHtml = strBindNo
                            tdNbastsignaturedays.InnerHtml = strBinddays
                        Case 3
                            Tdready4bast.InnerHtml = strBind
                            Tdready4bastno.InnerHtml = strBindNo
                            Tdready4bastdays.InnerHtml = strBinddays
                        Case 4
                            tdbastprocessing.InnerHtml = strBind
                            tdbastprocessingno.InnerHtml = strBindNo
                            tdbastprocessingdays.InnerHtml = strBinddays
                        Case 5
                            tdbautdone.InnerHtml = strBind
                            tdbautdoneno.InnerHtml = strBindNo
                            tdbautdonedays.InnerHtml = strBinddays
                        Case 6
                            tdTbautsiganture.InnerHtml = strBind
                            tdTbautsigantureno.InnerHtml = strBindNo
                            tdTbautsiganturedays.InnerHtml = strBinddays
                        Case 7
                            tdNbautsiganture.InnerHtml = strBind
                            tdNbautsigantureNo.InnerHtml = strBindNo
                            tdNbautsiganturedays.InnerHtml = strBinddays
                        Case 8
                            tdready4baut.InnerHtml = strBind
                            tdready4bautno.InnerHtml = strBindNo
                            tdready4bautdays.InnerHtml = strBinddays
                        Case 9
                            tdbautprocessing.InnerHtml = strBind
                            tdbautprocessingno.InnerHtml = strBindNo
                            tdbautprocessingdays.InnerHtml = strBinddays
                    End Select
                Next
            End If
        End If
    End Sub
    Protected Sub UWG_EPM_Detail_PO_InitializeLayout(ByVal sender As Object, ByVal e As Infragistics.WebUI.UltraWebGrid.LayoutEventArgs)
        e.Layout.Bands(0).Columns.FromKey("PONo").Header.Caption = "Customer Po"
        e.Layout.Bands(0).Columns.FromKey("Poname").Header.Caption = "PO Name"
        e.Layout.Bands(0).Columns.FromKey("Poname").Width = 120
        e.Layout.Bands(0).Columns.FromKey("Poname").CellStyle.Wrap = True
        e.Layout.Bands(0).Columns.FromKey("ContractpoDT").Header.Caption = "Total Po"
        e.Layout.Bands(0).Columns.FromKey("ContractpoDT").Width = 58
        e.Layout.Bands(0).Columns.FromKey("ContractpoDT").CellStyle.HorizontalAlign = HorizontalAlign.Center
        e.Layout.Bands(0).Columns.FromKey("ContractDT").Header.Caption = "Total EPM"
        e.Layout.Bands(0).Columns.FromKey("ContractDT").Width = 58
        e.Layout.Bands(0).Columns.FromKey("ContractDT").CellStyle.HorizontalAlign = HorizontalAlign.Center
        e.Layout.Bands(0).Columns.FromKey("Cancel").Header.Caption = "Cancel"
        e.Layout.Bands(0).Columns.FromKey("Cancel").Width = 58
        e.Layout.Bands(0).Columns.FromKey("Cancel").CellStyle.HorizontalAlign = HorizontalAlign.Center
        e.Layout.Bands(0).Columns.FromKey("Active").Header.Caption = "Active"
        e.Layout.Bands(0).Columns.FromKey("Active").Width = 58
        e.Layout.Bands(0).Columns.FromKey("Active").CellStyle.HorizontalAlign = HorizontalAlign.Center
        e.Layout.Bands(0).Columns.FromKey("completed").Header.Caption = "Work Completed"
        e.Layout.Bands(0).Columns.FromKey("completed").Width = 58
        e.Layout.Bands(0).Columns.FromKey("completed").CellStyle.HorizontalAlign = HorizontalAlign.Center
        e.Layout.Bands(0).Columns.FromKey("bautdone").Header.Caption = "Baut Done"
        e.Layout.Bands(0).Columns.FromKey("bautdone").Width = 58
        e.Layout.Bands(0).Columns.FromKey("bautdone").CellStyle.HorizontalAlign = HorizontalAlign.Center
        e.Layout.Bands(0).Columns.FromKey("bastdone").Header.Caption = "Bast Done"
        e.Layout.Bands(0).Columns.FromKey("bastdone").Width = 58
        e.Layout.Bands(0).Columns.FromKey("bastdone").CellStyle.HorizontalAlign = HorizontalAlign.Center
        e.Layout.Bands(0).Columns.FromKey("BASTPrecentage").Header.Caption = "%"
        e.Layout.Bands(0).Columns.FromKey("BASTPrecentage").Width = 45
        e.Layout.Bands(0).Columns.FromKey("BASTPrecentage").CellStyle.HorizontalAlign = HorizontalAlign.Center
        e.Layout.Bands(0).Columns.FromKey("Remark").Hidden = True
    End Sub
    Protected Sub UWG_EPM_Detail_PO_Click(ByVal sender As Object, ByVal e As Infragistics.WebUI.UltraWebGrid.ClickEventArgs)
        Me.WDM_EPM_Detail_PopUp.WindowState = Infragistics.Web.UI.LayoutControls.DialogWindowState.Normal
        Dim Pono As String = UWG_EPM_Detail_PO.DisplayLayout.ActiveRow.Cells.FromKey("pono").Text
        Dim dtRegion As New DataTable()
        dtRegion = objUtil.ExeQueryDT("exec uspDashboardDetailspononew '" & Pono & "',1," & CommonSite.GetDashBoardLevel() & "," & CommonSite.UserId(), "MilestoneTrackCount")
        WGD_EPM_Detail.DataSource = dtRegion
        WGD_EPM_Detail.DataBind()
    End Sub
    Protected Sub WGD_EPM_Detail_InitializeLayout(ByVal sender As Object, ByVal e As Infragistics.WebUI.UltraWebGrid.LayoutEventArgs)
        e.Layout.Bands(0).Columns.FromKey("SiteNo").Header.Caption = "Site No"
        e.Layout.Bands(0).Columns.FromKey("SiteNo").Width = 110
        e.Layout.Bands(0).Columns.FromKey("SiteName").Header.Caption = "Site Name"
        e.Layout.Bands(0).Columns.FromKey("SiteName").Width = 200
        e.Layout.Bands(0).Columns.FromKey("SiteName").CellStyle.Wrap = True
        e.Layout.Bands(0).Columns.FromKey("PONo").Header.Caption = "Customer Po"
        e.Layout.Bands(0).Columns.FromKey("PONo").Move(0)
        e.Layout.Bands(0).Columns.FromKey("poname").Header.Caption = "Po Name"
        e.Layout.Bands(0).Columns.FromKey("poname").Move(1)
        e.Layout.Bands(0).Columns.FromKey("poname").Width = 150
        e.Layout.Bands(0).Columns.FromKey("poname").CellStyle.Wrap = True
        e.Layout.Bands(0).Columns.FromKey("TSELPROJECTID").Header.Caption = "TSEL ID"
        e.Layout.Bands(0).Columns.FromKey("WorkPackageId").Header.Caption = "WPID"
        e.Layout.Bands(0).Columns.FromKey("Scope").Header.Caption = "Scope"
        e.Layout.Bands(0).Columns.FromKey("Scope").Width = 150
        e.Layout.Bands(0).Columns.FromKey("Scope").CellStyle.Wrap = True
        e.Layout.Bands(0).Columns.FromKey("CustomerPORecordDate").Hidden = True
        e.Layout.Bands(0).Columns.FromKey("POType").Hidden = True
        e.Layout.Bands(0).Columns.FromKey("completed").Header.Caption = "Work Completed"
        e.Layout.Bands(0).Columns.FromKey("completed").Width = 53
        e.Layout.Bands(0).Columns.FromKey("completed").CellStyle.HorizontalAlign = HorizontalAlign.Center
        e.Layout.Bands(0).Columns.FromKey("sitefolderrecieved").Header.Caption = "Site Folder Recieved"
        e.Layout.Bands(0).Columns.FromKey("sitefolderrecieved").Width = 100
        e.Layout.Bands(0).Columns.FromKey("sitefolderrecieved").CellStyle.HorizontalAlign = HorizontalAlign.Center
        e.Layout.Bands(0).Columns.FromKey("kpi").Header.Caption = "KPI"
        e.Layout.Bands(0).Columns.FromKey("kpi").Width = 40
        e.Layout.Bands(0).Columns.FromKey("kpi").CellStyle.HorizontalAlign = HorizontalAlign.Center
        e.Layout.Bands(0).Columns.FromKey("bautsubmitted").Header.Caption = "BAUT Submitted"
        e.Layout.Bands(0).Columns.FromKey("bautsubmitted").Width = 50
        e.Layout.Bands(0).Columns.FromKey("bautsubmitted").CellStyle.HorizontalAlign = HorizontalAlign.Center
        e.Layout.Bands(0).Columns.FromKey("bautdone").Header.Caption = "BAUT Done"
        e.Layout.Bands(0).Columns.FromKey("bautdone").Width = 50
        e.Layout.Bands(0).Columns.FromKey("bautdone").CellStyle.HorizontalAlign = HorizontalAlign.Center
        e.Layout.Bands(0).Columns.FromKey("bastsubmitted").Header.Caption = "BAST Submitted"
        e.Layout.Bands(0).Columns.FromKey("bastsubmitted").Width = 50
        e.Layout.Bands(0).Columns.FromKey("bastsubmitted").CellStyle.HorizontalAlign = HorizontalAlign.Center
        e.Layout.Bands(0).Columns.FromKey("bastdone").Header.Caption = "BAST Done"
        e.Layout.Bands(0).Columns.FromKey("bastdone").Width = 50
        e.Layout.Bands(0).Columns.FromKey("bastdone").CellStyle.HorizontalAlign = HorizontalAlign.Center
    End Sub
    Protected Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Me.WDM_EPM_Main.WindowState = Infragistics.Web.UI.LayoutControls.DialogWindowState.Hidden
    End Sub
    Protected Sub btnClose1_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Me.WDM_EPM_Detail_PopUp.WindowState = Infragistics.Web.UI.LayoutControls.DialogWindowState.Hidden
    End Sub
    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        UltraWebGridExcelExporter1.ExcelStartRow = 0
        UltraWebGridExcelExporter1.Export(UWG_EPM_Detail_PO)
        UltraWebGridExcelExporter1.DownloadName = "Over All Status By Region"
    End Sub
    Protected Sub btnExport1_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        'UltraWebGridExcelExporter1.ExcelStartRow = 0
        'UltraWebGridExcelExporter1.Export(WGD_EPM_Detail)
        'UltraWebGridExcelExporter1.DownloadName = "Over All Status By PO"
    End Sub
    Protected Sub ddYear_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddYear.SelectedIndexChanged
        Open_Achievement()
    End Sub
End Class
