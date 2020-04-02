Imports BusinessLogic
Imports Common
Imports Entities
Imports System.Data
Partial Class DashBoard_Reject
    Inherits System.Web.UI.Page
    Dim objBo As New BODashBoard
    Dim objBOM As New BOMailReport
    Dim objdb As New DBUtil
    Dim dt As New DataTable
    Dim dtn As New DataTable
    Dim objmail As New TakeMail
    Protected Sub BtnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSubmit.Click
        If txtRemarks.Text.Length <= 0 Then
            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('Please enter rejection reason!!!');", True)
        Else
            Dim GrpId As String = Request.QueryString("id")
            Dim i As Integer = -1
            'dedy 091106
            i = objBo.uspDocReject(Convert.ToInt32(GrpId), CommonSite.UserId(), CommonSite.UserName(), CommonSite.RollId(), txtRemarks.Text, ConfigurationManager.AppSettings("BAUTID"), ConfigurationManager.AppSettings("BASTID"), ConfigurationManager.AppSettings("WCTRBASTID"))
            If i = 0 Then
                ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('Error while doing the transaction.');", True)
            ElseIf i = 1 Then
                Try
                    objmail.sendMailReject(GrpId, CommonSite.UserId(), CommonSite.UserName(), txtRemarks.Text.ToString, ConfigurationManager.AppSettings("rejmailconst"), "Normal")
                Catch ex As Exception
                    objdb.ExeNonQuery("exec uspErrLog '', 'mailsendingreject','" & ex.Message.ToString.Replace("'", "''") & "','sendmailreject'")
                End Try
                Dim clientScript As String = ""
                clientScript = "<script language='javascript'>"
                clientScript += "WindowsCloseReject()"
                clientScript += "</script>"
                ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", clientScript, False)
            End If
        End If
    End Sub
End Class
