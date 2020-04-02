Imports BusinessLogic
Imports Common
Imports Entities
Imports System.Data

Partial Class RPT_frmPOErrLog
    Inherits System.Web.UI.Page
    Dim dt As New DataTable
    Dim objBO As New BOPOErrLog

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            getlist()
        End If
    End Sub

    Sub getlist()
        grdPOErrLogList.Columns(8).Visible = True
        dt = objBO.uspPOErrLogL()
        grdPOErrLogList.PageSize = Session("Page_size")
        grdPOErrLogList.DataSource = dt
        grdPOErrLogList.DataBind()
        grdPOErrLogList.Columns(8).Visible = False
    End Sub

    Protected Sub grdPOErrLogList_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdPOErrLogList.PageIndexChanging
        grdPOErrLogList.PageIndex = e.NewPageIndex
        getlist()
    End Sub

    Protected Sub grdPOErrLogList_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdPOErrLogList.RowDataBound
        Dim url As String
        Select Case e.Row.RowType
            Case DataControlRowType.DataRow
                Dim lbl As Label = CType(e.Row.Cells(0).FindControl("lblNo"), Label)
                lbl.Text = grdPOErrLogList.PageIndex * grdPOErrLogList.PageSize + (e.Row.RowIndex + 1) & ".&nbsp;"
                Dim k As String = ""
                Dim str() As String = Split(e.Row.Cells(1).Text, "^^^")
                k = str(0)
                If str.Length > 1 Then
                    e.Row.Cells(1).Text = str(1)
                End If
                If e.Row.Cells(3).Text <> 0 Then
                    'e.Row.Cells(3).Text = "<a href='..\PO\frmPOMissingInfo.aspx?id=" + k + "&Type=1'>" + e.Row.Cells(3).Text + "</a>"
                    url = "../PO/frmPOMissingInfo.aspx?id=" + k + "&Type=1"
                    e.Row.Cells(3).Text = "<a href='#' onclick=""window.open('" & url & "','mywindow','status=yes,menubar=no,scrollbars=yes,resizable=yes,width=600px,height=600px')"">" & e.Row.Cells(3).Text & "</a>"
                End If
                If e.Row.Cells(4).Text <> 0 Then
                    url = "../PO/frmPOMissingInfo.aspx?id=" + k + "&Type=2"
                    e.Row.Cells(4).Text = "<a href='#' onclick=""window.open('" & url & "','mywindow','status=yes,menubar=no,scrollbars=yes,resizable=yes,width=600px,height=600px')"">" & e.Row.Cells(4).Text & "</a>"
                End If
                If e.Row.Cells(5).Text <> 0 Then
                    url = "../PO/frmPOMissingInfo.aspx?id=" + k + "&Type=3"
                    e.Row.Cells(5).Text = "<a href='#' onclick=""window.open('" & url & "','mywindow','status=yes,menubar=no,scrollbars=yes,resizable=yes,width=600px,height=600px')"">" & e.Row.Cells(5).Text & "</a>"
                End If
                If e.Row.Cells(6).Text <> 0 Then
                    url = "../PO/frmPOMissingInfo.aspx?id=" + k + "&Type=4"
                    e.Row.Cells(6).Text = "<a href='#' onclick=""window.open('" & url & "','mywindow','status=yes,menubar=no,scrollbars=yes,resizable=yes,width=600px,height=600px')"">" & e.Row.Cells(6).Text & "</a>"
                End If
                If e.Row.Cells(7).Text <> 0 Then
                    url = "../PO/frmPOMissingInfo.aspx?id=" + k + "&Type=5"
                    e.Row.Cells(7).Text = "<a href='#' onclick=""window.open('" & url & "','mywindow','status=yes,menubar=no,scrollbars=yes,resizable=yes,width=600px,height=600px')"">" & e.Row.Cells(7).Text & "</a>"
                End If
        End Select
    End Sub
End Class
