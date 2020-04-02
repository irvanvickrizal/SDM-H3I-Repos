Imports Microsoft.VisualBasic

Public Class PDFUtilityController
    Public Shared Function MergePdfNew(ByVal pdffileA As String, ByVal pdffileB As String, ByVal outputFileFullName As String) As Boolean
        Dim result As Boolean = True
        Try
            Dim pdfMerger As New org.pdfbox.util.PDFMergerUtility
            With pdfMerger
                .setDestinationFileName(outputFileFullName)
                .addSource(pdffileA)
                .addSource(pdffileB)
                .mergeDocuments()
            End With
        Catch ex As Exception
        End Try
        Return result
    End Function
End Class
