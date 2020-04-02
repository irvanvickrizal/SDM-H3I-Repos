Imports BusinessLogic
Imports Common
Imports System.Data
Imports System.Web.UI
Imports Entities
Imports System.Net.Mail
Imports System.IO
Imports Common_NSNFramework
Imports System.Collections.Generic
Imports CRFramework

Partial Class frmTreeDocUploadNew
    Inherits System.Web.UI.Page
    Dim sqlstr As String
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
    Dim objBOAT As New BoAuditTrail
    Dim objdbutil As New DBUtil
    Dim objDo As New ETWFTransaction
    Dim objmail As New TakeMail
    Dim bautok As Integer
    Dim bastok As Integer
    Dim i, j As Integer
    Dim controller As New HCPTController
    Dim crcontrol As New CRController
    Dim BornIntegration As New BORNController
#Region "NSNFramework"
    Dim dbutils_nsn As New DBUtils_NSN
#End Region
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        If Page.IsPostBack = False Then
            If docid = ConfigurationManager.AppSettings("SSVDOCID") Then
                GvRejectedDocumentsUpload.Visible = True
            ElseIf docid = ConfigurationManager.AppSettings("KPIL0DOCID") Then
                GvRejectedDocumentsUpload.Visible = True
            Else
                GvRejectedDocumentsUpload.Visible = False
            End If
            objBo.fillDDL(ddlPO, "PoNo", True, "--Please Select--")
            ddlPO.SelectedItem.Text = Request.QueryString("pono").Replace("^^", " ")
            lblpo.Text = Request.QueryString("pono").Replace("^^", " ")
            ddlPO.Enabled = False
            ddlPO_SelectedIndexChanged(Nothing, Nothing)
            dt = objdbutil.ExeQueryDT("select docname,Appr_Required,DGBox from coddoc where doc_id = " & Request.QueryString("id") & "", "ddoc")
            hdndocname.Value = dt.Rows(0).Item("docname").ToString
            hdnapprequired.Value = dt.Rows(0).Item("Appr_Required").ToString
            hdnDGBox.Value = dt.Rows(0).Item("DGBox").ToString
            lbldoc.Text = hdndocname.Value
            BindBornIntegration(Integer.Parse(Request.QueryString("id")))
        End If
        BindRejectionData()
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
        Dim i As String
        Dim p As String = ddlPO.SelectedItem.Text & "-"
        Dim Scope() As String = lblsite.Text.Split("-")
        i = Scope(1)
        foldertype = ConfigurationManager.AppSettings("Type") & p & i & "\"
        Return foldertype
    End Function

    Public Sub chek4alldoc()
        Dim i As Integer
        Dim siten As String
        Dim Scope() As String = lblsite.Text.Split("-")
        siten = Scope(0)
        i = objbositedoc.chek4alldoc(hdnsiteid.Value, hdnversion.Value)
        If i = 0 Then
            hdnready4baut.Value = 1
            'insert to new table for report as final document uploaded
            objbositedoc.uspRPTUpdate(ddlPO.SelectedItem.Text, Scope(0))
        Else
            hdnready4baut.Value = 0
        End If
    End Sub

    'new code to ensure Doc on Parallel
    Public Sub check4alldocnew()
        Dim i As Integer
        Dim siten As String
        Dim Scope() As String = lblsite.Text.Split("-")
        siten = Scope(0)
        i = objbositedoc.chek4alldoc(hdnsiteid.Value, hdnversion.Value)
        If i = 0 Then
            hdnready4baut.Value = 1
            'insert to new table for report as final document uploaded
            objbositedoc.uspRPTUpdate(ddlPO.SelectedItem.Text, Scope(0))
        Else
            hdnready4baut.Value = 0
        End If
    End Sub

    Protected Sub ddldocument_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddldocument.SelectedIndexChanged
        Dim i, j As Integer
        Dim Scope() As String = lblsite.Text.Split("-")
        'bugfix100706
        'version = objbositedoc.getsiteversion(ddlPO.SelectedItem.Text, Scope(0), Scope(1))
        strSql = "select siteversion from podetails where pono='" & ddlPO.SelectedItem.Text & "' and siteno='" & Scope(0) & "' and FLDType='" & Scope(1) & "' and workpkgid=" & Scope(2)
        version = objdb.ExeQueryScalar(strSql)
        j = objdbutil.ExeQueryScalar("exec uspvalidatelink " & hdnsiteid.Value & "," & version & "," & ddldocument.SelectedItem.Value & " ")
        If j = 2 Then
            'i = objbositedoc.uspCheckIntegration(ddldocument.SelectedItem.Value, Scope(0))
            i = objdbutil.ExeQueryScalar("exec uspCheckIntegration '" & ddldocument.SelectedItem.Value & "' ,'" & Scope(0) & "'," & version & "")
            Select Case i
                Case 1
                    Dochecking()
                Case 2
                    hdnkeyval.Value = 0
                    makevisible()
                    ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "Showmain('Int');", True)
                Case 3
                    Dochecking()
                Case 4
                    makevisible()
                    hdnkeyval.Value = 0
                    ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "Showmain('IntD');", True)
                Case 3
            End Select
        Else
            hdnkeyval.Value = 0
            makevisible()
            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "Showmain('Lnk');", True)
        End If
    End Sub

    Protected Sub ddlPO_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPO.SelectedIndexChanged
        If ddlPO.SelectedItem.Text <> "--Please Select--" Then
            'here  we have to show only sites for which BAST process not finished.
            Dim lvlcode As String = Session("lvlcode")
            Dim area, region, zone, site As Integer
            area = Session("Area_Id")
            region = Session("Region_Id")
            zone = Session("Zone_Id")
            site = Session("Site_Id")
            Dim ddldt As DataTable
            ddldt = objbositedoc.uspDDLPOSiteNoByUser1(ddlPO.SelectedItem.Text, lvlcode, area, region, zone, site, Session("User_Id"))
            If ddldt.Rows.Count > 0 Then
                ddlsite.DataSource = ddldt
                ddlsite.DataTextField = "txt"
                ddlsite.DataValueField = "VAL"
                ddlsite.DataBind()
            Else
                If site = 0 Then
                    ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "Script", "alert('Site information missing for this user');", True)
                End If
            End If
        End If
        ddlsite.SelectedValue = Request.QueryString("siteid")
        Dim stValue() As String = ddlsite.SelectedValue.Split("-")
        hdnsiteid.Value = stValue(0)
        hdnpoid.Value = stValue(1)
        lblsite.Text = ddlsite.SelectedItem.Text
        'fill documents
        objBo.fillDDL(ddldocument, "coddocnew", 0, True, Constants._DDL_Default_Select)
        ddldocument.SelectedItem.Text = hdndocname.Value
        ddldocument.SelectedItem.Value = Request.QueryString("id")
        ddldocument_SelectedIndexChanged(Nothing, Nothing)
        'for wpid
        Dim scope() As String = lblsite.Text.Split("-")
        'bugfix100706
        'version = objbositedoc.getsiteversion(ddlPO.SelectedItem.Text, scope(0), scope(1))
        strSql = "select siteversion from podetails where pono='" & ddlPO.SelectedItem.Text & "' and siteno='" & scope(0) & "' and FLDType='" & scope(1) & "' and workpkgid=" & scope(2)
        version = objdb.ExeQueryScalar(strSql)
        lblwpid.Text = objdb.ExeQueryScalar("select workpkgid from podetails where siteno= '" & scope(0) & "' and pono='" & ddlPO.SelectedItem.Text & "' and siteversion=" & version & " and workpkgid=" & stValue(2))
    End Sub
