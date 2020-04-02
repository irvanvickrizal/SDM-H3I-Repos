Imports BusinessLogic
Imports Common
Imports Entities
Imports System.Data
Partial Class WCC_frmWccDocApproved
    Inherits System.Web.UI.Page
    Dim objDL As New BODDLs
    Dim dt As New DataTable
    Dim objBo As New BODashBoard
    Dim objutil As New DBUtil
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            binddata()
        End If
    End Sub
    Sub binddata()
        grdDocuments.Columns(10).Visible = True
        grdDocuments.Columns(11).Visible = True
        dt = objBo.uspWccSiteDocListTask(CommonSite.UserId(), Convert.ToInt32(Request.QueryString("id")), hdnSort.Value)
        grdDocuments.DataSource = dt
        grdDocuments.DataBind()
        If grdDocuments.Rows.Count = 0 Then
            Dim clientScript As String = ""
            clientScript = "<script language='javascript'>"
            clientScript += "WindowsClose()"
            clientScript += "</script>"
            'ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", clientScript, False)
        End If

        grdDocuments.Columns(7).Visible = False
        grdDocuments.Columns(8).Visible = True

        grdDocuments.Columns(10).Visible = False
        grdDocuments.Columns(11).Visible = False

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
        'i = objBo.uspDocApproved(Convert.ToInt32(GrpId), CommonSite.UserId(), CommonSite.UserName(), CommonSite.RollId())
        i = objutil.ExeQueryScalar("Exec [uspDocApproved] " & Convert.ToInt32(Request.QueryString("id")) & "," & CommonSite.UserId() & ",'" & CommonSite.UserName() & "'," & CommonSite.RollId() & "," & ConfigurationManager.AppSettings("BAUTID") & "," & ConfigurationManager.AppSettings("BASTID") & "")
        If i = 0 Then
            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('Error while doing the transaction.');", True)
        ElseIf i = 1 Then

            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('Data Saved Sucessfully.');", True)
            Response.Redirect("frmWccDocApproved.aspx?" + Request.QueryString.ToString())
        End If
    End Sub

    Protected Sub grdDocuments_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdDocuments.RowDataBound
        Select Case e.Row.RowType
            Case DataControlRowType.DataRow
                Dim lbl As Label = CType(e.Row.FindControl("lblNo"), Label)
                lbl.Text = (grdDocuments.PageIndex * grdDocuments.PageSize) + (e.Row.RowIndex + 1) & ".&nbsp;"
        End Select
        If e.Row.RowType = DataControlRowType.DataRow Then
            ''Dim url As String = Replace(e.Row.Cells(2).Text, ConfigurationManager.AppSettings("Fpath"), ConfigurationManager.AppSettings("Vpath"))
            'Dim url As String = ConfigurationManager.AppSettings("Vpath") & e.Row.Cells(2).Text
            'url = url.Replace("\", "/")
            Dim url As String = "../PO/frmViewDocument.aspx?id=" & e.Row.Cells(10).Text
            If url.Length > 14 Then
                'e.Row.Cells(4).Text = "<a href='#' onclick=""window.showModalDialog('" & url & "','mywindow','status=yes,menubar=no,width=800,scrollbars=yes,resizable=yes')"">" & e.Row.Cells(3).Text & "</a>"
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
        Response.Redirect("frmWccdashboard.aspx")
    End Sub
End Class
