Imports BusinessLogic
Imports Common
Imports System.Data
Imports System.Web.UI
Imports Entities
Imports System.Net.Mail
Imports System.IO
Imports DAO

Partial Class WCC_frmWccTreeDocUpload
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
    Dim objBOAT As New BoAuditTrail
    Dim objdbutil As New DBUtil
    Dim objDo As New ETWFTransaction
    Dim objmail As New TakeMail
    Dim objDa As New DASiteDocs

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            objBo.fillDDL(ddlPO, "PoNo", True, "--Please Select--")
            ddlPO.SelectedItem.Text = Request.QueryString("pono").Replace("^^", " ")
            lblpo.Text = Request.QueryString("pono").Replace("^^", " ")
            ddlPO.Enabled = False
            ddlPO_SelectedIndexChanged(Nothing, Nothing)
            dt = objdbutil.ExeQueryDT("select docname,Appr_Required,DGBox from wcccoddoc where doc_id = " & Request.QueryString("id") & "", "ddoc")
            'objbositedoc.getsssdetails(Request.QueryString("id"))
            hdndocname.Value = dt.Rows(0).Item("docname").ToString
            hdnapprequired.Value = dt.Rows(0).Item("Appr_Required").ToString
            hdnDGBox.Value = dt.Rows(0).Item("DGBox").ToString
            lbldoc.Text = hdndocname.Value
        End If
    End Sub
    Protected Sub btnupload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnupload.Click
        'File and FilePath Save in to WccSiteDoc Table
        If fileUpload.HasFile = True Then
            Dim flength As Integer = fileUpload.PostedFile.ContentLength
            If flength > ConfigurationManager.AppSettings("filesize") Then
                Response.Write("<script>alert('File size Exceeds maximum size')</script>")
            Else
                uploaddocument(hdnversion.Value, 2) 'hdnkeyval.Value
            End If
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
        foldertype = ConfigurationManager.AppSettings("WccType") & p & i & "\"
        Return foldertype
    End Function
    Public Sub chek4alldoc()
        Dim i As Integer
        Dim siten As String
        Dim Scope() As String = lblsite.Text.Split("-")
        siten = Scope(0) 'ddlsite.SelectedItem.Text
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
        version = objbositedoc.getsiteversion(ddlPO.SelectedItem.Text, Scope(0), Scope(1))
        j = objdbutil.ExeQueryScalar("exec uspvalidatelink " & hdnsiteid.Value & "," & version & "," & ddldocument.SelectedItem.Value & " ")
        If j = 2 Then
            'i = objbositedoc.uspCheckIntegration(ddldocument.SelectedItem.Value, Scope(0))
            i = objdbutil.ExeQueryScalar("exec uspCheckIntegration  '" & ddldocument.SelectedItem.Value & "' ,'" & Scope(0) & "'," & version & "")
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
            ddldt = objDa.uspWccDDLPOSiteNoByUser1(ddlPO.SelectedItem.Text, lvlcode, area, region, zone, site, Session("User_Id"))
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



    End Sub
