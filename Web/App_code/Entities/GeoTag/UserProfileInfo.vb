Imports Microsoft.VisualBasic

Public Class UserProfileInfo
    Inherits SubconInfo

    Private _usrid As Integer
    Public Property UserId() As Integer
        Get
            Return _usrid
        End Get
        Set(ByVal value As Integer)
            _usrid = value
        End Set
    End Property

    Private _fullname As String
    Public Property Fullname() As String
        Get
            Return _fullname
        End Get
        Set(ByVal value As String)
            _fullname = value
        End Set
    End Property


    Private _usrLogin As String
    Public Property UserLogin() As String
        Get
            Return _usrLogin
        End Get
        Set(ByVal value As String)
            _usrLogin = value
        End Set
    End Property


    Private _usrPassword As String
    Public Property UsrPassword() As String
        Get
            Return _usrPassword
        End Get
        Set(ByVal value As String)
            _usrPassword = value
        End Set
    End Property


    Private _phoneNo As String
    Public Property PhoneNo() As String
        Get
            Return _phoneNo
        End Get
        Set(ByVal value As String)
            _phoneNo = value
        End Set
    End Property


    Private _email As String
    Public Property Email() As String
        Get
            Return _email
        End Get
        Set(ByVal value As String)
            _email = value
        End Set
    End Property


    Private _userType As String
    Public Property UserType() As String
        Get
            Return _userType
        End Get
        Set(ByVal value As String)
            _userType = value
        End Set
    End Property


    Private _roleinf As New RoleInfo
    Public Property RoleInf() As RoleInfo
        Get
            Return _roleinf
        End Get
        Set(ByVal value As RoleInfo)
            _roleinf = value
        End Set
    End Property

    Private _signTitle As String
    Public Property SignTitle() As String
        Get
            Return _signTitle
        End Get
        Set(ByVal value As String)
            _signTitle = value
        End Set
    End Property

    Private _companyname As String
    Public Property CompanyName() As String
        Get
            Return _companyname
        End Get
        Set(ByVal value As String)
            _companyname = value
        End Set
    End Property



    Private _cminfo As New CMInfo
    Public Property CMAInfo() As CMInfo
        Get
            Return _cminfo
        End Get
        Set(ByVal value As CMInfo)
            _cminfo = value
        End Set
    End Property
End Class
