Imports TakeSMS
Imports Common
Partial Class sendsms
    Inherits System.Web.UI.Page
    Dim objdb As New DBUtil
    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim cellno As String = txtmobile.Text
        Dim msgt As String = txtmessage.Text
        Try
            Dim SMSTake As New TakeSMS("COM4")
            SMSTake.Open()
            SMSTake.SendSMS("+60176276693", "sairam1")
            SMSTake.Close()
            response.write("sending success")
        Catch ex As Exception
            objdb.ExeNonQuery("exec uspErrLog '', 'smserror','" & ex.Message.ToString().Replace("'", "''") & "','sendingsms'")
        End Try

        'Dim strClientIP As String
        'strClientIP = Request.UserHostAddress()
        'Response.Write(strClientIP)
    End Sub
End Class
