Imports BusinessLogic
Imports Common
Imports Entities
Imports System.Data
Partial Class WCC_frmWccDashBoard
    Inherits System.Web.UI.Page
    Dim objBOD As New BODDLs
    'Dim objBODP As New BOPDDLs
    Dim objET As New ETEBASTUsers
    Dim objBO As New BODashBoard
    Dim dt, dtdg As New DataTable
    Dim objUtil As New DBUtil
    Dim objdl As New BODDLs
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            If Session("User_Type") = "S" Then
            End If
            CreateAgenda()
        End If

    End Sub

    Sub CreateAgenda()
        Dim iMainTable As New HtmlTable
        iMainTable.Width = "100%"

        Dim dtTask As New DataTable
        dtTask = objBO.uspWccDashBoardAgendaTask(CommonSite.UserId())
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
                    iMainCellSiteName.InnerHtml = "<a href='frmWccDocApproved.aspx?id=" & dRowsStatus.Item("TSK_ID") & "'>Pending for Approval ( " & dRowsStatus.Item("CountUsrType") & " ) </a>"
                ElseIf "Reviewer" = dRowsStatus.Item("usrtype") Then
                    '  iMainCellSiteName.InnerHtml = "<a href='frmWccDocApproved.aspx?id=" & dRowsStatus.Item("TSK_ID") & "'>Pending for Review ( " & dRowsStatus.Item("CountUsrType") & " ) </a>"

                Else
                    iMainCellSiteName.InnerHtml = "<a href='frmWccDocApproved.aspx?id=" & dRowsStatus.Item("TSK_ID") & "'>Pending for Review & Approval ( " & dRowsStatus.Item("CountUsrType") & " ) </a>"

                End If
            End If

            iMainRow.Cells.Add(iMainCellSiteName)
            iMainRow.Cells.Add(iMainCellPrecentage)
            iMainTable.Rows.Add(iMainRow)
        Next
        'Dim dtAgenda As New DataTable
        ''dedy 091106
        'dtAgenda = objBO.uspDashBoardAgenda(CommonSite.GetDashBoardLevel(), CommonSite.UserId(), ConfigurationManager.AppSettings("WCTRBASTID"))
        'For Each dRowsStatus As DataRow In dtAgenda.Rows
        '    Dim iMainRow As New HtmlTableRow

        '    Dim iMainCellSiteName As New HtmlTableCell
        '    iMainCellSiteName.Width = "75%"
        '    iMainCellSiteName.Attributes.Add("class", "dashboard")
        '    iMainCellSiteName.Style.Add("align", "left")
        '    Dim iMainCellPrecentage As New HtmlTableCell
        '    iMainCellPrecentage.Width = "25%"
        '    iMainCellPrecentage.Attributes.Add("class", "dashboard")
        '    iMainCellPrecentage.Style.Add("align", "right")
        '    iMainRow.Cells.Add(iMainCellSiteName)
        '    iMainRow.Cells.Add(iMainCellPrecentage)
        '    iMainTable.Rows.Add(iMainRow)

        'Next
        ''gap
        'Dim iMaingap As New HtmlTableRow
        'Dim iMainCellgap As New HtmlTableCell
        'iMainCellgap.ColSpan = 2
        'iMainCellgap.Attributes.Add("class", "hgap")
        'iMaingap.Cells.Add(iMainCellgap)
        'iMainTable.Rows.Add(iMaingap)

        'tdAgenda.Controls.Add(iMainTable)
    End Sub

End Class
