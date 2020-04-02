Imports BusinessLogic
Imports System.Data
Imports Common
Imports System.IO
Partial Class PO_frmPOView
    Inherits System.Web.UI.Page
    Dim objdl As New BODDLs
    Dim objtipoview As New BOPODetails
    Dim dt1 As DataTable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        If Not IsPostBack Then
            objdl.fillDDL(ddlPO, "PoNo", True, Constants._DDL_Default_None)
            BindGridView()
        End If
    End Sub
    Sub BindGridView()
        dt1 = objtipoview.GetTIPOViewData(ddlPO.SelectedItem.Value)
        btnExport.Visible = IIf(dt1.Rows.Count > 0, True, False)
        grdPOrawdata.DataSource = dt1
        grdPOrawdata.PageSize = Session("Page_size")
        grdPOrawdata.DataBind()
        grdExport.DataSource = dt1
        grdExport.DataBind()
    End Sub
    Protected Sub grdPOrawdata_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdPOrawdata.PageIndexChanging
        grdPOrawdata.PageIndex = e.NewPageIndex
        BindGridView()
    End Sub
    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        ExportToExcel()
    End Sub
    Public Sub ExportToExcel()
        grdExport.Visible = True
        Dim attachment As String = "attachment;filename=" + IIf(Me.ddlPO.SelectedIndex = 0, "PO-All.xls", ddlPO.SelectedValue + ".xls")
        Response.ClearContent()
        Response.AddHeader("content-disposition", attachment)
        Response.ContentType = "application/ms-excel"
        Dim sw As StringWriter = New StringWriter()
        Dim htw As HtmlTextWriter = New HtmlTextWriter(sw)
        Dim frm As HtmlForm = New HtmlForm()
        grdPOrawdata.Parent.Controls.Add(frm)
        frm.Attributes("runat") = "server"
        frm.Controls.Add(grdExport)
        frm.RenderControl(htw)
        Response.Write(sw.ToString())
        Response.End()
        grdExport.Visible = False
    End Sub
    Protected Sub ddlPO_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPO.SelectedIndexChanged
        BindGridView()
    End Sub
End Class
