Imports Common
Imports System.Data.SqlClient
Imports System.Collections.Generic
Imports System.Data
Imports Microsoft.VisualBasic
Imports Entities
Imports BusinessLogic
Imports Common_NSNFramework

Public Class SMSNew
    Dim objetsitedoc As New ETSiteDoc
    Dim objbositedoc As New BOSiteDocs
    Dim objdb As New DBUtil
    Dim objsms As New mCore.SMS
    Dim objsms1 As New mCore.SMS
    Dim msgdata As New StringBuilder
    Dim msgdata1 As New StringBuilder
    Dim dts As New DataTable
    Dim dts1 As New DataTable
    Dim docname As String
    Dim npwd As String = ""
    Dim phone As String = ""
    Dim i As Integer
    Dim j As Integer
    Dim errmsg As String
#Region "NSNFramework"
    Dim objUtils_NSN As New DBUtils_NSN
#End Region
    Public Sub SendSMSATPDoc(ByVal listuser As List(Of UserInfo), ByVal siteid As Integer, ByVal siteversion As Integer, ByVal docatpid As Integer, ByVal isRejected As Boolean)
        objsms.License.Company = "TAKE UNITED SDN BHD"
        objsms.License.LicenseType = "PRO-DISTRIBUTION"
        objsms.License.Key = "TRQT-NF2L-Y7V9-HF4E"
        objsms.Port = ConfigurationManager.AppSettings("ModemPort").ToString
        objsms.BaudRate = 115200 'bugfix100805 must use this port number 115200
        objsms.DataBits = 8
        objsms.DisableCheckPIN = False 'true if no pin is required
        objsms.PIN = "1234" 'bugfix100806 error when requesting sms

        objsms1.License.Company = "TAKE UNITED SDN BHD"
        objsms1.License.LicenseType = "PRO-DISTRIBUTION"
        objsms1.License.Key = "TRQT-NF2L-Y7V9-HF4E"
        objsms1.Port = ConfigurationManager.AppSettings("ModemPort").ToString
        objsms1.BaudRate = 115200 'bugfix100805 must use this port number 115200
        objsms1.DataBits = 8
        objsms1.DisableCheckPIN = False 'true if no pin is required
        objsms1.PIN = "1234" 'bugfix100806 error when requesting sms

        For Each info As UserInfo In listuser
            msgdata.Append("Dear " & info.Username & ", ")
            msgdata.Append(Environment.NewLine)
            If isRejected = True Then
                msgdata.Append("for Site: " & objUtils_NSN.GetSiteID_PONO(siteid, siteversion) & "as ATP Document has been rejected, please to be revised")
            Else
                msgdata.Append("for Site: " & objUtils_NSN.GetSiteID_PONO(siteid, siteversion) & "as ATP Document in your task pending now")
            End If
            msgdata.Append(Environment.NewLine)
            msgdata.Append("Powered by e-BAST")
            If info.Handphone <> "" Then
                Try
                    objsms.SendSMS(info.Handphone, msgdata.ToString(), False)
                Catch ex As Exception
                    objsms.Disconnect() 'disconnect first modem
                    'when destination not invalid
                    If Not ex.Message.Contains("4007") Then
                        objdb.ExeNonQuery("exec uspErrLog '', 'sendsmsnew','" & objdb.removeUnwantedQueryChar(ex.Message.ToString()) & "','1st-modem'")
                        Try
                            objsms1.SendSMS(info.Handphone, msgdata.ToString(), False)
                        Catch ex1 As Exception
                            objdb.ExeNonQuery("exec uspErrLog '', 'requestSMS','" & objdb.removeUnwantedQueryChar(ex1.Message.ToString()) & "','2nd-modem'")
                        Finally
                            objsms1.Disconnect()
                        End Try
                    End If
                Finally
                    objsms.Disconnect()
                End Try
            End If
        Next
        'objsms.Disconnect()
        objsms.Dispose()
        'objsms1.Disconnect()
        objsms1.Dispose()
    End Sub
    Public Sub sendsmsnew(ByVal bastteamrole As Integer, ByVal bautteamrole As Integer, ByVal siteid As String, ByVal docid As Integer, ByVal pono As String, ByVal type As String)
        '--first modem object
        objsms.License.Company = "TAKE UNITED SDN BHD"
        objsms.License.LicenseType = "PRO-DISTRIBUTION"
        objsms.License.Key = "TRQT-NF2L-Y7V9-HF4E"
        objsms.Port = ConfigurationManager.AppSettings("ModemPort").ToString
        objsms.BaudRate = 115200 'bugfix100805 must use this port number 115200
        objsms.DataBits = 8
        objsms.DisableCheckPIN = False 'true if no pin is required
        objsms.PIN = "1234" 'bugfix100806 error when requesting sms
        '--second modem object
        objsms1.License.Company = "TAKE UNITED SDN BHD"
        objsms1.License.LicenseType = "PRO-DISTRIBUTION"
        objsms1.License.Key = "TRQT-NF2L-Y7V9-HF4E"
        objsms1.Port = ConfigurationManager.AppSettings("ModemPort").ToString
        objsms1.BaudRate = 115200 'bugfix100805 must use this port number 115200
        objsms1.DataBits = 8
        objsms1.DisableCheckPIN = False 'true if no pin is required
        objsms1.PIN = "1234" 'bugfix100806 error when requesting sms
        '--
        dts = objdb.ExeQueryDT("select name,phoneno from ebastusers_1 A inner join ebastuserrole B ON A.USR_ID=B.USR_ID where A.usrrole in (" & bastteamrole & "," & bautteamrole & ") and B.rgn_id=(select rgn_id from codsite where site_id= (select site_id from codsite where site_no='" & siteid & "'))", "ds")
        docname = objdb.ExeQueryScalarString("select docname from coddoc where doc_id=" & docid & "")
        For i = 0 To dts.Rows.Count - 1
            phone = "+" & dts.Rows(i).Item(1).ToString
            msgdata.Append("Dear " & dts.Rows(i).Item(0).ToString)
            msgdata.Append(Environment.NewLine)
            msgdata.Append("for Site: " & siteid & "-" & "PONo-" & pono & "-" & " docname & " & "Document is " & type)
            msgdata.Append(Environment.NewLine)
            msgdata.Append("Powered by e-BAST")
            If phone <> "" Then
                Try
                    objsms.SendSMS(phone, msgdata.ToString(), False)
                Catch ex As Exception
                    objsms.Disconnect() 'disconnect first modem
                    'when destination not invalid
                    If Not ex.Message.Contains("4007") Then
                        objdb.ExeNonQuery("exec uspErrLog '', 'sendsmsnew','" & objdb.removeUnwantedQueryChar(ex.Message.ToString()) & "','1st-modem'")
                        Try
                            objsms1.SendSMS(phone, msgdata.ToString(), False)
                        Catch ex1 As Exception
                            objdb.ExeNonQuery("exec uspErrLog '', 'requestSMS','" & objdb.removeUnwantedQueryChar(ex1.Message.ToString()) & "','2nd-modem'")
                        End Try
                        'sending alert to administrators
                        'dts1 = objdb.ExeQueryDT("select phoneno from ebastusers_1 where usrrole=1", "ds")
                        'For j = 0 To dts1.Rows.Count - 1
                        '    errmsg = ""
                        '    errmsg = errmsg + "Warning!! The first modem has encounter an error... (Requested by " & dts.Rows(i).Item(0).ToString & ")"
                        '    errmsg = errmsg + ex.Message.ToString() + " @ sub sendsmsnew "
                        '    Try
                        '        objsms1.SendSMS("+" & dts1.Rows(j).Item(0).ToString(), errmsg, False)
                        '    Catch ex1 As Exception
                        '        objdb.ExeNonQuery("exec uspErrLog '', 'requestSMS','" & objdb.removeUnwantedQueryChar(ex1.Message.ToString()) & "','2nd-modem'")
                        '    End Try
                        'Next
                        objsms1.Disconnect()
                    End If
                End Try
            End If
        Next
        objsms.Disconnect()
        objsms.Dispose()
        objsms1.Disconnect()
        objsms1.Dispose()
    End Sub
    Public Sub requestSMS(ByVal username As String, ByVal userlogin As String, ByVal siteno As String, ByVal pono As String, ByVal docname As String)
        '--first modem object
        'objsms.License.Company = "TAKE UNITED SDN BHD"
        'objsms.License.LicenseType = "PRO-DISTRIBUTION"
        'objsms.License.Key = "TRQT-NF2L-Y7V9-HF4E"
        ''objsms.Port = "COM6"
        'objsms.Port = comport
        'objsms.BaudRate = 115200 'bugfix100805 must use this port number 115200
        'objsms.DataBits = 8
        'objsms.DisableCheckPIN = False 'true if no pin is required
        'objsms.PIN = "1234" 'bugfix100806 error when requesting sms
        ''--second modem object
        'objsms1.License.Company = "TAKE UNITED SDN BHD"
        'objsms1.License.LicenseType = "PRO-DISTRIBUTION"
        'objsms1.License.Key = "TRQT-NF2L-Y7V9-HF4E"
        'objsms1.Port = comport
        'objsms1.BaudRate = 115200 'bugfix100805 must use this port number 115200
        'objsms1.DataBits = 8
        'objsms1.DisableCheckPIN = False 'true if no pin is required
        'objsms1.PIN = "1234" 'bugfix100806 error when requesting sms
        '--
        Dim phone As String = ""
        phone = "+" & objdb.ExeQueryScalarString("select phoneno from ebastusers_1 where usrlogin='" & userlogin & "' and rstatus=2")
        npwd = objdb.ExeQueryScalarString("Exec random_password 8,'simple'")
        msgdata.Append("Dear " & username & " your requested password is : " & npwd)
        msgdata.Append(Environment.NewLine)
        msgdata.Append("Powered by e-BAST")
        Try
            'objsms.SendSMS(phone, msgdata.ToString(), False)
            'Dim strSMS As String = "https://api.infobip.com/sms/1/text/query?username=NSN.2&password=Test1234&from=NOKIA-EBAST&to=" & phone & "&text=" & msgdata.ToString()
			Dim strSMS As String = "https://api.infobip.com/sms/1/text/query?username=NOKIA-INDONESIA&password=Test1234&from=NOKIA-EBAST&to=" & phone & "&text=" & msgdata.ToString()
            Dim client As New Net.WebClient
            client.DownloadString(strSMS)
        Catch ex As Exception         
            objdb.ExeNonQuery("exec uspErrLog '', 'requestSMS','" & objdb.removeUnwantedQueryChar(ex.Message.ToString()) & "','SMSGateway'")
		End Try
    End Sub

    Public Function SendDGPassSMS(ByVal fullname As String, ByVal phoneno As String, ByVal password As String) As Boolean
        Dim isSucceed As Boolean = True
         '--first modem object
        'objsms.License.Company = "TAKE UNITED SDN BHD"
        'objsms.License.LicenseType = "PRO-DISTRIBUTION"
        'objsms.License.Key = "TRQT-NF2L-Y7V9-HF4E"
        'objsms.Port = comport
        'objsms.BaudRate = 115200 'bugfix100805 must use this port number 115200
        'objsms.DataBits = 8
        'objsms.DisableCheckPIN = False 'true if no pin is required
        'objsms.PIN = "1234" 'bugfix100806 error when requesting sms
        ''--second modem object
        'objsms1.License.Company = "TAKE UNITED SDN BHD"
        'objsms1.License.LicenseType = "PRO-DISTRIBUTION"
        'objsms1.License.Key = "TRQT-NF2L-Y7V9-HF4E"
        'objsms1.Port = comport
        'objsms1.BaudRate = 115200 'bugfix100805 must use this port number 115200
        'objsms1.DataBits = 8
        'objsms1.DisableCheckPIN = False 'true if no pin is required
        'objsms1.PIN = "1234" 'bugfix100806 error when requesting sms
        ''--
        'Dim phone As String = ""
        'phone = "+" & objdb.ExeQueryScalarString("select phoneno from ebastusers_1 where usrlogin='" & userlogin & "' and rstatus=2")
        'npwd = objdb.ExeQueryScalarString("Exec random_password 8,'simple'")
        npwd = password
        msgdata.Append("Dear " & fullname & " your requested password is : " & npwd)
        msgdata.Append(Environment.NewLine)
        msgdata.Append("Powered by e-BAST")
        Try
            'objsms.SendSMS("+" & phoneno, msgdata.ToString(), False)
            'Dim strSMS As String = "https://api.infobip.com/sms/1/text/query?username=NSN.2&password=Test1234&from=NOKIA-EBAST&to=" & phoneno & "&text=" & msgdata.ToString()
			Dim strSMS As String = "https://api.infobip.com/sms/1/text/query?username=NOKIA-INDONESIA&password=Test1234&from=NOKIA-EBAST&to=" & phoneno & "&text=" & msgdata.ToString()
            Dim client As New Net.WebClient
            client.DownloadString(strSMS)
        Catch ex As Exception
            objdb.ExeNonQuery("exec uspErrLog '', 'requestSMS','" & objdb.removeUnwantedQueryChar(ex.Message.ToString()) & "','SMS-Gateway'")
            isSucceed = False
            'objsms.Disconnect() 'disconnect first modem
            'when destination not invalid
            'If Not ex.Message.Contains("4007") Then
            '    'objdb.ExeNonQuery("exec uspErrLog '', 'requestSMS','" & objdb.removeUnwantedQueryChar(ex.Message.ToString()) & "','1st-modem'")
            '    'Try
            '    '    objsms1.SendSMS("+" & phoneno, msgdata.ToString(), False)
            '    'Catch ex1 As Exception
            '    '    isSucceed = False
            '    '    objdb.ExeNonQuery("exec uspErrLog '', 'requestSMS','" & objdb.removeUnwantedQueryChar(ex1.Message.ToString()) & "','2nd-modem'")
            '    'End Try
            '    'sending alert to administrators
            '    'dts1 = objdb.ExeQueryDT("select phoneno from ebastusers_1 where usrrole=1", "ds")
            '    'For j = 0 To dts1.Rows.Count - 1
            '    '    errmsg = ""
            '    '    errmsg = errmsg + "Warning!! The first modem has encounter an error... (Requested by " & username & ")"
            '    '    errmsg = errmsg + ex.Message.ToString() + " @ sub requestSMS "
            '    '    Try
            '    '        objsms1.SendSMS("+" & dts1.Rows(j).Item(0).ToString(), errmsg, False)
            '    '    Catch ex1 As Exception
            '    '        objdb.ExeNonQuery("exec uspErrLog '', 'requestSMS','" & objdb.removeUnwantedQueryChar(ex1.Message.ToString()) & "','2nd-modem'")
            '    '    End Try
            '    'Next
            '    'objsms1.Disconnect()
            'End If

        End Try
        'objsms.Disconnect()
        'objsms.Dispose()
        'objsms1.Disconnect()
        'objsms1.Dispose()
        Return isSucceed
    End Function
End Class
