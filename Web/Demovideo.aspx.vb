Imports FileTransfer.FileTransfer
Partial Class Demovideo
    Inherits System.Web.UI.Page
    Protected Sub btngo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btngo.Click
        If txtpassword.Text = "buddy" Then
            lnkdownload.Visible = True
            lnkwatch.Visible = True
            lnkpassword.Visible = False
        Else
            lnkpassword.Visible = True
            'ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('Wrong Password');", True)
            response.write("<script language='javascript'>alert('Wrong Password');</script>")

        End If
    End Sub

    Protected Sub LinkButton1_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim strpath As String = Server.MapPath("") & "\"
        Dim rs As New FileTransfer.FileTransfer.File
        Dim Downloads As String = rs.Download("demo.mp4", strpath, 100000)
    End Sub
End Class
