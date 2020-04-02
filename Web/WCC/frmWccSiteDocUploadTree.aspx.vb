Imports BusinessLogic
Imports Common
Imports System.Data
Imports System.Web.UI
Imports System.IO
Imports Entities
Imports DAO
Partial Class WCC_frmWccSiteDocUploadTree
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
    Dim objetsitedoc As New ETSiteDoc
    Dim objbositedoc As New BOSiteDocs
    Dim isbaut As Boolean = False
    Dim isbast As Boolean = False
    Dim strlink As String
    Dim FileNamePath As String
    Dim FileNameOnly As String
    Dim ReFileName As String
    Dim sitename As String
    Dim ft As String
    Dim path As String
    Dim objda As New DASiteDocs
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            objbod.fillDDL(ddlPO, "WccPoNo", True, "--Please Select--")
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
                    ynRedirect()
                Else
                    dt = objbob.DigitalSign(Convert.ToInt32(Request.QueryString("id")), "1")
                    strpath = ConfigurationManager.AppSettings("WCCpath") & dt.Rows(0)("docpath").ToString()
                    Dim strsql As String
                    '>>> update for the ready for bast
                    strsql = "Exec uspWccSiteBASTDocListOnlineForm " & Request.QueryString("id")
                    dt = objUtil.ExeQueryDT(strsql, "select * from bastmaster where site_id =(select siteid from sitedoc where sw_id= " & Request.QueryString("id") & ") and siteversion=(select version from sitedoc where sw_id= " & Request.QueryString("id") & " )and pono=(select pono from sitedoc where sw_id= " & Request.QueryString("id") & " )")
                    If dt.Rows.Count > 0 Then
                        objUtil.ExeQueryScalar("update bastmaster set pstatus=0 where site_id =(select siteid from sitedoc where sw_id= " & Request.QueryString("id") & ") and siteversion=(select version from sitedoc where sw_id= " & Request.QueryString("id") & " )and pono=(select pono from sitedoc where sw_id= " & Request.QueryString("id") & " )")
                    End If
                    '>>> update for the ready for baut
                    strsql = "Exec uspWccSiteBASTDocListOnlineForm " & Request.QueryString("id")
                    dt = objUtil.ExeQueryDT(strsql, "select * from bastmaster where site_id =(select siteid from sitedoc where sw_id= " & Request.QueryString("id") & ") and siteversion=(select version from sitedoc where sw_id= " & Request.QueryString("id") & " )and pono=(select pono from sitedoc where sw_id= " & Request.QueryString("id") & " )")
                    If dt.Rows.Count > 0 Then
                        objUtil.ExeQueryScalar("update bastmaster set pstatus=0 where site_id =(select siteid from sitedoc where sw_id= " & Request.QueryString("id") & ") and siteversion=(select version from sitedoc where sw_id= " & Request.QueryString("id") & " )and pono=(select pono from sitedoc where sw_id= " & Request.QueryString("id") & " )")
                    End If
                    '>>> delete for other document
                    objUtil.ExeQueryScalar("update sitedoc set isUploaded=0, DocPath=NULL, OrgDocPath=NULL where sw_id=" & Request.QueryString("id"))
                    '>>> physical file delete
                    If File.Exists(strpath) Then
                        File.Delete(strpath)
                    End If
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

            ddldt = objDA.uspWccDDLPOSiteNoByUser1(ddlPO.SelectedItem.Value, lvlcode, area, region, zone, site, Session("User_Id"))
            If ddldt.Rows.Count > 0 Then
                ddlsite.DataSource = ddldt
                ddlsite.DataTextField = "txt"
                ddlsite.DataValueField = "VAL"
                ddlsite.DataBind()
                ddlsite.Items.Insert(0, "--Select--")

            Else
                If site = 0 Then
                    If objBo.checkdocchecklist(ddlPO.SelectedItem.Text) = 0 Then

                        ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "Script", "alert('Document checklist Not yet done for this PO Sites.');", True)
                    Else
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
            TreeView1.Visible = True
            TreeView1.Nodes.Clear()
            BindTree()
            lblsite.Text = ddlsite.SelectedItem.Text
            'Dim sqlStr As String = "Select IsNull(REPLACE(CONVERT(VARCHAR(9), SiteIntegration, 6), ' ', '/'),'')SiteIntegration from epmData where siteid='" & stDesc(0) & "'"
            Dim sqlStr As String = "Select IsNull(REPLACE(CONVERT(VARCHAR(9), SiteIntegration, 6), ' ', '/'),'')SiteIntegration from epmdata where  workpackageid in ( select workpkgid from wccpodetails where siteno= '" & stDesc(0) & "' and po_id='" & hdnpoId.Value & "')"
            lblIntDate.InnerText = objUtil.ExeQueryScalarString(sqlStr)
            'If Request.QueryString("ready") = "yes" Then
            '    btndone.Visible = True
            '    lblmsg.Visible = True
            'Else
            '    btndone.Visible = False
            '    lblmsg.Visible = False
            'End If
        Else
            TreeView1.Visible = False
            lblIntDate.InnerText = ""
            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "Script", "alert('Please select site');", True)
        End If
    End Sub
    Sub BindTree()
        Session("pon") = ddlPO.SelectedItem.Value
        Session("stno") = hdnsiteid.Value & "-" & hdnpoId.Value
        Dim firstsql As String = ""
        Dim siteandscope() As String = ddlsite.SelectedItem.Text.Split("-")
        Dim siteandpo() As String = ddlsite.SelectedItem.Value.Split("-")
        firstsql = "exec uspWccSiteDocUploadTree '" & siteandpo(0) & "'," & siteandpo(1) & ",'" & ddlPO.SelectedItem.Text & "'"
        dt = objUtil.ExeQueryDT(firstsql, "CODDoc1")
        For Each drow As DataRow In dt.Rows
            Dim node As New TreeNode
            Dim str As New StringBuilder
            Dim durl As String = ""
            str.Append(drow.Item("DocName").ToString)
            Dim po As String = ddlPO.SelectedItem.Text.Replace(" ", "^^")
            If drow.Item("Doctype") = "D" Then
                Dim strlink As String = "frmWccTreeDocUpload.aspx?id=" & drow.Item("Doc_Id") & "&siteid=" & ddlsite.SelectedValue & "&pono=" & po & "&siteno=" & ddlsite.SelectedItem.Text
                str.Append("<a href=" + strlink + "><Img ID='Image1' src='../Images/doc.gif' style='border:none';onmouseover='welcome to ebast' /></a>")
                If drow.Item("isuploaded") = "0" Then
                    str.Append("<Img ID='Image1' src='../Images/notok.jpg' style='border:none' />")
                Else
                    If drow.Item("apr") = True Then
                        If drow.Item("cpr") < drow.Item("tapr") Then
                            str.Append("<Img ID='Image1' src='../Images/upload.gif' style='border:none' />")
                            str.Append("<Img ID='Image1' src='../Images/chkyellow.png' style='border:none' />")
                            '>>>delete link
                            durl = "frmSiteDocUploadTree.aspx?id=" & drow.Item("parent_id") & "&del=1"
                            str.Append("<a href=" + durl + "><Img ID='Image1' src='../Images/deldoc.gif' style='border:none' /></a>")
                            '<<<
                            str.Append("<label id='lbl1' runat='server' class='lblTextC'>" + drow.Item("lmdt") + "</label>")
                        Else
                            str.Append("<Img ID='Image1' src='../Images/ok.jpg' style='border:none' />")
                            str.Append("<label id='lbl1' runat='server' class='lblTextC'>" + drow.Item("lmdt") + "</label>")
                        End If
                    Else
                        str.Append("<Img ID='Image1' src='../Images/ok.jpg' style='border:none' />")
                        str.Append("<label id='lbl1' runat='server' class='lblTextC'>" + drow.Item("lmdt") + "</label>")
                    End If
                End If
            Else
                If drow.Item("resval").ToString = 2 Then
                    If drow.Item("cpr") < drow.Item("tapr") Then
                        str.Append("<a><Img ID='Image1' src='../Images/doce.gif' style='border:none'/></a>")
                    Else
                        strlink = "../WCC/" & drow.Item("onlineform").ToString & "?id=" & drow.Item("SW_Id").ToString & "&KeyVal=" & 3
                        str.Append("<a href=" + strlink + "><Img ID='Image1' src='../Images/doce.gif' style='border:none'/></a>")
                    End If
                Else
                    str.Append("<Img ID='Image1' src='../Images/doce.gif' style='border:none'/>")
                End If
                If drow.Item("isuploaded") = "0" Then
                    str.Append("<Img ID='Image1' src='../Images/notok.jpg' style='border:none' />")
                Else
                    If drow.Item("apr") = True Then
                        If drow.Item("cpr") < drow.Item("tapr") Then
                            str.Append("<Img ID='Image1' src='../Images/upload.gif' style='border:none' />")
                            str.Append("<Img ID='Image1' src='../Images/chkyellow.png' style='border:none' />")
                            '>>>delete link
                            durl = "frmSiteDocUploadTree.aspx?id=" & drow.Item("parent_id") & "&del=1"
                            str.Append("<a href=" + durl + "><Img ID='Image1' src='../Images/deldoc.gif' style='border:none' /></a>")
                            '<<<
                            str.Append("<label id='lbl1' runat='server' class='lblTextC'>" + drow.Item("lmdt") + "</label>")
                        Else
                            str.Append("<Img ID='Image1' src='../Images/ok.jpg' style='border:none' />")
                            str.Append("<label id='lbl1' runat='server' class='lblTextC'>" + drow.Item("lmdt") + "</label>")
                        End If
                    Else
                        str.Append("<Img ID='Image1' src='../Images/ok.jpg' style='border:none' />")
                        str.Append("<label id='lbl1' runat='server' class='lblTextC'>" + drow.Item("lmdt") + "</label>")
                    End If
                End If
            End If
            node.Text = str.ToString
            node.Value = drow.Item("Doc_Id") & ""
            node.SelectAction = TreeNodeSelectAction.Select
            If node.Value < 1000 Then node.ShowCheckBox = False
            TreeView1.Nodes.Add(node)
            fillchild(node)
        Next
        TreeView1.ExpandAll()
    End Sub
    Sub fillchild(ByVal tn As TreeNode)
        Dim secondsql As String = ""
        Dim siteandscope() As String = ddlsite.SelectedItem.Text.Split("-")
        Dim siteandpo() As String = ddlsite.SelectedItem.Value.Split("-")
        secondsql = "exec uspWccSiteDocUploadTreeChild '" & siteandpo(0) & "'," & siteandpo(1) & "," & "'" & ddlPO.SelectedItem.Text & "'," & tn.Value
        dt = objUtil.ExeQueryDT(secondsql, "CODDoc2")
        For Each drow As DataRow In dt.Rows
            Dim node As New TreeNode
            Dim str As New StringBuilder
            Dim durl As String = ""
            str.Append(drow.Item("DocName").ToString)
            Dim po As String = ddlPO.SelectedItem.Text.Replace(" ", "^^")
            If drow.Item("Doctype") = "D" Then
                Dim strlink As String = "frmWccTreeDocUpload.aspx?id=" & drow.Item("Doc_Id") & "&siteid=" & ddlsite.SelectedValue & "&pono=" & po & "&siteno=" & ddlsite.SelectedItem.Text
                str.Append("<a href=" + strlink + "><Img ID='Image1' src='../Images/doc.gif' style='border:none';onmouseover='welcome to ebast' /></a>")
                If drow.Item("isuploaded") = "0" Then
                    str.Append("<Img ID='Image1' src='../Images/notok.jpg' style='border:none' />")
                Else
                    If drow.Item("cpr") < drow.Item("tapr") Then
                        str.Append("<Img ID='Image1' src='../Images/upload.gif' style='border:none' />")
                        str.Append("<Img ID='Image1' src='../Images/chkyellow.png' style='border:none' />")
                        '>>>delete link
                        durl = "frmSiteDocUploadTree.aspx?id=" & drow.Item("parent_id") & "&del=1"
                        str.Append("<a href=" + durl + "><Img ID='Image1' src='../Images/deldoc.gif' style='border:none' /></a>")
                        '<<<
                        str.Append("<label id='lbl1' runat='server' class='lblTextC'>" + drow.Item("lmdt") + "</label>")
                    Else
                        str.Append("<Img ID='Image1' src='../Images/ok.jpg' style='border:none' />")
                        str.Append("<label id='lbl1' runat='server' class='lblTextC'>" + drow.Item("lmdt") + "</label>")
                    End If
                End If
            Else
                If drow.Item("resval").ToString = 2 Then
                    If drow.Item("cpr") < drow.Item("tapr") Then
                        str.Append("<a><Img ID='Image1' src='../Images/doce.gif' style='border:none'/></a>")
                    Else
                        strlink = "../WCC/" & drow.Item("onlineform").ToString & "?id=" & drow.Item("SW_Id").ToString & "&KeyVal=" & 3
                        str.Append("<a href=" + strlink + "><Img ID='Image1' src='../Images/doce.gif' style='border:none'/></a>")
                    End If
                Else
                    str.Append("<Img ID='Image1' src='../Images/doce.gif' style='border:none'/>")
                End If
                If drow.Item("isuploaded") = "0" Then
                    str.Append("<Img ID='Image1' src='../Images/notok.jpg' style='border:none' />")
                Else
                    If drow.Item("cpr") < drow.Item("tapr") Then
                        str.Append("<Img ID='Image1' src='../Images/upload.gif' style='border:none' />")
                        str.Append("<Img ID='Image1' src='../Images/chkyellow.png' style='border:none' />")
                        '>>>delete link
                        durl = "frmSiteDocUploadTree.aspx?id=" & drow.Item("parent_id") & "&del=1"
                        str.Append("<a href=" + durl + "><Img ID='Image1' src='../Images/deldoc.gif' style='border:none' /></a>")
                        '<<<
                        str.Append("<label id='lbl1' runat='server' class='lblTextC'>" + drow.Item("lmdt") + "</label>")
                    Else
                        str.Append("<Img ID='Image1' src='../Images/ok.jpg' style='border:none' />")
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
    Protected Sub btndone_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Session("pon") = Nothing
        Response.Redirect("frmsitedocuploadtree.aspx")
    End Sub
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        For i As Integer = 0 To ddlsite.Items.Count - 1
            If UCase(ddlsite.Items(i).Text).Contains(UCase(txtSearch.Text)) = True Then
                ddlsite.SelectedIndex = i '+ 1
                ddlsite_SelectedIndexChanged(Nothing, Nothing)
                Exit For
            End If
        Next
    End Sub

    'Protected Sub btnUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpload.Click
    '    'File and FilePath Save in to WccSiteDoc Table
    '    If FileUpload.HasFile = True Then
    '        Dim flength As Integer = FileUpload.PostedFile.ContentLength
    '        If flength > ConfigurationManager.AppSettings("filesize") Then
    '            Response.Write("<script>alert('File size Exceeds maximum size')</script>")
    '        Else
    '            uploaddocument(0, 1) 'hdnkeyval.Value
    '        End If
    '    Else
    '        Response.Write("<script>alert('please select document to upload')</script>")
    '    End If
    'End Sub

    Protected Sub btnUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpload.Click
        'File and FilePath Save in to WccSiteDoc Table
        If fileUpload.HasFile = True Then
            Dim flength As Integer = fileUpload.PostedFile.ContentLength
            If flength > ConfigurationManager.AppSettings("filesize") Then
                Response.Write("<script>alert('File size Exceeds maximum size')</script>")
            Else
                uploaddocument(0, 1) 'hdnkeyval.Value
            End If
        Else
            Response.Write("<script>alert('please select document to upload')</script>")
        End If
    End Sub

