Imports BusinessLogic
Imports Common
Imports System.Data
Imports System.Web.UI
Imports System.IO
Partial Class frmSiteDocUploadTreeSubcon
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
    Dim poid As String
    Dim pono As String
    Dim scope As String
    Dim siteid As String
    Dim siteno As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Page_size") Is Nothing Then Response.Redirect("~/SessionTimeout.aspx")
        If Page.IsPostBack = False Then
            '>>>file delete operation
            If Request.QueryString("id") <> "" Then
                If Request.QueryString("del") = "1" Then
                    'validate delete for BAUT
                    pid = objUtil.ExeQueryScalar("select parent_id from coddoc where doc_id =(select docid from sitedoc where sw_id=" & Request.QueryString("id") & ")")
                    'If pid = ConfigurationManager.AppSettings("BAUTID") Then
                    bautc = objUtil.ExeQueryScalarString("select isuploaded from sitedoc where docid=" & pid & "  and siteid=(select siteid from sitedoc where sw_id=" & Request.QueryString("id") & " ) and version=(select version from sitedoc where sw_id=" & Request.QueryString("id") & ")")
                    If bautc = "True" Then
                        ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "Script", "alert('Since BAUT Document already uploaded you can not delete this Document.');", True)
                        Exit Sub
                    End If
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
                    objUtil.ExeQueryScalar("delete from wftransaction where docid=(select docid from sitedoc where sw_id=" & Request.QueryString("id") & ") and site_id=(select siteid from sitedoc where sw_id=" & Request.QueryString("id") & ")")
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
    Sub BindTree()
        Session("pon") = pono
        Session("stno") = siteno
        Dim firstsql As String = ""
        firstsql = "exec uspSiteDocUploadTree '" & siteid & "'," & poid & ",'" & pono & "'," & ConfigurationManager.AppSettings("WCTRBASTID") & ""
        dt = objUtil.ExeQueryDT(firstsql, "CODDoc1")
        If dt.Rows.Count > 0 Then
            For Each drow As DataRow In dt.Rows
                Dim node As New TreeNode
                Dim str As New StringBuilder
                Dim durl As String = ""
                str.Append(drow.Item("DocName").ToString)
                If drow.Item("Doctype") = "D" Then
                    Dim strlink As String = "frmTreeDocUploadSubcon.aspx?id=" & drow.Item("Doc_Id") & "&siteid=" & siteid & "&pono=" & pono & "&siteno=" & siteno
                    str.Append("<a href=" + strlink + "><Img ID='Image1' src='../Images/doc.gif' style='border:none';onmouseover='welcome to ebast' /></a>")
                    If drow.Item("isuploaded") = "0" Then
                        str.Append("<Img ID='Image1' src='../Images/notok.jpg' style='border:none' />")
                    Else
                        If drow.Item("apr") = True Then
                            If drow.Item("cpr") < drow.Item("tapr") Then
                                str.Append("<Img ID='Image1' src='../Images/upload.gif' style='border:none' />")
                                str.Append("<Img ID='Image1' src='../Images/chkyellow.png' style='border:none' />")
                                '>>>delete link
                                'durl = "frmSiteDocUploadTreeSubcon.aspx?id=" & drow.Item("sw_id") & "&del=1"
                                'str.Append("<a href=" + durl + "><Img ID='Image1' src='../Images/deldoc.gif' style='border:none' /></a>")
                                '<<<
                                str.Append("<label id='lbl1' runat='server' class='lblTextC'>" + drow.Item("lmdt") + "</label>")
                            Else
                                str.Append("<Img ID='Image1' src='../Images/ok.jpg' style='border:none' />")
                                'added by staish on 10th october
                                '>>>delete link
                                'durl = "frmSiteDocUploadTreeSubcon.aspx?id=" & drow.Item("sw_id") & "&del=1"
                                'str.Append("<a href=" + durl + "><Img ID='Image1' src='../Images/deldoc.gif' style='border:none' /></a>")
                                '<<<
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
                            str.Append("<a><Img ID='Image1' src='../Images/doce.gif' style='border:none'/></a>") ' & drow.Item("cpr") & drow.Item("tapr"))
                        Else
                            strlink = "../BAUT/" & drow.Item("onlineform").ToString & "?id=" & drow.Item("SW_Id").ToString
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
                                'durl = "frmSiteDocUploadTreeSubcon.aspx?id=" & drow.Item("sw_id") & "&del=1"
                                'str.Append("<a href=" + durl + "><Img ID='Image1' src='../Images/deldoc.gif' style='border:none' /></a>")
                                '<<<
                                str.Append("<label id='lbl1' runat='server' class='lblTextC'>" + drow.Item("lmdt") + "</label>")
                            Else
                                'str.Append("<Img ID='Image1' src='../Images/ok.jpg' style='border:none' />")
                                'str.Append("<label id='lbl1' runat='server' class='lblTextC'>" + drow.Item("lmdt") + "</label>")
                            End If
                        Else
                            str.Append("<Img ID='Image1' src='../Images/ok.jpg' style='border:none' />")
                            '*** added by satish 10thocober
                            '>>>delete link
                            'durl = "frmSiteDocUploadTreeSubcon.aspx?id=" & drow.Item("sw_id") & "&del=1"
                            'str.Append("<a href=" + durl + "><Img ID='Image1' src='../Images/deldoc.gif' style='border:none' /></a>")
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
                fillchild(node)
            Next
            TreeView1.ExpandAll()
        Else
            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "Script", "alert('Document Checklist Not Found.. It can be BAST Document is not checked or Double ePM WPID..');", True)
        End If
    End Sub
    Sub fillchild(ByVal tn As TreeNode)
        Dim secondsql As String = ""
        secondsql = "exec uspSiteDocUploadTreeChild '" & siteid & "'," & poid & "," & "'" & pono & "'," & tn.Value & "," & ConfigurationManager.AppSettings("WCTRBASTID") & ""
        dt = objUtil.ExeQueryDT(secondsql, "CODDoc2")
        For Each drow As DataRow In dt.Rows
            Dim node As New TreeNode
            Dim str As New StringBuilder
            Dim durl As String = ""
            str.Append(drow.Item("DocName").ToString)
            If drow.Item("Doctype") = "D" Then
                Dim strlink As String = "frmTreeDocUploadSubcon.aspx?id=" & drow.Item("Doc_Id") & "&siteid=" & siteid & "-" & poid & "-" & txtSearch.Text & "&pono=" & pono.Replace(" ", "^^") & "&siteno=" & siteno & "-" & scope
                If drow.Item("isuploaded") = "0" Then
                    str.Append("<a href=" + strlink + "><Img ID='Image1' src='../Images/doc.gif' style='border:none';onmouseover='welcome to ebast' /></a>")
                    str.Append("<Img ID='Image1' src='../Images/notok.jpg' style='border:none' />")
                Else
                    str.Append("<Img ID='Image1' src='../Images/doc.gif' style='border:none';onmouseover='welcome to ebast' />")
                    If drow.Item("cpr") < drow.Item("tapr") Then
                        str.Append("<Img ID='Image1' src='../Images/upload.gif' style='border:none' />")
                        str.Append("<Img ID='Image1' src='../Images/chkyellow.png' style='border:none' />")
                        '>>>delete link
                        'durl = "frmSiteDocUploadTreeSubcon.aspx?id=" & drow.Item("sw_id") & "&del=1"
                        'str.Append("<a href=" + durl + "><Img ID='Image1' src='../Images/deldoc.gif' style='border:none' /></a>")
                        '<<<
                        str.Append("<label id='lbl1' runat='server' class='lblTextC'>" + drow.Item("lmdt") + "</label>")
                    Else
                        str.Append("<Img ID='Image1' src='../Images/ok.jpg' style='border:none' />")
                        'added by satish 10 thoctober
                        '>>>delete link
                        'durl = "frmSiteDocUploadTreeSubcon.aspx?id=" & drow.Item("sw_id") & "&del=1"
                        'str.Append("<a href=" + durl + "><Img ID='Image1' src='../Images/deldoc.gif' style='border:none' /></a>")
                        '<<<
                        str.Append("<label id='lbl1' runat='server' class='lblTextC'>" + drow.Item("lmdt") + "</label>")
                    End If
                End If
            Else
                If drow.Item("resval").ToString = 2 Then
                    If drow.Item("cpr") < drow.Item("tapr") Then
                        str.Append("<a><Img ID='Image1' src='../Images/doce.gif' style='border:none'/></a>")
                    Else
                        str.Append("<Img ID='Image1' src='../Images/doce.gif' style='border:none'/>")
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
                        'durl = "frmSiteDocUploadTreeSubcon.aspx?id=" & drow.Item("sw_id") & "&del=1"
                        'str.Append("<a href=" + durl + "><Img ID='Image1' src='../Images/deldoc.gif' style='border:none' /></a>")
                        ''<<<
                        str.Append("<label id='lbl1' runat='server' class='lblTextC'>" + drow.Item("lmdt") + "</label>")
                    Else
                        str.Append("<Img ID='Image1' src='../Images/ok.jpg' style='border:none' />")
                        'added by satish()
                        '>>>delete link
                        'durl = "frmSiteDocUploadTreeSubcon.aspx?id=" & drow.Item("sw_id") & "&del=1"
                        'str.Append("<a href=" + durl + "><Img ID='Image1' src='../Images/deldoc.gif' style='border:none' /></a>")
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
    Protected Sub btndone_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Session("pon") = Nothing
        Response.Redirect("frmSiteDocUploadTreeSubcon.aspx")
    End Sub
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            poid = objUtil.ExeQueryScalarString("select po_id from podetails where workpkgid='" & txtSearch.Text & "'")
            pono = objUtil.ExeQueryScalarString("select custpono from epmdata where workpackageid='" & txtSearch.Text & "'")
            scope = objUtil.ExeQueryScalarString("select fldtype from podetails where workpkgid='" & txtSearch.Text & "'")
            siteid = objUtil.ExeQueryScalarString("select site_id from codsite where site_no in (select siteno from podetails where workpkgid='" & txtSearch.Text & "')")
            siteno = objUtil.ExeQueryScalarString("select siteno from podetails where workpkgid='" & txtSearch.Text & "'")
            'bugfix100624
            If poid <> "" And pono <> "" And scope <> "" And siteid <> "" And siteno <> "" Then
                TreeView1.Nodes.Clear()
                BindTree()
                TreeView1.Visible = True
            End If
            If poid = "" Then
                ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "Script", "alert('PO ID not found!! Document checklist not been created!!, Please contact NSN Regional office for support.');", True)
            ElseIf pono = "" Then
                ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "Script", "alert('PO No not found!! Document checklist not been created!!, Please contact NSN Regional office for support.');", True)
            ElseIf scope = "" Then
                ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "Script", "alert('Scope of Work not found!! Document checklist not been created!!, Please contact NSN Regional office for support.');", True)
            ElseIf siteid = "" Then
                ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "Script", "alert('Site ID not found!! Document checklist not been created!!, Please contact NSN Regional office for support.');", True)
            ElseIf siteno = "" Then
                ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "Script", "alert('Site No not found!! Document checklist not been created!!, Please contact NSN Regional office for support.');", True)
            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "Script", "alert('Document checklist not been created!!, Please contact administrator.');", True)
        End Try
    End Sub
End Class