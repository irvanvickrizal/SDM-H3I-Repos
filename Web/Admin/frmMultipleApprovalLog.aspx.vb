Imports Common
Imports System.Data
Partial Class Admin_frmMultipleApprovalLog
    Inherits System.Web.UI.Page

    Dim objdb As New DBUtil
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            BindData()
        End If
    End Sub

#Region "Custom methods"
    Private Sub BindData()
        Dim dtLogs As DataTable = objdb.ExeQueryDT("exec uspMultipleApprovalProcessLog_Get", "logs")
        If dtLogs.Rows.Count > 0 Then
            GvMultipleApprovalogs.DataSource = dtLogs
            GvMultipleApprovalogs.DataBind()
        End If
    End Sub
#End Region

End Class
