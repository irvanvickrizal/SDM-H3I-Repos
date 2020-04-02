Imports common
Imports System.Data
Imports System.IO
Partial Class EBastDoneDetails
    Inherits System.Web.UI.Page
    Dim dt As DataTable
    Dim objutil As New DBUtil
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If Session("Page_size") Is Nothing Then Response.Redirect("~/SessionTimeout.aspx")
        Dim dates As DataTable
        imgStart.Attributes.Add("onclick", "javascript:popUpCalendar(this,txtStart,'dd-mmm-yyyy');return false;")
        imgEnd.Attributes.Add("onclick", "javascript:popUpCalendar(this,txtEnd,'dd-mmm-yyyy');return false;")
        If Page.IsPostBack = False Then
            If Request.QueryString("date1") = "" Or Request.QueryString("date2") = "" Then
                dates = objutil.ExeQueryDT("select datename(dd,DATEADD(day,-30,GETDATE())) + '-' + datename(mm,DATEADD(day,-30,GETDATE())) + '-' + datename(year,DATEADD(day,-30,GETDATE())), datename(dd,GETDATE()) + '-' + datename(mm,GETDATE()) + '-' + datename(year,GETDATE()) ", "dates")
                txtStart.Value = dates.Rows(0).Item(0).ToString
                txtEnd.Value = dates.Rows(0).Item(1).ToString
            Else
                txtStart.Value = Request.QueryString("date1")
                txtEnd.Value = Request.QueryString("date2")
            End If
            If Session("User_Id") Is Nothing Then
            Else
                bindData()
            End If
            Select Case Request.QueryString("id")
                Case 2
                    grdDB.Columns(16).Visible = False
                    grdDB.Columns(18).Visible = False
                Case 3
                    grdDB.Columns(16).Visible = False
                    grdDB.Columns(18).Visible = False
                Case 4
                    grdDB.Columns(16).Visible = False
                    grdDB.Columns(18).Visible = False
                Case 5
                    grdDB.Columns(16).Visible = False
                    grdDB.Columns(18).Visible = False
                Case 6
                    grdDB.Columns(16).Visible = False
                    grdDB.Columns(18).Visible = False
                Case 7
                    grdDB.Columns(12).Visible = False
                    grdDB.Columns(14).Visible = False
                    grdDB.Columns(16).Visible = False
                    grdDB.Columns(18).Visible = False
            End Select
        End If
    End Sub
    Sub bindData()
        Dim area As Integer
        Dim region As String
        Dim dtra As New DataTable
        Dim dtr As New DataTable
        Dim dta As New DataTable
        Dim rgn As String = ""
        Dim i As Integer
        Dim strsql As String = ""
        dtra = objutil.ExeQueryDT("select ara_id,rgn_id from ebastuserrole where usr_id=" & Session("User_Id") & "", "RA")
        If dtra.Rows.Count = 0 Then
            strsql = "Exec [uspEBASTDoneDetails] " + Request.QueryString("id") & "," & ConfigurationManager.AppSettings("BAUTID") & "," & ConfigurationManager.AppSettings("BASTID") & ",'" & txtStart.Value.ToString & "','" & txtEnd.Value.ToString & "'"
        Else
            area = dtra.Rows(0).Item(0).ToString
            region = dtra.Rows(0).Item(1).ToString
            If region <> 0 Then
                'regional user
                strsql = "Exec [uspEBASTDoneDetailsNew2] " + Request.QueryString("id") & "," & ConfigurationManager.AppSettings("BAUTID") & "," & ConfigurationManager.AppSettings("BASTID") & "," & Session("User_Id") & ",'" & txtStart.Value.ToString & "','" & txtEnd.Value.ToString & "'"
            ElseIf area <> 0 Then
                'area user
                strsql = "Exec [uspEBASTDoneDetailsNew3] " + Request.QueryString("id") & "," & ConfigurationManager.AppSettings("BAUTID") & "," & ConfigurationManager.AppSettings("BASTID") & "," & Session("User_Id") & ",'" & txtStart.Value.ToString & "','" & txtEnd.Value.ToString & "'"
            Else
                'national user
                strsql = "Exec [uspEBASTDoneDetails] " + Request.QueryString("id") & "," & ConfigurationManager.AppSettings("BAUTID") & "," & ConfigurationManager.AppSettings("BASTID") & ",'" & txtStart.Value.ToString & "','" & txtEnd.Value.ToString & "'"
            End If
        End If
        dt = objutil.ExeQueryDT(strsql, "uspDashboard")
        grdDB.DataSource = dt
        grdDB.PageSize = Session("Page_size")
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
    Protected Sub btnRefresh_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRefresh.ServerClick
        If Session("webname") = "" Then
            Session("webname") = Split(Request.UrlReferrer.ToString, "/")(2).ToString
        End If
        If Split(Session("webname"), ":").Length > 1 Then
            Session("webname") = Split(Session("webname"), ":")(0)
        End If
        Dim url As String = "https://" & Session("webname").ToString & _
            Request.ServerVariables("URL") & "?id=" & Request.QueryString("id") & "&rw=" & _
            "&date1=" & txtStart.Value.ToString & "&date2=" & txtEnd.Value.ToString
        Response.Redirect(url)
    End Sub
End Class
