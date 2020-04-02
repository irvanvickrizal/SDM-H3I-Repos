
Imports BusinessLogic
Imports Common
Imports System.Data
Partial Class frmsitehelp
    Inherits System.Web.UI.Page
    Dim pono As String
    Dim dt As New DataTable
    Dim objbo As New BOPODetails
    Dim objdl As New BODDLs
    Dim objutil As New DBUtil
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Page_size") Is Nothing Then Response.Write("<script language=""JavaScript"">window.close();</script>")
        If Page.IsPostBack = False Then
            objdl.fillDDL(ddlPO, "PODetails", True, Constants._DDL_Default_Select)
        End If

    End Sub
    Sub bindData()
        grdPOrawdata.Columns(2).Visible = True
        'grdPOrawdata.Columns(1).Visible = True
        dt = objutil.ExeQueryDT("Exec uspPODetailsList '" & Session("pono") & "','" & ddlSelect.SelectedValue & "','" & txtSearch.Text.Replace("'", "''") & "'," & hdnDisp.Value & ",'" & hdnSort.Value & "'", "PoDetails")
        'dt = objbo.uspPODetailsList(Session("pono"), "siteno", "", hdnDisp.Value)
        grdPOrawdata.DataSource = dt
        grdPOrawdata.PageSize = Session("Page_size")
        grdPOrawdata.DataBind()
        grdPOrawdata.Columns(2).Visible = False
        'grdPOrawdata.Columns(1).Visible = False
    End Sub

    Protected Sub btnok_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnok.Click
        Dim POlist As String = String.Empty
        Dim POId As String = String.Empty
        Dim i As Integer
        For i = 0 To grdPOrawdata.Rows.Count - 1
            Dim c As HtmlInputCheckBox = CType(grdPOrawdata.Rows(i).Cells(2).FindControl("chkId"), HtmlInputCheckBox)
            If c.Checked Then
                POId = grdPOrawdata.Rows(i).Cells(2).Text
                If POlist.Length = 0 Then
                    POlist = POId
                Else
                    POlist = POlist & "~" & POId
                End If
            End If
        Next
        'Response.Redirect("~\PO\frmSiteDocSetup.aspx?a=" + sitelist + "")
        If POlist <> "" Then
            Dim strPO As String = ddlPO.SelectedItem.Text.Replace(" ", "!~")
            POlist = POlist & "^" & strPO
            Response.Write("<script>window.opener.document.getElementById('hdnSites').value='" + POlist + "';window.opener.document.form1.submit();window.close();</script>")
        Else
            Response.Write("<script>alert('Please select the Sites..');</script>")
        End If
    End Sub

    Protected Sub grdPOrawdata_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdPOrawdata.PageIndexChanging
        grdPOrawdata.PageIndex = e.NewPageIndex
        If ddlPO.SelectedItem.Text = Session("pono") Then
            bindData()
        End If
    End Sub

    Protected Sub grdPOrawdata_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdPOrawdata.SelectedIndexChanged
        hdnDisp.Value = 1
        bindData()
    End Sub

    Protected Sub ddlPO_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPO.SelectedIndexChanged
        If ddlPO.SelectedIndex > 0 Then            
            Session("pono") = ddlPO.SelectedItem.Text
            bindData()
            btnok.Visible = True
        Else
            btnok.Visible = False
        End If
    End Sub
    Protected Sub grdPOrawdata_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdPOrawdata.RowCreated
        If e.Row.RowType = DataControlRowType.Header Then
            Dim GridViewCount As GridView = DirectCast(sender, GridView)
            Dim GridViewCountRow As New GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert)
            Dim TableCell1 As New TableCell()
            TableCell1.CssClass = "PageHeader"
            TableCell1.Text = "Total Record(s) : " & dt.Rows.Count.ToString()
            TableCell1.ColumnSpan = 7
            TableCell1.HorizontalAlign = HorizontalAlign.Left
            GridViewCountRow.Cells.Add(TableCell1)
            GridViewCount.Controls(0).Controls.AddAt(0, GridViewCountRow)
        End If
    End Sub
    Protected Sub grdPOrawdata_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdPOrawdata.Sorting
        If hdnSort.Value = "" Or hdnSort.Value = e.SortExpression & " Desc" Then
            hdnSort.Value = e.SortExpression & " Asc"
        Else
            hdnSort.Value = e.SortExpression & " Desc"
        End If
        bindData()
    End Sub
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        bindData()
    End Sub
End Class
