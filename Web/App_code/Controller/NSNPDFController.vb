Imports Microsoft.VisualBasic
Imports System.io
Imports System.Security.AccessControl
Imports System.Web.UI.Page
Imports Org.apache.pdfbox.PDFMerger

Public Class NSNPDFController
    Public Function MergePdfNew(ByVal pdfName As String, ByVal pdfBlank As String, ByVal outputFileFullName As String) As String
        Dim result As String = "success"
        Try
            Dim pdfMerger As New org.apache.pdfbox.util.PDFMergerUtility
            With pdfMerger
                .setDestinationFileName(outputFileFullName)
                .addSource(pdfName)
                .addSource(pdfBlank)
                .mergeDocuments()
            End With
        Catch ex As Exception
            result = ex.Message.ToString()
        End Try
        Return result
    End Function
End Class
