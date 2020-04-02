Imports Microsoft.VisualBasic
Imports BCL.easyPDF7.Interop.EasyPDFLoader
Imports BCL.easyPDF7.Interop.EasyPDFPrinter
Imports System.IO
Imports System.Security.AccessControl

Public Class EBASTFileUpload
    Public Shared Function ConvertAnyFormatToPDF(ByVal IFileUpload As System.Web.UI.WebControls.FileUpload, ByVal strPath As String, ByVal strMergePdf As String) As String
        Dim FileNamePath As String = IFileUpload.PostedFile.FileName
        Dim FileNameOnly As String = System.IO.Path.GetFileName(IFileUpload.PostedFile.FileName)
        Dim ReFileName As String = Format(CDate(DateTime.Now), "ddMMyyyyHHss") & FileNameOnly
        Dim strReturn As String
        Dim oLoader As Loader
        Dim oPrinter As Printer
        Dim oPrintJob As PrintJob = Nothing
        If System.IO.Path.GetExtension(IFileUpload.PostedFile.FileName).ToLower = ".pdf" Then
            If System.IO.Directory.Exists(strPath) Then
                IFileUpload.PostedFile.SaveAs(strPath & ReFileName)
            Else
                'AccessPermission(strPath)
                System.IO.Directory.CreateDirectory(strPath)
                IFileUpload.PostedFile.SaveAs(strPath & ReFileName)
            End If
            MergePdf(strPath & ReFileName, strMergePdf, strPath & "New" + ReFileName)
            strReturn = "New" + ReFileName
        Else
            Try
                Dim newFile As String = ReplaceFileExtension(ReFileName)
                Dim type As Type
                type = System.Type.GetTypeFromProgID("easyPDF.Loader.7")
                oLoader = DirectCast(Activator.CreateInstance(type), Loader)
                oPrinter = oLoader.LoadObject("easyPDF.Printer.7")
                oPrinter.DefaultPrinter = "easyPDF SDK 7"
                oPrintJob = oPrinter.IEPrintJob
                Dim oIESetting As IESetting
                oIESetting = oPrintJob.IESetting
                oIESetting.Header = ""
                oIESetting.Footer = ""
                oIESetting.Save()
                Dim oPrintMonitor As PrinterMonitor
                oPrintMonitor = oPrinter.PrinterMonitor
                'Set initialization timeout value to 1 minutes 
                oPrintJob.InitializationTimeout = 0 * 60000
                'Set page conversion timeout value to 1 minutes 
                oPrintJob.PageConversionTimeout = 0 * 60000
                'Set file conversion timeout value to 5 minutes 
                oPrintJob.FileConversionTimeout = 0 * 60000
                'bugfix100901 replacing empty spaces after \
                strPath = strPath.Replace(" \", "\")
                If System.IO.Directory.Exists(strPath) Then
                    IFileUpload.PostedFile.SaveAs(strPath & ReFileName)
                Else
                    System.IO.Directory.CreateDirectory(strPath)
                    IFileUpload.PostedFile.SaveAs(strPath & ReFileName)
                End If
                If (System.IO.Path.GetExtension(IFileUpload.PostedFile.FileName).ToLower = ".pdf") Then
                    MergePdf(strPath & ReFileName, strMergePdf, strPath & "New" + ReFileName)
                    strReturn = "New" + ReFileName
                Else
                    If Not System.IO.Directory.Exists(strPath + newFile) Then
                        oPrintJob.PrintOut(strPath & ReFileName, strPath + newFile)
                    Else
                        System.IO.File.Delete(strPath + newFile)
                        oPrintJob.PrintOut(strPath & ReFileName, strPath + newFile)
                    End If
                    If System.IO.Directory.Exists(strPath + newFile) Then
                        System.IO.File.Delete(strPath + newFile)
                    End If
                    MergePdf(strPath & newFile, strMergePdf, strPath & "New" + newFile)
                    strReturn = "New" + newFile
                End If
            Catch ex As System.Runtime.InteropServices.COMException
                strReturn = ex.Message.ToString
                If (ex.ErrorCode = prnResult.PRN_R_CONVERSION_FAILED) And (Not oPrintJob Is Nothing) Then
                    strReturn = oPrintJob.ConversionResultMessage
                    Dim result As prnConversionResult = oPrintJob.ConversionResult
                    If (result = prnConversionResult.PRN_CR_CONVERSION Or result = prnConversionResult.PRN_CR_CONVERSION_INIT Or result = prnConversionResult.PRN_CR_CONVERSION_PRINT) Then
                        strReturn = oPrintJob.PrinterResultMessage
                    End If
                End If
            Finally
                oPrintJob = Nothing
                oPrinter = Nothing
            End Try
        End If
        Return strReturn
    End Function

    Public Shared Function ConvertAnyFormatToPDF(ByVal IFileUpload As System.Web.UI.WebControls.FileUpload, ByVal strPath As String) As String
        Dim FileNamePath As String = IFileUpload.PostedFile.FileName
        Dim FileNameOnly As String = System.IO.Path.GetFileName(IFileUpload.PostedFile.FileName)
        Dim ReFileName As String = Format(CDate(DateTime.Now), "ddMMyyyyHHss") & FileNameOnly
        Dim strReturn As String
        Dim oLoader As Loader
        Dim oPrinter As Printer
        Dim oPrintJob As PrintJob = Nothing
        Dim fileExt As String = System.IO.Path.GetExtension(IFileUpload.PostedFile.FileName).ToLower
        If System.IO.Path.GetExtension(IFileUpload.PostedFile.FileName).ToLower = ".pdf" Then
            If System.IO.Directory.Exists(strPath) Then
                IFileUpload.PostedFile.SaveAs(strPath & ReFileName)
            Else
                'AccessPermission(strPath)
                System.IO.Directory.CreateDirectory(strPath)
                IFileUpload.PostedFile.SaveAs(strPath & ReFileName)
            End If
            strReturn = ReFileName
        Else
            Try
                Dim newFile As String = ReplaceFileExtension(ReFileName)
                Dim type As Type
                type = System.Type.GetTypeFromProgID("easyPDF.Loader.7")
                oLoader = DirectCast(Activator.CreateInstance(type), Loader)
                oPrinter = oLoader.LoadObject("easyPDF.Printer.7")
                oPrinter.DefaultPrinter = "easyPDF SDK 7"
                oPrintJob = oPrinter.PrintJob
                Dim oPrintMonitor As PrinterMonitor
                oPrintMonitor = oPrinter.PrinterMonitor
                If System.IO.Directory.Exists(strPath) Then
                    IFileUpload.PostedFile.SaveAs(strPath & ReFileName)
                Else
                    System.IO.Directory.CreateDirectory(strPath)
                    IFileUpload.PostedFile.SaveAs(strPath & ReFileName)
                End If
                If (System.IO.Path.GetExtension(IFileUpload.PostedFile.FileName).ToLower = ".pdf") Then
                    strReturn = ReFileName
                Else
                    '---bugfix:100621
                    If fileExt = ".xls" Or fileExt = ".xlsx" Then
                        oPrintJob = oPrinter.ExcelPrintJob
                    ElseIf fileExt = ".ppt" Or fileExt = ".pptx" Then
                        oPrintJob = oPrinter.PowerPointPrintJob
                    ElseIf fileExt = ".doc" Or fileExt = ".docx" Then
                        oPrintJob = oPrinter.WordPrintJob
                    Else
                        oPrintJob = oPrinter.PrintJob
                    End If
                    '---
                    If System.IO.Directory.Exists(strPath + newFile) Then
                        System.IO.File.Delete(strPath + newFile)
                    End If
                    'bugfix100901 replacing empty spaces after \
                    strPath = strPath.Replace(" \", "\")
                    oPrintJob.PrintOut(strPath & ReFileName, strPath + newFile)
                    strReturn = newFile
                End If
            Catch ex As System.Runtime.InteropServices.COMException
                strReturn = ex.Message.ToString
                If (ex.ErrorCode = prnResult.PRN_R_CONVERSION_FAILED And Not oPrintJob Is Nothing) Then
                    strReturn = oPrintJob.ConversionResultMessage
                    Dim result As prnConversionResult = oPrintJob.ConversionResult
                    If (result = prnConversionResult.PRN_CR_CONVERSION Or result = prnConversionResult.PRN_CR_CONVERSION_INIT Or result = prnConversionResult.PRN_CR_CONVERSION_PRINT) Then
                        strReturn = oPrintJob.PrinterResultMessage
                    End If
                End If
            Finally
                oPrintJob = Nothing
                oPrinter = Nothing
            End Try
        End If
        Return strReturn
    End Function

    Public Shared Function ConvertAnyFormatToPDFHtmlNew(ByVal FileNamePath As String, ByVal strPath As String, ByVal filename As String) As String
		Dim isSucceed As Boolean = True
        Dim ReFileName As String = filename
        ReFileName = ReFileName + ".pdf"
        Dim strReturn As String
        Dim oLoader As Loader
        Dim oPrinter As Printer
        Dim oPrintJob As PrintJob
        Dim type As Type = System.Type.GetTypeFromProgID("easyPDF.Loader.7")
        oLoader = DirectCast(Activator.CreateInstance(type), Loader)
        oPrinter = oLoader.LoadObject("easyPDF.Printer.7")
        oPrinter.DefaultPrinter = "easyPDF SDK 7"
        'Dim oPrintJobType As prnPrintJobType
        'oPrintJobType = oPrinter.GetPrintJobTypeOf(IFileUpload.PostedFile.FileName.ToString())
        oPrintJob = oPrinter.IEPrintJob
        'Set initialization timeout value to 1 minutes 
        oPrintJob.InitializationTimeout = 0 * 60000
        'Set page conversion timeout value to 1 minutes 
        oPrintJob.PageConversionTimeout = 0 * 60000
        'Set file conversion timeout value to 5 minutes 
        oPrintJob.FileConversionTimeout = 0 * 60000
        Dim oIESetting As IESetting
        oIESetting = oPrintJob.IESetting
        oIESetting.Header = ""
        oIESetting.Footer = ""
        oIESetting.Save()
        Dim oPrintMonitor As PrinterMonitor
        oPrintMonitor = oPrinter.PrinterMonitor
        Try
            If Not System.IO.Directory.Exists(strPath + ReFileName) Then
                oPrintJob.PrintOut(FileNamePath, strPath + ReFileName)
            Else
                System.IO.File.Delete(strPath + ReFileName)
                oPrintJob.PrintOut(FileNamePath, strPath + ReFileName)
            End If
            strReturn = ReFileName
        Catch ex As System.Runtime.InteropServices.COMException
            strReturn = ex.Message.ToString
            If (ex.ErrorCode = prnResult.PRN_R_CONVERSION_FAILED And Not oPrintJob Is Nothing) Then
                strReturn = oPrintJob.ConversionResultMessage
                Dim result As prnConversionResult = oPrintJob.ConversionResult
                If (result = prnConversionResult.PRN_CR_CONVERSION Or result = prnConversionResult.PRN_CR_CONVERSION_INIT Or result = prnConversionResult.PRN_CR_CONVERSION_PRINT) Then
                    strReturn = oPrintJob.PrinterResultMessage
                End If
            End If
            'strReturn = ReFileName
        Finally
            oPrintJob = Nothing
            oPrinter = Nothing
        End Try
		If isSucceed = False Then
            Dim paccontrol As New PACController
            paccontrol.ErrorLogInsert(1, "ConvertAnyFormatToPDFHtmlNew", "Error: " & strReturn, "NON-SP")
        End If
		
        Return strReturn
    End Function

    Public Shared Function ConvertAnyFormatToPDFWithReFilename(ByVal IFileUpload As System.Web.UI.WebControls.FileUpload, ByVal strPath As String, ByVal refilename As String) As String
        Dim FileNamePath As String = IFileUpload.PostedFile.FileName
        'Dim FileNameOnly As String = System.IO.Path.GetFileName(IFileUpload.PostedFile.FileName)
        'Dim ReFileName As String = Format(CDate(DateTime.Now), "ddMMyyyyHHss") & FileNameOnly
        Dim strReturn As String
        Dim oLoader As Loader
        Dim oPrinter As Printer = Nothing
        Dim oPrintJob As PrintJob = Nothing
        Dim fileExt As String = System.IO.Path.GetExtension(IFileUpload.PostedFile.FileName).ToLower
        If System.IO.Path.GetExtension(IFileUpload.PostedFile.FileName).ToLower = ".pdf" Then
            If System.IO.Directory.Exists(strPath) Then
                IFileUpload.PostedFile.SaveAs(strPath & refilename)
            Else
                'AccessPermission(strPath)
                System.IO.Directory.CreateDirectory(strPath)
                IFileUpload.PostedFile.SaveAs(strPath & refilename)
            End If
            strReturn = refilename
        Else
            Try
                Dim newFile As String = ReplaceFileExtension(refilename)
                Dim type As Type
                type = System.Type.GetTypeFromProgID("easyPDF.Loader.7")
                oLoader = DirectCast(Activator.CreateInstance(type), Loader)
                oPrinter = oLoader.LoadObject("easyPDF.Printer.7")
                oPrinter.DefaultPrinter = "easyPDF SDK 7"
                oPrintJob = oPrinter.PrintJob
                Dim oPrintMonitor As PrinterMonitor
                oPrintMonitor = oPrinter.PrinterMonitor
                If System.IO.Directory.Exists(strPath) Then
                    IFileUpload.PostedFile.SaveAs(strPath & ReFileName)
                Else
                    System.IO.Directory.CreateDirectory(strPath)
                    IFileUpload.PostedFile.SaveAs(strPath & ReFileName)
                End If
                If (System.IO.Path.GetExtension(IFileUpload.PostedFile.FileName).ToLower = ".pdf") Then
                    strReturn = ReFileName
                Else
                    '---bugfix:100621
                    If fileExt = ".xls" Or fileExt = ".xlsx" Then
                        oPrintJob = oPrinter.ExcelPrintJob
                    ElseIf fileExt = ".ppt" Or fileExt = ".pptx" Then
                        oPrintJob = oPrinter.PowerPointPrintJob
                    ElseIf fileExt = ".doc" Or fileExt = ".docx" Then
                        oPrintJob = oPrinter.WordPrintJob
                    Else
                        oPrintJob = oPrinter.PrintJob
                    End If
                    '---
                    If System.IO.Directory.Exists(strPath + newFile) Then
                        System.IO.File.Delete(strPath + newFile)
                    End If
                    'bugfix100901 replacing empty spaces after \
                    strPath = strPath.Replace(" \", "\")
                    oPrintJob.PrintOut(strPath & refilename, strPath + newFile)
                    strReturn = newFile
                End If
            Catch ex As System.Runtime.InteropServices.COMException
                strReturn = ex.Message.ToString
                If (ex.ErrorCode = prnResult.PRN_R_CONVERSION_FAILED And Not oPrintJob Is Nothing) Then
                    strReturn = oPrintJob.ConversionResultMessage
                    Dim result As prnConversionResult = oPrintJob.ConversionResult
                    If (result = prnConversionResult.PRN_CR_CONVERSION Or result = prnConversionResult.PRN_CR_CONVERSION_INIT Or result = prnConversionResult.PRN_CR_CONVERSION_PRINT) Then
                        strReturn = oPrintJob.PrinterResultMessage
                    End If
                End If
            Finally
                oPrintJob = Nothing
                oPrinter = Nothing
            End Try
        End If
        Return strReturn
    End Function

    Private Shared Function MergePdf(ByVal pdfName As String, ByVal pdfBlank As String, ByVal outputFileFullName As String) As Boolean
        Dim result As Boolean = True
        Try
            Dim pdfMerger As New org.apache.pdfbox.util.PDFMergerUtility
            With pdfMerger
                .setDestinationFileName(outputFileFullName)
                .addSource(pdfName)
                .addSource(pdfBlank)
                .mergeDocuments()
            End With
        Catch ex As Exception
        End Try
        Return result
    End Function

    Public Shared Function ReplaceFileExtension(ByVal strFileName As String) As String
        Dim periodPos As Integer = strFileName.ToString().LastIndexOf(".")
        Return strFileName.Substring(0, periodPos) + ".pdf"
    End Function

    Public Shared Function GetFileExtension(ByVal strExtension As String) As Integer
        Dim intReturn As Integer
        Select Case strExtension.ToLower()
            Case ".doc"
                intReturn = 1
                Exit Select
            Case ".docx"
                intReturn = 1
                Exit Select
            Case ".rtf"
                intReturn = 1
                Exit Select
            Case ".wpd"
                intReturn = 1
                Exit Select
            Case ".wps"
                intReturn = 1
                Exit Select
            Case ".txt"
                intReturn = 1
                Exit Select
            Case ".xls"
                intReturn = 2
                Exit Select
            Case ".xlsx"
                intReturn = 2
                Exit Select
            Case ".ppt"
                intReturn = 3
                Exit Select
            Case ".pps"
                intReturn = 3
                Exit Select
            Case ".pptx"
                intReturn = 3
                Exit Select
            Case ".jpeg"
                intReturn = 4
                Exit Select
            Case ".jpg"
                intReturn = 4
                Exit Select
            Case ".tif"
                intReturn = 4
                Exit Select
            Case ".gif"
                intReturn = 4
                Exit Select
            Case ".png"
                intReturn = 4
                Exit Select
            Case ".bmp"
                intReturn = 4
                Exit Select
            Case ".psd"
                intReturn = 4
                Exit Select
            Case ".psp"
                intReturn = 4
                Exit Select
            Case Else
                intReturn = 4
                Exit Select
        End Select
        Return intReturn
    End Function

    Private Function AccessPermission(ByVal strFile As String) As String
        Try
            AddDirectorySecurity(strFile, "MYDOMAIN\MyAccount", FileSystemRights.ReadData, AccessControlType.Allow)
            Console.WriteLine("Removing access control entry from " + strFile)
            'Remove the access control entry from the directory.
            RemoveDirectorySecurity(strFile, "MYDOMAIN\MyAccount", FileSystemRights.ReadData, AccessControlType.Allow)
            Console.WriteLine("Done.")
        Catch e As Exception
            Console.WriteLine(e)
        End Try
        Return Console.ReadLine()
    End Function

    Sub AddDirectorySecurity(ByVal FileName As String, ByVal Account As String, ByVal Rights As FileSystemRights, ByVal ControlType As AccessControlType)
        'Create a new DirectoryInfoobject.
        Dim dInfo As New DirectoryInfo(FileName)
        'Get a DirectorySecurity object that represents the 
        'current security settings.
        Dim dSecurity As DirectorySecurity = dInfo.GetAccessControl()
        'Add the FileSystemAccessRule to the security settings. 
        dSecurity.AddAccessRule(New FileSystemAccessRule(Account, Rights, ControlType))
        'Set the new access settings.
        dInfo.SetAccessControl(dSecurity)
    End Sub
    'Removes an ACL entry on the specified directory for the specified account.
    Sub RemoveDirectorySecurity(ByVal FileName As String, ByVal Account As String, ByVal Rights As FileSystemRights, ByVal ControlType As AccessControlType)
        'Create a new DirectoryInfo object.
        Dim dInfo As New DirectoryInfo(FileName)
        'Get a DirectorySecurity object that represents the 
        'current security settings.
        Dim dSecurity As DirectorySecurity = dInfo.GetAccessControl()
        'Add the FileSystemAccessRule to the security settings. 
        dSecurity.RemoveAccessRule(New FileSystemAccessRule(Account, Rights, ControlType))
        'Set the new access settings.
        dInfo.SetAccessControl(dSecurity)
    End Sub
End Class
