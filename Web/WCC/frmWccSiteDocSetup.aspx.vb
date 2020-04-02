Imports BusinessLogic
Imports Common
Imports System.Data
Imports System.Web.UI
Imports Entities
Partial Class WCC_frmWccSiteDocSetup
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
    Protected Sub btnCancel_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.ServerClick
        Response.Redirect("~/WCC/frmWccPOSiteList.aspx")
    End Sub
    Sub BindTree()
        FillSection()
        TreeView1.ExpandAll()
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        If Page.IsPostBack = False Then
            objbl.fillDDL(ddlWF, "CODSite", True, Constants._DDL_Default_Select)
            hdnid.Value = objBo.uspGetSiteId(Request.QueryString("id"))
            hdnScope.Value = objutil.ExeQueryScalarString("Select Scope from wccPODetails where PO_Id=" & Request.QueryString("sno"))
            hdnScope.Value = objutil.ExeQueryScalar("Select Top 1 Scope_Id from wccCodScope where Scope = '" & hdnScope.Value.Replace("'", "''") & "' and Rstatus = 2")
            If hdnid.Value = "" Then hdnid.Value = 0
            ddlWF.SelectedValue = hdnid.Value 'Request.QueryString("id")
            lblSite.Text = ddlWF.SelectedItem.Text & "-" & Request.QueryString("SC").ToString
            Dim sqlStr As String = "Select IsNull(REPLACE(CONVERT(VARCHAR(9), SiteIntegration, 6), ' ', '/'),'')SiteIntegration from epmData where siteid='" & ddlWF.SelectedItem.Text & "'"
            lblIntDate.InnerText = objutil.ExeQueryScalarString(sqlStr)
            ddlWF.Visible = False
            TreeView1.Visible = True
            BindTree()
            checkTree()
            BindDoc()
        End If
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
            Else
                a1.Checked = False
            End If
        Next
    End Sub
    Sub checkTree()
        dt = objBo.uspWccSiteDocList(ddlWF.SelectedValue, Request.QueryString("PNo"), "", Request.QueryString("SC"))
        TreeView1.ExpandAll()
        If dt.Rows.Count > 0 Then ClearNodes()
        If dt.Rows.Count > 0 Then
            For j As Integer = 0 To dt.Rows.Count - 1
                For Each Pnode As TreeNode In TreeView1.Nodes
                    If Pnode.Value = dt.Rows(j).Item("Doc_Id") Then
                        Pnode.Expand()
                        Pnode.Checked = True
                        Exit For
                    End If
                    If Pnode.ChildNodes.Count > 0 Then
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
                                    Exit For
                                End If
                            End If
                        Next

                    End If
                Next
            Next
            TreeView1.Enabled = False
            btnSave.Enabled = False
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
    End Function
    Sub FillSection()
        dt = objutil.ExeQueryDT("Select C.Doc_Id,DocName,DocType,IsNull(WFId,0)WFId,Appr_Required from WCCcoddoc C Left Outer join CodWFDoc W on W.Doc_Id=C.Doc_Id where Parent_id=0 order by SerialNo", "CODDoc")
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
                    node.ShowCheckBox = False
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
    Sub fillchild(ByVal tn As TreeNode)
        'dt = objutil.ExeQueryDT("Select Doc_Id,DocName,DocType(select count(*) from coddoc as c where c.parent_id=parent_Id) as cc from codDoc where parent_Id = " & tn.Value & " group by parent_id,Doc_Id,DocName", "CODDoc")
        dt = objBo.uspWccTICODSubSectionDoc(tn.Value, , hdnScope.Value)
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
                    node.ShowCheckBox = False
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
            tn.ChildNodes.Add(node)
            If drow.Item("cc").ToString > 0 Then fillchild(node)
        Next

    End Sub
    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim str As String = ""
        Dim SCount As Integer = 0
        Dim strPO As String = Request.QueryString("PNo")
        For Each pchild As TreeNode In TreeView1.CheckedNodes
            str = str & IIf(str = "", "", ",") & "'" & pchild.Value & "'" : SCount = SCount + 1
        Next
        If str <> "" Then
            Dim parentdt As DataTable = objutil.ExeQueryDT("select parent_Id from WccCodDoc where doc_id in(" & str.Replace("'", "") & ") and parent_Id not in(" & str.Replace("'", "") & ",0)", "WccCODDoc")
            If parentdt.Rows.Count > 0 Then
                For pd As Integer = 0 To parentdt.Rows.Count - 1
                    str = str & IIf(str = "", "", ",") & "'" & parentdt.Rows(pd).Item("Parent_Id") & "'" : SCount = SCount + 1
                Next
            End If
            Dim i As Integer = -1
            Dim perc As Double = 0
            perc = Format(100 / SCount, "0.00")
            Dim version As Integer = 0
            version = objBo.getsiteversion(Request.QueryString("PNo"), ddlWF.SelectedItem.Text, Request.QueryString("SC"))
            i = objBo.uspWccSiteDocIU(ddlWF.SelectedValue, Constants.STATUS_ACTIVE, Session("User_Name"), str, perc, 1, version, , strPO)
            If i = 0 Then
                ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('Error while doing the transaction.');", True)
            ElseIf i = 1 Then
                btnSave.Enabled = False
                AuditTrail(str)
                BindDoc()
                objutil.ExeNonQuery("update WccSiteDoc set rstatus=4,isuploaded=1 where siteid=" & ddlWF.SelectedValue & " and version=" & version & " and pono='" & strPO & "' and docid=" & ConfigurationManager.AppSettings("WCTRBASTID") & "")
            End If
        Else
            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('Please select Documents');", True)
        End If
    End Sub

    Sub BindDoc()
        Dim j As Integer
        grdDoc.Columns(6).Visible = True
        grdDoc.Columns(7).Visible = True

        dt = objBo.uspWccGetSiteDocs(hdnid.Value, Request.QueryString("PNo"), Request.QueryString("SC"))
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
    End Sub

    Protected Sub btnEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Dim j As Double = 0
        Dim str As New StringBuilder
        Dim str1 As String = ""
        For i As Integer = 0 To grdDoc.Rows.Count - 1
            Dim aa As HtmlInputText
            aa = CType(grdDoc.Rows(i).Cells(3).FindControl("textperc"), HtmlInputText)
            j = j + Format(Val(aa.Value), "0.00")
            Dim chk As CheckBox
            chk = CType(grdDoc.Rows(i).Cells(4).FindControl("chkwf"), CheckBox)
            If chk.Checked = True And grdDoc.Rows(i).Cells(6).Text = False And grdDoc.Rows(i).Cells(7).Text = 0 Then
                Dim ddl As DropDownList
                ddl = CType(grdDoc.Rows(i).Cells(5).FindControl("ddlwf"), DropDownList)
                If ddl.SelectedItem.Text <> "No Values" Then
                    str1 = " Update WccSiteDoc Set Percentage = " & aa.Value & ",WF_Id=" & ddl.SelectedValue & " Where SW_Id = " & grdDoc.DataKeys(i).Value
                Else
                    ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('Please select the process flow');", True)
                    Exit Sub
                End If
            ElseIf chk.Checked = False And grdDoc.Rows(i).Cells(6).Text = True Then
                str1 = " Update WccSiteDoc Set Percentage = " & aa.Value & ",WF_Id = 0 where SW_Id = " & grdDoc.DataKeys(i).Value
            Else
                str1 = " Update WccSiteDoc Set Percentage = " & aa.Value & " where SW_Id = " & grdDoc.DataKeys(i).Value
            End If
            str.Append(str1)
        Next
        If j > 99 Then
            j = objBo.uspUpdateDocPerc(str.ToString)
            If j = 1 Then
                Response.Redirect("frmPOSiteList.aspx?type=N")
            End If
        Else
            Dim stral As String = "Total must be equal to 100, current is " + j.ToString
            'Response.Write("<script language='javascript'>alert('" & stral & "');</script>")
            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('" & stral & "');", True)
        End If
    End Sub
    Sub AuditTrail(ByVal str As String)
        objET.PoNo = Request.QueryString("pno")
        objET.SiteId = ddlWF.SelectedItem.Value
        objET.DocId = str
        objET.Roleid = Session("Role_Id")
        objET.Userid = Session("User_Id")
        objET.Task = Constants.AuditTrailCreated_Task
        objET.Status = Constants.AuditTrail_Status
        objET.fldtype = Request.QueryString("SC").ToString
        objBOAT.uspAuditTrailI(objET)

        'BOcommon.result(objBOAT.uspAuditTrailI(objET), True, "", "", Constants._INSERT)
    End Sub
    Protected Sub grdDoc_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdDoc.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            totperc = totperc + Val(e.Row.Cells(2).Text)
            'e.Row.Cells(2).Text = Format(Val(e.Row.Cells(2).Text), "0.00")
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
