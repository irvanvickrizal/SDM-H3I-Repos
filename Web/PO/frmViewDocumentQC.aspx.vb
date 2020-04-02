Imports System
Imports BusinessLogic
Imports System.Data
Imports System.IO

Partial Class PO_frmViewDocumentQC
    Inherits System.Web.UI.Page
    Dim strpath As String
    Dim dt As DataTable
    Dim dtDocAdditional As DataTable
    Dim objbo As New BODashBoard
    Dim objBODocs As New BOSiteDocs
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        If Page.IsPostBack = False Then
            dt = objbo.DigitalSign(Convert.ToInt32(Request.QueryString("id")), "1")
            BindAdditionalDocument()
            strpath = dt.Rows(0)("docpath").ToString()
            docView.Attributes.Add("src", ConfigurationManager.AppSettings("Vpath") + strpath.Replace("\", "/"))
        End If
        'bugfix100806 fixing missing signature after document sign
        If Request.QueryString("reloaded") <> "true" Then
            BindAdditionalDocument()
            Response.AddHeader("Refresh", "0;URL=" + Request.Url.ToString.Replace(Request.Url.ToString()(2).ToString, "www.telkomsel.nsnebast.com") + "&reloaded=true")
        End If
    End Sub
    Protected Sub btnRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        'bugfix100806 fixing missing signature after document sign
        Response.AddHeader("Refresh", "0;URL=" + Request.Url.ToString.Replace(Request.Url.ToString()(2).ToString, "www.telkomsel.nsnebast.com") + "&reloaded=true")
    End Sub

    Protected Sub LbtRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LbtRefresh.Click
        Response.AddHeader("Refresh", "0;URL=" + Request.Url.ToString.Replace(Request.Url.ToString()(2).ToString, "www.telkomsel.nsnebast.com") + "&reloaded=true")
    End Sub

    Protected Sub grddocuments_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdDocuments.RowDataBound
        'Select Case e.Row.RowType
        '    Case DataControlRowType.DataRow
        '        Dim lbl As Label = CType(e.Row.FindControl("lblNo"), Label)
        '        lbl.Text = (grdDocuments.PageIndex * grdDocuments.PageSize) + (e.Row.RowIndex + 1) & ".&nbsp;"
        'End Select
        'If e.Row.RowType = DataControlRowType.DataRow Then
        '    Dim url As String = "../PO/frmViewDocument.aspx?id=" & e.Row.Cells(4).Text
        '    If e.Row.Cells(2).Text <> "" And e.Row.Cells(2).Text <> "&nbsp;" Then
        '        e.Row.Cells(3).Text = "<a href='#' onclick=""window.open('" & url & "','mywindow','status=yes,menubar=no,scrollbars=yes,resizable=yes,width=700px,height=600px')"">" & e.Row.Cells(3).Text & "</a>"
        '    Else
        '        e.Row.Cells(3).Text = e.Row.Cells(3).Text
        '    End If
        'End If
    End Sub

#Region "custom methods"
    Private Sub BindAdditionalDocument()
        dtDocAdditional = objBODocs.uspSiteTIDocList(Request.QueryString("id"))
        grdDocuments.DataSource = dtDocAdditional
        grdDocuments.DataBind()
        'grdDocuments.Columns(1).Visible = False
        'grdDocuments.Columns(2).Visible = False
        'grdDocuments.Columns(4).Visible = False
        'grdDocuments.Columns(5).Visible = False
    End Sub

#End Region

End Class
