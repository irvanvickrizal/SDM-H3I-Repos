Imports BusinessLogic
Imports Common
Imports Entities
Imports System.Data
Imports System.IO

Partial Class frmDocViewSubcon
    Inherits System.Web.UI.Page
    Dim objDL As New BODDLs
    Dim dt As New DataTable
    Dim objBo As New BOSiteDocs
    Dim objUtil As New DBUtil
    Dim poid As String
    Dim pono As String
    Dim scope As String
    Dim siteid As String
    Dim siteno As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Cache.SetNoStore()
        If Session("Page_size") Is Nothing Then
            Response.Redirect("~/SessionTimeout.aspx")
        End If
        ibExcelExportTop.Visible = False
    End Sub

    Sub binddata()
        grdDocuments.Columns(10).Visible = True
        grdDocuments.Columns(11).Visible = True
        'bugfix100623
        'dt = objBo.uspSiteDocList(siteid, pono, hdnSort.Value, scope)
        dt = objUtil.ExeQueryDT("exec uspSiteDocListSubcon " & siteid & ", '" & hdnSort.Value & "', '" & pono & "', '" & scope & "', " & txtSearch.Text & ", '" & Session("User_Login") & "'", "SiteDocListSubcon")
        grdDocuments.DataSource = dt
        grdDocuments.PageSize = Session("Page_size")
        grdDocuments.DataBind()
        grdDocuments.Columns(9).Visible = False
        grdDocuments.Columns(10).Visible = False
        grdDocuments.Columns(11).Visible = False
    End Sub

    Protected Sub grdDocuments_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdDocuments.RowCreated
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(1).Visible = False
            e.Row.Cells(2).Visible = False
            e.Row.Cells(3).Visible = False
        ElseIf e.Row.RowType = DataControlRowType.Header Then
            e.Row.Cells(1).Visible = False
            e.Row.Cells(2).Visible = False
            e.Row.Cells(3).Visible = False
        ElseIf e.Row.RowType = DataControlRowType.Footer Then
            e.Row.Cells(1).Visible = False
            e.Row.Cells(2).Visible = False
            e.Row.Cells(3).Visible = False
        End If
    End Sub

    Protected Sub grdDocuments_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdDocuments.RowDataBound
        Select Case e.Row.RowType
            Case DataControlRowType.DataRow
                Dim lbl As Label = CType(e.Row.FindControl("lblNo"), Label)
                lbl.Text = (grdDocuments.PageIndex * grdDocuments.PageSize) + (e.Row.RowIndex + 1) & ".&nbsp;"
        End Select
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim url As String = String.Empty
            Dim lblDocid As Label = CType(e.Row.FindControl("LblDocId"), Label)
            If lblDocid.Text = ConfigurationManager.AppSettings("ATP") Then
                url = "frmViewDocumentATP.aspx?id=" & e.Row.Cells(9).Text
            Else
                url = "frmViewDocument.aspx?id=" & e.Row.Cells(9).Text
            End If
            If e.Row.Cells(2).Text <> "" And e.Row.Cells(2).Text <> "&nbsp;" Then
                e.Row.Cells(4).Text = "<a href='#' onclick=""window.open('" & url & "','mywindow','fullscreen=yes')"">" & e.Row.Cells(3).Text & "</a>"
            Else
                e.Row.Cells(4).Text = e.Row.Cells(3).Text
            End If
            Dim url2 As String
            url2 = "frmviewlog.aspx?id=" & e.Row.Cells(1).Text & "&sid=" & siteid & "-" & scope & "-" & txtSearch.Text & "&pono=" & pono
            e.Row.Cells(8).Text = "<a href='#' onclick=""window.open('" & url2 & "','mywindow','status=yes,menubar=no,center=yes,scrollbars=yes,resizable=yes,width:1000px')"">" & " ViewLog" & "</a>"
        End If
    End Sub

    Protected Sub grdDocuments_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdDocuments.PageIndexChanging
        grdDocuments.PageIndex = e.NewPageIndex
        binddata()
    End Sub

    Protected Sub gvSearch_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvSearch.RowCreated
        If e.Row.RowType = DataControlRowType.Header Then
            Dim Pono, Siteno, SiteCDate, SiteDcDate, Scope As String
            Pono = Session("pono")
            Siteno = Session("SiteNO")
            SiteCDate = Session("SiteCRDate")
            SiteDcDate = Session("SiteDocDt")
            Scope = Session("Scope")
            Dim oGridView As GridView = DirectCast(sender, GridView)
            Dim oGridViewRow As New GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert)
            Dim oTableCell As New TableCell()
            oTableCell.HorizontalAlign = HorizontalAlign.Left
            oTableCell.Text = "Po No : " & Pono & " <BR>Site Creation Date :" & SiteCDate
            oTableCell.Font.Bold = True
            oTableCell.ColumnSpan = 3
            oGridViewRow.Cells().Add(oTableCell)
            oTableCell = New TableCell()
            oTableCell.HorizontalAlign = HorizontalAlign.Center
            oTableCell.Text = "Scope: " & Scope & ""
            oTableCell.Font.Bold = True
            oTableCell.ColumnSpan = 3
            oGridViewRow.Cells.Add(oTableCell)
            oGridViewRow.CssClass = "ms-formlabel GridEvenRows"
            oGridView.Controls(0).Controls.AddAt(0, oGridViewRow)
            oTableCell = New TableCell()
            oTableCell.HorizontalAlign = HorizontalAlign.Right
            oTableCell.Text = "Site: " & Siteno & "<BR>Site Doc Check Date :" & SiteDcDate
            oTableCell.Font.Bold = True
            oTableCell.ColumnSpan = 3
            oGridViewRow.Cells.Add(oTableCell)
            oGridViewRow.CssClass = "ms-formlabel GridEvenRows"
            oGridView.Controls(0).Controls.AddAt(0, oGridViewRow)
        End If
    End Sub

    Public Sub ExportToExcel()
        Dim tw As New StringWriter()
        Dim hw As New System.Web.UI.HtmlTextWriter(tw)
        Dim frm As HtmlForm = New HtmlForm()
        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("content-disposition", "attachment;filename=AuditLogInfo.xls")
        Response.Charset = ""
        EnableViewState = False
        Controls.Add(frm)
        frm.Controls.Add(gvSearch)
        frm.RenderControl(hw)
        Response.Write(tw.ToString())
        Response.End()
    End Sub

    Protected Sub ibExcelExportTop_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibExcelExportTop.Click
        ExportToExcel()
    End Sub

    Protected Sub gvSearch_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvSearch.RowDataBound
        Select Case e.Row.RowType
            Case DataControlRowType.DataRow
                Dim lbl As Label = CType(e.Row.Cells(0).FindControl("lblNo"), Label)
                lbl.Text = gvSearch.PageIndex * gvSearch.PageSize + (e.Row.RowIndex + 1) & ".&nbsp;"
        End Select
    End Sub

    Private Sub DownlaodFile(ByVal filename As String, ByVal forcedownload As Boolean)
        Dim ext As String = Path.GetExtension(filename)
        Dim type As String = ""
        If ext <> "" Then
            Select Case ext.ToLower()
                Case ".pdf"
                    type = "text/pdf"
                Case ".htm"
                    type = "text/HTML"
                Case ".txt"
                    type = "text/plain"
                Case ".tif"
                    type = "text/tif"
                Case ".doc"
                Case ".rtf"
                    type = "Application/msword"
                Case ".xls"
                    type = "text/xls"

            End Select
        End If
        If forcedownload Then
            Response.AppendHeader("content-disposition", "attachment; filename=" + filename)
        End If
        If type <> "" Then
            Response.WriteFile(filename)
            Response.End()
        End If
    End Sub

    Public Sub Download(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim lnk As LinkButton = CType(sender, LinkButton)
        Dim drow As GridViewRow = CType(lnk.NamingContainer, GridViewRow)
        Dim i As Integer
        i = drow.RowIndex
        Dim strPath As String = Trim(grdDocuments.Rows(i).Cells(2).Text)
        Dim fpath As String = ConfigurationManager.AppSettings("Fpath") & strPath
        If strPath <> "" Then
            If File.Exists(fpath) Then
                DownlaodFile(fpath, True)
            Else
                ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "Script", "alert('No File Exists With This Site No');", True)
            End If
        Else
            lnk.Enabled = False
        End If
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        poid = objUtil.ExeQueryScalarString("select po_id from podetails where workpkgid='" & txtSearch.Text & "'")
        pono = objUtil.ExeQueryScalarString("select pono from podetails where workpkgid='" & txtSearch.Text & "'")
        scope = objUtil.ExeQueryScalarString("select fldtype from podetails where workpkgid='" & txtSearch.Text & "'")
        siteid = objUtil.ExeQueryScalarString("select site_id from codsite where site_no in (select siteno from podetails where workpkgid='" & txtSearch.Text & "')")
        siteno = objUtil.ExeQueryScalarString("select siteno from podetails where workpkgid='" & txtSearch.Text & "'")

        If poid <> "" And pono <> "" And scope <> "" And siteid <> "" And siteno <> "" Then
            binddata()
        Else
            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "Script", "alert('Data cannot be found for the current WPID');", True)
        End If
    End Sub
End Class
