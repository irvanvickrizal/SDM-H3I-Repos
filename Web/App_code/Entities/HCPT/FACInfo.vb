﻿Imports Microsoft.VisualBasic

Public Class FACInfo
    Private _facid As Int32
    Public Property FACID() As Int32
        Get
            Return _facid
        End Get
        Set(ByVal value As Int32)
            _facid = value
        End Set
    End Property

    Private _siteinf As New SiteInfo
    Public Property SiteInf() As SiteInfo
        Get
            Return _siteinf
        End Get
        Set(ByVal value As SiteInfo)
            _siteinf = value
        End Set
    End Property

    Private _ispassed As Boolean
    Public Property IsPassed() As Boolean
        Get
            Return _ispassed
        End Get
        Set(ByVal value As Boolean)
            _ispassed = value
        End Set
    End Property

    Private _isnotpasssed As Boolean
    Public Property IsNotPassed() As Boolean
        Get
            Return _isnotpasssed
        End Get
        Set(ByVal value As Boolean)
            _isnotpasssed = value
        End Set
    End Property

    Private _cmainfo As New CMInfo
    Public Property CMAInfo() As CMInfo
        Get
            Return _cmainfo
        End Get
        Set(ByVal value As CMInfo)
            _cmainfo = value
        End Set
    End Property

    Private _potype As String
    Public Property POType() As String
        Get
            Return _potype
        End Get
        Set(ByVal value As String)
            _potype = value
        End Set
    End Property

End Class