#Region "UploadDocument,DoInsertTrans 20090403, CreatePDF"
    Sub uploaddocument(ByVal vers As Integer, ByVal keyval As Integer)
        Dim scope() As String = lblsite.Text.Split("-")
        FileNamePath = fileUpload.PostedFile.FileName
        FileNameOnly = System.IO.Path.GetFileName(fileUpload.PostedFile.FileName)
        ReFileName = Format(CDate(DateTime.Now), "ddMMyyyyHHss") & FileNameOnly
        sitename = scope(0)
        ft = ConfigurationManager.AppSettings("WccType") & ddlPO.SelectedItem.Text & "-" & scope(1) & "\"
        path = ConfigurationManager.AppSettings("WCCPath") & sitename & ft
        sitename = scope(0)
        Dim strResult As String = DOInsertTrans(hdnsiteid.Value, ddldocument.SelectedItem.Value, vers, path)
        Dim DocPath As String = ""
        If strResult = "0" Or strResult = "1" Then
            DocPath = sitename & ft & Common.TakeFileUpload.ConvertAnyFormatToPDF(fileUpload, path)
        Else
            DocPath = sitename & ft & Common.TakeFileUpload.ConvertAnyFormatToPDF(fileUpload, path, path + strResult)
        End If
        With objetsitedoc
            .SiteID = hdnsiteid.Value
            .DocId = ddldocument.SelectedItem.Value
            .IsUploded = 1
            .Version = vers
            .keyval = keyval
            ' .DocPath = sitename & ft & secpath & Common.TakeFileUpload.ConvertAnyFormatToPDF(fileUpload, path) ' creating folder also inside this
            .DocPath = DocPath
            .AT.RStatus = Constants.STATUS_ACTIVE
            .AT.LMBY = Session("User_Name")
            .orgDocPath = sitename & ft & ReFileName
            .PONo = ddlPO.SelectedItem.Text
        End With
        objda.WccUpdateDocupload1(objetsitedoc)
        lblStatus.Text = "Document Uploaded Successfully"
        ' sendmail2()
        chek4alldoc() ' for messaage to previous screen ' and saving final docupload date in reporttable
        'for BAUT
        objbositedoc.check4BAUTBAST(hdnsiteid.Value, hdnversion.Value, 1, ddlPO.SelectedItem.Text, Session("User_Name"))
        'for BAST1
        objbositedoc.check4BAUTBAST(hdnsiteid.Value, hdnversion.Value, 2, ddlPO.SelectedItem.Text, Session("User_Name"))
        'for BAST2
        objbositedoc.check4BAUTBAST(hdnsiteid.Value, hdnversion.Value, 3, ddlPO.SelectedItem.Text, Session("User_Name"))
        'Fill Transaction table
        'siteid,docid,ver

        'AuditTrail()
        If hdnready4baut.Value = 1 Then
            Response.Redirect("frmWccSiteDocUploadTree.aspx?ready=yes")
        Else
            Response.Redirect("frmWccSiteDocUploadTree.aspx")
        End If

    End Sub
    Function DOInsertTrans(ByVal siteid As String, ByVal docid As Integer, ByVal version As Integer, ByVal strPath As String) As String
        Dim wfid As Integer
        Dim dtNew As DataTable
        wfid = objdbutil.ExeQueryScalar("select wf_id from wccsitedoc where siteid='" & siteid & "' and docid=" & docid & " and version =" & version & "")
        If wfid <> 0 Then
            dtNew = objbositedoc.doinserttrans(wfid, docid)
            Dim aa As Integer = 0
            Dim status As Integer = 0
            If hdnDGBox.Value = True Then
                If dtNew.Rows.Count > 0 Then
                    'objbositedoc.DelWFTransaction(ddldocument.SelectedItem.Value, dt.Rows(0).Item(1).ToString, ddlsite.SelectedItem.Value, hdnversion.Value)
                    objbositedoc.DelWFTransaction(ddldocument.SelectedItem.Value, dtNew.Rows(0).Item(1).ToString, hdnsiteid.Value, hdnversion.Value)
                    Dim bb As Boolean
                    For aa = 0 To dtNew.Rows.Count - 1
                        fillDetails()
                        If (dtNew.Rows(aa).Item(5).ToString <> "1") Then status = 1
                        objDo.UserType = dtNew.Rows(aa).Item(3).ToString
                        objDo.UsrRole = dtNew.Rows(aa).Item(4).ToString
                        objDo.WFId = dtNew.Rows(aa).Item(1).ToString
                        objDo.TSK_Id = dtNew.Rows(aa).Item(5).ToString
                        objDo.UGP_Id = dtNew.Rows(aa).Item("grpId").ToString
                        objDo.XVal = dtNew.Rows(aa).Item("X_Coordinate").ToString
                        objDo.YVal = dtNew.Rows(aa).Item("Y_Coordinate").ToString
                        objDo.PageNo = dtNew.Rows(aa).Item("PageNo").ToString
                        objDo.Site_Id = hdnsiteid.Value
                        objDo.Status = status
                        objbositedoc.uspwftransactionIU(objDo)
                        If (dtNew.Rows(aa).Item(5).ToString <> "1") Then
                            If bb = False Then
                                'sendmailTrans(hdnsiteid.Value, dtNew.Rows(aa).Item(3).ToString, dtNew.Rows(aa).Item(4).ToString)
                                Try
                                    objmail.sendmailTrans(0, hdnsiteid.Value, dtNew.Rows(aa).Item(3).ToString, dtNew.Rows(aa).Item(4).ToString, ConfigurationManager.AppSettings("uploadmailconst"))
                                Catch ex As Exception
                                    objdb.ExeNonQuery("exec uspErrLog '', 'mailsending','" & ex.Message.ToString.Replace("'", "''") & "','sendmailbaut'")
                                End Try
                                bb = True
                            End If
                        End If
                    Next
                End If
                Return "1"

            Else

                Dim strSql1 As String = "select wfdid,wfid,sorder,grpcode,roleid,a.tsk_id,a.grpId,tt.tskname,0 as   X_Coordinate,0 as  Y_Coordinate," & _
                         "0 as  PageNo,IsNull(a.Sorder,0)Sno from twfdefinition A inner join tusertype B on a.grpid=b.grpid inner join ttask as tt on tt.tsk_id=a.tsk_id " & _
              " where wfid=" & wfid & "   order by wfdid"
                dtNew = objdbutil.ExeQueryDT(strSql1, "dd")
                ' dtNew = objdbutil.ExeQueryScalar("select wf_id from sitedoc where siteid='" & siteid & "' and docid=" & docid & " and version =" & version & "")
                Return CreatePDFFile(strPath, wfid)
            End If
        Else
            Dim status As Integer = 99
            'means there no approval process for this document 'no workflow
            objbositedoc.uspwftransactionNOTWFI(ddldocument.SelectedItem.Value, 0, hdnsiteid.Value, hdnversion.Value, 1, Session("User_Id"), status, 2, Session("User_Name"))
            'dedy 091106
            'objdb.ExeNonQuery("exec bautcheckinsert '" & ddlPO.SelectedItem.Text & "'," & hdnsiteid.Value & ", " & hdnversion.Value & ", '" & Session("User_Id") & "'," & ConfigurationManager.AppSettings("BAUTID"))
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

            'Y = mat(dvNotIn.Count / 3.0)
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
                            'sendmailTrans(hdnsiteid.Value, dvNotIn(TopK + J).Item(3).ToString, dvNotIn(TopK + J).Item(4).ToString)
                            Try
                                objmail.sendmailTrans(0, hdnsiteid.Value, dvNotIn(TopK + J).Item(3).ToString, dvNotIn(TopK + J).Item(4).ToString, ConfigurationManager.AppSettings("uploadmailconst"))
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
#End Region

    Sub fillDetails()
        objDo.Site_Id = hdnsiteid.Value
        objDo.SiteVersion = hdnversion.Value
        objDo.DocId = ddldocument.SelectedItem.Value
        objDo.AT.RStatus = Constants.STATUS_ACTIVE
        objDo.AT.LMBY = Session("User_Name")
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
        objET.fldtype = Scope(1)
        objBOAT.uspAuditTrailI(objET)
    End Sub
    Sub makevisible()
        ddlPO.Visible = False
        ddlsite.Visible = False
        ddldocument.Visible = False
    End Sub
    Sub Dochecking()
        roleid = Session("Role_Id")
        grp = Session("User_Type")
        Dim scope() As String = lblsite.Text.Split("-")
        version = objbositedoc.getsiteversion(ddlPO.SelectedItem.Text, scope(0), scope(1))
        hdnversion.Value = version
        If objbositedoc.uspWccApprRequired(hdnsiteid.Value, ddldocument.SelectedValue, hdnversion.Value) <> 0 Then
            If objbositedoc.verifypermission(ddldocument.SelectedItem.Value, roleid, grp) <> 0 Then
                Select Case objbositedoc.uspWccDocUploadverify(hdnsiteid.Value, ddldocument.SelectedItem.Value, version).ToString
                    Case 1  ''This document not attached to this site
                        hdnkeyval.Value = 1
                        btnupload.Attributes.Clear()
                        'belowcase not going to happen we need to test the scenariao.
                        btnupload.Attributes.Add("onclick", "javascript:return " + "confirm('This Document Not attached to this site,Do you want to upload?')")
                    Case 2 '' means document was already uploaded for this version of site,do u want to overwrite
                        hdnkeyval.Value = 2
                        btnupload.Attributes.Clear()
                        btnupload.Attributes.Add("onclick", "javascript:return " + "confirm('This Document already uploaded Do you want to replace?')")
                    Case 3 '' means document was not yet uploaded for thissite
                        hdnkeyval.Value = 3
                        btnupload.Attributes.Clear()
                    Case 4 'means document already processed for sencod stage cannot upload
                        hdnkeyval.Value = 0
                        ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "Showmain('2sta');", True)
                        makevisible()
                        Exit Sub
                        'Response.Redirect("frmSiteDocUploadTree.aspx")
                End Select
                Page.FindControl("Panel1").Visible = True
            Else
                If Session("Role_Id") = 1 Then ''if role is sysadmin then no need to verify permission everthing permitted.
                    'version = objbositedoc.getsiteversion(ddlPO.SelectedItem.Text, ddlsite.SelectedItem.Text)
                    version = objbositedoc.getsiteversion(ddlPO.SelectedItem.Text, scope(0), scope(1))
                    hdnversion.Value = version
                    Select Case objbositedoc.uspWccDocUploadverify(hdnsiteid.Value, ddldocument.SelectedItem.Value, version).ToString
                        Case 1  ''This document not attached to this site
                            hdnkeyval.Value = 1
                            btnupload.Attributes.Clear()
                            'belowcase not going to happen we need to test the scenariao.
                            btnupload.Attributes.Add("onclick", "javascript:return " + "confirm('This Document Not attached to this site,Do you want to  upload?')")
                        Case 2 '' means document was already uploaded for this version of site,do u want to overwrite
                            hdnkeyval.Value = 2
                            btnupload.Attributes.Clear()
                            btnupload.Attributes.Add("onclick", "javascript:return " + "confirm('This Document already uploaded Do you want to replace?')")
                        Case 3 '' means document was not yet uploaded for thissite
                            hdnkeyval.Value = 3
                            btnupload.Attributes.Clear()
                        Case 4 'means document already processed for sencod stage cannot upload
                            ' ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "Script", "alert('This Document already processed for second stage so cannot upload again ');", True)
                            hdnkeyval.Value = 0
                            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "Showmain('2sta');", True)
                            makevisible()
                            Exit Sub
                            'Response.Redirect("frmSiteDocUploadTree.aspx")
                    End Select
                    Page.FindControl("Panel1").Visible = True
                Else
                    hdnkeyval.Value = 0
                    Page.FindControl("Panel1").Visible = False
                    'ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "Script", "alert('No permission to upload this Document');", True)
                    ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "Showmain('nop');", True)
                End If
            End If
            makevisible()
        Else 'Seeta 20081230 Appr Not Required

            ''version = objbositedoc.getsiteversion(ddlPO.SelectedItem.Text, ddlsite.SelectedItem.Text)
            'version = objbositedoc.getsiteversion(ddlPO.SelectedItem.Text, scope(0), scope(1))
            'hdnversion.Value = version
            Select Case objbositedoc.uspWccDocUploadverify(hdnsiteid.Value, ddldocument.SelectedItem.Value, version).ToString
                Case 1  ''This document not attached to this site
                    hdnkeyval.Value = 1
                    btnupload.Attributes.Clear()
                    'belowcase not going to happen we need to test the scenariao.
                    btnupload.Attributes.Add("onclick", "javascript:return " + "confirm('This Document Not attached to this site,Do you want to  upload?')")
                Case 2 '' means document was already uploaded for this version of site,do u want to overwrite
                    hdnkeyval.Value = 2
                    btnupload.Attributes.Clear()
                    btnupload.Attributes.Add("onclick", "javascript:return " + "confirm('This Document already uploaded Do you want to replace?')")
                Case 3 '' means document was not yet uploaded for thissite
                    hdnkeyval.Value = 3
                    btnupload.Attributes.Clear()
                Case 4 'means document already processed for sencod stage cannot upload
                    hdnkeyval.Value = 0
                    ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "Showmain('2sta');", True)
                    makevisible()
                    Exit Sub
                    'Response.Redirect("frmSiteDocUploadTree.aspx")
            End Select
            makevisible()
            Page.FindControl("Panel1").Visible = True
        End If

    End Sub
    Public Sub sendmail2() ' for extra document uploded
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
                mySMTPClient.Send(myEmail)
            Catch ex As Exception
                objdb.ExeNonQuery("exec uspErrLog '', 'mailsending','" & ex.Message.ToString() & "','sendmailtrans'")
            End Try
        End If
    End Sub
    Protected Sub btnback_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnback.Click
        Response.Redirect("frmWccSiteDocUploadTree.aspx")
    End Sub
End Class
