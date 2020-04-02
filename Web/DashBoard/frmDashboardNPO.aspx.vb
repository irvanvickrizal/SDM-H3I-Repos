Imports System.Data
Imports BusinessLogic
Imports Common
Imports Entities
Imports FusionCharts.Charts

Partial Class DashBoard_frmDashboardAgendaNPO
    Inherits System.Web.UI.Page
    Dim objBO As New BODashBoard
    Dim objUtil As New DBUtil
    Dim generalcontrol As New GeneralController


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        If Not Page.IsPostBack Then
            BindListPO(DdlPONO)
            BindRejectedDocuments(CommonSite.UserId)
            BindTaskPending(CommonSite.UserId)
            BindDocApprovalStatus(CommonSite.UserId)
            BindDocReadyCreation(CommonSite.UserId, ConfigurationManager.AppSettings("SSVDOCID"), lblSSVL0NUCount)
            BindDocReadyCreation(CommonSite.UserId, ConfigurationManager.AppSettings("KPIL0DOCID"), lblKPIL0RCCount)
            BindDocReadyCreation(CommonSite.UserId, ConfigurationManager.AppSettings("SSVL2DOCID"), lblSSVL2NUCount)    'Added by Fauzan, 30 Nov 2018. Bind SSV L2 Document
            BindDocReadyCreation(CommonSite.UserId, ConfigurationManager.AppSettings("KPIL2DOCID"), lblKPIL2RCCount)    'Added by Fauzan, 30 Nov 2018. Bind KPI L2 Document
            BindDocProgress(String.Empty, String.Empty)     'Added by Fauzan, 8 Nov 2018. Initiate First Time Chart
            If Integer.Parse(lblKPIL0RCCount.Text) = 0 Then
                lbtViewDetailKPIL0RC.Enabled = False
            End If
            'Added by Fauzan, 30 Nov 2018. Set enable/disable KPI L2 Details
            If Integer.Parse(lblKPIL2RCCount.Text) = 0 Then
                lbtViewDetailKPIL2RC.Enabled = False
            End If
        End If
    End Sub

    Private Sub lbtViewDetailKPIL2RC_Click(sender As Object, e As EventArgs) Handles lbtViewDetailKPIL2RC.Click
        Response.Redirect("frmKPIL0RC.aspx?id=" & ConfigurationManager.AppSettings("KPIL2DOCID") & "")
    End Sub

    Private Sub lbtViewDetailSSVL2_Click(sender As Object, e As EventArgs) Handles lbtViewDetailSSVL2.Click
        Response.Redirect("frmSSVL0RC.aspx?id=" & ConfigurationManager.AppSettings("SSVL2DOCID") & "")
    End Sub

    Protected Sub lbtViewDetailKPIL0RC_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lbtViewDetailKPIL0RC.Click
        Response.Redirect("frmKPIL0RC.aspx?id=" & ConfigurationManager.AppSettings("KPIL0DOCID") & "")
    End Sub

    Protected Sub lbtViewDetailSSVRC_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lbtViewDetailSSVRC.Click
        'Response.Redirect("frmSSVL0RC.aspx")       'Commented by Fauzan, 30 Nov 2018
        Response.Redirect("frmSSVL0RC.aspx?id=" & ConfigurationManager.AppSettings("SSVDOCID") & "")
    End Sub

    Protected Sub lbtViewRejectedDoc_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lbtViewRejectedDoc.Click
        Response.Redirect("frmRejectedDocNPO.aspx")
    End Sub

    Private Sub DdlPONO_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlPONO.SelectedIndexChanged
        BindDocProgress(DdlPONO.SelectedValue, String.Empty)
    End Sub

