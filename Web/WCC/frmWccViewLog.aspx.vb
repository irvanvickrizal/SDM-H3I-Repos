Imports Entities
Imports BusinessLogic
Imports DAO
Imports Common
Imports System.Data
Partial Class WCC_frmWccViewLog
    Inherits System.Web.UI.Page
    Dim objBO As New BoAuditTrail
    Dim objET As New ETAuditTrail
    Dim dt As DataTable
    Dim objdb As New dbutil

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Page_size") Is Nothing Then
            Response.Redirect("~/SessionTimeout.aspx")
        End If
        If Page.IsPostBack = False Then
            Response.Cache.SetNoStore()
            If Request.QueryString("id") <> "" Then
                BindData()
            End If
        End If
    End Sub
    Sub BindData()
        Dim PoNo As String
        If Not Request.QueryString("PONo") Is Nothing Then Session("PONo") = Request.QueryString("PONo")
        PoNo = Session("PoNo")
        Dim scope() As String = Request.QueryString("sid").Split("-")
        'dt = objBO.uspAuditTrailD(Request.QueryString("id"), scope(0), scope(1))
        'dt = objBO.uspWccAuditTrailD(Request.QueryString("id"), scope(0), Session("PoNo"), scope(1))
        dt = objdb.ExeQueryDT("Exec uspWccAuditTrailD " & Request.QueryString("id") & "," & scope(0) & ",'" & Session("PoNo") & "','" & scope(1) & "'", "dxy")
        If dt.Rows.Count > 0 Then
            tdpono.InnerText = dt.Rows(0).Item("PoNo").ToString
            tdsiteno.InnerText = dt.Rows(0).Item("Site_no").ToString
            tdsitename.InnerText = dt.Rows(0).Item("site_Name").ToString
            tdscope.InnerText = dt.Rows(0).Item("fldtype").ToString
            tdwpname.InnerText = dt.Rows(0).Item("workpackagename").ToString
            tdwpid.InnerText = dt.Rows(0).Item("workpkgid").ToString
            tddocument.InnerHtml = "<b>" & dt.Rows(0).Item("docname").ToString & "</b>"
        End If
        'Dim lst As New ArrayList

        Dim dt1 As New DataTable
        dt1 = New DataTable("Audit")
        Dim name As DataColumn = New DataColumn("AuditInfo")
        name.DataType = System.Type.GetType("System.String")
        Dim Uname As DataColumn = New DataColumn("User")
        Uname.DataType = System.Type.GetType("System.String")
        Dim uType As DataColumn = New DataColumn("UserType")
        uType.DataType = System.Type.GetType("System.String")
        Dim uRole As DataColumn = New DataColumn("UserRole")
        uRole.DataType = System.Type.GetType("System.String")
        Dim EventStart As DataColumn = New DataColumn("EventStart")
        EventStart.DataType = System.Type.GetType("System.String")
        Dim EventEnd As DataColumn = New DataColumn("EventEnd")
        EventEnd.DataType = System.Type.GetType("System.String")
        Dim remarks As DataColumn = New DataColumn("Remarks")
        remarks.DataType = System.Type.GetType("System.String")
        dt1.Columns.Add(name)
        dt1.Columns.Add(EventStart)
        dt1.Columns.Add(Uname)
        dt1.Columns.Add(uType)
        dt1.Columns.Add(uRole)
        dt1.Columns.Add(EventEnd)
        dt1.Columns.Add(remarks)
        Dim i As Integer
        'Dim strs As String = ""
        'Dim dstrs As String = ""
        For i = 0 To dt.Rows.Count - 1
            'strs = strs & dt.Rows(i).Item(7).ToString() & " : " & dt.Rows(i).Item(10).ToString() & " - Date :" & dt.Rows(i).Item(8).ToString() & "<BR/>"            
            Dim row1 As DataRow
            row1 = dt1.NewRow()
            row1.Item("AuditInfo") = dt.Rows(i).Item("Task").ToString()
            row1.Item("User") = dt.Rows(i).Item("User").ToString()
            row1.Item("UserType") = dt.Rows(i).Item("UserType").ToString()
            row1.Item("UserRole") = dt.Rows(i).Item("UserType").ToString()
            row1.Item("EventStart") = dt.Rows(i).Item("EventStart").ToString()
            row1.Item("EventEnd") = dt.Rows(i).Item("EventEnd").ToString
            row1.Item("Remarks") = dt.Rows(i).Item("Remarks").ToString
            dt1.Rows.Add(row1)
            'Dim row2 As DataRow
            'row2 = dt1.NewRow()
            'row2.Item("User") = dt.Rows(i).Item(10).ToString()
            'Dim row3 As DataRow
            'row3 = dt1.NewRow()
            'row3.Item("Date") = dt.Rows(i).Item(8).ToString()
            'dt1.Rows.Add(row2)
            'dt1.Rows.Add(row3)
            'lst.Add(strs)
        Next
        gvSearch.DataSource = dt1
        gvSearch.DataBind()


    End Sub

    Protected Sub gvSearch_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvSearch.RowDataBound
        Select Case e.Row.RowType
            Case DataControlRowType.DataRow
                Dim lbl As Label = CType(e.Row.Cells(0).FindControl("lblNo"), Label)
                lbl.Text = gvSearch.PageIndex * gvSearch.PageSize + (e.Row.RowIndex + 1) & ".&nbsp;"
        End Select
    End Sub
End Class
