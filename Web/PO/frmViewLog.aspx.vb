Imports Entities
Imports BusinessLogic
Imports DAO
Imports Common
Imports System.Data
Imports System.Collections.Generic
Imports Common_NSNFramework
Imports PrintHelper
Imports System.Data.DataTable
Imports System.IO
Imports iTextSharp.text
Imports iTextSharp.text.pdf

Partial Class PO_frmviewlog
    Inherits System.Web.UI.Page
    Dim objBO As New BoAuditTrail
    Dim objET As New ETAuditTrail
    Dim dt As DataTable
    Dim objUtil As New DBUtil
    Dim PoNo As String
    Dim strSql As String
    Dim scope() As String
    Dim dtSiteAtt As New DataTable
    Dim dbutils_nsn As New DBUtils_NSN
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Page_size") Is Nothing Then
            Response.Redirect("~/SessionTimeout.aspx")
        End If
        If Page.IsPostBack = False Then
            BtnPrintVIewLog.Enabled = False
            BtnSaveViewLog.Enabled = False
            Response.Cache.SetNoStore()
            If Request.QueryString("id") <> "" Then
                If (Request.QueryString("id") = ConfigurationManager.AppSettings("ATP")) Then
                    'SetATPViewLog()
                    SetCommonViewLog()
                Else
                    SetCommonViewLog()
                End If

            End If
        End If
    End Sub

    Protected Sub BtnPrintViewLog_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs) Handles BtnPrintVIewLog.Click
        PrintViewLog()
    End Sub

    Protected Sub BtnSaveViewLog_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs) Handles BtnSaveViewLog.Click
        Dim scope() As String = Request.QueryString("sid").Split("-")
        SaveViewLog(scope(2))
    End Sub

    Protected Sub BtnExportToExcel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnExportToExcel.Click
        If gvSearch.Rows.Count() > 0 Then
            ExportToExcel()
        Else
            If (Not ClientScript.IsStartupScriptRegistered("alert")) Then
                Page.ClientScript.RegisterStartupScript _
                (Me.GetType(), "alert", "invalidExportToExcel();", True)
            End If
        End If
    End Sub

    Private Sub PrintViewLog()
        BtnPrintVIewLog.Visible = False
        BtnSaveViewLog.Visible = False
        PrintHelper.PrintWebControl(PnlPrint)
        Me.Dispose()
    End Sub

    Private Sub SaveViewLog(ByVal packageid As String)
        strSql = "select PoNo, SiteNo, cs.Site_Name sitename, fldtype, workpackagename,workpkgid,(select docname from coddoc where doc_id=" & ConfigurationManager.AppSettings("ATP") & ") docname from podetails po " & _
        "inner join codsite cs on cs.site_no = po.siteno where po.workpkgid='" & packageid & "'"

        dtSiteAtt = objUtil.ExeQueryDT(strSql, "AuditTrail")
        Dim doc As Document = New Document
        Dim strDocTime As String = String.Format("{0:ddMMyyyy-hhmmss}", DateTime.Now)
        Dim stringPath As String = ConfigurationManager.AppSettings("FpathViewLog2G") & "ViewLog_" & dtSiteAtt.Rows(0).Item("Siteno").ToString & "_" & strDocTime & ".pdf"

        PdfWriter.GetInstance(doc, New FileStream(stringPath, FileMode.Create))
        doc.Open()
        Dim tbInfo As New PdfPTable(3)
        Dim oCell As PdfPCell 
        Dim mainFont As New Font(Font.FontFamily.TIMES_ROMAN, 10, Font.BOLD)
        tbInfo.TotalWidth = 400.0F
        tbInfo.LockedWidth = True
        Dim widths() As Single = New Single() {4.5F, 0.3F, 10.0F}
        tbInfo.SetWidths(widths)
        tbInfo.HorizontalAlignment = 0
        tbInfo.SpacingBefore = 20.0F
        tbInfo.SpacingAfter = 30.0F
        tbInfo.DefaultCell.Border = 0


        tbInfo.AddCell("PONO")
        tbInfo.AddCell(":")
        oCell = New PdfPCell(New iTextSharp.text.Phrase(dtSiteAtt.Rows(0).Item("PoNo").ToString, FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.NORMAL)))
        oCell.Border = 0
        tbInfo.AddCell(oCell)

        tbInfo.AddCell("Siteno")
        tbInfo.AddCell(":")
        oCell = New PdfPCell(New iTextSharp.text.Phrase(dtSiteAtt.Rows(0).Item("Siteno").ToString, FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.NORMAL)))
        oCell.Border = 0
        tbInfo.AddCell(oCell)

        tbInfo.AddCell("Sitename")
        tbInfo.AddCell(":")
        oCell = New PdfPCell(New iTextSharp.text.Phrase(dtSiteAtt.Rows(0).Item("sitename").ToString, FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.NORMAL)))
        oCell.Border = 0
        tbInfo.AddCell(oCell)

        tbInfo.AddCell("Scope")
        tbInfo.AddCell(":")
        oCell = New PdfPCell(New iTextSharp.text.Phrase(dtSiteAtt.Rows(0).Item("fldtype").ToString, FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.NORMAL)))
        oCell.Border = 0
        tbInfo.AddCell(oCell)

        tbInfo.AddCell("Work Package name")
        tbInfo.AddCell(":")
        oCell = New PdfPCell(New iTextSharp.text.Phrase(dtSiteAtt.Rows(0).Item("workpackagename").ToString, FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.NORMAL)))
        oCell.Border = 0
        tbInfo.AddCell(oCell)


        tbInfo.AddCell("Package id")
        tbInfo.AddCell(":")
        oCell = New PdfPCell(New iTextSharp.text.Phrase(dtSiteAtt.Rows(0).Item("workpkgid").ToString, FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.NORMAL)))
        oCell.Border = 0
        tbInfo.AddCell(oCell)

        tbInfo.AddCell("Docname")
        tbInfo.AddCell(":")
        oCell = New PdfPCell(New iTextSharp.text.Phrase(dtSiteAtt.Rows(0).Item("docname").ToString, FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.NORMAL)))
        oCell.Border = 0
        tbInfo.AddCell(oCell)

        doc.Add(New Paragraph("View Log"))
        doc.Add(tbInfo)
        doc.Close()
        Response.Redirect("~/PilotSites/ViewLogPDF/" & "ViewLog_" & dtSiteAtt.Rows(0).Item("Siteno").ToString & "_" & strDocTime & ".pdf")
    End Sub

    Private Sub SetCommonViewLog()
        BindData()
        mvPanelViewLog.SetActiveView(vwCommonLog)
    End Sub

    Private Sub SetATPViewLog()
        If (Request.QueryString("sid") <> "") Then
            Dim scope() As String = Request.QueryString("sid").Split("-")
            BindLogATP(scope(2))
            mvPanelViewLog.SetActiveView(vwATPLog)
        End If
    End Sub

    Private Sub BindData()
        scope = Request.QueryString("sid").Split("-")
        If Not Request.QueryString("PONo") Is Nothing Then Session("PONo") = Request.QueryString("PONo")
        PoNo = Session("PoNo")
        'dt = objBO.uspAuditTrailD(Request.QueryString("id"), scope(0), Session("PoNo"), scope(1))
        strSql = "Exec HCPT_uspAuditTrailD " & Request.QueryString("id") & ",'" & scope(2) & "'"
        dt = objUtil.ExeQueryDT(strSql, "AuditTrail")
        If dt.Rows.Count > 0 Then
            tdpono.InnerText = dt.Rows(0).Item("PoNo").ToString
            tdsiteno.InnerText = dt.Rows(0).Item("Site_no").ToString
            tdsitename.InnerText = dt.Rows(0).Item("site_Name").ToString
            tdscope.InnerText = dt.Rows(0).Item("fldtype").ToString
            tdwpname.InnerText = dt.Rows(0).Item("workpackagename").ToString
            tdwpid.InnerText = dt.Rows(0).Item("workpkgid").ToString
            'tddocument.InnerHtml = "<b>" & dt.Rows(0).Item("docname").ToString & "</b>"
            LblDocument.Text = dt.Rows(0).Item("docname").ToString
        End If
        'Dim dt1 As New DataTable
        'dt1 = New DataTable("Audit")
        'Dim name As DataColumn = New DataColumn("AuditInfo")
        'name.DataType = System.Type.GetType("System.String")
        'Dim Uname As DataColumn = New DataColumn("User")
        'Uname.DataType = System.Type.GetType("System.String")
        'Dim uType As DataColumn = New DataColumn("UserType")
        'uType.DataType = System.Type.GetType("System.String")
        'Dim uRole As DataColumn = New DataColumn("UserRole")
        'uRole.DataType = System.Type.GetType("System.String")
        'Dim EventStart As DataColumn = New DataColumn("EventStart")
        'EventStart.DataType = System.Type.GetType("System.String")
        'Dim EventEnd As DataColumn = New DataColumn("EventEnd")
        'EventEnd.DataType = System.Type.GetType("System.String")
        'Dim remarks As DataColumn = New DataColumn("Remarks")
        'remarks.DataType = System.Type.GetType("System.String")
        'Dim categories As DataColumn = New DataColumn("Categories")
        'categories.DataType = System.Type.GetType("System.String")
        'dt1.Columns.Add(name)
        'dt1.Columns.Add(EventStart)
        'dt1.Columns.Add(Uname)
        'dt1.Columns.Add(uType)
        'dt1.Columns.Add(uRole)
        'dt1.Columns.Add(EventEnd)
        'dt1.Columns.Add(remarks)
        'dt1.Columns.Add(categories)
        'Dim i As Integer
        'For i = 0 To dt.Rows.Count - 1
        '    Dim row1 As DataRow
        '    row1 = dt1.NewRow()
        '    row1.Item("AuditInfo") = dt.Rows(i).Item("Task").ToString()
        '    row1.Item("User") = dt.Rows(i).Item("User").ToString()
        '    row1.Item("UserType") = dt.Rows(i).Item("UserType").ToString()
        '    row1.Item("UserRole") = dt.Rows(i).Item("UserRole").ToString()
        '    row1.Item("EventStart") = dt.Rows(i).Item("EventStartTime").ToString()
        '    row1.Item("EventEnd") = dt.Rows(i).Item("EventEndTime").ToString
        '    row1.Item("Remarks") = dt.Rows(i).Item("Remarks").ToString
        '    row1.Item("Categories") = dt.Rows(i).Item("Categories").ToString
        '    dt1.Rows.Add(row1)
        'Next
        If (Request.QueryString("id") IsNot Nothing) Then
            Dim docidLog As Integer = Integer.Parse(Request.QueryString("id"))
            If (docidLog = ConfigurationManager.AppSettings("ATP")) Then
                gvSearch.Columns(5).Visible = False
                gvSearch.Columns(6).Visible = False
                gvSearch.Columns(7).Visible = True
            Else
                gvSearch.Columns(5).Visible = True
                gvSearch.Columns(6).Visible = True
                gvSearch.Columns(7).Visible = False
            End If
        End If
        gvSearch.DataSource = dt
        gvSearch.DataBind()
    End Sub

    Private Sub BindLogATP(ByVal packageid As String)
        strSql = "select PoNo, SiteNo, (select top 1 sitename from epmdata where workpackageid = po.workpkgid order by lmdt desc)sitename, fldtype, workpackagename,workpkgid,(select docname from coddoc where doc_id=" & ConfigurationManager.AppSettings("ATP") & ") docname from podetails po " & _
        "inner join codsite cs on cs.site_no = po.siteno where po.workpkgid='" & packageid & "'"

        dtSiteAtt = objUtil.ExeQueryDT(strSql, "AuditTrail")
        tdpono.InnerText = dtSiteAtt.Rows(0).Item("PoNo").ToString
        tdsiteno.InnerText = dtSiteAtt.Rows(0).Item("Siteno").ToString
        tdsitename.InnerText = dtSiteAtt.Rows(0).Item("sitename").ToString
        tdscope.InnerText = dtSiteAtt.Rows(0).Item("fldtype").ToString
        tdwpname.InnerText = dtSiteAtt.Rows(0).Item("workpackagename").ToString
        tdwpid.InnerText = dtSiteAtt.Rows(0).Item("workpkgid").ToString
        'tddocument.InnerHtml = "<b>" & dtSiteAtt.Rows(0).Item("docname").ToString & "</b>"

        gvATPLog.DataSource = dbutils_nsn.GetAuditTrailATP(packageid, ConfigurationManager.AppSettings("ATP"))
        gvATPLog.DataBind()

        If (gvATPLog.Rows.Count > 0) Then
            BtnPrintVIewLog.Enabled = True
            BtnSaveViewLog.Enabled = True
            btnClose.Visible = False
        End If
    End Sub

    Private Sub ExportToExcel()
        Dim tw As New StringWriter()
        Dim hw As New System.Web.UI.HtmlTextWriter(tw)
        Dim frm As HtmlForm = New HtmlForm()
        Dim strFilename As String = "2G_" + LblDocument.Text + "_Log" + DateTime.Now.ToShortDateString + ".xls"
        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("content-disposition", "attachment;filename=" & strFilename)
        Response.Charset = ""
        EnableViewState = False
        Controls.Add(frm)
        gvSearch.AllowPaging = False
        bindData()
        frm.Controls.Add(gvSearch)
        frm.RenderControl(hw)
        Response.Write(tw.ToString())
        Response.End()
    End Sub

    Protected Sub gvSearch_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvSearch.RowDataBound
        Select Case e.Row.RowType
            Case DataControlRowType.DataRow
                Dim lbl As Label = CType(e.Row.Cells(0).FindControl("lblNo"), Label)
                lbl.Text = gvSearch.PageIndex * gvSearch.PageSize + (e.Row.RowIndex + 1) & ".&nbsp;"
                Dim lblDuration As Label = CType(e.Row.Cells(0).FindControl("LblDelays"), Label)
        End Select
    End Sub

    Protected Sub gvATPLog_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvATPLog.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            Dim lblDelay As Label = e.Row.FindControl("LblDelays")
            Dim lblStartDate As Label = e.Row.FindControl("LblStartTime")
            Dim lblEndTime As Label = e.Row.FindControl("LblEndTime")
            Dim lblTask As Label = e.Row.FindControl("LblTaskId")
            If (String.IsNullOrEmpty(lblStartDate.Text) = False And String.IsNullOrEmpty(lblEndTime.Text) = False) Then
                Dim startdate As DateTime = DateTime.ParseExact(e.Row.Cells(4).Text, "dd-MMM-yyyy", Nothing)
                Dim enddate As DateTime = DateTime.ParseExact(e.Row.Cells(5).Text, "dd-MMM-yyyy", Nothing)
                Dim days As Long
                'days = DateDiff("d", enddate, startdate)
                days = DateDiff("d", startdate, enddate)
                If days < 0 Then
                    lblDelay.Text = "0"
                Else
                    lblDelay.Text = days.ToString()
                End If
            End If
            If (String.IsNullOrEmpty(lblTask.Text) = False) Then
                If (lblTask.Text = ConfigurationManager.AppSettings("ATPOnSiteInvitDate")) Then
                    e.Row.BackColor = Drawing.ColorTranslator.FromHtml("#00A2E8")
                End If
            End If
        End If
    End Sub
End Class
