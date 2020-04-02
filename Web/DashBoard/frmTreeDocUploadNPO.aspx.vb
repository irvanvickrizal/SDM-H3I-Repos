Imports BusinessLogic
Imports Common
Imports System.Data
Imports System.Web.UI
Imports System.Web.UI.webControl
Imports Entities
Imports System.Net.Mail
Imports System.IO
Imports System.Collections.Generic
Imports Common_NSNFramework
Imports CRFramework

Partial Class DashBoard_frmTreeDocUploadNPO
    Inherits System.Web.UI.Page
    Dim dt As DataTable
    Dim dtn As DataTable
    Dim ft As String
    Dim objBo As New BODDLs
    Dim objdb As New DBUtil
    Dim strSql As String
    Dim objbodoc As New BOCODDocument
    Dim docid As String
    Dim sec As String
    Dim subsec As String
    Dim sitename As String
    Dim path As String
    Dim intFileNameLength As Integer
    Dim FileNamePath As String
    Dim FileNameOnly As String
    Dim ReFileName As String
    Dim roleid As Integer
    Dim grp As String
    Dim objetsitedoc As New ETSiteDoc
    Dim objbositedoc As New BOSiteDocs
    Dim version As Integer
    Dim objBOM As New BOMailReport
    Dim objET As New ETAuditTrail
    Dim objBOAT As New BOAuditTrail
    Dim objdbutil As New DBUtil
    Dim objDo As New ETWFTransaction
    Dim objmail As New TakeMail
    Dim bautok As Integer
    Dim bastok As Integer
    Dim i, j As Integer
    Dim generalcontrol As New GeneralController
    Dim controller As New HCPTController
    Dim kpicontrol As New KPIController
    Dim crcontrol As New CRController

#Region "NSNFramework"
    Dim dbutils_nsn As New DBUtils_NSN
#End Region
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        If Page.IsPostBack = False Then
            BindSiteInfo(GetWPID())
            BindSiteHistory()
            Dochecking()
            dt = objdbutil.ExeQueryDT("select docname,Appr_Required,DGBox from coddoc where doc_id = " & GetDOCID() & "", "ddoc")
            hdndocname.Value = dt.Rows(0).Item("docname").ToString
            hdnapprequired.Value = dt.Rows(0).Item("Appr_Required").ToString
            hdnDGBox.Value = dt.Rows(0).Item("DGBox").ToString
            lbldoc.Text = hdndocname.Value
            If GetFrom().Equals("ssvrc") Then
                btnLastApproverSubmit.Visible = False
            End If
        End If
    End Sub
    Protected Sub btnupload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnupload.Click
        If FileUpload.HasFile = True Then
            'add file limiter by audy 20121113
            If System.IO.Path.GetExtension(fileUpload.FileName.ToLower) <> ".pdf" Then
                ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('Only PDF files Allowed');", True)
                Exit Sub
            End If
            uploaddocument(hdnversion.Value, hdnkeyval.Value, 0)
        Else
            Response.Write("<script>alert('please select document to upload')</script>")
        End If
    End Sub
    Public Function getfoldertype(ByVal pono As String, ByVal siteno As String) As String
        Dim foldertype As String
        Dim i As String = lblScope.Text
        Dim p As String = lblPONO.Text & "-"
        foldertype = ConfigurationManager.AppSettings("Type") & p & i & "\"
        Return foldertype
    End Function
    Public Sub chek4alldoc()
        Dim i As Integer
        Dim siten As String
        'Dim Scope() As String = lblsite.Text.Split("-")
        siten = lblSiteNo.Text
        i = objbositedoc.chek4alldoc(hdnsiteid.Value, hdnversion.Value)
        If i = 0 Then
            hdnready4baut.Value = 1
            'insert to new table for report as final document uploaded
            objbositedoc.uspRPTUpdate(lblPONO.Text, lblSiteNo.Text)
        Else
            hdnready4baut.Value = 0
        End If
    End Sub


