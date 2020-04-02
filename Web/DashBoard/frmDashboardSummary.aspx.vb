Imports System.Data
Imports System.Text
Imports FusionCharts.Charts
Imports ClosedXML.Excel
Imports System.IO

Partial Class DashBoard_frmDashboardSummary
    Inherits System.Web.UI.Page
    Dim generalcontrol As New GeneralController
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            BindListPO(DdlPOList)
            BindRegions(DdlRegions)
            BindReportTabel(Integer.Parse(DdlRegions.SelectedValue), DdlPOList.SelectedValue)
            BindDocProgress(Integer.Parse(DdlRegions.SelectedValue), DdlPOList.SelectedValue)
        End If
    End Sub

    Protected Sub DdlPOList_SelectedIndexChanged(ByVal ssender As Object, ByVal e As EventArgs) Handles DdlPOList.SelectedIndexChanged
        BindDocProgress(Integer.Parse(DdlRegions.SelectedValue), DdlPOList.SelectedValue)
    End Sub

    Protected Sub DdlRegions_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles DdlRegions.SelectedIndexChanged
        BindDocProgress(Integer.Parse(DdlRegions.SelectedValue), DdlPOList.SelectedValue)
    End Sub

    Protected Sub lbtDownloadTotalSites_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lbtDownloadTotalSites.Click
        Dim dtResult As DataTable = generalcontrol.RPT_EStorageProgressSummaryGetReport(Integer.Parse(DdlRegions.SelectedValue), DdlPOList.SelectedValue, "totalsites")
        RptExportToExcel(dtResult, "Sheet1")
    End Sub

    Protected Sub lbtDownloadCompleted_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lbtDownloadCompleted.Click
        Dim dtResult As DataTable = generalcontrol.RPT_EStorageProgressSummaryGetReport(Integer.Parse(DdlRegions.SelectedValue), DdlPOList.SelectedValue, "hasdone")
        RptExportToExcel(dtResult, "Sheet1")
    End Sub

    Protected Sub lbtDownloadInProgress_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lbtDownloadInProgress.Click
        Dim dtResult As DataTable = generalcontrol.RPT_EStorageProgressSummaryGetReport(Integer.Parse(DdlRegions.SelectedValue), DdlPOList.SelectedValue, "inprogress")
        RptExportToExcel(dtResult, "Sheet1")
    End Sub

    Protected Sub lbtDownloadPending_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lbtDownloadPending.Click
        Dim dtResult As DataTable = generalcontrol.RPT_EStorageProgressSummaryGetReport(Integer.Parse(DdlRegions.SelectedValue), DdlPOList.SelectedValue, "pending")
        RptExportToExcel(dtResult, "Sheet1")
    End Sub

