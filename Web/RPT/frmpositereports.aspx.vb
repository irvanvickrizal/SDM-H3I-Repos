Imports BusinessLogic
Imports Common
Imports System
Imports System.Data
Partial Class RPT_frmpositereports
    Inherits System.Web.UI.Page
    Dim objUtil As New BODashBoard
    Dim objdl As New BODDLs

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            objdl.fillDDL(DDPoDetails, "pono", True, Constants._DDL_Default_Select)
        End If
        tdTitle.Style.Add("display", "none")
    End Sub
    Sub FinanicalReport()
        Dim iMainTable As New HtmlTable
        iMainTable.Width = "100%"
        'iMainTable.Height = "100%"
        iMainTable.Border = 1
        iMainTable.Align = "left"
        iMainTable.BorderColor = "#ffffff"
        iMainTable.CellPadding = 0
        iMainTable.CellSpacing = 0

        Dim iMainRows1 As New HtmlTableRow

        Dim iMianCell11 As New HtmlTableCell
        Dim iMianCell12 As New HtmlTableCell
        Dim iMianCell13 As New HtmlTableCell
        Dim iMianCell14 As New HtmlTableCell
        Dim iMianCell15 As New HtmlTableCell
        Dim iMianCell16 As New HtmlTableCell
        Dim iMianCell17 As New HtmlTableCell
        Dim iMianCell18 As New HtmlTableCell
        Dim iMianCell19 As New HtmlTableCell
        Dim iMianCell110 As New HtmlTableCell
        Dim iMianCell111 As New HtmlTableCell
        Dim iMianCell112 As New HtmlTableCell

        iMianCell11.Attributes.Add("class", "GridHeaderCenter")
        iMianCell11.InnerHtml = "Site No"

        iMianCell12.Attributes.Add("class", "GridHeaderCenter")
        iMianCell12.InnerHtml = "Scope"
        iMianCell13.Attributes.Add("class", "GridHeaderCenter")
        iMianCell13.InnerHtml = "Project Id"
        iMianCell14.Attributes.Add("class", "GridHeaderCenter")
        iMianCell14.InnerHtml = "Band"
        iMianCell15.Attributes.Add("class", "GridHeaderCenter")
        iMianCell15.InnerHtml = "Configuration"
        iMianCell16.Attributes.Add("class", "GridHeaderCenter")
        iMianCell16.InnerHtml = "Antenna&nbsp;Name&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
        iMianCell17.Attributes.Add("class", "GridHeaderCenter")
        iMianCell17.InnerHtml = "Antenna Qty"
        iMianCell18.Attributes.Add("class", "GridHeaderCenter")
        iMianCell18.InnerHtml = "Feeder Length"

        iMianCell19.Attributes.Add("class", "GridHeaderCenter")
        iMianCell19.InnerHtml = "Feeder&nbsp;Type"
        iMianCell110.Attributes.Add("class", "GridHeaderCenter")
        iMianCell110.InnerHtml = "Feeder Qty"
        iMianCell111.Attributes.Add("class", "GridHeaderCenter")
        iMianCell111.InnerHtml = "Value 1 (euro rate)"
        iMianCell112.Attributes.Add("class", "GridHeaderCenter")
        iMianCell112.InnerHtml = "Value 2 (euro rate)"


        iMainRows1.Cells.Add(iMianCell11)
        iMainRows1.Cells.Add(iMianCell12)
        iMainRows1.Cells.Add(iMianCell13)
        iMainRows1.Cells.Add(iMianCell14)
        iMainRows1.Cells.Add(iMianCell15)
        iMainRows1.Cells.Add(iMianCell16)
        iMainRows1.Cells.Add(iMianCell17)
        iMainRows1.Cells.Add(iMianCell18)
        iMainRows1.Cells.Add(iMianCell19)
        iMainRows1.Cells.Add(iMianCell110)
        iMainRows1.Cells.Add(iMianCell111)
        iMainRows1.Cells.Add(iMianCell112)
        iMainTable.Rows.Add(iMainRows1)

        Dim dtStatus As New DataTable
        dtStatus = objUtil.PoSiteReports(DDPoDetails.SelectedValue)
        Dim intCount As Integer = 0, TotalValue1Amount As Decimal = 0, TotalValue2Amount As Decimal = 0

        Dim strAreaName As String = ""
        For intCount = 0 To dtStatus.Rows.Count - 1

            Dim iMainRows As New HtmlTableRow

            Dim iMianCell1 As New HtmlTableCell
            Dim iMianCell2 As New HtmlTableCell
            Dim iMianCell3 As New HtmlTableCell
            Dim iMianCell4 As New HtmlTableCell
            Dim iMianCell5 As New HtmlTableCell
            Dim iMianCell6 As New HtmlTableCell
            Dim iMianCell7 As New HtmlTableCell
            Dim iMianCell8 As New HtmlTableCell
            Dim iMianCell9 As New HtmlTableCell
            Dim iMianCell10 As New HtmlTableCell
            Dim iMianCell011 As New HtmlTableCell
            Dim iMianCell012 As New HtmlTableCell

            iMianCell1.Attributes.Add("class", "lblReportRowsLeft")
            iMianCell2.Attributes.Add("class", "lblReportRowsLeft")
            iMianCell3.Attributes.Add("class", "lblReportRowsLeft")
            iMianCell4.Attributes.Add("class", "lblReportRowsLeft")
            iMianCell5.Attributes.Add("class", "lblReportRowsLeft")
            iMianCell6.Attributes.Add("class", "lblReportRowsLeft")
            iMianCell7.Attributes.Add("class", "lblReportRows")
            iMianCell8.Attributes.Add("class", "lblReportRows")
            iMianCell9.Attributes.Add("class", "lblReportRowsLeft")
            iMianCell10.Attributes.Add("class", "lblReportRows")
            iMianCell011.Attributes.Add("class", "lblReportRowsRight")
            iMianCell012.Attributes.Add("class", "lblReportRowsRight")


            iMianCell1.InnerHtml = dtStatus.Rows(intCount)("siteno")
            iMianCell2.InnerHtml = dtStatus.Rows(intCount)("fldtype")
            iMianCell3.InnerHtml = dtStatus.Rows(intCount)("WorkPkgid")
            If dtStatus.Rows(intCount)("band").ToString.Length > 4 Then
                iMianCell4.InnerHtml = dtStatus.Rows(intCount)("band").ToString
            Else
                iMianCell4.InnerHtml = "&nbsp;"
            End If
            If dtStatus.Rows(intCount)("config").ToString.Length > 4 Then
                iMianCell5.InnerHtml = dtStatus.Rows(intCount)("config").ToString
            Else
                iMianCell5.InnerHtml = "&nbsp;"
            End If
            iMianCell6.Width = "150px"
            If dtStatus.Rows(intCount)("AntennaName").ToString.Length > 4 Then
                iMianCell6.InnerHtml = dtStatus.Rows(intCount)("AntennaName").ToString
            Else
                iMianCell6.InnerHtml = "&nbsp;"
            End If


            iMianCell7.InnerHtml = dtStatus.Rows(intCount)("Antennaqty")
            iMianCell8.InnerHtml = dtStatus.Rows(intCount)("FeederLen")
            If dtStatus.Rows(intCount)("FeederType").ToString.Length > 4 Then
                iMianCell9.InnerHtml = dtStatus.Rows(intCount)("FeederType").ToString
            Else
                iMianCell9.InnerHtml = "&nbsp;"
            End If

            iMianCell10.InnerHtml = dtStatus.Rows(intCount)("FeederQty")
            iMianCell011.InnerHtml = dtStatus.Rows(intCount)("value1")
            iMianCell012.InnerHtml = dtStatus.Rows(intCount)("value2")
            TotalValue2Amount = TotalValue2Amount + dtStatus.Rows(intCount)("value1")

            TotalValue1Amount = TotalValue1Amount + dtStatus.Rows(intCount)("value2")

            iMainRows.Cells.Add(iMianCell1)
            iMainRows.Cells.Add(iMianCell2)
            iMainRows.Cells.Add(iMianCell3)
            iMainRows.Cells.Add(iMianCell4)
            iMainRows.Cells.Add(iMianCell5)
            iMainRows.Cells.Add(iMianCell6)
            iMainRows.Cells.Add(iMianCell7)
            iMainRows.Cells.Add(iMianCell8)
            iMainRows.Cells.Add(iMianCell9)
            iMainRows.Cells.Add(iMianCell10)
            iMainRows.Cells.Add(iMianCell011)
            iMainRows.Cells.Add(iMianCell012)
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
        Dim iMianCell33 As New HtmlTableCell
        Dim iMianCell34 As New HtmlTableCell
        Dim iMianCell35 As New HtmlTableCell
        Dim iMianCell36 As New HtmlTableCell
        Dim iMianCell37 As New HtmlTableCell
        Dim iMianCell38 As New HtmlTableCell
        Dim iMianCell39 As New HtmlTableCell
        Dim iMianCell310 As New HtmlTableCell
        Dim iMianCell311 As New HtmlTableCell
        Dim iMianCell312 As New HtmlTableCell
        iMianCell31.Attributes.Add("class", "lblReportRows")
        iMianCell32.Attributes.Add("class", "lblReportRows")
        iMianCell33.Attributes.Add("class", "lblReportRows")
        iMianCell34.Attributes.Add("class", "lblReportRowsRight")
        iMianCell35.Attributes.Add("class", "lblReportRows")
        iMianCell36.Attributes.Add("class", "lblReportRowsRight")
        iMianCell37.Attributes.Add("class", "lblReportRows")
        iMianCell38.Attributes.Add("class", "lblReportRowsRight")

        iMianCell39.Attributes.Add("class", "lblReportRows")
        iMianCell310.Attributes.Add("class", "lblReportRowsRight")
        iMianCell311.Attributes.Add("class", "lblReportRowsRight")
        iMianCell312.Attributes.Add("class", "lblReportRowsRight")


        iMianCell31.InnerHtml = "Totals"
        iMianCell32.InnerHtml = "&nbsp;"
        iMianCell33.InnerHtml = "&nbsp;"
        iMianCell34.Style.Add("align", "right")
        iMianCell34.InnerHtml = "&nbsp;"
        iMianCell35.InnerHtml = "&nbsp;"
        iMianCell36.Style.Add("align", "right")
        iMianCell36.InnerHtml = "&nbsp;"
        iMianCell37.InnerHtml = "&nbsp;"
        iMianCell38.Style.Add("align", "right")
        iMianCell38.InnerHtml = "&nbsp;"
        iMianCell39.InnerHtml = "&nbsp;"
        iMianCell310.InnerHtml = "&nbsp;"
        iMianCell311.InnerHtml = TotalValue1Amount.ToString("0.00")
        iMianCell312.InnerHtml = TotalValue2Amount.ToString("0.00")



        iMainRows3.Cells.Add(iMianCell31)
        iMainRows3.Cells.Add(iMianCell32)
        iMainRows3.Cells.Add(iMianCell33)
        iMainRows3.Cells.Add(iMianCell34)
        iMainRows3.Cells.Add(iMianCell35)
        iMainRows3.Cells.Add(iMianCell36)
        iMainRows3.Cells.Add(iMianCell37)
        iMainRows3.Cells.Add(iMianCell38)
        iMainRows3.Cells.Add(iMianCell39)
        iMainRows3.Cells.Add(iMianCell310)
        iMainRows3.Cells.Add(iMianCell311)
        iMainRows3.Cells.Add(iMianCell312)
        iMainTable.Rows.Add(iMainRows3)

        tdTitle.Controls.Add(iMainTable)

    End Sub

    Protected Sub BtnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSubmit.Click
        FinanicalReport()
        tdTitle.Style.Add("display", " ")
    End Sub
End Class
