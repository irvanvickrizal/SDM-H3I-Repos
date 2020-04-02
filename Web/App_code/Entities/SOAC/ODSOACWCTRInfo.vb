Imports Microsoft.VisualBasic

Public Class ODSOACWCTRInfo
    Inherits CMInfo


    Private _sno As Int32
    Public Property SNO() As Int32
        Get
            Return _sno
        End Get
        Set(ByVal value As Int32)
            _sno = value
        End Set
    End Property


    Private _soacinfo As New ODSOACInfo
    Public Property SOACInfo() As ODSOACInfo
        Get
            Return _soacinfo
        End Get
        Set(ByVal value As ODSOACInfo)
            _soacinfo = value
        End Set
    End Property


    Private _wfid As Integer
    Public Property WFID() As Integer
        Get
            Return _wfid
        End Get
        Set(ByVal value As Integer)
            _wfid = value
        End Set
    End Property


    Private _siteinfo As SiteInfo
    Public Property SiteInf() As SiteInfo
        Get
            Return _siteinfo
        End Get
        Set(ByVal value As SiteInfo)
            _siteinfo = value
        End Set
    End Property


    Private _wday As String
    Public Property WDay() As String
        Get
            Return _wday
        End Get
        Set(ByVal value As String)
            _wday = value
        End Set
    End Property



    Private _wDate As Integer
    Public Property WDate() As Integer
        Get
            Return _wDate
        End Get
        Set(ByVal value As Integer)
            _wDate = value
        End Set
    End Property


    Private _wMonth As Integer
    Public Property WMonth() As Integer
        Get
            Return _wMonth
        End Get
        Set(ByVal value As Integer)
            _wMonth = value
        End Set
    End Property


    Private _wyear As Integer
    Public Property WYear() As Integer
        Get
            Return _wyear
        End Get
        Set(ByVal value As Integer)
            _wyear = value
        End Set
    End Property


    Private _wstart As Nullable(Of DateTime)
    Public Property WStart() As Nullable(Of DateTime)
        Get
            Return _wstart
        End Get
        Set(ByVal value As Nullable(Of DateTime))
            _wstart = value
        End Set
    End Property


    Private _wsfinish As Nullable(Of DateTime)
    Public Property WSFinish() As Nullable(Of DateTime)
        Get
            Return _wsfinish
        End Get
        Set(ByVal value As Nullable(Of DateTime))
            _wsfinish = value
        End Set
    End Property


    Private _whfinish As Nullable(Of DateTime)
    Public Property WHFinish() As Nullable(Of DateTime)
        Get
            Return _whfinish
        End Get
        Set(ByVal value As Nullable(Of DateTime))
            _whfinish = value
        End Set
    End Property


    Private _durationexec As Integer
    Public Property DurataionExec() As Integer
        Get
            Return _durationexec
        End Get
        Set(ByVal value As Integer)
            _durationexec = value
        End Set
    End Property



    Private _actualexec As Integer
    Public Property ActualExec() As Integer
        Get
            Return _actualexec
        End Get
        Set(ByVal value As Integer)
            _actualexec = value
        End Set
    End Property


    Private _totalA As Integer
    Public Property TotalA() As Integer
        Get
            Return _totalA
        End Get
        Set(ByVal value As Integer)
            _totalA = value
        End Set
    End Property


    Private _dotherR1 As String
    Public Property DOtherR1() As String
        Get
            Return _dotherR1
        End Get
        Set(ByVal value As String)
            _dotherR1 = value
        End Set
    End Property


    Private _dotherR2 As String
    Public Property DOtherR2() As String
        Get
            Return _dotherR2
        End Get
        Set(ByVal value As String)
            _dotherR2 = value
        End Set
    End Property


    Private _dotherR3 As String
    Public Property DOtherR3() As String
        Get
            Return _dotherR3
        End Get
        Set(ByVal value As String)
            _dotherR3 = value
        End Set
    End Property



    Private _dotherR1Days As Integer
    Public Property DOtherR1Days() As Integer
        Get
            Return _dotherR1Days
        End Get
        Set(ByVal value As Integer)
            _dotherR1Days = value
        End Set
    End Property


    Private _dotherR2Days As Integer
    Public Property DOtherR2Days() As Integer
        Get
            Return _dotherR2Days
        End Get
        Set(ByVal value As Integer)
            _dotherR2Days = value
        End Set
    End Property


    Private _dotherR3Days As Integer
    Public Property DOtherR3Days() As Integer
        Get
            Return _dotherR3Days
        End Get
        Set(ByVal value As Integer)
            _dotherR3Days = value
        End Set
    End Property


    Private _totalB As Integer
    Public Property TotalB() As Integer
        Get
            Return _totalB
        End Get
        Set(ByVal value As Integer)
            _totalB = value
        End Set
    End Property


    Private _totalC As Integer
    Public Property TotalC() As Integer
        Get
            Return _totalC
        End Get
        Set(ByVal value As Integer)
            _totalC = value
        End Set
    End Property



    Private _totald As Integer
    Public Property TotalD() As Integer
        Get
            Return _totald
        End Get
        Set(ByVal value As Integer)
            _totald = value
        End Set
    End Property

End Class
