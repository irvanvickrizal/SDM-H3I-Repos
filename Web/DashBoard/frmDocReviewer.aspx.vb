Imports BusinessLogic
Imports Common
Imports Entities
Imports System.Data
Partial Class frmDocReviewer
    Inherits System.Web.UI.Page
    Dim objDL As New BODDLs
    Dim dt As New DataTable
    Dim objBo As New BODashBoard
    Dim objUtil As New DBUtil
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        If Not Page.IsPostBack Then
            binddata()
        End If
    End Sub
    Sub binddata()
        dt = objUtil.ExeQueryDT("exec [uspSiteDocListreviewerTask] " & CommonSite.UserId() & "," & Request.QueryString("id"), "GetRecord")
        grdDocuments.DataSource = dt
        grdDocuments.DataBind()
        If grdDocuments.Rows.Count = 0 Then
            Dim clientScript As String = ""
            clientScript = "<script language='javascript'>"
            clientScript += "WindowsClose()"
            clientScript += "</script>"
        End If
        grdDocuments.Columns(8).Visible = False
        grdDocuments.Columns(9).Visible = True
        grdDocuments.Columns(11).Visible = False
        grdDocuments.Columns(12).Visible = False
    End Sub
    Public Sub doapp(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim docid As Integer
        Dim tbtn As Button = CType(sender, Button)
        Dim row As GridViewRow = CType(tbtn.NamingContainer, GridViewRow)
        Dim i As Integer = row.RowIndex
        docid = grdDocuments.Rows(i).Cells(1).Text.ToString
    End Sub
    Public Sub dorej(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim docid As Integer
        Dim tbtn As Button = CType(sender, Button)
        Dim row As GridViewRow = CType(tbtn.NamingContainer, GridViewRow)
        Dim i As Integer = row.RowIndex
        docid = grdDocuments.Rows(i).Cells(1).Text.ToString
        Response.Write(docid)
    End Sub
    Protected Sub grdDocuments_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdDocuments.PageIndexChanging
        grdDocuments.PageIndex = e.NewPageIndex
        binddata()
    End Sub
    Protected Sub grdDocuments_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles grdDocuments.RowDeleting
        Dim GrpId As String = grdDocuments.DataKeys(e.RowIndex).Value().ToString()
        Dim i As Integer = -1
        'i = objBo.uspDocApproved(Convert.ToInt32(GrpId), CommonSite.UserId(), CommonSite.UserName(), CommonSite.RollId(), ConfigurationManager.AppSettings("BAUTID"), ConfigurationManager.AppSettings("BASTID"))
        i = objUtil.ExeQueryScalar("Exec [uspDocApproved] " & Convert.ToInt32(Request.QueryString("id")) & "," & CommonSite.UserId() & ",'" & CommonSite.UserName() & "'," & CommonSite.RollId() & "," & ConfigurationManager.AppSettings("BAUTID") & "," & ConfigurationManager.AppSettings("BASTID") & "")
        If i = 0 Then
            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('Error while doing the transaction.');", True)
        ElseIf i = 1 Then
            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('Data Saved Sucessfully.');", True)
            Response.Redirect("frmDocreviewer.aspx?" + Request.QueryString.ToString())
        End If
    End Sub
    Protected Sub grdDocuments_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdDocuments.RowDataBound
        Select Case e.Row.RowType
            Case DataControlRowType.DataRow
                Dim lbl As Label = CType(e.Row.FindControl("lblNo"), Label)
                lbl.Text = (grdDocuments.PageIndex * grdDocuments.PageSize) + (e.Row.RowIndex + 1) & ".&nbsp;"
        End Select
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim url As String = "../PO/frmViewDocument.aspx?id=" & e.Row.Cells(11).Text
            If url.Length > 14 Then
                e.Row.Cells(4).Text = "<a href='#' onclick=""window.open('" & url & "','mywindow','status=yes,menubar=no,scrollbars=yes,resizable=yes,width=700px,height=600px')"">" & e.Row.Cells(3).Text & "</a>"
            Else
                e.Row.Cells(4).Text = e.Row.Cells(3).Text
            End If
        End If
    End Sub
    Protected Sub grdDocuments_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdDocuments.RowCreated
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(1).Visible = False
            e.Row.Cells(2).Visible = False
            e.Row.Cells(3).Visible = False
        ElseIf e.Row.RowType = DataControlRowType.Header Then
            e.Row.Cells(1).Visible = False
            e.Row.Cells(2).Visible = False
            e.Row.Cells(3).Visible = False
        ElseIf e.Row.RowType = DataControlRowType.Footer Then
            e.Row.Cells(1).Visible = False
            e.Row.Cells(2).Visible = False
            e.Row.Cells(3).Visible = False
        End If
    End Sub
    Protected Sub BtnClose_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnClose.ServerClick
        Response.Redirect("../frmdashboard.aspx")
    End Sub
End Class
