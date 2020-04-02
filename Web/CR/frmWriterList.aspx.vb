Imports BusinessLogic
Imports Common
Imports Entities
Imports System.Data
Partial Class CR_frmWriterList
    Inherits System.Web.UI.Page
    Dim dt As DataTable
    Dim objBo As New BOMOM
    Dim objdo As New ETMomUsers

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session("Page_size") Is Nothing Then
            Response.Write("<script language=""JavaScript"">window.close();</script>")
        End If
        'Response.Redirect("~/SessionTimeout.aspx")
        If Page.IsPostBack = False Then
            bindData()
        End If
        If Request.QueryString("id") > 0 And Request.QueryString("SelMode") = "True" Then
            Dim retVal As String = Request.QueryString("id") & "####" & Request.QueryString("SS")
            Response.Write("<script>window.returnValue = '" + retVal + "';window.close();</script>")
        End If
    End Sub
    Sub bindData()
        dt = objBo.uspGetUsers(Constants._UsrType, "N", hdnsort.Value)
        grdWriter.DataSource = dt
        grdWriter.PageSize = Session("Page_size")

        grdWriter.DataBind()
        If Request.QueryString("SelMode") = "true" Then
            grdWriter.Columns(1).Visible = False
            grdWriter.Columns(2).Visible = True
        Else
            grdWriter.Columns(1).Visible = True
            grdWriter.Columns(2).Visible = False
        End If
    End Sub
    Protected Sub grdWriter_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdWriter.PageIndexChanging
        grdWriter.PageIndex = e.NewPageIndex()
        bindData()
    End Sub

    Protected Sub grdWriter_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdWriter.Sorting
        If hdnsort.Value = e.SortExpression & " Desc" Or hdnsort.Value = "" Then
            hdnsort.Value = e.SortExpression & " Asc"
        Else
            hdnsort.Value = e.SortExpression & " Desc"
        End If
        bindData()
    End Sub

    Protected Sub btnClose_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.ServerClick
        Response.Write("<script language='javascript'>window.close();</script>>")
    End Sub

    Protected Sub grdWriter_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdWriter.RowDataBound
        Select Case e.Row.RowType
            Case DataControlRowType.DataRow
                Dim lbl As Label = CType(e.Row.Cells(0).FindControl("lblNo"), Label)
                lbl.Text = grdWriter.PageIndex * grdWriter.PageSize + (e.Row.RowIndex + 1) & ".&nbsp;"
        End Select
    End Sub
End Class
