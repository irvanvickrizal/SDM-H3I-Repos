Imports common
Imports System.Data
Imports System.IO
Partial Class EBastDoneDetails_fullinfo
    Inherits System.Web.UI.Page
    Dim dt As DataTable
    Dim objutil As New DBUtil
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Page_size") Is Nothing Then
            Response.Redirect("~/SessionTimeout.aspx")
        End If
        If Page.IsPostBack = False Then
            bindData()
            Select Case Request.QueryString("id")
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
            strsql = "Exec [uspEBASTDoneDetails] " + Request.QueryString("id") & "," & ConfigurationManager.AppSettings("BAUTID") & "," & ConfigurationManager.AppSettings("BASTID")
        Else
            area = dtra.Rows(0).Item(0).ToString
            region = dtra.Rows(0).Item(1).ToString
            If region <> 0 Then
                'region user
                'dtr = objutil.ExeQueryDT("select rgn_id,rgncode from codregion where rgn_id  in (select rgn_id from ebastuserrole where usr_id=" & Session("User_Id") & ")", "RA")
                'For i = 0 To dtra.Rows.Count - 1
                '    rgn = IIf(rgn <> "", rgn & "," & dtr.Rows(i).Item(1).ToString, dtr.Rows(i).Item(1).ToString)
                'Next
                'strsql = "Exec [uspEBASTDoneDetailsNew] " + Request.QueryString("id") & "," & ConfigurationManager.AppSettings("BAUTID") & "," & ConfigurationManager.AppSettings("BASTID") & ",'" & rgn & "'"
                'bugfix101004 filter the site by user role cross match with site master
                strsql = "Exec [uspEBASTDoneDetailsNew2] " + Request.QueryString("id") & "," & ConfigurationManager.AppSettings("BAUTID") & "," & ConfigurationManager.AppSettings("BASTID") & "," & Session("User_Id")
            ElseIf area <> 0 Then
                'area user
                'dta = objutil.ExeQueryDT("select rgn_id,rgncode from codregion where ara_id in (select ara_id from ebastuserrole where usr_id=" & Session("User_Id") & ")", "RA")
                'For i = 0 To dta.Rows.Count - 1
                '    rgn = IIf(rgn <> "", rgn & "," & dta.Rows(i).Item(1).ToString, dta.Rows(i).Item(1).ToString)
                'Next
                'strsql = "Exec [uspEBASTDoneDetailsNew] " + Request.QueryString("id") & "," & ConfigurationManager.AppSettings("BAUTID") & "," & ConfigurationManager.AppSettings("BASTID") & ",'" & rgn & "'"
                'bugfix101004 filter the site by user role cross match with site master
                strsql = "Exec [uspEBASTDoneDetailsNew2] " + Request.QueryString("id") & "," & ConfigurationManager.AppSettings("BAUTID") & "," & ConfigurationManager.AppSettings("BASTID") & "," & Session("User_Id")
            Else
                'national user
                strsql = "Exec [uspEBASTDoneDetails] " + Request.QueryString("id") & "," & ConfigurationManager.AppSettings("BAUTID") & "," & ConfigurationManager.AppSettings("BASTID")
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
End Class
