Imports BusinessLogic
Imports Common
Imports System.Data
Imports System.Web.UI
Imports Entities
Imports System.Data.SqlClient
Partial Class frmMultiSiteDocSetup
    Inherits System.Web.UI.Page
    Dim dt As DataTable
    Dim objBo As New BOSiteDocs
    Dim objbl As New BODDLs
    Dim strSql As String
    Dim objET As New ETAuditTrail
    Dim objBOAT As New BoAuditTrail
    Dim totperc As Double = 0
    Dim objutil As New DBUtil
    Protected Sub btnCancel_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.ServerClick
        Response.Redirect("~/PO/frmPOSiteList.aspx")
    End Sub
    Sub BindTree()
        FillSection()
        TreeView1.ExpandAll()
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        btnSave.Attributes.Add("onclick", "javascript:return checkIsEmpty();")
        If Page.IsPostBack = False Then
            objbl.fillDDL(ddlWF, "CODSite", True, Constants._DDL_Default_Select)
            If Not Request.QueryString("id") Is Nothing Then
                hdnid.Value = objBo.uspGetSiteId(Request.QueryString("id"))
            End If
            If hdnid.Value = "" Then hdnid.Value = 0
            ddlWF.SelectedValue = hdnid.Value
            ddlWF.Visible = False
            TreeView1.Visible = True
            BindTree()
        End If
        If hdnSites.Value <> "" Then
            Dim siteid As String = hdnSites.Value
            Dim strReply() As String = siteid.Split("^")
            hdnpo.Value = strReply(1).Replace("!~", " ")
            siteid = strReply(0)
            siteid = siteid.Replace("~", ",")
            Dim sid As String = ""
            Dim dtnew As New DataTable
            dtnew = objutil.ExeQueryDT("select pono,site_id,siteno,fldtype,workpkgid from podetails p inner join codsite s on s.site_no=p.siteno where po_id in (" + siteid + ")", "podetails")
            lblSite.Text = ""
            For k As Integer = 0 To dtnew.Rows.Count - 1
                lblSite.Text = lblSite.Text & IIf(lblSite.Text <> "", ", ", "") & dtnew.Rows(k).Item("siteno").ToString & "-" & dtnew.Rows(k).Item("fldtype").ToString & "-" & dtnew.Rows(k).Item("workpkgid").ToString
            Next
            Session("dtnew") = dtnew
        End If
    End Sub
    Private Function SelectChildrenRecursive(ByVal tn As TreeNode, ByVal searchvalue As Integer) As Integer
        If tn.Value = searchvalue Then
            tn.Expand()
            tn.Checked = True
            Return 1
        End If
        If tn.ChildNodes.Count > 0 Then
            For Each tnc As TreeNode In tn.ChildNodes
                Dim a As Integer = SelectChildrenRecursive(tnc, searchvalue)
                If a = 1 Then
                    tnc.Expand()
                    tnc.Checked = True
                    Return 1
                End If
            Next
        End If
    End Function
    Protected Sub TreeView1_TreeNodeCheckChanged(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.TreeNodeEventArgs)
        If Not e Is Nothing And Not e.Node Is Nothing Then
            If e.Node.ChildNodes.Count > 0 Then
                CheckTreeNodeRecursive(e.Node, e.Node.Checked)
            End If
        End If
    End Sub
    Sub FillSection()
        dt = objutil.ExeQueryDT("Select C.Doc_Id,DocName,DocType,IsNull(WFId,0)WFId,Appr_Required from coddoc C Left Outer join CodWFDoc W on W.Doc_Id=C.Doc_Id where Parent_id=0 order by SerialNo", "CODDoc")
        For Each drow As DataRow In dt.Rows
            Dim node As New TreeNode
            Dim str As New StringBuilder
            node.Text = drow.Item("DocName").ToString
            node.Value = drow.Item("Doc_Id") & ""
            node.SelectAction = TreeNodeSelectAction.Select
            Dim Sstr As New StringBuilder
            If drow.Item("WFId") & "" = 0 Then
                If drow.Item("Appr_Required") = 0 Then
                    Sstr.Append("<font style='Color :DarkGreen'>")
                Else
                    node.ShowCheckBox = True
                    Sstr.Append("<font style='Color :OrangeRed'>")
                End If
                Sstr.Append(drow.Item("DocName").ToString & "" & "</font>")
                node.Text = Sstr.ToString
            Else
                Sstr.Append("<font style='Color :Blue'>")
                Sstr.Append(drow.Item("DocName").ToString & "" & "</font>")
                node.Text = Sstr.ToString
            End If
            If drow.Item("WFId") > 0 Then
                node.ShowCheckBox = True
                node.Checked = True
            ElseIf drow.Item("Appr_Required") = 0 Then
                node.ShowCheckBox = True
            End If
            TreeView1.Attributes.Add("onclick", "TreeNodeCheckChanged(event, this)")
            TreeView1.Nodes.Add(node)
            fillchild(node)
        Next
    End Sub
    Sub fillChild(ByVal tn As TreeNode)
        dt = objBo.uspTICODSubSectionDoc(tn.Value, , )
        For Each drow As DataRow In dt.Rows
            Dim node As New TreeNode
            Dim str As New StringBuilder
            node.Text = str.ToString & "  "
            node.Value = drow.Item("Doc_Id").ToString
            node.SelectAction = TreeNodeSelectAction.Select
            If drow.Item("WFId").ToString = 0 Then
                If drow.Item("Appr_Required") = 0 Then
                    str.Append("<font style='Color :DarkGreen'>")
                    node.ShowCheckBox = True
                Else
                    str.Append("<font style='Color :OrangeRed'>")
                    node.ShowCheckBox = True
                End If
                str.Append(drow.Item("DocName").ToString & "" & "</font>")
                node.Text = str.ToString
            Else
                str.Append("<font style='Color :Blue'>")
                str.Append(drow.Item("DocName").ToString & "" & "</font>")
                node.Text = str.ToString
                node.ShowCheckBox = True
            End If
            If drow.Item("DocType").ToString = "O" Then
                If drow.Item("Appr_Required").ToString = "True" And drow.Item("WFId").ToString > 0 Then
                    node.Checked = True
                ElseIf drow.Item("Appr_Required").ToString = "False" Then
                    node.Checked = True
                End If
            End If
            'customer request by default is checked
            'node.Checked = True
            'New Request only BAST, BAUT, BOQ, WCTR and also ATP Doc Online will be defined at first stage -- irvan v
            If drow.Item("Doc_Id").ToString() = ConfigurationManager.AppSettings("ATP") Then
                node.Checked = True
            End If
            tn.ChildNodes.Add(node)
            If drow.Item("cc").ToString > 0 Then fillChild(node)
        Next
    End Sub
    Sub CheckTreeNodeRecursive(ByVal parent As TreeNode, ByVal fCheck As Boolean)
        Dim child As TreeNode
        For Each child In parent.ChildNodes
            If child.ShowCheckBox.HasValue = False Then
                If child.Checked <> fCheck Then
                    child.Checked = fCheck
                End If
                If child.ChildNodes.Count > 0 Then
                    CheckTreeNodeRecursive(child, fCheck)
                End If
            End If
        Next
    End Sub
    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        TreeView1.Enabled = False
        btnSave.Enabled = False
        Dim str As String = ""
        Dim SCount As Integer = 0
        For Each pchild As TreeNode In TreeView1.CheckedNodes
            str = str & IIf(str = "", "", ",") & "'" & pchild.Value & "'" : SCount = SCount + 1
        Next
        If str <> "" Then
            Dim parentdt As DataTable = objutil.ExeQueryDT("select parent_Id from coddoc where doc_id in(" & str.Replace("'", "") & ") and parent_Id not in(" & str.Replace("'", "") & ",0)", "CODDoc")
            If parentdt.Rows.Count > 0 Then
                For pd As Integer = 0 To parentdt.Rows.Count - 1
                    str = str & IIf(str = "", "", ",") & "'" & parentdt.Rows(pd).Item("Parent_Id") & "'" : SCount = SCount + 1
                Next
            End If
            Dim i As Integer = -1
            Dim perc As Double = 0
            perc = Format(100 / SCount, "0.00")
            Dim dtnn As New DataTable
            dtnn = Session("dtnew")
            If dtnn.Rows.Count <= 5 Then
                For j As Integer = 0 To dtnn.Rows.Count - 1
                    Dim version As Integer = 0
                    'bugfix101111
                    'version = objBo.getsiteversion(dtnn.Rows(j).Item("pono").ToString, dtnn.Rows(j).Item("SiteNo").ToString, dtnn.Rows(j).Item("fldtype").ToString)
                    strSql = "select siteversion from podetails where pono='" & dtnn.Rows(j).Item("pono").ToString & "' and siteno='" & dtnn.Rows(j).Item("siteno").ToString & "' and fldtype='" & dtnn.Rows(j).Item("fldtype").ToString & "' and workpkgid=" & dtnn.Rows(j).Item("workpkgid").ToString
                    version = objutil.ExeQueryScalar(strSql)
                    i = objBo.uspSiteDocIU(dtnn.Rows(j).Item("site_id").ToString, Constants.STATUS_ACTIVE, Session("User_Name"), str, perc, 1, version, , hdnpo.Value)
                    objutil.ExeNonQuery("update sitedoc set rstatus=4,isuploaded=1 where siteid=" & dtnn.Rows(j).Item("site_id").ToString & " and version=" & version & " and pono='" & dtnn.Rows(j).Item("pono").ToString & "' and docid=" & ConfigurationManager.AppSettings("WCTRBASTID") & "")
                    AuditTrail(str, dtnn.Rows(j).Item("Site_ID"), dtnn.Rows(j).Item("pono"), dtnn.Rows(j).Item("fldtype"))
                Next
                If i = 0 Then
                    ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('Error while doing the transaction.');", True)
                ElseIf i = 1 Then
                    BindDoc()
                    ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('Checklist created successfully');", True)
                End If
            Else
                ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('Sorry only 5 sites allowed');", True)
            End If
        Else
            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('Please select Documents');", True)
        End If
    End Sub
    Sub BindDoc()
        Dim j As Integer
        grdDoc.Columns(6).Visible = True
        grdDoc.Columns(7).Visible = True
        grdDoc.Columns(8).Visible = True
        Dim dt1 As New DataTable
        dt1 = Session("dtnew")
        'bugfix101111
        'dt = objBo.uspGetSiteDocs(dt1.Rows(0).Item("Site_Id"), dt1.Rows(0).Item("Pono"), dt1.Rows(0).Item("fldtype"))
        strSql = "Exec uspGetSiteDocs " & dt1.Rows(0).Item("Site_Id").ToString & ",'" & dt1.Rows(0).Item("Pono").ToString & "','" & dt1.Rows(0).Item("fldtype").ToString & "'," & dt1.Rows(0).Item("workpkgid").ToString
        dt = objutil.ExeQueryDT(strSql, "GetSiteDocs")
        grdDoc.DataSource = dt
        grdDoc.DataBind()
        If grdDoc.Rows.Count > 0 Then
            btnEdit.Visible = True
            If totperc <> 100 Then
                grdDoc.Rows(grdDoc.Rows.Count - 1).Cells(2).Text = Val(grdDoc.Rows(grdDoc.Rows.Count - 1).Cells(2).Text) + (100 - totperc)
                Dim txtperc As HtmlInputText
                txtperc = CType(grdDoc.Rows(grdDoc.Rows.Count - 1).Cells.Item(3).FindControl("textPerc"), HtmlInputText)
                txtperc.Value = Format(Val(txtperc.Value + (100 - totperc)), "0.00")
                grdDoc.Rows(grdDoc.Rows.Count - 1).Cells(2).Text = txtperc.Value
            End If
        End If
        For j = 0 To grdDoc.Rows.Count - 1
            Dim chkwf As New CheckBox
            chkwf = CType(grdDoc.Rows(j).Cells(3).FindControl("chkwf"), CheckBox)
            If grdDoc.Rows(j).Cells(7).Text > 0 Then
                chkwf.Checked = True
            Else
                chkwf.Checked = False
            End If
        Next
        grdDoc.Columns(6).Visible = False
        grdDoc.Columns(7).Visible = False
        grdDoc.Columns(8).Visible = False
    End Sub
    Protected Sub btnEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Dim j As Double = 0
        Dim str As New StringBuilder
        Dim str1 As String = ""
        Dim dtnn As New DataTable
        dtnn = Session("dtnew")
        For K As Integer = 0 To dtnn.Rows.Count - 1
            Dim version As Integer = 0
            version = objBo.getsiteversion(dtnn.Rows(K).Item("pono").ToString, dtnn.Rows(K).Item("SiteNo").ToString, dtnn.Rows(K).Item("fldtype").ToString, dtnn.Rows(K).Item("workpkgid"))
            Dim site_id As Integer = dtnn.Rows(K).Item("site_id").ToString
            For i As Integer = 0 To grdDoc.Rows.Count - 1
                grdDoc.Columns(8).Visible = True
                Dim doc_id As Integer = grdDoc.Rows(i).Cells(8).Text.ToString
                grdDoc.Columns(8).Visible = False
                Dim aa As HtmlInputText
                aa = CType(grdDoc.Rows(i).Cells(3).FindControl("textperc"), HtmlInputText)
                j = j + Format(Val(aa.Value), "0.00")
                Dim chk As CheckBox
                chk = CType(grdDoc.Rows(i).Cells(4).FindControl("chkwf"), CheckBox)
                If chk.Checked = True And grdDoc.Rows(i).Cells(6).Text = False And grdDoc.Rows(i).Cells(7).Text = 0 Then
                    Dim ddl As DropDownList
                    ddl = CType(grdDoc.Rows(i).Cells(5).FindControl("ddlwf"), DropDownList)
                    If ddl.SelectedItem.Text <> "No Values" Then
                        str1 = " Update Sitedoc Set Percentage = " & aa.Value & ",WF_Id=" & ddl.SelectedValue & " Where siteid=" & site_id & " and docid =" & doc_id & " and version = " & version
                    Else
                        ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('Please select the process flow');", True)
                        Exit Sub
                    End If
                ElseIf chk.Checked = False And grdDoc.Rows(i).Cells(6).Text = True Then
                    str1 = " Update SiteDoc Set Percentage = " & aa.Value & ",WF_Id = 0 where siteid=" & site_id & " and docid =" & doc_id & " and version = " & version
                Else
                    str1 = " Update SiteDoc Set Percentage = " & aa.Value & " where siteid=" & site_id & " and docid =" & doc_id & " and version = " & version
                End If
                str.Append(str1)
            Next
        Next
        If j > 99 Then
            j = objBo.uspUpdateDocPerc(str.ToString)
        Else
            Dim stral As String = "Total must be equal to 100, current is " + j.ToString
            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('" & stral & "');", True)
        End If
        If j = 1 Then
            Session("dtnew") = Nothing
            Response.Redirect("frmPOSiteList.aspx?type=N")
        End If
    End Sub
    Sub AuditTrail(ByVal str As String, ByVal siteid As Integer, ByVal pono As String, ByVal scope As String)
        objET.PoNo = pono
        objET.SiteId = siteid
        objET.DocId = str
        objET.Roleid = Session("Role_Id")
        objET.Userid = Session("User_Id")
        objET.Task = Constants.AuditTrailCreated_Task
        objET.Status = Constants.AuditTrail_Status
        objET.fldtype = scope
        objBOAT.uspAuditTrailI(objET)
    End Sub
    Protected Sub grdDoc_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdDoc.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            totperc = totperc + Val(e.Row.Cells(2).Text)
        End If
    End Sub
    Public Sub getwf(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim chk As CheckBox = CType(sender, CheckBox)
        Dim drow As GridViewRow = CType(chk.NamingContainer, GridViewRow)
        Dim i As Integer
        i = drow.RowIndex
        Dim chkwfn As CheckBox
        chkwfn = grdDoc.Rows(i).Cells(4).FindControl("chkwf")
        If chkwfn.Checked = True Then
            Dim ddlwf As DropDownList
            ddlwf = grdDoc.Rows(i).Cells(5).FindControl("ddlwf")
            ddlwf.Visible = True
            objbl.fillDDL(ddlwf, "TWorkFlow", False, Constants._DDL_Default_Select)
        End If
    End Sub
End Class