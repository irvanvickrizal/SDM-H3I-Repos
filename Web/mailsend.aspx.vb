Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Net
Imports System.IO
Imports Common
Partial Class mailsend
    Inherits System.Web.UI.Page
    Dim conobj As New SqlConnection
    Dim cmd As New SqlCommand
    Dim kk As New System.Diagnostics.EventLog
    Dim i, j, m, y, k, grconst As Integer
    Dim uname As String
    Dim phone As String
    Dim msgdata As New Text.StringBuilder
    Dim dt1, dt2 As New DataTable
    Dim objutil As New DBUtil
    Dim dt3 As New DataTable
    Dim objmail As New TakeMail
    Dim url As String
    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Response.Redirect("mailsendScheduler1.aspx?rdx=0")
    End Sub
    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        objmail.sendMailRejection()
        ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScriptdd", "alert('Rejection alert send successfully');", True)
    End Sub
End Class
