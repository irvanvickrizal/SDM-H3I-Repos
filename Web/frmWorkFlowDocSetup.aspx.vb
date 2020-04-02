Imports BusinessLogic
Imports Common
Imports System.Data
Imports System.Web.UI
Partial Class frmWorkFlowDocSetup
    Inherits System.Web.UI.Page
    Dim dt As DataTable
    Dim objBo As New BOWTDoc
    Dim objbl As New BODDLs
    Dim strSql As String
    Dim objutil As New DBUtil
    Protected Sub btnCancel_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.ServerClick
        Response.Redirect("frmWFDoc.aspx")
    End Sub
    Sub BindTree()
        FillSection()
        TreeView1.ExpandAll()
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Page_size") Is Nothing Then Response.Redirect("~\SessionTimeout.aspx")
        btnSave.Attributes.Add("onclick", "javascript:return checkIsEmpty();")
        If Page.IsPostBack = False Then
            objbl.fillDDL(ddlWF, "TWorkFlow", True, Constants._DDL_Default_Select)
            ddlWF.SelectedValue = Request.QueryString("id")
            ddlWF.Enabled = False
            TreeView1.Visible = True
            BindTree()
            checkTree()
        End If
    End Sub
    Sub checkTree()
        dt = objBo.uspWFDocList(ddlWF.SelectedValue)
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
                            Dim a As Integer = SelectChildrenRecursive(cNode, dt.Rows(j).Item(0))
                            If a = 1 Then
                                cNode.Expand()
                            End If
                        Next

                    End If
                Next
            Next
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
    Sub FillSection()
        dt = objutil.ExeQueryDT("Select C.Doc_Id,DocName from coddoc C Left Outer join CodWFDoc W on W.Doc_Id=C.Doc_Id where Parent_id=0 order by SerialNo", "CODDoc")
        If dt.Rows.Count > 0 Then
            For Each drow As DataRow In dt.Rows
                Dim node As New TreeNode
                Dim str As New StringBuilder
                str.Append(drow.Item("DocName").ToString & "    ")
                'str.Append("<Img ID='Image1' src='Images/ok.jpg' style='border:none' />")
                node.Text = str.ToString  'drow.Item("txt") & ""
                node.Value = drow.Item("Doc_Id") & ""
                'node.PopulateOnDemand = True
                node.SelectAction = TreeNodeSelectAction.Select   'TreeNodeSelectAction.SelectExpand
                If node.Value < 1000 Then node.ShowCheckBox = False
                TreeView1.Nodes.Add(node)
                FillChild(node)
            Next
        Else
            TreeView1.Nodes.Clear()
            Dim tn As New TreeNode
            tn.Text = "Documents"
            tn.Value = 0
            TreeView1.Nodes.Add(tn)
            FillChild(tn)
        End If
    End Sub
    Sub FillChild(ByVal tn As TreeNode)
        If tn.Value <> 0 Then
            dt = objBo.uspBOTaskSubDoc(tn.Value, Constants._WFReq)
            For Each drow As DataRow In dt.Rows
                Dim node As New TreeNode
                node.Text = drow.Item("txt") & "  "
                node.Value = drow.Item("val") & ""
                'node.PopulateOnDemand = True
                node.SelectAction = TreeNodeSelectAction.Select   'TreeNodeSelectAction.SelectExpand
                tn.ChildNodes.Add(node)
                If drow.Item("cc").ToString > 0 Then FillChild(node)
            Next
        End If
    End Sub
    Sub CheckTreeNodeRecursive(ByVal parent As TreeNode, ByVal fCheck As Boolean)
        Dim child As TreeNode
        For Each child In parent.ChildNodes
            If child.Checked <> fCheck Then
                child.Checked = fCheck
            End If
            If child.ChildNodes.Count > 0 Then
                CheckTreeNodeRecursive(child, fCheck)
            End If
        Next
    End Sub
    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim str As String = ""
        For Each pchild As TreeNode In TreeView1.CheckedNodes
            If pchild.Value > Constants._MinDocStart Then str = str & IIf(str = "", "", ",") & "'" & pchild.Value & "'"
        Next
        Dim strDocName As String = ""
        If str <> "" Then
            dt = objBo.uspWFDocCheck(ddlWF.SelectedValue, str)
            If dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    strDocName = strDocName & IIf(strDocName = "", "", ",") & "" & dt.Rows(i).Item("DocName") & ""
                Next
            End If
        End If
        If strDocName <> "" Then
            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('Already assign work flow for given document (" & strDocName & ") .Please uncheck and then click into the save.');", True)
        Else
            Dim i As Integer = -1
            If str <> "" Then
                i = objBo.uspBoWTDocInsert(ddlWF.SelectedValue, Constants.STATUS_ACTIVE, Session("UserName"), str)
                If i = 0 Then
                    ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('Error while doing the transaction.');", True)
                ElseIf i = 1 Then
                    ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('Data Saved Sucessfully.');", True)
                    Response.Redirect("frmWFDoc.aspx")
                End If
            Else
                If objutil.ExeQueryScalar("Select Count(*) from sitedoc where WF_id=" & ddlWF.SelectedValue) = 0 Then
                    i = objBo.uspBoWTDocInsert(ddlWF.SelectedValue, Constants.STATUS_ACTIVE, Session("UserName"), str)
                    If i = 0 Then
                        ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('Error while doing the transaction.');", True)
                    ElseIf i = 1 Then
                        ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('Data Saved Sucessfully.');", True)
                        Response.Redirect("frmWFDoc.aspx")
                    End If
                Else
                    ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('Process flow have transaction Data...');", True)
                End If
            End If
        End If
    End Sub
End Class