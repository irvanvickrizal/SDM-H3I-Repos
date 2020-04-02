Imports BusinessLogic
Imports Common
Imports Entities
Imports System.Data
Partial Class PendingUploadDocument
    Inherits System.Web.UI.Page
    Dim objBO As New BODashBoard
    Dim sqlstr As String
    Dim objUtil As New DBUtil
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        If Request.QueryString("id") = 2 Then
            tdHead.InnerText = "Rejected Documents"
        End If
        CreateBaut()
    End Sub
    Sub CreateBaut()
        Dim iMainTable As New HtmlTable
        Dim dtStatus As New DataTable
        Dim cnt As Integer = 0
        'bugfix101107
        'dtStatus = objBO.uspDashBoardUploadDoc(CommonSite.GetDashBoardLevel(), CommonSite.UserId(), Request.QueryString("id").ToString())
        sqlstr = "exec [uspDashBoardUploadDoc] " & CommonSite.GetDashBoardLevel().ToString & "," & CommonSite.UserId().ToString & "," & Request.QueryString("id").ToString() & "," & Session("User_Id").ToString
        dtStatus = objutil.ExeQueryDT(sqlstr, "DashBoardUploadDoc")
        Dim iMainRow1 As New HtmlTableRow
        Dim iMainCellSiteNo1 As New HtmlTableCell
        iMainCellSiteNo1.Width = "5%"
        iMainCellSiteNo1.Style.Add("align", "left")
        iMainCellSiteNo1.Attributes.Add("class", "tableHeaders")
        iMainCellSiteNo1.InnerHtml = "No."
        iMainRow1.Cells.Add(iMainCellSiteNo1)
        Dim iMainCellSiteName1 As New HtmlTableCell
        iMainCellSiteName1.Width = "20%"
        iMainCellSiteName1.Style.Add("align", "left")
        iMainCellSiteName1.Attributes.Add("class", "tableHeaders")
        iMainCellSiteName1.InnerHtml = "Document Name"
        iMainRow1.Cells.Add(iMainCellSiteName1)
        Dim iMainCellPrecentage1 As New HtmlTableCell
        iMainCellPrecentage1.Width = "20%"
        iMainCellPrecentage1.Style.Add("align", "left")
        iMainCellPrecentage1.Attributes.Add("class", "tableHeaders")
        iMainCellPrecentage1.InnerHtml = "Site No"
        iMainRow1.Cells.Add(iMainCellPrecentage1)
        If Request.QueryString("id") = 2 Then
            Dim iMainCellPrecentage2 As New HtmlTableCell
            iMainCellPrecentage2.Width = "20%"
            iMainCellPrecentage2.Style.Add("align", "left")
            iMainCellPrecentage2.Attributes.Add("class", "tableHeaders")
            iMainCellPrecentage2.InnerHtml = "Remarks"
            iMainRow1.Cells.Add(iMainCellPrecentage2)
            Dim iMainCellPrecentage3 As New HtmlTableCell
            iMainCellPrecentage3.Width = "15%"
            iMainCellPrecentage3.Style.Add("align", "left")
            iMainCellPrecentage3.Attributes.Add("class", "tableHeaders")
            iMainCellPrecentage3.InnerHtml = "Work Package Id"
            iMainRow1.Cells.Add(iMainCellPrecentage3)
            Dim iMainCellPrecentage4 As New HtmlTableCell
            iMainCellPrecentage4.Width = "15%"
            iMainCellPrecentage4.Style.Add("align", "left")
            iMainCellPrecentage4.Attributes.Add("class", "tableHeaders")
            iMainCellPrecentage4.InnerHtml = "PO No"
            iMainRow1.Cells.Add(iMainCellPrecentage4)
            Dim iMainCellPrecentage5 As New HtmlTableCell
            iMainCellPrecentage5.Width = "15%"
            iMainCellPrecentage5.Style.Add("align", "left")
            iMainCellPrecentage5.Attributes.Add("class", "tableHeaders")
            iMainCellPrecentage5.InnerHtml = "Canceled Date"
            iMainRow1.Cells.Add(iMainCellPrecentage5)
        ElseIf Request.QueryString("id") = 0 Then
            Dim iMainCellPrecentage2 As New HtmlTableCell
            iMainCellPrecentage2.Width = "40%"
            iMainCellPrecentage2.Style.Add("align", "left")
            iMainCellPrecentage2.Attributes.Add("class", "tableHeaders")
            iMainCellPrecentage2.InnerHtml = "PoNo"
            iMainRow1.Cells.Add(iMainCellPrecentage2)
        End If
        iMainTable.Rows.Add(iMainRow1)
        For Each dRowsStatus As DataRow In dtStatus.Rows
            cnt = cnt + 1
            Dim iMainRow As New HtmlTableRow
            Dim iMainCellSiteNo As New HtmlTableCell
            iMainCellSiteNo.Width = "5%"
            iMainCellSiteNo.Style.Add("align", "left")
            iMainCellSiteNo.InnerHtml = cnt
            iMainRow.Cells.Add(iMainCellSiteNo)
            Dim iMainCellSiteName As New HtmlTableCell
            iMainCellSiteName.Width = "25%"
            iMainCellSiteName.Style.Add("align", "center")
            iMainCellSiteName.InnerHtml = dRowsStatus.Item("docname")
            iMainRow.Cells.Add(iMainCellSiteName)
            Dim iMainCellPrecentage As New HtmlTableCell
            iMainCellPrecentage.Width = "2%"
            iMainCellPrecentage.Style.Add("align", "center")
            iMainCellPrecentage.InnerHtml = dRowsStatus.Item("site_no")
            iMainRow.Cells.Add(iMainCellPrecentage)
            If Request.QueryString("id") = 2 Then
                Dim iMainCellPrecentage2 As New HtmlTableCell
                iMainCellPrecentage2.Width = "20%"
                iMainCellPrecentage2.Style.Add("align", "left")
                iMainCellPrecentage2.Style.Add("vertical-align", "top")
                iMainCellPrecentage2.InnerHtml = dRowsStatus.Item("remarks")
                iMainRow.Cells.Add(iMainCellPrecentage2)
                Dim iMainCellPrecentage3 As New HtmlTableCell
                iMainCellPrecentage3.Width = "15%"
                iMainCellPrecentage3.Style.Add("align", "left")
                iMainCellPrecentage3.Style.Add("vertical-align", "top")
                iMainCellPrecentage3.InnerHtml = dRowsStatus.Item("workpkgid")
                iMainRow.Cells.Add(iMainCellPrecentage3)
                Dim iMainCellPrecentage4 As New HtmlTableCell
                iMainCellPrecentage4.Width = "15%"
                iMainCellPrecentage4.Style.Add("align", "left")
                iMainCellPrecentage4.Style.Add("vertical-align", "top")
                iMainCellPrecentage4.InnerHtml = dRowsStatus.Item("pono")
                iMainRow.Cells.Add(iMainCellPrecentage4)
                Dim iMainCellPrecentage5 As New HtmlTableCell
                iMainCellPrecentage5.Width = "15%"
                iMainCellPrecentage5.Style.Add("align", "left")
                iMainCellPrecentage5.Style.Add("vertical-align", "top")
                iMainCellPrecentage5.InnerHtml = dRowsStatus.Item("rejectdate")
                iMainRow.Cells.Add(iMainCellPrecentage5)
            ElseIf Request.QueryString("id") = 0 Then
                Dim iMainCellPrecentage3 As New HtmlTableCell
                iMainCellPrecentage3.Width = "40%"
                iMainCellPrecentage3.Style.Add("align", "center")
                iMainCellPrecentage3.Style.Add("vertical-align", "top")
                iMainCellPrecentage3.InnerHtml = dRowsStatus.Item("pono")
                iMainRow.Cells.Add(iMainCellPrecentage3)
            End If
            iMainTable.Rows.Add(iMainRow)
        Next
        tdBast.Controls.Add(iMainTable)
    End Sub
End Class
