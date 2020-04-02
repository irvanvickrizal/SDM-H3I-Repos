Imports AXmsCtrl
Partial Class sendsms2
    Inherits System.Web.UI.Page
    Dim objsms As New mCore.SMS
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        objSMS.License.Company = "TAKE UNITED SDN BHD"
        objSMS.License.LicenseType = "PRO-DISTRIBUTION"
        objSMS.License.Key = "TRQT-NF2L-Y7V9-HF4E"
    End Sub
    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        objsms.Port = "COM6"
        objsms.BaudRate = 115200 'bugfix100805 must use this port number 115200
        objsms.DataBits = 8
        objsms.PIN = "9437" 'bugfix100806 error when requesting sms
        If objsms.Connect Then
            Response.Write("Connected Successfully!")
            Dim strSendResult As String
            strSendResult = objsms.SendSMS(TextRecipient.Text.ToString(), TextMessage.Text.ToString(), False)
            Response.Write(strSendResult & DateTime.Now & "<br>")
            objsms.Disconnect()
        Else
            Response.Write(objsms.ErrorCode & ": " & objsms.ErrorDescription)
        End If
        objsms = Nothing
    End Sub
End Class
