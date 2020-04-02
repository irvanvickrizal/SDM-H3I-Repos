
Imports BusinessLogic
Imports Common
Imports System
Imports System.Data
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.IO
Imports System.IO.StreamWriter
Partial Class RPT_frmFinancialReports
    Inherits System.Web.UI.Page
    Dim objbo As New BODashBoard
    Dim objdl As New BODDLs
    Dim rpt As New ReportDocument
    Dim objUtil As New DBUtil

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        If Not Session("doc") Is Nothing Then
            CrystalReportViewer1.ReportSource = CType(Session("doc"), ReportDocument)
            CrystalReportViewer1.DataBind()
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            objdl.fillDDL(ddlPO, "PoNo", True, Constants._DDL_Default_All)
            objdl.fillDDL(DDMonth, "GetMonthFinanicialReport1", True, Constants._DDL_Default_Select)
            objdl.fillDDL(DDYear, "GetYearFinanicialReport1", True, Constants._DDL_Default_Select)

            If Not Request("pono") Is Nothing And Not Request("pono") = "0" Then ddlPO.SelectedValue = Request("pono")
            If Not Request("month") Is Nothing And Not Request("month") = "0" Then DDMonth.SelectedValue = Request("month")
            If Not Request("year") Is Nothing And Not Request("year") = "0" Then DDYear.SelectedValue = Request("year")
        End If

        tdTitle.Style.Add("display", "none")

        If Not ddlPO.SelectedValue Is Nothing And Not ddlPO.SelectedValue = "0" Then
            FinanicalReport()
            displayGraph()
        End If
    End Sub

    Sub FinanicalReport()
        Dim iMainTable As New HtmlTable
        iMainTable.Width = "100%"
        iMainTable.Border = 1
        iMainTable.Align = "left"
        iMainTable.BorderColor = "#ffffff"
        iMainTable.CellPadding = 0
        iMainTable.CellSpacing = 0

        Dim iMainRows1 As New HtmlTableRow

        Dim iMianCell11 As New HtmlTableCell
        Dim iMianCell12 As New HtmlTableCell
        'Dim iMianCell13 As New HtmlTableCell
        'Dim iMianCell14 As New HtmlTableCell
        Dim iMianCell15 As New HtmlTableCell
        Dim iMianCell16 As New HtmlTableCell
        'Dim iMianCell17 As New HtmlTableCell
        'Dim iMianCell18 As New HtmlTableCell

        iMianCell11.Attributes.Add("class", "GridHeaderCenter")
        iMianCell11.InnerHtml = "Customer&nbsp;PO&nbsp;"
        iMianCell12.Attributes.Add("class", "GridHeaderCenter")
        iMianCell12.InnerHtml = "Scope"
        'iMianCell13.Attributes.Add("class", "GridHeaderCenter")
        'iMianCell13.InnerHtml = "Sites Integrated"
        'iMianCell14.Attributes.Add("class", "GridHeaderCenter")
        'iMianCell14.InnerHtml = "Potential Sales&nbsp;Accruals <font style=""color:red; font-size:16px""><sup> * </sup></font>"
        iMianCell15.Attributes.Add("class", "GridHeaderCenter")
        iMianCell15.InnerHtml = "On&nbsp;Air"
        iMianCell16.Attributes.Add("class", "GridHeaderCenter")
        iMianCell16.InnerHtml = "Realized Sales <font style=""color:red; font-size:16px""><sup> * </sup></font>"
        'iMianCell17.Attributes.Add("class", "GridHeaderCenter")
        'iMianCell17.InnerHtml = "BAST"
        'iMianCell18.Attributes.Add("class", "GridHeaderCenter")
        'iMianCell18.InnerHtml = "Invoice Pending <font style=""color:red; font-size:16px""><sup> * </sup></font>"

        iMainRows1.Cells.Add(iMianCell11)
        iMainRows1.Cells.Add(iMianCell12)
        'iMainRows1.Cells.Add(iMianCell13)
        'iMainRows1.Cells.Add(iMianCell14)
        iMainRows1.Cells.Add(iMianCell15)
        iMainRows1.Cells.Add(iMianCell16)
        'iMainRows1.Cells.Add(iMianCell17)
        'iMainRows1.Cells.Add(iMianCell18)
        iMainTable.Rows.Add(iMainRows1)

        Dim dtStatus As New DataTable
        dtStatus = objUtil.ExeQueryDT("Exec GetFinanicalReports1 " & DDMonth.SelectedValue & "," & DDYear.SelectedValue & ",'" & ddlPO.SelectedValue & "'", "PoRpt")
        Dim intCount As Integer = 0, TotalSite As Integer = 0, TotalIntegration As Integer = 0, TotalOnAir As Integer = 0, TotalBast As Integer = 0, TotalIntegrationAmount As Decimal = 0, TotalOnAirAmount As Decimal = 0, TotalBastAmount As Decimal = 0

        Dim strAreaName As String = ""
        For intCount = 0 To dtStatus.Rows.Count - 1

            Dim iMainRows As New HtmlTableRow

            Dim iMianCell1 As New HtmlTableCell
            Dim iMianCell2 As New HtmlTableCell
            'Dim iMianCell3 As New HtmlTableCell
            'Dim iMianCell4 As New HtmlTableCell
            Dim iMianCell5 As New HtmlTableCell
            Dim iMianCell6 As New HtmlTableCell
            'Dim iMianCell7 As New HtmlTableCell
            'Dim iMianCell8 As New HtmlTableCell

            iMianCell1.Attributes.Add("class", "lblReportRows")
            iMianCell2.Attributes.Add("class", "lblReportRows")
            'iMianCell3.Attributes.Add("class", "lblReportRows")
            'iMianCell4.Attributes.Add("class", "lblReportRowsRight")
            iMianCell5.Attributes.Add("class", "lblReportRows")
            iMianCell6.Attributes.Add("class", "lblReportRowsRight")
            'iMianCell7.Attributes.Add("class", "lblReportRows")
            'iMianCell8.Attributes.Add("class", "lblReportRowsRight")

            iMianCell1.InnerHtml = dtStatus.Rows(intCount)("pono")
            iMianCell2.InnerHtml = dtStatus.Rows(intCount)("fldtype")

            'TotalIntegration = TotalIntegration + dtStatus.Rows(intCount)("TotalIntegration")
            'iMianCell3.InnerHtml = dtStatus.Rows(intCount)("TotalIntegration")

            'TotalIntegrationAmount = TotalIntegrationAmount + dtStatus.Rows(intCount)("TotalIntegrationAmount")
            'iMianCell4.InnerHtml = dtStatus.Rows(intCount)("TotalIntegrationAmount")

            TotalOnAir = TotalOnAir + dtStatus.Rows(intCount)("TotalOnAir")
            iMianCell5.InnerHtml = dtStatus.Rows(intCount)("TotalOnAir")

            TotalOnAirAmount = TotalOnAirAmount + dtStatus.Rows(intCount)("TotalOnAirAmount")
            iMianCell6.InnerHtml = "<img src='http://www.telkomsel.nsnebast.com/Images/euro.gif'/>" & dtStatus.Rows(intCount)("TotalOnAirAmount")

            'TotalBast = TotalBast + dtStatus.Rows(intCount)("TotalBast")
            'iMianCell7.InnerHtml = dtStatus.Rows(intCount)("TotalBast")

            'TotalBastAmount = TotalBastAmount + dtStatus.Rows(intCount)("TotalBastAmount")
            'iMianCell8.InnerHtml = dtStatus.Rows(intCount)("TotalBastAmount")


            iMainRows.Cells.Add(iMianCell1)
            iMainRows.Cells.Add(iMianCell2)
            'iMainRows.Cells.Add(iMianCell3)
            'iMainRows.Cells.Add(iMianCell4)
            iMainRows.Cells.Add(iMianCell5)
            iMainRows.Cells.Add(iMianCell6)
            'iMainRows.Cells.Add(iMianCell7)
            'iMainRows.Cells.Add(iMianCell8)
            iMainTable.Rows.Add(iMainRows)
        Next

        Dim iMaingap As New HtmlTableRow
        Dim iMainCellgap As New HtmlTableCell
        iMainCellgap.ColSpan = 8
        iMainCellgap.Attributes.Add("class", "hgap")
        iMaingap.Cells.Add(iMainCellgap)
        iMainTable.Rows.Add(iMaingap)

        Dim iMainRows3 As New HtmlTableRow
        iMainRows3.Style.Add("align", "center")

        Dim iMianCell31 As New HtmlTableCell
        Dim iMianCell32 As New HtmlTableCell
        'Dim iMianCell33 As New HtmlTableCell
        'Dim iMianCell34 As New HtmlTableCell
        Dim iMianCell35 As New HtmlTableCell
        Dim iMianCell36 As New HtmlTableCell
        'Dim iMianCell37 As New HtmlTableCell
        'Dim iMianCell38 As New HtmlTableCell
        iMianCell31.Attributes.Add("class", "lblReportRows")
        iMianCell32.Attributes.Add("class", "lblReportRows")
        'iMianCell33.Attributes.Add("class", "lblReportRows")
        'iMianCell34.Attributes.Add("class", "lblReportRowsRight")
        iMianCell35.Attributes.Add("class", "lblReportRows")
        iMianCell36.Attributes.Add("class", "lblReportRowsRight")
        'iMianCell37.Attributes.Add("class", "lblReportRows")
        'iMianCell38.Attributes.Add("class", "lblReportRowsRight")

        iMianCell31.InnerHtml = "Total"
        iMianCell32.InnerHtml = "&nbsp;"
        'iMianCell33.InnerHtml = TotalIntegration
        'iMianCell34.Style.Add("align", "right")
        'iMianCell34.InnerHtml = TotalIntegrationAmount.ToString("0.00")
        iMianCell35.InnerHtml = TotalOnAir
        iMianCell36.Style.Add("align", "right")
        iMianCell36.InnerHtml = "<img src='http://www.telkomsel.nsnebast.com/Images/euro.gif'/>" & TotalOnAirAmount.ToString("0.00")
        'iMianCell37.InnerHtml = TotalBast
        'iMianCell38.Style.Add("align", "right")
        'iMianCell38.InnerHtml = TotalBastAmount.ToString("0.00")

        iMainRows3.Cells.Add(iMianCell31)
        iMainRows3.Cells.Add(iMianCell32)
        'iMainRows3.Cells.Add(iMianCell33)
        'iMainRows3.Cells.Add(iMianCell34)
        iMainRows3.Cells.Add(iMianCell35)
        iMainRows3.Cells.Add(iMianCell36)
        'iMainRows3.Cells.Add(iMianCell37)
        'iMainRows3.Cells.Add(iMianCell38)
        iMainTable.Rows.Add(iMainRows3)

        tdTitle.Controls.Add(iMainTable)
        tdTitle.Style.Add("display", " ")
    End Sub

    Protected Sub BtnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSubmit.Click
        FinanicalReport()
        displayGraph()
    End Sub

    Protected Sub ddlPO_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Response.redirect("http://telkomsel.www.telkomsel.nsnebast.com" & _
            Request.ServerVariables("URL") & "?month=" & DDMonth.SelectedValue & _
                "&year=" & DDYear.SelectedValue & "&pono=" & ddlPO.SelectedValue)
    End Sub

    Sub displayGraph()
        Dim con As New SqlConnection(ConfigurationManager.AppSettings("conn"))
        Dim Conninfo As New ConnectionInfo
        Dim cmd As New SqlCommand("uspRPTPO", con)

        cmd.CommandText = "Exec GetFinanicalReports1 " & DDMonth.SelectedValue & "," & DDYear.SelectedValue & ",'" & ddlPO.SelectedValue & "'"

        Dim da As New SqlDataAdapter(cmd)
        Dim ds As New ManagementPOReport

        da.Fill(ds, "FinancialReports")

        Conninfo.ServerName = ConfigurationManager.AppSettings("SName")
        Conninfo.DatabaseName = ConfigurationManager.AppSettings("RDB")
        Conninfo.UserID = ConfigurationManager.AppSettings("RUsrName")
        Conninfo.Password = ConfigurationManager.AppSettings("RUsrPwd")

        rpt = New ReportDocument
        rpt.Load(Server.MapPath("FinancialReports.rpt"))
        SetDBLogonForReport(Conninfo, rpt)
        rpt.SetDataSource(ds)
        Session("FinancialReports") = rpt

        CrystalReportViewer1.ReportSource = rpt

        Dim prmFld1 As ParameterField = CrystalReportViewer1.ParameterFieldInfo(0)
        Dim prmVal1 As ParameterDiscreteValue = New ParameterDiscreteValue()
        prmVal1.Value = DDMonth.SelectedValue.ToString
        prmFld1.CurrentValues.Add(prmVal1)

        Dim prmFld2 As ParameterField = CrystalReportViewer1.ParameterFieldInfo(1)
        Dim prmVal2 As ParameterDiscreteValue = New ParameterDiscreteValue()
        prmVal2.Value = DDYear.SelectedValue.ToString
        prmFld2.CurrentValues.Add(prmVal2)

        CrystalReportViewer1.DataBind()

        If Not ddlPO.SelectedValue Is Nothing And Not ddlPO.SelectedValue = "0" Then
            CrystalReportViewer1.Visible = True
        Else
            CrystalReportViewer1.Visible = False
        End If
    End Sub

    Public Sub SetDBLogonForReport(ByVal ConnectionInfo As ConnectionInfo, ByVal reportDocument As ReportDocument)
        Dim rpttables As Tables = reportDocument.Database.Tables
        Dim tab As CrystalDecisions.CrystalReports.Engine.Table
        For Each tab In rpttables
            Dim rpttableslogoninfo As TableLogOnInfo = tab.LogOnInfo
            tab.LogOnInfo.ConnectionInfo = ConnectionInfo
            tab.ApplyLogOnInfo(tab.LogOnInfo)
        Next
    End Sub
End Class