#Region "Custom Methods"
    Private Sub BindDocProgress(ByVal rgnid As Integer, ByVal pono As String)
        Dim jsonData As [String]
        Dim jsonSettingMap As String = String.Empty
        Dim jsonSettingValue As String = String.Empty
        Dim jsonGetData As String = String.Empty
        'jsonData = "{'chart': {'caption': 'Indonesia Population',        'subcaption': 'Indonesia Merdeka',        'formatNumberScale': '0',        'NumberSuffix': 'M', 'showLabels': '1',   'theme': 'fint' },      'colorrange': {  'color': [{   'minvalue': '0', 'maxvalue':'100','displayvalue': '<100M', 'code': '#D0DFA3' }, {'minvalue': '101', 'maxvalue':'100000000000','displayvalue': '>100M', 'code': '#6baa01'}]}, 'data': [{'id': '01', 'value': '3189000000' }]  }"

        jsonSettingMap = "'chart': {'caption': 'E-Storage Document Progress',        'subcaption': 'CAC Completeness', 'showBorder':'0', 'showShadow' : '6','startingAngle': '310','showLabels': '0','showPercentValues': '1','showLegend': '1','legendShadow': '0','legendBorderAlpha': '0', 'decimals': '0', 'captionFontSize': '14', 'subcaptionFontSize': '14', 'subcaptionFontBold': '0', 'toolTipColor': '#ffffff','toolTipBorderThickness': '0','toolTipBgColor': '#000000', 'toolTipBgAlpha': '80','toolTipBorderRadius': '2', 'toolTipPadding': '5', 'theme':'fint', },"
        'jsonSettingValue = "'colorrange': {  'color': [{   'minvalue': '0', 'maxvalue':'50','displayvalue': '<51%', 'code': '#ff0000' }, {'minvalue': '51', 'maxvalue':'101','displayvalue': '>51%', 'code': '#6baa01'}]},"
        Dim getresults As DataTable = generalcontrol.RPT_EStorageProgressSummary(rgnid, pono)
        Dim count As Integer = 0
        If getresults.Rows.Count > 0 Then

            jsonGetData += "'data': [ "
            For Each drw As DataRow In getresults.Rows

                jsonGetData += "{'label': '" & "CAC Completed" & "', 'value': '" & drw.Item("dochasdone") & "'},"
                jsonGetData += "{'label': '" & "CAC Pending" & "', 'value': '" & drw.Item("notstartedyet") & "'},"
                jsonGetData += "{'label': '" & "CAC In-Progress" & "', 'value': '" & drw.Item("docinprogress") & "', 'issliced': '1'}"
                lblCompleted.Text = drw.Item("dochasdone").ToString()
                lblInProgress.Text = drw.Item("docinprogress").ToString()
                lblPending.Text = drw.Item("notstartedyet").ToString()
                lblTotalSites.Text = drw.Item("TotalSites").ToString()
                count += 1
            Next
            jsonGetData += " ]"
        End If
        jsonData = "{ " & jsonSettingMap & jsonSettingValue & jsonGetData & " }"
        ' Initialize chart
        Dim chart As New Chart("doughnut3d", "surveyProgress", "650", "450", "json", jsonData)
        ltrGraphProgress.Text = chart.Render()
    End Sub

    'Private Sub BindReport(ByVal rgnid As Integer, ByVal pono As String)
    '    Dim dtResults As DataTable = generalcontrol.RPT_EStorageProgressSummary(rgnid, pono)
    '    If dtResults.Rows.Count > 0 Then
    '        Dim xmlData As New StringBuilder()
    '        Dim xmlCategories As String = String.Empty
    '        Dim xmlDataSetDocInProgress As String = String.Empty
    '        Dim xmlDataSetDocHasDone As String = String.Empty
    '        Dim xmlDataSetDocNotStartedYet As String = String.Empty
    '        xmlData.Append("<chart palettecolors='#ed1c24' Caption='E-Storage Document Uploade Progress' subCaption='H3I Project' numberprefix='$' plotgradientcolor='' bgcolor='FFFFFF' aligncaptionwithcanvas='0' divlinecolor='CCCCCC' showvalues='0' captionpadding='10' legendshadow='0' legendborderalpha='0' plottooltext='<div><b>$label</b><br/>Cust.PONO : <b>$seriesname</b><br/>TotalDoc : <b>$$value</b></div>' theme='zune' >")
    '        xmlCategories += "<categories>"
    '        For Each drw As DataRow In dtResults.Rows
    '            xmlCategories += "<category label='" & drw("hotasperpo").ToString() & "' />"
    '            xmlDataSetDocInProgress += "<set value='" & drw("docinprogress").ToString() & "'/>"
    '            xmlDataSetDocHasDone += "<set value='" & drw("dochasdone").ToString() & "'/>"
    '            xmlDataSetDocNotStartedYet += "<set value='" & drw("notstartedyet").ToString() & "'/>"
    '        Next
    '        xmlCategories += "</categories>"
    '        Dim grpXmlDataSetDocNotStartedYet As String = "<dataset seriesname='Not Started Yet' color='ed1c24'>" & xmlDataSetDocNotStartedYet & "</dataset>"
    '        Dim grpXmlDataSetDocInProgress As String = "<dataset seriesname='In-Progress' color='008ee4'>" & xmlDataSetDocInProgress & "</dataset>"
    '        Dim grpXmlDataSetDocHasDone As String = "<dataset seriesname='CAC Completed' color='f8bd19'>" & xmlDataSetDocHasDone & "</dataset>"
    '        xmlData.Append(xmlCategories)
    '        xmlData.Append(grpXmlDataSetDocNotStartedYet)
    '        xmlData.Append(grpXmlDataSetDocInProgress)
    '        'xmlData.Append(grpXmlDataSetDocHasDone)
    '        xmlData.AppendFormat("</chart>")
    '        Dim factoryOutput As New Chart("msbar2d", "EstorageProgressSummary", "100%", "350", "xml", xmlData.ToString())
    '        'ltrGraphProgress.Controls.Clear()
    '        'ltrGraphProgress.Text = factoryOutput.Render()
    '    End If
    'End Sub

    Private Sub BindListPO(ByVal ddl As DropDownList)
        ddl.DataSource = generalcontrol.GetAllHOTasPO()
        ddl.DataTextField = "hotasperpo"
        ddl.DataValueField = "hotasperpo"
        ddl.DataBind()
        ddl.Items.Insert(0, New ListItem("--ALL--", String.Empty))
    End Sub

    Private Sub BindRegions(ByVal ddl As DropDownList)
        ddl.DataSource = generalcontrol.GetAllRegions()
        ddl.DataTextField = "RGNName"
        ddl.DataValueField = "RGN_ID"
        ddl.DataBind()
        ddl.Items.Insert(0, New ListItem("--ALL--", "0"))
    End Sub
    Private Sub BindReportTabel(ByVal rgnid As Integer, ByVal pono As String)
        Dim ds As DataTable = generalcontrol.EStorageProgressSummary(rgnid, pono)
        GvReport.DataSource = ds
        GvReport.DataBind()
    End Sub

    Private Function RptExportToExcel(ByVal dtresults As DataTable, ByVal sheetname As String) As String
        Dim isSucceed As String = "success"
        Try
            Dim strFilename As String = GetReportName() & "_" & String.Format("{0:ddMMMyyyy}", Date.Now)
            dtresults.TableName = sheetname
            Using wb As New XLWorkbook()
                wb.Worksheets.Add(dtresults)

                Response.Clear()
                Response.Buffer = True
                Response.Charset = ""
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                Response.AddHeader("content-disposition", "attachment;filename=" & strFilename & ".xlsx")
                Using MyMemoryStream As New MemoryStream()
                    wb.SaveAs(MyMemoryStream)
                    MyMemoryStream.WriteTo(Response.OutputStream)
                    Response.Flush()
                    Response.[End]()
                End Using
            End Using
        Catch ex As Exception
            isSucceed = "error : " & ex.Message.ToString()
        End Try


        Return isSucceed
    End Function



    Private Function GetReportName() As String
        Dim strRptName As String = String.Empty
        strRptName += "EStorage_Progress_"
        If DdlPOList.SelectedIndex = 0 Then
            strRptName += "AllPO_"
        Else
            strRptName += DdlPOList.SelectedItem.Text.Replace("/", "_").Replace(" ", "_") & "_"
        End If

        If DdlRegions.SelectedIndex = 0 Then
            strRptName += "AllRegions" & "_"
        Else
            strRptName += DdlRegions.SelectedItem.Text.Replace(" ", "_") & "_"
        End If

        Return strRptName
    End Function
#End Region


End Class
