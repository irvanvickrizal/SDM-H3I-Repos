Imports Microsoft.VisualBasic

Public Class MailConfigInfo
    Private _configid As Integer
    Public Property ConfigID() As Integer
        Get
            Return _configid
        End Get
        Set(ByVal value As Integer)
            _configid = value
        End Set
    End Property

    Private _ConfigName As String
    Public Property ConfigName() As String
        Get
            Return _ConfigName
        End Get
        Set(ByVal value As String)
            _ConfigName = value
        End Set
    End Property

    Private _smtp As String
    Public Property SMTP() As String
        Get
            Return _smtp
        End Get
        Set(ByVal value As String)
            _smtp = value
        End Set
    End Property

    Private _smtpport As String
    Public Property SMTPPort() As String
        Get
            Return _smtpport
        End Get
        Set(ByVal value As String)
            _smtpport = value
        End Set
    End Property

    Private _mailusername As String
    Public Property MailUsername() As String
        Get
            Return _mailusername
        End Get
        Set(ByVal value As String)
            _mailusername = value
        End Set
    End Property

    Private _mailpassword As String
    Public Property MailPassword() As String
        Get
            Return _mailpassword
        End Get
        Set(ByVal value As String)
            _mailpassword = value
        End Set
    End Property
End Class
