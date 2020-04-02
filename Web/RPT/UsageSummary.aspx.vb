Imports common
Imports System.Data
Imports System.IO
Partial Class UsageSummary
    Inherits System.Web.UI.Page
    Dim dt As DataTable
    Dim objutil As New DBUtil
    Dim strsql As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        btnStart.Attributes.Add("onclick", "javascript:popUpCalendar(this,txtStart,'dd-mmm-yyyy');return false;")
        btnEnd.Attributes.Add("onclick", "javascript:popUpCalendar(this,txtEnd,'dd-mmm-yyyy');return false;")
        'If Session("Page_size") Is Nothing Then
        '    Response.Redirect("~/SessionTimeout.aspx")
        'End If
    End Sub

    Sub bindData()
        strsql = "exec uspUsageSummary '" & txtStart.Value & "','" & txtEnd.Value & "'"
        dt = objutil.ExeQueryDT(strsql, "uspUsageSummary")
        grdDB.DataSource = dt
        'grdDB.PageSize = Session("Page_size")
        grdDB.DataBind()
        GridView2.DataSource = dt
        GridView2.DataBind()
    End Sub

    Protected Sub grdDB_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdDB.PageIndexChanging
        grdDB.PageIndex = e.NewPageIndex
        bindData()
    End Sub

    Protected Sub grdDB_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdDB.RowDataBound
        Select Case e.Row.RowType
            Case DataControlRowType.DataRow
                Dim lbl As Label = CType(e.Row.FindControl("lblNo"), Label)
                lbl.Text = (grdDB.PageIndex * grdDB.PageSize) + (e.Row.RowIndex + 1) & ".&nbsp;"
        End Select
    End Sub

    Protected Sub GridView2_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridView2.PageIndexChanging
        GridView2.PageIndex = e.NewPageIndex
        bindData()
    End Sub

    Protected Sub GridView2_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView2.RowDataBound
        Select Case e.Row.RowType
            Case DataControlRowType.DataRow
                Dim lbl As Label = CType(e.Row.FindControl("lblNo"), Label)
                lbl.Text = (GridView2.PageIndex * GridView2.PageSize) + (e.Row.RowIndex + 1) & ".&nbsp;"
        End Select
    End Sub

    Protected Sub btnExport_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.ServerClick
        ExportToExcel()
    End Sub

    Public Sub ExportToExcel()
        Dim tw As New StringWriter()
        Dim hw As New System.Web.UI.HtmlTextWriter(tw)
        Dim frm As HtmlForm = New HtmlForm()
        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("content-disposition", "attachment;filename=Milestone.xls")
        Response.Charset = ""
        EnableViewState = False
        Controls.Add(frm)
        frm.Controls.Add(GridView2)
        frm.RenderControl(hw)
        Response.Write(tw.ToString())
        Response.End()
    End Sub

    Protected Sub btnGenerate_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGenerate.ServerClick
        bindData()
    End Sub
End Class