#Region "UploadDocument,DoInsertTrans 20090403, CreatePDF"

    Sub uploaddocument(ByVal vers As Integer, ByVal keyval As Integer, ByVal type As Integer)
        Dim scope() As String = lblsite.Text.Split("-")
        sitename = scope(0)
        ft = ConfigurationManager.AppSettings("Type") & scope(1) & "-" & scope(2) & "\"
        Dim fpath As String = ConfigurationManager.AppSettings("BufferPath")
        path = ConfigurationManager.AppSettings("Fpath") & sitename & ft
        Dim DocPath As String = ""
        Dim err As Boolean = True
        Dim strResult As String = DOInsertTrans(hdnsiteid.Value, ddldocument.SelectedItem.Value, vers, path)
        If type = 0 Then
            FileNamePath = fileUpload.PostedFile.FileName
            FileNameOnly = System.IO.Path.GetFileName(fileUpload.PostedFile.FileName)
            ReFileName = Format(CDate(DateTime.Now), "ddMMyyyyHHss") & FileNameOnly
            sitename = scope(0)
            If strResult = "0" Or strResult = "1" Then
                'DocPath = sitename & ft & Common.TakeFileUpload.ConvertAnyFormatToPDF(fileUpload, path)
                DocPath = sitename & ft & LocalFileUpload.ConvertAnyFormatToPDF(fileUpload, path) ' local purposed only
            Else
                'DocPath = sitename & ft & Common.TakeFileUpload.ConvertAnyFormatToPDF(fileUpload, path, path + strResult)
                DocPath = sitename & ft & LocalFileUpload.ConvertAnyFormatToPDF(fileUpload, path) ' local purposed only
            End If
        Else
            ReFileName = fileUpload1.Text.Replace(" ", String.Empty)
            If fileUpload1.Text <> "" Then
                Dim fileToMove As String = fpath + "\temp\" + fileUpload1.Text
                Dim locationToMove As String = path + ReFileName
                If System.IO.File.Exists(fileToMove) = True Then
                    Try
                        If Directory.Exists(path) Then
                            If File.Exists(locationToMove) Then
                                File.Delete(locationToMove)
                            End If
                            File.Move(fileToMove, locationToMove)
                        Else
                            Directory.CreateDirectory(path)
                            System.IO.File.Move(fileToMove, locationToMove)
                        End If
                    Catch ex As Exception
                        Response.Write("<script language='javascript'>alert('File corrupted during upload, please re-upload to eBAST');</script>")
                        err = True
                    End Try
                Else
                    Response.Write("<script language='javascript'>alert('File not found in the buffer');</script>")
                    err = True
                End If
            Else
                Response.Write("<script language='javascript'>alert('File name empty');</script>")
                err = True
            End If
            DocPath = sitename & ft + ReFileName
        End If
        If err = True Then
            With objetsitedoc
                .SiteID = hdnsiteid.Value
                .DocId = ddldocument.SelectedItem.Value
                .IsUploded = 1
                .Version = vers
                .keyval = keyval
                .DocPath = DocPath
                .AT.RStatus = Constants.STATUS_ACTIVE
                .AT.LMBY = Session("User_Name")
                .orgDocPath = sitename & ft & ReFileName
                .PONo = ddlPO.SelectedItem.Text
            End With
            Try
                'objbositedoc.updatedocupload(objetsitedoc)
				Dim hcptcontrol As New HCPTController
                hcptcontrol.HCPT_UpdateDocUpload(objetsitedoc)
            Catch ex As Exception
                Response.Write("<script language='javascript'>alert('Update failed please check your uploaded file name..');</script>")
                err = False
                Exit Sub
            End Try
            If err = True Then
                lblStatus.Text = "Document Uploaded Successfully"
                sendmail2()
                chek4alldoc() 'for messaage to previous screen and saving final docupload date in reporttable
                'for BAUT
                'bautok = objbositedoc.check4BAUTBAST(hdnsiteid.Value, hdnversion.Value, 1, ddlPO.SelectedItem.Text, Session("User_Name"))
                bautok = objdb.ExeQueryScalar("exec uspcheck4BAUTBAST " & hdnsiteid.Value & " ," & hdnversion.Value & "," & 1 & ",'" & ddlPO.SelectedItem.Text & "','" & Session("User_Name") & "'," & ConfigurationManager.AppSettings("BAUTID") & "," & ConfigurationManager.AppSettings("BASTID") & " ")
                'for BAST1
                'bastok = objbositedoc.check4BAUTBAST(hdnsiteid.Value, hdnversion.Value, 2, ddlPO.SelectedItem.Text, Session("User_Name"))
                bastok = objdb.ExeQueryScalar("exec uspcheck4BAUTBAST " & hdnsiteid.Value & " ," & hdnversion.Value & "," & 2 & ",'" & ddlPO.SelectedItem.Text & "','" & Session("User_Name") & "'," & ConfigurationManager.AppSettings("BAUTID") & "," & ConfigurationManager.AppSettings("BASTID") & " ")
                If bautok = 888 Then
                    'send mail to BAUT intiator
                    i = objdbutil.ExeQueryScalar("exec uspgetBAUTBASTInitiator " & hdnsiteid.Value & "," & ConfigurationManager.AppSettings("BAUTID") & "," & 0 & " ")
                    Try
                        objmail.sendMailIniBAUTBAST(i, sitename, ConfigurationManager.AppSettings("BAUTmailconst"), ConfigurationManager.AppSettings("BAUTID"), ddlPO.SelectedItem.Text)
                    Catch ex As Exception
                        objdb.ExeNonQuery("exec uspErrLog '', 'mailsending','" & ex.Message.ToString.Replace("'", "''") & "','sendmailtransBAUTInitiator'")
                    End Try
                End If
                If bastok = 999 Then
                    'send mail to BAST initiator
                    j = objdbutil.ExeQueryScalar("exec uspgetBAUTBASTInitiator " & hdnsiteid.Value & "," & 0 & "," & ConfigurationManager.AppSettings("BASTID") & " ")
                    Try
                        objmail.sendMailIniBAUTBAST(i, sitename, ConfigurationManager.AppSettings("BASTmailconst"), ConfigurationManager.AppSettings("BASTID"), ddlPO.SelectedItem.Text)
                    Catch ex As Exception
                        objdb.ExeNonQuery("exec uspErrLog '', 'mailsending','" & ex.Message.ToString.Replace("'", "''") & "','sendmailtransBASTInitiator'")
                    End Try
                End If
                AuditTrail()
                If hdnready4baut.Value = 1 Then
                    Response.Redirect("frmSiteDocUploadTree.aspx?ready=yes")
                Else
                    Response.Redirect("frmSiteDocUploadTree.aspx")
                End If
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

            If DocIsPartofApprovalSheet(docid) = True Or hdnDGBox.Value = True Then
                dtNew = crcontrol.GetWorkflowDetail(wfid)
            Else
                dtNew = objbositedoc.doinserttrans(wfid, docid)
			End If
			
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
                Dim strSql1 As String = "select wfdid,wfid,sorder,grpcode,roleid,a.tsk_id,a.grpId,tt.tskname,0 as   X_Coordinate,0 as  Y_Coordinate," & _
                "0 as  PageNo,IsNull(a.Sorder,0)Sno from twfdefinition A inner join tusertype B on a.grpid=b.grpid inner join ttask as tt on tt.tsk_id=a.tsk_id " & _
                " where wfid=" & wfid & "   order by wfdid"
                dtNew = objdbutil.ExeQueryDT(strSql1, "dd")
                Return CreatePDFFile(strPath, wfid)
            End If
        Else
            Dim status As Integer = 99
            'means there no approval process for this document 'no workflow
            If controller.WFTransaction_D(lblwpid.Text.TrimStart().TrimEnd(), ddldocument.SelectedItem.Value) = True Then
                Dim transinfo As New DOCTransactionInfo
                transinfo.TaskId = 1
                transinfo.SiteInf.PackageId = lblwpid.Text.TrimStart().TrimEnd()
                transinfo.DocInf.DocId = ddldocument.SelectedItem.Value
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
        Dim strSql1 As String = "select wfdid,wfid,sorder,grpcode,roleid,a.tsk_id,a.grpId,tt.tskname,0 as   X_Coordinate,0 as  Y_Coordinate," & _
        "0 as  PageNo,IsNull(a.Sorder,0)Sno from twfdefinition A inner join tusertype B on a.grpid=b.grpid inner join ttask as tt on tt.tsk_id=a.tsk_id " & _
        " where tt.tsk_id =1 and wfid=" & ProcessId & "   order by wfdid"
        dt1 = objdbutil.ExeQueryDT(strSql1, "dd")
        Dim strSql2 As String = "select wfdid,wfid,sorder,grpcode,roleid,a.tsk_id,a.grpId,tt.tskname,0 as   X_Coordinate,0 as  Y_Coordinate," & _
        "0 as  PageNo,IsNull(a.Sorder,0)Sno from twfdefinition A inner join tusertype B on a.grpid=b.grpid inner join ttask as tt on tt.tsk_id=a.tsk_id " & _
        " where tt.tsk_id <> 1 and wfid=" & ProcessId & "   order by wfdid"
        dt2 = objdbutil.ExeQueryDT(strSql2, "dd")
        If dt1.Rows.Count > 0 Then
            If hdnkeyval.Value = 2 Then
                objbositedoc.DelWFTransaction(ddldocument.SelectedItem.Value, dt1.Rows(0).Item(1).ToString, hdnsiteid.Value, hdnversion.Value)
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
#End Region

    Sub fillDetails()
        objDo.Site_Id = hdnsiteid.Value
        objDo.SiteVersion = hdnversion.Value
        objDo.DocId = ddldocument.SelectedItem.Value
        objDo.AT.RStatus = Constants.STATUS_ACTIVE

        If (DocIsPartofApprovalSheet(objDo.DocId) = True) Then
            objDo.AT.LMBY = Session("User_Id")
        Else
            objDo.AT.LMBY = Session("User_Name")
		End If
		
        ' If (Request.QueryString("id") = ConfigurationManager.AppSettings("ATP")) Then
            ' objDo.AT.LMBY = Session("User_Id")
        ' Else
            ' objDo.AT.LMBY = Session("User_Name")
        ' End If
    End Sub

    Sub AuditTrail()
        objET.PoNo = Request.QueryString("pono").Replace("^^", " ")
        objET.SiteId = hdnsiteid.Value
        objET.DocId = Request.QueryString("id")
        objET.Task = "1"
        objET.Status = "1"
        objET.Userid = Session("User_Id")
        objET.Roleid = Session("Role_Id")
        Dim Scope() As String = lblsite.Text.Split("-")
        'objET.fldtype = Scope(1)
        'objBOAT.uspAuditTrailI(objET)
        '-- New code Irvan Vickrizal for audittrail
        objET.fldtype = Scope(1)

        strSql = "select siteversion from podetails where workpkgid ='" & objET.fldtype & "'"
        Dim siteversion As DataTable = objdbutil.ExeQueryDT(strSql, "ddd")

        If (siteversion.Rows.Count > 1) Then
            lblErrorMessage.Visible = True
            lblErrorMessage.Text = "There are more than one PO that have same package id, please contact administrator"
        Else
            lblErrorMessage.Visible = False
            dbutils_nsn.InsertAuditTrailNew(objET, Scope(2))
            'If (Request.QueryString("id") = ConfigurationManager.AppSettings("ATP")) Then
            '    'for Audit ATP Only -- Irvan v Code
            '    'dbutils_nsn.InsertAuditTrailNew(objET, Scope(2))
            '    dbutils_nsn.InsertAuditTrailATPNew(objET, Scope(2))
            'Else

            'End If
        End If
    End Sub
    Protected Sub GvRejectedDocumentsUpload_PageIndexChanging(ByVal sender As Object, ByVal e As GridViewPageEventArgs) Handles GvRejectedDocumentsUpload.PageIndexChanging
        GvRejectedDocumentsUpload.PageIndex = e.NewPageIndex
        BindRejectionData()
    End Sub

    Sub makevisible()
        ddlPO.Visible = False
        ddlsite.Visible = False
        ddldocument.Visible = False
    End Sub
	
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

    Sub Dochecking()
        roleid = Session("Role_Id")
        grp = Session("User_Type")
        Dim scope() As String = lblsite.Text.Split("-")
        'bugfix100706
        'version = objbositedoc.getsiteversion(ddlPO.SelectedItem.Text, scope(0), scope(1))
        strSql = "select siteversion from podetails where pono='" & ddlPO.SelectedItem.Text & "' and siteno='" & scope(0) & "' and FLDType='" & scope(1) & "' and workpkgid=" & scope(2)
        version = objdb.ExeQueryScalar(strSql)
        hdnversion.Value = version
        'bugfix101024 allow administrator to override
        Dim strQueryVerifiedPermission As String = "select count(*) from twfdefinition where wfid=(" & _
        "select wf_id from sitedoc where siteid=(select site_id from codsite where site_no ='" & scope(0) & "') and version =" & version & "and docid=" & ddldocument.SelectedItem.Value & ")" & _
        "and tsk_id = 1 and roleid=" & roleid & " and grpid =(select grpid from tusertype where grpcode='" & grp & "')"

        Dim isVerifiedPermission As Integer

        If (DocIsPartofApprovalSheet(Integer.Parse(ddldocument.SelectedValue)) = True) Then
            isVerifiedPermission = objdb.ExeQueryScalar(strQueryVerifiedPermission)
        Else
            isVerifiedPermission = objbositedoc.verifypermission(ddldocument.SelectedItem.Value, roleid, grp)
		End If
		
        ' If (Integer.Parse(ddldocument.SelectedValue) = ConfigurationManager.AppSettings("ATP")) Then
            ' isVerifiedPermission = objdb.ExeQueryScalar(strQueryVerifiedPermission)
        ' Else
            ' isVerifiedPermission = objbositedoc.verifypermission(ddldocument.SelectedItem.Value, roleid, grp)
        ' End If

        'Dim isVerifiedPermission As Integer = objdb.ExeQueryScalar(strQueryVerifiedPermission)

        If objbositedoc.uspApprRequired(hdnsiteid.Value, ddldocument.SelectedValue, hdnversion.Value) <> 0 Then
            'If objbositedoc.verifypermission(ddldocument.SelectedItem.Value, roleid, grp) <> 0 Then
            If isVerifiedPermission <> 0 Then
                Select Case objbositedoc.DocUploadverify(hdnsiteid.Value, ddldocument.SelectedItem.Value, version).ToString
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
                        If Session("User_Login") <> "sysadmin" Then
                            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "Showmain('2sta');", True)
                            makevisible()
                            Exit Sub
                        End If
                End Select
                Page.FindControl("Panel1").Visible = True
            Else
                If Session("Role_Id") = 1 Then 'if role is sysadmin then no need to verify permission everthing permitted.
                    Select Case objbositedoc.DocUploadverify(hdnsiteid.Value, ddldocument.SelectedItem.Value, version).ToString
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
                            hdnkeyval.Value = 0
                            If Session("User_Login") <> "sysadmin" Then
                                ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "Showmain('2sta');", True)
                                makevisible()
                                Exit Sub
                            End If
                    End Select
                    Page.FindControl("Panel1").Visible = True
                Else
                    hdnkeyval.Value = 0
                    Page.FindControl("Panel1").Visible = False
                    ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "Showmain('nop');", True)
                End If
            End If
            makevisible()
        Else 'Seeta 20081230 Appr Not Required
            Select Case objbositedoc.DocUploadverify(hdnsiteid.Value, ddldocument.SelectedItem.Value, version).ToString
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
                    If Session("User_Login") <> "sysadmin" Then
                        ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "Showmain('2sta');", True)
                        makevisible()
                        Exit Sub
                    End If
            End Select
            makevisible()
            Page.FindControl("Panel1").Visible = True
        End If
    End Sub

    Public Sub sendmail2()
        Dim i As Integer
        Dim siten As String
        Dim Scope() As String = lblsite.Text.Split("-")
        siten = Scope(0)
        i = objbositedoc.sendmail2(hdnsiteid.Value, ddldocument.SelectedItem.Value)
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
                'mySMTPClient.Send(myEmail)
            Catch ex As Exception
                objdb.ExeNonQuery("exec uspErrLog '', 'mailsending','" & ex.Message.ToString() & "','sendmailtrans'")
            End Try
        End If
    End Sub

    Protected Sub btnback_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnback.Click
        Response.Redirect("frmSiteDocUploadTree.aspx")
    End Sub
    Protected Sub btnupload1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnupload1.Click
        uploaddocument(hdnversion.Value, hdnkeyval.Value, 1)
    End Sub

    Protected Sub gvProcessApproved_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs) Handles gvProcessApproved.RowCommand
        If e.CommandName.Equals("opendoc") Then
            Response.Redirect("frmBORNDocTransfered.aspx?docid=" & Request.QueryString("id") & "&wpid=" & lblwpid.Text & "&ssno=" & e.CommandArgument.ToString() & "&swid=" & GetSWID())
        ElseIf e.CommandName.Equals("opendocatp") Then
            Response.Redirect("frmBORNDocTransferedATP.aspx?docid=" & Request.QueryString("id") & "&wpid=" & lblwpid.Text & "&ssno=" & e.CommandArgument.ToString() & "&swid=" & GetSWID() & "&sid=" & hdnsiteid.Value)
        End If
    End Sub

    Protected Sub gvProcessApproved_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles gvProcessApproved.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            Dim hdnTransType As HiddenField = CType(e.Row.FindControl("hdnTransType"), HiddenField)
            Dim imgUploadDoc As ImageButton = CType(e.Row.FindControl("imgUploadDoc"), ImageButton)
            Dim imgUploadDocATP As ImageButton = CType(e.Row.FindControl("imgUploadDocATP"), ImageButton)
            If hdnTransType IsNot Nothing And imgUploadDoc IsNot Nothing Then
                If (hdnTransType.Value.ToLower.Equals("atp")) Then
                    imgUploadDoc.Visible = False
                Else
                    imgUploadDocATP.Visible = False
                End If
            End If
        End If
    End Sub

