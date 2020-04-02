Imports BusinessLogic
Imports Common
Imports System.Data
Imports System.Web.UI
Imports Entities
Imports Common_NSNFramework
Partial Class frmsiteDocSetup
    Inherits System.Web.UI.Page
    Dim dt As DataTable
    Dim objBo As New BOSiteDocs
    Dim objbl As New BODDLs
    Dim strSql As String
    Dim objET As New ETAuditTrail
    Dim objBOAT As New BoAuditTrail
    Dim totperc As Double = 0
    Public dtwf As New DataTable
    Dim objutil As New DBUtil
    Dim dtdoc As New DataTable
    Dim dtdocview As New DataView
    Dim dcount As String
    Dim strresult As String
    Dim jk As Integer
    Dim kk As Integer
    Dim strkk As String
    Dim tt As Integer
    Dim dcontroller As New DocController
#Region "NSNFramework"
    Dim dbutils_nsn As New DBUtils_NSN
#End Region
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        If Page.IsPostBack = False Then
            objbl.fillDDL(ddlWF, "CODSite", True, Constants._DDL_Default_Select)
            hdnid.Value = objBo.uspGetSiteId(Request.QueryString("id"))
            Try
                hdnScope.Value = objutil.ExeQueryScalarString("Select Scope from PODetails where PO_Id=" & Request.QueryString("sno") & " and workpkgid=" & Request.QueryString("wpid"))
                'bugfix100525
                hdnScope.Value = objutil.ExeQueryScalar("select scope_id from codscope where alias=(Select Top 1 alias from CodScope where Scope = '" & hdnScope.Value.Replace("'", "''") & "' and Rstatus = 2)")
                If hdnid.Value = "" Then hdnid.Value = 0
                ddlWF.SelectedValue = hdnid.Value
                lblSite.Text = ddlWF.SelectedItem.Text & "-" & Request.QueryString("SC").ToString
                Dim sqlStr As String = "Select IsNull(REPLACE(CONVERT(VARCHAR(9), taskcompleted, 6), ' ', '/'),'') SiteIntegration from dashboardmilestone where  workpackageid in ( select workpkgid from podetails where siteno= '" & ddlWF.SelectedItem.Text & "' and po_id='" & Request.QueryString("sno") & "')"
                lblIntDate.InnerText = objutil.ExeQueryScalarString(sqlStr)
                ddlWF.Visible = False
                TreeView1.Visible = True
                BindTree()
                checkTree()
                BindDoc()
            Catch
                ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('Warning!! Scope is empty..');", True)
            End Try
        End If
    End Sub

    Protected Sub btnCancel_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.ServerClick
        Response.Redirect("~/PO/frmPOSiteList.aspx")
    End Sub

    Sub BindTree()
        FillSection()
        TreeView1.ExpandAll()
    End Sub

    Sub ClearNodes()
        For Each aa As TreeNode In TreeView1.Nodes
            If aa.ChildNodes.Count > 0 Then
                aa.ExpandAll()
                aa.Checked = False
                For Each bb As TreeNode In aa.ChildNodes
                    bb.ExpandAll()
                    bb.Checked = False
                    If bb.ChildNodes.Count > 0 Then
                        For Each cc As TreeNode In bb.ChildNodes
                            cc.ExpandAll()
                            cc.Checked = False
                            If cc.ChildNodes.Count > 0 Then
                                subNodeclear(cc, cc.Value)
                            Else
                                cc.Checked = False
                            End If
                        Next
                    Else
                        bb.Checked = False
                    End If
                Next
            Else
                aa.Checked = False
            End If
            aa.Checked = False
        Next
    End Sub

    Sub subNodeclear(ByVal tn As TreeNode, ByVal tnVal As Integer)
        For Each a1 As TreeNode In tn.ChildNodes
            a1.ExpandAll()
            If a1.ChildNodes.Count > 0 Then
                subNodeclear(a1, a1.Value)
            End If
            a1.Checked = False
        Next
    End Sub

    Sub checkTree()
        'dt = objBo.uspSiteDocList(ddlWF.SelectedValue, Request.QueryString("PNo"), "", Request.QueryString("SC"))
        dt = objutil.ExeQueryDT("Exec uspSiteDocList '" & ddlWF.SelectedValue & "','','" & Request.QueryString("PNo") & "','" & Request.QueryString("SC") & "'," & Request.QueryString("wpid"), "SiteDocList")
        If dt.Rows.Count > 0 Then
            Session("dtdoc") = dt
        Else
            Session("dtdoc") = Nothing
        End If
        TreeView1.ExpandAll()
        If dt.Rows.Count > 0 Then ClearNodes()
        If dt.Rows.Count > 0 Then
            For j As Integer = 0 To dt.Rows.Count - 1
                For Each Pnode As TreeNode In TreeView1.Nodes
                    If Pnode.Value = dt.Rows(j).Item("Doc_Id") Then
                        Pnode.Expand()
                        Pnode.Checked = True
                        If Pnode.Value = dcontroller.GetDocIDByDocName("CR Online") Then
                            If dcontroller.IsInDocCRCOTransaction("CR Online", Request.QueryString("wpid")) = True Then
                                Pnode.ShowCheckBox = False
                            End If
                        ElseIf Pnode.Value = dcontroller.GetDocIDByDocName("CO Online") Then
                            If dcontroller.IsInDocCRCOTransaction("CO Online", Request.QueryString("wpid")) = True Then
                                Pnode.ShowCheckBox = False
                            End If
                        End If
                        Exit For
                    End If
                    If Pnode.ChildNodes.Count > 0 Then
                        If Pnode.Value = dcontroller.GetDocIDByDocName("CR Online") Then
                            If dcontroller.IsInDocCRCOTransaction("CR Online", Request.QueryString("wpid")) = True Then
                                Pnode.ShowCheckBox = False
                            End If
                        ElseIf Pnode.Value = dcontroller.GetDocIDByDocName("CO Online") Then
                            If dcontroller.IsInDocCRCOTransaction("CO Online", Request.QueryString("wpid")) = True Then
                                Pnode.ShowCheckBox = False
                            End If
                        End If
                        For Each cNode As TreeNode In Pnode.ChildNodes
                            If cNode.ChildNodes.Count > 0 Then
                                Dim a As Integer = SelectChildrenRecursive(cNode, dt.Rows(j).Item(0))
                                If a = 1 Then
                                    Exit For
                                End If
                            Else
                                If cNode.Value = dt.Rows(j).Item("Doc_Id") Then
                                    cNode.Expand()
                                    cNode.Checked = True
                                    If cNode.Value = dcontroller.GetDocIDByDocName("CR Online") Then
                                        If dcontroller.IsInDocCRCOTransaction("CR Online", Request.QueryString("wpid")) = True Then
                                            cNode.ShowCheckBox = False
                                        End If
                                    ElseIf cNode.Value = dcontroller.GetDocIDByDocName("CO Online") Then
                                        If dcontroller.IsInDocCRCOTransaction("CO Online", Request.QueryString("wpid")) = True Then
                                            cNode.ShowCheckBox = False
                                        End If
                                    End If
                                    Exit For
                                End If
                            End If
                        Next
                    End If
                Next
            Next
        End If
    End Sub
    Private Function SelectChildrenRecursiveNew(ByVal tn As TreeNode, ByVal searchvalue As Integer) As Integer
        If tn.Value = searchvalue Then
            Return 1
        Else
            Return 0
        End If
    End Function
    Private Function SelectChildrenRecursive(ByVal tn As TreeNode, ByVal searchvalue As Integer) As Integer
        If SelectChildrenRecursiveNew(tn, searchvalue) = 1 Then
            tn.Expand()
            tn.Checked = True
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
        Return 0
    End Function
    Sub FillSection()
        dt = objutil.ExeQueryDT("Select C.Doc_Id, DocName, DocType, IsNull(WFId,0)WFId, Appr_Required from coddoc C Left Outer join CodWFDoc W on W.Doc_Id=C.Doc_Id where Parent_id=0 and c.rstatus = 2 order by DocName asc", "CODDoc")
        For Each drow As DataRow In dt.Rows
            Dim node As New TreeNode
            Dim str As New StringBuilder
            node.Text = drow.Item("DocName").ToString
            node.Value = drow.Item("Doc_Id") & ""
            node.SelectAction = TreeNodeSelectAction.Select

            Dim isCRCOOnline As Boolean = False

            'If Integer.Parse(node.Value) = dcontroller.GetDocIDByDocName("CR Online") Then
            '    If dcontroller.IsInDocCRCOTransaction("CR Online", Request.QueryString("wpid")) = True Then
            '        node.ShowCheckBox = False
            '        isCRCOOnline = True
            '    End If
            'ElseIf Integer.Parse(node.Value) = dcontroller.GetDocIDByDocName("CR Online") Then
            '    If dcontroller.IsInDocCRCOTransaction("CO Online", Request.QueryString("wpid")) = True Then
            '        node.ShowCheckBox = False
            '        isCRCOOnline = True
            '    End If
            'End If

            Dim Sstr As New StringBuilder
            If isCRCOOnline = False Then
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
            End If

           
            TreeView1.Attributes.Add("onclick", "TreeNodeCheckChanged(event, this)")
            TreeView1.Nodes.Add(node)
            fillchild(node)
        Next
    End Sub
    Sub fillchild(ByVal tn As TreeNode)
        dt = objBo.uspTICODSubSectionDoc(tn.Value, , hdnScope.Value)
        For Each drow As DataRow In dt.Rows
            Dim node As New TreeNode
            Dim str As New StringBuilder
            node.Text = str.ToString & "  "
            node.Value = drow.Item("Doc_Id").ToString
            node.SelectAction = TreeNodeSelectAction.Select
            Dim isCRCOOnline As Boolean = False

            'If Integer.Parse(node.Value) = dcontroller.GetDocIDByDocName("CR Online") Then
            '    If dcontroller.IsInDocCRCOTransaction("CR Online", Request.QueryString("wpid")) = True Then
            '        node.Selected = False
            '        isCRCOOnline = True
            '    End If
            'ElseIf Integer.Parse(node.Value) = dcontroller.GetDocIDByDocName("CR Online") Then
            '    If dcontroller.IsInDocCRCOTransaction("CO Online", Request.QueryString("wpid")) = True Then
            '        node.Selected = False
            '        isCRCOOnline = True
            '    End If
            'End If

            If isCRCOOnline = False Then
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
                'If drow.Item("DocType").ToString = "O" Then
                '    If drow.Item("Appr_Required").ToString = "True" And drow.Item("WFId").ToString > 0 Then
                '        node.Checked = True
                '    ElseIf drow.Item("Appr_Required").ToString = "False" Then
                '        node.Checked = True
                '    End If
                'End If
                'customer request by default is checked
                'node.Checked = True
                'New Request only BAST, BAUT, BOQ, WCTR and also ATP Doc Online will be defined at first stage -- irvan v
                'If drow.Item("Doc_Id").ToString() = ConfigurationManager.AppSettings("ATP") Then
                '    node.Checked = True
                'End If

                If tn.Value = ConfigurationManager.AppSettings("QCID") Then
                    node.Checked = True
                End If
            End If

            tn.ChildNodes.Add(node)
            If drow.Item("cc").ToString > 0 Then fillchild(node)
        Next
    End Sub
    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        btnSave.Visible = False
        btnCancel.Visible = False
        Dim str As String = ""
        Dim SCount As Integer = 0
        Dim strPO As String = Request.QueryString("PNo")
        If Session("dtdoc") Is Nothing Then
            'bugfix100812 checking for bast which is must be selected
            For Each pchild As TreeNode In TreeView1.Nodes
                If pchild.Value.ToString = "1032" Then
                    If pchild.Checked = False Then
                        ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('BAST document must be selected!');", True)
                        Exit Sub
                    End If
                End If
            Next
            For Each pchild As TreeNode In TreeView1.CheckedNodes
                str = str & IIf(str = "", "", ",") & "'" & pchild.Value & "'" : SCount = SCount + 1
            Next
        Else 'means addition doc save
            dtdoc = Session("dtdoc")
            dtdocview = dtdoc.DefaultView
            dtdocview.Sort = "Doc_id"
            'bugfix100812 checking for bast which is must be selected
            For Each pchild As TreeNode In TreeView1.Nodes
                If pchild.Value.ToString = "1032" Then
                    If pchild.Checked = False Then
                        ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('BAST document must be selected!');", True)
                        Exit Sub
                    End If
                End If
            Next
            'first validate whether newly added documents parent docs uploaded or not
            'check docs
            For Each pchild As TreeNode In TreeView1.CheckedNodes
                If dtdocview.FindRows(pchild.Value).Length = 0 Then
                    'bugfix100623
                    'Dim Vers As Integer = objBo.getsiteversion(Request.QueryString("PNo"), ddlWF.SelectedItem.Text, Request.QueryString("SC"))
                    strSql = "select siteversion from podetails where pono='" & Request.QueryString("PNo") & "' and siteno='" & ddlWF.SelectedItem.Text & "' and FLDType='" & Request.QueryString("SC") & "' and workpkgid=" & Request.QueryString("wpid")
                    Dim Vers As Integer = objutil.ExeQueryScalar(strSql)
                    dcount = objutil.ExeQueryScalar("select isuploaded from sitedoc where docid =(select parent_Id from coddoc where doc_id=" & pchild.Value & ") and siteid=" & ddlWF.SelectedValue & " and version=" & Vers & " and pono='" & strPO & "'")
                    If dcount = True Then
                        strresult = strresult & IIf(strresult = "", pchild.Value, strresult & "," & pchild.Value)
                    End If
                    str = str & IIf(str = "", "", ",") & "'" & pchild.Value & "'" : SCount = SCount + 1
                Else
                    SCount = SCount + 1
                End If
            Next
        End If
        If Session("dtdoc") Is Nothing Then
            If str = "" Then
                ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('Please select Documents');", True)
                Exit Sub
            End If
        End If
        'bugfix101013 sysadmin allows to add new document checklist
        If Session("Role_Id") <> "1" Then
            If strresult <> "" Then
                ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('Cannot add these documents since their parent documents already generated');", True)
                Exit Sub
            End If
        End If
        Dim parentdt As New DataTable
        If str <> "" Then
            parentdt = objutil.ExeQueryDT("select parent_Id from coddoc where doc_id in(" & str.Replace("'", "") & ") and parent_Id not in(" & str.Replace("'", "") & ",0)", "CODDoc")
        End If
        If parentdt.Rows.Count > 0 Then
            If Session("dtdoc") Is Nothing Then
                For pd As Integer = 0 To parentdt.Rows.Count - 1
                    str = str & IIf(str = "", "", ",") & "'" & parentdt.Rows(pd).Item("Parent_Id") & "'" : SCount = SCount + 1
                Next
            Else 'means addition doc save
                For pd As Integer = 0 To parentdt.Rows.Count - 1
                    If dtdocview.FindRows(parentdt.Rows(pd).Item("Parent_Id")).Length = 0 Then
                        str = str & IIf(str = "", "", ",") & "'" & parentdt.Rows(pd).Item("Parent_Id") & "'" : SCount = SCount + 1
                    End If
                Next
            End If
        End If
        'checking any unchecking or not
        If Not Session("dtdoc") Is Nothing Then
            dtdoc = Session("dtdoc")
            For jk = 0 To dtdoc.Rows.Count - 1
                For Each pchild As TreeNode In TreeView1.CheckedNodes
                    If pchild.Value = dtdoc.Rows(jk).Item(0).ToString Then
                        kk = 0
                        Exit For
                    Else
                        kk = 1
                    End If
                Next
                If kk = 1 Then
                    'bugfix100623
                    'Dim Vers As Integer = objBo.getsiteversion(Request.QueryString("PNo"), ddlWF.SelectedItem.Text, Request.QueryString("SC"))
                    strSql = "select siteversion from podetails where pono='" & Request.QueryString("PNo") & "' and siteno='" & ddlWF.SelectedItem.Text & "' and FLDType='" & Request.QueryString("SC") & "' and workpkgid=" & Request.QueryString("wpid")
                    Dim Vers As Integer = objutil.ExeQueryScalar(strSql)
                    Dim crdocid As Integer = New DocController().GetDocIDByDocName("CR Online")
                    Dim codocid As Integer = New DocController().GetDocIDByDocName("CO Online")
                    If Integer.Parse(dtdoc.Rows(jk).Item(0).ToString()) = crdocid Then
                        Dim isHaveTransaction As Boolean = False
                        If isHaveTransaction = True Then

                        Else

                        End If
                    ElseIf Integer.Parse(dtdoc.Rows(jk).Item(0).ToString()) = codocid Then

                    Else
                        objutil.ExeUpdate("delete from sitedoc where docid=" & dtdoc.Rows(jk).Item(0).ToString & " and siteid=" & ddlWF.SelectedValue & " and version=" & Vers & " and pono='" & strPO & "'")
                        objutil.ExeUpdate("delete from wftransaction where docid=" & dtdoc.Rows(jk).Item(0).ToString & " and site_id=" & ddlWF.SelectedValue & " and siteversion=" & Vers)
                        kk = 0
                    End If
                End If
            Next
        End If
        Dim i As Integer = -1
        Dim perc As Double = 0
        perc = Format(100 / SCount, "0.00")
        Dim version As Integer = 0
        'bugfix100623
        'version = objBo.getsiteversion(Request.QueryString("PNo"), ddlWF.SelectedItem.Text, Request.QueryString("SC"))
        strSql = "select siteversion from podetails where pono='" & Request.QueryString("PNo") & "' and siteno='" & ddlWF.SelectedItem.Text & "' and FLDType='" & Request.QueryString("SC") & "' and workpkgid=" & Request.QueryString("wpid")
        version = objutil.ExeQueryScalar(strSql)
        If str <> "" Then
            i = objBo.uspSiteDocIU(ddlWF.SelectedValue, Constants.STATUS_ACTIVE, CommonSite.UserId, str, perc, 1, version, , strPO)
        Else
            i = 99 'means only deletion of docs..no new docs added
        End If
        'means additional document just update the percentage
        If Not Session("dtdoc") Is Nothing Then
            objutil.ExeNonQuery("update sitedoc set percentage=" & perc & " where siteid=" & ddlWF.SelectedValue & " and version=" & version & " and pono='" & strPO & "'")
        End If
        If i = 0 Then
            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('Error while doing the transaction.');", True)
        ElseIf i = 1 Then
            AuditTrail(str)
            'bugfix110122 why need to update the wcrt rstatus and isuploaded?
            'objutil.ExeNonQuery("update sitedoc set rstatus=4, isuploaded=1 where siteid=" & ddlWF.SelectedValue & " and version=" & version & " and pono='" & strPO & "' and docid=" & ConfigurationManager.AppSettings("WCTRBASTID") & "")
        ElseIf i = 99 Then
            'bugfix110122 why need to update the wcrt rstatus and isuploaded?
            'objutil.ExeNonQuery("update sitedoc set rstatus=4, isuploaded=1 where siteid=" & ddlWF.SelectedValue & " and version=" & version & " and pono='" & strPO & "' and docid=" & ConfigurationManager.AppSettings("WCTRBASTID") & "")
        End If
        BindDoc()
        loadingdiv.Style("display") = "none"
    End Sub
    Sub BindDoc()
        Dim j As Integer
        grdDoc.Columns(4).Visible = True
        grdDoc.Columns(6).Visible = True
        grdDoc.Columns(7).Visible = True
        'dt = objBo.uspGetSiteDocs(hdnid.Value, Request.QueryString("PNo"), Request.QueryString("SC"))
        dt = objutil.ExeQueryDT("Exec uspGetSiteDocs '" & hdnid.Value & "','" & Request.QueryString("PNo") & "','" & Request.QueryString("SC") & "'," & Request.QueryString("wpid"), "GetSiteDocs")
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
                Dim ddlwf As New DropDownList
                ddlwf = grdDoc.Rows(j).Cells(5).FindControl("ddlwf")
                ddlwf.Visible = True
                objbl.fillDDL(ddlwf, "TWorkFlow", False, Constants._DDL_Default_Select)
                ddlwf.SelectedValue = CType(grdDoc.Rows(j).Cells(7).Text, Int16)
            Else
                chkwf.Checked = False
            End If
        Next
        grdDoc.Columns(4).Visible = False
        grdDoc.Columns(6).Visible = False
        grdDoc.Columns(7).Visible = False
    End Sub
    Protected Sub btnEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Dim j As Double = 0
        Dim str As New StringBuilder
        Dim str1 As String = ""
        For i As Integer = 0 To grdDoc.Rows.Count - 1
            Dim aa As HtmlInputText
            aa = CType(grdDoc.Rows(i).Cells(3).FindControl("textperc"), HtmlInputText)
            j = j + Format(Val(aa.Value), "0.00")
            Dim ddl As New DropDownList
            ddl = CType(grdDoc.Rows(i).Cells(4).FindControl("ddlwf"), DropDownList)
            If ddl.Visible = True Then
                If ddl.SelectedItem.Text <> "No Values" Then
                    str1 = " Update Sitedoc Set Percentage = " & aa.Value & ",WF_Id=" & ddl.SelectedValue & " Where SW_Id = " & grdDoc.DataKeys(i).Value
                Else
                    ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('Please select the process flow');", True)
                    Exit Sub
                End If
                str.Append(str1)
            End If
        Next
        If j > 99 Then
            j = objBo.uspUpdateDocPerc(str.ToString)
            If j = 1 Then
                Response.Redirect("frmPOSiteList.aspx?type=N")
            End If
        Else
            Dim stral As String = "Total must be equal to 100, current is " + j.ToString
            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('" & stral & "');", True)
        End If
        'bugfix110223 to update the process flow users id in the wftransaction
        objutil.ExeQuery("exec updateWFTransactionUserID1wpid '" & Request.QueryString("wpid") & "' ")
    End Sub
    Sub AuditTrail(ByVal str As String)
        objET.PoNo = Request.QueryString("pno")
        objET.SiteId = ddlWF.SelectedItem.Value
        objET.DocId = str.Replace("'", "") 'bugfix110406 error during checklist creation
        objET.Roleid = Session("Role_Id")
        objET.Userid = Session("User_Id")
        objET.Task = Constants.AuditTrailCreated_Task
        objET.Status = Constants.AuditTrail_Status
        objET.fldtype = Request.QueryString("SC").ToString
        'objBOAT.uspAuditTrailI(objET)
        dbutils_nsn.InsertAuditTrailNew(objET, Request.QueryString("wpid"))
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
        Else
        End If
    End Sub
End Class