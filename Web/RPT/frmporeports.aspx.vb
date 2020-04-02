Imports System
Imports System.Data
Imports BusinessLogic
Imports Common
Partial Class RPT_frmporeports
    Inherits System.Web.UI.Page
    Dim objUtil As New DBUtil

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Page_size") Is Nothing Then Response.Redirect("~/SessionTimeout.aspx")
        If Not Page.IsPostBack Then
            PoReportArea()
        End If

    End Sub
    Sub PoReportArea()        
        Dim iMainTable As New HtmlTable
        iMainTable.Width = "100%"
        'iMainTable.Height = "100%"
        iMainTable.Border = 1
        iMainTable.Align = "left"
        iMainTable.BorderColor = "Orange"
        iMainTable.CellPadding = 0
        iMainTable.CellSpacing = 0

        Dim iMainRows1 As New HtmlTableRow    
        Dim iMianCell10 As New HtmlTableCell
        Dim iMianCell9 As New HtmlTableCell
        'iMianCell10.ColSpan = 1
        Dim iMianCell11 As New HtmlTableCell
        Dim iMianCell12 As New HtmlTableCell
        Dim iMianCell13 As New HtmlTableCell
        Dim iMianCell14 As New HtmlTableCell
        Dim iMianCell15 As New HtmlTableCell
        iMianCell10.Attributes.Add("class", "lblReportHeader")
        iMianCell10.InnerHtml = "Area Name"
        iMianCell9.Attributes.Add("class", "lblReportHeader")
        iMianCell9.InnerHtml = "Customer PO"
        iMianCell11.Attributes.Add("class", "lblReportHeader")
        iMianCell11.InnerHtml = "Total Sites"
        'iMianCell12.InnerHtml = "LUST"
        iMianCell13.Attributes.Add("class", "lblReportHeader")
        iMianCell13.InnerHtml = "BAUT"
        iMianCell14.Attributes.Add("class", "lblReportHeader")
        iMianCell14.InnerHtml = "BAST"
        iMianCell15.Attributes.Add("class", "lblReportHeader")
        iMianCell15.InnerHtml = "%"
        iMainRows1.Cells.Add(iMianCell10)
        iMainRows1.Cells.Add(iMianCell9)
        iMainRows1.Cells.Add(iMianCell11)
        'iMainRows1.Cells.Add(iMianCell12)
        iMainRows1.Cells.Add(iMianCell13)
        iMainRows1.Cells.Add(iMianCell14)
        iMainRows1.Cells.Add(iMianCell15)
        iMainTable.Rows.Add(iMainRows1)

        Dim dtStatus As New DataTable
        dtStatus = objUtil.ExeQueryDT("exec [uspPOReportArea]", "codarea")
        Dim intCount As Integer = 0, TotalSite As Integer = 0, total As Integer = 0, TotalBaut As Integer = 0, TotalBast As Integer = 0

        Dim strAreaName As String = ""
        For intCount = 0 To dtStatus.Rows.Count - 1
           
            Dim iMainRows As New HtmlTableRow
            Dim iMianCell00 As New HtmlTableCell
            Dim iMianCell0 As New HtmlTableCell
            Dim iMianCell1 As New HtmlTableCell
            'Dim iMianCell2 As New HtmlTableCell
            Dim iMianCell3 As New HtmlTableCell
            Dim iMianCell4 As New HtmlTableCell
            Dim iMianCell5 As New HtmlTableCell
            Dim strArea As String
            If (strAreaName = dtStatus.Rows(intCount)("ara_name").ToString) Then
                strArea = ""                

            Else
                strArea = "<a href=""#"" onclick=""DisplayArea(" + intCount.ToString + ")""> "
                strArea = strArea + dtStatus.Rows(intCount)("ara_name").ToString + "</a>"
                strAreaName = dtStatus.Rows(intCount)("ara_name").ToString


            End If
            iMianCell0.InnerHtml = strArea
            

            iMianCell00.Attributes.Add("class", "lblReportRows")
            iMianCell0.Attributes.Add("class", "lblReportRows")
            iMianCell1.Attributes.Add("class", "lblReportRows")
            iMianCell3.Attributes.Add("class", "lblReportRows")
            iMianCell4.Attributes.Add("class", "lblReportRows")
            iMianCell5.Attributes.Add("class", "lblReportRows")

            iMianCell00.Width = "20%"
            iMianCell0.Width = "20%"
            iMianCell1.Width = "20%"
            'iMianCell2.Width = "3%"
            iMianCell3.Width = "20%"
            iMianCell4.Width = "20%"
            iMianCell5.Width = "20%"

            'iMianCell0.Attributes.Add("class", "dashboard")
            iMianCell00.InnerHtml = dtStatus.Rows(intCount)("PoNo")
            TotalSite = TotalSite + dtStatus.Rows(intCount)("TotalSite")
            total = total + TotalSite
            iMianCell1.InnerHtml = dtStatus.Rows(intCount)("TotalSite")
            'iMianCell2.InnerHtml = dtStatus.Rows(intCount)("lust")
            TotalBast = TotalBast + dtStatus.Rows(intCount)("BAUT")
            iMianCell3.InnerHtml = dtStatus.Rows(intCount)("BAUT")
            TotalBaut = TotalBaut + dtStatus.Rows(intCount)("BAUT")
            iMianCell4.InnerHtml = dtStatus.Rows(intCount)("BAUT")
            Dim dl As Double = (iMianCell3.InnerHtml / TotalSite) * 100
            iMianCell5.InnerHtml = dl.ToString("0.00") 'dtStatus.Rows(intCount)("Precentage")   

            iMainRows.Cells.Add(iMianCell0)
            iMainRows.Cells.Add(iMianCell00)
            iMainRows.Cells.Add(iMianCell1)
            'iMainRows.Cells.Add(iMianCell2)
            iMainRows.Cells.Add(iMianCell3)
            iMainRows.Cells.Add(iMianCell4)
            iMainRows.Cells.Add(iMianCell5)
            iMainTable.Rows.Add(iMainRows)

            Dim iMainrow2 As New HtmlTableRow
            Dim iMianCell6 As New HtmlTableCell
            iMianCell6.ID = "AreaId" + intCount.ToString
            iMianCell6.Style.Add("display", "none")
            iMianCell6.ColSpan = 6
            iMianCell6.Controls.Add(PoReportRegion(dtStatus.Rows(intCount)("ara_id")))
            iMainrow2.Cells.Add(iMianCell6)
            iMainTable.Rows.Add(iMainrow2)

            TotalSite = 0
        Next
        HDArea.Value = dtStatus.Rows.Count
        'gap
        Dim iMaingap As New HtmlTableRow
        Dim iMainCellgap As New HtmlTableCell
        iMainCellgap.ColSpan = 6
        iMainCellgap.Attributes.Add("class", "hgap")
        iMaingap.Cells.Add(iMainCellgap)
        iMainTable.Rows.Add(iMaingap)


        Dim iMainRows3 As New HtmlTableRow
        iMainRows3.Style.Add("align", "Right")
        Dim iMianCell300 As New HtmlTableCell
        Dim iMianCell30 As New HtmlTableCell        
        Dim iMianCell31 As New HtmlTableCell
        Dim iMianCell32 As New HtmlTableCell
        Dim iMianCell33 As New HtmlTableCell
        Dim iMianCell34 As New HtmlTableCell
        Dim iMianCell35 As New HtmlTableCell
        iMianCell30.Attributes.Add("class", "lblReportRows")
        iMianCell31.Attributes.Add("class", "lblReportRows")
        iMianCell32.Attributes.Add("class", "lblReportRows")
        iMianCell33.Attributes.Add("class", "lblReportRows")
        iMianCell34.Attributes.Add("class", "lblReportRows")
        iMianCell30.InnerHtml = "Totals"

        iMianCell31.InnerHtml = total
        iMianCell32.InnerHtml = TotalBaut
        iMianCell33.InnerHtml = TotalBast
        Dim dll As Double = TotalBast / total * 100
        iMianCell34.InnerHtml = dll.ToString("0.00")
        iMianCell35.InnerHtml = ""
        iMainRows3.Cells.Add(iMianCell300)
        iMainRows3.Cells.Add(iMianCell30)
        iMainRows3.Cells.Add(iMianCell31)
        iMainRows3.Cells.Add(iMianCell32)
        iMainRows3.Cells.Add(iMianCell33)
        iMainRows3.Cells.Add(iMianCell34)
        iMainRows3.Cells.Add(iMianCell35)
        iMainTable.Rows.Add(iMainRows3)

        bindArea.Controls.Add(iMainTable)
        'gap
        'Dim iMaingap1 As New HtmlTableRow
        'Dim iMainCellgap1 As New HtmlTableCell
        'iMainCellgap1.ColSpan = 6
        'iMainCellgap1.Attributes.Add("class", "hgap")
        'iMaingap.Cells.Add(iMainCellgap1)
        'iMainTable.Rows.Add(iMaingap1)
    End Sub
    Function PoReportRegion(ByVal araid As Integer) As HtmlTable

        Dim iMainTable As New HtmlTable
        iMainTable.Align = "Right"
        iMainTable.Width = "100%"
        iMainTable.Border = 1
        iMainTable.BorderColor = "Orange"
        iMainTable.CellPadding = 4
        iMainTable.CellSpacing = 0
        Dim iMainRows1 As New HtmlTableRow
        Dim iMianCell10 As New HtmlTableCell
        Dim imiancell00 As New HtmlTableCell
        Dim iMianCell11 As New HtmlTableCell
        'Dim iMianCell12 As New HtmlTableCell
        Dim iMianCell13 As New HtmlTableCell
        Dim iMianCell14 As New HtmlTableCell
        Dim iMianCell15 As New HtmlTableCell
        iMianCell10.Attributes.Add("class", "lblReport")
        imiancell00.Attributes.Add("class", "lblReport")
        iMianCell11.Attributes.Add("class", "lblReport")
        iMianCell13.Attributes.Add("class", "lblReport")
        iMianCell14.Attributes.Add("class", "lblReport")
        iMianCell15.Attributes.Add("class", "lblReport")
        iMianCell10.InnerHtml = "Region Name"
        imiancell00.InnerHtml = "Customer PO"
        iMianCell11.InnerHtml = "Total Sites"
        'iMianCell12.InnerHtml = "LUST"
        iMianCell13.InnerHtml = "BAUT"
        iMianCell14.InnerHtml = "BAST"
        iMianCell15.InnerHtml = " % "
        iMainRows1.Cells.Add(iMianCell10)
        iMainRows1.Cells.Add(imiancell00)
        iMainRows1.Cells.Add(iMianCell11)
        'iMainRows1.Cells.Add(iMianCell12)
        iMainRows1.Cells.Add(iMianCell13)
        iMainRows1.Cells.Add(iMianCell14)
        iMainRows1.Cells.Add(iMianCell15)
        iMainTable.Rows.Add(iMainRows1)

        Dim dtStatus As New DataTable
        dtStatus = objUtil.ExeQueryDT("exec [uspPOReportRegion] " & araid, "codregion")
        Dim intCount As Integer = 0
        Dim pono As String = ""
        For intCount = 0 To dtStatus.Rows.Count - 1
            Dim iMainRows As New HtmlTableRow
            Dim iMianCell0 As New HtmlTableCell
            Dim iMiancell000 As New HtmlTableCell
            Dim iMianCell1 As New HtmlTableCell
            'Dim iMianCell2 As New HtmlTableCell
            Dim iMianCell3 As New HtmlTableCell
            Dim iMianCell4 As New HtmlTableCell
            Dim iMianCell5 As New HtmlTableCell
            iMianCell0.Width = "21%"
            iMiancell000.Width = "21%"
            iMianCell1.Width = "21%"
            'iMianCell2.Width = "3%"
            iMianCell3.Width = "21%"
            iMianCell4.Width = "21%"
            iMianCell15.Width = "21%"
            iMianCell0.InnerHtml = dtStatus.Rows(intCount)("rgnname")
           
            iMianCell0.InnerHtml = "<a href=""#"" onclick=""DisplayRegion(" + araid.ToString + "," + intCount.ToString + "," + dtStatus.Rows.Count.ToString + ")""> " + dtStatus.Rows(intCount)("rgnname").ToString + "</a>"
            iMiancell000.InnerHtml = dtStatus.Rows(intCount)("PoNo")
            pono = dtStatus.Rows(intCount)("PoNo")
            iMianCell1.InnerHtml = dtStatus.Rows(intCount)("TotalSite")
            'iMianCell2.InnerHtml = dtStatus.Rows(intCount)("lust")
            iMianCell3.InnerHtml = dtStatus.Rows(intCount)("BAST")
            iMianCell4.InnerHtml = dtStatus.Rows(intCount)("BAUT")
            Dim dl As Double = (iMianCell3.InnerHtml / iMianCell1.InnerHtml) * 100
            iMianCell5.InnerHtml = dl.ToString("0.00") 'dtStatus.Rows(intCount)("Precentage")
            iMainRows.Cells.Add(iMianCell0)
            iMainRows.Cells.Add(iMiancell000)
            iMainRows.Cells.Add(iMianCell1)
            'iMainRows.Cells.Add(iMianCell2)
            iMainRows.Cells.Add(iMianCell3)
            iMainRows.Cells.Add(iMianCell4)
            iMainRows.Cells.Add(iMianCell5)
            iMainTable.Rows.Add(iMainRows)
            Dim iMainrow2 As New HtmlTableRow
            Dim iMianCell6 As New HtmlTableCell
            iMianCell6.ID = "RegionId" + araid.ToString + "-" + intCount.ToString
            iMianCell6.Style.Add("display", "none")
            iMianCell6.ColSpan = 6
            iMianCell6.Style.Add("padding-left", "50px")
            iMianCell6.Controls.Add(PoReportSite(dtStatus.Rows(intCount)("rgn_id"), pono))
            iMainrow2.Cells.Add(iMianCell6)
            iMainTable.Rows.Add(iMainrow2)
        Next
        HDRegion.Value = dtStatus.Rows.Count
        Return iMainTable
        'gap
        'Dim iMaingap1 As New HtmlTableRow
        'Dim iMainCellgap1 As New HtmlTableCell
        'iMainCellgap1.ColSpan = 6
        'iMainCellgap1.Attributes.Add("class", "hgap")
        'iMaingap1.Cells.Add(iMainCellgap1)
        'iMainTable.Rows.Add(iMaingap1)
    End Function
    Function PoReportSite(ByVal rgn_id As Integer, ByVal pno As String) As HtmlTable

        Dim iMainTable As New HtmlTable
        iMainTable.Width = "100%"
        iMainTable.CellPadding = 4
        iMainTable.CellSpacing = 0
        Dim iMainRows1 As New HtmlTableRow
        Dim iMianCell10 As New HtmlTableCell
        Dim iMianCell11 As New HtmlTableCell
        Dim iMianCell12 As New HtmlTableCell
        Dim iMianCell13 As New HtmlTableCell
        Dim iMianCell14 As New HtmlTableCell
        Dim iMianCell15 As New HtmlTableCell
        iMianCell10.InnerHtml = "Site No"

        iMianCell11.InnerHtml = "Scope"
        iMianCell12.InnerHtml = "Integration Date"
        iMianCell13.InnerHtml = "BAUT Date"
        iMianCell14.InnerHtml = "BAST Date"
        iMianCell10.Attributes.Add("class", "lblReport")
        iMianCell11.Attributes.Add("class", "lblReport")
        iMianCell12.Attributes.Add("class", "lblReport")
        iMianCell13.Attributes.Add("class", "lblReport")
        iMianCell14.Attributes.Add("class", "lblReport")
        'iMianCell15.InnerHtml = "%"
        iMainRows1.Cells.Add(iMianCell10)
        iMainRows1.Cells.Add(iMianCell11)
        iMainRows1.Cells.Add(iMianCell12)
        iMainRows1.Cells.Add(iMianCell13)
        iMainRows1.Cells.Add(iMianCell14)
        iMainRows1.Cells.Add(iMianCell15)
        iMainTable.Rows.Add(iMainRows1)

        Dim dtStatus As New DataTable
        Dim strQuery As String = "exec [uspPOReportSite] " & rgn_id & ",'" & pno & "'"
        dtStatus = objUtil.ExeQueryDT(strQuery, "codregion")
        '"exec [uspPOReportSite] " & rgn_id & ",'" & pno & "'"
        Dim intCount As Integer = 0

        For intCount = 0 To dtStatus.Rows.Count - 1
            Dim iMainRows As New HtmlTableRow
            Dim iMianCell0 As New HtmlTableCell
            Dim iMianCell1 As New HtmlTableCell
            Dim iMianCell2 As New HtmlTableCell
            Dim iMianCell3 As New HtmlTableCell
            Dim iMianCell4 As New HtmlTableCell
            Dim iMianCell5 As New HtmlTableCell
            iMianCell0.Width = "20%"
            iMianCell1.Width = "20%"
            iMianCell2.Width = "20%"
            iMianCell3.Width = "20%"
            iMianCell4.Width = "20%"
            'iMianCell15.Width = "20%"
            iMianCell0.InnerHtml = dtStatus.Rows(intCount)("Site_No")

            iMianCell1.InnerHtml = dtStatus.Rows(intCount)("Scope")
            iMianCell2.InnerHtml = dtStatus.Rows(intCount)("LAUT").ToString()


            iMianCell3.InnerHtml = dtStatus.Rows(intCount)("BAUT").ToString()
            iMianCell4.InnerHtml = dtStatus.Rows(intCount)("BAST").ToString()
            'iMianCell5.InnerHtml = 'dtStatus.Rows(intCount)("Precentage")
            iMainRows.Cells.Add(iMianCell0)
            iMainRows.Cells.Add(iMianCell1)
            iMainRows.Cells.Add(iMianCell2)
            iMainRows.Cells.Add(iMianCell3)
            iMainRows.Cells.Add(iMianCell4)
            'iMainRows.Cells.Add(iMianCell5)
            iMainTable.Rows.Add(iMainRows)
        Next
        Return iMainTable
    End Function
End Class
