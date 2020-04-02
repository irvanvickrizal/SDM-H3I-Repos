Imports Microsoft.VisualBasic
Imports CRFramework

Public Class CRCOMultipleApprovalInfo
    Inherits CRInfo



    Private _sno As Int32
    Public Property SNO() As Int32
        Get
            Return _sno
        End Get
        Set(ByVal value As Int32)
            _sno = value
        End Set
    End Property


    Private _doctype As String
    Public Property DocType() As String
        Get
            Return _doctype
        End Get
        Set(ByVal value As String)
            _doctype = value
        End Set
    End Property


    Private _submitDate As DateTime
    Public Property SubmitDate() As DateTime
        Get
            Return _submitDate
        End Get
        Set(ByVal value As DateTime)
            _submitDate = value
        End Set
    End Property


    Private _modifiedtransasctiondate As DateTime
    Public Property ModifiedTransactionDate() As DateTime
        Get
            Return _modifiedtransasctiondate
        End Get
        Set(ByVal value As DateTime)
            _modifiedtransasctiondate = value
        End Set
    End Property

End Class
