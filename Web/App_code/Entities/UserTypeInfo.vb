Imports Microsoft.VisualBasic

Public Class UserTypeInfo
    Private _usertypeid As Integer
    Public Property UserTypeId() As Integer
        Get
            Return _usertypeid
        End Get
        Set(ByVal value As Integer)
            _usertypeid = value
        End Set
    End Property

    Private _usertype As String
    Public Property UserType() As String
        Get
            Return _usertype
        End Get
        Set(ByVal value As String)
            _usertype = value
        End Set
    End Property


    Private _userCompany As String
    Public Property UserCompany() As String
        Get
            Return _userCompany
        End Get
        Set(ByVal value As String)
            _userCompany = value
        End Set
    End Property


End Class