#Region "custom methods"
    Private Sub BindDocReadyCreation(ByVal userid As Integer, ByVal docid As Integer, ByVal lblCount As Label)
        lblCount.Text = generalcontrol.Document_ReadyToUploadCount(docid, userid)
    End Sub

    Private Sub BindDocApprovalStatus(ByVal userid As Integer)
        Dim dtResult As DataTable = generalcontrol.DocumentNPO_GetApprovalStatus(userid)
        If dtResult.Rows.Count > 0 Then
            For Each drw As DataRow In dtResult.Rows
                Dim lbllink As String
                
                If drw.Item("doctype") = "kpil0" Then
                    If Integer.Parse(drw.Item("ECount")) > 0 Then
                        lbllink = "<a class='popup1' href='../RPT/frmKPIL0Rpt.aspx?docid=2145&from=ds' title='KPI L0 DONE' style='color:white;'>  " & drw.Item("Ecount") & "  </a>"
                    Else
                        lbllink = drw.Item("Ecount")
                    End If
                    ltrKPIL0DoneCount.Text = lbllink
                ElseIf drw.Item("doctype") = "aprstatus" Then
                    If Integer.Parse(drw.Item("ECount")) > 0 Then
                        lbllink = "<a class='popup' href='../RPT/frmSiteApprovalStatus_NPO.aspx' style='color:white;'> " & drw.Item("Ecount") & "  </a>"
                    Else
                        lbllink = drw.Item("Ecount")
                    End If
                    ltrDASCount.Text = lbllink
                End If
            Next
        End If
    End Sub

    Private Sub BindTaskPending(ByVal userid As Integer)
        Dim dtTask As DataTable = objBO.uspDashBoardAgendaTask(CommonSite.UserId())
        Dim tsites As Integer
        If dtTask.Rows.Count > 0 Then
            tsites = dtTask.Compute("sum(Site_No)", "")
        Else
            tsites = 0
        End If
        If tsites > 0 Then
            ltrTaskPendingCount.Text = " <a href='frmSiteDocCount.aspx?id=" & CommonSite.UserId() & "'> " & tsites & "</a>"
        Else
            ltrTaskPendingCount.Text = tsites
        End If
    End Sub

    Private Sub BindRejectedDocuments(ByVal userid As Integer)
        Dim dtAgenda As New DataTable
        Dim getCount As Integer = 0
        Dim strsql As String = "exec [HCPT_uspDashBoardAgenda_NPO_RejectedDoc] " & CommonSite.UserId().ToString
        dtAgenda = objUtil.ExeQueryDT(strsql, "uspDashBoardAgenda")

        If dtAgenda.Rows.Count > 0 Then
            For Each drw As DataRow In dtAgenda.Rows
                getCount = Integer.Parse(drw.Item("CountUsrType"))
				lblRejectedDocCount.Text = getCount
            Next
        End If
        If getCount > 0 Then
            lbtViewRejectedDoc.Enabled = True
        Else
            lbtViewRejectedDoc.Enabled = False
        End If
    End Sub

    Private Sub BindListPO(ByVal ddl As DropDownList)
        ddl.DataSource = generalcontrol.GetAllHOTasPO()
        ddl.DataTextField = "hotasperpo"
        ddl.DataValueField = "hotasperpo"
        ddl.DataBind()
        ddl.Items.Insert(0, New ListItem("--ALL--", String.Empty))
    End Sub

    ''' <summary>
    ''' Added by Fauzan, 8 Nov 2018. Bind Data for Pie Chart
    ''' </summary>
    ''' <param name="pono">PO Number</param>
    ''' <param name="siteid">Site ID</param>
    Private Sub BindDocProgress(ByVal pono As String, ByVal siteid As String)
        Dim jsonData As [String]
        Dim jsonSettingMap As String = String.Empty
        Dim jsonSettingValue As String = String.Empty
        Dim jsonGetData As String = String.Empty
        'jsonData = "{'chart': {'caption': 'Indonesia Population',        'subcaption': 'Indonesia Merdeka',        'formatNumberScale': '0',        'NumberSuffix': 'M', 'showLabels': '1',   'theme': 'fint' },      'colorrange': {  'color': [{   'minvalue': '0', 'maxvalue':'100','displayvalue': '<100M', 'code': '#D0DFA3' }, {'minvalue': '101', 'maxvalue':'100000000000','displayvalue': '>100M', 'code': '#6baa01'}]}, 'data': [{'id': '01', 'value': '3189000000' }]  }"

        jsonSettingMap = "'chart': {'caption': 'Site Document Progress',        'subcaption': 'in Percentage',  'palletecolors':'#0075c2,#1aaf5d,#f2c500,#f45b00,#8e0000', 'showBorder':'0', 'showShadow' : '6','startingAngle': '310','showLabels': '0','showPercentValues': '1','showLegend': '1','legendShadow': '0','legendBorderAlpha': '0', 'decimals': '0', 'captionFontSize': '14', 'subcaptionFontSize': '14', 'subcaptionFontBold': '0', 'toolTipColor': '#ffffff','toolTipBorderThickness': '0','toolTipBgColor': '#000000', 'toolTipBgAlpha': '80','toolTipBorderRadius': '2', 'toolTipPadding': '5', },"
        'jsonSettingValue = "'colorrange': {  'color': [{   'minvalue': '0', 'maxvalue':'50','displayvalue': '<51%', 'code': '#ff0000' }, {'minvalue': '51', 'maxvalue':'101','displayvalue': '>51%', 'code': '#6baa01'}]},"
        Dim getresults As DataTable = New GeneralController().GetSummaryProgress(pono, siteid)
        Dim count As Integer = 0
        If getresults.Rows.Count > 0 Then

            jsonGetData += "'data': [ "
            For Each drw As DataRow In getresults.Rows
                If count > 0 Then
                    jsonGetData += ", "
                End If
                If drw.Item("counttype").Equals("done") Then
                    jsonGetData += "{'label': '" & drw.Item("counttype") & "', 'value': '" & drw.Item("rowcount") & "', 'issliced': '1'}"
                Else
                    jsonGetData += "{'label': '" & drw.Item("counttype") & "', 'value': '" & drw.Item("rowcount") & "'}"
                End If

                count += 1
            Next
            jsonGetData += " ]"
        End If


        jsonData = "{ " & jsonSettingMap & jsonSettingValue & jsonGetData & " }"
        ' Initialize chart
        Dim chart As New Chart("doughnut3d", "surveyProgress", "550", "350", "json", jsonData)
        ltrDocProgress.Text = chart.Render()
    End Sub

#End Region
End Class
