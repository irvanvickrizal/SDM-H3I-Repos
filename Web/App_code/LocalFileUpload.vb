Imports Microsoft.VisualBasic

Public Class LocalFileUpload
    Public Shared Function ConvertAnyFormatToPDF(ByVal IFileUpload As System.Web.UI.WebControls.FileUpload, ByVal strPath As String) As String
        Dim FileNamePath As String = IFileUpload.PostedFile.FileName
        Dim FileNameOnly As String = System.IO.Path.GetFileName(IFileUpload.PostedFile.FileName)
        Dim ReFileName As String = Format(CDate(DateTime.Now), "ddMMyyyyHHss") & FileNameOnly
        Dim strReturn As String = String.Empty
        If System.IO.Path.GetExtension(IFileUpload.PostedFile.FileName).ToLower = ".pdf" Then
            If System.IO.Directory.Exists(strPath) Then
                IFileUpload.PostedFile.SaveAs(strPath & ReFileName)
            Else
                'AccessPermission(strPath)
                System.IO.Directory.CreateDirectory(strPath)
                IFileUpload.PostedFile.SaveAs(strPath & ReFileName)
            End If
            'TakeFileUpload.MergePdf(strPath & ReFileName, strMergePdf, strPath & "New" + ReFileName)
            strReturn = ReFileName
        End If
        Return strReturn
    End Function

    Public Shared Function ReplaceFileExtension(ByVal strFileName As String) As String
        Dim periodPos As Integer = strFileName.ToString().LastIndexOf(".")
        Return strFileName.Substring(0, periodPos) + ".pdf"
    End Function
End Class