#Region "UploadDocument,DoInsertTrans 20090403, CreatePDF"

    Sub uploaddocument(ByVal vers As Integer, ByVal keyval As Integer, ByVal type As Integer)
        ft = ConfigurationManager.AppSettings("Type") & lblSiteNo.Text & "-" & lblScope.Text & "\"
        Dim fpath As String = ConfigurationManager.AppSettings("BufferPath")
        path = ConfigurationManager.AppSettings("Fpath") & sitename & ft
        Dim DocPath As String = ""
        Dim err As Boolean = True
        Dim strResult As String = DOInsertTrans(hdnsiteid.Value, GetDOCID(), vers, path)
        If type = 0 Then
            FileNamePath = fileUpload.PostedFile.FileName
            FileNameOnly = System.IO.Path.GetFileName(fileUpload.PostedFile.FileName)
            ReFileName = Format(CDate(DateTime.Now), "ddMMyyyyHHss") & FileNameOnly
            If strResult = "0" Or strResult = "1" Then
                'DocPath = sitename & ft & Common.TakeFileUpload.ConvertAnyFormatToPDF(fileUpload, path)
                DocPath = sitename & ft & LocalFileUpload.ConvertAnyFormatToPDF(fileUpload, path) ' local purposed only
            Else
                'DocPath = sitename & ft & Common.TakeFileUpload.ConvertAnyFormatToPDF(fileUpload, path, path + strResult)
                DocPath = sitename & ft & LocalFileUpload.ConvertAnyFormatToPDF(fileUpload, path) ' local purposed only
            End If
        End If
        If err = True Then
            With objetsitedoc
                .SiteID = hdnsiteid.Value
                .DocId = GetDOCID()
                .IsUploded = 1
                .Version = vers
                .keyval = keyval
                .DocPath = DocPath
                .AT.RStatus = Constants.STATUS_ACTIVE
                .AT.LMBY = Session("User_Name")
                .orgDocPath = sitename & ft & ReFileName
                .PONo = lblPONO.Text
            End With
            Try
                objbositedoc.updatedocupload(objetsitedoc)
            Catch ex As Exception
                Response.Write("<script language='javascript'>alert('Update failed please check your uploaded file name..');</script>")
                err = False
                Exit Sub
            End Try
            If err = True Then
                sendmail2()
                chek4alldoc() 'for messaage to previous screen and saving final docupload date in reporttable
                'for BAUT
                'bautok = objbositedoc.check4BAUTBAST(hdnsiteid.Value, hdnversion.Value, 1, ddlPO.SelectedItem.Text, Session("User_Name"))
                bautok = objdb.ExeQueryScalar("exec uspcheck4BAUTBAST " & hdnsiteid.Value & " ," & hdnversion.Value & "," & 1 & ",'" & lblPONO.Text & "','" & Session("User_Name") & "'," & ConfigurationManager.AppSettings("BAUTID") & "," & ConfigurationManager.AppSettings("BASTID") & " ")
                'for BAST1
                'bastok = objbositedoc.check4BAUTBAST(hdnsiteid.Value, hdnversion.Value, 2, ddlPO.SelectedItem.Text, Session("User_Name"))
                bastok = objdb.ExeQueryScalar("exec uspcheck4BAUTBAST " & hdnsiteid.Value & " ," & hdnversion.Value & "," & 2 & ",'" & lblPONO.Text & "','" & Session("User_Name") & "'," & ConfigurationManager.AppSettings("BAUTID") & "," & ConfigurationManager.AppSettings("BASTID") & " ")
                If bautok = 888 Then
                    'send mail to BAUT intiator
                    i = objdbutil.ExeQueryScalar("exec uspgetBAUTBASTInitiator " & hdnsiteid.Value & "," & ConfigurationManager.AppSettings("BAUTID") & "," & 0 & " ")
                    Try
                        objmail.sendMailIniBAUTBAST(i, sitename, ConfigurationManager.AppSettings("BAUTmailconst"), ConfigurationManager.AppSettings("BAUTID"), lblPONO.Text)
                    Catch ex As Exception
                        objdb.ExeNonQuery("exec uspErrLog '', 'mailsending','" & ex.Message.ToString.Replace("'", "''") & "','sendmailtransBAUTInitiator'")
                    End Try
                End If
                If bastok = 999 Then
                    'send mail to BAST initiator
                    j = objdbutil.ExeQueryScalar("exec uspgetBAUTBASTInitiator " & hdnsiteid.Value & "," & 0 & "," & ConfigurationManager.AppSettings("BASTID") & " ")
                    Try
                        objmail.sendMailIniBAUTBAST(i, sitename, ConfigurationManager.AppSettings("BASTmailconst"), ConfigurationManager.AppSettings("BASTID"), lblPONO.Text)
                    Catch ex As Exception
                        objdb.ExeNonQuery("exec uspErrLog '', 'mailsending','" & ex.Message.ToString.Replace("'", "''") & "','sendmailtransBASTInitiator'")
                    End Try
                End If
                AuditTrail()
                'Modified by Fauzan, 30 Nov 2018. Add 1 more Parameter
                ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "Showmain('scs', " & GetDOCID() & ");", True)
            Else
                lblStatus.Text = "Document Upload failed"
            End If
        Else
            Response.Write("<script language='javascript'>alert('Document upload failed');</script>")
        End If
    End Sub

    Function DOInsertTrans(ByVal siteid As String, ByVal docid As Integer, ByVal version As Integer, ByVal strPath As String) As String
        Dim wfid As Integer
        Dim dtNew As DataTable

        Dim getdocapprovalsheet As String() = ConfigurationManager.AppSettings("docwithapprovalsheet").Split(",")
        Dim totalIndexApprovalsheet As Integer = getdocapprovalsheet.Length
        Dim incrementindex As Integer = 0
        Dim isPartofDocApprovalSheet As Boolean = False
        While incrementindex < totalIndexApprovalsheet
            If (getdocapprovalsheet(incrementindex).Equals(docid.ToString())) Then
                isPartofDocApprovalSheet = True
            End If
            incrementindex += 1
        End While

        wfid = objdbutil.ExeQueryScalar("select wf_id from sitedoc where siteid='" & siteid & "' and docid=" & docid & " and version =" & version & "")
        If wfid <> 0 Then
            ' If docid = ConfigurationManager.AppSettings("ATP") Then
            ' dtNew = crcontrol.GetWorkflowDetail(wfid)
            ' Else
            ' dtNew = objbositedoc.doinserttrans(wfid, docid)
            ' End If

            'Modified by Fauzan, 30 Nov 2018. All Document Transaction store HCPT_WFTransaction
            'If Document type is offline/without approval, WFID will be 0.
            'It will not save to Workflow transaction. Just flagging in DocSite Table that the document has been uploaded
            dtNew = crcontrol.GetWorkflowDetail(wfid)
            'If DocIsPartofApprovalSheet(docid) = True Or hdnDGBox.Value = True Then
            '    dtNew = crcontrol.GetWorkflowDetail(wfid)
            'Else
            '    dtNew = objbositedoc.doinserttrans(wfid, docid)
            'End If

            Dim status As Integer = 0
            Dim isSucceed As Boolean = True
            If hdnDGBox.Value = True Or isPartofDocApprovalSheet = True Then
                If dtNew.Rows.Count > 0 Then
                    If controller.WFTransaction_D(lblwpid.Text, docid) = True Then
                        Dim sorder As Integer
                        Dim aa As Integer = 0
                        For aa = 0 To dtNew.Rows.Count - 1
                            'fillDetails()
                            sorder = dtNew.Rows(aa).Item("sorder")
                            Dim transinfo As New DOCTransactionInfo
                            transinfo.TaskId = Integer.Parse(dtNew.Rows(aa).Item("Tsk_id").ToString())
                            transinfo.SiteInf.PackageId = lblwpid.Text
                            transinfo.DocInf.DocId = docid
                            transinfo.WFID = wfid
                            transinfo.UGPID = Integer.Parse(dtNew.Rows(aa).Item("GrpId").ToString())
                            transinfo.PageNo = 1
                            transinfo.RStatus = 2
                            transinfo.RoleInf.RoleId = Integer.Parse(dtNew.Rows(aa).Item("RoleId").ToString())
                            transinfo.CMAInfo.LMBY = CommonSite.UserId
                            transinfo.Status = 1
                            If sorder = 1 Then
                                transinfo.StartDateTime = Date.Now()
                                transinfo.EndDateTime = Date.Now()
                                transinfo.Status = 0
                            ElseIf sorder = 2 Then
                                transinfo.StartDateTime = Date.Now()
                            Else
                                transinfo.StartDateTime = Nothing
                                transinfo.EndDateTime = Nothing
                            End If

                            transinfo.Xval = 0
                            transinfo.Yval = 0

                            If controller.WFTransaction_I(transinfo) = False Then
                                isSucceed = False
                                Exit For
                            End If
                        Next
                    End If
                End If

                If isSucceed = True Then
                    If (isPartofDocApprovalSheet = True Or hdnDGBox.Value = True) Then
                        'SendATPMail(hdnsiteid.Value, hdnversion.Value, ConfigurationManager.AppSettings("ATP"))
                    End If
                    Return "1"
                Else
                    controller.WFTransaction_D(lblwpid.Text, docid)
                    Return "0"
                End If
            Else
                Dim strSql1 As String = "select wfdid,wfid,sorder,grpcode,roleid,a.tsk_id,a.grpId,tt.tskname,0 as   X_Coordinate,0 as  Y_Coordinate," &
                "0 as  PageNo,IsNull(a.Sorder,0)Sno from twfdefinition A inner join tusertype B on a.grpid=b.grpid inner join ttask as tt on tt.tsk_id=a.tsk_id " &
                " where wfid=" & wfid & "   order by wfdid"
                dtNew = objdbutil.ExeQueryDT(strSql1, "dd")
                Return CreatePDFFile(strPath, wfid)
            End If
        Else
            Dim status As Integer = 99
            'means there no approval process for this document 'no workflow
            If controller.WFTransaction_D(lblwpid.Text.TrimStart().TrimEnd(), GetDOCID()) = True Then
                Dim transinfo As New DOCTransactionInfo
                transinfo.TaskId = 1
                transinfo.SiteInf.PackageId = lblwpid.Text.TrimStart().TrimEnd()
                transinfo.DocInf.DocId = GetDOCID()
                transinfo.WFID = 99
                If CommonSite.UserType.Equals("N") Then
                    transinfo.UGPID = 1
                ElseIf CommonSite.UserType.Equals("S") Then
                    transinfo.UGPID = 2
                ElseIf CommonSite.UserType.Equals("H") Then
                    transinfo.UGPID = 11
                Else
                    transinfo.UGPID = 4
                End If

                transinfo.PageNo = 1
                transinfo.RStatus = 2
                transinfo.RoleInf.RoleId = CommonSite.RollId
                transinfo.CMAInfo.LMBY = CommonSite.UserId
                transinfo.Status = 1
                transinfo.StartDateTime = Date.Now()
                transinfo.EndDateTime = Date.Now()
                transinfo.Status = 0
                transinfo.Xval = 0
                transinfo.Yval = 0
                If controller.WFTransaction_I(transinfo) = False Then
                    'Err = False
                End If
            Else
                'Err = False
            End If
            Return "0"
        End If
    End Function

    Function CreatePDFFile(ByVal StrPath As String, Optional ByVal ProcessId As Integer = 0) As String
        Dim dt1, dt2 As DataTable
        Dim filenameorg1 As String = "", ReFileName1 As String = ""
        Dim strSql1 As String = "select wfdid,wfid,sorder,grpcode,roleid,a.tsk_id,a.grpId,tt.tskname,0 as   X_Coordinate,0 as  Y_Coordinate," &
        "0 as  PageNo,IsNull(a.Sorder,0)Sno from twfdefinition A inner join tusertype B on a.grpid=b.grpid inner join ttask as tt on tt.tsk_id=a.tsk_id " &
        " where tt.tsk_id =1 and wfid=" & ProcessId & "   order by wfdid"
        dt1 = objdbutil.ExeQueryDT(strSql1, "dd")
        Dim strSql2 As String = "select wfdid,wfid,sorder,grpcode,roleid,a.tsk_id,a.grpId,tt.tskname,0 as   X_Coordinate,0 as  Y_Coordinate," &
        "0 as  PageNo,IsNull(a.Sorder,0)Sno from twfdefinition A inner join tusertype B on a.grpid=b.grpid inner join ttask as tt on tt.tsk_id=a.tsk_id " &
        " where tt.tsk_id <> 1 and wfid=" & ProcessId & "   order by wfdid"
        dt2 = objdbutil.ExeQueryDT(strSql2, "dd")
        If dt1.Rows.Count > 0 Then
            If hdnkeyval.Value = 2 Then
                objbositedoc.DelWFTransaction(GetDOCID(), dt1.Rows(0).Item(1).ToString, hdnsiteid.Value, hdnversion.Value)
            End If
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
                fillDetails()
                objDo.Status = status
                objDo.UserType = dvIn(TopK).Item(3).ToString
                objDo.UsrRole = dvIn(TopK).Item(4).ToString
                objDo.WFId = dvIn(TopK).Item(1).ToString
                objDo.TSK_Id = dvIn(TopK).Item(5).ToString
                objDo.UGP_Id = dvIn(TopK).Item("grpId").ToString
                objDo.XVal = Constants._XVal + (Constants._IncrXVal * J)
                objDo.YVal = Y * Constants._Yval
                objDo.PageNo = -1
                objDo.Site_Id = hdnsiteid.Value
                objbositedoc.uspwftransactionIU(objDo)
            Next
            Dim iMainTable1 As New HtmlTable
            iMainTable1.Width = "100%"
            iMainTable1.Align = "left"
            Y = Math.Ceiling((Format(dvNotIn.Count, "#.0") / 3.0))
            If Y = 0 Then Y = 1
            status = 1
            For TopK = 0 To dvNotIn.Count - 1
                Dim iMainRowk As New HtmlTableRow
                For J = 0 To 2
                    If ((TopK + J) < dvNotIn.Count) Then
                        fillDetails()
                        objDo.Status = status
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
                        objDo.Site_Id = hdnsiteid.Value
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
                            Try
                                objmail.sendMailTrans(0, hdnsiteid.Value, dvNotIn(TopK + J).Item(3).ToString, dvNotIn(TopK + J).Item(4).ToString, ConfigurationManager.AppSettings("uploadmailconst"))
                            Catch ex As Exception
                                objdb.ExeNonQuery("exec uspErrLog '', 'mailsending','" & ex.Message.ToString.Replace("'", "''") & "','sendmailbaut'")
                            End Try
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
            sw.WriteLine("<html><head><style type=""text/css"">.lblText{font-family: verdana;font-size: 8pt;color: #000000;}")
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
        Return EBASTFileUpload.ConvertAnyFormatToPDFHtmlNew(StrPath & ReFileName1, StrPath, filenameorg1)
    End Function

    Private Function DocIsPartofApprovalSheet(ByVal docid As Integer) As Boolean
        Dim isPart As Boolean = False
        Dim getdocapprovalsheet As String() = ConfigurationManager.AppSettings("docwithapprovalsheet").Split(",")
        Dim totalIndexApprovalsheet As Integer = getdocapprovalsheet.Length
        Dim incrementindex As Integer = 0
        While incrementindex < totalIndexApprovalsheet
            If (getdocapprovalsheet(incrementindex).Equals(docid.ToString())) Then
                isPart = True
            End If
            incrementindex += 1
        End While

        Return isPart
    End Function
