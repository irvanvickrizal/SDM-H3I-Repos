Imports BusinessLogic
Imports Common
Imports System.Data

Partial Class DashboardpopupBautNew
    Inherits System.Web.UI.Page
    Dim objutil As New DBUtil
    Dim objBO As New BODashBoard
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CreateBaut()
    End Sub
    Sub CreateBaut()
        Dim strsql As String = "exec uspbautRCsitestatusdetails " & Session("User_Id") & ", '" & Session("lvlcode") & "','" & ConfigurationManager.AppSettings("BAUTID") & "'"
        Dim dtStatus As New DataTable
        dtStatus = objutil.ExeQueryDT(strsql, "BAUTRC")
        If (dtStatus.Rows.Count >= 1) Then
            grddocuments.DataSource = dtStatus
            grddocuments.DataBind()
            grddocuments.Columns(2).Visible = True
            grddocuments.Columns(3).Visible = False
        End If

    End Sub
    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        Return
    End Sub

    Protected Sub grddocuments_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grddocuments.RowDataBound
        Select Case e.Row.RowType
            Case DataControlRowType.DataRow
                Dim lbl As Label = CType(e.Row.FindControl("lblNo"), Label)
                lbl.Text = (grddocuments.PageIndex * grddocuments.PageSize) + (e.Row.RowIndex + 1) & ".&nbsp;"
        End Select
    End Sub

    Protected Sub BtnClose_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnClose.ServerClick
        Response.Redirect("../frmdashboard.aspx")
    End Sub
    Protected Sub grddocuments_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grddocuments.PageIndexChanging
        grddocuments.PageIndex = e.NewPageIndex
        grddocuments.DataBind()
    End Sub
End Class
