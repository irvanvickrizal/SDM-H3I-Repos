Imports BusinessLogic
Imports Common
Imports System.Data
Partial Class DashboardpopupBastNew
    Inherits System.Web.UI.Page
    Dim objutil As New DBUtil
    Dim objBO As New BODashBoard
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CreateBast()
    End Sub
    Sub CreateBast()
        Dim strsql As String = "exec uspbastRCsitestatusdetails " & Session("User_Id") & ", '" & Session("lvlcode") & "','" & ConfigurationManager.AppSettings("BASTID") & "'"
        Dim dtStatus As New DataTable
        dtStatus = objutil.ExeQueryDT(strsql, "popbast")
        grddocuments.DataSource = dtStatus
        grddocuments.DataBind()
        If (CommonSite.UserType().ToLower() = "n") Then
            grddocuments.Columns(2).Visible = False
            grddocuments.Columns(3).Visible = True

        Else
            grddocuments.Columns(2).Visible = False
            grddocuments.Columns(3).Visible = True

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
