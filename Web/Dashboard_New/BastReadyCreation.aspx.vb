Imports System.Data
Imports Common
Imports BusinessLogic

Partial Class Dashboard_New_BastReadyCreation
    Inherits System.Web.UI.Page
    Dim objutil As New DBUtil
    Dim objBO As New BODashBoard

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        BindData()
    End Sub

    Protected Sub BtnDashboard_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnDashboard.Click
        Response.Redirect("../frmdashboard_Temp.aspx")
    End Sub

    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        Return
    End Sub

    Protected Sub grdDocuments_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdDocuments.PageIndexChanging
        grdDocuments.PageIndex = e.NewPageIndex
        grdDocuments.DataBind()
    End Sub

#Region "Custom Methods"
    Private Sub BindData()
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
#End Region

End Class
