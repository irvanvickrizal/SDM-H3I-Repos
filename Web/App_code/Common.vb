Imports Microsoft.VisualBasic
Imports System.Net.Mail
Imports System.IO.File
Imports System.IO

Public Class CommonSite

    'Added by Fauzan, 22 Nov 2018. Email Submission Document
    Public Shared ReadOnly Approval_Action As String = "Approval Document"
    Public Shared ReadOnly Detail_Pending_Approval_Action As String = "Detail Pending Approval Document"
    'End

    Public Shared Function PageSize() As Integer
        If HttpContext.Current.Session("Page_size") Is Nothing Then
            Return 8
        Else
            Return Convert.ToInt32(HttpContext.Current.Session("Page_size"))
        End If
    End Function
    Public Shared Function GetDashBoardLevel() As Integer
        If HttpContext.Current.Session("Site_Id") = "0" Then

            If HttpContext.Current.Session("Zone_Id") = "0" Then

                If HttpContext.Current.Session("Region_Id") = "0" Then

                    If HttpContext.Current.Session("Area_Id") = "0" Then

                        If HttpContext.Current.Session("Jv_Id") = "0" Then
                            Return 0
                        Else
                            If HttpContext.Current.Session("Jv_Id") Is Nothing Then
                                Return 0
                            Else
                                Return 5
                            End If

                        End If
                    Else
                        Return 1
                    End If

                Else
                    Return 2
                End If
            Else
                Return 3
            End If

        Else
            If HttpContext.Current.Session("Site_Id") Is Nothing Then
                Return 0
            Else
                Return 4
            End If
        End If
    End Function
    Public Shared Function GetDashBoardLevelId() As Integer
        If HttpContext.Current.Session("Site_Id") = "0" Then

            If HttpContext.Current.Session("Zone_Id") = "0" Then

                If HttpContext.Current.Session("Region_Id") = "0" Then

                    If HttpContext.Current.Session("Area_Id") = "0" Then

                        If HttpContext.Current.Session("Jv_Id") = "0" Then
                            Return 0
                        Else
                            Return HttpContext.Current.Session("Jv_Id")
                        End If
                    Else
                        Return HttpContext.Current.Session("Area_Id")
                    End If

                Else
                    Return HttpContext.Current.Session("Region_Id")
                End If
            Else
                Return HttpContext.Current.Session("Zone_Id")
            End If

        Else
            Return HttpContext.Current.Session("Site_Id")
        End If
    End Function
    Public Shared Function GetSiteId() As Integer
        If HttpContext.Current.Session("Site_Id") Is Nothing Then
            Return 0
        Else
            Return Convert.ToInt32(HttpContext.Current.Session("Site_Id"))
        End If
    End Function

    Public Shared Function UserId() As Integer
        If HttpContext.Current.Session("User_Id") Is Nothing Then
            Return 0
        Else
            Return Convert.ToInt32(HttpContext.Current.Session("User_Id"))
        End If
    End Function

    Public Shared Function UserLogin() As String
        If HttpContext.Current.Session("User_Login") Is Nothing Then
            Return "SysAdmin"
        Else
            Return HttpContext.Current.Session("User_Login")
        End If
    End Function

    Public Shared Function UserName() As String
        If HttpContext.Current.Session("User_Name") Is Nothing Then
            Return "SysAdmin"
        Else
            Return HttpContext.Current.Session("User_Name")
        End If
    End Function


    Public Shared Function RollId() As Integer
        If HttpContext.Current.Session("Role_Id") Is Nothing Then
            Return 1
        Else
            Return HttpContext.Current.Session("Role_Id")
        End If
    End Function



    Public Shared Function UserType() As String
        If HttpContext.Current.Session("User_Type") Is Nothing Then
            Return 0
        Else
            Return HttpContext.Current.Session("User_Type")
        End If

    End Function
    Public Shared Function SCRID() As Integer
        If HttpContext.Current.Session("SRCId") Is Nothing Then
            Return 0
        Else
            If (HttpContext.Current.Session("SRCId") = "") Then
                Return 1

            Else
                Return HttpContext.Current.Session("SRCId")
            End If

        End If
    End Function
    Public Sub MailPro(ByVal SenderMail As String, ByVal ReceiverMail As String, ByVal Subject As String, ByVal strBody As String)
        Dim mm As New MailMessage(SenderMail, ReceiverMail)
        mm.Subject = Subject
        mm.Body = strBody
        mm.IsBodyHtml = True
        Dim client As New SmtpClient
        client.Host = ConfigurationManager.AppSettings("smtp")
        client.Port = ConfigurationManager.AppSettings("Portno")
        client.DeliveryMethod = SmtpDeliveryMethod.PickupDirectoryFromIis
        client.Send(mm)
    End Sub
    Public Shared Function PageCount(ByVal strUrl As String) As Integer
        Dim fs As FileStream = New FileStream(strUrl, FileMode.Open, FileAccess.Read)

        Dim r As StreamReader = New StreamReader(fs)

        Dim pdfText As String = r.ReadToEnd()
        Dim regx As System.Text.RegularExpressions.Regex = New Regex("/Type\s*/Page[^s]")

        Dim matches As System.Text.RegularExpressions.MatchCollection = regx.Matches(pdfText)

        Return matches.Count
    End Function
    Public Shared Function BAUTID() As Integer
        Return ConfigurationManager.AppSettings("BAUTID")
    End Function

    ''' <summary>
    ''' Added by Fauzan, 2 Dec 2018
    ''' To get Document ID from request data
    ''' </summary>
    ''' <param name="docIdFrom">
    ''' To prevent missing param id
    ''' Will return the default document id from page request
    ''' </param>
    ''' <returns></returns>
    Public Shared Function DocId(ByVal docIdFrom As Integer) As Integer
        If Not HttpContext.Current Is Nothing Then
            If Not String.IsNullOrEmpty(HttpContext.Current.Request.QueryString("id")) Then
                Return HttpContext.Current.Request.QueryString("id")
                'Can put another condition if the parameter is different
            End If
        End If
        Return docIdFrom
    End Function

End Class
