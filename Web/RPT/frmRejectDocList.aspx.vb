Imports BusinessLogic
Imports Common
Imports Entities
Imports System.Data

Partial Class frmRejectDocList
    Inherits System.Web.UI.Page
    Dim dt As New DataTable
    Dim objBO As New BOPOErrLog
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Page_size") Is Nothing Then Response.Redirect("~\SessionTimeout.aspx")
        If Not IsPostBack Then
            GetList()
        End If
    End Sub
    Sub GetList()
        dt = objBO.uspRejectDocL()
        grdDocuments.PageSize = Session("Page_size")
        grdDocuments.DataSource = dt
        grdDocuments.DataBind()
        grdDocuments.Columns(1).Visible = False
    End Sub

    Protected Sub grdDocuments_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdDocuments.RowDataBound
        Dim url As String
        Select Case e.Row.RowType
            Case DataControlRowType.DataRow
                Dim lbl As Label = CType(e.Row.Cells(0).FindControl("lblNo"), Label)
                lbl.Text = grdDocuments.PageIndex * grdDocuments.PageSize + (e.Row.RowIndex + 1) & ".&nbsp;"
                e.Row.Cells(6).Text = e.Row.Cells(6).Text.Replace(" &lt;br/&gt;", " - ")
                If e.Row.Cells(2).Text <> "" Then
                    url = "../PO/frmViewDocument.aspx?id=" & e.Row.Cells(1).Text
                    e.Row.Cells(2).Text = "<a href='#' onclick=""window.open('" & url & "','mywindow','status=yes,menubar=no,scrollbars=yes,resizable=yes,width=600px,height=600px')"">" & e.Row.Cells(2).Text & "</a>"
                End If
        End Select
    End Sub
End Class
