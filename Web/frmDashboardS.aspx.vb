Imports BusinessLogic
Imports Common
Imports Entities
Imports System.Data
Partial Class newRPT_frmdashboardS
    Inherits System.Web.UI.Page
    Dim objBOD As New BODDLs
    Dim objET As New ETEBASTUsers
    Dim objBO As New BODashBoard
    Dim dt, dtdg As New DataTable
    Dim objUtil As New DBUtil
    Dim strsql As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        If Not IsPostBack Then
            'BindControl()
        End If
        CreateSiteStatus()
        CreateBaut()
        CreateAgenda()
    End Sub
    Sub BindControl()
        Dim DashBoardUserControl As UserControl
        Select Case (CommonSite.GetDashBoardLevel())
            Case 0
                'Select Case (CommonSite.RollId())
                '    Case 2
                '        DashBoardUserControl = LoadControl("DashBoard/DashBoardSubAdmin.ascx")
                '    Case 3
                '        DashBoardUserControl = LoadControl("DashBoard/DashBoardSubAdmin.ascx")
                '    Case Else
                DashBoardUserControl = LoadControl("DashBoard/DashBoardAdmin.ascx")
                'End Select
            Case 1
                DashBoardUserControl = LoadControl("DashBoard/DashBoardArea.ascx")
            Case 2
                DashBoardUserControl = LoadControl("DashBoard/DashBoardRegion.ascx")
            Case 3
                DashBoardUserControl = LoadControl("DashBoard/DashBoardZone.ascx")
            Case 4
                DashBoardUserControl = LoadControl("DashBoard/DashBoardSite.ascx")
            Case 5
                DashBoardUserControl = LoadControl("DashBoard/DashBoardTopLevel.ascx")
            Case Else
                DashBoardUserControl = LoadControl("DashBoard/DashBoardAdmin.ascx")
        End Select
        'DashBoardAdmin1.Controls.Clear()
        'DashBoardAdmin1.Controls.Add(DashBoardUserControl)
    End Sub
    Sub CreateSiteStatus()
        'Dim iMainTable As New HtmlTable
        'iMainTable.Width = "100%"
        'iMainTable.CellPadding = 0
        'iMainTable.CellSpacing = 0
        'Dim dtStatus As New DataTable
        'dtStatus = objBO.uspDashBoardSiteStatus(CommonSite.GetDashBoardLevel(), CommonSite.UserId(), CommonSite.SCRID(), CommonSite.UserType())
        'Dim intCount As Integer = 0
        'HdTotal.Value = dtStatus.Rows.Count
        'For intCount = 0 To dtStatus.Rows.Count - 1
        '    If intCount < 15 Then
        '        Dim iMainRow As New HtmlTableRow
        '        iMainRow.ID = "TRDesc" & intCount
        '        Dim iMainCellSiteName As New HtmlTableCell
        '        iMainCellSiteName.Attributes.Add("class", "dashboard")
        '        iMainCellSiteName.Width = "75%"
        '        iMainCellSiteName.Style.Add("align", "left")
        '        Dim iMainCellPrecentage As New HtmlTableCell
        '        iMainCellPrecentage.Width = "25%"
        '        iMainCellPrecentage.Style.Add("align", "right")
        '        iMainCellSiteName.InnerHtml = "<a href='Po/frmSiteDocStatus.aspx?id=" & dtStatus.Rows(intCount).Item("site_no") & "'>" & dtStatus.Rows(intCount).Item("site_no") & "</a>"
        '        iMainCellPrecentage.InnerHtml = dtStatus.Rows(intCount)("Percentage")
        '        iMainRow.Cells.Add(iMainCellSiteName)
        '        iMainRow.Cells.Add(iMainCellPrecentage)
        '        iMainTable.Rows.Add(iMainRow)
        '    Else
        '        Exit For
        '    End If
        'Next intCount
        'Dim dv As New DataView
        'dv = dtStatus.DefaultView
        ''dv.Sort = "Percentage asc"
        'Dim intCountac As Integer = 0
        'For intCount = dv.Count - 1 To 0 Step -1
        '    If intCountac < 15 Then
        '        Dim iMainRow As New HtmlTableRow
        '        iMainRow.ID = "TRAsc" & intCountac
        '        iMainRow.Style.Add("display", "none")
        '        Dim iMainCellSiteName As New HtmlTableCell
        '        iMainCellSiteName.Width = "75%"
        '        iMainCellSiteName.Attributes.Add("class", "dashboard")
        '        iMainCellSiteName.Style.Add("align", "left")
        '        Dim iMainCellPrecentage As New HtmlTableCell
        '        iMainCellPrecentage.Width = "25%"
        '        iMainCellPrecentage.Style.Add("align", "right")
        '        iMainCellSiteName.InnerHtml = "<a href='Po/frmSiteDocStatus.aspx?id=" & dv(intCount)("site_no") & "'>" & dv(intCount)("site_no") & "</a>"
        '        iMainCellPrecentage.InnerHtml = dv(intCount)("Percentage")
        '        iMainRow.Cells.Add(iMainCellSiteName)
        '        iMainRow.Cells.Add(iMainCellPrecentage)
        '        iMainTable.Rows.Add(iMainRow)
        '    Else
        '        Exit For
        '    End If
        '    intCountac = intCountac + 1
        'Next intCount
        ''gap
        'Dim iMaingap As New HtmlTableRow
        'Dim iMainCellgap As New HtmlTableCell
        'iMainCellgap.ColSpan = 2
        'iMainCellgap.Attributes.Add("class", "hgap")
        'iMaingap.Cells.Add(iMainCellgap)
        'iMainTable.Rows.Add(iMaingap)
        'Dim iMainRowMore As New HtmlTableRow
        'iMainRowMore.Style.Add("background-image", "url(Images/gra1px.jpg)")
        'iMainRowMore.Style.Add("background-repeat", "repeat-x")
        'iMainRowMore.Attributes.Add("class", "dashboard")
        'iMainRowMore.Height = 33
        'Dim iMainCellMore As New HtmlTableCell
        'iMainCellMore.Width = "75%"
        'iMainCellMore.Attributes.Add("class", "dashboard")
        'iMainCellMore.InnerHtml = "<a href=# onclick=""SiteStatus(0)""><img src=""Images/arrow1.png"" id=""idUpArrow""  border=""0""/></a>"
        'iMainCellMore.Style.Add("align", "left")
        'Dim iMainCellMoreright As New HtmlTableCell
        'iMainCellMoreright.Width = "25%"
        'If (intCountac > 14) Then
        '    iMainCellMoreright.InnerHtml = "<a href=# onclick=popwindowDashBoard(6)> More </a>"
        'End If
        'iMainRowMore.Cells.Add(iMainCellMore)
        'iMainRowMore.Cells.Add(iMainCellMoreright)
        'iMainTable.Rows.Add(iMainRowMore)
        'TdStatus.Controls.Add(iMainTable)
        Dim iMainTable As New HtmlTable
        Dim dtStatus As New DataTable
        'dedy 091106
        dtStatus = objUtil.ExeQueryDT("exec uspEBastDone " & ConfigurationManager.AppSettings("BAUTID") & "," & ConfigurationManager.AppSettings("BASTID"), "sitedoc")
        Dim strBind As String = ""
        If dtStatus.Rows.Count > 1 Then
            For intCount As Integer = 0 To dtStatus.Rows.Count - 1
                If (dtStatus.Rows(intCount)("ECount") = 0) Then
                    strBind += "<div class=""hgap""></div>" & dtStatus.Rows(intCount)("Process").ToString & " ( " & dtStatus.Rows(intCount)("ECount").ToString & " )"
                Else
                    strBind += "<div class=""hgap""></div><a href=# onclick=popSitesDetails(" + intCount.ToString + ")> " & dtStatus.Rows(intCount)("Process").ToString & " ( " & dtStatus.Rows(intCount)("ECount").ToString & " ) </a>"
                End If
            Next
        End If
        TdStatus.InnerHtml = strBind
        Dim iMainTable1 As New HtmlTable
        'from here to end coomented by satish
        'Dim dtStatus1 As New DataTable
        'dtStatus1 = objUtil.ExeQueryDT("exec [uspSLAProcess]", "sitedoc")
        'Dim strBind1 As String = "", strComplete As String = "", strNotComplete As String = ""
        'If dtStatus1.Rows.Count > 1 Then
        '    If (dtStatus1.Rows(0)("ECount") = 0) Then
        '        strBind1 += "<div class=""hgap""></div> 15 Days ( " & dtStatus1.Rows(0)("ECount").ToString & " )"
        '    Else
        '        strBind1 += "<div class=""hgap""></div><a href=# onclick=popSitesSLA(" + dtStatus1.Rows(0)("Process").ToString + ")> 15 Days ( " & dtStatus1.Rows(0)("ECount").ToString & " ) </a>"
        '    End If
        '    If (dtStatus1.Rows(1)("ECount") = 0) Then
        '        strBind1 += "<div class=""hgap""></div> 30 Days ( " & dtStatus1.Rows(1)("ECount").ToString & " )"
        '    Else
        '        strBind1 += "<div class=""hgap""></div><a href=# onclick=popSitesSLA(" + dtStatus1.Rows(1)("Process").ToString + ")> 30 Days ( " & dtStatus1.Rows(1)("ECount").ToString & " ) </a>"
        '    End If
        '    'Complete
        '    If (dtStatus1.Rows(2)("ECount") = 0) Then
        '        strComplete += "<div class=""hgap""></div> < 30 Days  ( " & dtStatus1.Rows(2)("ECount").ToString & " )"
        '    Else
        '        strComplete += "<div class=""hgap""></div><a href=# onclick=popSitesSLA(" + dtStatus1.Rows(2)("Process").ToString + ")> < 30 Days ( " & dtStatus1.Rows(2)("ECount").ToString & " ) </a>"
        '    End If
        '    'If (dtStatus1.Rows(3)("ECount") = 0) Then
        '    '    strComplete += "<div class=""hgap""></div> < 60 Days  ( " & dtStatus1.Rows(3)("ECount").ToString & " )"
        '    'Else
        '    '    strComplete += "<div class=""hgap""></div><a href=# onclick=popSitesSLA(" + dtStatus1.Rows(3)("Process").ToString + ")> < 60 Days ( " & dtStatus1.Rows(3)("ECount").ToString & " ) </a>"
        '    'End If
        '    'Not Complete
        '    If (dtStatus1.Rows(4)("ECount") = 0) Then
        '        strNotComplete += "<div class=""hgap""></div> < 30 Days  ( " & dtStatus1.Rows(4)("ECount").ToString & " )"
        '    Else
        '        strNotComplete += "<div class=""hgap""></div><a href=# onclick=popSitesSLA(" + dtStatus1.Rows(4)("Process").ToString + ")> < 30 Days ( " & dtStatus1.Rows(4)("ECount").ToString & " ) </a>"
        '    End If
        '    If (dtStatus1.Rows(5)("ECount") = 0) Then
        '        strNotComplete += "<div class=""hgap""></div> > 30 Days  ( " & dtStatus1.Rows(5)("ECount").ToString & " )"
        '    Else
        '        strNotComplete += "<div class=""hgap""></div><a href=# onclick=popSitesSLA(" + dtStatus1.Rows(5)("Process").ToString + ")> > 30 Days ( " & dtStatus1.Rows(5)("ECount").ToString & " ) </a>"
        '    End If
        'End If
        'TdStatusNew.InnerHtml = strBind1
        'TdComplete.InnerHtml = strComplete
        'TdNotComplete.InnerHtml = strNotComplete
    End Sub
    Sub CreateBaut()
        Dim iMainTable As New HtmlTable
        Dim dtStatus As New DataTable
        dtStatus = objBO.GetBautAndBast(CommonSite.RollId())
        Dim strBind As String = ""
        If dtStatus.Columns.Count > 1 Then
            For iLoop As Integer = 0 To dtStatus.Rows.Count - 1
                If (dtStatus.Rows(iLoop)("DisType").ToString().ToLower() = "baut") Then
                    If (dtStatus.Rows(iLoop)("TotalCount") > 0) Then
                        'strBind += "<div class=""hgap""></div><a href='DashBoard/dashboardpopupbaut.aspx'> Ready for Baut ( " & dtStatus.Rows(iLoop)("TotalCount") & " ) </a><br></br>"
                    End If
                    'strBind += "<div class=""hgap""></div><a href=# onclick=popwindowDashBoard(2)> Ready for Baut ( " & dtStatus.Rows(iLoop)("TotalCount") & " ) </a><br></br>"
                Else
                    If (dtStatus.Rows(iLoop)("TotalCount") > 0) Then
                        strBind += "<div class=""hgap""></div><a href='DashBoard/dashboardpopupbast.aspx' > Ready for Bast ( " & dtStatus.Rows(iLoop)("TotalCount") & " ) </a><br></br>"
                    End If

                End If
            Next
        End If
        dtStatus = objBO.uspDashBoardBautHome(CommonSite.GetDashBoardLevel(), CommonSite.UserId())
        If dtStatus.Rows.Count > 0 Then
            strBind += "<div class=""hgap""></div><a href=# onclick=popwindowDashBoard(2)> Near End ( " & dtStatus.Rows.Count & " ) </a>"
        End If
        TdBaut.InnerHtml = strBind
    End Sub
    Sub CreateAgenda()
        Dim iMainTable As New HtmlTable
        iMainTable.Width = "100%"
        Dim dtTask As New DataTable
        dtTask = objBO.uspDashBoardAgendaTask(CommonSite.UserId())
        For Each dRowsStatus As DataRow In dtTask.Rows
            Dim iMainRow As New HtmlTableRow
            Dim iMainCellSiteName As New HtmlTableCell
            iMainCellSiteName.Width = "75%"
            iMainCellSiteName.Attributes.Add("class", "dashboard")
            iMainCellSiteName.Style.Add("align", "left")
            Dim iMainCellPrecentage As New HtmlTableCell
            iMainCellPrecentage.Width = "25%"
            iMainCellPrecentage.Attributes.Add("class", "dashboard")
            iMainCellPrecentage.Style.Add("align", "right")
            If dRowsStatus.Item("CountUsrType") > 0 Then
                If "Approver" = dRowsStatus.Item("usrtype") Then
                    iMainCellSiteName.InnerHtml = "<a href='DashBoard/frmDocApproved.aspx?id=" & dRowsStatus.Item("TSK_ID") & "'>Pending for Approval ( " & dRowsStatus.Item("CountUsrType") & " ) </a>"
                Else
                    iMainCellSiteName.InnerHtml = "<a href='DashBoard/frmDocApproved.aspx?id=" & dRowsStatus.Item("TSK_ID") & "'>Pending for Review & Approval ( " & dRowsStatus.Item("CountUsrType") & " ) </a>"
                End If
            End If
            iMainRow.Cells.Add(iMainCellSiteName)
            iMainRow.Cells.Add(iMainCellPrecentage)
            iMainTable.Rows.Add(iMainRow)
        Next
        Dim dtAgenda As New DataTable
        'bugfix101107
        'dtAgenda = objBO.uspDashBoardAgenda(CommonSite.GetDashBoardLevel(), CommonSite.UserId(), ConfigurationManager.AppSettings("WCTRBASTID"))
        strsql = "exec [uspDashBoardAgenda] " & CommonSite.GetDashBoardLevel().ToString & "," & CommonSite.UserId().ToString & "," & ConfigurationManager.AppSettings("WCTRBASTID").ToString & "," & Session("User_Id").ToString
        dtAgenda = objUtil.ExeQueryDT(strsql, "DashBoardAgenda")
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
            ElseIf dRowsStatus.Item("usrtype").ToString().ToLower() = "wc" Then
                If dRowsStatus.Item("CountUsrType") > 0 Then
                    iMainCellSiteName.InnerHtml = "<a href=DashBoard/DashboardpopupWCTRBast.aspx>Regenerate rejected WCTR ( " & dRowsStatus.Item("CountUsrType") & " ) </a>"
                End If
            Else
                If dRowsStatus.Item("CountUsrType") > 0 Then
                    'iMainCellSiteName.InnerHtml = "<a href=USR/frmUserList.aspx> Pending for SubCon ( " & dRowsStatus.Item("CountUsrType") & " )</a>"
                End If
            End If
            iMainRow.Cells.Add(iMainCellSiteName)
            iMainRow.Cells.Add(iMainCellPrecentage)
            iMainTable.Rows.Add(iMainRow)
        Next
        'gap
        Dim iMaingap As New HtmlTableRow
        Dim iMainCellgap As New HtmlTableCell
        iMainCellgap.ColSpan = 2
        iMainCellgap.Attributes.Add("class", "hgap")
        iMaingap.Cells.Add(iMainCellgap)
        iMainTable.Rows.Add(iMaingap)
        tdAgenda.Controls.Add(iMainTable)
    End Sub
End Class
