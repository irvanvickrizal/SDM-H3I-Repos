Imports BusinessLogic
Imports System.Data
Imports Common
Partial Class PO_frmSiteReActivation
    Inherits System.Web.UI.Page
    Dim dt As DataTable
    Dim objbo As New BOPODetails
    Dim objdl As New BODDLs
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Page_size") Is Nothing Then Response.Redirect("~\SessionTimeout.aspx")
        If Page.IsPostBack = False Then
            objdl.fillDDL(ddlPO, "PODetails", False, Constants._DDL_Default_Select)
            If Not Request.QueryString("Id") Is Nothing Then
                Session("PONo") = Request.QueryString("Id")
                ddlPO.SelectedValue = Session("PONo")
            End If
            bindData()
        End If
    End Sub
    Sub bindData()
        dt = objbo.uspSiteList4Reactive(ddlPO.SelectedValue, ddlSelect.SelectedValue, txtSearch.Text.Replace("'", "''"))
        grdPOrawdata.DataSource = dt
        grdPOrawdata.PageSize = Session("Page_size")
        grdPOrawdata.DataBind()
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        bindData()
    End Sub

    Protected Sub ddlPO_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPO.SelectedIndexChanged
        Session("PONo") = ddlPO.SelectedValue
        bindData()
    End Sub

    Protected Sub grdPOrawdata_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdPOrawdata.PageIndexChanging
        grdPOrawdata.PageIndex = e.NewPageIndex
        bindData()
    End Sub

    Protected Sub grdPOrawdata_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdPOrawdata.RowCreated
        If e.Row.RowType = DataControlRowType.Header Or e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(1).Visible = False
        End If
    End Sub
    Sub go(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim lnk As LinkButton = CType(sender, LinkButton)
        Dim row As GridViewRow = CType(lnk.NamingContainer, GridViewRow)
        Dim sno As Integer
        sno = Int32.Parse(grdPOrawdata.Rows(row.RowIndex).Cells(1).Text)
        objbo.uspSiteActive(sno)
        Response.Redirect("frmSiteReActivation.aspx")
    End Sub

    Protected Sub grdPOrawdata_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdPOrawdata.RowDataBound
        Dim url As String
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lnk As LinkButton = CType(e.Row.Cells(10).FindControl("lnkActive"), LinkButton)
            lnk.Attributes.Add("onclick", "javascript:return " + "confirm('Are you sure to Active the Site?')")
            url = "ViewCancelSiteDetails.aspx?Sno=" + e.Row.Cells(1).Text
            e.Row.Cells(2).Text = "<a href='#' onclick=""window.open('" & url & "','mywindow','status=yes,menubar=no,scrollbars=yes,resizable=yes,width=600px,height=600px')"">" & e.Row.Cells(2).Text & "</a>"
        End If
    End Sub
End Class