#Region "UploadDocument,DoInsertTrans 20090403, CreatePDF"
    Sub uploaddocument(ByVal vers As Integer, ByVal keyval As Integer)
        Try
            Dim scope() As String = lblsite.Text.Split("-")
            FileNamePath = fileUpload.PostedFile.FileName
            FileNameOnly = System.IO.Path.GetFileName(fileUpload.PostedFile.FileName)
            ReFileName = Format(CDate(DateTime.Now), "ddMMyyyyHHss") & FileNameOnly
            sitename = scope(0)
            ft = ConfigurationManager.AppSettings("WccType") & ddlPO.SelectedItem.Text & "-" & scope(1) & "\"
            path = ConfigurationManager.AppSettings("WCCPath") & sitename & ft
            sitename = scope(0)
            'Dim strResult As String = DOInsertTrans(hdnsiteid.Value, ddldocument.SelectedItem.Value, vers, path)
            Dim DocPath As String = ""
            'If strResult = "0" Or strResult = "1" Then
            DocPath = sitename & ft & Common.TakeFileUpload.ConvertAnyFormatToPDF(fileUpload, path)
            'Else
            'DocPath = sitename & ft & Common.TakeFileUpload.ConvertAnyFormatToPDF(fileUpload, path, path + strResult)
            'End If
            With objetsitedoc
                .SiteID = hdnsiteid.Value + 1
                .DocId = "999" 'ddldocument.SelectedItem.Value
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
            lblStatus.Visible = True
            lblStatus.Text = "Document Uploaded Successfully"

            'Fill Transaction table
            'siteid,docid,ver

            'AuditTrail()

        Catch ex As Exception
            Response.Write("<script>alert('please select Required Document')</script>")
        End Try

    End Sub
#End Region

   
End Class
