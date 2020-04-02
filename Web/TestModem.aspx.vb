
Partial Class TestModem
    Inherits System.Web.UI.Page
    Dim objsms As New mCore.SMS
    Dim objsms1 As New mCore.SMS

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub BtnTestClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnTest.Click
        If Not String.IsNullOrEmpty(TxtPhone.Text) Then
            LblErrorMessage.Text = SendingMessage(TxtPhone.Text, TxtMessage.Text)
        End If
    End Sub

    Private Function SendingMessage(ByVal strPhone As String, ByVal strMessage As String) As String
        Dim strResult As String = "Sending Message was succesfully"
        objsms.License.Company = "TAKE UNITED SDN BHD"
        objsms.License.LicenseType = "PRO-DISTRIBUTION"
        objsms.License.Key = "TRQT-NF2L-Y7V9-HF4E"
        objsms.Port = "COM4"
        objsms.BaudRate = 115200 'bugfix100805 must use this port number 115200
        objsms.DataBits = 8
        objsms.DisableCheckPIN = False 'true if no pin is required
        objsms.PIN = "1234" 'bugfix100806 error when requesting sms

        objsms1.License.Company = "TAKE UNITED SDN BHD"
        objsms1.License.LicenseType = "PRO-DISTRIBUTION"
        objsms1.License.Key = "TRQT-NF2L-Y7V9-HF4E"
        objsms1.Port = "COM41"
        objsms1.BaudRate = 115200 'bugfix100805 must use this port number 115200
        objsms1.DataBits = 8
        objsms1.DisableCheckPIN = False 'true if no pin is required
        objsms1.PIN = "1234" 'bugfix100806 error when requesting sms
        If strPhone <> "" Then
            Try
                objsms.SendSMS(strPhone, strMessage, False)
            Catch ex As Exception
                objsms.Disconnect() 'disconnect first modem
                'when destination not invalid
                If Not ex.Message.Contains("4007") Then
                    'objdb.ExeNonQuery("exec uspErrLog '', 'sendsmsnew','" & objdb.removeUnwantedQueryChar(ex.Message.ToString()) & "','1st-modem'")
                    strResult = ex.Message.tostring()
                    Try
                        strResult = "Sending Message was succesfully"
                        objsms1.SendSMS(strPhone, strMessage, False)
                    Catch ex1 As Exception
                        'objdb.ExeNonQuery("exec uspErrLog '', 'requestSMS','" & objdb.removeUnwantedQueryChar(ex1.Message.ToString()) & "','2nd-modem'")
                        strResult = ex1.Message.tostring()
                    Finally
                        objsms1.Disconnect()
                    End Try
                End If
            Finally
                objsms.Disconnect()
            End Try
        End If
        'objsms.Disconnect()
        objsms.Dispose()
        'objsms1.Disconnect()
        objsms1.Dispose()
        Return strResult
    End Function

End Class
