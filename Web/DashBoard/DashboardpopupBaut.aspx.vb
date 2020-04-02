Imports BusinessLogic
Imports Common
Imports System.Data
Partial Class DashboardpopupBaut
    Inherits System.Web.UI.Page
    Dim objutil As New DBUtil
    Dim objBO As New BODashBoard
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        CreateBaut()
    End Sub
    Sub CreateBaut()
        Dim strbautsql As String = ""
        Dim area As Integer
        Dim region As String
        Dim dtra As New DataTable
        Dim dtr As New DataTable
        Dim dta As New DataTable
        Dim rgn As String = ""
        Dim strsql As String = ""
        dtra = objutil.ExeQueryDT("select ara_id,rgn_id from ebastuserrole where usr_id=" & Session("User_Id") & "", "RA")
        If dtra.Rows.Count = 0 Then
            strbautsql = "exec uspDashBoardBautDigital " & ConfigurationManager.AppSettings("BAUTID")
        Else
            area = dtra.Rows(0).Item(0).ToString
            region = dtra.Rows(0).Item(1).ToString
            If region <> 0 Then
                'region user
                'bugfix101004 filter the site by user role cross match with site master
                strbautsql = "exec uspDashBoardBautDigitalNew2 " & ConfigurationManager.AppSettings("BAUTID") & "," & Session("User_Id")
            ElseIf area <> 0 Then
                'area user
                'bugfix101004 filter the site by user role cross match with site master
                strbautsql = "exec uspDashBoardBautDigitalNew2 " & ConfigurationManager.AppSettings("BAUTID") & "," & Session("User_Id")
            Else
                'national user
                strbautsql = "exec uspDashBoardBautDigital " & ConfigurationManager.AppSettings("BAUTID")
            End If
        End If
        Dim iMainTable As New HtmlTable
        Dim dtStatus As New DataTable
        dtStatus = objutil.ExeQueryDT(strbautsql, "popbaut")
        If (dtStatus.Rows.Count >= 1) Then
            grdDocuments.DataSource = dtStatus
            grdDocuments.DataBind()
            grdDocuments.Columns(2).Visible = True
            grdDocuments.Columns(3).Visible = False
        End If
    End Sub
    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        Return
    End Sub
    Protected Sub grdDocuments_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdDocuments.RowDataBound
        Select Case e.Row.RowType
            Case DataControlRowType.DataRow
                Dim lbl As Label = CType(e.Row.FindControl("lblNo"), Label)
                lbl.Text = (grdDocuments.PageIndex * grdDocuments.PageSize) + (e.Row.RowIndex + 1) & ".&nbsp;"
        End Select
    End Sub
    Protected Sub BtnClose_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnClose.ServerClick
        Response.Redirect("../frmdashboard.aspx")
    End Sub
    Protected Sub grdDocuments_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdDocuments.PageIndexChanging
        grdDocuments.PageIndex = e.NewPageIndex
        grdDocuments.DataBind()
    End Sub
End Class
