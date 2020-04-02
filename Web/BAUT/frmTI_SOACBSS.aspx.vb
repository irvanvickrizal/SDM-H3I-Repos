Imports System
Imports DAO
Imports Entities
Imports BusinessLogic
Imports System.Data
Imports Common
Imports System.IO
Partial Class BAUT_frmTI_SOACBSS
    Inherits System.Web.UI.Page
    Dim DT As New DataTable
    Dim objET1 As New ETSiteDoc
    Dim objET As New ETAuditTrail
    Dim objBo As New BOSiteDocs
    Dim objBOAT As New BoAuditTrail
    Dim docId As Integer = 0
    Dim i, j, k, l As Integer
    Dim strsql As String
    Dim objdo As New ETWFTransaction
    Dim objutil As New DBUtil
    Dim dt1 As New DataTable
    Dim grp As String
    Dim roleid As Integer
    Dim FileNamePath As String
    Dim FileNameOnly As String
    Dim ReFileName As String
    Dim objBOM As New BOMailReport
    Dim stemp As String
    Dim str As String
    Dim objdb As New DBUtil
    Dim objmail As New TakeMail
    Dim oddt As New DataTable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        btnDate.Attributes.Add("onclick", "javascript:popUpCalendar(this,txtDate,'dd-mmm-yyyy');return false;")
        btnCal1.Attributes.Add("onclick", "javascript:popUpCalendar(this,txtDated,'dd-mmm-yyyy');return false;")
        btnCal2.Attributes.Add("onclick", "javascript:popUpCalendar(this,txtCORDated,'dd-mmm-yyyy');return false;")
        btnCal3.Attributes.Add("onclick", "javascript:popUpCalendar(this,txtNoticeDt,'dd-mmm-yyyy');return false;")
        btnCal4.Attributes.Add("onclick", "javascript:popUpCalendar(this,txtOnAir,'dd-mmm-yyyy');return false;")
        If Page.IsPostBack = False Then
            Getdata()
            filloddata()
            DT = objBo.uspSiteTIDocList(Request.QueryString("id"))
            grddocuments.DataSource = DT
            grddocuments.DataBind()
            grddocuments.Columns(1).Visible = False
            grddocuments.Columns(2).Visible = False
            grddocuments.Columns(4).Visible = False
        End If
        ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScriptdd", "getControlPosition();", True)
    End Sub
    Sub filloddata()
        Dim strsql As String = "Exec odsoacdetail " & Request.QueryString("id")
        oddt = objutil.ExeQueryDT(strsql, "odsoac")
        If oddt.Rows.Count > 0 Then
            txtProj.Text = oddt.Rows(0).Item("ProjectID").ToString
            txtDate.Value = IIf(oddt.Rows(0).Item("SoacDate").ToString.IndexOf("1900") = -1, oddt.Rows(0).Item("SoacDate").ToString, "")
            txtRef.Text = oddt.Rows(0).Item("Agrefno").ToString
            txtDated.Value = IIf(oddt.Rows(0).Item("AgDate").ToString.IndexOf("1900") = -1, oddt.Rows(0).Item("AgDate").ToString, "")
            txtCOR.Text = oddt.Rows(0).Item("CRrefno").ToString
            txtCORDated.Value = IIf(oddt.Rows(0).Item("CRDate").ToString.IndexOf("1900") = -1, oddt.Rows(0).Item("CRDate").ToString, "")
            txtNoticeNo.Text = oddt.Rows(0).Item("Noticerefno").ToString
            txtNoticeDt.Value = IIf(oddt.Rows(0).Item("NoticeDate").ToString.IndexOf("1900") = -1, oddt.Rows(0).Item("NoticeDate").ToString, "")
            txtOnAir.Value = IIf(oddt.Rows(0).Item("Onair").ToString.IndexOf("1900") = -1, oddt.Rows(0).Item("Onair").ToString, "")
            txtPO.Value = oddt.Rows(0).Item("USDPO").ToString
            txtImplt.Value = oddt.Rows(0).Item("USDACT").ToString
            txtDelta.Value = oddt.Rows(0).Item("USDDelta").ToString
            txtPO0.Value = oddt.Rows(0).Item("IDRPO").ToString
            txtImplt0.Value = oddt.Rows(0).Item("IDRACT").ToString
            txtDelta0.Value = oddt.Rows(0).Item("IDRDelta").ToString
        End If
    End Sub
    Sub Getdata()
        str = "Exec uspBautOnLine " & Request.QueryString("id")
        DT = objutil.ExeQueryDT(str, "SiteDoc1")
        If DT.Rows.Count > 0 Then
            hdnsiteid.Value = DT.Rows(0).Item("SiteId").ToString
            hdnversion.Value = DT.Rows(0).Item("version").ToString
            hdnWfId.Value = DT.Rows(0).Item("WF_Id").ToString
            hdndocId.Value = DT.Rows(0).Item("docId").ToString
            hdnSiteno.Value = DT.Rows(0).Item("site_no").ToString
            hdnScope.Value = DT.Rows(0).Item("Scope").ToString
            hdnDGBox.Value = DT.Rows(0).Item("DGBox").ToString
            ' i = objBO.uspCheckIntegration(hdndocid.Value, hdnSiteno.Value)
            i = objdb.ExeQueryScalar("exec uspCheckIntegration  '" & hdndocid.Value & "' ,'" & hdnSiteno.Value & "'," & hdnversion.Value & "")
            Select Case i
                Case 1
                    Dochecking()
                Case 2
                    hdnKeyVal.Value = 0
                    ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "Showmain('Int');", True)
                Case 3
                    Dochecking()
                Case 4
                    hdnKeyVal.Value = 0
                    ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "Showmain('IntD');", True)
            End Select
            lblSite.Text = DT.Rows(0).Item("site_no") & "/" & DT.Rows(0).Item("site_name").ToString
            lblPORef.Text = DT.Rows(0).Item("pono").ToString
            lblPODated.Text = DT.Rows(0).Item("custporecdt").ToString
            lblPONO.Text = DT.Rows(0).Item("pono").ToString
            lblNumber.Text = " [SOACNumber] / [Site ID] / [BASTCode] / [LetterCode] /  [WorkCompletionMonth&Year] / [RegionCode] / [BASTMonth] / [BASTYear]"
            str = "Exec [uspGetOnLineFormBind] " & DT.Rows(0).Item("WF_Id").ToString & "," & DT.Rows(0).Item("SiteId").ToString
            DT = objutil.ExeQueryDT(str, "SiteDoc1")
            dtList.DataSource = DT
            dtList.DataBind()
            DLDigitalSign.DataSource = DT
            DLDigitalSign.DataBind()
            HDDgSignTotal.Value = DT.Rows.Count
            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScriptdd", "getControlPosition();", True)
        Else
            hdnsiteid.Value = 0
            hdnversion.Value = 0
            hdnWfId.Value = 0
            hdndocId.Value = 0
            hdnSiteno.Value = ""
        End If
    End Sub
    Sub Dochecking()
        roleid = Session("Role_Id")
        grp = Session("User_Type")
        If objBo.uspApprRequired(hdnsiteid.Value, hdndocId.Value, hdnversion.Value) <> 0 Then
            If objBo.verifypermission(hdndocId.Value, roleid, grp) <> 0 Then
                Select Case objBo.DocUploadverify(hdnsiteid.Value, hdndocId.Value, hdnversion.Value).ToString
                    Case 1  ''This document not attached to this site
                        hdnKeyVal.Value = 1
                        btnGenerate.Attributes.Clear()
                        'belowcase not going to happen we need to test the scenariao.
                        btnGenerate.Attributes.Add("onclick", "javascript:return " + "confirm('This Document Not attached to this site,Do you want to upload?')")
                    Case 2 '' means document was already uploaded for this version of site,do u want to overwrite
                        hdnKeyVal.Value = 2
                        btnGenerate.Attributes.Clear()
                        btnGenerate.Attributes.Add("onclick", "javascript:return " + "confirm('This Document already uploaded Do you want to replace?')")
                    Case 3 '' means document was not yet uploaded for thissite
                        hdnKeyVal.Value = 3
                        btnGenerate.Attributes.Clear()
                    Case 4 'means document already processed for sencod stage cannot upload
                        hdnKeyVal.Value = 0
                        ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "Showmain('2sta');", True)
                        'makevisible()
                        Exit Sub
                End Select
            Else
                If Session("Role_Id") = 1 Then ''if role is sysadmin then no need to verify permission everthing permitted.                    
                    Select Case objBo.DocUploadverify(hdnsiteid.Value, hdndocId.Value, hdnversion.Value).ToString
                        Case 1  ''This document not attached to this site
                            hdnKeyVal.Value = 1
                            btnGenerate.Attributes.Clear()
                            'belowcase not going to happen we need to test the scenariao.
                            btnGenerate.Attributes.Add("onclick", "javascript:return " + "confirm('This Document Not attached to this site,Do you want to  upload?')")
                        Case 2 '' means document was already uploaded for this version of site,do u want to overwrite
                            hdnKeyVal.Value = 2
                            btnGenerate.Attributes.Clear()
                            btnGenerate.Attributes.Add("onclick", "javascript:return " + "confirm('This Document already uploaded Do you want to replace?')")
                        Case 3 '' means document was not yet uploaded for thissite
                            hdnKeyVal.Value = 3
                            btnGenerate.Attributes.Clear()
                        Case 4 'means document already processed for sencod stage cannot upload
                            ' ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "Script", "alert('This Document already processed for second stage so cannot upload again ');", True)
                            hdnKeyVal.Value = 0
                            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "Showmain('2sta');", True)
                            Exit Sub
                            'Response.Redirect("frmSiteDocUploadTree.aspx")
                    End Select
                Else
                    hdnKeyVal.Value = 0
                    ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "Showmain('nop');", True)
                End If
            End If
        Else 'Seeta 20081230 Appr Not Required
            Select Case objBo.DocUploadverify(hdnsiteid.Value, hdndocId.Value, hdnversion.Value).ToString
                Case 1  ''This document not attached to this site
                    hdnKeyVal.Value = 1
                    btnGenerate.Attributes.Clear()
                    'belowcase not going to happen we need to test the scenariao.
                    btnGenerate.Attributes.Add("onclick", "javascript:return " + "confirm('This Document Not attached to this site,Do you want to  upload?')")
                Case 2 '' means document was already uploaded for this version of site,do u want to overwrite
                    hdnKeyVal.Value = 2
                    btnGenerate.Attributes.Clear()
                    btnGenerate.Attributes.Add("onclick", "javascript:return " + "confirm('This Document already uploaded Do you want to replace?')")
                Case 3 '' means document was not yet uploaded for thissite
                    hdnKeyVal.Value = 3
                    btnGenerate.Attributes.Clear()
                Case 4 'means document already processed for sencod stage cannot upload
                    hdnKeyVal.Value = 0
                    ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "Showmain('2sta');", True)
                    Exit Sub
                    'Response.Redirect("frmSiteDocUploadTree.aspx")
            End Select
        End If
    End Sub
    Sub uploaddocument(ByVal vers As Integer, ByVal keyval As Integer)
        DT = objBo.getbautdocdetailsNEW(hdndocId.Value) '(Constants._Doc_SSR)
        Dim sec As String = DT.Rows(0).Item("sec_name").ToString
        Dim subsec As String = DT.Rows(0).Item("subsec_name").ToString
        Dim secpath As String = ""
        Dim ft As String = ""
        Dim path As String = ""
        Dim filenameorg As String
        FileNameOnly = "-" & Constants._Doc_SQAC & "-"
        filenameorg = hdnSiteno.Value & FileNameOnly & Format(CDate(DateTime.Now), "ddMMyyyyHHss")
        ReFileName = filenameorg & ".htm"
        secpath = IIf(subsec = "" Or subsec = "0", sec & "\", sec & "\" & subsec & "\")
        ft = ConfigurationManager.AppSettings("Type") & lblPO.Text & "-" & hdnScope.Value & "\"
        path = ConfigurationManager.AppSettings("Fpath") & hdnSiteno.Value & ft & secpath
        Dim strResult As String = DOInsertTrans(hdnsiteid.Value, hdndocId.Value, vers, path)

        Dim DocPath As String = ""
        If strResult = "0" Then
            DocPath = hdnSiteno.Value & ft & secpath & CreatePDFFile(Replace(path, "  \", "\"))
        ElseIf strResult = "1" Then
            DocPath = hdnSiteno.Value & ft & secpath & CreatePDFFile(Replace(path, "  \", "\"))
        Else
            DocPath = hdnSiteno.Value & ft & secpath & CreatePDFFile(Replace(path, "  \", "\"))
        End If
        Response.Write(DocPath)
        With objET1
            .SiteID = hdnsiteid.Value
            .DocId = hdndocId.Value
            .IsUploded = 1
            .Version = vers
            .keyval = keyval
            .DocPath = DocPath
            .AT.RStatus = Constants.STATUS_ACTIVE
            .AT.LMBY = Session("User_Name")
            .orgDocPath = DocPath 'hdnSiteno.Value & ft & secpath & ReFileName
            .PONo = lblPO.Text
        End With
        objBo.updatedocupload(objET1)
        Dim strsql As String = "Update bautmaster set Pstatus=1 where Site_ID = " & hdnsiteid.Value & " and SiteVersion =" & hdnversion.Value & " and PONO = '" & lblPO.Text & "'"
        objutil.ExeUpdate(strsql)
        'sendmail2()
        chek4alldoc() ' for messaage to previous screen ' and saving final docupload date in reporttable
        'for BAUT
        'objBo.check4BAUTBAST(hdnsiteid.Value, hdnversion.Value, 1, lblPONO.Text, Session("User_Name"))
        objdb.ExeQueryScalar("exec uspcheck4BAUTBAST " & hdnsiteid.Value & " ," & hdnversion.Value & "," & 1 & ",'" & lblPONO.Text & "','" & Session("User_Name") & "'," & ConfigurationManager.AppSettings("BAUTID") & "," & ConfigurationManager.AppSettings("BASTID") & " ")
        'for BAST1
        'objBo.check4BAUTBAST(hdnsiteid.Value, hdnversion.Value, 2, lblPONO.Text, Session("User_Name"))
        objdb.ExeQueryScalar("exec uspcheck4BAUTBAST " & hdnsiteid.Value & " ," & hdnversion.Value & "," & 2 & ",'" & lblPONO.Text & "','" & Session("User_Name") & "'," & ConfigurationManager.AppSettings("BAUTID") & "," & ConfigurationManager.AppSettings("BASTID") & " ")
        'Fill Transaction table
        AuditTrail()
        If hdnready4baut.Value = 1 Then
            Response.Redirect("../PO/frmSiteDocUploadTree.aspx?ready=yes")
        Else
            Response.Redirect("../PO/frmSiteDocUploadTree.aspx")
        End If
    End Sub
    Public Sub chek4alldoc()
        Dim i As Integer
        i = objBo.chek4alldoc(hdnsiteid.Value, hdnversion.Value)
        If i = 0 Then
            hdnready4baut.Value = 1
            'insert to new table for report as final document uploaded
            objBo.uspRPTUpdate(lblPONO.Text, hdnSiteno.Value)
        Else
            hdnready4baut.Value = 0
        End If

    End Sub
    Sub AuditTrail()
        objET.PoNo = lblPONO.Text 'Request.QueryString("pono").Replace("^^", " ")
        objET.SiteId = hdnsiteid.Value
        objET.DocId = hdndocId.Value 'Request.QueryString("id")
        objET.Task = "1"
        objET.Status = "1"
        objET.Userid = Session("User_Id")
        objET.Roleid = Session("Role_Id")
        objET.fldtype = hdnScope.Value 'Scope(1)
        objBOAT.uspAuditTrailI(objET)
    End Sub
    Protected Sub btnGenerate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGenerate.Click
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScriptdd", "getControlPosition();", True)
        If Request.Browser.Browser = "IE" Then
            'added below sub for saving the online form data
            Dim strsql As String = "Exec odsoacinsert " & Request.QueryString("id") & " ,'" & lblPONO.Text & "','" & hdnsiteid.Value & "','" & hdnversion.Value & "'," & _
            "'" & txtProj.Text & "'," & IIf(txtDate.Value.ToString <> "", "'" & txtDate.Value & "'", "null") & ",'" & txtRef.Text & "'," & IIf(txtDated.Value.ToString <> "", "'" & txtDated.Value & "'", "null") & ", " & _
            "'" & txtCOR.Text & "'," & IIf(txtCORDated.Value.ToString <> "", "'" & txtCORDated.Value & "'", "null") & "," & _
            "'" & txtNoticeNo.Text & "'," & IIf(txtNoticeDt.Value.ToString <> "", "'" & txtNoticeDt.Value & "'", "null") & ", " & _
            "" & IIf(txtOnAir.Value.ToString <> "", "'" & txtOnAir.Value & "'", "null") & "," & Val(txtPO.Value) & "," & Val(txtImplt.Value) & "," & Val(txtDelta.Value) & "," & _
            "" & Val(txtPO0.Value) & "," & Val(txtImplt0.Value) & "," & Val(txtDelta0.Value) & ",'" & Session("User_Name") & "'"
            Dim result As Integer = 0
            result = objutil.ExeQueryScalar(strsql)
            If result = 1 Then
                If (hdnDGBox.Value = "") Then hdnDGBox.Value = 0
                If (hdnKeyVal.Value = "") Then hdnKeyVal.Value = 0
                If (hdnversion.Value = "") Then hdnversion.Value = 0

                If hdnDGBox.Value = True Then
                    strsql = "select count(*) from docSignPositon where doc_id=" & hdndocId.Value
                    If objutil.ExeQueryScalar(strsql) > 0 Then
                        btnDate.Visible = False
                        btnCal1.Visible = False
                        btnCal2.Visible = False
                        btnCal3.Visible = False
                        btnCal4.Visible = False

                        lblDate.Text = txtDate.Value
                        If lblDate.Text = "" Then lblDate.Text = ""
                        lblProj.Text = txtProj.Text
                        If lblProj.Text = "" Then lblProj.Text = ""
                        lblRef.Text = txtRef.Text
                        If lblRef.Text = "" Then lblRef.Text = ""
                        lblDated.Text = txtDated.Value
                        If lblDated.Text = "" Then lblDated.Text = ""
                        lblCOR.Text = txtCOR.Text
                        If lblCOR.Text = "" Then lblCOR.Text = ""
                        lblCORDated.Text = txtCORDated.Value
                        If lblCORDated.Text = "" Then lblCORDated.Text = ""
                        lblNoticeNo.Text = txtNoticeNo.Text
                        If lblNoticeNo.Text = "" Then lblNoticeNo.Text = ""
                        lblNoticeDt.Text = txtNoticeDt.Value
                        If lblNoticeDt.Text = "" Then lblNoticeDt.Text = ""
                        lblOnAir.Text = txtOnAir.Value
                        If lblOnAir.Text = "" Then lblOnAir.Text = ""
                        lblPO.Text = txtPO.Value
                        If lblPO.Text = "" Then lblPO.Text = ""
                        lblImplt.Text = txtImplt.Value
                        If lblImplt.Text = "" Then lblImplt.Text = ""
                        lblDelta.Text = txtDelta.Value
                        If lblDelta.Text = "" Then lblDelta.Text = ""
                        lblPO0.Text = txtPO.Value
                        If lblPO0.Text = "" Then lblPO0.Text = ""
                        lblImplt0.Text = txtImplt0.Value
                        If lblImplt0.Text = "" Then lblImplt0.Text = ""
                        lblDelta0.Text = txtDelta0.Value
                        If lblDelta0.Text = "" Then lblDelta0.Text = ""

                        txtDate.Visible = False
                        txtProj.Visible = False
                        txtRef.Visible = False
                        txtDated.Visible = False
                        txtCOR.Visible = False
                        txtCORDated.Visible = False
                        txtNoticeNo.Visible = False
                        txtNoticeDt.Visible = False
                        txtOnAir.Visible = False
                        txtPO.Visible = False
                        txtImplt.Visible = False
                        txtDelta.Visible = False
                        txtPO0.Visible = False
                        txtImplt0.Visible = False
                        txtDelta0.Visible = False

                        btnGenerate.Visible = False
                        grddocuments.Visible = False

                        uploaddocument(Convert.ToInt32(hdnversion.Value), Convert.ToInt32(hdnKeyVal.Value))
                        Response.Redirect("../PO/frmSiteDocUploadTree.aspx")
                    Else
                        Response.Write("<script>alert('please Set X,Y coordinates for Digital sign in document Template..')</script>")
                    End If
                Else
                    btnDate.Visible = False
                    btnCal1.Visible = False
                    btnCal2.Visible = False
                    btnCal3.Visible = False
                    btnCal4.Visible = False

                    lblDate.Text = txtDate.Value
                    If lblDate.Text = "" Then lblDate.Text = ""
                    lblProj.Text = txtProj.Text
                    If lblProj.Text = "" Then lblProj.Text = ""
                    lblRef.Text = txtRef.Text
                    If lblRef.Text = "" Then lblRef.Text = ""
                    lblDated.Text = txtDated.Value
                    If lblDated.Text = "" Then lblDated.Text = ""
                    lblCOR.Text = txtCOR.Text
                    If lblCOR.Text = "" Then lblCOR.Text = ""
                    lblCORDated.Text = txtCORDated.Value
                    If lblCORDated.Text = "" Then lblCORDated.Text = ""
                    lblNoticeNo.Text = txtNoticeNo.Text
                    If lblNoticeNo.Text = "" Then lblNoticeNo.Text = ""
                    lblNoticeDt.Text = txtNoticeDt.Value
                    If lblNoticeDt.Text = "" Then lblNoticeDt.Text = ""
                    lblOnAir.Text = txtOnAir.Value
                    If lblOnAir.Text = "" Then lblOnAir.Text = ""
                    lblPO.Text = txtPO.Value
                    If lblPO.Text = "" Then lblPO.Text = ""
                    lblImplt.Text = txtImplt.Value
                    If lblImplt.Text = "" Then lblImplt.Text = ""
                    lblDelta.Text = txtDelta.Value
                    If lblDelta.Text = "" Then lblDelta.Text = ""
                    lblPO0.Text = txtPO.Value
                    If lblPO0.Text = "" Then lblPO0.Text = ""
                    lblImplt0.Text = txtImplt0.Value
                    If lblImplt0.Text = "" Then lblImplt0.Text = ""
                    lblDelta0.Text = txtDelta0.Value
                    If lblDelta0.Text = "" Then lblDelta0.Text = ""

                    txtProj.Visible = False
                    txtRef.Visible = False
                    txtDated.Visible = False
                    txtCOR.Visible = False
                    txtCORDated.Visible = False
                    txtNoticeNo.Visible = False
                    txtNoticeDt.Visible = False
                    txtOnAir.Visible = False
                    txtPO.Visible = False
                    txtImplt.Visible = False
                    txtDelta.Visible = False
                    txtPO0.Visible = False
                    txtImplt0.Visible = False
                    txtDelta0.Visible = False

                    btnGenerate.Visible = False
                    grddocuments.Visible = False

                    uploaddocument(Convert.ToInt32(hdnversion.Value), Convert.ToInt32(hdnKeyVal.Value))
                    Response.Redirect("../PO/frmSiteDocUploadTree.aspx")
                End If
            Else
                ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('Error while saving the Data');", True)
            End If
        Else
            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('Please use Internet Explorer');", True)
        End If
    End Sub
    Function CreatePDFFile(ByVal StrPath As String) As String
        StrPath = StrPath.Replace(" \", "\").ToString 'bugfix100816 causing error when the path containing space before \
        Dim dtba As New DataTable
        Dim filenameorg As String
        Dim ReFileName As String

        filenameorg = hdnSiteno.Value & "-BAST-" & Format(CDate(DateTime.Now), "ddMMyyyyHHss")
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
        dvPrint.RenderControl(sw)
        sw.WriteLine("</body>")
        sw.WriteLine("</html>")
        sw.Close()
        sw.Dispose()
        Return Common.TakeFileUpload.ConvertAnyFormatToPDFHtmlNew(StrPath & ReFileName, StrPath, filenameorg)
        'Dim strpath As String = "bks013/ti/HOT 080475-Upgrade/Financial Calculation and Approval Document/BKS013-BAUT-090220092133.pdf"
        'Return strpath
    End Function

    Function DOInsertTrans(ByVal siteid As String, ByVal docName As String, ByVal version As Integer, ByVal path As String) As String
        docId = hdndocId.Value
        Dim dtNew As DataTable
        If hdnWfId.Value <> 0 Then
            dtNew = objBo.doinserttrans(hdnWfId.Value, docId)
            If hdnDGBox.Value = True Then
                If dtNew.Rows.Count > 0 Then
                    objBo.DelWFTransaction(hdndocId.Value, dtNew.Rows(0).Item(1).ToString, hdnsiteid.Value, hdnversion.Value)
                    Dim bb As Boolean
                    Dim aa As Integer = 0
                    Dim status As Integer
                    For aa = 0 To dtNew.Rows.Count - 1
                        fillDetails()
                        If (dtNew.Rows(aa).Item(5).ToString <> "1") Then status = 1
                        objdo.DocId = docId
                        objdo.UserType = dtNew.Rows(aa).Item(3).ToString
                        objdo.UsrRole = dtNew.Rows(aa).Item(4).ToString
                        objdo.WFId = dtNew.Rows(aa).Item(1).ToString
                        objdo.UGP_Id = dtNew.Rows(aa).Item("grpId").ToString
                        objdo.TSK_Id = dtNew.Rows(aa).Item(5).ToString
                        objdo.XVal = dtNew.Rows(aa).Item("X_Coordinate").ToString
                        objdo.YVal = dtNew.Rows(aa).Item("Y_Coordinate").ToString
                        objdo.PageNo = dtNew.Rows(aa).Item("PageNo").ToString
                        objdo.Status = status
                        objBo.uspwftransactionIU(objdo)
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
            objBo.uspwftransactionNOTWFI(docId, 0, hdnsiteid.Value, hdnversion.Value, 1, Session("User_Id"), status, 2, Session("User_Name"))
            Return "0"
        End If
    End Function
    Sub CreateXY()
        Dim dtNew As DataTable
        Dim strSql1 As String = "select wfdid,wfid,sorder,grpcode,roleid,a.tsk_id,a.grpId,tt.tskname,0 as   X_Coordinate,0 as  Y_Coordinate," & _
                                "0 as  PageNo,IsNull(a.Sorder,0)Sno from twfdefinition A inner join tusertype B on a.grpid=b.grpid inner join ttask as tt on tt.tsk_id=a.tsk_id " & _
                     " where tt.TSK_Id=1 and wfid=" & hdnWfId.Value & "   order by wfdid"
        dtNew = objUtil.ExeQueryDT(strSql1, "dd")
        ' dtNew = objdbutil.ExeQueryScalar("select wf_id from sitedoc where siteid='" & siteid & "' and docid=" & docid & " and version =" & version & "")
        If dtNew.Rows.Count > 0 Then

            objBO.DelWFTransaction(hdndocid.Value, dtNew.Rows(0).Item(1).ToString, hdnsiteid.Value, hdnversion.Value)

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
            objBO.uspwftransactionIU(objdo)
        Next
        strSql1 = "select wfdid,wfid,sorder,grpcode,roleid,a.tsk_id,a.grpId,tt.tskname,0 as   X_Coordinate,0 as  Y_Coordinate," & _
                              "0 as  PageNo,IsNull(a.Sorder,0)Sno from twfdefinition A inner join tusertype B on a.grpid=b.grpid inner join ttask as tt on tt.tsk_id=a.tsk_id " & _
                   " where tt.TSK_Id<> 1 and wfid=" & hdnWfId.Value & "   order by wfdid"
        DtNewOne = objUtil.ExeQueryDT(strSql1, "dd")

        dvNotIn = DtNewOne.DefaultView

        dvNotIn.RowFilter = "TSK_Id <>1"
        status = 1
        Dim bb As Boolean, intCount As Integer = 0, IncrMentY As Integer = 0
        For IncrMentX As Integer = 0 To dvNotIn.Count - 1
            Dim iHDX As HiddenField = CType(DLDigitalSign.Items(IncrMentX).FindControl("hdXCoordinate"), HiddenField)
            Dim iHDY As HiddenField = CType(DLDigitalSign.Items(IncrMentX).FindControl("hdYCoordinate"), HiddenField)

            fillDetails()
            objdo.Status = status
            objdo.UserType = dvNotIn(IncrMentX).Item(3).ToString
            objdo.UsrRole = dvNotIn(IncrMentX).Item(4).ToString
            objdo.WFId = dvNotIn(IncrMentX).Item(1).ToString
            objdo.TSK_Id = dvNotIn(IncrMentX).Item(5).ToString
            objdo.UGP_Id = dvNotIn(IncrMentX).Item("grpId").ToString

            Dim strversion = Request.Browser.Type
            If InStr(strversion, "IE6") > 0 Then
                If dvNotIn.Count = 2 Then
                    If IncrMentX = 0 Then
                        objdo.XVal = (iHDX.Value * 2) + 10
                    Else
                        objdo.XVal = (iHDX.Value / 2) + 86
                    End If
                Else
                    objdo.XVal = (iHDX.Value / 2) + 36 + (intCount * 30)
                End If
                objdo.YVal = 224 + (791 - iHDY.Value) + (IncrMentY * 52)
            ElseIf InStr(strversion, "IE7") > 0 Then
                If dvNotIn.Count = 2 Then
                    If IncrMentX = 0 Then
                        objdo.XVal = (iHDX.Value * 2) + 10
                    Else
                        objdo.XVal = (iHDX.Value / 2) + 86
                    End If
                Else
                    objdo.XVal = (iHDX.Value / 2) + 36 + (intCount * 30)
                End If
                objdo.YVal = 224 + (791 - iHDY.Value) + (IncrMentY * 52)
            ElseIf InStr(strversion, "IE8") > 0 Then
                If dvNotIn.Count = 2 Then
                    If IncrMentX = 0 Then
                        objdo.XVal = (iHDX.Value * 2) + 18
                    Else
                        objdo.XVal = (iHDX.Value / 2) + 86
                    End If
                Else
                    objdo.XVal = (iHDX.Value / 2) + 36 + (intCount * 30)
                End If
                objdo.YVal = (791 - iHDY.Value) + 54 + (IncrMentY * 52)
            ElseIf InStr(strversion, "Fire") > 0 Then
                If dvNotIn.Count = 2 Then
                    If IncrMentX = 0 Then
                        objdo.XVal = (iHDX.Value * 2) + 18
                    Else
                        objdo.XVal = (iHDX.Value / 2) + 86
                    End If
                Else
                    objdo.XVal = (iHDX.Value / 2) + 36 + (intCount * 30)
                End If
                objdo.YVal = (791 - iHDY.Value) + 54 + (IncrMentY * 52)
            Else
                If dvNotIn.Count = 2 Then
                    If IncrMentX = 0 Then
                        objdo.XVal = (iHDX.Value * 2) + 10
                    Else
                        objdo.XVal = (iHDX.Value / 2) + 86
                    End If
                Else
                    objdo.XVal = (iHDX.Value / 2) + 36 + (intCount * 30)
                End If
                objdo.YVal = 224 + (791 - iHDY.Value) + (IncrMentY * 52)
            End If

            Y = (Math.Ceiling(iHDY.Value / 791))

            If (IncrMentX > 0) Then

                If (IncrMentX Mod 2) = 0 Then
                    intCount = 0
                    IncrMentY = IncrMentY + 1
                Else
                    intCount = intCount + 1
                End If
            Else
                intCount = intCount + 1
            End If

            If Y = 0 Then Y = 1
            Y = 1
            objdo.PageNo = Y
            objdo.Site_Id = hdnsiteid.Value
            objdo.Status = status
            objBO.uspwftransactionIU(objdo)
            If bb = False Then
                'sendmailTrans(hdnsiteid.Value, dvNotIn(IncrMentX).Item(3).ToString, dvNotIn(IncrMentX).Item(4).ToString)

                Try
                    objmail.sendmailTrans(0, hdnsiteid.Value, dvNotIn(IncrMentX).Item(3).ToString, dvNotIn(IncrMentX).Item(4).ToString, ConfigurationManager.AppSettings("uploadmailconst"))
                Catch ex As Exception
                    objdb.ExeNonQuery("exec uspErrLog '', 'mailsending','" & ex.Message.ToString.Replace("'", "''") & "','sendmailbaut'")
                End Try
                bb = True
            End If
        Next
    End Sub
    'Sub sendmailTrans(ByVal siteid As Integer, ByVal usertype As String, ByVal usrrole As Integer)
    '    Dim dtn As New DataTable
    '    dtn = objBo.uspgetemail(siteid, usertype, usrrole)
    '    If dtn.Rows.Count > 0 Then
    '        If dtn.Rows(0).Item(3) <> "X" Then
    '            DT = objBOM.uspMailReportLD(Constants.docupload, )  ''this is fro document upload time sending mail
    '            Dim k As Integer
    '            Dim Remail As String = "'"
    '            Dim name As String = ""
    '            Dim doc As New StringBuilder
    '            Remail = dtn.Rows(0).Item(3).ToString
    '            name = dtn.Rows(0).Item(2).ToString
    '            Dim senderAdd As String = ConfigurationManager.AppSettings("Smailid")
    '            Dim receiverAdd As String = Remail
    '            Dim mySMTPClient As New System.Net.Mail.SmtpClient
    '            Dim myEmail As New System.Net.Mail.MailMessage
    '            myEmail.BodyEncoding() = System.Text.Encoding.UTF8
    '            myEmail.SubjectEncoding = System.Text.Encoding.UTF8
    '            myEmail.To.Add(receiverAdd)
    '            myEmail.Subject = dt.Rows(0).Item(3).ToString  ''subject from table
    '            myEmail.Body = dt.Rows(0).Item(2).ToString & name & ",<br> <br>" ''salutatation from table
    '            myEmail.Body = myEmail.Body & dt.Rows(0).Item(4).ToString & " <br> <br> "
    '            myEmail.Body = myEmail.Body & " Process - " & ConfigurationManager.AppSettings("Type").Replace("\", "") & " <br> <br> "
    '            Dim sb As String = ""
    '            sb = "<table  border=1>"
    '            sb = sb & "<tr><td>Contract #</td><td>Site</td><td>Scope</td><td>Document</td><td>Date Submitted</td></tr>"
    '            For k = 0 To dtn.Rows.Count - 1
    '                sb = sb & "<tr><td> " & dtn.Rows(k).Item(6).ToString & "  </td><td>" & dtn.Rows(k).Item(4).ToString & "</td><td>" & dtn.Rows(k).Item(5).ToString & " </td><td>" & dtn.Rows(k).Item(0).ToString & "</td><td>" & dtn.Rows(k).Item(7).ToString & "</td></tr>"
    '            Next
    '            sb = sb & "</table>"
    '            myEmail.Body = myEmail.Body & sb
    '            myEmail.Body = myEmail.Body & "<br/>" & DT.Rows(0).Item(5).ToString   ''closing
    '            myEmail.IsBodyHtml = True
    '            myEmail.From = New System.Net.Mail.MailAddress(senderAdd, "NSN")
    '            mySMTPClient.Host = ConfigurationManager.AppSettings("smtp")
    '            Try
    '                mySMTPClient.Send(myEmail)
    '            Catch ex As Exception
    '                objutil.ExeNonQuery("exec uspErrLog '', 'mailsending','" & ex.Message.ToString() & "','sendmailtrans'")
    '            End Try
    '        End If
    '    End If
    'End Sub
    Sub fillDetails()
        objdo.DocId = hdndocId.Value
        objdo.Site_Id = hdnsiteid.Value
        objdo.SiteVersion = hdnversion.Value
        objdo.AT.RStatus = Constants.STATUS_ACTIVE
        objdo.AT.LMBY = Session("User_Name")
    End Sub
    Sub DOThis(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rad As RadioButtonList = CType(sender, RadioButtonList)
        Dim grdr As GridViewRow = CType(rad.NamingContainer, GridViewRow)
        Dim x As Integer = grdr.RowIndex
        Dim rdl As RadioButtonList = grddocuments.Rows(x).Cells(5).FindControl("rdbstatus")
        Dim txt1 As TextBox = grddocuments.Rows(x).Cells(6).FindControl("txtremarks")
        txt1.Text = "Please enter remarks here......"
        If rdl.SelectedValue = 0 Then  'approve
            txt1.Visible = False
        Else 'reject
            txt1.Visible = True
        End If
    End Sub
    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        Return
    End Sub
    Protected Sub grddocuments_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grddocuments.RowDataBound
        Select Case e.Row.RowType
            Case DataControlRowType.DataRow
                Dim lbl As Label = CType(e.Row.FindControl("lblNo"), Label)
                lbl.Text = (grddocuments.PageIndex * grddocuments.PageSize) + (e.Row.RowIndex + 1) & ".&nbsp;"
        End Select
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim url As String = "../PO/frmViewDocument.aspx?id=" & e.Row.Cells(4).Text
            If e.Row.Cells(2).Text <> "" And e.Row.Cells(2).Text <> "&nbsp;" Then
                e.Row.Cells(3).Text = "<a href='#' onclick=""window.open('" & url & "','mywindow','status=yes,menubar=no,scrollbars=yes,resizable=yes,width=700px,height=600px')"">" & e.Row.Cells(3).Text & "</a>"
            Else
                e.Row.Cells(3).Text = e.Row.Cells(3).Text
            End If
        End If
    End Sub
End Class
