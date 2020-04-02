Imports BusinessLogic
Imports Common
Imports Entities
Imports System.Data
Imports FusionCharts.Charts
Imports NSNCustomizeConfiguration


Partial Class DashBoard_frmSiteAllTaskPending
    Inherits System.Web.UI.Page
    Dim dtTaskPendings As New DataTable
    Dim dtTaskPendingsRFT As New DataTable
    Dim dtTaskPendingsQC As New DataTable
    Dim dtTaskPendingsAppr As New DataTable
    Dim dtTaskPendingsSOAC As New DataTable
    Dim objBo As New BODashBoard
    Dim objdb As New DBUtil
    Dim delegatecontroller As New DelegationController
	Dim generalcontrol As New GeneralController

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            LinkDisabled()
            HyperLinkDisabled()
            BindData()
            BindRejectedDocumentsHCPT(CommonSite.UserId())
            BindRFTReadyCreation()
            BindDataApprovedLast30Days()
            BindDoneReport(CommonSite.UserId)
            BindDocProgress(String.Empty, String.Empty)
            BindPONO(DdlPONO)
			BindDocDone(CommonSite.UserId)
        End If
    End Sub

    Private Sub LbtTI2GClick(ByVal sender As Object, ByVal e As EventArgs) Handles LbtTI2GLink.Click
        Response.Redirect("frmSiteDocCount.aspx")
    End Sub

    Protected Sub DdlPONO_SelectedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles DdlPONO.SelectedIndexChanged
        BindDocProgress(DdlPONO.SelectedValue, String.Empty)
    End Sub
    Protected Sub lbtViewRejectedDoc_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lbtViewRejectedDoc.Click
        Response.Redirect("frmRejectedDocNPOHCPT.aspx")
    End Sub

