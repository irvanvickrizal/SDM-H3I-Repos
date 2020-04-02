Imports System
Imports System.Data
Imports System.Web
Imports System.Collections
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports System.IO
Imports Common

Imports Entities
Imports BusinessLogic
Imports System.Configuration

    '/ <summary>
    '/ This web method will provide an web method to load any
    '/ file onto the server; the UploadFile web method
    '/ will accept the report and store it in the local file system.
'/ </summary>
<WebService(Namespace:="http://tempuri.org/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class EbastWebservice
    Inherits System.Web.Services.WebService
    Dim objBOAT As New BoAuditTrail
    Dim objdbutil As New DBUtil
    Dim objDo As New ETWFTransaction
    Dim objetsitedoc As New ETSiteDoc
    Dim objbositedoc As New BOSiteDocs
    Dim objBOM As New BOMailReport
    Dim DtProcess As New DataTable
    Dim dt As DataTable
    Dim dtn As DataTable
    Dim objdb As New DBUtil
    Dim objET As New ETAuditTrail
    Dim objBO As New BOSiteDocs

    <WebMethod(BufferResponse:=True, Description:="Folder Creation")> _
 Public Function FTPFloderCreation(ByRef strOut As String, ByVal strProcess As String, ByVal PoNO As String, ByVal Siteno As String, ByVal DocId As String, ByVal Scope As String) As String
        Dim newStrResult As String = ""
        Try
            'Creating Directry
            Dim DocPath As String = "", strPath As String, strPathNew As String
            Dim dsPath As DataTable = objBO.getbautdocdetailsNEW(DocId) '(Constants._Doc_SSR)
            Dim sec As String = dsPath.Rows(0).Item("sec_name").ToString
            Dim subsec As String = dsPath.Rows(0).Item("subsec_name").ToString
            Dim secpath As String = IIf(subsec = "" Or subsec = "0", sec & "\", sec & "\" & subsec & "\")

            If dsPath.Rows.Count >= 1 Then
                DocPath = dsPath.Rows(0)("docname").ToString
            End If
            If strProcess = "ti2g" Then
                strPath = ConfigurationManager.AppSettings("Fpathti2g") & Siteno & "\" & strProcess & "\" & PoNO & "-" & Scope & "\" & secpath

            ElseIf strProcess = "ti3g" Then
                strPath = ConfigurationManager.AppSettings("Fpathti3g") & Siteno & "\" & strProcess & "\" & PoNO & "-" & Scope & "\" & secpath

            ElseIf strProcess = "sis2g" Then
                strPath = ConfigurationManager.AppSettings("Fpathsis2g") & Siteno & "\" & strProcess & "\" & PoNO & "-" & Scope & "\" & secpath

            ElseIf strProcess = "sis3g" Then
                strPath = ConfigurationManager.AppSettings("Fpathsis3g") & Siteno & "\" & strProcess & "\" & PoNO & "-" & Scope & "\" & secpath

            ElseIf strProcess = "sitac2g" Then

                strPath = ConfigurationManager.AppSettings("Fpathsitac2g") & Siteno & "\" & strProcess & "\" & PoNO & "-" & Scope & "\" & secpath
            ElseIf strProcess = "sitac3g" Then
                strPath = ConfigurationManager.AppSettings("Fpathsitac3g") & Siteno & "\" & strProcess & "\" & PoNO & "-" & Scope & "\" & secpath

            ElseIf strProcess = "cme2g" Then
                strPath = ConfigurationManager.AppSettings("Fpathcme2g") & Siteno & "\" & strProcess & "\" & PoNO & "-" & Scope & "\" & secpath


            Else
                strPath = ConfigurationManager.AppSettings("Fpathcme3g") & Siteno & "\" & strProcess & "\" & PoNO & "-" & Scope & "\" & secpath

            End If
            strPathNew = Siteno & "\" & strProcess & "\" & PoNO & "-" & Scope & "\" & secpath
            If System.IO.Directory.Exists(strPath) Then

            Else
                'AccessPermission(strPath)
                System.IO.Directory.CreateDirectory(strPath)
            End If
            strOut = "ok"
            Return strPathNew

        Catch ex As Exception
            Return ex.Message.ToString()
            strOut = "no"
        End Try
    End Function
    <WebMethod(BufferResponse:=True, Description:="File Upload")> _
   Public Function FTPFileUploadProcess(ByVal strPathNew As String, ByVal fileName As String, ByVal strProcess As String, ByVal PoNO As String, ByVal Siteno As String, ByVal SiteId As String, ByVal DocId As String, ByVal Scope As String, ByVal Version As String, ByVal strUserName As String, ByVal userid As Integer, ByVal roleid As Integer) As String
        Dim newStrResult As String = "ok"
        Try

            Dim DocPath As String = "", strPath As String = ConfigurationManager.AppSettings("Fpath") & strPathNew
            'Dim dsPath As DataTable = objdb.ExeQueryDT("exec [uspWebDocPath] " & DocId & ",'" & strProcess & "'", "Sitesdetails")
            'If dsPath.Rows.Count >= 1 Then
            '    DocPath = dsPath.Rows(0)("DocPath").ToString
            'End If
            If strProcess = "ti2g" Then
                strPath = ConfigurationManager.AppSettings("Fpathti2g") & strPathNew

            ElseIf strProcess = "ti3g" Then
                strPath = ConfigurationManager.AppSettings("Fpathti3g") & strPathNew

            ElseIf strProcess = "sis2g" Then
                strPath = ConfigurationManager.AppSettings("Fpathsis2g") & strPathNew

            ElseIf strProcess = "sis3g" Then
                strPath = ConfigurationManager.AppSettings("Fpathsis3g") & strPathNew

            ElseIf strProcess = "sitac2g" Then

                strPath = ConfigurationManager.AppSettings("Fpathsitac2g") & strPathNew
            ElseIf strProcess = "sitac3g" Then
                strPath = ConfigurationManager.AppSettings("Fpathsitac3g") & strPathNew

            ElseIf strProcess = "cme2g" Then
                strPath = ConfigurationManager.AppSettings("Fpathcme2g") & strPathNew
            Else
                strPath = ConfigurationManager.AppSettings("Fpathcme3g") & strPathNew

            End If

            Dim keyval As Int16
            Select Case objbositedoc.DocUploadverify(SiteId, DocId, Version).ToString
                Case 1  ''This document not attached to this site
                    keyval = 1
                Case 2 '' means document was already uploaded for this version of site,do u want to overwrite
                    keyval = 2
                Case 3 '' means document was not yet uploaded for thissite
                    keyval = 3
                Case 4 'means document already processed for sencod stage cannot upload
                    keyval = 0
            End Select
            If newStrResult.ToLower = "ok" Then
                Dim strResult As String = "1"
                strResult = DOInsertTrans(SiteId, DocId, Version, strPath, Convert.ToInt32(userid), strUserName)
                If strResult = "0" Or strResult = "1" Then

                    DocPath = strPathNew + fileName
                Else
                    If (Common.TakeFileUpload.MergePdfNew(strPath + fileName, strPath + strResult, strPath + "NewMerge.pdf")) Then
                        DocPath = strPathNew & "NewMerge.pdf"
                    Else
                        DocPath = strPathNew + fileName
                    End If

                End If

                With objetsitedoc
                    .SiteID = SiteId
                    .DocId = DocId
                    .IsUploded = 1
                    .Version = Version
                    .keyval = keyval
                    .DocPath = DocPath
                    .AT.RStatus = Constants.STATUS_ACTIVE
                    .AT.LMBY = strUserName
                    .orgDocPath = DocPath
                    .PONo = PoNO
                End With
                'objbositedoc.updatedocupload(objetsitedoc)
                Dim hcptcontrol As New HCPTController
                hcptcontrol.HCPT_UpdateDocUpload(objetsitedoc)
                sendmail2(SiteId, DocId, Siteno)
                chek4alldoc(SiteId, Version, PoNO, Siteno, strProcess) ' for messaage to previous screen ' and saving final docupload date in reporttable

                AuditTrail(PoNO, SiteId, DocId, userid, roleid, Scope)
            End If
            Return newStrResult

        Catch ex As Exception
            Return ex.Message.ToString()
        End Try
    End Function
    <WebMethod(BufferResponse:=True, Description:="File Upload")> _
    Public Function FileUploadProcess(ByVal f() As Byte, ByVal fileName As String, ByVal strProcess As String, ByVal PoNO As String, ByVal Siteno As String, ByVal SiteId As String, ByVal DocId As String, ByVal Scope As String, ByVal Version As String, ByVal strUserName As String, ByVal userid As Integer, ByVal roleid As Integer) As String
        Dim newStrResult As String = ""
        Try
            'Creating Directry
            Dim DocPath As String = "", strPath As String, strPathNew As String
            Dim dsPath As DataTable = objdb.ExeQueryDT("exec [uspWebDocPath] " & DocId & ",'" & strProcess & "'", "Sitesdetails")
            If dsPath.Rows.Count >= 1 Then
                DocPath = dsPath.Rows(0)("DocPath").ToString
            End If
            strPath = ConfigurationManager.AppSettings("Fpath") & Siteno & "\" & strProcess & "\" & PoNO & "-" & Scope & "\" & DocPath
            strPathNew = Siteno & "\" & strProcess & "\" & PoNO & "-" & Scope & "\" & DocPath
            If System.IO.Directory.Exists(strPath) Then
                newStrResult = UploadFile(f, strPath + fileName)
            Else
                'AccessPermission(strPath)
                System.IO.Directory.CreateDirectory(strPath)
                newStrResult = UploadFile(f, strPath + fileName)
            End If
            Dim keyval As Int16
            Select Case objbositedoc.DocUploadverify(SiteId, DocId, Version).ToString
                Case 1  ''This document not attached to this site
                    keyval = 1
                     Case 2 '' means document was already uploaded for this version of site,do u want to overwrite
                    keyval = 2
                Case 3 '' means document was not yet uploaded for thissite
                    keyval = 3
                Case 4 'means document already processed for sencod stage cannot upload
                    keyval = 0
            End Select
            If newStrResult.ToLower = "ok" Then
                Dim strResult As String = "1"
                strResult = DOInsertTrans(SiteId, DocId, Version, strPath, Convert.ToInt32(userid), strUserName)
                If strResult = "0" Or strResult = "1" Then

                    DocPath = strPathNew + fileName
                Else
                    If (Common.TakeFileUpload.MergePdfNew(strPath + fileName, strPath + strResult, strPath + "NewMerge.pdf")) Then
                        DocPath = strPathNew & "NewMerge.pdf"
                    Else
                        DocPath = strPathNew + fileName
                    End If

                End If

                With objetsitedoc
                    .SiteID = SiteId
                    .DocId = DocId
                    .IsUploded = 1
                    .Version = Version
                    .keyval = keyval
                    .DocPath = DocPath
                    .AT.RStatus = Constants.STATUS_ACTIVE
                    .AT.LMBY = strUserName
                    .orgDocPath = DocPath
                    .PONo = PoNO
                End With
                'objbositedoc.updatedocupload(objetsitedoc)
                Dim hcptcontrol As New HCPTController
                hcptcontrol.HCPT_UpdateDocUpload(objetsitedoc)
                sendmail2(SiteId, DocId, Siteno)
                chek4alldoc(SiteId, Version, PoNO, Siteno, strProcess) ' for messaage to previous screen ' and saving final docupload date in reporttable

                AuditTrail(PoNO, SiteId, DocId, userid, roleid, Scope)
            End If
            Return newStrResult

        Catch ex As Exception
            Return ex.Message.ToString()
        End Try
    End Function
    <WebMethod()> _
    Public Function UploadFile(ByVal f() As Byte, ByVal fileName As String, ByVal TypePonoScope As String, ByVal Siteno As String, ByVal DocId As String) As String

        Try
            'Creating Directry
            Dim StrDirectory As String = ConfigurationManager.AppSettings("Fpath") & Siteno & "\" & TypePonoScope & "\"
            If System.IO.Directory.Exists(StrDirectory) Then
                Return UploadFile(f, StrDirectory + fileName)
            Else
                'AccessPermission(strPath)
                System.IO.Directory.CreateDirectory(StrDirectory)
                Return UploadFile(f, StrDirectory + fileName)
            End If

        Catch ex As Exception
            Return ex.Message.ToString()
        End Try
    End Function
    <WebMethod(BufferResponse:=True, Description:="Login")> _
   Public Function UserLogin(ByVal UserName As String, ByVal Password As String, ByRef StrOutPut As String) As DataSet
        Dim RetDataset As New DataSet
        Try
            Dim strpassword As String = Trim(FormsAuthentication.HashPasswordForStoringInConfigFile(Password.Replace("'", "''") & "TAKE", "MD5"))

            Dim dtPo As DataTable = objdb.ExeQueryDT("EXEC [WebValidateLogin] '" & UserName & "','" & strpassword & "'", "Sitesdetails")
            RetDataset.Tables.Add(dtPo)
            StrOutPut = "ok"
        Catch ex As Exception
            StrOutPut = ex.Message.ToString()
        End Try
        Return RetDataset
    End Function
    Function UploadFile(ByVal f() As Byte, ByVal fileName As String) As String
        ' the byte array argument contains the content of the file
        ' the string argument contains the name and extension
        ' of the file passed in the byte array
        Try

            ' instance a memory stream and pass the
            ' byte array to its constructor
            Dim ms As MemoryStream = New MemoryStream(f)

            ' instance a filestream pointing to the 
            ' storage folder, use the original file name
            ' to name the resulting file
            Dim fs As FileStream = New FileStream(fileName, FileMode.Create)

            ' write the memory stream containing the original
            ' file as a byte array to the filestream
            ms.WriteTo(fs)

            ' clean up
            ms.Close()
            fs.Close()
            fs.Dispose()

            ' return OK if we made it this far
            Return "OK"
        Catch ex As Exception
            ' return the error message if the operation fails
            Return ex.Message.ToString()
        End Try
    End Function
    <WebMethod(BufferResponse:=True, Description:="Returns an ADO.NetDataSet")> _
    Public Function EBastProcess(ByRef StrOutPut As String) As DataSet
        Dim RetDataset As New DataSet

        DtProcess.Columns.Add("ProcessType", Type.GetType("System.String"))
        DtProcess.Columns.Add("ProcessName", Type.GetType("System.String"))
        Try
            Dim myrowt As DataRow
            myrowt = DtProcess.NewRow
            myrowt("ProcessType") = "ti2g"
            myrowt("ProcessName") = "ti2g"
            DtProcess.Rows.Add(myrowt)
            Dim myrowt3 As DataRow
            myrowt3 = DtProcess.NewRow
            myrowt3("ProcessType") = "ti3g"
            myrowt3("ProcessName") = "ti3g"
            DtProcess.Rows.Add(myrowt3)


            Dim myrowsis As DataRow
            myrowsis = DtProcess.NewRow
            myrowsis("ProcessType") = "sis2g"
            myrowsis("ProcessName") = "sis2g"
            DtProcess.Rows.Add(myrowsis)

            Dim myrowsis3 As DataRow
            myrowsis3 = DtProcess.NewRow
            myrowsis3("ProcessType") = "sis3g"
            myrowsis3("ProcessName") = "sis3g"
            DtProcess.Rows.Add(myrowsis3)

            Dim myrowSitac As DataRow
            myrowSitac = DtProcess.NewRow
            myrowSitac("ProcessType") = "sitac2g"
            myrowSitac("ProcessName") = "sitac2g"
            DtProcess.Rows.Add(myrowSitac)

            Dim myrowSitac3 As DataRow
            myrowSitac3 = DtProcess.NewRow
            myrowSitac3("ProcessType") = "sitac3g"
            myrowSitac3("ProcessName") = "sitac3g"
            DtProcess.Rows.Add(myrowSitac3)

            Dim myrowCME As DataRow
            myrowCME = DtProcess.NewRow
            myrowCME("ProcessType") = "cme2g"
            myrowCME("ProcessName") = "cme2g"
            DtProcess.Rows.Add(myrowCME)

            Dim myrowCME3 As DataRow
            myrowCME3 = DtProcess.NewRow
            myrowCME3("ProcessType") = "cme3g"
            myrowCME3("ProcessName") = "cme3g"
            DtProcess.Rows.Add(myrowCME3)
            RetDataset.Tables.Add(DtProcess)
            StrOutPut = "ok"
        Catch ex As Exception
            ' return the error message if the operation fails
            StrOutPut = ex.Message.ToString()
            ' Return Nothing
        End Try
        Return RetDataset
    End Function
    <WebMethod(BufferResponse:=True, Description:="Returns an PoDetails")> _
  Public Function EBastPoNO(ByVal strProcess As String, ByRef StrOutPut As String) As DataSet
        Dim RetDataset As New DataSet
        Try
            Dim dtPo As DataTable = objdb.ExeQueryDT("exec [uspWebPoDetails] '" & strProcess & "'", "Podetails")
            RetDataset.Tables.Add(dtPo)

            StrOutPut = "ok"
        Catch ex As Exception
            ' return the error message if the operation fails
            StrOutPut = ex.Message.ToString()
            ' Return Nothing
        End Try
        Return RetDataset
    End Function
    <WebMethod(BufferResponse:=True, Description:="Returns an Site Details")> _
  Public Function EBastSite(ByVal strPoNO As String, ByVal strProcess As String, ByRef StrOutPut As String) As DataSet
        Dim RetDataset As New DataSet
        Try
            Dim dtPo As DataTable = objdb.ExeQueryDT("exec [uspWebSiteDetails] '" & strPoNO & "','" & strProcess & "'", "Sitesdetails")
            RetDataset.Tables.Add(dtPo)

            StrOutPut = "ok"
        Catch ex As Exception
            ' return the error message if the operation fails
            StrOutPut = ex.Message.ToString()
            ' Return Nothing
        End Try
        Return RetDataset
    End Function
    <WebMethod(BufferResponse:=True, Description:="Returns an Scope")> _
  Public Function EBastScope(ByVal strPoNO As String, ByVal strSiteno As String, ByVal strProcess As String, ByRef StrOutPut As String) As DataSet
        Dim RetDataset As New DataSet
        Try
            Dim dtPo As DataTable = objdb.ExeQueryDT("exec [uspWebscopeetails] '" & strPoNO & "','" & strSiteno & "','" & strProcess & "'", "Sitesdetails")
            RetDataset.Tables.Add(dtPo)

            StrOutPut = "ok"
        Catch ex As Exception
            ' return the error message if the operation fails
            StrOutPut = ex.Message.ToString()
            ' Return Nothing
        End Try
        Return RetDataset
    End Function
    <WebMethod(BufferResponse:=True, Description:="Returns an Dcoument")> _
  Public Function EBastDocument(ByVal strPoNO As String, ByVal strSiteno As String, ByVal strScope As String, ByVal strProcess As String, ByRef StrOutPut As String, ByVal UserId As Integer) As DataSet
        Dim RetDataset As New DataSet
        Try
            Dim dtPo As DataTable = objdb.ExeQueryDT("exec [uspWebDocumentDetails] '" & strPoNO & "','" & strSiteno & "','" & strScope & "','" & strProcess & "'", "Sitesdetails")
            RetDataset.Tables.Add(dtPo)

            StrOutPut = "ok"
        Catch ex As Exception
            ' return the error message if the operation fails
            StrOutPut = ex.Message.ToString()
            ' Return Nothing
        End Try
        Return RetDataset
    End Function
    
    Public Sub chek4alldoc(ByVal siteid As Integer, ByVal version As Integer, ByVal pono As String, ByVal SiteNO As String, ByVal strProcess As String)
        Dim i As Integer
        Dim strSql As String

        If strProcess = "ti2g" Then
            strSql = "select count(*) from ti2g..sitedoc where  isrequired=1 and isuploaded = 0  and version=" & version & " and  siteid=" & siteid

        ElseIf strProcess = "ti3g" Then
            strSql = "select count(*) from ti3g..sitedoc where  isrequired=1 and isuploaded = 0  and version=" & version & " and  siteid=" & siteid

        ElseIf strProcess = "sis2g" Then
            strSql = "select count(*) from sis2g..sitedoc where  isrequired=1 and isuploaded = 0  and version=" & version & " and  siteid=" & siteid

        ElseIf strProcess = "sis3g" Then
            strSql = "select count(*) from sis3g..sitedoc where  isrequired=1 and isuploaded = 0  and version=" & version & " and  siteid=" & siteid

        ElseIf strProcess = "sitac2g" Then

            strSql = "select count(*) from sitac2g..sitedoc where  isrequired=1 and isuploaded = 0  and version=" & version & " and  siteid=" & siteid
        ElseIf strProcess = "sitac3g" Then
            strSql = "select count(*) from sitac3g..sitedoc where  isrequired=1 and isuploaded = 0  and version=" & version & " and  siteid=" & siteid

        ElseIf strProcess = "cme2g" Then
            strSql = "select count(*) from cme2g..sitedoc where  isrequired=1 and isuploaded = 0  and version=" & version & " and  siteid=" & siteid
        Else
            strSql = "select count(*) from cme3g..sitedoc where  isrequired=1 and isuploaded = 0  and version=" & version & " and  siteid=" & siteid

        End If
        i = objdb.ExeQueryScalar(strSql)
        If i = 0 Then
            'insert to new table for report as final document uploaded
            objbositedoc.uspRPTUpdate(pono, SiteNO)
            strSql = "Exec uspRPTUpdate  '" & pono & "','" & SiteNO & "'" ','" & strProcess & "'"

            objdb.ExeQueryScalar(strSql)
        Else
        End If

    End Sub
    Public Sub sendmail2(ByVal Siteid As Integer, ByVal docid As Integer, ByVal SiteNo As String) ' for extra document uploded
        Dim i As Integer

        i = objbositedoc.sendmail2(Siteid, docid)
        dt = objBOM.uspMailReportLD(7, )
        Dim mySMTPClient As New System.Net.Mail.SmtpClient
        Dim myEmail As New System.Net.Mail.MailMessage
        If i > 0 Then

            myEmail.BodyEncoding() = System.Text.Encoding.UTF8
            myEmail.SubjectEncoding = System.Text.Encoding.UTF8
            myEmail.To.Add(ConfigurationManager.AppSettings("Rmailid"))
            myEmail.Subject = dt.Rows(0).Item("MailType").ToString
            myEmail.Body = myEmail.Body & dt.Rows(0).Item("Salutation").ToString & "<br>"
            myEmail.Body = myEmail.Body & "For Site: " & SiteNo & "<br>"
            myEmail.Body = myEmail.Body & dt.Rows(0).Item("Body").ToString & "<br>"
            myEmail.Body = myEmail.Body & dt.Rows(0).Item("Closing").ToString
            myEmail.IsBodyHtml = True
            myEmail.From = New System.Net.Mail.MailAddress(ConfigurationManager.AppSettings("Smailid"), "NSN")
            mySMTPClient.Host = ConfigurationManager.AppSettings("smtp")
            Try
                mySMTPClient.Send(myEmail)
            Catch ex As Exception
                objdb.ExeNonQuery("exec uspErrLog '', 'mailsending','" & ex.Message.ToString() & "','sendmailtrans'")
            End Try
        End If
    End Sub
    Sub AuditTrail(ByVal Pono As String, ByVal SiteId As Integer, ByVal docid As Integer, ByVal userid As Integer, ByVal RoleId As Integer, ByVal scope As String)
        objET.PoNo = Pono
        objET.SiteId = SiteId
        objET.DocId = docid
        objET.Task = "1"
        objET.Status = "1"
        objET.Userid = userid
        objET.Roleid = RoleId
        objET.fldtype = scope
        objBOAT.uspAuditTrailI(objET)
    End Sub
    Function DOInsertTrans(ByVal siteid As String, ByVal docid As Integer, ByVal version As Integer, ByVal strPath As String, ByVal UserID As Integer, ByVal UserName As String) As String
        Dim wfid As Integer
        Dim dtNew As DataTable
        wfid = objdbutil.ExeQueryScalar("select wf_id from sitedoc where siteid='" & siteid & "' and docid=" & docid & " and version =" & version & "")
        objbositedoc.DelWFTransaction(docid, wfid, siteid, version)

        If wfid <> 0 Then
            dtNew = objbositedoc.doinserttrans(wfid, docid)
            Dim aa As Integer = 0
            Dim status As Integer = 0
            Dim strSql1 As String = "select wfdid,wfid,sorder,grpcode,roleid,a.tsk_id,a.grpId,tt.tskname,0 as   X_Coordinate,0 as  Y_Coordinate," & _
                     "0 as  PageNo,IsNull(a.Sorder,0)Sno from twfdefinition A inner join tusertype B on a.grpid=b.grpid inner join ttask as tt on tt.tsk_id=a.tsk_id " & _
          " where wfid=" & wfid & "   order by wfdid"
            dtNew = objdbutil.ExeQueryDT(strSql1, "dd")
            ' dtNew = objdbutil.ExeQueryScalar("select wf_id from sitedoc where siteid='" & siteid & "' and docid=" & docid & " and version =" & version & "")

            Return CreatePDFFile(strPath, wfid, siteid, docid, version)

        Else
            Dim status As Integer = 99

            objbositedoc.uspwftransactionNOTWFI(docid, 0, siteid, version, 1, UserID, 99, 2, UserName)
            Return "0"
        End If

    End Function
    Function CreatePDFFile(ByVal StrPath As String, Optional ByVal ProcessId As Integer = 0, Optional ByVal siteid As String = "", Optional ByVal docid As Integer = 0, Optional ByVal version As Integer = 0) As String
        Dim dt1, dt2 As DataTable
        Dim filenameorg1 As String = "", ReFileName1 As String = ""
        Dim strSql1 As String = "select wfdid,wfid,sorder,grpcode,roleid,a.tsk_id,a.grpId,tt.tskname,0 as   X_Coordinate,0 as  Y_Coordinate," & _
                      "0 as  PageNo,IsNull(a.Sorder,0)Sno from twfdefinition A inner join tusertype B on a.grpid=b.grpid inner join ttask as tt on tt.tsk_id=a.tsk_id " & _
           " where tt.tsk_id =1 and wfid=" & ProcessId & "   order by wfdid"
        dt1 = objdbutil.ExeQueryDT(strSql1, "dd")
        Dim strSql2 As String = "select wfdid,wfid,sorder,grpcode,roleid,a.tsk_id,a.grpId,tt.tskname,0 as   X_Coordinate,0 as  Y_Coordinate," & _
                    "0 as  PageNo,IsNull(a.Sorder,0)Sno from twfdefinition A inner join tusertype B on a.grpid=b.grpid inner join ttask as tt on tt.tsk_id=a.tsk_id " & _
         " where tt.tsk_id <> 1 and wfid=" & ProcessId & "   order by wfdid"
        dt2 = objdbutil.ExeQueryDT(strSql2, "dd")
        If dt1.Rows.Count > 0 Then
            objbositedoc.DelWFTransaction(docid, dt1.Rows(0).Item(1).ToString, siteid, version)

            Dim status As Integer = 0

            Dim dvIn As New DataView
            Dim dvNotIn As New DataView
            Dim J As Integer = 1
            Dim Y As Integer = 0, TopK As Integer = 0
            dvIn = dt1.DefaultView

            dvIn.RowFilter = "TSK_Id=1"

            dvNotIn = dt2.DefaultView

            dvNotIn.RowFilter = "TSK_Id <>1"

            For TopK = 0 To dvIn.Count - 1
                objDo.Site_Id = siteid
                objDo.SiteVersion = version
                objDo.DocId = docid
                objDo.AT.RStatus = Constants.STATUS_ACTIVE
                objDo.AT.LMBY = "admin"
                objDo.Status = 2
                objDo.UserType = dvIn(TopK).Item(3).ToString
                objDo.UsrRole = dvIn(TopK).Item(4).ToString
                objDo.WFId = dvIn(TopK).Item(1).ToString
                objDo.TSK_Id = dvIn(TopK).Item(5).ToString
                objDo.UGP_Id = dvIn(TopK).Item("grpId").ToString
                objDo.XVal = Constants._XVal + (Constants._IncrXVal * J)
                objDo.YVal = Y * Constants._Yval
                objDo.PageNo = -1
                objDo.Site_Id = siteid
                objbositedoc.uspwftransactionIU(objDo)
            Next
            Dim iMainTable1 As New HtmlTable
            iMainTable1.Width = "100%"
            iMainTable1.Align = "left"

            'Y = mat(dvNotIn.Count / 3.0)
            Y = Math.Ceiling((Format(dvNotIn.Count, "#.0") / 3.0))
            If Y = 0 Then Y = 1
            status = 1
            For TopK = 0 To dvNotIn.Count - 1
                Dim iMainRowk As New HtmlTableRow

                For J = 0 To 2
                    If ((TopK + J) < dvNotIn.Count) Then
                        objDo.Site_Id = siteid
                        objDo.SiteVersion = version
                        objDo.DocId = docid
                        objDo.AT.RStatus = Constants.STATUS_ACTIVE
                        objDo.AT.LMBY = "admin"
                        objDo.Status = 2
                        objDo.UserType = dvNotIn(TopK + J).Item(3).ToString
                        objDo.UsrRole = dvNotIn(TopK + J).Item(4).ToString
                        objDo.WFId = dvNotIn(TopK + J).Item(1).ToString
                        objDo.TSK_Id = dvNotIn(TopK + J).Item(5).ToString
                        objDo.UGP_Id = dvNotIn(TopK + J).Item("grpId").ToString
                        If dvNotIn.Count = 2 Then
                            objDo.XVal = Constants._XVal + (Constants._IncrXVal * J + (J * 100))
                        Else
                            objDo.XVal = Constants._XVal + (Constants._IncrXVal * J + 50)
                        End If
                        objDo.YVal = 791 - (75 * Y)
                        objDo.PageNo = -1
                        objDo.Site_Id = siteid
                        objDo.Status = status
                        objbositedoc.uspwftransactionIU(objDo)
                        Dim iMainCellj As New HtmlTableCell
                        Dim iMainTable As New HtmlTable
                        iMainTable.Style.Add("border-collapse", "collapse")
                        iMainTable.Height = 140
                        iMainTable.Width = 200
                        iMainTable.BorderColor = "#000000"
                        iMainTable.Border = 1

                        iMainTable.CellPadding = 0
                        iMainTable.CellSpacing = 0
                        Dim iMainRow1 As New HtmlTableRow
                        Dim iMainCell1 As New HtmlTableCell
                        iMainCell1.Height = 95
                        iMainRow1.Cells.Add(iMainCell1)

                        Dim iMainRow2 As New HtmlTableRow
                        Dim iMainCell2 As New HtmlTableCell
                        iMainCell2.Height = 45
                        iMainCell2.Align = "center"
                        iMainCell2.VAlign = "center"
                        iMainCell2.InnerHtml = dvNotIn(TopK + J).Item("tskname").ToString
                        iMainRow2.Cells.Add(iMainCell2)

                        iMainTable.Rows.Add(iMainRow1)
                        iMainTable.Rows.Add(iMainRow2)
                        iMainCellj.Controls.Add(iMainTable)
                        iMainRowk.Cells.Add(iMainCellj)
                        If TopK + J = 0 Then
                            sendmailTrans(siteid, dvNotIn(TopK + J).Item(3).ToString, dvNotIn(TopK + J).Item(4).ToString)
                        End If

                    End If

                    iMainTable1.Rows.Add(iMainRowk)

                    Dim iMainRowk1 As New HtmlTableRow
                    Dim iMainCellj1 As New HtmlTableCell
                    iMainCellj1.Height = 10

                    iMainRowk1.Cells.Add(iMainCellj1)
                    iMainTable1.Rows.Add(iMainRowk1)
                Next
                If (Y > 1) Then
                    Y = Y - 1
                End If
                TopK = TopK + J - 1
            Next

            filenameorg1 = Format(CDate(DateTime.Now), "ddMMyyyyHHss")
            ReFileName1 = filenameorg1 & ".htm"
            If (System.IO.File.Exists(StrPath & ReFileName1)) Then
                System.IO.File.Delete(StrPath & ReFileName1)
            End If
            If Not System.IO.Directory.Exists(StrPath) Then
                System.IO.Directory.CreateDirectory(StrPath)
            End If
            Dim sw As HtmlTextWriter = New HtmlTextWriter(New StreamWriter(StrPath & ReFileName1, False, System.Text.UnicodeEncoding.UTF8))
            'sw.WriteLine("<html><head><link href=""http://localhost:2821/E-Bast/css/Styles.css"" rel=""stylesheet"" type=""text/css"" /></head>")
            sw.WriteLine("<html><head>  <style type=""text/css"">  .lblText{font-family: verdana;	font-size: 8pt;color: #000000;}")
            sw.WriteLine(".textFieldStyle{background-color: white;border: 1px solid; color: black; font-family: verdana; font-size: 9pt;}")
            sw.WriteLine(".GridHeader{color: #0e1b42;background-color: Orange;font-size: 9pt;font-family: Verdana;text-align:Left;vertical-align: bottom;font-weight:bold;}")
            sw.WriteLine(".GridEvenRows{background-color: #e3e3e3;vertical-align:top;font-family: verdana;	font-size: 8pt;color: #000000;}")
            sw.WriteLine(".GridOddRows{background-color: white;vertical-align:top;font-family: verdana;	font-size: 8pt;color: #000000;}")
            sw.WriteLine(".PagerTitle{font-size: 8pt;background-color: #cddbbf;text-align:Right;vertical-align: middle;color: #25375b;font-weight:bold;}")
            sw.WriteLine("</style> </head>")
            sw.WriteLine("<body>")
            iMainTable1.RenderControl(sw)
            sw.WriteLine("</body>")
            sw.WriteLine("</html>")
            sw.Close()
            sw.Dispose()
        End If

        Return Common.TakeFileUpload.ConvertAnyFormatToPDFHtmlNew(StrPath & ReFileName1, StrPath, filenameorg1)
    End Function
    Sub sendmailTrans(ByVal siteid As Integer, ByVal usertype As String, ByVal usrrole As Integer)
        Dim dtn As New DataTable
        dtn = objbositedoc.uspgetemail(siteid, usertype, usrrole)
        If dtn.Rows.Count > 0 Then
            If dtn.Rows(0).Item(3) <> "X" Then
                dt = objBOM.uspMailReportLD(Constants.docupload, )  ''this is fro document upload time sending mail
                Dim k As Integer
                Dim Remail As String = "'"
                Dim name As String = ""
                Dim doc As New StringBuilder
                Remail = dtn.Rows(0).Item(3).ToString
                name = dtn.Rows(0).Item(2).ToString
                Dim senderAdd As String = ConfigurationManager.AppSettings("Smailid")
                Dim receiverAdd As String = Remail
                Dim mySMTPClient As New System.Net.Mail.SmtpClient
                Dim myEmail As New System.Net.Mail.MailMessage
                myEmail.BodyEncoding() = System.Text.Encoding.UTF8
                myEmail.SubjectEncoding = System.Text.Encoding.UTF8
                myEmail.To.Add(receiverAdd)
                myEmail.Subject = dt.Rows(0).Item(3).ToString  ''subject from table
                myEmail.Body = dt.Rows(0).Item(2).ToString & name & ",<br> <br>" ''salutatation from table
                myEmail.Body = myEmail.Body & dt.Rows(0).Item(4).ToString & " <br> <br> "
                myEmail.Body = myEmail.Body & " Process - " & ConfigurationManager.AppSettings("Type").Replace("\", "") & " <br> <br> "
                Dim sb As String = ""
                sb = "<table  border=1>"
                sb = sb & "<tr><td>Contract #</td><td>Site</td><td>Scope</td><td>Document</td><td>Date Requested</td></tr>"
                For k = 0 To dtn.Rows.Count - 1
                    sb = sb & "<tr><td> " & dtn.Rows(k).Item(6).ToString & "  </td><td>" & dtn.Rows(k).Item(4).ToString & "</td><td>" & dtn.Rows(k).Item(5).ToString & " </td><td>" & dtn.Rows(k).Item(0).ToString & "</td><td>" & dtn.Rows(k).Item(7).ToString & "</td></tr>"
                Next
                sb = sb & "</table>"
                myEmail.Body = myEmail.Body & sb
                myEmail.Body = myEmail.Body & "<br/>" & dt.Rows(0).Item(5).ToString   ''closing
                myEmail.IsBodyHtml = True
                myEmail.From = New System.Net.Mail.MailAddress(senderAdd, "NSN")
                mySMTPClient.Host = ConfigurationManager.AppSettings("smtp")
                Try
                    mySMTPClient.Send(myEmail)
                Catch ex As Exception
                    objdb.ExeNonQuery("exec uspErrLog '', 'mailsending','" & ex.Message.ToString() & "','sendmailtrans'")
                End Try
            End If
        End If
    End Sub
End Class

