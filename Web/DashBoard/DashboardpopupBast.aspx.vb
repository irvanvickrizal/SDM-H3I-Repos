Imports BusinessLogic
Imports Common
Imports System.Data
Partial Class DashboardpopupBast
    Inherits System.Web.UI.Page
    Dim objutil As New DBUtil
    Dim objBO As New BODashBoard
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        CreateBaut()
    End Sub
    Sub CreateBaut()
        Dim iMainTable As New HtmlTable
        Dim strbastsql As String = ""
        Dim area As Integer
        Dim region As String
        Dim dtra As New DataTable
        Dim dtr As New DataTable
        Dim dta As New DataTable
        Dim rgn As String = ""
        Dim strsql As String = ""
        dtra = objutil.ExeQueryDT("select ara_id,rgn_id from ebastuserrole where usr_id=" & Session("User_Id") & "", "RA")
        If dtra.Rows.Count = 0 Then
            strbastsql = "exec uspDashBoardBastDigital " & ConfigurationManager.AppSettings("BASTID")
        Else
            area = dtra.Rows(0).Item(0).ToString
            region = dtra.Rows(0).Item(1).ToString
            If region <> 0 Then
                'region user
                strbastsql = "exec uspDashBoardBastDigitalNew2 " & ConfigurationManager.AppSettings("BASTID") & "," & Session("User_Id")
            ElseIf area <> 0 Then
                'area user
                strbastsql = "exec uspDashBoardBastDigitalNew3 " & ConfigurationManager.AppSettings("BASTID") & "," & Session("User_Id")
            Else
                'national user
                strbastsql = "exec uspDashBoardBastDigital " & ConfigurationManager.AppSettings("BASTID")
            End If
        End If
        Dim dtStatus As New DataTable
        dtStatus = objutil.ExeQueryDT(strbastsql, "popbast")
        grdDocuments.DataSource = dtStatus
        grdDocuments.DataBind()
        If (CommonSite.UserType().ToLower() = "n") Then
            grdDocuments.Columns(2).Visible = False
            grdDocuments.Columns(3).Visible = True
        Else
            grdDocuments.Columns(2).Visible = False
            grdDocuments.Columns(3).Visible = True
        End If
    End Sub
    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        Return
    End Sub
    Protected Sub grdDocuments_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdDocuments.RowDataBound
        Select Case e.Row.RowType
            Case DataControlRowType.DataRow
                'Dim lbl As Label = CType(e.Row.FindControl("lblNo"), Label)
                'lbl.Text = (grdDocuments.PageIndex * grdDocuments.PageSize) + (e.Row.RowIndex + 1) & ".&nbsp;"
                'Dim LblReadyBast As Label = CType(e.Row.FindControl("LblReadyBAST"), Label)
                'If (LblReadyBast IsNot Nothing) Then
                '    Dim docUploadedCount = Integer.Parse(LblReadyBast.Text)
                '    'Dim lblSiteNo As Label = CType(e.Row.FindControl("LblSiteNo"), Label)
                '    'Dim lblSiteNoDisabled As Label = CType(e.Row.FindControl("LblSiteNoDisabled"), Label)
                '    'Dim imgGreen As Image = CType(e.Row.FindControl("imgGreen"), Image)
                '    'Dim imgRed As Image = CType(e.Row.FindControl("imgRed"), Image)
                '    'If (docUploadedCount > 0) Then
                '    '    lblSiteNo.Visible = False
                '    '    'imgGreen.Visible = False
                '    '    'lblSiteNoDisabled.Visible = True
                '    '    'imgRed.Visible = True
                '    'Else
                '    '    lblSiteNo.Visible = True
                '    '    'imgGreen.Visible = True
                '    '    lblSiteNoDisabled.Visible = False
                '    '    'imgRed.Visible = False
                '    'End If
                'End If
        End Select
    End Sub
    Protected Sub BtnClose_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnClose.ServerClick
        Response.Redirect("../frmdashboard_Temp.aspx")
    End Sub
    Protected Sub grdDocuments_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdDocuments.PageIndexChanging
        grdDocuments.PageIndex = e.NewPageIndex
        grdDocuments.DataBind()
    End Sub
End Class
