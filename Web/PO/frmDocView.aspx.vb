Imports BusinessLogic
Imports Common
Imports Entities
Imports System.Data
Imports System.IO
Partial Class frmDocView
    Inherits System.Web.UI.Page
    Dim objDL As New BODDLs
    Dim dt As New DataTable
    Dim objBo As New BOSiteDocs
    Dim objutil As New DBUtil
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Cache.SetNoStore()
        If Session("Page_size") Is Nothing Then
            Response.Redirect("~/SessionTimeout.aspx")
        End If
        Button1.Attributes.Add("onclick", "javascript:return checkIsEmpty();")
        If Page.IsPostBack = False Then
            objDL.fillDDL(ddlPO1, "PONo", True, Constants._DDL_Default_Select)
        End If
        ibExcelExportTop.Visible = False
        Button1.Visible = True
    End Sub
    Sub binddata()
        Dim stval() As String = ddlSite.SelectedValue.Split("-")
        Dim scope() As String = ddlSite.SelectedItem.Text.Split("-")
        Session("wpid") = stval(2)
        'dt = objBo.uspSiteDocList(stval(0), ddlPO1.SelectedItem.Text, hdnSort.Value, scope(1))
        Dim roleid As Integer = CommonSite.RollId
        'dt = objBo.uspSiteDocList(stval(0), ddlPO1.SelectedItem.Text, hdnSort.Value, scope(1))
        dt = objutil.ExeQueryDT("Exec uspSiteDocList_new " & stval(0) & ",'" & hdnSort.Value & "','" & ddlPO1.SelectedItem.Text & "','" & scope(1) & "'," & stval(2) & "," & roleid, "SiteDocList")
        grdDocuments.Columns(9).Visible = True
        grdDocuments.Columns(10).Visible = True
        grdDocuments.Columns(11).Visible = True
        grdDocuments.DataSource = dt
        grdDocuments.PageSize = Session("Page_size")
        grdDocuments.DataBind()
        grdDocuments.Columns(9).Visible = False
        grdDocuments.Columns(10).Visible = False
        grdDocuments.Columns(11).Visible = False
    End Sub
    Protected Sub ddlSite_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSite.SelectedIndexChanged
        tblAudit.Visible = False
        ibExcelExportTop.Visible = False
        binddata()
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
            'Dim url As String = "frmViewDocument.aspx?id=" & e.Row.Cells(9).Text
            Dim LblDocId As Label = CType(e.Row.FindControl("LblDocId"), Label)
            Dim url As String = String.Empty
            If LblDocId.Text = ConfigurationManager.AppSettings("ATP") Then
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
            Dim stval() As String = ddlSite.SelectedItem.Value.Split("-")
            Dim scope() As String = ddlSite.SelectedItem.Text.Split("-")
            url2 = "frmViewLog.aspx?id=" & e.Row.Cells(1).Text & "&sid=" & stval(0) & "-" & scope(1) & "-" & Session("wpid")
            e.Row.Cells(8).Text = "<a href='#' onclick=""window.open('" & url2 & _
            "','mywindow','status=yes,menubar=no,center=yes,scrollbars=yes,resizable=yes,width:1000px')"">" & " ViewLog" & "</a>"
        End If
    End Sub

    Protected Sub grdDocuments_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdDocuments.PageIndexChanging
        grdDocuments.PageIndex = e.NewPageIndex
        binddata()
    End Sub

    Protected Sub ddlPO1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPO1.SelectedIndexChanged
        tblAudit.Visible = False
        ibExcelExportTop.Visible = False
        If ddlPO1.SelectedIndex > 0 Then
            'here we have to show only sites for which BAST process not finished.
            Dim lvlcode As String = Session("lvlcode")
            Dim area, region, zone, site As Integer
            area = Session("Area_Id")
            region = Session("Region_Id")
            zone = Session("Zone_Id")
            site = Session("Site_Id")
            Dim ddldt As DataTable
            ddldt = objBo.uspDDLPOSiteNoByUser1(ddlPO1.SelectedItem.Value, lvlcode, area, region, zone, site, Session("User_Id"))
            If ddldt.Rows.Count > 0 Then
                ddlSite.DataSource = ddldt
                ddlSite.DataTextField = "txt"
                ddlSite.DataValueField = "VAL"
                ddlSite.DataBind()
                ddlSite_SelectedIndexChanged(Nothing, Nothing)
            Else
                If site = 0 Then
                    ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "Script", "alert('Site information missing for this user');", True)
                    Me.ddlSite.Items.Clear()
                End If
            End If
        End If
        Session("PoNo") = ddlPO1.SelectedItem.Text
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        For i As Integer = 0 To ddlSite.Items.Count - 1
            If UCase(ddlSite.Items(i).Text).Contains(UCase(txtSearch.Text)) = True Then
                ddlSite.SelectedIndex = i '+ 1
                ddlSite_SelectedIndexChanged(Nothing, Nothing)
                Exit For
            End If
        Next
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim stval() As String = ddlSite.SelectedValue.Split("-")
        Dim scope() As String = ddlSite.SelectedItem.Text.Split("-")
        dt = objBo.uspSiteDocListInfo(ddlPO1.SelectedItem.Value, stval(0))
        If dt.Rows.Count > 0 Then
            Session("pono") = dt.Rows(0).Item("PoNo").ToString()
            Session("SiteNO") = dt.Rows(0).Item("Site_no").ToString()
            Session("SiteCRDate") = dt.Rows(0).Item("SDate").ToString()
            Session("SiteDocDt") = dt.Rows(0).Item("SDDate").ToString()
            Session("Scope") = dt.Rows(0).Item("Scope").ToString
            gvSearch.DataSource = dt
            gvSearch.DataBind()
            tblAudit.Visible = True
            ibExcelExportTop.Visible = True
        End If
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
End Class
