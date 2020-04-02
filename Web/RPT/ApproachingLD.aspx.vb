Imports common
Imports System.Data
Imports System.IO
Partial Class ApproachingLD
    Inherits System.Web.UI.Page
    Dim dt As DataTable
    Dim objutil As New DBUtil
    Dim strsql As String = ""
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If Session("Page_size") Is Nothing Then
        '    Response.Redirect("~/SessionTimeout.aspx")
        'End If
        If Page.IsPostBack = False Then
            bindData()
        Else
            bindData2()
        End If
    End Sub
    Sub bindData()
        dt = objutil.ExeQueryDT("select rgnname [txt], rgn_id[val] from codregion", "region")
        ddlRegion.DataSource = dt
        ddlRegion.DataTextField = "txt"
        ddlRegion.DataValueField = "val"
        ddlRegion.DataBind()
    End Sub

    Sub bindData2()
        Session("pono") = ddlPONo.SelectedValue
        dt = objutil.ExeQueryDT("select distinct custpono [txt], custpono [val] from aproachingLDs where rgn_id=" & ddlRegion.SelectedValue, "podetails")
        ddlPONo.DataSource = dt
        ddlPONo.DataTextField = "txt"
        ddlPONo.DataValueField = "val"
        ddlPONo.DataBind()
        If Session("pono") <> "" Then
            dt = objutil.ExeQueryDT("select distinct custpono from aproachingLDs where rgn_id=" & ddlRegion.SelectedValue & " and custpono='" & Session("pono") & "'", "podetails")
            If dt.Rows.Count > 0 Then
                ddlPONo.SelectedValue = Session("pono")
                strsql = "exec uspApproachingLDs " & ddlType.SelectedValue & ",'" & Session("pono") & "'"
                dt = objutil.ExeQueryDT(strsql, "ApproachingLDs")
                grdDB.DataSource = dt
                'grdDB.PageSize = Session("Page_size")
                grdDB.DataBind()
                GridView2.DataSource = dt
                GridView2.DataBind()
            End If
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

    'Protected Sub ddlPONo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPONo.SelectedIndexChanged
    '    Response.Redirect("http://" & Request.ServerVariables("SERVER_NAME") & ":" & Request.ServerVariables("SERVER_PORT") & _
    '        Request.ServerVariables("URL").Replace("Sub", "") & _
    '        "?pono=" & Request("pono") & "&scope=" & Request("scope"))
    'End Sub
End Class
