Imports System.Data
Imports BusinessLogic
Imports Common
Partial Class DashBoard_frmagendaallscopes
    Inherits System.Web.UI.Page
    Dim objBO As New BODashBoard
    Dim objutil As New DBUtil
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack = True Then
            CreateAgenda()
        End If
    End Sub
    Sub CreateAgenda()
        Dim iMainTableCME As New HtmlTable
        Dim iMainTableSIS As New HtmlTable
        Dim iMainTableSITAC As New HtmlTable
        iMainTableCME.Width = "100%"
        iMainTableSIS.Width = "100%"
        iMainTableSITAC.Width = "100%"

        Dim dtTask As New DataTable
        dtTask = objutil.ExeQueryDT("exec uspDashBoardAgenda_task_4allscopes " & CommonSite.UserId() & "", "agenda")
        Dim dvcme As New DataView
        Dim dvsis As New DataView
        Dim dvsitac As New DataView
        dvcme = dtTask.DefaultView
        dvcme.RowFilter = "PROCESS= 'CME'"
        Dim j As Integer
        If dvcme.Count <> 0 Then
            For j = 0 To dvcme.Count - 1
                Dim iMainRow As New HtmlTableRow
                Dim iMainCellSiteName As New HtmlTableCell
                iMainCellSiteName.Width = "75%"
                iMainCellSiteName.Style.Add("align", "left")
                Dim iMainCellPrecentage As New HtmlTableCell
                iMainCellPrecentage.Width = "25%"
                iMainCellPrecentage.Style.Add("align", "right")

                If dvcme.Table.Rows(j).Item("CountUsrType") > 0 Then
                    If "Approver" = dvcme.Table.Rows(j).Item("usrtype") Then
                        iMainCellSiteName.InnerHtml = "<a href='https://telkomsel.www.telkomsel.nsnebast.com/CME2G/DashBoard/frmDocApproved.aspx?fromscope=all&userid=" & CommonSite.UserId() & "&id=" & dvcme.Table.Rows(j).Item("TSK_ID") & "'>Pending for Approval ( " & dvcme.Table.Rows(j).Item("CountUsrType") & " ) </a>"
                    ElseIf "Reviewer" = dvcme.Table.Rows(j).Item("usrtype") Then
                        iMainCellSiteName.InnerHtml = "<a href='https://telkomsel.www.telkomsel.nsnebast.com/CME2G/DashBoard/frmDocApproved.aspx?fromscope=all&userid=" & CommonSite.UserId() & "&id=" & dvcme.Table.Rows(j).Item("TSK_ID") & "'>Pending for Review ( " & dvcme.Table.Rows(j).Item("CountUsrType") & " ) </a>"
                    Else
                        iMainCellSiteName.InnerHtml = "<a href='https://telkomsel.www.telkomsel.nsnebast.com/CME2G/DashBoard/frmDocApproved.aspx?fromscope=all&userid=" & CommonSite.UserId() & "&id=" & dvcme.Table.Rows(j).Item("TSK_ID") & "'>Pending for Review & Approval ( " & dvcme.Table.Rows(j).Item("CountUsrType") & " ) </a>"
                    End If
                End If
                iMainRow.Cells.Add(iMainCellSiteName)
                iMainRow.Cells.Add(iMainCellPrecentage)
                iMainTableCME.Rows.Add(iMainRow)
            Next
            tdAgendacme.Controls.Add(iMainTableCME)
        Else
            Dim lbl As New Label
            lbl.Text = "No Documents Pending"
            tdAgendacme.Controls.Add(lbl)

        End If




        dvsis = dtTask.DefaultView
        dvsis.RowFilter = "PROCESS= 'SIS'"
        Dim i As Integer
        If dvsis.Count <> 0 Then

            For i = 0 To dvsis.Count - 1
                Dim iMainRow As New HtmlTableRow
                Dim iMainCellSiteName As New HtmlTableCell
                iMainCellSiteName.Width = "75%"
                iMainCellSiteName.Style.Add("align", "left")
                Dim iMainCellPrecentage As New HtmlTableCell
                iMainCellPrecentage.Width = "25%"
                iMainCellPrecentage.Style.Add("align", "right")

                If dvsis.Table.Rows(i).Item("CountUsrType") > 0 Then
                    If "Approver" = dvsis.Table.Rows(i).Item("usrtype") Then
                        iMainCellSiteName.InnerHtml = "<a href='https://telkomsel.www.telkomsel.nsnebast.com/SIS2G/DashBoard/frmDocApproved.aspx?fromscope=all&userid=" & CommonSite.UserId() & "&id=" & dvsis.Table.Rows(i).Item("TSK_ID") & "'>Pending for Approval ( " & dvsis.Table.Rows(i).Item("CountUsrType") & " ) </a>"

                    ElseIf "Reviewer" = dvsis.Table.Rows(i).Item("usrtype") Then
                        iMainCellSiteName.InnerHtml = "<a href='https://telkomsel.www.telkomsel.nsnebast.com/SIS2G/DashBoard/frmDocReviewer.aspx?fromscope=all&userid=" & CommonSite.UserId() & "&id=" & dvsis.Table.Rows(i).Item("TSK_ID") & "'>Pending for Review ( " & dvsis.Table.Rows(i).Item("CountUsrType") & " ) </a>"

                    Else
                        iMainCellSiteName.InnerHtml = "<a href='https://telkomsel.www.telkomsel.nsnebast.com/SIS2G/DashBoard/frmDocApproved.aspx?fromscope=all&userid=" & CommonSite.UserId() & "&id=" & dvsis.Table.Rows(i).Item("TSK_ID") & "'>Pending for Review & Approval ( " & dvsis.Table.Rows(i).Item("CountUsrType") & " ) </a>"

                    End If
                End If
                iMainRow.Cells.Add(iMainCellSiteName)
                iMainRow.Cells.Add(iMainCellPrecentage)
                iMainTableSIS.Rows.Add(iMainRow)
            Next

            tdAgendasis.Controls.Add(iMainTableSIS)
        Else
            Dim lbl As New Label
            lbl.Text = "No Documents Pending"
            tdAgendasis.Controls.Add(lbl)

        End If
        dvsitac = dtTask.DefaultView
        dvsitac.RowFilter = "PROCESS= 'SITAC'"
        Dim k As Integer
        If dvsitac.Count <> 0 Then

            For k = 0 To dvsitac.Count - 1
                Dim iMainRow As New HtmlTableRow
                Dim iMainCellSiteName As New HtmlTableCell
                iMainCellSiteName.Width = "75%"
                iMainCellSiteName.Style.Add("align", "left")
                Dim iMainCellPrecentage As New HtmlTableCell
                iMainCellPrecentage.Width = "25%"
                iMainCellPrecentage.Style.Add("align", "right")

                If dvsitac.Table.Rows(k).Item("CountUsrType") > 0 Then
                    If "Approver" = dvsitac.Table.Rows(k).Item("usrtype") Then
                        iMainCellSiteName.InnerHtml = "<a href='https://telkomsel.www.telkomsel.nsnebast.com/SIATC2G/DashBoard/frmDocApproved.aspx?fromscope=all&userid=" & CommonSite.UserId() & "&id=" & dvsitac.Table.Rows(k).Item("TSK_ID") & "'>Pending for Approval ( " & dvsitac.Table.Rows(k).Item("CountUsrType") & " ) </a>"
                    ElseIf "Reviewer" = dvsitac.Table.Rows(k).Item("usrtype") Then
                        iMainCellSiteName.InnerHtml = "<a href='https://telkomsel.www.telkomsel.nsnebast.com/SITAC2G/DashBoard/frmDocApproved.aspx?fromscope=all&userid=" & CommonSite.UserId() & "&id=" & dvsitac.Table.Rows(k).Item("TSK_ID") & "'>Pending for Review ( " & dvsitac.Table.Rows(k).Item("CountUsrType") & " ) </a>"
                    Else
                        iMainCellSiteName.InnerHtml = "<a href='https://telkomsel.www.telkomsel.nsnebast.com/SITAC2G/DashBoard/frmDocApproved.aspx?fromscope=all&userid=" & CommonSite.UserId() & "&id=" & dvsitac.Table.Rows(k).Item("TSK_ID") & "'>Pending for Review & Approval ( " & dvsitac.Table.Rows(k).Item("CountUsrType") & " ) </a>"
                    End If
                End If
                iMainRow.Cells.Add(iMainCellSiteName)
                iMainRow.Cells.Add(iMainCellPrecentage)
                iMainTableSITAC.Rows.Add(iMainRow)
            Next

            tdAgendasitac.Controls.Add(iMainTableSITAC)
        Else
            Dim lbl As New Label
            lbl.Text = "No Documents Pending"
            tdAgendasitac.Controls.Add(lbl)

        End If

    End Sub

End Class
