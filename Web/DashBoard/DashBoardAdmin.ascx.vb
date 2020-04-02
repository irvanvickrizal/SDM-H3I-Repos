Imports System.Data
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging
Imports System.Collections
Imports System.Web.Security
Imports BusinessLogic
Imports Common
Imports Entities
Partial Class Include_DashBoardAdmin
    Inherits System.Web.UI.UserControl
    Dim objBOD As New BODDLs
    Dim objET As New ETEBASTUsers
    Dim objBO As New BODashBoard
    Dim dtdg As New DataTable
    Dim objUtil As New DBUtil

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'createTable()
        If Page.IsPostBack = False Then
            If Session("User_Type") = "S" Then
            End If
            BindDataEPM_Infra()
            Check_Month()
            Open_Achievement()
        End If


    End Sub

    Sub Open_Achievement()
        Dim dtChart As New DataTable()
        dtChart = objUtil.ExeQueryDT("exec uspgetgrouping4month " & ddMonth.SelectedValue & "," & ddYear.SelectedValue, "MilestoneTrackCount")
        UWD_Month.DataSource = dtChart
        UWD_Month.DataBind()
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
            WebPanel2.Header.Text = "Monthly Achievement (January)"
        End If

        If Date1 = 2 Then
            ddMonth.SelectedIndex = 2
            WebPanel2.Header.Text = "Monthly Achievement (February)"
        End If
        If Date1 = 3 Then
            ddMonth.SelectedIndex = 3
            WebPanel2.Header.Text = "Monthly Achievement (March)"
        End If
        If Date1 = 4 Then
            ddMonth.SelectedIndex = 4
            WebPanel2.Header.Text = "Monthly Achievement (April)"
        End If
        If Date1 = 5 Then
            ddMonth.SelectedIndex = 5
            WebPanel2.Header.Text = "Monthly Achievement (May)"
        End If
        If Date1 = 6 Then
            ddMonth.SelectedIndex = 6
            WebPanel2.Header.Text = "Monthly Achievement (June)"
        End If
        If Date1 = 7 Then
            ddMonth.SelectedIndex = 7
            WebPanel2.Header.Text = "Monthly Achievement (July)"
        End If
        If Date1 = 8 Then
            ddMonth.SelectedIndex = 8
            WebPanel2.Header.Text = "Monthly Achievement (August)"
        End If
        If Date1 = 9 Then
            ddMonth.SelectedIndex = 9
            WebPanel2.Header.Text = "Monthly Achievement (September)"
        End If

        If Date1 = 10 Then
            ddMonth.SelectedIndex = 10
            WebPanel2.Header.Text = "Monthly Achievement (October)"
        End If

        If Date1 = 11 Then
            ddMonth.SelectedIndex = 11
            WebPanel2.Header.Text = "Monthly Achievement (November)"
        End If

        If Date1 = 12 Then
            ddMonth.SelectedIndex = 12
            WebPanel2.Header.Text = "Monthly Achievement (December)"
        End If

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

#Region "Add by Suresh"
  

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
            'myrow1("BASTPrecentage") = Math.Round((BastPrecentage / dtRegion.Rows.Count)).ToString + " %"
            myrow1("BASTPrecentage") = Format((bastdone / Active) * 100, "#0").ToString + " %"
            myrow1("Remark") = ""
            dtdg.Rows.Add(myrow1)
        End If
        UWD_EPM.DataSource = dtdg
        Try
            UWD_EPM.DataBind()
        Catch ex As Exception
            ' GVEPM.PageIndex = 0
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
            'myrow1("BASTPrecentage") = Math.Round((BastPrecentage / dtRegion.Rows.Count)).ToString + " %"
            myrow1("BASTPrecentage") = Format((bastdone / Active) * 100, "#0").ToString + " %"
            myrow1("Remark") = ""
            dtdg.Rows.Add(myrow1)
        End If

        UWG_EPM_Detail_PO.DataSource = dtdg
        Try
            UWG_EPM_Detail_PO.DataBind()
        Catch ex As Exception
            ' GVEPM.PageIndex = 0
        End Try
    End Sub