#Region "Custom Methods"
    Private Sub BindData()
        dtTaskPendings = objdb.ExeQueryDT("exec HCPT_uspGetAllTaskPendings " & CommonSite.UserId(), "taskPendings")
        If dtTaskPendings.Rows.Count > 0 Then
            For intCount As Integer = 0 To dtTaskPendings.Rows.Count - 1
                If (dtTaskPendings.Rows(intCount)("scope").ToString().ToUpper() = "TI2G") Then
                    If dtTaskPendings.Rows(intCount)("TotalTaskPending") = 0 Then
                        LblTI2GLinkDisabled.Visible = True
                    Else
                        LbtTI2GLink.Visible = True
                        LbtTI2GLink.Text = Convert.ToString(dtTaskPendings.Rows(intCount)("TotalTaskPending"))
                    End If
                End If

                If (dtTaskPendings.Rows(intCount)("scope").ToString().ToUpper() = "TI3G") Then
                    If dtTaskPendings.Rows(intCount)("TotalTaskPending") = 0 Then
                        LblTI3GLinkDisabled.Visible = True
                    Else
                        HpTI3GLink.Visible = True
                        HpTI3GLink.Text = Convert.ToString(dtTaskPendings.Rows(intCount)("TotalTaskPending"))
                        HpTI3GLink.Target = "_top"
                        HpTI3GLink.NavigateUrl = ConfigurationManager.AppSettings("3GTIURL") & "?S=" & Session("User_Login") & "&P=" & Session("User_Pwd") & "&R=" & Session("Res") & "&PT=3G&tskid=1"
                    End If
                End If

                If (dtTaskPendings.Rows(intCount)("scope").ToString().ToUpper() = "SITAC2G") Then
                    If dtTaskPendings.Rows(intCount)("TotalTaskPending") = 0 Then
                        LblSITAC2GLinkDisabled.Visible = True
                    Else
                        HpSITAC2GLink.Visible = True
                        HpSITAC2GLink.Text = Convert.ToString(dtTaskPendings.Rows(intCount)("TotalTaskPending"))
                        HpSITAC2GLink.Target = "_top"
                        HpSITAC2GLink.NavigateUrl = ConfigurationManager.AppSettings("SITacURL") & "?S=" & Session("User_Login") & "&P=" & Session("User_Pwd") & "&R=" & Session("Res") & "&PT=2G&tskid=1"
                    End If
                End If

                If (dtTaskPendings.Rows(intCount)("scope").ToString().ToUpper() = "SITAC3G") Then
                    If dtTaskPendings.Rows(intCount)("TotalTaskPending") = 0 Then
                        LblSITAC3GLinkDisabled.Visible = True
                    Else
                        HpSITAC3GLink.Visible = True
                        HpSITAC3GLink.Text = Convert.ToString(dtTaskPendings.Rows(intCount)("TotalTaskPending"))
                        HpSITAC3GLink.Target = "_top"
                        HpSITAC3GLink.NavigateUrl = ConfigurationManager.AppSettings("3GSITacURL") & "?S=" & Session("User_Login") & "&P=" & Session("User_Pwd") & "&R=" & Session("Res") & "&PT=3G&tskid=1"
                    End If
                End If

                If (dtTaskPendings.Rows(intCount)("scope").ToString().ToUpper() = "CME2G") Then
                    If dtTaskPendings.Rows(intCount)("TotalTaskPending") = 0 Then
                        LblCME2GLinkDisabled.Visible = True
                    Else
                        HpCME2GLink.Visible = True
                        HpCME2GLink.Text = Convert.ToString(dtTaskPendings.Rows(intCount)("TotalTaskPending"))
                        HpCME2GLink.Target = "_top"
                        HpCME2GLink.NavigateUrl = ConfigurationManager.AppSettings("CMEURL") & "?S=" & Session("User_Login") & "&P=" & Session("User_Pwd") & "&R=" & Session("Res") & "&PT=2G&tskid=1"
                    End If
                End If

                If (dtTaskPendings.Rows(intCount)("scope").ToString().ToUpper() = "CME3G") Then
                    If dtTaskPendings.Rows(intCount)("TotalTaskPending") = 0 Then
                        LblCME3GLinkDisabled.Visible = True
                    Else
                        HpCME3GLink.Visible = True
                        HpCME3GLink.Text = Convert.ToString(dtTaskPendings.Rows(intCount)("TotalTaskPending"))
                        HpCME3GLink.Target = "_top"
                        HpCME3GLink.NavigateUrl = ConfigurationManager.AppSettings("3GCMEURL") & "?S=" & Session("User_Login") & "&P=" & Session("User_Pwd") & "&R=" & Session("Res") & "&PT=3G&tskid=1"
                    End If
                End If

                If (dtTaskPendings.Rows(intCount)("scope").ToString().ToUpper() = "SIS2G") Then
                    If dtTaskPendings.Rows(intCount)("TotalTaskPending") = 0 Then
                        LblSIS2GLinkDisabled.Visible = True
                    Else
                        HpSIS2GLink.Visible = True
                        HpSIS2GLink.Text = Convert.ToString(dtTaskPendings.Rows(intCount)("TotalTaskPending"))
                        HpSIS2GLink.Target = "_top"
                        HpSIS2GLink.NavigateUrl = ConfigurationManager.AppSettings("SISURL") & "?S=" & Session("User_Login") & "&P=" & Session("User_Pwd") & "&R=" & Session("Res") & "&PT=2G&tskid=1"
                    End If
                End If

                If (dtTaskPendings.Rows(intCount)("scope").ToString().ToUpper() = "SIS3G") Then
                    If dtTaskPendings.Rows(intCount)("TotalTaskPending") = 0 Then
                        LblSIS3GLinkDisabled.Visible = True
                    Else
                        HpSIS3GLink.Visible = True
                        HpSIS3GLink.Text = Convert.ToString(dtTaskPendings.Rows(intCount)("TotalTaskPending"))
                        HpSIS3GLink.Target = "_top"
                        HpSIS3GLink.NavigateUrl = ConfigurationManager.AppSettings("3GSISURL") & "?S=" & Session("User_Login") & "&P=" & Session("User_Pwd") & "&R=" & Session("Res") & "&PT=3G&tskid=1"
                    End If
                End If
            Next
        End If
    End Sub

    Private Sub BindRFTReadyCreation()
        dtTaskPendingsRFT = objdb.ExeQueryDT("exec HCPT_uspRFT_GetReadyCreation_Count " & CommonSite.UserId() & ", " & ConfigurationManager.AppSettings("BAUTID"), "taskPendings")
        If dtTaskPendingsRFT.Rows.Count > 0 Then
            For intCount As Integer = 0 To dtTaskPendingsRFT.Rows.Count - 1
                If (dtTaskPendingsRFT.Rows(intCount)("scope").ToString().ToUpper() = "TI2G") Then
                    If dtTaskPendingsRFT.Rows(intCount)("Totaldoc") = 0 Then
                        LblTI2GRFTLink.Visible = True
                    Else
                        LbtTI2GRFTLink.Visible = True
                        LblLinkRFT2G.Text = Convert.ToString(dtTaskPendingsRFT.Rows(intCount)("Totaldoc"))
                    End If
                End If
                LblTI3GRFTLink.Visible = True
                LblCME2GRFTLink.Visible = True
                LblCME3GRFTLink.Visible = True
                LblSIS2GRFTLink.Visible = True
                LblSIS3GRFTLink.Visible = True
                LblSITAC2GRFTLink.Visible = True
                LblSITAC3GRFTLink.Visible = True
            Next
        End If
    End Sub

    Private Sub BindDataApprovedLast30Days()
        dtTaskPendingsAppr = objdb.ExeQueryDT("exec HCPT_uspTrans_GetDocApprovedByUser30Days_Count " & CommonSite.UserId(), "taskPendings")
        If dtTaskPendingsAppr.Rows.Count > 0 Then
            For intCount As Integer = 0 To dtTaskPendingsAppr.Rows.Count - 1
                If (dtTaskPendingsAppr.Rows(intCount)("scope").ToString().ToUpper() = "TI2G") Then
                    If dtTaskPendingsAppr.Rows(intCount)("Totaldoc") = 0 Then
                        LblTI2GAPPLink.Visible = True
                    Else
                        LbtTI2GAPPLink.Visible = True
                        LblLinkAPP2G.Text = Convert.ToString(dtTaskPendingsAppr.Rows(intCount)("Totaldoc"))
                    End If
                End If
                LblTI3GAPPLink.Visible = True
                LblCME2GAPPLink.Visible = True
                LblCME3GAPPLink.Visible = True
                LblSIS2GAPPLink.Visible = True
                LblSIS3GAPPLink.Visible = True
                LblSITAC2GAPPLink.Visible = True
                LblSITAC3GAPPLink.Visible = True
            Next
        End If
    End Sub

    Private Sub LinkDisabled()
        LblTI2GLinkDisabled.Visible = False
        LblSIS2GLinkDisabled.Visible = False
        LblSITAC2GLinkDisabled.Visible = False
        LblCME2GLinkDisabled.Visible = False
        LblTI3GLinkDisabled.Visible = False
        LblSIS3GLinkDisabled.Visible = False
        LblSITAC3GLinkDisabled.Visible = False
        LblCME3GLinkDisabled.Visible = False

        LblTI2GRFTLink.Visible = False
        LblTI3GRFTLink.Visible = False

        LblSIS2GAPPLink.Visible = False
        LblSITAC2GAPPLink.Visible = False
        LblCME2GAPPLink.Visible = False
        LblTI2GAPPLink.Visible = False

        LblSIS3GAPPLink.Visible = False
        LblSITAC3GAPPLink.Visible = False
        LblCME3GAPPLink.Visible = False
        LblTI3GAPPLink.Visible = False
        
    End Sub

    Private Sub HyperLinkDisabled()
        LbtTI2GLink.Visible = False
        HpTI3GLink.Visible = False
        HpSIS2GLink.Visible = False
        HpSIS3GLink.Visible = False
        HpSITAC2GLink.Visible = False
        HpSITAC3GLink.Visible = False
        HpCME2GLink.Visible = False
        HpCME3GLink.Visible = False

        LbtTI2GRFTLink.Visible = False
        HpSIS2GRFTLink.Visible = False
        HpSITAC2GRFTLink.Visible = False
        HpCME2GRFTLink.Visible = False

        HpTI3GRFTLink.Visible = False
        HpSIS3GRFTLink.Visible = False
        HpSITAC3GRFTLink.Visible = False
        HpCME3GRFTLink.Visible = False

        LbtTI2GAPPLink.Visible = False
        HpCME2GAPPLink.Visible = False
        HpSITAC2GAPPLink.Visible = False
        HpSIS2GAPPLink.Visible = False
        HpTI3GAPPLink.Visible = False
        HpCME3GAPPLink.Visible = False
        HpSITAC3GAPPLink.Visible = False
        HpSIS3GAPPLink.Visible = False

    End Sub

    Private Sub BindDoneReport(ByVal userid As Integer)
        Dim dtResult As DataTable = New GeneralController().GetSummaryDone(userid)
        ltrATPDoneCount.Text = 0
        ltrCACDoneCount.Text = 0
        ltrMSFICount.Text = 0
        ltrPACDoneCount.Text = 0
        ltrFACDoneCount.Text = 0
        ltrATPDonePerc.Text = "<div class=""progress-bar"" style=""width: 0%""></div>"
        ltrCACDonePerc.Text = "<div class=""progress-bar"" style=""width: 0%""></div>"
        ltrFACDonePerc.Text = "<div class=""progress-bar"" style=""width: 0%""></div>"
        ltrMSFIDonePerc.Text = "<div class=""progress-bar"" style=""width: 0%""></div>"
        ltrPACDonePerc.Text = "<div class=""progress-bar"" style=""width: 0%""></div>"

        If dtResult.Rows.Count > 0 Then
            For Each drw As DataRow In dtResult.Rows
                If drw.Item("alias_docname") = "einstcom" Then
                    Dim getperc = Integer.Parse(drw.Item("totaldone")) / 6889
                    Dim strPerc As String = getperc.ToString() & "%"
                    ltrATPDonePerc.Text = "<div class=""progress-bar"" style=""width:" & strPerc & """></div>"
                    ltrATPDoneCount.Text = "<a class=""popup"" title=""ATP DONE"" href=""../RPT/frmEBAUTDone.aspx?docid=2001""><span style=""font-family: Verdana; font-size: 10pt;color:#fff;"">" & drw.Item("totaldone") & "</span></a>"

                ElseIf drw.Item("alias_docname") = "msfi" Then
                    Dim getperc = Integer.Parse(drw.Item("totaldone")) / 6889
                    Dim strPerc As String = getperc.ToString() & "%"
                    ltrMSFIDonePerc.Text = "<div class=""progress-bar"" style=""width:" & strPerc & """></div>"
                    ltrMSFICount.Text = "<a class=""popup"" title=""MSFI DONE"" href=""../RPT/frmEBAUTDone.aspx?docid=2045""><span style=""font-family: Verdana; font-size: 10pt;color:#fff;"">" & drw.Item("totaldone") & "</span></a>"
                ElseIf drw.Item("alias_docname") = "cac" Then
                    Dim getperc = Integer.Parse(drw.Item("totaldone")) / 6889
                    Dim strPerc As String = getperc.ToString() & "%"
                    ltrCACDonePerc.Text = "<div class=""progress-bar"" style=""width:" & strPerc & """></div>"
                    ltrCACDoneCount.Text = "<a class=""popup"" title=""CAC DONE"" href=""../RPT/frmEBAUTDone.aspx?docid=2046""><span style=""font-family: Verdana; font-size: 10pt;color:#fff;"">" & drw.Item("totaldone") & "</span></a>"
                ElseIf drw.Item("alias_docname") = "epac" Then
                    Dim getperc = Integer.Parse(drw.Item("totaldone")) / 6889
                    Dim strPerc As String = getperc.ToString() & "%"
                    ltrPACDonePerc.Text = "<div class=""progress-bar"" style=""width:" & strPerc & """></div>"
                    ltrPACDoneCount.Text = "<a class=""popup"" title=""PAC DONE"" href=""../RPT/frmEBAUTDone.aspx?docid=2047""><span style=""font-family: Verdana; font-size: 10pt;color:#fff;"">" & drw.Item("totaldone") & "</span></a>"
                ElseIf drw.Item("alias_docname") = "efac" Then
                    Dim getperc = Integer.Parse(drw.Item("totaldone")) / 6889
                    Dim strPerc As String = getperc.ToString() & "%"
                    ltrFACDonePerc.Text = "<div class=""progress-bar"" style=""width:" & strPerc & """></div>"
                    ltrFACDoneCount.Text = "<a class=""popup"" title=""FAC DONE"" href=""../RPT/frmEBAUTDone.aspx?docid=1031""><span style=""font-family: Verdana; font-size: 10pt;color:#fff;"">" & drw.Item("totaldone") & "</span></a>"
                End If
            Next
        End If
    End Sub

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

    Private Sub BindPONO(ByVal ddl As DropDownList)
        ddl.DataSource = New GeneralController().GetAllPONO()
        ddl.DataTextField = "pono"
        ddl.DataValueField = "pono"
        ddl.DataBind()
        ddl.Items.Insert(0, New ListItem("--Select PONO--", "0"))
    End Sub

    Private Sub BindSiteList(ByVal ddl As DropDownList)

    End Sub

    Private Sub BindDocDone(ByVal userid As Integer)
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
                End If
            Next
        End If
    End Sub
    Private Sub BindRejectedDocumentsHCPT(ByVal userid As Integer)
        Dim getrejectdoc As Integer
        Dim dtResult As DataTable = generalcontrol.RejectedDoc_HCPT(userid)

        If dtResult.Rows.Count > 0 Then
            For Each drw As DataRow In dtResult.Rows
                getrejectdoc = Integer.Parse(drw.Item("CountUsrType"))
                lblRejectedDocCount.Text = getrejectdoc
            Next
        End If
        If getrejectdoc > 0 Then
            lblRejectedDocCountDisabled.Visible = False
			lblRejectedDocCount.Visible = True
        Else
            lblRejectedDocCountDisabled.Visible = True
			lblRejectedDocCount.Visible = False
        End If
    End Sub


#End Region

End Class