#End Region
    Sub fillDetails()
        objDo.Site_Id = hdnsiteid.Value
        objDo.SiteVersion = hdnversion.Value
        objDo.DocId = GetDOCID()
        objDo.AT.RStatus = Constants.STATUS_ACTIVE
        If (Request.QueryString("id") = ConfigurationManager.AppSettings("ATP")) Then
            objDo.AT.LMBY = Session("User_Id")
        Else
            objDo.AT.LMBY = Session("User_Name")
        End If
    End Sub
    Sub AuditTrail()
        objET.PoNo = lblPONO.Text
        objET.SiteId = hdnsiteid.Value
        objET.DocId = Request.QueryString("id")
        objET.Task = "1"
        objET.Status = "1"
        objET.Userid = Session("User_Id")
        objET.Roleid = Session("Role_Id")
        objET.fldtype = lblScope.Text
        If (Request.QueryString("id") = ConfigurationManager.AppSettings("ATP")) Then
            'for Audit ATP Only -- Irvan v Code
            'dbutils_nsn.InsertAuditTrailNew(objET, Scope(2))
            dbutils_nsn.InsertAuditTrailATPNew(objET, lblwpid.Text)
        Else
            dbutils_nsn.InsertAuditTrailNew(objET, lblwpid.Text)
        End If
        'objBOAT.uspAuditTrailI(objET)
    End Sub

    Sub Dochecking()
        roleid = Session("Role_Id")
        grp = Session("User_Type")
        'bugfix100706
        'version = objbositedoc.getsiteversion(ddlPO.SelectedItem.Text, scope(0), scope(1))
        strSql = "select siteversion from podetails where pono='" & lblPONO.Text & "' and siteno='" & lblSiteNo.Text & "' and FLDType='" & lblScope.Text & "' and workpkgid=" & lblwpid.Text
        version = objdb.ExeQueryScalar(strSql)
        hdnversion.Value = version
        If objbositedoc.uspApprRequired(hdnsiteid.Value, GetDOCID(), hdnversion.Value) <> 0 Then
            If objbositedoc.verifypermission(GetDOCID(), roleid, grp) <> 0 Then
                Select Case objbositedoc.DocUploadverify(hdnsiteid.Value, GetDOCID(), version).ToString
                    Case 1 'This document not attached to this site
                        hdnkeyval.Value = 1
                        btnupload.Attributes.Clear()
                        'belowcase not going to happen we need to test the scenariao.
                        btnupload.Attributes.Add("onclick", "javascript:return " + "confirm('This Document Not attached to this site,Do you want to upload?')")
                    Case 2 ' means document was already uploaded for this version of site,do u want to overwrite
                        hdnkeyval.Value = 2
                        btnupload.Attributes.Clear()
                        btnupload.Attributes.Add("onclick", "javascript:return " + "confirm('This Document already uploaded Do you want to replace?')")
                    Case 3 ' means document was not yet uploaded for thissite
                        hdnkeyval.Value = 3
                        btnupload.Attributes.Clear()
                    Case 4 'means document already processed for sencod stage cannot upload
                        hdnkeyval.Value = 0
                        'Modified by Fauzan, 30 Nov 2018. Add 1 more Parameter
                        ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "Showmain('2sta', " & GetDOCID() & ");", True)
                        Exit Sub
                        'Response.Redirect("frmSiteDocUploadTreeSubcon.aspx")
                End Select
                Page.FindControl("Panel1").Visible = True
            Else
                If Session("Role_Id") = 1 Then 'if role is sysadmin then no need to verify permission everthing permitted.
                    Select Case objbositedoc.DocUploadverify(hdnsiteid.Value, GetDOCID(), version).ToString
                        Case 1  ''This document not attached to this site
                            hdnkeyval.Value = 1
                            btnupload.Attributes.Clear()
                            'belowcase not going to happen we need to test the scenariao.
                            btnupload.Attributes.Add("onclick", "javascript:return " + "confirm('This Document Not attached to this site,Do you want to  upload?')")
                        Case 2 '' means document was already uploaded for this version of site,do u want to overwrite
                            hdnkeyval.Value = 2
                            btnupload.Attributes.Clear()
                            btnupload.Attributes.Add("onclick", "javascript:return " + "confirm('This Document already uploaded Do you want to replace?')")
                        Case 3 ' means document was not yet uploaded for thissite
                            hdnkeyval.Value = 3
                            btnupload.Attributes.Clear()
                        Case 4 'means document already processed for sencod stage cannot upload
                            ' ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "Script", "alert('This Document already processed for second stage so cannot upload again ');", True)
                            hdnkeyval.Value = 0
                            'Modified by Fauzan, 30 Nov 2018. Add 1 more Parameter
                            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "Showmain('2sta', " & GetDOCID() & ");", True)
                            Exit Sub
                    End Select
                    Page.FindControl("Panel1").Visible = True
                Else
                    'hdnkeyval.Value = 0
                    'Page.FindControl("Panel1").Visible = False
                    'ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "Showmain('nop');", True)
                    hdnkeyval.Value = 3
                End If
            End If
        Else 'Seeta 20081230 Appr Not Required
            Select Case objbositedoc.DocUploadverify(hdnsiteid.Value, GetDOCID(), version).ToString
                Case 1 'This document not attached to this site
                    hdnkeyval.Value = 1
                    btnupload.Attributes.Clear()
                    'belowcase not going to happen we need to test the scenariao.
                    btnupload.Attributes.Add("onclick", "javascript:return " + "confirm('This Document Not attached to this site,Do you want to  upload?')")
                Case 2 'means document was already uploaded for this version of site,do u want to overwrite
                    hdnkeyval.Value = 2
                    btnupload.Attributes.Clear()
                    btnupload.Attributes.Add("onclick", "javascript:return " + "confirm('This Document already uploaded Do you want to replace?')")
                Case 3 'means document was not yet uploaded for thissite
                    hdnkeyval.Value = 3
                    btnupload.Attributes.Clear()
                Case 4 'means document already processed for sencod stage cannot upload
                    hdnkeyval.Value = 0
                    'Modified by Fauzan, 30 Nov 2018. Add 1 more Parameter
                    ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "Showmain('2sta', " & GetDOCID() & ");", True)
                    Exit Sub
            End Select

            Page.FindControl("Panel1").Visible = True
        End If
    End Sub
    Public Sub sendmail2()
        Dim i As Integer
        Dim siten As String
        siten = lblSiteNo.Text
        i = objbositedoc.sendmail2(hdnsiteid.Value, GetDOCID())
        dt = objBOM.uspMailReportLD(7, )
        Dim mySMTPClient As New System.Net.Mail.SmtpClient
        Dim myEmail As New System.Net.Mail.MailMessage
        If i > 0 Then
            myEmail.BodyEncoding() = System.Text.Encoding.UTF8
            myEmail.SubjectEncoding = System.Text.Encoding.UTF8
            myEmail.To.Add(ConfigurationManager.AppSettings("Rmailid"))
            myEmail.Subject = dt.Rows(0).Item("MailType").ToString
            myEmail.Body = myEmail.Body & dt.Rows(0).Item("Salutation").ToString & "<br>"
            myEmail.Body = myEmail.Body & "For Site: " & siten & "<br>"
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
    Protected Sub btnback_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnback.Click
        If Not String.IsNullOrEmpty(Request.QueryString("from")) Then
            If Request.QueryString("from") = "ssvrc" Then
                Response.Redirect("frmSSVL0RC.aspx")
            Else
                Response.Redirect("frmSiteDocUploadTreeSubcon.aspx")
            End If
        End If
    End Sub