#Region "Custom Methods"
    Private Sub SendATPMail(ByVal siteid As Int32, ByVal siteversion As Integer, ByVal docid As Integer)

        strSql = "select usr.usr_id,usr.email,usr.name,usr.phoneNo,usr.usrType from wftransactionreview wfr " & _
                 "inner join ebastusers_1 usr on usr.usr_id = wfr.userid " & _
                 "where usr.usrRole = (select usrRole from ebastusers_1 where usr_id =(select userid from wftransaction where site_id = " & siteid & " and siteversion = " & siteversion & " and startdatetime is not null and enddatetime is null and docid =" & docid & ")) " & _
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

    Private Sub BindRejectionData()
        Dim dtStatus As New DataTable
        sqlstr = "exec [HCPT_uspTrans_GetRejectedDocsDetail] " & CommonSite.UserId().ToString
        dtStatus = objdb.ExeQueryDT(sqlstr, "DashBoardUploadDoc")

        GvRejectedDocumentsUpload.DataSource = dtStatus
        GvRejectedDocumentsUpload.DataBind()
    End Sub

    Private Sub BindBornIntegration(ByVal docid As Integer)
        Dim hasintegrated As Boolean = BornIntegration.BORN_CheckHasIntegrated(docid)
        If hasintegrated Then
            mvBornPanel.SetActiveView(vwDocList)
            gvProcessApproved.DataSource = BornIntegration.BORN_GetDocApproved(docid, lblwpid.Text)
            gvProcessApproved.DataBind()
        Else
            mvBornPanel.SetActiveView(vwEmpty)
        End If
    End Sub

    Private Function GetSWID() As String
        If Not String.IsNullOrEmpty(Request.QueryString("sid")) Then
            Return Request.QueryString("sid")
        Else
            Return "0"
        End If
    End Function
#End Region
End Class