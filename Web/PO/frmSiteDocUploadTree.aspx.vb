Imports BusinessLogic
Imports Common
Imports System.Data
Imports System.Web.UI
Imports System.IO
Imports Common_NSNFramework

Partial Class frmSiteDocUploadTreeNew
    Inherits System.Web.UI.Page
    Dim dt As DataTable
    Dim dt1 As DataTable
    Dim dts As DataTable
    Dim dts1 As DataTable
    Dim dtss As DataTable
    Dim objBo As New BOSiteDocs
    Dim objbod As New BODDLs
    Dim strSql As String
    Dim dvtree As DataView
    Dim objUtil As New DBUtil
    Dim strpath As String
    Dim objbob As New BODashBoard
    Dim isbaut As Boolean = False
    Dim isbast As Boolean = False
    Dim strlink As String
    Dim version As Integer
    Dim objbositedoc As New BOSiteDocs
    Dim pid As Integer
    Dim bautc As String
#Region "NSNFramework"
    Dim dbutilsNSN As New DBUtils_NSN
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User_Login") Is Nothing Then Response.Redirect("~/SessionTimeout.aspx")
        If Page.IsPostBack = False Then
            objbod.fillDDL(ddlPO, "PoNo", True, "--Please Select--")
            If Not Request.QueryString("id") Is Nothing Then
                If Not Session("pon") Is Nothing Then
                    ddlPO.SelectedValue = Session("pon")
                    ddlPO_SelectedIndexChanged(Nothing, Nothing)
                    ddlsite.SelectedValue = Session("stno")
                    ddlsite_SelectedIndexChanged(Nothing, Nothing)
                End If
            End If
            If Not Session("pon") Is Nothing Then
                ddlPO.SelectedValue = Session("pon")
                ddlPO_SelectedIndexChanged(Nothing, Nothing)
                ddlsite.SelectedValue = Session("stno")
                ddlsite_SelectedIndexChanged(Nothing, Nothing)
            End If
            '>>>file delete operation
            If Request.QueryString("id") <> "" Then
                If Request.QueryString("del") = "1" Then
                    ''validate delete if parent is already uploaded
                    'pid = objUtil.ExeQueryScalar("select parent_id from coddoc where doc_id =(select docid from sitedoc where sw_id=" & Request.QueryString("id") & ")")
                    'bautc = objUtil.ExeQueryScalarString("select isuploaded from sitedoc where docid=" & pid & "  and siteid=(select siteid from sitedoc where sw_id=" & Request.QueryString("id") & " ) and version=(select version from sitedoc where sw_id=" & Request.QueryString("id") & ")")
                    'If bautc = "True" Then
                    '    ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "Script", "alert('Since Parent Document already uploaded you can not delete this Document.');", True)
                    '    Exit Sub
                    'End If
                    ynRedirect()
                Else
                    dt = objbob.DigitalSign(Convert.ToInt32(Request.QueryString("id")), "1")
                    strpath = ConfigurationManager.AppSettings("Fpath") & dt.Rows(0)("docpath").ToString()
                    Dim strsql As String
                    '>>> update for the ready for bast
                    strsql = "Exec uspSiteBASTDocListOnlineForm " & Request.QueryString("id")
                    dt = objUtil.ExeQueryDT(strsql, "select * from bastmaster where site_id =(select siteid from sitedoc where sw_id= " & Request.QueryString("id") & ") and siteversion=(select version from sitedoc where sw_id= " & Request.QueryString("id") & " )and pono=(select pono from sitedoc where sw_id= " & Request.QueryString("id") & " )")
                    If dt.Rows.Count > 0 Then
                        objUtil.ExeQueryScalar("update bastmaster set pstatus=0 where site_id =(select siteid from sitedoc where sw_id= " & Request.QueryString("id") & ") and siteversion=(select version from sitedoc where sw_id= " & Request.QueryString("id") & " )and pono=(select pono from sitedoc where sw_id= " & Request.QueryString("id") & " )")
                    End If
                    '>>> update for the ready for baut
                    strsql = "Exec uspSiteBASTDocListOnlineForm " & Request.QueryString("id")
                    dt = objUtil.ExeQueryDT(strsql, "select * from bastmaster where site_id =(select siteid from sitedoc where sw_id= " & Request.QueryString("id") & ") and siteversion=(select version from sitedoc where sw_id= " & Request.QueryString("id") & " )and pono=(select pono from sitedoc where sw_id= " & Request.QueryString("id") & " )")
                    If dt.Rows.Count > 0 Then
                        objUtil.ExeQueryScalar("update bastmaster set pstatus=0 where site_id =(select siteid from sitedoc where sw_id= " & Request.QueryString("id") & ") and siteversion=(select version from sitedoc where sw_id= " & Request.QueryString("id") & " )and pono=(select pono from sitedoc where sw_id= " & Request.QueryString("id") & " )")
                    End If
                    '>>> delete for other document
                    objUtil.ExeQueryScalar("update sitedoc set isUploaded=0, DocPath=NULL, OrgDocPath=NULL where sw_id=" & Request.QueryString("id"))
                    'commented below line and add 2 line by satish on 21st june2011.
                    'objUtil.ExeQueryScalar("delete from wftransaction where docid=(select docid from sitedoc where sw_id=" & Request.QueryString("id") & ") and site_id=(select siteid from sitedoc where sw_id=" & Request.QueryString("id") & ")")

                    objUtil.ExeQueryScalar("delete from wftransaction where docid=(select docid from sitedoc where sw_id=" & Request.QueryString("id") & ") and siteversion=(select version from sitedoc where sw_id=" & Request.QueryString("id") & " ) and  site_id=(select siteid from sitedoc where sw_id=" & Request.QueryString("id") & ")")
                    objUtil.ExeQueryScalar("delete from wftransactionReview where docid=(select docid from sitedoc where sw_id=" & Request.QueryString("id") & ")  and siteversion=(select version from sitedoc where sw_id=" & Request.QueryString("id") & " ) and  site_id=(select siteid from sitedoc where sw_id=" & Request.QueryString("id") & ")")



                    '>>> physical file delete
                    'bugfix110122 disabled in case later the document was needed
                    'ddlPO.SelectedValue = Session("pon")
                    'ddlPO_SelectedIndexChanged(Nothing, Nothing)
                    'ddlsite.SelectedValue = Session("stno")
                    'ddlsite_SelectedIndexChanged(Nothing, Nothing)
                    'If File.Exists(strpath) Then
                    '    File.Delete(strpath)
                    'End If
                End If
                '<<<
            End If
        End If
    End Sub

    Public Sub ynRedirect()
        Dim strContents As String
        Dim objReader As StreamReader
        Dim jspath As String = Server.MapPath("PO").Replace("\PO", "") & "\JavaScript\ynRedirect.js"
        If File.Exists(jspath) Then
            Try
                objReader = New StreamReader(jspath)
                strContents = objReader.ReadToEnd()
                objReader.Close()
                Try
                    System.Web.HttpContext.Current.Response.Write(strContents.Replace("{str}", "Proceed to delete the document?"))
                Catch ex As Exception
                    lblError.InnerText = "Error retrieving YesNo message box: " + ex.Message
                End Try
            Catch Ex As Exception
                lblError.InnerText = "Error retrieving script: " + Ex.Message
            End Try
        End If
    End Sub

    Public Function readFile(ByVal FullPath As String) As String
        Dim strContents As String
        Dim objReader As StreamReader
        Try
            objReader = New StreamReader(FullPath)
            strContents = objReader.ReadToEnd()
            objReader.Close()
            readFile = strContents
        Catch Ex As Exception
            readFile = "read file error"
        End Try
    End Function

    Protected Sub ddlPO_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        ddlsite.Items.Clear()
        TreeView1.Visible = False
        lblIntDate.InnerText = ""
        If ddlPO.SelectedItem.Text <> "--Please Select--" Then
            'here  we have to show only sites for which BAST process not finished.
            Dim lvlcode As String = Session("lvlcode")
            Dim area, region, zone, site As Integer
            area = Session("Area_Id")
            region = Session("Region_Id")
            zone = Session("Zone_Id")
            site = Session("Site_Id")
            Dim ddldt As DataTable
            ddldt = objBo.uspDDLPOSiteNoByUser1(ddlPO.SelectedItem.Value, lvlcode, area, region, zone, site, Session("User_Id"))
            If ddldt.Rows.Count > 0 Then
                ddlsite.DataSource = ddldt
                ddlsite.DataTextField = "txt"
                ddlsite.DataValueField = "VAL"
                ddlsite.DataBind()
                ddlsite.Items.Insert(0, "--Select--")
            Else
                If site = 0 Then
                    If objBo.checkdocchecklist(ddlPO.SelectedItem.Text) = 0 Then
                        lblwpid.Text = ""
                        ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "Script", "alert('Document checklist Not yet done for this PO Sites.');", True)
                    Else
                        lblwpid.Text = ""
                        ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "Script", "alert('No Site information for this  user');", True)
                    End If
                End If
            End If
        Else
            TreeView1.Visible = False
        End If
    End Sub

    Protected Sub ddlsite_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlsite.SelectedIndexChanged
        If ddlsite.SelectedItem.Text <> "--Select--" Then
            Dim stValue() As String = ddlsite.SelectedValue.Split("-")
            Dim stDesc() As String = ddlsite.SelectedItem.Text.Split("-")
            hdnsiteid.Value = stValue(0)
            hdnpoId.Value = stValue(1)
            hdnwpId.Value = stValue(2)
            TreeView1.Visible = True
            TreeView1.Nodes.Clear()
            BindTree()
            Dim sqlStr As String = "Select IsNull(REPLACE(CONVERT(VARCHAR(9),taskcompleted,6),' ','/'),'') SiteIntegration from dashboardmilestone where workpackageid=" & stDesc(2) & " and pono='" & ddlPO.SelectedItem.Text & "'"
            lblIntDate.InnerText = objUtil.ExeQueryScalarString(sqlStr)
            'for workpackageid
            'version = objbositedoc.getsiteversion(ddlPO.SelectedItem.Text, stDesc(0), stDesc(1))
            'bugfix100706
            strSql = "select siteversion from podetails where pono='" & ddlPO.SelectedItem.Text & "' and siteno='" & stDesc(0) & "' and FLDType='" & stDesc(1) & "' and workpkgid=" & stDesc(2)
            version = objUtil.ExeQueryScalar(strSql)
            lblwpid.Text = objUtil.ExeQueryScalar("select workpkgid from podetails where siteno= '" & stDesc(0) & "' and pono='" & ddlPO.SelectedItem.Text & "' and siteversion=" & version & " and workpkgid=" & stDesc(2))
            If Request.QueryString("ready") = "yes" Then
                btndone.Visible = True
                lblmsg.Visible = True
            Else
                btndone.Visible = False
                lblmsg.Visible = False
            End If
        Else
            TreeView1.Visible = False
            lblIntDate.InnerText = ""
            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "Script", "alert('Please select site');", True)
        End If
    End Sub

    Sub BindTree()
        Session("pon") = ddlPO.SelectedItem.Value
        Session("stno") = hdnsiteid.Value & "-" & hdnpoId.Value & "-" & hdnwpId.Value
        Dim firstsql As String = ""
        Dim siteandscope() As String = ddlsite.SelectedItem.Text.Split("-")
        Dim siteandpo() As String = ddlsite.SelectedItem.Value.Split("-")
        firstsql = "exec HCPT_uspSiteDocUploadTree '" & siteandpo(0) & "'," & siteandpo(1) & ",'" & ddlPO.SelectedItem.Text & "'," & ConfigurationManager.AppSettings("WCTRBASTID") & ""
        dt = objUtil.ExeQueryDT(firstsql, "CODDoc1")
		Dim dtChilds As DataTable = New HCPTController().HPCT_GetChildDoc_All(siteandpo(0), siteandpo(1), ddlPO.SelectedItem.Text, hdnwpId.Value, ConfigurationManager.AppSettings("WCTRBASTID"))
        For Each drow As DataRow In dt.Rows
            Dim node As New TreeNode
            Dim str As New StringBuilder
            Dim durl As String = ""
            str.Append(drow.Item("DocName").ToString)
            Dim po As String = ddlPO.SelectedItem.Text.Replace(" ", "^^")
            Dim strlink As String
            Dim strlink1 As String
            If drow.Item("Doctype") = "D" Then
                strlink = "frmTreeDocUpload.aspx?id=" & drow.Item("Doc_Id") & "&siteid=" & ddlsite.SelectedValue & "&pono=" & po & "&siteno=" & ddlsite.SelectedItem.Text
                If drow.Item("resval").ToString = 2 Then
                    If drow.Item("cpr") < drow.Item("tapr") Then
                        'bugfix100727 if document is rejected
                        If drow.Item("isuploaded") = "0" Then
                            str.Append("<a href=" + strlink + "><Img ID='Image1' src='../Images/doc.gif' style='border:none';onmouseover='welcome to e-RFT' /></a>")
                        Else
                            str.Append("<a><Img ID='Image1' src='../Images/doc.gif' style='border:none'/></a>")
                        End If
                    Else
                        str.Append("<a href=" + strlink + "><Img ID='Image1' src='../Images/doc.gif' style='border:none';onmouseover='welcome to e-RFT' /></a>")
                    End If
                Else
                    str.Append("<a href=" + strlink + "><Img ID='Image1' src='../Images/doc.gif' style='border:none';onmouseover='welcome to e-RFT' /></a>")
                End If
                If drow.Item("isuploaded") = "0" Then
                    str.Append("<Img ID='Image1' src='../Images/notok.jpg' style='border:none' />")
                Else
                    If drow.Item("apr") = True Then
                        If drow.Item("cpr") < drow.Item("tapr") Then
                            str.Append("<Img ID='Image1' src='../Images/upload.gif' style='border:none' />")
                            str.Append("<Img ID='Image1' src='../Images/chkyellow.png' style='border:none' />")
                            '>>>delete link
                            durl = "frmSiteDocUploadTree.aspx?id=" & drow.Item("sw_id") & "&del=1"
                            str.Append("<a href=" + durl + "><Img ID='Image1' src='../Images/deldoc.gif' style='border:none' /></a>")
                            '<<<
                            str.Append("<label id='lbl1' runat='server' class='lblTextC'>" + drow.Item("lmdt") + "</label>")
                        Else
                            str.Append("<Img ID='Image1' src='../Images/ok.jpg' style='border:none' />")
                            '>>>delete link
                            durl = "frmSiteDocUploadTree.aspx?id=" & drow.Item("sw_id") & "&del=1"
                            str.Append("<a href=" + durl + "><Img ID='Image1' src='../Images/deldoc.gif' style='border:none' /></a>")
                            '<<<
                            str.Append("<label id='lbl1' runat='server' class='lblTextC'>" + drow.Item("lmdt") + "</label>")
                        End If
                    Else
                        str.Append("<Img ID='Image1' src='../Images/ok.jpg' style='border:none' />")
                        str.Append("<label id='lbl1' runat='server' class='lblTextC'>" + drow.Item("lmdt") + "</label>")
                    End If
                End If
            Else
                If (Integer.Parse(drow.Item("doc_id")).ToString() = ConfigurationManager.AppSettings("BAUTID")) Then
                    strlink = "#"
                    strlink1 = "#"
                Else
                    strlink = "../BAUT/" & drow.Item("onlineform").ToString & "?id=" & drow.Item("SW_Id").ToString & "&wpid=" & hdnwpId.Value
                    strlink1 = "frmTreeDocUpload2.aspx?id=" & drow.Item("Doc_Id") & "&siteid=" & ddlsite.SelectedValue & "&pono=" & po & "&siteno=" & ddlsite.SelectedItem.Text
                End If
                
                If drow.Item("resval").ToString = 2 Then
                    If drow.Item("cpr") < drow.Item("tapr") Then
                        'bugfix100727 if online form is rejected
                        If drow.Item("isuploaded") = "0" Then
                            'bugfix101007 disable the bast link so that the user will use the dashboard link
                            If drow.Item("Doc_Id") <> "1031" Then
                                str.Append("<a href=" + strlink + "><Img ID='Image1' src='../Images/doce.gif' style='border:none'/></a>")
                            Else
                                str.Append("<a href='#' onclick='RFTReadyCreationWarning()'><Img ID='Image1' src='../Images/doce.gif' style='border:none'/></a>")
                            End If
                            'bugfix101013 sysadmin allows to upload online from as scanned document
                            If Session("Role_Id") = "1" Then
                                str.Append("<a href=" + strlink1 + "><Img ID='Image1' src='../Images/doc.gif' style='border:none';onmouseover='welcome to ebast' /></a>")
                            End If
                        Else
                            str.Append("<Img ID='Image1' src='../Images/doce.gif' style='border:none'/>")
                        End If
                    Else
                        'bugfix101007 disable the bast link so that the user will use the dashboard link
                        If drow.Item("Doc_Id") <> "2047" Then
                            str.Append("<a href=" + strlink + "><Img ID='Image1' src='../Images/doce.gif' style='border:none'/></a>")
                        Else
                            str.Append("<Img ID='Image1' src='../Images/doce.gif' style='border:none'/>")
                        End If
                        'bugfix101013 sysadmin allows to upload online from as scanned document
                        If Session("Role_Id") = "1" Then
                            str.Append("<a href=" + strlink1 + "><Img ID='Image1' src='../Images/doc.gif' style='border:none';onmouseover='welcome to ebast' /></a>")
                        End If
                    End If
                Else
                    'bugfix101007 disable the bast link so that the user will use the dashboard link
                    If drow.Item("Doc_Id") <> "1031" Then
                        str.Append("<a href=" + strlink + "><Img ID='Image1' src='../Images/doce.gif' style='border:none'/></a>")
                    Else
                        str.Append("<a href='#' onclick='RFTReadyCreationWarning()'><Img ID='Image1' src='../Images/doce.gif' style='border:none'/></a>")
                    End If
                    'bugfix101013 sysadmin allows to upload online from as scanned document
                    If Session("Role_Id") = "1" Then
                        str.Append("<a  href=" + strlink1 + "><Img ID='Image1' src='../Images/doc.gif' style='border:none';onmouseover='welcome to ebast' /></a>")
                    End If
                End If
                If drow.Item("isuploaded") = "0" Then
                    str.Append("<Img ID='Image1' src='../Images/notok.jpg' style='border:none' />")
                Else
                    If drow.Item("apr") = True Then
                        If drow.Item("cpr") < drow.Item("tapr") Then
                            str.Append("<Img ID='Image1' src='../Images/upload.gif' style='border:none' />")
                            str.Append("<Img ID='Image1' src='../Images/chkyellow.png' style='border:none' />")
                            '>>>delete link
                            durl = "frmSiteDocUploadTree.aspx?id=" & drow.Item("sw_id") & "&del=1"
                            str.Append("<a href=" + durl + "><Img ID='Image1' src='../Images/deldoc.gif' style='border:none' /></a>")
                            '<<<
                            str.Append("<label id='lbl1' runat='server' class='lblTextC'>" + drow.Item("lmdt") + "</label>")
                        Else
                            str.Append("<Img ID='Image1' src='../Images/ok.jpg' style='border:none' />")
                            '>>>delete link
                            durl = "frmSiteDocUploadTree.aspx?id=" & drow.Item("sw_id") & "&del=1"
                            str.Append("<a href=" + durl + "><Img ID='Image1' src='../Images/deldoc.gif' style='border:none' /></a>")
                            '<<<
                            str.Append("<label id='lbl1' runat='server' class='lblTextC'>" + drow.Item("lmdt") + "</label>")
                        End If
                    Else
                        str.Append("<Img ID='Image1' src='../Images/ok.jpg' style='border:none' />")
                        '>>>delete link
                        durl = "frmSiteDocUploadTree.aspx?id=" & drow.Item("sw_id") & "&del=1"
                        str.Append("<a href=" + durl + "><Img ID='Image1' src='../Images/deldoc.gif' style='border:none' /></a>")
                        '<<<
                        str.Append("<label id='lbl1' runat='server' class='lblTextC'>" + drow.Item("lmdt") + "</label>")
                    End If
                End If
            End If
            node.Text = str.ToString
            node.Value = drow.Item("Doc_Id") & ""
            node.SelectAction = TreeNodeSelectAction.Select
            If node.Value < 1000 Then node.ShowCheckBox = False
            TreeView1.Nodes.Add(node)
			'fillchild(node)
            fillchild(node, dtChilds)
        Next
        TreeView1.ExpandAll()
    End Sub

    Sub fillchild(ByVal tn As TreeNode)
        Dim secondsql As String = ""
        Dim siteandscope() As String = ddlsite.SelectedItem.Text.Split("-")
        Dim siteandpo() As String = ddlsite.SelectedItem.Value.Split("-")
        secondsql = "exec HCPT_uspSiteDocUploadTreeChild '" & siteandpo(0) & "'," & siteandpo(1) & "," & "'" & ddlPO.SelectedItem.Text & "'," & tn.Value & ",'" & hdnwpId.Value & "'," & ConfigurationManager.AppSettings("WCTRBASTID") & ""
        dt = objUtil.ExeQueryDT(secondsql, "CODDoc2")
        For Each drow As DataRow In dt.Rows
            Dim node As New TreeNode
            Dim str As New StringBuilder
            Dim durl As String = ""
            str.Append(drow.Item("DocName").ToString)
            Dim po As String = ddlPO.SelectedItem.Text.Replace(" ", "^^")
            Dim strlink As String
            Dim strlink1 As String
            If drow.Item("Doctype") = "D" Then
                strlink = "frmTreeDocUpload.aspx?id=" & drow.Item("Doc_Id") & "&siteid=" & ddlsite.SelectedValue & "&pono=" & po & "&siteno=" & ddlsite.SelectedItem.Text & "&swid=" & drow.Item("sw_id")
                If drow.Item("resval").ToString = 2 Then
                    If drow.Item("cpr") < drow.Item("tapr") Then
                        'bugfix100727 if document is rejected
                        If drow.Item("isuploaded") = "0" Then
                            str.Append("<a href=" + strlink + "><Img ID='Image1' src='../Images/doc.gif' style='border:none';onmouseover='welcome to ebast' /></a>")
                        Else
                            str.Append("<a><Img ID='Image1' src='../Images/doc.gif' style='border:none'/></a>")
                        End If
                    Else
                        str.Append("<a href=" + strlink + "><Img ID='Image1' src='../Images/doc.gif' style='border:none';onmouseover='welcome to ebast' /></a>")
                    End If
                Else
                    str.Append("<a href=" + strlink + "><Img ID='Image1' src='../Images/doc.gif' style='border:none';onmouseover='welcome to ebast' /></a>")
                End If

                If drow.Item("isuploaded") = "0" Then
                    str.Append("<Img ID='Image1' src='../Images/notok.jpg' style='border:none' />")
                Else
                    If drow.Item("cpr") < drow.Item("tapr") And drow.Item("apr") = True Then
                        str.Append("<Img ID='Image1' src='../Images/upload.gif' style='border:none' />")
                        str.Append("<Img ID='Image1' src='../Images/chkyellow.png' style='border:none' />")
                        '>>>delete link
                        durl = "frmSiteDocUploadTree.aspx?id=" & drow.Item("sw_id") & "&del=1"
                        str.Append("<a href=" + durl + "><Img ID='Image1' src='../Images/deldoc.gif' style='border:none' /></a>")
                        '<<<
                        str.Append("<label id='lbl1' runat='server' class='lblTextC'>" + drow.Item("lmdt") + "</label>")
                    Else
                        str.Append("<Img ID='Image1' src='../Images/ok.jpg' style='border:none' />")
                        '>>>delete link
                        durl = "frmSiteDocUploadTree.aspx?id=" & drow.Item("sw_id") & "&del=1"
                        str.Append("<a href=" + durl + "><Img ID='Image1' src='../Images/deldoc.gif' style='border:none' /></a>")
                        '<<<
                        str.Append("<label id='lbl1' runat='server' class='lblTextC'>" + drow.Item("lmdt") + "</label>")
                    End If
                End If
            Else
                Dim atpdocid As Integer = System.Configuration.ConfigurationManager.AppSettings("ATP")
                Dim isDocumentCompleted As Boolean = dbutilsNSN.IsUnderDocumentCompleted(siteandpo(0), 0, drow.Item("SW_Id"), atpdocid)
                'Dim isDocumentCompleted As Boolean = True
                If Integer.Parse(drow.Item("Doc_Id").ToString()) = atpdocid Then
                    strlink = "frmTreeDocUpload.aspx?id=" & drow.Item("Doc_Id") & "&siteid=" & ddlsite.SelectedValue & "&pono=" & po & "&siteno=" & ddlsite.SelectedItem.Text
                Else
                    strlink = "../BAUT/" & drow.Item("onlineform").ToString & "?id=" & drow.Item("SW_Id").ToString & "&wpid=" & hdnwpId.Value
                End If
                strlink1 = "frmTreeDocUpload.aspx?id=" & drow.Item("Doc_Id") & "&siteid=" & ddlsite.SelectedValue & "&pono=" & po & "&siteno=" & ddlsite.SelectedItem.Text & "&swid=" & drow.Item("sw_id")
                If drow.Item("resval").ToString = 2 Then
                    If drow.Item("cpr") < drow.Item("tapr") Then
                        'bugfix100727 if online form is rejected
                        If drow.Item("isuploaded") = "0" Then
                            If (isDocumentCompleted = True) Then
                                str.Append("<a href=" + strlink + "><Img ID='Image1' src='../Images/doce.gif' style='border:none'/></a>")
                            Else
                                str.Append("<a href='#' onclick='DocumentNotCompleted()'><Img ID='Image1' src='../Images/doce.gif' style='border:none'/></a>")
                            End If
                            'str.Append("<a href=" + strlink + "><Img ID='Image1' src='../Images/doce.gif' style='border:none'/></a>")
                            'bugfix101013 sysadmin allows to upload online from as scanned document
                            If Session("Role_Id") = "1" Then
                                str.Append("<a href=" + strlink1 + "><Img ID='Image1' src='../Images/doc.gif' style='border:none';onmouseover='welcome to ebast' /></a>")
                            End If
                        Else
                            str.Append("<a><Img ID='Image1' src='../Images/doce.gif' style='border:none'/></a>")
                        End If
                    Else
                        If (isDocumentCompleted = True) Then
                            str.Append("<a href=" + strlink + "><Img ID='Image1' src='../Images/doce.gif' style='border:none'/></a>")
                        Else
                            str.Append("<a href='#' onclick='DocumentNotCompleted()'><Img ID='Image1' src='../Images/doce.gif' style='border:none'/></a>")
                        End If

                        'bugfix101013 sysadmin allows to upload online from as scanned document
                        If Session("Role_Id") = "1" Then
                            str.Append("<a href=" + strlink1 + "><Img ID='Image1' src='../Images/doc.gif' style='border:none';onmouseover='welcome to ebast' /></a>")
                        End If
                    End If
                Else
                    If (isDocumentCompleted = True) Then
                        str.Append("<a href=" + strlink + "><Img ID='Image1' src='../Images/doce.gif' style='border:none'/></a>")
                    Else
                        str.Append("<a href='#' onclick='DocumentNotCompleted()'><Img ID='Image1' src='../Images/doce.gif' style='border:none'/></a>")
                    End If

                    'bugfix101013 sysadmin allows to upload online from as scanned document
                    If Session("Role_Id") = "1" Then
                        str.Append("<a href=" + strlink1 + "><Img ID='Image1' src='../Images/doc.gif' style='border:none';onmouseover='welcome to ebast' /></a>")
                    End If
                End If
                If drow.Item("isuploaded") = "0" Then
                    str.Append("<Img ID='Image1' src='../Images/notok.jpg' style='border:none' />")
                Else
                    If drow.Item("cpr") < drow.Item("tapr") Then
                        str.Append("<Img ID='Image1' src='../Images/upload.gif' style='border:none' />")
                        str.Append("<Img ID='Image1' src='../Images/chkyellow.png' style='border:none' />")
                        '>>>delete link
                        durl = "frmSiteDocUploadTree.aspx?id=" & drow.Item("sw_id") & "&del=1"
                        str.Append("<a href=" + durl + "><Img ID='Image1' src='../Images/deldoc.gif' style='border:none' /></a>")
                        '<<<
                        str.Append("<label id='lbl1' runat='server' class='lblTextC'>" + drow.Item("lmdt") + "</label>")
                    Else
                        str.Append("<Img ID='Image1' src='../Images/ok.jpg' style='border:none' />")
                        '>>>delete link
                        durl = "frmSiteDocUploadTree.aspx?id=" & drow.Item("sw_id") & "&del=1"
                        str.Append("<a href=" + durl + "><Img ID='Image1' src='../Images/deldoc.gif' style='border:none' /></a>")
                        '<<<
                        str.Append("<label id='lbl1' runat='server' class='lblTextC'>" + drow.Item("lmdt") + "</label>")
                    End If
                End If
            End If
            node.Text = str.ToString
            node.Value = drow.Item("Doc_Id")
            node.SelectAction = TreeNodeSelectAction.Select
            If node.Value < 1000 Then node.ShowCheckBox = False
            tn.ChildNodes.Add(node)
            If drow.Item("cc").ToString > 0 Then fillchild(node)
        Next
    End Sub
	
	Sub fillchild(ByVal tn As TreeNode, ByVal dtchild As DataTable)
        Dim secondsql As String = ""
        Dim siteandscope() As String = ddlsite.SelectedItem.Text.Split("-")
        Dim siteandpo() As String = ddlsite.SelectedItem.Value.Split("-")
        Dim dtChildDoc As DataView = dtchild.DefaultView
        dtChildDoc.RowFilter = "parent_id =" + tn.Value
        'secondsql = "exec HCPT_uspSiteDocUploadTreeChild '" & siteandpo(0) & "'," & siteandpo(1) & "," & "'" & ddlPO.SelectedItem.Text & "'," & tn.Value & ",'" & hdnwpId.Value & "'," & ConfigurationManager.AppSettings("WCTRBASTID") & ""
        'dt = objUtil.ExeQueryDT(secondsql, "CODDoc2")
        For Each drow As DataRow In dtChildDoc.ToTable.Rows
            Dim node As New TreeNode
            Dim str As New StringBuilder
            Dim durl As String = ""
            str.Append(drow.Item("DocName").ToString)
            Dim po As String = ddlPO.SelectedItem.Text.Replace(" ", "^^")
            Dim strlink As String
            Dim strlink1 As String
            If drow.Item("Doctype") = "D" Then
                strlink = "frmTreeDocUpload.aspx?id=" & drow.Item("Doc_Id") & "&siteid=" & ddlsite.SelectedValue & "&pono=" & po & "&sid=" & drow.Item("sw_id") & "&siteno=" & ddlsite.SelectedItem.Text
                If drow.Item("resval").ToString = 2 Then
                    If drow.Item("cpr") < drow.Item("tapr") Then
                        'bugfix100727 if document is rejected
                        If drow.Item("isuploaded") = "0" Then
                            str.Append("<a href=" + strlink + "><Img ID='Image1' src='../Images/doc.gif' style='border:none';onmouseover='welcome to ebast' /></a>")
                        Else
                            str.Append("<a><Img ID='Image1' src='../Images/doc.gif' style='border:none'/></a>")
                        End If
                    Else
                        str.Append("<a href=" + strlink + "><Img ID='Image1' src='../Images/doc.gif' style='border:none';onmouseover='welcome to ebast' /></a>")
                    End If
                Else
                    str.Append("<a href=" + strlink + "><Img ID='Image1' src='../Images/doc.gif' style='border:none';onmouseover='welcome to ebast' /></a>")
                End If

                If drow.Item("isuploaded") = "0" Then
                    str.Append("<Img ID='Image1' src='../Images/notok.jpg' style='border:none' />")
                Else
                    If drow.Item("cpr") < drow.Item("tapr") And drow.Item("apr") = True Then
                        str.Append("<Img ID='Image1' src='../Images/upload.gif' style='border:none' />")
                        str.Append("<Img ID='Image1' src='../Images/chkyellow.png' style='border:none' />")
                        '>>>delete link
                        durl = "frmSiteDocUploadTree.aspx?id=" & drow.Item("sw_id") & "&del=1"
                        str.Append("<a href=" + durl + "><Img ID='Image1' src='../Images/deldoc.gif' style='border:none' /></a>")
                        '<<<
                        str.Append("<label id='lbl1' runat='server' class='lblTextC'>" + drow.Item("lmdt") + "</label>")
                    Else
                        str.Append("<Img ID='Image1' src='../Images/ok.jpg' style='border:none' />")
                        '>>>delete link
                        durl = "frmSiteDocUploadTree.aspx?id=" & drow.Item("sw_id") & "&del=1"
                        str.Append("<a href=" + durl + "><Img ID='Image1' src='../Images/deldoc.gif' style='border:none' /></a>")
                        '<<<
                        str.Append("<label id='lbl1' runat='server' class='lblTextC'>" + drow.Item("lmdt") + "</label>")
                    End If
                End If
            Else
                Dim atpdocid As Integer = System.Configuration.ConfigurationManager.AppSettings("ATP")
                Dim isDocumentCompleted As Boolean = dbutilsNSN.IsUnderDocumentCompleted(siteandpo(0), 0, drow.Item("SW_Id"), atpdocid)
                'Dim isDocumentCompleted As Boolean = True
                If Integer.Parse(drow.Item("Doc_Id").ToString()) = atpdocid Then
                    strlink = "frmTreeDocUpload.aspx?id=" & drow.Item("Doc_Id") & "&siteid=" & ddlsite.SelectedValue & "&pono=" & po & "&siteno=" & ddlsite.SelectedItem.Text
                Else
                    strlink = "../BAUT/" & drow.Item("onlineform").ToString & "?id=" & drow.Item("SW_Id").ToString & "&wpid=" & hdnwpId.Value
                End If
                strlink1 = "frmTreeDocUpload.aspx?id=" & drow.Item("Doc_Id") & "&siteid=" & ddlsite.SelectedValue & "&pono=" & po & "&siteno=" & ddlsite.SelectedItem.Text & "&swid=" & drow.Item("sw_id")
                If drow.Item("resval").ToString = 2 Then
                    If drow.Item("cpr") < drow.Item("tapr") Then
                        'bugfix100727 if online form is rejected
                        If drow.Item("isuploaded") = "0" Then
                            If (isDocumentCompleted = True) Then
                                str.Append("<a href=" + strlink + "><Img ID='Image1' src='../Images/doce.gif' style='border:none'/></a>")
                            Else
                                str.Append("<a href='#' onclick='DocumentNotCompleted()'><Img ID='Image1' src='../Images/doce.gif' style='border:none'/></a>")
                            End If
                            'str.Append("<a href=" + strlink + "><Img ID='Image1' src='../Images/doce.gif' style='border:none'/></a>")
                            'bugfix101013 sysadmin allows to upload online from as scanned document
                            If Session("Role_Id") = "1" Then
                                str.Append("<a href=" + strlink1 + "><Img ID='Image1' src='../Images/doc.gif' style='border:none';onmouseover='welcome to ebast' /></a>")
                            End If
                        Else
                            str.Append("<a><Img ID='Image1' src='../Images/doce.gif' style='border:none'/></a>")
                        End If
                    Else
                        If (isDocumentCompleted = True) Then
                            str.Append("<a href=" + strlink + "><Img ID='Image1' src='../Images/doce.gif' style='border:none'/></a>")
                        Else
                            str.Append("<a href='#' onclick='DocumentNotCompleted()'><Img ID='Image1' src='../Images/doce.gif' style='border:none'/></a>")
                        End If

                        'bugfix101013 sysadmin allows to upload online from as scanned document
                        If Session("Role_Id") = "1" Then
                            str.Append("<a href=" + strlink1 + "><Img ID='Image1' src='../Images/doc.gif' style='border:none';onmouseover='welcome to ebast' /></a>")
                        End If
                    End If
                Else
                    If (isDocumentCompleted = True) Then
                        str.Append("<a href=" + strlink + "><Img ID='Image1' src='../Images/doce.gif' style='border:none'/></a>")
                    Else
                        str.Append("<a href='#' onclick='DocumentNotCompleted()'><Img ID='Image1' src='../Images/doce.gif' style='border:none'/></a>")
                    End If

                    'bugfix101013 sysadmin allows to upload online from as scanned document
                    If Session("Role_Id") = "1" Then
                        str.Append("<a href=" + strlink1 + "><Img ID='Image1' src='../Images/doc.gif' style='border:none';onmouseover='welcome to ebast' /></a>")
                    End If
                End If
                If drow.Item("isuploaded") = "0" Then
                    str.Append("<Img ID='Image1' src='../Images/notok.jpg' style='border:none' />")
                Else
                    If drow.Item("cpr") < drow.Item("tapr") Then
                        str.Append("<Img ID='Image1' src='../Images/upload.gif' style='border:none' />")
                        str.Append("<Img ID='Image1' src='../Images/chkyellow.png' style='border:none' />")
                        '>>>delete link
                        durl = "frmSiteDocUploadTree.aspx?id=" & drow.Item("sw_id") & "&del=1"
                        str.Append("<a href=" + durl + "><Img ID='Image1' src='../Images/deldoc.gif' style='border:none' /></a>")
                        '<<<
                        str.Append("<label id='lbl1' runat='server' class='lblTextC'>" + drow.Item("lmdt") + "</label>")
                    Else
                        str.Append("<Img ID='Image1' src='../Images/ok.jpg' style='border:none' />")
                        '>>>delete link
                        durl = "frmSiteDocUploadTree.aspx?id=" & drow.Item("sw_id") & "&del=1"
                        str.Append("<a href=" + durl + "><Img ID='Image1' src='../Images/deldoc.gif' style='border:none' /></a>")
                        '<<<
                        str.Append("<label id='lbl1' runat='server' class='lblTextC'>" + drow.Item("lmdt") + "</label>")
                    End If
                End If
            End If
            node.Text = str.ToString
            node.Value = drow.Item("Doc_Id")
            node.SelectAction = TreeNodeSelectAction.Select
            If node.Value < 1000 Then node.ShowCheckBox = False
            tn.ChildNodes.Add(node)
            If drow.Item("cc").ToString > 0 Then fillchild(node, dtchild)
        Next
    End Sub
	
    Protected Sub btndone_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Session("pon") = Nothing
        Response.Redirect("frmsitedocuploadtree.aspx")
    End Sub
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        For i As Integer = 0 To ddlsite.Items.Count - 1
            If UCase(ddlsite.Items(i).Text).Contains(UCase(txtSearch.Text)) = True Then
                ddlsite.SelectedIndex = i
                ddlsite_SelectedIndexChanged(Nothing, Nothing)
                Exit For
            End If
        Next
    End Sub
End Class