#Region "Custom Methods"
    Private Sub BindSiteInfo(ByVal wpid As String)
        Dim info As SiteInfo = kpicontrol.GetSiteDetail_WPID(wpid, CommonSite.UserId)
        If info IsNot Nothing Then
            lblPONO.Text = info.PONO
            lblScope.Text = info.FLDType
            lblSiteNo.Text = info.SiteNo
            lblwpid.Text = wpid
            hdnpoid.Value = info.POID
            hdnversion.Value = info.SiteVersion
            hdnsiteid.Value = info.SiteID
        End If
    End Sub
    Private Sub BindSiteHistory()
        Dim ds As DataSet = kpicontrol.KPISiteHistory(GetWPID())
        GvSiteHistory.DataSource = ds
        GvSiteHistory.DataBind()
    End Sub
    'Protected Sub GvSiteHistory_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles GvSiteHistory.RowDataBound
    '    'If e.Row.RowType = DataControlRowType.DataRow Then
    '    '    Dim LblDocId As Label = CType(e.Row.FindControl("lblGuid"), Label)
    '    '    Dim url As String = String.Empty
    '    '    If LblDocId.Text = ConfigurationManager.AppSettings("ATP") Then
    '    '        url = "frmViewDocumentATP.aspx?id=" & e.Row.Cells(9).Text
    '    '    Else
    '    '        url = "frmViewDocument.aspx?id=" & e.Row.Cells(9).Text
    '    '    End If
    '    '    If e.Row.Cells(2).Text <> "" And e.Row.Cells(2).Text <> "&nbsp;" Then
    '    '        e.Row.Cells(4).Text = "<a href='#' onclick=""window.open('" & url & "','mywindow','fullscreen=yes')"">" & e.Row.Cells(3).Text & "</a>"
    '    '    Else
    '    '        e.Row.Cells(4).Text = e.Row.Cells(3).Text
    '    '    End If
    '    'End If
    '    If (e.Row.RowType = DataControlRowType.DataRow) Then
    '        Dim Lblguid As Label = CType(e.Row.FindControl("Lblguid"), Label)
    '        Dim DocPath As String = kpicontrol.KPI_SiteHistory_Docpath(GetWPID(), Lblguid.text)
    '    End If
    'End Sub

    Private Sub SendATPMail(ByVal siteid As Int32, ByVal siteversion As Integer, ByVal docid As Integer)
        strSql = "select usr.usr_id,usr.email,usr.name,usr.phoneNo,usr.usrType from wftransactionreview wfr " &
                 "inner join ebastusers_1 usr on usr.usr_id = wfr.userid " &
                 "where usr.usrRole = (select usrRole from ebastusers_1 where usr_id =(select userid from wftransaction where site_id = " & siteid & " and siteversion = " & siteversion & " and startdatetime is not null and enddatetime is null and docid = " & ConfigurationManager.AppSettings("ATP") & ")) " &
                 "and wfr.site_id = " & siteid & "and wfr.siteversion = " & siteversion & " and wfr.docid= " & docid

        Dim useremails As DataTable = objdbutil.ExeQueryDT(strSql, "ddd")
        Dim list As New List(Of UserInfo)
        Dim intcount As Integer = 0
        Dim semicoloncount As Integer = 0
        For intcount = 0 To useremails.Rows.Count - 1
            Dim userinfo As New UserInfo
            userinfo.UserId = useremails.Rows(intcount).Item(0)
            userinfo.Email = useremails.Rows(intcount).Item(1)
            userinfo.Username = useremails.Rows(intcount).Item(2)
            userinfo.Handphone = useremails.Rows(intcount).Item(3)
            userinfo.UserType = useremails.Rows(intcount).Item(4)
            list.Add(userinfo)
        Next
        Dim notif As New NotificationBase
        notif.SendMailReviewerATP(list, siteid, siteversion)
    End Sub

    Private Function GetWPID() As String
        If Not String.IsNullOrEmpty(Request.QueryString("wpid")) Then
            Return Request.QueryString("wpid")
        Else
            Return String.Empty
        End If
    End Function

    Private Function GetDOCID() As Integer
        If Not String.IsNullOrEmpty(Request.QueryString("docid")) Then
            Return Request.QueryString("docid")
        Else
            Return 0
        End If
    End Function

    Private Function GetFrom() As String
        If Not String.IsNullOrEmpty(Request.QueryString("from")) Then
            Return Request.QueryString("from")
        Else
            Return "def"
        End If
    End Function

#End Region
End Class
