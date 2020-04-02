Imports BusinessLogic
Imports Common
Imports Entities
Imports System.Data
Imports System.Threading
Imports System.ComponentModel
Imports Common_NSNFramework
Imports NSNCustomizeConfiguration
Imports CRFramework

Partial Class frmDashboard_Temp
    Inherits System.Web.UI.Page
    Dim objBOD As New BODDLs
    Dim objET As New ETEBASTUsers
    Dim objBO As New BODashBoard
    Dim dt, dtdg As New DataTable
    Dim objUtil As New DBUtil
    Dim objdl As New BODDLs
    Dim objutils_nsn As New DBUtils_NSN
    Dim strsql As String
    Dim intATPCountPending As Integer = 0
    Dim intQCCountPending As Integer = 0
    Dim controller As New WCCController

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        If Page.IsPostBack = False Then
            'Dim isDashboardConfig As Integer = objUtil.ExeQueryScalar("select count(*) from DashboardUser where roleid=" & CommonSite.RollId)
            'If isDashboardConfig = 0 Then
            '    GeneralDashboard()
            'Else
            '    'Dim strDashboardType As String = objUtil.ExeQueryScalarString("select Dashboard_Type from DashboardUser where roleid=" & CommonSite.RollId)
            '    'SpecifyDashboard(strDashboardType)
            'End If
            GeneralDashboard()
        End If
    End Sub

    Protected Sub LbtCRDashboardAgendaClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles LbtCRDashboardAgenda.Click
        Response.Redirect("Dashboard/CRDashboardAgenda.aspx?typedash=cf&displaytype=plain")
    End Sub

    Protected Sub LbtSiteNeedCRClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles LbtSiteNeedCR.Click
        Response.Redirect("Dashboard/CRDashboardAgenda.aspx?typedash=nc&displaytype=plain")
    End Sub

    Protected Sub LbtCRRejectionClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles LbtCRRejection.Click
        Response.Redirect("CR/CRCORejection.aspx?type=cr")
    End Sub

    Protected Sub LbtCORejectionClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles LbtCORejection.Click
        Response.Redirect("CR/CRCORejection.aspx?type=co")
    End Sub

    Private Sub GeneralDashboard()
        Dim isDashboard As Boolean = True
        MvCorePanel.SetActiveView(VwGeneral)
        If (CommonSite.RollId = ConfigurationManager.AppSettings("ATPROLEAPP")) Then
            intATPCountPending = objutils_nsn.CountATPDocTaskPending(CommonSite.UserId, ConfigurationManager.AppSettings("ATPROLEAPP"))
            If (Request.QueryString("from") Is Nothing) Then
                If (intATPCountPending > 0) Then
                    'Response.Redirect("~/Dashboard/frmSiteDocCount_ATP.aspx?id=" & CommonSite.UserId)
                    'isDashboard = False
                End If
            End If
        End If

        If (isDashboard = True) Then
            CreateAgenda()
            CreateSiteStatus()
        End If
    End Sub

    Private Sub SpecifyDashboard(ByVal dashboardtype As String)
        If dashboardtype.ToLower().Equals("cronly") Then
            MvCorePanel.SetActiveView(VwCRCOOnly)
            strsql = "exec [uspCRCO_Agenda] " & CommonSite.UserId & ", " & ConfigurationManager.AppSettings("CRDOCID") & ", " & ConfigurationManager.AppSettings("CODOCID")
            Dim dtCRAgenda As DataTable = objUtil.ExeQueryDT(strsql, "cragenda")
            If dtCRAgenda.Rows.Count > 0 Then
                For intCount As Integer = 0 To dtCRAgenda.Rows.Count - 1
                    If (dtCRAgenda.Rows(intCount)("TypeofPending").ToString() = "CODone") Then
                        If dtCRAgenda.Rows(intCount)("Total") = "0" Then
                            LblCODoneNull.Visible = True
                            LblCODone.Visible = False
                        Else
                            LblCODoneNull.Visible = False
                            LblCODone.Visible = True
                            LblCODone.Text = "CO Done(" & dtCRAgenda.Rows(intCount)("Total") & ")"
                        End If
                    ElseIf (dtCRAgenda.Rows(intCount)("TypeofPending").ToString() = "COTselSignature") Then
                        If dtCRAgenda.Rows(intCount)("Total") = "0" Then
                            LblCOTselUnderSignature.Visible = True
                            LblCOTselUnderSignature.Visible = False
                        Else
                            LblCOTselUnderSignatureNull.Visible = False
                            LblCOTselUnderSignature.Visible = True
                            LblCOTselUnderSignature.Text = "CO Tsel Under Signature(" & dtCRAgenda.Rows(intCount)("Total") & ")"
                        End If
                    ElseIf (dtCRAgenda.Rows(intCount)("TypeofPending").ToString() = "CONSNSignature") Then
                        If dtCRAgenda.Rows(intCount)("Total") = "0" Then
                            LblCONSNUnderSignatureNull.Visible = True
                            LblCONSNUnderSignature.Visible = False
                        Else
                            LblCONSNUnderSignatureNull.Visible = False
                            LblCONSNUnderSignature.Visible = True
                            LblCONSNUnderSignature.Text = "CO NSN Under Signature(" & dtCRAgenda.Rows(intCount)("Total") & ")"
                        End If
                    ElseIf (dtCRAgenda.Rows(intCount)("TypeofPending").ToString() = "CORejection") Then
                        If dtCRAgenda.Rows(intCount)("Total") = 0 Then
                            LblCORejectionNull.Visible = True
                            LbtCORejection.Visible = False
                        Else
                            LblCORejectionNull.Visible = False
                            LbtCORejection.Visible = True
                            LbtCORejection.Text = "CO Rejection(" & dtCRAgenda.Rows(intCount)("Total") & ")"
                        End If
                    ElseIf (dtCRAgenda.Rows(intCount)("TypeofPending").ToString() = "COReadyCreation") Then
                        If dtCRAgenda.Rows(intCount)("Total") = 0 Then
                            LblCOReadyCreationNull.Visible = True
                            LblCOReadyCreation.Visible = False
                        Else
                            LblCOReadyCreationNull.Visible = False
                            LblCOReadyCreation.Visible = True
                            LblCOReadyCreation.Text = "CO Ready Creation(" & dtCRAgenda.Rows(intCount)("Total") & ")"
                        End If
                    ElseIf (dtCRAgenda.Rows(intCount)("TypeofPending").ToString() = "CRFinalDone") Then
                        If dtCRAgenda.Rows(intCount)("Total") = "0" Then
                            LblCRFinalNull.Visible = True
                            LblCRFinal.Visible = False
                            LbtCRDashboardAgenda.Visible = False
                        Else
                            LblCRFinalNull.Visible = False
                            LblCRFinal.Visible = False
                            LbtCRDashboardAgenda.Visible = True
                            LblCRFinal.Text = "CR Final Done(" & dtCRAgenda.Rows(intCount)("Total") & ")"
                            LbtCRDashboardAgenda.Text = "CR Final Done(" & dtCRAgenda.Rows(intCount)("Total") & ")"
                        End If
                    ElseIf (dtCRAgenda.Rows(intCount)("TypeofPending").ToString() = "TselSignature") Then
                        If dtCRAgenda.Rows(intCount)("Total") = "0" Then
                            LblCRTselUnderSignatureNull.Visible = True
                            LblCRTselUnderSignature.Visible = False
                        Else
                            LblCRTselUnderSignatureNull.Visible = False
                            LblCRTselUnderSignature.Visible = True
                            LblCRTselUnderSignature.Text = "CR Tsel Under Signature(" & dtCRAgenda.Rows(intCount)("Total") & ")"
                        End If
                    ElseIf (dtCRAgenda.Rows(intCount)("TypeofPending").ToString() = "NSNSignature") Then
                        If dtCRAgenda.Rows(intCount)("Total") = "0" Then
                            LblCRUnderNSNSignatureNull.Visible = True
                            LblCRUnderNSNSignature.Visible = False
                        Else
                            LblCRUnderNSNSignatureNull.Visible = False
                            LblCRUnderNSNSignature.Visible = True
                            LblCRUnderNSNSignature.Text = "CR NSN Under Signature(" & dtCRAgenda.Rows(intCount)("Total") & ")"
                        End If
                    ElseIf (dtCRAgenda.Rows(intCount)("TypeofPending").ToString() = "CRRejection") Then
                        If dtCRAgenda.Rows(intCount)("Total") = "0" Then
                            LblCRRejection.Visible = True
                            LbtCRRejection.Visible = False
                        Else
                            LblCRRejection.Visible = False
                            LbtCRRejection.Visible = True
                            LbtCRRejection.Text = "CR Rejection(" & dtCRAgenda.Rows(intCount)("Total") & ")"
                        End If
                    ElseIf (dtCRAgenda.Rows(intCount)("TypeofPending").ToString() = "CRReadyCreation") Then
                        If dtCRAgenda.Rows(intCount)("Total") = "0" Then
                            LblCRReadyCreationNull.Visible = True
                            LblCRReadyCreation.Visible = False
                        Else
                            LblCRReadyCreationNull.Visible = False
                            LblCRReadyCreation.Visible = True
                            LblCRReadyCreation.Text = "CR Ready Creation (" & dtCRAgenda.Rows(intCount)("Total") & ")"
                        End If
                    End If
                Next
            End If
        ElseIf dashboardtype.ToLower().Equals("general") Then
            GeneralDashboard()
        ElseIf dashboardtype.ToLower().Equals("generalreport") Then
            'Response.Redirect("frmDashboardGeneralReport.aspx")
            GeneralDashboard()
        End If
    End Sub

    Sub CreateAgenda()
        Dim iMainTable As New HtmlTable
        iMainTable.Width = "100%"
        Dim tsites As Integer
        Dim dtTask As New DataTable
        dtTask = objBO.uspDashBoardAgendaTask(CommonSite.UserId())
        If dtTask.Rows.Count > 0 Then
            Dim iMainRownew As New HtmlTableRow
            Dim iMainCellSiteNamenew As New HtmlTableCell
            iMainCellSiteNamenew.Width = "75%"
            iMainCellSiteNamenew.Attributes.Add("class", "dashboard")
            iMainCellSiteNamenew.Style.Add("align", "left")
            Dim iMainCellPrecentagenew As New HtmlTableCell
            iMainCellPrecentagenew.Width = "25%"
            iMainCellPrecentagenew.Attributes.Add("class", "dashboard")
            iMainCellPrecentagenew.Style.Add("align", "right")
            tsites = dtTask.Compute("sum(Site_No)", "")
            If tsites > 0 Then
                iMainCellSiteNamenew.InnerHtml = "<img src='../Images/bullet_yellow.png' alt='listicon' width='12' height='12' />" & " <a href='DashBoard/frmSiteDocCount.aspx?id=" & CommonSite.UserId() & "'>Task Pending Sites ( " & tsites & " ) </a>"
            Else
                iMainCellSiteNamenew.InnerHtml = "<img src='../Images/bullet_yellow.png' alt='listicon' width='12' height='12' />" & " Task Pending Sites ( 0 )"
            End If
            iMainRownew.Cells.Add(iMainCellSiteNamenew)
            iMainRownew.Cells.Add(iMainCellPrecentagenew)
            iMainTable.Rows.Add(iMainRownew)
        End If
 
        Dim dtAgenda As New DataTable
        strsql = "exec [HCPT_uspDashBoardAgenda] " & CommonSite.GetDashBoardLevel().ToString & "," & CommonSite.UserId().ToString & "," & ConfigurationManager.AppSettings("WCTRBASTID").ToString & "," & Session("User_Id").ToString
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

            If dRowsStatus.Item("usrtype").ToString().ToLower() = "rft" Then
                If dRowsStatus.Item("CountUsrType") > 0 Then
                    iMainCellSiteName.InnerHtml = "<img src='../Images/bullet_yellow.png' alt='listicon' width='12' height='12' />" & " <a href='../HCPT_Dashboard/frmFACReadyCreation.aspx'>FAC Ready Creation ( " & dRowsStatus.Item("CountUsrType") & " ) </a>"
                Else
                    iMainCellSiteName.InnerHtml = "<img src='../Images/bullet_yellow.png' alt='listicon' width='12' height='12' />" & " FAC Ready Creation ( 0 )"
                End If
                iMainRow.Cells.Add(iMainCellSiteName)
                iMainRow.Cells.Add(iMainCellPrecentage)
                iMainTable.Rows.Add(iMainRow)
            End If
            If dRowsStatus.Item("usrtype").ToString().ToLower() = "kpi" Then
                If dRowsStatus.Item("CountUsrType") > 0 Then
                    iMainCellSiteName.InnerHtml = "<img src='../Images/bullet_yellow.png' alt='listicon' width='12' height='12' />" & " <a href='../PO/frmQCReadyCreation.aspx'>KPI Ready Creation ( " & dRowsStatus.Item("CountUsrType") & " ) </a>"
                Else
                    iMainCellSiteName.InnerHtml = "<img src='../Images/bullet_yellow.png' alt='listicon' width='12' height='12' />" & " KPI Ready Creation ( 0 )"
                End If
                'iMainRow.Cells.Add(iMainCellSiteName)
                'iMainRow.Cells.Add(iMainCellPrecentage)
                'iMainTable.Rows.Add(iMainRow)
            End If
            If dRowsStatus.Item("usrtype").ToString().ToLower() = "r" Then
                If dRowsStatus.Item("CountUsrType") > 0 Then
                    iMainCellSiteName.InnerHtml = "<img src='../Images/bullet_yellow.png' alt='listicon' width='12' height='12' />" & " <a href=# onclick=popwindowDashBoard(7) >Rejected Documents ( " & dRowsStatus.Item("CountUsrType") & " ) </a>"
                Else
                    iMainCellSiteName.InnerHtml = "<img src='../Images/bullet_yellow.png' alt='listicon' width='12' height='12' />" & " Rejected Documents ( 0 )"
                End If
                iMainRow.Cells.Add(iMainCellSiteName)
                iMainRow.Cells.Add(iMainCellPrecentage)
                iMainTable.Rows.Add(iMainRow)
            End If
            If dRowsStatus.Item("usrtype").ToString().ToLower() = "ld" Then
                If dRowsStatus.Item("CountUsrType") > 0 Then
                    iMainCellSiteName.InnerHtml = "<img src='../Images/bullet_yellow.png' alt='listicon' width='12' height='12' />" & " <a href=# onclick=popwindowDashBoard(8) >Document Sign Last 30 Days ( " & dRowsStatus.Item("CountUsrType") & " ) </a>"
                Else
                    iMainCellSiteName.InnerHtml = "<img src='../Images/bullet_yellow.png' alt='listicon' width='12' height='12' />" & " Document Sign Last 30 Days ( 0 )"
                End If
                iMainRow.Cells.Add(iMainCellSiteName)
                iMainRow.Cells.Add(iMainCellPrecentage)
                iMainTable.Rows.Add(iMainRow)
            End If

           
        Next
        Dim iMaingap As New HtmlTableRow
        Dim iMainCellgap As New HtmlTableCell
        iMainCellgap.ColSpan = 2
        iMainCellgap.Attributes.Add("class", "hgap")
        iMaingap.Cells.Add(iMainCellgap)
        iMainTable.Rows.Add(iMaingap)
        tdAgenda.Controls.Add(iMainTable)
    End Sub

    Sub CreateSiteStatus()
        Dim iMainTable As New HtmlTable
        Dim dtStatus As New DataTable
        Dim dtra As New DataTable
        Dim dtr As New DataTable
        Dim dta As New DataTable
        Dim mycase As String = ""
        Dim rgn As String = ""
        Dim i As Integer = 0
        If Session("User_Name") Is Nothing Then
            Response.Redirect("~/SessionTimeOut.aspx")
        Else
            Dim thread As BackgroundWorker

            thread = New BackgroundWorker
            dtStatus = objUtil.ExeQueryDT("exec HCPT_uspDashboard_SiteAcceptanceStatus " & ConfigurationManager.AppSettings("FACDOCID") & "," & ConfigurationManager.AppSettings("PACDOCID") & "," & CommonSite.UserId, "sitedoc")
            Dim strBind As String = ""
            Dim strBindNo As String = ""
            Dim strBinddays As String = ""
            If dtStatus.Rows.Count > 1 Then
                For intCount As Integer = 0 To dtStatus.Rows.Count - 1
                    If (dtStatus.Rows(intCount)("ECount") = 0) Then
                        strBind = "<div class=""hgap"">" & dtStatus.Rows(intCount)("Process").ToString & " </div>"
                        'strBindNo = "<div class=""hgap"">( " & dtStatus.Rows(intCount)("nodays").ToString & " )</div>"
                        strBinddays = "<div class=""hgap"">" & dtStatus.Rows(intCount)("ECount").ToString & "</div>"
                    Else
                        If dtStatus.Rows(intCount)("nodays") Is DBNull.Value Then
                            strBind = "<div class=""hgap""><a href=# onclick=popSitesDetails(" + intCount.ToString + ")> " & dtStatus.Rows(intCount)("Process").ToString & "  </a></div>"
                            If (String.IsNullOrEmpty(intCount) = False) Then
                                If (Integer.Parse(intCount.ToString) = 0) Then 'FAC REPORT'
                                    strBind = "<div class=""hgap""><a class='popup' href='RPT/frmEBASTDone.aspx?docid=1031' title='FAC DONE'> " & dtStatus.Rows(intCount)("Process").ToString & "  </a></div>"
                                ElseIf (Integer.Parse(intCount.ToString) = 1) Then 'PAC REPORT'
                                    strBind = "<div class=""hgap""><a class='popup' href='RPT/frmEBAUTDone.aspx?docid=2047' title='PAC DONE'> " & dtStatus.Rows(intCount)("Process").ToString & "  </a></div>"
                                ElseIf (Integer.Parse(intCount.ToString) = 2) Then 'CAC REPORT'
                                    strBind = "<div class=""hgap""><a class='popup' href='RPT/frmCACReport.aspx?docid=2046' title='CAC DONE'> " & dtStatus.Rows(intCount)("Process").ToString & "  </a></div>"
                                ElseIf (Integer.Parse(intCount.ToString) = 3) Then 'ATP Report'
                                    'strBind = "<div class=""hgap"">" & dtStatus.Rows(intCount)("Process").ToString & " </div>"
                                    strBind = "<div class=""hgap""><a class='popup' href='RPT/frmATPReport.aspx?docid=2001' title='ATP DONE'> " & dtStatus.Rows(intCount)("Process").ToString & "  </a></div>"
                                ElseIf (Integer.Parse(intCount.ToString) = 4) Then 'Site Approval Status REPORT'
                                    strBind = "<div class=""hgap""><a class='popup' href='RPT/frmSiteApprovalStatus.aspx'> " & dtStatus.Rows(intCount)("Process").ToString & "  </a></div>"
                                End If
                            End If
                            'strBindNo = "<div class=""hgap"">( " & dtStatus.Rows(intCount)("nodays").ToString & " )"
                            strBinddays = "<div class=""hgap"">" & dtStatus.Rows(intCount)("ECount").ToString & "</div>"
                        Else

                            If (String.IsNullOrEmpty(intCount) = False) Then
                                If (Integer.Parse(intCount.ToString) = 0) Then
                                    strBind = "<div class=""hgap""><a class='popup' href='RPT/frmEBASTDone.aspx' title='BAST DONE'> " & dtStatus.Rows(intCount)("Process").ToString & "  </a></div>"
                                ElseIf (Integer.Parse(intCount.ToString) = 2) Then
                                    strBind = "<div class=""hgap""><a class='popup' href='RPT/frmEBAUTDone.aspx' title='BAUT DONE'> " & dtStatus.Rows(intCount)("Process").ToString & "  </a></div>"
                                ElseIf (Integer.Parse(intCount.ToString) = 3) Then
                                    strBind = "<div class=""hgap""><a class='popup' href='RPT/frmSiteApprovalStatus.aspx'> " & dtStatus.Rows(intCount)("Process").ToString & "  </a></div>"
                                End If
                            End If
                            strBindNo = "<div class=""hgap"">( " & dtStatus.Rows(intCount)("nodays").ToString & " )"
                            strBinddays = dtStatus.Rows(intCount)("ECount").ToString & " Days"
                        End If
                    End If
                    Select Case intCount
                        Case 0
                            Tdbastdone.InnerHtml = strBind
                            TdbastdoneNO.InnerHtml = strBindNo
                            Tdbastdonedays.InnerHtml = strBinddays
                        Case 1
                            Tdready4bast.InnerHtml = strBind
                            Tdready4bastno.InnerHtml = strBindNo
                            Tdready4bastdays.InnerHtml = strBinddays
                        Case 2
                            tdCACdone.InnerHtml = strBind
                            tdCACdoneno.InnerHtml = strBindNo
                            tdcacdonedays.InnerHtml = strBinddays
                        Case 3
                            tdbautdone.InnerHtml = strBind
                            tdbautdoneno.InnerHtml = strBindNo
                            tdbautdonedays.InnerHtml = strBinddays
                        Case 4
                            tdsiteapprovalstatus.InnerHtml = strBind
                            tdsiteapprovalno.InnerHtml = strBindNo
                            tdsiteapprovalstatusdays.InnerHtml = strBinddays
                    End Select
                Next
            End If
        End If
    End Sub

    Protected Sub BtnViewDashboardClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnViewDashboard.Click
        Response.Redirect("frmDashboard.aspx")
    End Sub

End Class
