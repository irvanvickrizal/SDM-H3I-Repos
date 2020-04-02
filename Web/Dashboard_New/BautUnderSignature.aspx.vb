Imports Common
Imports SyStem.IO
Imports System.Data

Partial Class Dashboard_New_BautUnderSignature
    Inherits System.Web.UI.Page

    Dim dt As DataTable
    Dim objutil As New DBUtil
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        If Page.IsPostBack = False Then
            BindData()
            LblHeaderBastUnderSignature.Text = "BAUT Under Tsel Signature"
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
                    LblHeaderBastUnderSignature.Text = "BAUT Under NSN Signature"
                    grdDB.Columns(12).Visible = False
                    grdDB.Columns(14).Visible = False
                    grdDB.Columns(16).Visible = False
                    grdDB.Columns(18).Visible = False
            End Select
        End If
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

    Protected Sub BtnExport_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnExport.Click
        ExportToExcel()
    End Sub

    Protected Sub btnRefresh_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnRefresh.Click
        BindData()
    End Sub

#Region "Custom Methods"
    Private Sub BindData()
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
            strsql = "Exec [uspEBASTDoneDetails] " + Request.QueryString("id") & "," & ConfigurationManager.AppSettings("BAUTID") & "," & ConfigurationManager.AppSettings("BASTID") & ",'" & Date.Now.ToString() & "','" & Date.Now.ToString() & "'"
        Else
            area = dtra.Rows(0).Item(0).ToString
            region = dtra.Rows(0).Item(1).ToString
            If region <> 0 Then
                'regional user
                strsql = "Exec [uspEBASTDoneDetailsNew2] " + Request.QueryString("id") & "," & ConfigurationManager.AppSettings("BAUTID") & "," & ConfigurationManager.AppSettings("BASTID") & "," & Session("User_Id") & ",'" & Date.Now.ToString() & "','" & Date.Now.ToString() & "'"
            ElseIf area <> 0 Then
                'area user
                strsql = "Exec [uspEBASTDoneDetailsNew3] " + Request.QueryString("id") & "," & ConfigurationManager.AppSettings("BAUTID") & "," & ConfigurationManager.AppSettings("BASTID") & "," & Session("User_Id") & ",'" & Date.Now.ToString & "','" & Date.Now.ToString() & "'"
            Else
                'national user
                strsql = "Exec [uspEBASTDoneDetails] " + Request.QueryString("id") & "," & ConfigurationManager.AppSettings("BAUTID") & "," & ConfigurationManager.AppSettings("BASTID") & ",'" & DateTime.Now & "','" & DateTime.Now & "'"
            End If
        End If
        dt = objutil.ExeQueryDT(strsql, "uspDashboard")
        grdDB.DataSource = dt
        grdDB.PageSize = Session("Page_size")
        grdDB.DataBind()
        GridView2.DataSource = dt
        GridView2.DataBind()
    End Sub

    Public Sub ExportToExcel()
        Dim tw As New StringWriter()
        Dim hw As New System.Web.UI.HtmlTextWriter(tw)
        Dim frm As HtmlForm = New HtmlForm()
        Dim filename As String = String.Empty
        If Request.QueryString("id").Equals("1") Then
            filename = "BAST_Tsel_Signature_" & DateTime.Now.Date.ToString() & ".xls"
        Else
            filename = "BAST_NSN_Signature_" & DateTime.Now.Date.ToString() & ".xls"
        End If
        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("content-disposition", "attachment;filename=" & filename)
        Response.Charset = ""
        EnableViewState = False
        Controls.Add(frm)
        frm.Controls.Add(GridView2)
        frm.RenderControl(hw)
        Response.Write(tw.ToString())
        Response.End()
    End Sub
#End Region


End Class
