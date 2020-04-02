Imports BusinessLogic
Imports Common
Imports Entities
Imports System.Data
Imports System.IO

Partial Class Dashboard_New_RejectedDocument
    Inherits System.Web.UI.Page
    Dim objBO As New BODashBoard
    Dim sqlstr As String
    Dim objUtil As New DBUtil

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        If Page.IsPostBack = False Then
            BindData()
        End If
    End Sub

    Protected Sub BtnDashboard_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnDashboard.Click
        Response.Redirect("../frmDashboard_temp.aspx")
    End Sub

    Protected Sub BtnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnExport.Click
        ExportExcel()
    End Sub

#Region "Custom Methods"
    Private Sub BindData()
        Dim dtStatus As New DataTable
        Dim cnt As Integer = 0

        sqlstr = "exec [uspDashBoardUploadDoc] " & CommonSite.GetDashBoardLevel().ToString & "," & CommonSite.UserId().ToString & "," & Request.QueryString("id").ToString() & "," & Session("User_Id").ToString
        dtStatus = objUtil.ExeQueryDT(sqlstr, "DashBoardUploadDoc")

        GvRejectedDocuments.DataSource = dtStatus
        GvRejectedDocuments.DataBind()

    End Sub

    Private Sub ExportExcel()
        If GvRejectedDocuments.Rows.Count > 0 Then
            Dim tw As New StringWriter()
            Dim strFilename As String = "RejectedDocuments_" + DateTime.Now.ToShortDateString + ".xls"
            Dim hw As New System.Web.UI.HtmlTextWriter(tw)
            Dim frm As HtmlForm = New HtmlForm()
            Response.ContentType = "application/vnd.ms-excel"
            Response.AddHeader("content-disposition", "attachment;filename=" + strFilename)
            Response.Charset = ""
            EnableViewState = False
            Controls.Add(frm)
            frm.Controls.Add(GvRejectedDocuments)
            frm.RenderControl(hw)
            Response.Write(tw.ToString())
            Response.End()
        End If
        
    End Sub
    
#End Region

End Class