#End Region

    Protected Sub UWD_EPM_Click(ByVal sender As Object, ByVal e As Infragistics.WebUI.UltraWebGrid.ClickEventArgs)
       


    End Sub

    Protected Sub UWG_EPM_Detail_PO_Click(ByVal sender As Object, ByVal e As Infragistics.WebUI.UltraWebGrid.ClickEventArgs) Handles UWG_EPM_Detail_PO.Click

        Me.WDM_EPM_Detail_PopUp.WindowState = Infragistics.Web.UI.LayoutControls.DialogWindowState.Normal
        Dim Pono As String = UWG_EPM_Detail_PO.DisplayLayout.ActiveRow.Cells.FromKey("pono").Text
        Dim dtRegion As New DataTable()
        dtRegion = objUtil.ExeQueryDT("exec uspDashboardDetailspononew '" & Pono & "',1," & CommonSite.GetDashBoardLevel() & "," & CommonSite.UserId(), "MilestoneTrackCount")
        WGD_EPM_Detail.DataSource = dtRegion
        WGD_EPM_Detail.DataBind()
    End Sub

    Protected Sub UWG_EPM_Detail_PO_InitializeLayout(ByVal sender As Object, ByVal e As Infragistics.WebUI.UltraWebGrid.LayoutEventArgs) Handles UWG_EPM_Detail_PO.InitializeLayout
        e.Layout.Bands(0).Columns.FromKey("PONo").Header.Caption = "Customer Po"
        e.Layout.Bands(0).Columns.FromKey("Poname").Header.Caption = "PO Name"
        e.Layout.Bands(0).Columns.FromKey("Poname").Width = 250
        e.Layout.Bands(0).Columns.FromKey("ContractpoDT").Header.Caption = "Total Po"
        e.Layout.Bands(0).Columns.FromKey("ContractpoDT").Width = 60
        e.Layout.Bands(0).Columns.FromKey("ContractDT").Header.Caption = "Total EPM"
        e.Layout.Bands(0).Columns.FromKey("ContractDT").Width = 60
        ' e.Layout.Bands(0).Columns.FromKey("ContractDT").Header.Caption = "Contract"
        e.Layout.Bands(0).Columns.FromKey("Cancel").Header.Caption = "Cancel"
        e.Layout.Bands(0).Columns.FromKey("Cancel").Width = 60
        e.Layout.Bands(0).Columns.FromKey("Active").Header.Caption = "Active"
        e.Layout.Bands(0).Columns.FromKey("Active").Width = 60
        e.Layout.Bands(0).Columns.FromKey("completed").Header.Caption = "Task Completed"
        e.Layout.Bands(0).Columns.FromKey("completed").Width = 70
        e.Layout.Bands(0).Columns.FromKey("bautdone").Header.Caption = "Baut Done"
        e.Layout.Bands(0).Columns.FromKey("bautdone").Width = 60
        e.Layout.Bands(0).Columns.FromKey("bastdone").Header.Caption = "Bast Done"
        e.Layout.Bands(0).Columns.FromKey("bastdone").Width = 60
        e.Layout.Bands(0).Columns.FromKey("BASTPrecentage").Header.Caption = "%"
        e.Layout.Bands(0).Columns.FromKey("BASTPrecentage").Width = 60
        e.Layout.Bands(0).Columns.FromKey("Remark").Header.Caption = "Remark"
        e.Layout.Bands(0).Columns.FromKey("Remark").Hidden = True
    End Sub

    Protected Sub UWD_EPM_InitializeLayout(ByVal sender As Object, ByVal e As Infragistics.WebUI.UltraWebGrid.LayoutEventArgs)
        e.Layout.Bands(0).Columns.FromKey("region").Header.Caption = "Region"
        e.Layout.Bands(0).Columns.FromKey("region").Width = 200

        e.Layout.Bands(0).Columns.FromKey("noregion").Hidden = True
        e.Layout.Bands(0).Columns.FromKey("ContractpoDT").Header.Caption = "Total Po"
        e.Layout.Bands(0).Columns.FromKey("ContractpoDT").Width = 100
        e.Layout.Bands(0).Columns.FromKey("ContractDT").Header.Caption = "Contract"
        e.Layout.Bands(0).Columns.FromKey("ContractDT").Width = 100
        e.Layout.Bands(0).Columns.FromKey("Cancel").Header.Caption = "Cancel"
        e.Layout.Bands(0).Columns.FromKey("Cancel").Width = 80
        e.Layout.Bands(0).Columns.FromKey("Active").Header.Caption = "Active"
        e.Layout.Bands(0).Columns.FromKey("Active").Width = 80
        e.Layout.Bands(0).Columns.FromKey("completed").Header.Caption = "Completed"
        e.Layout.Bands(0).Columns.FromKey("completed").Width = 80

        e.Layout.Bands(0).Columns.FromKey("bautdone").Header.Caption = "Baut Done"
        e.Layout.Bands(0).Columns.FromKey("bautdone").Width = 50
        e.Layout.Bands(0).Columns.FromKey("bastdone").Header.Caption = "Bast Done"
        e.Layout.Bands(0).Columns.FromKey("bastdone").Width = 50
        e.Layout.Bands(0).Columns.FromKey("BASTPrecentage").Header.Caption = "%"
        e.Layout.Bands(0).Columns.FromKey("BASTPrecentage").Width = 35
        ' e.Layout.Bands(0).Columns.FromKey("Remark").Header.Caption = "Remark"
        e.Layout.Bands(0).Columns.FromKey("Remark").Hidden = True
        'e.Layout.Bands(0).Columns.FromKey("Remark").Width = 100
      

    End Sub

    Protected Sub WGD_EPM_Detail_InitializeLayout(ByVal sender As Object, ByVal e As Infragistics.WebUI.UltraWebGrid.LayoutEventArgs) Handles WGD_EPM_Detail.InitializeLayout
        e.Layout.Bands(0).Columns.FromKey("SiteNo").Header.Caption = "Site No"
        e.Layout.Bands(0).Columns.FromKey("SiteNo").Width = 110
        e.Layout.Bands(0).Columns.FromKey("SiteName").Header.Caption = "Site Name"
        e.Layout.Bands(0).Columns.FromKey("SiteName").Width = 350
        e.Layout.Bands(0).Columns.FromKey("PONo").Header.Caption = "Customer Po"
        e.Layout.Bands(0).Columns.FromKey("PONo").Move(0)
        e.Layout.Bands(0).Columns.FromKey("poname").Header.Caption = "Po Name"
        e.Layout.Bands(0).Columns.FromKey("poname").Move(1)
        e.Layout.Bands(0).Columns.FromKey("poname").Width = 200
        e.Layout.Bands(0).Columns.FromKey("TSELPROJECTID").Header.Caption = "TSEL ID"
        e.Layout.Bands(0).Columns.FromKey("WorkPackageId").Header.Caption = "WPID"

        e.Layout.Bands(0).Columns.FromKey("Scope").Header.Caption = "Scope"
        e.Layout.Bands(0).Columns.FromKey("Scope").Width = 250
        e.Layout.Bands(0).Columns.FromKey("CustomerPORecordDate").Hidden = True
        e.Layout.Bands(0).Columns.FromKey("POType").Hidden = True
        e.Layout.Bands(0).Columns.FromKey("completed").Header.Caption = "Completed"
        e.Layout.Bands(0).Columns.FromKey("completed").Width = 60
        e.Layout.Bands(0).Columns.FromKey("sitefolderrecieved").Header.Caption = "Site Folder Recieved"
        e.Layout.Bands(0).Columns.FromKey("sitefolderrecieved").Width = 100
        e.Layout.Bands(0).Columns.FromKey("kpi").Header.Caption = "KPI"
        e.Layout.Bands(0).Columns.FromKey("kpi").Width = 40
        e.Layout.Bands(0).Columns.FromKey("bautsubmitted").Header.Caption = "BAUT Submitted"
        e.Layout.Bands(0).Columns.FromKey("bautdone").Header.Caption = "BAUT Done"
        e.Layout.Bands(0).Columns.FromKey("bautdone").Width = 60
        e.Layout.Bands(0).Columns.FromKey("bastsubmitted").Header.Caption = "BAST Submitted"
        e.Layout.Bands(0).Columns.FromKey("bastsubmitted").Width = 60
        e.Layout.Bands(0).Columns.FromKey("bastdone").Header.Caption = "BAST Done"
        e.Layout.Bands(0).Columns.FromKey("bastdone").Width = 60
    End Sub

    'Protected Sub UltraWebGrid1_InitializeLayout(ByVal sender As Object, ByVal e As Infragistics.WebUI.UltraWebGrid.LayoutEventArgs) Handles UltraWebGrid1.InitializeLayout
    '    e.Layout.Bands(0).Columns.FromKey("PoNo").Header.Caption = "PO #"
    '    e.Layout.Bands(0).Columns.FromKey("TotalSite").Header.Caption = "Total Sites"
    '    e.Layout.Bands(0).Columns.FromKey("TotalDocument").Header.Caption = "Total Req Docs"
    '    e.Layout.Bands(0).Columns.FromKey("TotalUploadDocument").Header.Caption = "Uploaded"
    '    e.Layout.Bands(0).Columns.FromKey("NsnApproved").Header.Caption = "NSN Approved"
    '    e.Layout.Bands(0).Columns.FromKey("CustomerApproved").Header.Caption = "Customer Approved"
    '    e.Layout.Bands(0).Columns.FromKey("CustomerApproved").Width = 150
    '    e.Layout.Bands(0).Columns.FromKey("CompleteDocument").Header.Caption = "Completed"
    '    e.Layout.Bands(0).Columns.FromKey("RemainingDocument").Header.Caption = "Remaining"


    '    e.Layout.Bands(0).Columns.FromKey("Precentage").Header.Caption = "%"


    'End Sub

    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Me.WDM_EPM_Main.WindowState = Infragistics.Web.UI.LayoutControls.DialogWindowState.Hidden

    End Sub

    Protected Sub btnClose1_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Me.WDM_EPM_Detail_PopUp.WindowState = Infragistics.Web.UI.LayoutControls.DialogWindowState.Hidden
    End Sub

    Private Function IsActiveRow() As Boolean
        If Me.UWD_EPM.DisplayLayout.ActiveRow Is Nothing Then
            Return False
        End If
        If (UWD_EPM.DisplayLayout.ActiveRow.Cells(0).Text = "") Then
            Return False
        End If
        Return True
    End Function

   
    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        UltraWebGridExcelExporter1.ExcelStartRow = 0
        UltraWebGridExcelExporter1.Export(UWG_EPM_Detail_PO)
        UltraWebGridExcelExporter1.DownloadName = "c:/File to Upload/Test_Export"
    End Sub

    Protected Sub btnExport1_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        UltraWebGridExcelExporter1.ExcelStartRow = 0
        UltraWebGridExcelExporter1.Export(WGD_EPM_Detail)
        UltraWebGridExcelExporter1.DownloadName = "Overall Status by PO"
    End Sub

    Protected Sub ddMonth_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Change_Month()
        Open_Achievement()
    End Sub

  
    Protected Sub UWD_EPM_Click1(ByVal sender As Object, ByVal e As Infragistics.WebUI.UltraWebGrid.ClickEventArgs)
        If IsActiveRow() Then
            BindDataEPM_Infra_Detail(UWD_EPM.DisplayLayout.ActiveRow.Cells.FromKey("noregion").Text)
            Me.WDM_EPM_Main.WindowState = Infragistics.Web.UI.LayoutControls.DialogWindowState.Normal
        End If
    End Sub

    Protected Sub UWD_Month_InitializeLayout(ByVal sender As Object, ByVal e As Infragistics.WebUI.UltraWebGrid.LayoutEventArgs)
        e.Layout.Bands(0).Columns.FromKey("Region").Header.Caption = "Region"
        e.Layout.Bands(0).Columns.FromKey("Region").Width = 160
        e.Layout.Bands(0).Columns.FromKey("noregion").Hidden = True
        'e.Layout.Bands(0).Columns.FromKey("Site Folder Rec").Width = 85
        e.Layout.Bands(0).Columns.FromKey("Work Completed").Header.Caption = "Work Completed"
        e.Layout.Bands(0).Columns.FromKey("Work Completed").Width = 85
        e.Layout.Bands(0).Columns.FromKey("BAUT Submitted").Header.Caption = "BAUT Submitted"
        e.Layout.Bands(0).Columns.FromKey("BAUT Submitted").Width = 85
        e.Layout.Bands(0).Columns.FromKey("BAUT Completed").Header.Caption = "BAUT Completed"
        e.Layout.Bands(0).Columns.FromKey("BAUT Completed").Width = 85
        e.Layout.Bands(0).Columns.FromKey("BAST Submitted").Header.Caption = "BAST Submitted"
        e.Layout.Bands(0).Columns.FromKey("BAST Submitted").Width = 85
        e.Layout.Bands(0).Columns.FromKey("BAST Completed").Header.Caption = "BAST Completed"
        e.Layout.Bands(0).Columns.FromKey("BAST Completed").Width = 85
        e.Layout.ColFootersVisibleDefault = Infragistics.WebUI.UltraWebGrid.ShowMarginInfo.Yes
        e.Layout.Bands(0).Columns(0).FooterText = "Total"


        'e.Layout.Bands(0).Columns.FromKey("Work Completed").Footer.Total = Infragistics.WebUI.UltraWebGrid.SummaryInfo.Sum
        'e.Layout.Bands(0).Columns.FromKey("Work Completed").Footer.Formula = "SUM([Work Completed])"
    End Sub
End Class
