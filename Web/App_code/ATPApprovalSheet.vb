Imports System.Data
Imports System.IO
Imports BusinessLogic
Imports Microsoft.VisualBasic

Public Class ATPApprovalSheet
    Dim controller As New HCPTController
    Dim objBo As New BODashBoard
    Dim objBOS As New BOSiteDocs
    Dim docInfo As DOCTransactionInfo
    Dim aliasName As String = ""
    Dim docPath As String = ""
    Dim webControl As New System.Web.UI.Page

    Public Sub New()

    End Sub

    Public Sub New(ByVal _objectDoc As DOCTransactionInfo)
        Me.docInfo = _objectDoc
        Dim dt As DataTable = objBOS.getbautdocdetailsNEW(docInfo.DocInf.DocId)
        If dt.Rows.Count > 0 Then
            aliasName = dt.Rows(0).Item("alias_docname").ToString
        End If
        dt = objBo.DigitalSign(Convert.ToInt32(docInfo.SNO))
        If dt.Rows.Count > 0 Then
            docPath = dt.Rows(0)("docpath").ToString()
        End If
    End Sub

    Public Function generateATPApprovalSheet() As Boolean
        Dim isSucceed As Boolean = True
        Dim info As SiteInfo = controller.GetSiteInfoDetail(docInfo.SiteInf.PackageId)
        If info IsNot Nothing Then
            Try
                Dim atpapprovaltemplate As String = readApprovalTemplate()
                If Not String.IsNullOrEmpty(atpapprovaltemplate) Then
                    Dim sb As StringBuilder = New StringBuilder()
                    Dim filenameorg As String
                    Dim ReFileName As String
                    Dim ft As String = ConfigurationManager.AppSettings("Type") & info.Scope & "-" & info.PackageId & "\"
                    filenameorg = "DocApprovalSheet-" & Format(CDate(DateTime.Now), "ddMMyyyyHHss")
                    ReFileName = filenameorg & ".htm"
                    Dim docpath As String = info.SiteNo & ft
                    'Dim strPath As String = ConfigurationManager.AppSettings(GetFolderPath(scontype)) + docpath
                    sb.Append(atpapprovaltemplate)
                    sb.Replace("[LOGOHCPT]", "<img src='https://hcptdemo.nsnebast.com/Images/three-logo.png' height='46px' width='60px' alt='hcptlogo' />")
                    sb.Replace("[LOGONSN]", "<img src='https://hcptdemo.nsnebast.com/Images/nokia.png' height='36px' width='104px' alt='nokialogo'/>")
                    sb.Replace("[DIVPONOTXT]", info.PONO)
                    sb.Replace("[DIVSITENAMETXT]", info.SiteName)
                    sb.Replace("[DIVSITEIDTXT]", info.SiteNo)
                    sb.Replace("[DIVSCOPETXT]", info.Scope)
                    sb.Replace("[DOCNAME]", docInfo.DocInf.DocName)
                    sb.Replace("[DIVREVIEWERTXT]", getATPReviewer(docInfo.DocInf.DocId, docInfo.SiteInf.PackageId))
                    Dim _docPath As String = getDefaultDocPath(docInfo.DocInf.DocId)
                    Dim orgdocpath As String = _docPath & ReFileName
                    If Not System.IO.Directory.Exists(_docPath) Then
                        System.IO.Directory.CreateDirectory(_docPath)
                    End If
                    Try
                        Dim sw As HtmlTextWriter = New HtmlTextWriter(New StreamWriter(_docPath & ReFileName, False, UnicodeEncoding.UTF8))
                        sw.WriteLine(sb.ToString())
                        sw.Close()
                        sw.Dispose()
                    Catch ex As Exception
                        isSucceed = False
                        controller.ErrorLogInsert(150, "GenerateATPApprovalSheet", ex.Message.ToString(), "ATPApprovalSheet Class")
                    End Try
                    If isSucceed = True Then
                        'Dim newdocpath As String = Common.TakeFileUpload.ConvertAnyFormatToPDFHtmlNew(_docPath & ReFileName, _docPath, filenameorg)
                        Dim newdocpath As String = EBASTFileUpload.ConvertAnyFormatToPDFHtmlNew(_docPath & ReFileName, _docPath, filenameorg)
                        'controller.ATPDocCompleted_U(Integer.Parse(ConfigurationManager.AppSettings("ATP")), info.PackageId, getDefaultDocPath2(docInfo.DocInf.DocId) & newdocpath, Integer.Parse(ConfigurationManager.AppSettings("ATPDoc")))
                        'Doc Approval Sheet Merge syntax and Change code Irvan 22 Sep 2017----------------------------------------------------------------------------------------
                        Dim firstDocPath As String = _docPath & newdocpath
                        Dim SecondDocPath As String = ConfigurationManager.AppSettings("FPath") & docpath
                        Dim strResult As String = New NSNPDFController().MergePdfNew(firstDocPath, SecondDocPath, _docPath & "Approved_" & aliasName & ".pdf")
                        controller.ATPDocCompleted_U(docInfo.DocInf.DocId, info.PackageId, getDefaultDocPath2(docInfo.DocInf.DocId) & "Approved_" & aliasName & ".pdf", Integer.Parse(ConfigurationManager.AppSettings("ATPDoc")))
                        '-----------------------------End of Line new syntax-------------------------------------------------------------------------------------------------------
                    End If
                Else
                    isSucceed = False
                End If
            Catch ex As Exception
                isSucceed = False
                controller.ErrorLogInsert(150, "GenerateATPApprovalSheet", ex.Message.ToString(), "ATPApprovalSheet Class")
            End Try
        End If
        Return isSucceed
    End Function

    Private Function getATPReviewer(ByVal docid As Integer, ByVal wpid As String) As String
        Dim count As Integer = 1
        Dim strReviewer As String = String.Empty
        For Each info As DOCTransactionInfo In controller.GetDocReviewerLog(docid, wpid)
            Dim strCompany As String = String.Empty
            Dim strTaskName As String = String.Empty
            If count > 1 Then
                strReviewer += "<br/>"
            End If
            If info.UserInf.UserType.ToLower().Equals("s") Then
                strCompany = "Partner"
            ElseIf info.UserInf.UserType.ToLower().Equals("n") Then
                strCompany = "NSN"
            ElseIf info.UserInf.UserType.ToLower().Equals("h") Then
                strCompany = "Huawei"
            ElseIf info.UserInf.UserType.ToLower().Equals("c") Then
                strCompany = "H3I"
            End If

            If info.TaskId = 1 Then
                strTaskName = "Prepared by"
            Else
                strTaskName = "Approved by"
            End If
            strReviewer += strCompany & " " & strTaskName & " <b>" & info.UserInf.Username & " as " & info.UserInf.SignTitle & "</b> on <b>" & String.Format("{0:dd-MMM-yyyy hh:mm:ss}", info.EndDateTime) & "</b>"
            count += 1
        Next
        Return strReviewer
    End Function

    Private Function readApprovalTemplate() As String
        Dim serverpath As String = webControl.Server.MapPath("ApprovalSheet_Template")
        Dim filepath As String = serverpath + "\" + "ATP_ApprovalSheet.htm"
        'filepath = HttpContext.Current.Server.MapPath("~/Template/EBAST/ABSOLUTE_TEMPLATE/ATP_ApprovalSheet.htm")
        'filepath = "E:\\EBAST\\ABSOLUTE_TEMPLATE\\ATP_ApprovalSheet.htm"
        Dim content As String = String.Empty
        Try
            Using reader As StreamReader = New StreamReader(filepath, System.Text.Encoding.UTF8)
                content = reader.ReadToEnd()
            End Using
        Catch ex As Exception
            content = String.Empty
            controller.ErrorLogInsert(150, "ReadAppprovalTemplate", ex.Message.ToString(), "ATPApprovalSheet Class")
        End Try

        If String.IsNullOrEmpty(content) Then
            Try
                filepath = "E:\\EBAST\\ABSOLUTE_TEMPLATE\\ATP_ApprovalSheet.htm"
                Console.WriteLine("Re-attempt to read Approval sheet template")
                Using reader As StreamReader = New StreamReader(filepath, System.Text.Encoding.UTF8)
                    content = reader.ReadToEnd()
                End Using
            Catch ex As Exception
                controller.ErrorLogInsert(150, "ReadAppprovalTemplate", ex.Message.ToString(), "ATPApprovalSheet Class")
                content = String.Empty
            End Try
        End If

        Return content
    End Function

    Private Function getDefaultDocPath(ByVal docid As Integer) As String
        Dim dt As DataTable = objBOS.getbautdocdetailsNEW(docid) '(Constants._Doc_SSR)
        Dim sec As String = dt.Rows(0).Item("sec_name").ToString
        Dim subsec As String = dt.Rows(0).Item("subsec_name").ToString
        Dim aliasname As String = dt.Rows(0).Item("alias_docname").ToString
        aliasname = aliasname
        Dim secpath As String = ""
        Dim ft As String = ""
        Dim path As String = ""
        Dim filenameorg As String
        Dim FileNameOnly As String = "-" & "ATPFINAL" & "-"
        filenameorg = docInfo.SiteInf.SiteNo & FileNameOnly & Format(CDate(DateTime.Now), "ddMMyyyyHHss")
        Dim ReFileName As String = filenameorg & ".htm"
        secpath = IIf(subsec = "" Or subsec = "0", sec & "\", sec & "\" & subsec & "\")
        ft = ConfigurationManager.AppSettings("Type") & docInfo.SiteInf.Scope & "-" & docInfo.SiteInf.PackageId & "\"
        path = ConfigurationManager.AppSettings("Fpath") & docInfo.SiteInf.SiteNo.TrimEnd.TrimStart() & ft & secpath

        Return path
    End Function

    Private Function getDefaultDocPath2(ByVal docid As Integer) As String
        Dim dt As DataTable = objBOS.getbautdocdetailsNEW(docid) '(Constants._Doc_SSR)
        Dim sec As String = dt.Rows(0).Item("sec_name").ToString
        Dim subsec As String = dt.Rows(0).Item("subsec_name").ToString
        Dim secpath As String = ""
        Dim ft As String = ""
        Dim path As String = ""
        Dim filenameorg As String
        Dim FileNameOnly As String = "-" & "ATPFINAL" & "-"
        filenameorg = docInfo.SiteInf.SiteNo & FileNameOnly & Format(CDate(DateTime.Now), "ddMMyyyyHHss")
        Dim ReFileName As String = filenameorg & ".htm"
        secpath = IIf(subsec = "" Or subsec = "0", sec & "\", sec & "\" & subsec & "\")
        ft = ConfigurationManager.AppSettings("Type") & docInfo.SiteInf.Scope & "-" & docInfo.SiteInf.PackageId & "\"
        path = docInfo.SiteInf.SiteNo.TrimEnd.TrimStart() & ft & secpath

        Return path
    End Function
End Class