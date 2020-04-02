Imports System.Data
Imports System.IO
Imports Common
Imports BusinessLogic
Imports Entities
Imports DAO
Imports CRFramework

Partial Class WCC_frmODWcc
    Inherits System.Web.UI.Page
    Dim dt As New DataTable
    Dim objUtil As New DBUtil
    Dim str As String
    Dim objETWcc As New ETODWcc
    Dim objEtWfTransaction As New ETWFTransaction
    Dim ObjBO As New BOODWcc
    Dim ObjBOSDoc As New BOSiteDocs
    Dim objbl As New BODDLs

    Dim dt2 As New DataTable
    Dim objdb As New DBUtil
    Dim cst As New Constants
    Dim docId As Integer = 0
    Dim i, j, k, l As Integer
    Dim strsql As String
    Dim objdo As New ETWFTransaction
    Dim dt1 As New DataTable
    Dim objET As New ETAuditTrail
    Dim objBOAT As New BoAuditTrail
    Dim roleid As Integer
    Dim grp As String
    Dim FileNamePath As String
    Dim FileNameOnly As String
    Dim ReFileName As String
    Dim objBOM As New BOMailReport
    Dim objET1 As New ETSiteDoc
    Dim objDA As New DASiteDocs

    Dim url As String
    Dim objmail As New TakeMail
    Dim oddt As New DataTable

    Dim controller As New CRController

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        imgdateissue.Attributes.Add("onclick", "javascript:popUpCalendar(this,txtDateIssued,'dd-mmm-yyyy');return false;")
        ImgWCTRDate.Attributes.Add("onclick", "javascript:popUpCalendar(this,txtStartDate,'dd-mmm-yyyy');return false;")
        If Not IsPostBack Then
            Binddata()
            BindWorkflow(DdlWorkflow)
        End If
    End Sub

    Sub Binddata()
        'str = "Exec uspTIBautOnLine " & Request.QueryString("id")
        str = "Exec uspWccBautOnLine " & Request.QueryString("id")
        dt = objUtil.ExeQueryDT(str, "SiteDoc1")
        If dt.Rows.Count > 0 Then
            lblPO.Text = dt.Rows(0).Item("pono").ToString
            hdnSiteno.Value = dt.Rows(0).Item("site_no").ToString
            hdnsiteid.Value = dt.Rows(0).Item("SiteId").ToString
            hdnversion.Value = dt.Rows(0).Item("version").ToString
            hdnWfId.Value = dt.Rows(0).Item("WF_Id").ToString
            hdndocid.Value = dt.Rows(0).Item("docid").ToString
            hdnScope.Value = dt.Rows(0).Item("Scope").ToString
            hdnDGBox.Value = dt.Rows(0).Item("DGBox").ToString
            hdnPono.Value = dt.Rows(0).Item("pono").ToString
            str = "Exec [uspGetOnLineFormBind] " & dt.Rows(0).Item("WF_Id").ToString & "," & dt.Rows(0).Item("SiteId").ToString & "" '," & dt.Rows(0).Item("docid").ToString & "," & dt.Rows(0).Item("version").ToString
            dt = objUtil.ExeQueryDT(str, "SiteDoc1")
            Dim dtv As DataView = dt.DefaultView
            dtv.Sort = "tsk_id desc"
            'DLDigitalSign.DataSource = dt
            'DLDigitalSign.DataBind()
            HDDgSignTotal.Value = dt.Rows.Count
            'Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "MyScriptddd", "getControlPosition();", True)
            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScriptxdd", "getControlPosition();", True)
        Else
            hdnsiteid.Value = 0
            hdnversion.Value = 0
            hdnWfId.Value = 0
            hdndocid.Value = 0
        End If
        '        ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScriptdd", "getControlPosition();", True)


    End Sub

    Sub UploadDocument(ByVal vers As Integer, ByVal keyval As Integer)
        ''ss
        'dt = ObjBOSDoc.uspWccGetBautDocDetailsNew(hdndocid.Value) '(Constants._Doc_SSR)
        'Dim sec As String = dt.Rows(0).Item("sec_name").ToString
        'Dim subsec As String = dt.Rows(0).Item("subsec_name").ToString
        'Dim secpath As String = ""
        'Dim ft As String = ""
        'Dim path As String = ""
        'Dim filenameorg As String
        'FileNameOnly = "-" & Constants._Doc_BAUT & "-"
        'filenameorg = hdnSiteno.Value & FileNameOnly & Format(CDate(DateTime.Now), "ddMMyyyyHHss")
        'ReFileName = filenameorg & ".htm"
        'secpath = IIf(subsec = "" Or subsec = "0", sec & "\", sec & "\" & subsec & "\")
        'ft = ConfigurationManager.AppSettings("WccType") & lblPO.Text & "-" & hdnScope.Value & "\"
        'path = ConfigurationManager.AppSettings("WccPath") & hdnSiteno.Value & ft '& secpath

        'Dim strResult As String = DOInsertTrans(hdnsiteid.Value, hdndocid.Value, vers, path)

        'Dim DocPath As String = ""
        'If strResult = "0" Then
        '    DocPath = hdnSiteno.Value & ft & CreatePDFFile(path)
        'ElseIf strResult = "1" Then
        '    DocPath = hdnSiteno.Value & ft & CreatePDFFile(path)
        'Else
        '    DocPath = hdnSiteno.Value & ft & CreatePDFFile(path)
        'End If
        '' Response.Write(DocPath)
        'With objET1
        '    .SiteID = hdnsiteid.Value
        '    .DocId = hdndocid.Value
        '    .IsUploded = 1
        '    .Version = vers
        '    .keyval = keyval
        '    .DocPath = DocPath
        '    .AT.RStatus = Constants.STATUS_ACTIVE
        '    .AT.LMBY = Session("User_Name")
        '    .orgDocPath = hdnSiteno.Value & ft & secpath & ReFileName
        '    .PONo = lblPO.Text
        'End With
        'objDA.WccUpdateDocupload1(objET1)
        ''Dim strsql As String = "Update bautmaster set Pstatus=1,ReferenceNO='" & lblBautNo.Text & "'where Site_ID = " & hdnsiteid.Value & " and SiteVersion =" & hdnversion.Value & " and PONO = '" & lblPO.Text & "'"
        ''objUtil.ExeUpdate(strsql)
        ''AuditTrail()
        'If Request.QueryString("pono") IsNot Nothing Then
        '    ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScripts", "WindowsClose();", True)
        'Else
        '    'If hdnready4baut.Value = 1 Then
        '    '    Response.Redirect("../PO/frmSiteDocUploadTree.aspx?ready=yes")
        '    'Else
        '    '    Response.Redirect("../PO/frmSiteDocUploadTree.aspx")
        '    'End If
        'End If
    End Sub

    Function DOInsertTrans(ByVal siteid As String, ByVal docName As String, ByVal version As Integer, ByVal path As String) As String
        docId = hdndocid.Value
        Dim dtNew As DataTable
        If hdnWfId.Value <> 0 Then
            dtNew = ObjBOSDoc.doinserttrans(hdnWfId.Value, docId)
            If hdnDGBox.Value = True Then
                If dtNew.Rows.Count > 0 Then
                    ObjBOSDoc.DelWFTransaction(hdndocid.Value, dtNew.Rows(0).Item(1).ToString, hdnsiteid.Value, hdnversion.Value)
                    Dim bb As Boolean
                    Dim aa As Integer = 0
                    Dim status As Integer
                    For aa = 0 To dtNew.Rows.Count - 1
                        fillDetails()
                        If (dtNew.Rows(aa).Item(5).ToString <> "1") Then status = 1
                        objdo.Status = status
                        objdo.DocId = docId
                        objdo.UserType = dtNew.Rows(aa).Item(3).ToString
                        objdo.UsrRole = dtNew.Rows(aa).Item(4).ToString
                        objdo.WFId = dtNew.Rows(aa).Item(1).ToString
                        objdo.UGP_Id = dtNew.Rows(aa).Item("grpId").ToString
                        objdo.TSK_Id = dtNew.Rows(aa).Item(5).ToString
                        objdo.XVal = dtNew.Rows(aa).Item("X_Coordinate").ToString
                        objdo.YVal = dtNew.Rows(aa).Item("Y_Coordinate").ToString
                        objdo.PageNo = dtNew.Rows(aa).Item("PageNo").ToString
                        'ObjBOSDoc.uspWccWfTransactionIU(objdo)
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
                CreateXY()
                Return "1"
            End If
        Else
            Dim status As Integer = 99
            ' objBO.uspwftransactionNOTWFI(docId, 0, hdnsiteid.Value, hdnversion.Value, 1, Session("User_Id"), status, 2, Session("User_Name"))
            Return "0"
        End If
    End Function

    Sub fillDetails()
        objdo.DocId = hdndocid.Value
        objdo.Site_Id = hdnsiteid.Value
        objdo.SiteVersion = hdnversion.Value
        objdo.AT.RStatus = Constants.STATUS_ACTIVE
        objdo.AT.LMBY = Session("User_Name")
    End Sub

    Protected Sub btnGenerate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGenerate.Click

        lblSubcontractorName.Text = txtSubconractorName.Value
        If lblSubcontractorName.Text = "" Then lblSubcontractorName.Text = "N/A"
        lblDateIssued.Text = txtDateIssued.Value
        If lblDateIssued.Text = "" Then lblDateIssued.Text = "N/A"
        lblCertificateNumber.Text = txtCertificateNumber.Value
        If lblCertificateNumber.Text = "" Then lblCertificateNumber.Text = "N/A"
        lblSiteID.Text = txtSiteId.Value
        If lblSiteID.Text = "" Then lblSiteID.Text = "N/A"
        lblSiteName.Text = txtSiteName.Value
        If lblSiteName.Text = "" Then lblSiteName.Text = "N/A"
        lblWorkPackageId.Text = txtWorkPackageID.Value
        If lblWorkPackageId.Text = "" Then lblWorkPackageId.Text = "N/A"
        lblWorkDescription.Text = txtWorkDescription.Value
        If lblWorkDescription.Text = "" Then lblWorkDescription.Text = "N/A"
        'lblStartDate.Text = txtStartDate.Value
        'If lblStartDate.Text = "" Then lblStartDate.Text = "N/A"
        'lblCompletionDate.Text = txtCompletionDate.Value
        'If lblCompletionDate.Text = "" Then lblCompletionDate.Text = "N/A"
        'lblDelayDateFromPo.Text = txtDelayDateFromPo.Value
        'If lblDelayDateFromPo.Text = "" Then lblDelayDateFromPo.Text = "N/A"
        lblPOPartner.Text = txtPoPartner.Value
        If lblPOPartner.Text = "" Then lblPOPartner.Text = "N/A"
        lblPoTelkomsel.Text = txtPoTelkomsel.Value
        If lblPoTelkomsel.Text = "" Then lblPoTelkomsel.Text = "N/A"
        lblTelkomselBaut_BastDate.Text = txtTelcomeSelBAUTDate.Value
        If lblTelkomselBaut_BastDate.Text = "" Then lblTelkomselBaut_BastDate.Text = "N/A"


        txtSubconractorName.Visible = False
        txtDateIssued.Visible = False
        txtCertificateNumber.Visible = False
        txtSiteId.Visible = False
        txtSiteName.Visible = False
        txtWorkPackageID.Visible = False
        txtWorkDescription.Visible = False
        txtPoPartner.Visible = False
        txtPoTelkomsel.Visible = False
        txtTelcomeSelBAUTDate.Visible = False
        imgdateissue.Visible = False
        
        GenerateUploadDocument(hdnversion.Value)

    End Sub

    Sub GenerateUploadDocument(ByVal vers As Integer)

        objETWcc.swid = Request.QueryString("id")
        objETWcc.PoNo = lblPO.Text
        objETWcc.Podate = Date.Now
        objETWcc.SiteNo = hdnsiteid.Value
        objETWcc.Version = vers
        objETWcc.SubcontractorName = txtSubconractorName.Value
        objETWcc.DateIssued = txtDateIssued.Value
        objETWcc.CertificateNumber = txtCertificateNumber.Value
        objETWcc.SiteID = txtSiteId.Value
        objETWcc.SiteName = txtSiteName.Value
        objETWcc.WorkPackageID = txtWorkPackageID.Value
        objETWcc.WorkDescription = txtWorkDescription.Value
        objETWcc.POPartner = txtPoPartner.Value
        objETWcc.POTelkomsel = txtPoTelkomsel.Value
        objETWcc.TelkomselBAUTBASTDate = txtTelcomeSelBAUTDate.Value
        objETWcc.AT.LMBY = Session("User_Name")

        'ObjBO.uspDocWccinsert(objETWcc)
        Dim KeyVal = Request.QueryString("KeyVal")

        UploadDocument(0, KeyVal)


        'objEtWfTransaction.DocId = "1001"
        'objEtWfTransaction.WFId = "1"
        'objEtWfTransaction.Site_Id = "323"
        'objEtWfTransaction.SiteVersion = "0"
        'objEtWfTransaction.TSK_Id = "1"
        'objEtWfTransaction.USR_Id = "181"
        'objEtWfTransaction.StartDateTime = "2009-10-27 16:11:39.703"
        'objEtWfTransaction.EndDateTime = "2009-10-29 16:11:39.703"
        'objEtWfTransaction.Status = "99"
        'objEtWfTransaction.AT.RStatus = "2"
        'objEtWfTransaction.AT.LMBY = "Administrator"
        'objEtWfTransaction.AT.LMDT = ""
        'objEtWfTransaction.XVal = "0"
        'objEtWfTransaction.YVal = "1"
        'objEtWfTransaction.UGP_Id = "1"
        'objEtWfTransaction.PageNo = "1"

        'ObjBO.uspWccWFTransactionInsert(objEtWfTransaction)

    End Sub

    Function CreatePDFFile(ByVal StrPath As String) As String
        StrPath = StrPath.Replace(" \", "\").ToString 'bugfix100816 causing error when the path containing space before \
        Dim dtba As New DataTable
        Dim filenameorg As String
        Dim ReFileName As String

        filenameorg = hdnSiteno.Value & "-WCC-" & Format(CDate(DateTime.Now), "ddMMyyyyHHss")
        ReFileName = filenameorg & ".htm"
        If (System.IO.File.Exists(StrPath & ReFileName)) Then
            System.IO.File.Delete(StrPath & ReFileName)
        End If
        If Not System.IO.Directory.Exists(StrPath) Then
            System.IO.Directory.CreateDirectory(StrPath)
        End If
        Dim sw As HtmlTextWriter = New HtmlTextWriter(New StreamWriter(StrPath & ReFileName, False, System.Text.UnicodeEncoding.UTF8))
        sw.WriteLine("<html><head><style type=""text/css"">")
        sw.WriteLine("tr{padding: 3px;}")
        sw.WriteLine(".MainCSS{margin-bottom: 0px;margin-left: 20px;margin-right: 20px;margin-top: 0px;width: 800px;height: 700px;text-align: center;}")
        sw.WriteLine(".lblText{font-family: verdana;font-size: 8pt;color: #000000;text-align: left;}")
        sw.WriteLine(".lblBText{font-family: verdana;font-size: 8pt;color: #000000;text-align: left;font-weight: bold;}")
        sw.WriteLine(".lblBold{font-family: verdana;font-size: 12pt;color: #000000;font-weight: bold;}")
        sw.WriteLine(".textFieldStyle{background-color: white;border: 1px solid;color: black;font-family: verdana;font-size: 9pt;}")
        sw.WriteLine(".GridHeader{color: #0e1b42;background-color: Orange;font-size: 9pt;font-family: verdana;text-align: Left;vertical-align: bottom;font-weight: bold;}")
        sw.WriteLine(".GridEvenRows{background-color: #e3e3e3;vertical-align: top;font-family: verdana;font-size: 8pt;color: #000000;}")
        sw.WriteLine(".GridOddRows{background-color: white;vertical-align: top;font-family: verdana;font-size: 8pt;color: #000000;}")
        sw.WriteLine(".PagerTitle{font-size: 8pt;background-color: #cddbbf;text-align: Right;vertical-align: middle;color: #25375b;font-weight: bold;}")
        sw.WriteLine(".Hcap{height: 5px;}")
        sw.WriteLine(".VCap{width: 10px;}")
        sw.WriteLine(".lblBBTextL{border-bottom: 1px #000 solid;border-left: 1px #000 solid;border-top: 1px #000 solid;font-family: verdana;font-size: 8pt;color: #000000;text-align: left;font-weight: bold;text-decoration: none;}")
        sw.WriteLine(".lblBBTextM{border-bottom: 1px #000 solid;border-top: 1px #000 solid;font-family: verdana;font-size: 8pt;color: #000000;text-align: left;font-weight: normal;text-decoration: none;width: 1%;}")
        sw.WriteLine(".lblBBTextR{border-right: 1px #000 solid;border-bottom: 1px #000 solid;border-top: 1px #000 solid;font-family: verdana;font-size: 8pt;color: #000000;text-align: left;font-weight: normal;text-decoration: none;}")
        sw.WriteLine("#lblTotalA{font-weight: bold;}")
        sw.WriteLine("#lblJobDelay{font-weight: bold;}")
        sw.WriteLine("</style></head>")
        sw.WriteLine("<body class=""MainCSS"">")
        DivWCC.RenderControl(sw)
        sw.WriteLine("</body>")
        sw.WriteLine("</html>")
        sw.Close()
        sw.Dispose()
        Return Common.TakeFileUpload.ConvertAnyFormatToPDFHtmlNew(StrPath & ReFileName, StrPath, filenameorg)
    End Function

    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        Return
    End Sub

    Sub CreateXY()
        Dim dtNew As DataTable
        Dim strSql1 As String = "select wfdid,wfid,sorder,grpcode,roleid,a.tsk_id,a.grpId,tt.tskname,0 as   X_Coordinate,0 as  Y_Coordinate," & _
                                "0 as  PageNo,IsNull(a.Sorder,0)Sno from twfdefinition A inner join tusertype B on a.grpid=b.grpid inner join ttask as tt on tt.tsk_id=a.tsk_id " & _
                     " where tt.TSK_Id=1 and wfid=" & hdnWfId.Value & "   order by wfdid"

        dtNew = objutil.ExeQueryDT(strSql1, "dd")
        If dtNew.Rows.Count > 0 Then
            'ObjBO.DelWFTransaction(hdndocid.Value, dtNew.Rows(0).Item(1).ToString, hdnsiteid.Value, hdnversion.Value)
            objUtil.ExeNonQuery("exec uspwccwftransactionD " & hdndocid.Value & "," & dtNew.Rows(0).Item(1).ToString & "," & hdnsiteid.Value & "," & hdnversion.Value & " ")
        End If
        Dim status As Integer = 0
        Dim DtNewOne As DataTable
        Dim dvIn As New DataView
        Dim dvNotIn As New DataView
        Dim J As Integer = 1
        Dim Y As Integer = 0, TopK As Integer = 0
        dvIn = dtNew.DefaultView

        dvIn.RowFilter = "TSK_Id=1"
        For TopK = 0 To dvIn.Count - 1
            fillDetails()
            objdo.Status = status
            objdo.UserType = dvIn(TopK).Item(3).ToString
            objdo.UsrRole = dvIn(TopK).Item(4).ToString
            objdo.WFId = dvIn(TopK).Item(1).ToString
            objdo.TSK_Id = dvIn(TopK).Item(5).ToString
            objdo.UGP_Id = dvIn(TopK).Item("grpId").ToString
            objdo.XVal = 0
            objdo.YVal = 0
            objdo.PageNo = 0
            objdo.Site_Id = hdnsiteid.Value
            objdo.Status = status
            'ObjBOSDoc.uspWccWfTransactionIU(objdo)
        Next
        strSql1 = "select wfdid,wfid,sorder,grpcode,roleid,a.tsk_id,a.grpId,tt.tskname,0 as   X_Coordinate,0 as  Y_Coordinate," & _
                              "0 as  PageNo,IsNull(a.Sorder,0)Sno from twfdefinition A inner join tusertype B on a.grpid=b.grpid inner join ttask as tt on tt.tsk_id=a.tsk_id " & _
                   " where tt.TSK_Id <> 1 and wfid=" & hdnWfId.Value & "   order by sorder"
        DtNewOne = objutil.ExeQueryDT(strSql1, "dd")

        dvNotIn = DtNewOne.DefaultView

        dvNotIn.RowFilter = "TSK_Id <>1"
        status = 1
        Dim bb As Boolean, intCount As Integer = 0, IncrMentY As Integer = 0
        For IncrMentX As Integer = 0 To dvNotIn.Count - 1
            'Dim iHDX As HiddenField = CType(DLDigitalSign.Items(IncrMentX).FindControl("hdXCoordinate"), HiddenField)
            'Dim iHDY As HiddenField = CType(DLDigitalSign.Items(IncrMentX).FindControl("hdYCoordinate"), HiddenField)

            fillDetails()
            objdo.Status = status
            objdo.UserType = dvNotIn(IncrMentX).Item(3).ToString
            objdo.UsrRole = dvNotIn(IncrMentX).Item(4).ToString
            objdo.WFId = dvNotIn(IncrMentX).Item(1).ToString
            objdo.TSK_Id = dvNotIn(IncrMentX).Item(5).ToString
            objdo.UGP_Id = dvNotIn(IncrMentX).Item("grpId").ToString
            'If dvNotIn.Count = 2 Then
            '    If IncrMentX = 0 Then
            '        objdo.XVal = (iHDX.Value / 2) - 13
            '    Else
            '        objdo.XVal = (iHDX.Value / 2) + (intCount * 27)
            '    End If
            'Else
            '    objdo.XVal = (iHDX.Value / 2) + (intCount * 27)
            'End If


            'objdo.YVal = 282 + (791 - iHDY.Value) + (IncrMentY * 52)

            'Y = (Math.Ceiling(iHDY.Value / 791))

            'If (IncrMentX > 0) Then

            '    If (IncrMentX Mod 2) = 0 Then
            '        intCount = 0
            '        IncrMentY = IncrMentY + 1
            '    Else
            '        intCount = intCount + 1
            '    End If
            'Else
            '    intCount = intCount + 1
            'End If

            'If Y = 0 Then Y = 1
            'Y = 1
            objdo.XVal = 0
            objdo.YVal = 0
            objdo.PageNo = 0
            objdo.Site_Id = hdnsiteid.Value
            ObjBOSDoc.uspWccWfTransactionIU(objdo)
            If bb = False Then
                ' sendmailTrans(hdnsiteid.Value, dvNotIn(IncrMentX).Item(3).ToString, dvNotIn(IncrMentX).Item(4).ToString)
                Try
                    objmail.sendmailTrans(0, hdnsiteid.Value, dvNotIn(IncrMentX).Item(3).ToString, dvNotIn(IncrMentX).Item(4).ToString, ConfigurationManager.AppSettings("uploadmailconst"))
                Catch ex As Exception
                    objdb.ExeNonQuery("exec uspErrLog '', 'mailsending','" & ex.Message.ToString.Replace("'", "''") & "','sendmailbaut'")
                End Try
                bb = True
            End If
        Next
        ' loop to update xy co-odinates
        ' loop to update xy co-odinates
        Dim strsql2 As String
        Dim dtnew2 As DataTable
        Dim dvnotin2 As DataView
        Dim tcount As Integer = 0, IncrY As Integer = 0

        strsql2 = "select wfdid,wfid,sorder,grpcode,roleid,a.tsk_id,a.grpId,tt.tskname,0 as   X_Coordinate,0 as  Y_Coordinate," & _
                              "0 as  PageNo,IsNull(a.Sorder,0)Sno from twfdefinition A inner join tusertype B on a.grpid=b.grpid inner join ttask as tt on tt.tsk_id=a.tsk_id " & _
                   " where tt.TSK_Id not in (1,5) and wfid=" & hdnWfId.Value & "   order by wfdid"
        dtnew2 = objutil.ExeQueryDT(strsql2, "dd2")

        dvnotin2 = dtnew2.DefaultView

        dvnotin2.RowFilter = "TSK_Id <>1"
        status = 1

        For IncrMentX As Integer = 0 To dvnotin2.Count - 1
            Dim iHDX As HiddenField = CType(DLDigitalSign.Items(IncrMentX).FindControl("hdXCoordinate"), HiddenField)
            Dim iHDY As HiddenField = CType(DLDigitalSign.Items(IncrMentX).FindControl("hdYCoordinate"), HiddenField)

            objdo.DocId = hdndocId.Value
            objdo.Site_Id = hdnsiteid.Value
            objdo.SiteVersion = hdnversion.Value

            objdo.Status = status
            objdo.UserType = dvnotin2(IncrMentX).Item(3).ToString
            objdo.UsrRole = dvnotin2(IncrMentX).Item(4).ToString
            objdo.WFId = dvnotin2(IncrMentX).Item(1).ToString
            objdo.TSK_Id = dvnotin2(IncrMentX).Item(5).ToString
            objdo.UGP_Id = dvnotin2(IncrMentX).Item("grpId").ToString

            Dim introw As Integer = 0
            If Request.QueryString("rw") IsNot Nothing Then
                introw = Request.QueryString("rw")
            End If
            Dim strversion As String = Request.Browser.Type
            If InStr(strversion, "IE6") > 0 Then

                If dvnotin2.Count = 2 Then

                    If IncrMentX = 0 Then
                        objdo.XVal = iHDX.Value + 30
                    Else
                        objdo.XVal = (iHDX.Value / 2) + 43 + (intCount * 30)
                    End If
                Else
                    objdo.XVal = (iHDX.Value / 2) + (intCount * 30)
                End If

                objdo.YVal = 185 + (iHDY.Value - 791) + (IncrY * 52) - (introw * 40)

            ElseIf InStr(strversion, "IE7") > 0 Then
                If dvnotin2.Count = 2 Then

                    If IncrMentX = 0 Then
                        objdo.XVal = iHDX.Value + 30
                    Else
                        objdo.XVal = (iHDX.Value / 2) + 43 + (intCount * 30)
                    End If
                Else
                    objdo.XVal = (iHDX.Value / 2) + (intCount * 30)
                End If

                objdo.YVal = 185 + (iHDY.Value - 791) + (IncrY * 52) - (introw * 40)

            ElseIf InStr(strversion, "IE8") > 0 Then

                If dvnotin2.Count = 2 Then
                    If IncrMentX = 0 Then
                        objdo.XVal = iHDX.Value + 33
                    Else
                        objdo.XVal = (iHDX.Value / 2) + 44 + (intCount * 30)
                    End If
                Else
                    objdo.XVal = (iHDX.Value / 2) + (intCount * 30)
                End If

                objdo.YVal = 195 + (791 - iHDY.Value) + (IncrY * 52) - (introw * 14)


            ElseIf InStr(strversion, "Fire") > 0 Then
                If dvnotin2.Count = 2 Then
                    If IncrMentX = 0 Then
                        objdo.XVal = iHDX.Value + 30
                    Else
                        objdo.XVal = (iHDX.Value / 2) + 56 + (intCount * 30)
                    End If
                Else
                    objdo.XVal = (iHDX.Value / 2) + (intCount * 30)
                End If
                objdo.YVal = 200 + (791 - iHDY.Value) + (IncrY * 52) + (introw * 12)
            Else
                If dvnotin2.Count = 2 Then

                    If IncrMentX = 0 Then
                        objdo.XVal = iHDX.Value + 30
                    Else
                        objdo.XVal = (iHDX.Value / 2) + 43 + (intCount * 30)
                    End If
                Else
                    objdo.XVal = (iHDX.Value / 2) + (intCount * 30)
                End If

                objdo.YVal = 185 + (iHDY.Value - 791) + (IncrY * 52) - (introw * 40)

            End If

            Y = (Math.Ceiling(iHDY.Value / 791))

            If (IncrMentX > 0) Then

                If (IncrMentX Mod 2) = 0 Then
                    tcount = 0
                    IncrY = IncrY + 1
                Else
                    tcount = tcount + 1
                End If
            Else
                tcount = tcount + 1
            End If

            If Y = 0 Then Y = 1
            Y = 1
            objdo.PageNo = Y
            objdo.Site_Id = hdnsiteid.Value
            objdb.ExeNonQuery("update WccWfTransaction set xval=" & objdo.XVal & ",yval=" & objdo.YVal & ", pageno=" & objdo.PageNo & " where site_id= " & objdo.Site_Id & " and siteversion= " & objdo.SiteVersion & " and docid= " & objdo.DocId & " and tsk_id=" & objdo.TSK_Id & "")
        Next
    End Sub

    Protected Sub DdlWorkflow_IndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DdlWorkflow.SelectedIndexChanged
        If DdlWorkflow.SelectedIndex > -1 Then
            BindSignature(Integer.Parse(DdlWorkflow.SelectedValue.ToString()), txtWorkPackageID.Value, 1)
        End If
    End Sub

#Region "Custom Methods"
    Private Sub BindWorkflow(ByVal ddl As DropDownList)
        objbl.fillDDL(DdlWorkflow, "TWorkFlow", False, Constants._DDL_Default_Select)
        DdlWorkflow.Items.Insert(0, "-- Choose Workflow --")
    End Sub

    Private Sub BindSignature(ByVal wfid As Integer, ByVal packageid As String, ByVal grpid As Integer)
        RptDigitalSign.DataSource = controller.GetUserApproverByGrouping(packageid, grpid, wfid)
        RptDigitalSign.DataBind()
    End Sub
#End Region
End Class
